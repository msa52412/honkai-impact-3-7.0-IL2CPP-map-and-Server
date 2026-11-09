using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PemukulPaku.GameServer;
using PemukulPaku.GameServer.Game;

namespace HttpServer.Controllers
{
	/// <summary>
	/// GM 面板的「深渊」控制 —— 三套深渊各自独立，互不影响。
	///
	///   迪拉克之海(type=2)  ID = battle_config_id     温度 = hard_level
	///   量子奇点  (type=3)  ID = qt_battle_config_id  温度 = qt_hard_level（新增，回退 hard_level）
	///   超弦空间  (type=4)  ID = site_groups 组号     温度 = dynamic_hard_level
	///
	/// 每套都只有两个输入 + 两个动作：切换配置 / 清空记录。
	///
	/// 数据落在哪
	/// ----------
	///   配置   run\abyss_ow.json（前两套）/ run\ultra_endless.json（超弦）
	///          这两个文件都是按 mtime 热加载，写完下一次请求就生效，不用重启。
	///   进度   run\data\abyss_state.json / run\data\ultra_endless_state.json
	///          ★ 清空必须走 State.Reset()（内存字典 + 落盘），只删 json 会被内存值写回来。
	///
	/// 切换配置同时会把该套设为「当前深渊」（AbyssConfig.SelectedEndlessType），
	/// 否则改了配置但挑战面板还指着另一套，看起来像没生效。
	///
	/// ★ 一律用 System.Text.Json.Nodes，不要用 Newtonsoft 的 JObject：
	///   WriteAsJsonAsync 走 STJ，JObject 会被反射成内部字段的嵌套数组。
	/// </summary>
	public static class GmEndlessController
	{
		/// <summary>
		/// ★ 必须显式给 TypeInfoResolver。
		///   JsonNode.ToJsonString(options) 序列化数组元素（JsonValueCustomized&lt;T&gt;）时会走
		///   options.MakeReadOnly()，没有 resolver 就直接抛
		///   "JsonSerializerOptions instance must specify a TypeInfoResolver setting"。
		///   写标量（hard_level 那种）碰不到这条路径，所以迪拉克/量子能过、只有超弦
		///   （要写 site_groups 数组）炸，排查时很容易误判成配置问题。
		/// </summary>
		private static readonly JsonSerializerOptions Pretty = new()
		{
			WriteIndented = true,
			TypeInfoResolver = new System.Text.Json.Serialization.Metadata.DefaultJsonTypeInfoResolver()
		};

		private sealed class ConfigRequest
		{
			/// <summary>
			/// 迪拉克/量子 = 战斗配置号；超弦 = 场景组号（可逗号分隔多个）。
			///
			/// 这里用 JsonElement 而不是 string：面板/脚本若要传数字字面量
			/// （<c>{"id":62134}</c>），System.Text.Json 给 string 属性反序列化会直接抛
			/// JsonException，接口回 500 且 body 为空 —— 现象就是「点了没反应/报错」。
			/// 收 JsonElement 后字符串和数字都能吃。
			/// </summary>
			public JsonElement id { get; set; }
			/// <summary>温度 / 扰动</summary>
			public uint temp { get; set; }
		}

		private sealed class SelectRequest
		{
			/// <summary>dirac / quantum / ultra，或 2 / 3 / 4</summary>
			public JsonElement mode { get; set; }
		}

		private sealed class ResetRequest
		{
			/// <summary>dirac / quantum / ultra</summary>
			public string mode { get; set; }
			public uint uid { get; set; }
		}

