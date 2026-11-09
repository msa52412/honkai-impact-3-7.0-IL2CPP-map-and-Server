using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PemukulPaku.GameServer;
using Common.Utils.ExcelReader;

namespace HttpServer.Controllers
{
	/// <summary>
	/// GM 面板的「卡池设置」—— 改扩充池 / 精准池的阵容，不用手编 json。
	///
	///   GET  /gm/gacha              编辑页（run\webui\gacha.html）
	///   GET  /gm/api/gacha/index    候选清单（角色 / 武器 / 圣痕 / 完整套装，全星级）
	///   GET  /gm/api/gacha/state    两个池当前生效的阵容
	///   POST /gm/api/gacha/pool1    {s, a:[4]}                      扩充池
	///   POST /gm/api/gacha/pool2    {upWeapon, weapons:[6], upStigmata:[3], sets:[[3]x4]}
	///
	/// 候选清单从哪来
	/// --------------
	/// `docs\物品表_角色_武器_圣痕.xlsx` 由 `tools\gen_gacha_index.py` 摊平成
	/// `run\webui\gacha_index.json`（xlsx 比它新就自动重建，和配置表页同一套路）。
	/// 浏览器端拿它做「按 id / 中文名搜索补全」。
	///
	/// 写文件的两个原则
	/// ----------------
	/// 1. **只动该动的键**：用 JObject 读池文件，只替换 drops / content（扩充池再加上
	///    up_avatar_id / display_video_avatar），其余字段（bg_image、video_cg_key、
	///    ex_id、保底次数…）和键顺序原样保留。整份反序列化成 GachaPool 再序列化回去
	///    会把没配的字段补成默认值，比如给精准池塞一个空的 video_cg_key。
	/// 2. 保留同种类、同 UP 分类、同物品的独立权重（包括零权重）。
	///    新物品沿用同种类、同分类的权重；没有可参考项才用本地历史默认值。
	///    本地默认权重不代表已核实的官服概率。
	///
	/// 角色掉落用的是 **avatarCardID**（60803 这种），不是 avatarID —— 客户端要靠
	/// AvatarCardData 才能把结算页画成角色卡。索引里已经带好了 card 字段。
	/// </summary>
	public static class GmGachaController
	{
		private const string INDEX_NAME = "gacha_index.json";
		private const string POOL1 = "gacha_pool_1.json";
		private const string POOL2 = "gacha_pool_2.json";

		private static readonly object BuildLock = new object();
		private static readonly object WriteLock = new object();

		// ---------------------------------------------------------------- 注册

		public static void AddHandlers(WebApplication app)
		{
			app.MapGet("/gm/gacha", (HttpContext ctx) => Guard(ctx, () => ServePage(ctx)));
			app.MapGet("/gm/api/gacha/index", (HttpContext ctx) => Guard(ctx, () => ServeIndex(ctx)));
			app.MapGet("/gm/api/gacha/state", (HttpContext ctx) => Guard(ctx, () => GetState(ctx)));
			app.MapPost("/gm/api/gacha/pool1", (HttpContext ctx) => Guard(ctx, () => SavePool1(ctx)));
			app.MapPost("/gm/api/gacha/pool2", (HttpContext ctx) => Guard(ctx, () => SavePool2(ctx)));
            app.MapPost("/gm/api/gacha/new-period", (HttpContext ctx) => Guard(ctx, () => NewPeriod(ctx)));
		}

        public sealed class NewPeriodRequest {public int Pool {get;set;}}
        private static async Task NewPeriod(HttpContext ctx)
        {
            if(!GmController.IsAllowed(ctx)){await GmController.Deny(ctx);return;}
            var req=await ctx.Request.ReadFromJsonAsync<NewPeriodRequest>();
            if(req==null || (req.Pool!=1 && req.Pool!=2)){await Fail(ctx,"卡池只能选择1或2");return;}
            string name=req.Pool==1?POOL1:POOL2;
            lock(WriteLock)
            {
                var pool=ReadPool(name);
                if(!(pool["enabled"]?.Value<bool>()??false))throw new InvalidOperationException("请先启用并保存卡池");
                pool["period_key"]=Guid.NewGuid().ToString("N");
                // The new main period also controls wish allowance; discard a manual old wish override.
                pool.Remove("wish_period_key");
                WritePool(name,pool);
            }
            await ctx.Response.WriteAsJsonAsync(new{ok=true,output=req.Pool==1?"扩充新一期已开启：百抽S保护继承，本期能量与十抽保护重置。重开补给页即可刷新。":"精准新一期已开启：本期50抽保护、UP收集与许愿次数刷新，十抽保护继承。重开补给页即可刷新。"});
        }