		public static void AddHandlers(WebApplication app)
		{
			// 统一包一层 try/catch：ASP.NET 默认异常页是空 body，前端只会看到「500」，
			// 连原因都拿不到（日志中间件在 finally 里读 StatusCode，那时还是 200，
			// 所以日志也会显示成成功）。这里把异常文本原样回给面板。
			app.MapGet("/gm/api/endless", (HttpContext ctx) => Guard(ctx, () => GetState(ctx)));
			app.MapPost("/gm/api/endless/dirac", (HttpContext ctx) => Guard(ctx, () => SetDirac(ctx)));
			app.MapPost("/gm/api/endless/quantum", (HttpContext ctx) => Guard(ctx, () => SetQuantum(ctx)));
			app.MapPost("/gm/api/endless/ultra", (HttpContext ctx) => Guard(ctx, () => SetUltra(ctx)));
			app.MapPost("/gm/api/endless/select", (HttpContext ctx) => Guard(ctx, () => SetSelectedMode(ctx)));
			app.MapPost("/gm/api/endless/reset", (HttpContext ctx) => Guard(ctx, () => ResetProgress(ctx)));
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
				c.Warn("深渊接口异常：" + ex);
				if (!ctx.Response.HasStarted)
				{
					ctx.Response.StatusCode = 200;
					await ctx.Response.WriteAsJsonAsync(new { ok = false, output = "内部错误：" + ex.GetType().Name + " - " + ex.Message });
				}
			}
		}

		// ---------------------------------------------------------------- 查询

		private static async Task GetState(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			JsonObject abyss = ReadJson("abyss_ow.json");
			JsonObject ultra = ReadJson("ultra_endless.json");
			JsonObject abyssState = ReadJson(Path.Combine("data", "abyss_state.json"));
			JsonObject ultraState = ReadJson(Path.Combine("data", "ultra_endless_state.json"));

			await ctx.Response.WriteAsJsonAsync(new
			{
				ok = true,
				selected = AbyssConfig.SelectedEndlessType,
				dirac = new
				{
					id = IntOf(abyss, "battle_config_id"),
					temp = IntOf(abyss, "hard_level"),
					groupLevel = IntOf(abyss, "group_level")
				},
				quantum = new
				{
					id = IntOf(abyss, "qt_battle_config_id"),
					temp = IntOf(abyss, "qt_hard_level"),
					fallbackTemp = IntOf(abyss, "hard_level"),
					groupLevel = IntOf(abyss, "group_level")
				},
				ultra = new
				{
					id = string.Join(",", IntArrayOf(ultra, "site_groups")),
					temp = IntOf(ultra, "dynamic_hard_level"),
					scheduleId = UltraEndlessState.EffectiveScheduleId
				},
				progress = BuildProgress(abyssState, ultraState)
			});
		}

		private static JsonObject BuildProgress(JsonObject abyssState, JsonObject ultraState)
		{
			JsonObject result = new JsonObject();

			foreach (var user in abyssState)
			{
				JsonObject entry = (result[user.Key] as JsonObject) ?? new JsonObject();
				if (user.Value is JsonObject types)
				{
					foreach (var t in types)
					{
						JsonObject st = t.Value as JsonObject ?? new JsonObject();
						entry["abyss" + t.Key] = new JsonObject
						{
							["score"] = IntOf(st, "Score"),
							["finished"] = (st["FinishedGroups"] as JsonArray)?.Count ?? 0,
							["curGroup"] = IntOf(st, "CurGroupId"),
							["splitIndex"] = IntOf(st, "SplitIndex")
						};
					}
				}
				result[user.Key] = entry;
			}

			foreach (var user in ultraState)
			{
				JsonObject entry = (result[user.Key] as JsonObject) ?? new JsonObject();
				JsonObject st = user.Value as JsonObject ?? new JsonObject();
				int total = 0;
				if (st["Sites"] is JsonObject sites)
				{
					foreach (var site in sites)
					{
						if (site.Value is JsonObject floors)
						{
							foreach (var f in floors)
							{
								total += IntOf(floors, f.Key);
							}
						}
					}
				}
				entry["ultra"] = new JsonObject
				{
					["totalScore"] = total,
					["scheduleId"] = IntOf(st, "ScheduleId")
				};
				result[user.Key] = entry;
			}

			return result;
		}

		// ---------------------------------------------------------------- 只切模式