		private static async Task Guard(HttpContext ctx, Func<Task> body)
		{
			try
			{
				await body();
			}
			catch (Exception ex)
			{
				Common.Utils.Logger c = new Common.Utils.Logger("GM", ConsoleColor.Magenta, traceOnError: false);
				c.Warn("卡池接口异常：" + ex);
				if (!ctx.Response.HasStarted)
				{
					ctx.Response.StatusCode = 200;
					await ctx.Response.WriteAsJsonAsync(new { ok = false, output = "内部错误：" + ex.GetType().Name + " - " + ex.Message });
				}
			}
		}

		// ---------------------------------------------------------------- 路径

		private static string RunDir => Directory.GetCurrentDirectory();

		/// <summary>run\ -> 项目根。</summary>
		private static string ProjectRoot =>
			Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", ".."));

		private static string IndexPath => Path.Combine(RunDir, "webui", INDEX_NAME);
		private static string XlsxPath => Path.Combine(ProjectRoot, "docs", "物品表_角色_武器_圣痕.xlsx");
		private static string GeneratorPath => Path.Combine(ProjectRoot, "tools", "gen_gacha_index.py");
		private static string PythonPath => Path.Combine(ProjectRoot, "tools", "python", "python.exe");
		private static string PagePath => Path.Combine(RunDir, "webui", "gacha.html");

		// ---------------------------------------------------------------- 页面 / 索引

		private static async Task ServePage(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			if (!File.Exists(PagePath))
			{
				ctx.Response.ContentType = "text/html; charset=utf-8";
				await ctx.Response.WriteAsync("<meta charset=\"utf-8\"><p>找不到页面 " + PagePath + "</p>");
				return;
			}

			ctx.Response.ContentType = "text/html; charset=utf-8";
			await ctx.Response.WriteAsync(File.ReadAllText(PagePath, Encoding.UTF8));
		}

		private static async Task ServeIndex(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			string note = EnsureIndex();
			if (!File.Exists(IndexPath))
			{
				await ctx.Response.WriteAsJsonAsync(new { ok = false, output = note ?? "索引文件不存在：" + IndexPath });
				return;
			}

			ctx.Response.ContentType = "application/json; charset=utf-8";
			await ctx.Response.WriteAsync(File.ReadAllText(IndexPath, Encoding.UTF8));
		}

		/// <summary>xlsx 比索引新就重建一次。返回 null 表示没问题，否则是失败原因。</summary>
		private static string EnsureIndex()
		{
			lock (BuildLock)
			{
				bool need = !File.Exists(IndexPath)
					|| (File.Exists(XlsxPath) && File.GetLastWriteTimeUtc(XlsxPath) > File.GetLastWriteTimeUtc(IndexPath));
				if (!need)
				{
					return null;
				}
				if (!File.Exists(GeneratorPath) || !File.Exists(PythonPath))
				{
					return File.Exists(IndexPath) ? null : "找不到生成器或内嵌 python";
				}

				try
				{
					ProcessStartInfo psi = new ProcessStartInfo
					{
						FileName = PythonPath,
						WorkingDirectory = ProjectRoot,
						UseShellExecute = false,
						RedirectStandardOutput = true,
						RedirectStandardError = true,
						CreateNoWindow = true,
						StandardOutputEncoding = Encoding.UTF8,
						StandardErrorEncoding = Encoding.UTF8
					};
					psi.ArgumentList.Add(GeneratorPath);
					psi.ArgumentList.Add("--apply");
					psi.Environment["PYTHONIOENCODING"] = "utf-8";

					using Process p = Process.Start(psi);
					if (p == null)
					{
						return "生成器进程启动失败";
					}
					string stdout = p.StandardOutput.ReadToEnd();
					string stderr = p.StandardError.ReadToEnd();
					if (!p.WaitForExit(30000))
					{
						try { p.Kill(true); } catch { /* 忽略 */ }
						return "生成索引超时（30 秒）";
					}
					if (p.ExitCode != 0)
					{
						return "生成索引失败：" + Shorten(string.IsNullOrWhiteSpace(stderr) ? stdout : stderr, 400);
					}

					Common.Utils.Logger c = new Common.Utils.Logger("GM", ConsoleColor.Magenta, traceOnError: false);
					c.Log("[卡池] 已重新生成候选清单：" + Shorten(stdout.Trim(), 160));
					return null;
				}
				catch (Exception ex)
				{
					return "调用生成器异常：" + ex.Message;
				}
			}
		}

		private static GachaIndex LoadIndex()
		{
			string note = EnsureIndex();
			if (!File.Exists(IndexPath))
			{
				throw new InvalidOperationException(note ?? "候选清单不存在，先跑 tools\\gen_gacha_index.py --apply");
			}
			GachaIndex idx = JsonConvert.DeserializeObject<GachaIndex>(File.ReadAllText(IndexPath, Encoding.UTF8));
			if (idx == null)
			{
				throw new InvalidOperationException("候选清单解析成空");
			}
			idx.Normalize();
            idx.Avatars.RemoveAll(a => !GachaEligibility.IsRegularAvatar(a.Id));
			return idx;
		}

		// ---------------------------------------------------------------- 查询

		private static async Task GetState(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			GachaIndex idx = LoadIndex();
			JObject p1 = ReadPool(POOL1);
			JObject p2 = ReadPool(POOL2);

			// ---- 扩充池：featured 的那张角色卡 = UP S 级，其余 = A 级
			List<int> s = new List<int>(), a = new List<int>();
			foreach (JObject d in Drops(p1))
			{
				if (!IsKind(d, "avatarCard")) continue;
				int card = IntOf(d, "id");
				GachaIdxAvatar av = idx.AvatarByCard(card);
				if (av == null) continue;
				if (BoolOf(d, "featured")) s.Add(av.Id); else a.Add(av.Id);
			}

			// ---- 精准池
			int upWeapon = 0;
			List<int> weapons = new List<int>(), upStigma = new List<int>();
			List<List<int>> sets = new List<List<int>>();
			Dictionary<string, List<int>> byBase = new Dictionary<string, List<int>>();
			List<string> baseOrder = new List<string>();
			foreach (JObject d in Drops(p2))
			{
				bool featured = BoolOf(d, "featured");
				int id = IntOf(d, "id");
				if (IsKind(d, "weapon"))
				{
					if (featured) upWeapon = id; else weapons.Add(id);
				}
				else if (IsKind(d, "stigmata"))
				{
					if (featured)
					{
						upStigma.Add(id);
					}
					else
					{
						GachaIdxItem it = idx.StigmataById(id);
						string b = it?.BaseName ?? ("?" + id);
						if (!byBase.ContainsKey(b)) { byBase[b] = new List<int>(); baseOrder.Add(b); }
						byBase[b].Add(id);
					}
				}
			}
			foreach (string b in baseOrder) sets.Add(byBase[b]);
			upStigma = upStigma.OrderBy(x => idx.StigmataById(x)?.PosRank ?? 9).ToList();

			await ctx.Response.WriteAsJsonAsync(new
			{
				ok = true,
				// 读一次 GachaConfig.Summary() 会触发它的 mtime 热加载：
				// 既能让改完的文件立刻生效，也能把「服务端眼里现在是什么」回显出来。
				effective = GachaConfig.Summary(),
				index = new
				{
					avatars = idx.Avatars.Count,
					weapons = idx.Weapons.Count,
					stigmata = idx.Stigmata.Count,
					sets = idx.Sets.Count,
					generated = idx.Generated
				},
				pool1 = new
				{
					file = POOL1,
					title = StrOf(p1, "title"),
					type = IntOf(p1, "gacha_type"),
					s = s.FirstOrDefault(),
					a,
					content = StrOf(p1, "content"),
					drops = Drops(p1).Count()
				},
				pool2 = new
				{
					file = POOL2,
					title = StrOf(p2, "title"),
					type = IntOf(p2, "gacha_type"),
					upWeapon,
					weapons,
					upStigmata = upStigma,
					sets,
					content = StrOf(p2, "content"),
					drops = Drops(p2).Count()
				}
			});
		}