		// 「切换配置」那三个接口都是「写配置号 + 切面板」二合一，所以必须填 ID。
		// 只想要「换一套深渊看看」时填 ID 很反直觉，面板里 ID 留空以前是直接静默
		// return 不发请求，看起来就是按钮坏了。这个接口专门解决那一步：只动
		// SelectedEndlessType，配置号原样保留。
		private static async Task SetSelectedMode(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			SelectRequest req = await ctx.Request.ReadFromJsonAsync<SelectRequest>();
			if (!TryParseMode(req?.mode, out uint type, out string name))
			{
				await Fail(ctx, "mode 只支持 dirac / quantum / ultra 或 2 / 3 / 4");
				return;
			}

			AbyssConfig.SetSelected(type);

			JsonObject file = ReadJson("abyss_ow.json");
			string idOf = type switch
			{
				2u => Keep(file, "battle_config_id"),
				3u => Keep(file, "qt_battle_config_id"),
				_  => Keep(file, "site_groups"),
			};

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("挑战面板切到「" + name + "」，配置保持 " + (idOf ?? "(未配置)") + "（未改动）");
			sb.AppendLine("客户端退出深渊界面重新进即可生效。");
			await ctx.Response.WriteAsJsonAsync(new { ok = true, output = sb.ToString() });
		}

		private static string Keep(JsonObject file, string key)
		{
			return file.TryGetPropertyValue(key, out JsonNode node) && node != null
				? node.ToString()
				: null;
		}

		// ---------------------------------------------------------------- 迪拉克之海

		private static async Task SetDirac(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			ConfigRequest req = await ctx.Request.ReadFromJsonAsync<ConfigRequest>();
			if (!TryParseId(req?.id, out uint id))
			{
				await Fail(ctx, "ID 必须是数字（迪拉克之海填 OWEndlessBattleConfig 里的 BattleConfigID，例如 62136）");
				return;
			}

			string err = ValidateBattleConfig(id, 2);
			if (err != null)
			{
				await Fail(ctx, err);
				return;
			}

			JsonObject file = ReadJson("abyss_ow.json");
			file["battle_config_id"] = id;
			if (req.temp > 0u)
			{
				file["hard_level"] = req.temp;
			}
			WriteJson("abyss_ow.json", file);

			AbyssConfig.SetSelected(2u);

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("迪拉克之海 -> 配置 " + id + "，温度 " + (req.temp > 0u ? req.temp.ToString() : "(未改)"));
			sb.AppendLine("已写入 abyss_ow.json，并把挑战面板切到迪拉克之海。");
			sb.AppendLine("客户端退出深渊界面重新进即可看到新配置。");
			await ctx.Response.WriteAsJsonAsync(new { ok = true, output = sb.ToString() });
		}

		// ---------------------------------------------------------------- 量子奇点

		private static async Task SetQuantum(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			ConfigRequest req = await ctx.Request.ReadFromJsonAsync<ConfigRequest>();
			if (!TryParseId(req?.id, out uint id))
			{
				await Fail(ctx, "ID 必须是数字（量子奇点填 type=3 的 BattleConfigID，例如 100033）");
				return;
			}

			string err = ValidateBattleConfig(id, 3);
			if (err != null)
			{
				await Fail(ctx, err);
				return;
			}

			JsonObject file = ReadJson("abyss_ow.json");
			file["qt_battle_config_id"] = id;
			if (req.temp > 0u)
			{
				file["qt_hard_level"] = req.temp;
			}
			WriteJson("abyss_ow.json", file);

			AbyssConfig.SetSelected(3u);

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("量子奇点 -> 配置 " + id + "，温度 " + (req.temp > 0u ? req.temp.ToString() : "(未改)"));
			sb.AppendLine("已写入 abyss_ow.json（qt_battle_config_id / qt_hard_level），挑战面板切到量子奇点。");
			sb.AppendLine("量子温度与迪拉克完全独立，互不影响。");
			await ctx.Response.WriteAsJsonAsync(new { ok = true, output = sb.ToString() });
		}

		// ---------------------------------------------------------------- 超弦空间