		// ---------------------------------------------------------------- 保存：扩充池

		private static async Task SavePool1(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			Pool1Request req = await ctx.Request.ReadFromJsonAsync<Pool1Request>();
			int sId = req?.s ?? 0;
			List<int> aIds = (req?.a ?? new List<int>()).Where(x => x != 0).ToList();

			GachaIndex idx = LoadIndex();

			GachaIdxAvatar up = idx.Avatars.FirstOrDefault(x => x.Id == sId);
			if (up == null)
			{
				await Fail(ctx, "S 级角色 " + sId + " 不在候选清单里（先在页面上从下拉里选）");
				return;
			}
			if (up.Star != "S")
			{
				await Fail(ctx, "角色 " + sId + " " + up.Name + " 是 " + up.Star + " 级，UP 位只能放 S 级");
				return;
			}
			if (aIds.Count != 4)
			{
				await Fail(ctx, "A 级角色要正好 4 个，现在 " + aIds.Count + " 个");
				return;
			}
			if (aIds.Distinct().Count() != aIds.Count)
			{
				await Fail(ctx, "A 级角色有重复：" + string.Join(", ", aIds));
				return;
			}
			if (aIds.Contains(sId))
			{
				await Fail(ctx, "A 级里不能放 UP 的那个 S 级角色");
				return;
			}
			List<string> badA = new List<string>();
			List<string> namesA = new List<string>();
			foreach (int id in aIds)
			{
				GachaIdxAvatar av = idx.Avatars.FirstOrDefault(x => x.Id == id);
				if (av == null) { badA.Add(id + "（清单里没有）"); continue; }
				if (av.Star != "A") { badA.Add(id + " " + av.Name + " 是 " + av.Star + " 级"); continue; }
				namesA.Add(av.Name);
			}
			if (badA.Count > 0)
			{
				await Fail(ctx, "A 级角色不合法：" + string.Join("；", badA));
				return;
			}

			JObject pool = ReadPool(POOL1);
			int upW = ItemWeight(pool, "avatarCard", up.Card, true, 57);
			

			JArray drops = new JArray();
			drops.Add(NewDrop("avatarCard", up.Card, 3, upW, true, false, "UP S级 " + up.Name));
			foreach (int id in aIds)
			{
				GachaIdxAvatar av = idx.Avatars.First(x => x.Id == id);
				drops.Add(NewDrop("avatarCard", av.Card, 2, ItemWeight(pool, "avatarCard", av.Card, false, 202), false, true, "A级 " + av.Name));
			}

			pool["drops"] = drops;
			pool["content"] = "本期 UP：" + up.Name;
			pool["up_avatar_id"] = sId;
            var avatarRow=BaseExcelReader<AvatarData,AvatarDataExcel>.GetInstance().FromId(sId);
            bool awakened=avatarRow?.AvatarType==1;
            pool["ex_id"]=awakened?2:1;
            pool["ex_energy_need"]=awakened?100:210;
            pool["ex_energy_per_draw"]=50;
			pool["display_video_avatar"] = sId;

			WritePool(POOL1, pool);

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("扩充池已保存（" + POOL1 + "）");
			sb.AppendLine("  UP S级：" + up.Name + "（avatarID " + sId + " → 角色卡 " + up.Card + "，权重 " + upW + "）");
			sb.AppendLine("  A 级  ：" + string.Join(" / ", namesA) + "（沿用各物品权重）");
			sb.AppendLine("  保底次数没动，现有物品保留独立权重。更换物品后的概率以生效卡池为准。客户端退出补给页再进一次即可看到。");
			await ctx.Response.WriteAsJsonAsync(new { ok = true, output = sb.ToString() });
		}