		private static async Task SetUltra(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			ConfigRequest req = await ctx.Request.ReadFromJsonAsync<ConfigRequest>();
			List<int> groups = ParseGroups(req?.id);			if (groups.Count == 0)
			{
				await Fail(ctx, "ID 必须是组号（超弦空间的 site_groups，例如 165；多个用逗号分隔）");
				return;
			}

			JsonObject file = ReadJson("ultra_endless.json");
			JsonArray arr = new JsonArray();
			foreach (int g in groups)
			{
				arr.Add(g);
			}
			file["site_groups"] = arr;
			if (req.temp > 0u)
			{
				file["dynamic_hard_level"] = req.temp;
			}
			WriteJson("ultra_endless.json", file);

			AbyssConfig.SetSelected(4u);

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("超弦空间 -> 组 [" + string.Join(",", groups) + "]，温度 "
				+ (req.temp > 0u ? req.temp.ToString() : "(未改)"));
			sb.AppendLine("已写入 ultra_endless.json（site_groups / dynamic_hard_level），挑战面板切到超弦空间。");
			sb.AppendLine("期号保持 " + UltraEndlessState.EffectiveScheduleId + "（换期请直接改 ultra_endless.json 的 schedule_id）。");
			await ctx.Response.WriteAsJsonAsync(new { ok = true, output = sb.ToString() });
		}

		// ---------------------------------------------------------------- 清空记录

		private static async Task ResetProgress(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			ResetRequest req = await ctx.Request.ReadFromJsonAsync<ResetRequest>();
			uint uid = req?.uid ?? 0u;
			if (uid == 0u)
			{
				await Fail(ctx, "缺少 uid（先在左边选一个会话）");
				return;
			}

			string mode = (req.mode ?? string.Empty).Trim().ToLowerInvariant();
			string text;
			switch (mode)
			{
				case "dirac":
					text = "迪拉克之海 uid=" + uid + "：" + (OwEndlessState.Reset(uid, 2u) ? "已清空" : "本来就没有记录");
					break;
				case "quantum":
					text = "量子奇点 uid=" + uid + "：" + (OwEndlessState.Reset(uid, 3u) ? "已清空" : "本来就没有记录");
					break;
				case "ultra":
					text = "超弦空间 uid=" + uid + "：" + (UltraEndlessState.Reset(uid) ? "已清空" : "本来就没有记录");
					break;
				default:
					await Fail(ctx, "mode 只接受 dirac / quantum / ultra");
					return;
			}

			await ctx.Response.WriteAsJsonAsync(new { ok = true, output = text + "\n客户端重新进对应界面即可。" });
		}

		// ---------------------------------------------------------------- 工具

		private static async Task Fail(HttpContext ctx, string message)
		{
			await ctx.Response.WriteAsJsonAsync(new { ok = false, output = message });
		}

		private static bool TryParseId(string raw, out uint id)
		{
			id = 0u;
			return !string.IsNullOrWhiteSpace(raw) && uint.TryParse(raw.Trim(), out id) && id != 0u;
		}

		/// <summary>兼容 {"id":62134} 与 {"id":"62134"} 两种写法。</summary>
		private static bool TryParseId(JsonElement? rawNullable, out uint id)
		{
			id = 0u;
			if (rawNullable == null)
			{
				return false;
			}
			JsonElement raw = rawNullable.Value;
			switch (raw.ValueKind)
			{
				case JsonValueKind.Number:
					return raw.TryGetUInt32(out id) && id != 0u;
				case JsonValueKind.String:
					return TryParseId(raw.GetString(), out id);
				default:
					return false;
			}
		}

		/// <summary>只切「挑战面板指向哪套深渊」，不动任何配置号。接受 dirac/quantum/ultra 或 2/3/4。</summary>
		private static bool TryParseMode(JsonElement? rawNullable, out uint type, out string name)
		{
			type = 0u;
			name = null;
			if (rawNullable == null)
			{
				return false;
			}
			JsonElement raw = rawNullable.Value;

			if (raw.ValueKind == JsonValueKind.Number)
			{
				if (!raw.TryGetUInt32(out uint n) || n < 2u || n > 4u)
				{
					return false;
				}
				type = n;
			}
			else if (raw.ValueKind == JsonValueKind.String)
			{
				switch ((raw.GetString() ?? string.Empty).Trim().ToLowerInvariant())
				{
					case "dirac":   type = 2u; break;
					case "quantum": type = 3u; break;
					case "ultra":   type = 4u; break;
					default: return false;
				}
			}
			else
			{
				return false;
			}

			name = type == 2u ? "迪拉克之海" : (type == 3u ? "量子奇点" : "超弦空间");
			return true;
		}