		// ---------------------------------------------------------------- 保存：精准池

		private static async Task SavePool2(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			Pool2Request req = await ctx.Request.ReadFromJsonAsync<Pool2Request>();
			int upWeapon = req?.upWeapon ?? 0;
			List<int> weapons = (req?.weapons ?? new List<int>()).Where(x => x != 0).ToList();
			List<int> upStigma = (req?.upStigmata ?? new List<int>()).Where(x => x != 0).ToList();
			List<List<int>> sets = (req?.sets ?? new List<List<int>>())
				.Select(x => (x ?? new List<int>()).Where(i => i != 0).ToList())
				.Where(x => x.Count > 0).ToList();

			GachaIndex idx = LoadIndex();

			// ---- UP 武器
			GachaIdxItem upWItem = idx.WeaponById(upWeapon);
			if (upWItem == null || !GachaEligibility.IsAllowedWeapon(upWeapon))
			{
				await Fail(ctx, "UP 武器 " + upWeapon + " 不在候选清单里");
				return;
			}
			// ---- 其他武器 6 个
			if (weapons.Count != 6)
			{
				await Fail(ctx, "其他武器要正好 6 个，现在 " + weapons.Count + " 个");
				return;
			}
			List<string> badW = new List<string>();
			foreach (int id in weapons)
			{
				if (idx.WeaponById(id) == null || !GachaEligibility.IsAllowedWeapon(id)) badW.Add(id + "（清单里没有）");
			}
			if (badW.Count > 0)
			{
				await Fail(ctx, "其他武器不合法：" + string.Join("；", badW));
				return;
			}
			List<int> allWeapon = new List<int> { upWeapon };
			allWeapon.AddRange(weapons);
			if (allWeapon.Distinct().Count() != allWeapon.Count)
			{
				await Fail(ctx, "武器有重复：UP 和「其他」里不能放同一把");
				return;
			}

			// ---- UP 圣痕：正好上中下各一件
			if (upStigma.Count != 3)
			{
				await Fail(ctx, "UP 圣痕要正好 3 件（上/中/下），现在 " + upStigma.Count + " 件");
				return;
			}
			string[] wantPos = { "T", "M", "B" };
			string[] posCn = { "上", "中", "下" };
			List<string> badS = new List<string>();
			for (int i = 0; i < 3; i++)
			{
				GachaIdxItem it = idx.StigmataById(upStigma[i]);
				if (it == null) { badS.Add(upStigma[i] + "（清单里没有）"); continue; }
				if (it.Pos != wantPos[i]) badS.Add("第 " + (i + 1) + " 个应是「" + posCn[i] + "」，填的是 " + it.Name);
			}
			if (badS.Count > 0)
			{
				await Fail(ctx, "UP 圣痕不合法：" + string.Join("；", badS));
				return;
			}

			// ---- 其他 4 套，每套 3 件
			if (sets.Count != 4)
			{
				await Fail(ctx, "其他圣痕要正好 4 套，现在 " + sets.Count + " 套");
				return;
			}
			List<string> badSet = new List<string>();
			for (int i = 0; i < sets.Count; i++)
			{
				if (sets[i].Count != 3) { badSet.Add("第 " + (i + 1) + " 套不是 3 件"); continue; }
				List<string> bases = new List<string>();
				List<int> setIds = new List<int>();
				foreach (int id in sets[i])
				{
					GachaIdxItem it = idx.StigmataById(id);
					if (it == null) { badSet.Add("第 " + (i + 1) + " 套里的 " + id + " 清单里没有"); continue; }
					bases.Add(it.BaseName);
					if (it.SetId != 0) setIds.Add(it.SetId);
				}
				if (bases.Count != 3) continue;
				// 同一套的判据：setID 一致（混搭套三件不同名），或者名字一致（兜底老索引）
				bool sameSet = setIds.Count == 3 && setIds.Distinct().Count() == 1;
				bool sameName = bases.Distinct().Count() == 1;
				if (!sameSet && !sameName)
				{
					badSet.Add("第 " + (i + 1) + " 套的三件既不同名也不属于同一套装："
						+ string.Join(" / ", bases));
				}
			}
			if (badSet.Count > 0)
			{
				await Fail(ctx, "其他圣痕不合法：" + string.Join("；", badSet));
				return;
			}

			List<int> allStigma = new List<int>(upStigma);
			foreach (List<int> s in sets) allStigma.AddRange(s);
			if (allStigma.Distinct().Count() != allStigma.Count)
			{
				await Fail(ctx, "圣痕有重复：UP 和其他套之间、或套与套之间不能放同一件");
				return;
			}

			// ---- 写文件
			JObject pool = ReadPool(POOL2);
			int upWt = ItemWeight(pool, "weapon", upWeapon, true, 64);
			

			JArray drops = new JArray();
			drops.Add(NewDrop("weapon", upWeapon, upWItem.DropRarity, upWt, true, true,
				"UP 武器 ★" + upWItem.DropRarity + " " + upWItem.Name));
			foreach (int id in weapons)
			{
				GachaIdxItem it = idx.WeaponById(id);
				drops.Add(NewDrop("weapon", id, it.DropRarity, ItemWeight(pool, "weapon", id, false, 32), false, true,
					"武器 ★" + it.DropRarity + " " + it.Name));
			}
			foreach (int id in upStigma)
			{
				GachaIdxItem it = idx.StigmataById(id);
				drops.Add(NewDrop("stigmata", id, it.DropRarity, ItemWeight(pool, "stigmata", id, true, 64), true, true,
					"UP 圣痕 ★" + it.DropRarity + " " + it.Name));
			}
			List<string> setNames = new List<string>();
			foreach (List<int> s in sets)
			{
				GachaIdxItem first = idx.StigmataById(s[0]);
				setNames.Add(first?.BaseName ?? s[0].ToString());
				foreach (int id in s)
				{
					GachaIdxItem it = idx.StigmataById(id);
					drops.Add(NewDrop("stigmata", id, it.DropRarity, ItemWeight(pool, "stigmata", id, false, 32), false, true,
						"圣痕 ★" + it.DropRarity + " " + it.Name));
				}
			}

			pool["drops"] = drops;
			pool["content"] = "本期 UP：" + upWItem.Name + " + " + UpSetName(idx, upStigma) + "（上/中/下）";

			WritePool(POOL2, pool);

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("精准池已保存（" + POOL2 + "）");
			sb.AppendLine("  UP 武器：" + upWItem.Name + "（权重 " + upWt + "）");
			sb.AppendLine("  其他武器 " + weapons.Count + " 把：" + string.Join(" / ", weapons.Select(x => idx.WeaponById(x).Name)));
			sb.AppendLine("  UP 圣痕：" + string.Join(" / ", upStigma.Select(x => idx.StigmataById(x).Name)));
			sb.AppendLine("  其他圣痕 " + sets.Count + " 套：" + string.Join(" / ", setNames));
			sb.AppendLine("  共 " + drops.Count + " 条掉落，保底次数没动。客户端退出补给页再进一次即可看到。");
			await ctx.Response.WriteAsJsonAsync(new { ok = true, output = sb.ToString() });
		}