		private static List<int> ParseGroups(string raw)
		{
			List<int> list = new List<int>();
			if (string.IsNullOrWhiteSpace(raw))
			{
				return list;
			}
			foreach (string part in raw.Split(new[] { ',', '，', ' ' }, StringSplitOptions.RemoveEmptyEntries))
			{
				if (int.TryParse(part.Trim(), out int g) && g != 0 && !list.Contains(g))
				{
					list.Add(g);
				}
			}
			return list;
		}

		/// <summary>
		/// 超弦的 id 三种写法都收：字符串 "165,166"、单个数字 165、数组 [165,166]。
		/// </summary>
		private static List<int> ParseGroups(JsonElement? rawNullable)
		{
			List<int> list = new List<int>();
			if (rawNullable == null)
			{
				return list;
			}
			JsonElement raw = rawNullable.Value;

			switch (raw.ValueKind)
			{
				case JsonValueKind.String:
					return ParseGroups(raw.GetString());
				case JsonValueKind.Number:
					if (raw.TryGetInt32(out int one) && one != 0)
					{
						list.Add(one);
					}
					return list;
				case JsonValueKind.Array:
					foreach (JsonElement e in raw.EnumerateArray())
					{
						int g = 0;
						if (e.ValueKind == JsonValueKind.Number)
						{
							e.TryGetInt32(out g);
						}
						else if (e.ValueKind == JsonValueKind.String)
						{
							int.TryParse((e.GetString() ?? string.Empty).Trim(), out g);
						}
						if (g != 0 && !list.Contains(g))
						{
							list.Add(g);
						}
					}
					return list;
				default:
					return list;
			}
		}

		/// <summary>校验 battle_config_id 是否存在且属于该模式 —— 填错会让整条进关链路断掉。</summary>
		private static string ValidateBattleConfig(uint id, int type)
		{
			Common.Utils.ExcelReader.OWEndlessBattleConfigExcel cfg = OwEndlessConfig.BattleConfig(id);
			if (cfg == null)
			{
				return "配置 " + id + " 不在 OWEndlessBattleConfig 表里，换个 ID 试试";
			}
			if (cfg.EndlessConfigType != type)
			{
				return "配置 " + id + " 属于 type=" + cfg.EndlessConfigType + "，不能用在 "
					+ (type == 2 ? "迪拉克之海" : "量子奇点") + " 上";
			}
			return null;
		}

		private static int IntOf(JsonObject obj, string key)
		{
			try
			{
				JsonNode? node = obj?[key];
				return node == null ? 0 : node.GetValue<int>();
			}
			catch
			{
				return 0;
			}
		}

		private static int[] IntArrayOf(JsonObject obj, string key)
		{
			try
			{
				if (obj?[key] is JsonArray arr)
				{
					return arr.Select(n => n == null ? 0 : n.GetValue<int>()).ToArray();
				}
			}
			catch
			{
				// 忽略
			}
			return Array.Empty<int>();
		}

		private static JsonObject ReadJson(string relative)
		{
			string path = Path.Combine(Directory.GetCurrentDirectory(), relative);
			try
			{
				if (File.Exists(path))
				{
					return JsonNode.Parse(File.ReadAllText(path)) as JsonObject ?? new JsonObject();
				}
			}
			catch (Exception ex)
			{
				Common.Utils.Logger c = new Common.Utils.Logger("GM", ConsoleColor.Magenta, traceOnError: false);
				c.Warn("读取 " + relative + " 失败：" + ex.Message);
			}
			return new JsonObject();
		}

		private static void WriteJson(string relative, JsonObject obj)
		{
			string path = Path.Combine(Directory.GetCurrentDirectory(), relative);
			File.WriteAllText(path, obj.ToJsonString(Pretty), new UTF8Encoding(false));
		}
	}
}