		/// <summary>UP 圣痕的套名（去掉「（上）」这类后缀）。</summary>
		private static string UpSetName(GachaIndex idx, List<int> upStigma)
		{
			List<string> bases = upStigma
				.Select(x => idx.StigmataById(x)?.BaseName)
				.Where(x => !string.IsNullOrEmpty(x))
				.Distinct()
				.ToList();
			return bases.Count == 0 ? "圣痕" : string.Join(" + ", bases);
		}

		// ---------------------------------------------------------------- 文件读写

		private static JObject ReadPool(string name)
		{
			string path = Path.Combine(RunDir, name);
			if (!File.Exists(path))
			{
				throw new FileNotFoundException("卡池文件不存在：" + path);
			}
			JObject obj = JObject.Parse(File.ReadAllText(path, Encoding.UTF8));
			if (!(obj["drops"] is JArray))
			{
				obj["drops"] = new JArray();
			}
			return obj;
		}

		private static void WritePool(string name, JObject pool)
		{
			string path = Path.Combine(RunDir, name);
			lock (WriteLock)
			{
				// 按用户约定，GM生成的卡池备份统一写入E盘。
				try
				{
					string bak = @"E:\codex\bh3\official70-repair\gm-backups";
					Directory.CreateDirectory(bak);
					if (File.Exists(path))
					{
						File.Copy(path, Path.Combine(bak, name + "." + DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + "_" + Guid.NewGuid().ToString("N")[..8]), true);
					}
				}
				catch (Exception ex)
				{
					Common.Utils.Logger c = new Common.Utils.Logger("GM", ConsoleColor.Magenta, traceOnError: false);
					c.Warn("[卡池] 备份失败（继续写）：" + ex.Message);
				}

				string json = pool.ToString(Formatting.Indented);
				JObject check = JObject.Parse(json);   // 写之前先自检一遍，别把池写坏
				if (check["drops"] == null)
				{
					throw new InvalidOperationException("生成的 drops 是空的，拒绝写文件");
				}
				File.WriteAllText(path, json, new UTF8Encoding(false));
			}

			Common.Utils.Logger lg = new Common.Utils.Logger("GM", ConsoleColor.Magenta, traceOnError: false);
			lg.Log("[卡池] 已写入 " + name + "（" + ((JArray)pool["drops"]).Count + " 条掉落）");
		}

		private static JObject NewDrop(string kind, int id, int rarity, int weight,
			bool featured, bool pity, string note)
		{
			JObject d = new JObject
			{
				["kind"] = kind,
				["id"] = id,
				["rarity"] = rarity,
				["num"] = 1,
				["weight"] = weight
			};
			if (featured) d["featured"] = true;
			if (pity) d["pity"] = true;
			d["note"] = note;
			return d;
		}

		private static IEnumerable<JObject> Drops(JObject pool)
		{
			if (pool["drops"] is JArray arr)
			{
				foreach (JToken t in arr)
				{
					if (t is JObject o) yield return o;
				}
			}
		}

		/// <summary>Keep per-item weights; only new items inherit their own kind/category.</summary>
        private static int ItemWeight(JObject pool, string kind, int id, bool featured, int fallback)
        {
            var category = Drops(pool).Where(d => IsKind(d, kind) && BoolOf(d, "featured") == featured).ToList();
            var exact = category.FirstOrDefault(d => IntOf(d, "id") == id);
            int? existing = exact?["weight"]?.Value<int>();
            if (existing.HasValue && existing.Value >= 0) return existing.Value;
            foreach (var d in category)
            {
                int? weight = d["weight"]?.Value<int>();
                if (weight.HasValue && weight.Value > 0) return weight.Value;
            }
            return fallback;
        }

		private static bool IsKind(JObject d, string kind) =>
			string.Equals(StrOf(d, "kind"), kind, StringComparison.OrdinalIgnoreCase);

		private static string StrOf(JObject o, string key) => o[key]?.Type == JTokenType.String ? (string)o[key] : "";

		private static int IntOf(JObject o, string key)
		{
			JToken t = o[key];
			if (t == null || t.Type == JTokenType.Null) return 0;
			int v;
			return int.TryParse(t.ToString(), out v) ? v : 0;
		}

		private static bool BoolOf(JObject o, string key) =>
			o[key]?.Type == JTokenType.Boolean && (bool)o[key];

		// ---------------------------------------------------------------- 工具

		private static async Task Fail(HttpContext ctx, string message)
		{
			await ctx.Response.WriteAsJsonAsync(new { ok = false, output = message });
		}

		private static string Shorten(string s, int max)
		{
			if (string.IsNullOrEmpty(s)) return "";
			s = s.Replace("\r", " ").Replace("\n", " ").Trim();
			return s.Length <= max ? s : s.Substring(0, max) + "…";
		}

		// ---------------------------------------------------------------- DTO

		private sealed class Pool1Request
		{
			/// <summary>UP 的 S 级角色（avatarID）。</summary>
			public int s { get; set; }
			/// <summary>4 个 A 级角色（avatarID）。</summary>
			public List<int> a { get; set; }
		}

		private sealed class Pool2Request
		{
			public int upWeapon { get; set; }
			public List<int> weapons { get; set; }
			public List<int> upStigmata { get; set; }
			public List<List<int>> sets { get; set; }
		}
	}

	// -------------------------------------------------------------------- 候选清单

	internal sealed class GachaIdxAvatar
	{
		[JsonProperty("id")] public int Id { get; set; }
		[JsonProperty("card")] public int Card { get; set; }
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("alias")] public string Alias { get; set; }
		[JsonProperty("star")] public string Star { get; set; }
	}

	internal sealed class GachaIdxItem
	{
		[JsonProperty("id")] public int Id { get; set; }
		[JsonProperty("name")] public string Name { get; set; }
		/// <summary>星级 = maxRarity（2~6）。drop 里的 rarity 就用它，4★/6★ 也能如实记录。</summary>
		[JsonProperty("star")] public int Star { get; set; }
		[JsonProperty("ids")] public List<int> Ids { get; set; }
		[JsonProperty("ph")] public bool Placeholder { get; set; }
		[JsonProperty("pos")] public string Pos { get; set; }
		[JsonProperty("base")] public string BaseName { get; set; }
		/// <summary>圣痕所属套装 = StigmataData.setID。混搭套三个部位不同名，只能靠它判断同一套。</summary>
		[JsonProperty("set")] public int SetId { get; set; }

		internal int PosRank => Pos == "T" ? 0 : Pos == "M" ? 1 : Pos == "B" ? 2 : 9;
		/// <summary>写进 drop 的 rarity；索引里没带 star 时按 5 兜底（老索引兼容）。</summary>
		internal int DropRarity => Star > 0 ? Star : 5;
	}

	internal sealed class GachaIdxSet
	{
		[JsonProperty("name")] public string Name { get; set; }
		[JsonProperty("t")] public int T { get; set; }
		[JsonProperty("m")] public int M { get; set; }
		[JsonProperty("b")] public int B { get; set; }
	}

	internal sealed class GachaIndex
	{
		[JsonProperty("generated")] public string Generated { get; set; }
		[JsonProperty("avatars")] public List<GachaIdxAvatar> Avatars { get; set; }
		[JsonProperty("weapons")] public List<GachaIdxItem> Weapons { get; set; }
		[JsonProperty("stigmata")] public List<GachaIdxItem> Stigmata { get; set; }
		[JsonProperty("sets")] public List<GachaIdxSet> Sets { get; set; }

		private Dictionary<int, GachaIdxAvatar> _byCard;
		private Dictionary<int, GachaIdxItem> _weaponById;
		private Dictionary<int, GachaIdxItem> _stigmaById;

		internal void Normalize()
		{
			Avatars ??= new List<GachaIdxAvatar>();
			Weapons ??= new List<GachaIdxItem>();
			Stigmata ??= new List<GachaIdxItem>();
			Sets ??= new List<GachaIdxSet>();

			_byCard = new Dictionary<int, GachaIdxAvatar>();
			foreach (GachaIdxAvatar a in Avatars)
			{
				if (a.Card != 0) _byCard[a.Card] = a;
			}

			_weaponById = new Dictionary<int, GachaIdxItem>();
			foreach (GachaIdxItem it in Weapons)
			{
				foreach (int id in it.Ids ?? new List<int> { it.Id }) _weaponById[id] = it;
				_weaponById[it.Id] = it;
			}

			_stigmaById = new Dictionary<int, GachaIdxItem>();
			foreach (GachaIdxItem it in Stigmata)
			{
				foreach (int id in it.Ids ?? new List<int> { it.Id }) _stigmaById[id] = it;
				_stigmaById[it.Id] = it;
			}
		}

		internal GachaIdxAvatar AvatarByCard(int card) =>
			_byCard != null && _byCard.TryGetValue(card, out GachaIdxAvatar a) ? a : null;

		internal GachaIdxItem WeaponById(int id) =>
			_weaponById != null && _weaponById.TryGetValue(id, out GachaIdxItem it) ? it : null;

		internal GachaIdxItem StigmataById(int id) =>
			_stigmaById != null && _stigmaById.TryGetValue(id, out GachaIdxItem it) ? it : null;
	}
}
