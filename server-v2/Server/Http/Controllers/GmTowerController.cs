using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Common.Database;
using Common.Utils.ExcelReader;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PemukulPaku.GameServer;
using PemukulPaku.GameServer.Game;

namespace HttpServer.Controllers
{
	/// <summary>
	/// GM 面板的「后崩坏书爬塔」控制 —— 第一章（DLC）/ 第二章（ScDLC）各自独立。
	///
	///   GET  /gm/api/tower?uid=1002      查两座塔当前生效的赛季 + 该 uid 的进度
	///   POST /gm/api/tower/dlc   {weather: <ID>}
	///   POST /gm/api/tower/scdlc {weather: <ID>}
	///   POST /gm/api/tower/reset {mode:"dlc"|"scdlc", uid}
	///
	/// 天气怎么生效
	/// ------------
	/// 爬塔的天气**不由服务端下发**，客户端拿 rsp.schedule_id 自己去查赛季表的 Weather。
	/// 所以「切天气」= 在赛季表里挑一期带该天气的赛季，把它的 ID 当 schedule_id 发下去。
	/// 落盘是 run\tower.json（TowerTuning 按 mtime 热加载，改完下一次请求就生效）。
	///
	/// ★ 面板上那个输入框叫「ID」，两种 ID 都收：
	///     命中赛季表里的 ID  -> 钉死这一期（dlc_schedule_id，优先级最高）
	///     否则当作天气 ID    -> 挑一期带该天气的赛季（dlc_weather）
	///   两张表号段不重叠（赛季 1~733 / 天气 1001~1008），所以不会看串。
	///   配置页只留赛季表，这两列都在里面，填哪个都行。
	///
	/// ★ 换赛季会连带换 ConfigType 和总层数，并且 DlcTower.GetOrCreate 会重置进度。
	///   这是「切换配置」的应有行为，但要在面板上写清楚。
	///
	/// ★ 一律用 System.Text.Json.Nodes，不要用 Newtonsoft 的 JObject
	///   （WriteAsJsonAsync 走 STJ，JObject 会被反射成内部字段数组）。
	/// </summary>
	public static class GmTowerController
	{
		private static readonly JsonSerializerOptions Pretty = new()
		{
			WriteIndented = true,
			// ★ 必须给 resolver：ToJsonString 写数组元素时会 MakeReadOnly()，
			//   没有 resolver 直接抛 InvalidOperationException（深渊那边踩过）。
			TypeInfoResolver = new System.Text.Json.Serialization.Metadata.DefaultJsonTypeInfoResolver()
		};

		private sealed class WeatherRequest
		{
			/// <summary>
			/// 面板上那个「ID」输入框。两种都收：
			///   1001 起 = 天气 ID（挑一期带该天气的赛季）
			///   1 起    = 赛季 ID（直接钉死这一期）
			/// 两张表的号段不重叠（赛季 1~733，天气 1001~1008），所以不会看串。
			/// 0 = 两个都取消，回到按时间自动选赛季。
			/// </summary>
			public uint weather { get; set; }
		}

		private sealed class ResetRequest
		{
			/// <summary>dlc / scdlc</summary>
			public string mode { get; set; }
			public uint uid { get; set; }
		}

		public static void AddHandlers(WebApplication app)
		{
			app.MapGet("/gm/api/tower", (HttpContext ctx) => Guard(ctx, () => GetState(ctx)));
			app.MapPost("/gm/api/tower/dlc", (HttpContext ctx) => Guard(ctx, () => SetWeather(ctx, false)));
			app.MapPost("/gm/api/tower/scdlc", (HttpContext ctx) => Guard(ctx, () => SetWeather(ctx, true)));
			app.MapPost("/gm/api/tower/reset", (HttpContext ctx) => Guard(ctx, () => Reset(ctx)));
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
				c.Warn("爬塔接口异常：" + ex);
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

			uint uid = 0u;
			if (ctx.Request.Query.TryGetValue("uid", out var raw) && uint.TryParse(raw.ToString(), out uint parsed))
			{
				uid = parsed;
			}

			DLCTowerScheduleDataExcel dlc = DlcTowerService.CurrentSchedule();
			ScDLCTowerScheduleDataExcel scdlc = ScDlcTowerService.CurrentSchedule();
			(uint dlcWeather, uint dlcSchedule) = TowerTuning.Dlc;
			(uint scdlcWeather, uint scdlcSchedule) = TowerTuning.ScDlc;

			JsonObject progress = new JsonObject();
			if (uid != 0u)
			{
				DlcTowerScheme a = DlcTower.Get(uid);
				progress["dlc"] = a == null
					? null
					: (JsonNode)new JsonObject
					{
						["progress"] = a.Progress,
						["maxProgress"] = a.MaxProgress,
						["costTime"] = a.CostTime,
						["scored"] = a.FloorScores?.Count ?? 0
					};

				ScDlcTowerScheme b = ScDlcTower.Get(uid);
				progress["scdlc"] = b == null
					? null
					: (JsonNode)new JsonObject
					{
						["curFloor"] = b.CurFloor,
						["maxFloorReached"] = b.MaxFloorReached,
						["maxScore"] = b.MaxScore,
						["sweep"] = b.MaxCanSweepFloor
					};
			}

			await ctx.Response.WriteAsJsonAsync(new
			{
				ok = true,
				dlc = new
				{
					weather = dlcWeather,
					scheduleId = dlcSchedule,
					effScheduleId = dlc?.ID ?? 0,
					effWeather = dlc?.Weather ?? 0,
					configType = dlc?.ConfigType ?? 0,
					maxFloor = dlc?.MaxFloor ?? 0,
					start = dlc?.StartTime ?? "",
					end = dlc?.EndTime ?? ""
				},
				scdlc = new
				{
					weather = scdlcWeather,
					scheduleId = scdlcSchedule,
					effScheduleId = scdlc?.ID ?? 0,
					effWeather = scdlc?.Weather ?? 0,
					configType = scdlc?.ConfigType ?? 0,
					maxFloor = scdlc?.MaxFloor ?? 0,
					start = scdlc?.StartTime ?? "",
					end = scdlc?.EndTime ?? ""
				},
				progress
			});
		}

		// ---------------------------------------------------------------- 切天气

		private static async Task SetWeather(HttpContext ctx, bool second)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			WeatherRequest req = await ctx.Request.ReadFromJsonAsync<WeatherRequest>();
			uint id = req?.weather ?? 0u;
			string label = second ? "后崩坏书2 爬塔" : "后崩坏书1 爬塔";
			string weatherKey = second ? "scdlc_weather" : "dlc_weather";
			string scheduleKey = second ? "scdlc_schedule_id" : "dlc_schedule_id";

			StringBuilder sb = new StringBuilder();
			JsonObject file = ReadJson("tower.json");

			if (id == 0u)
			{
				file[weatherKey] = 0u;
				file[scheduleKey] = 0u;
				WriteJson("tower.json", file);
				sb.AppendLine(label + " -> 取消指定，回到按时间自动选赛季");
			}
			else if (SeasonExists(second, (int)id))
			{
				// 命中赛季 ID：直接钉死这一期（优先级最高）
				file[scheduleKey] = id;
				file[weatherKey] = 0u;
				WriteJson("tower.json", file);
				sb.AppendLine(label + " -> 钉死赛季 " + id);
			}
			else
			{
				// 否则按天气 ID 处理：赛季表里必须真有哪一期用这个天气，
				// 否则客户端查不到天气、塔页会空
				List<int> seasons = SeasonsOfWeather(second, (int)id);
				if (seasons.Count == 0)
				{
					await Fail(ctx, label + "：ID " + id + " 既不是赛季 ID，也不是任何赛季在用的天气。\n"
						+ "可用天气：" + string.Join(" / ", AvailableWeathers(second)) + "\n"
						+ "可用赛季 ID：" + SeasonIdRange(second) + "（填 0 可取消指定）");
					return;
				}
				file[weatherKey] = id;
				file[scheduleKey] = 0u;
				WriteJson("tower.json", file);
				sb.AppendLine(label + " -> 天气 " + id + "（共 " + seasons.Count + " 期赛季用过，取最合适的一期）");
			}

			// 立刻回显实际会生效的那一期（TowerTuning 按 mtime 重载，这里读到的是新值）
			if (second)
			{
				ScDLCTowerScheduleDataExcel row = ScDlcTowerService.CurrentSchedule();
				sb.AppendLine("生效：" + Describe(row?.ID ?? 0, row?.Weather ?? 0, row?.ConfigType ?? 0,
					row?.MaxFloor ?? 0, row?.StartTime, row?.EndTime));
			}
			else
			{
				DLCTowerScheduleDataExcel row = DlcTowerService.CurrentSchedule();
				sb.AppendLine("生效：" + Describe(row?.ID ?? 0, row?.Weather ?? 0, row?.ConfigType ?? 0,
					row?.MaxFloor ?? 0, row?.StartTime, row?.EndTime));
			}
			sb.AppendLine("已写入 tower.json。换赛季会把该玩家的进度重置（客户端重进塔页即可看到）。");

			await ctx.Response.WriteAsJsonAsync(new { ok = true, output = sb.ToString() });
		}

		// ---------------------------------------------------------------- 清空记录

		private static async Task Reset(HttpContext ctx)
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
				case "dlc":
					text = "后崩坏书1 爬塔 uid=" + uid + "：" + (DlcTowerService.ClearRecords(uid) ? "已清空" : "本来就没什么可清的");
					break;
				case "scdlc":
					text = "后崩坏书2 爬塔 uid=" + uid + "：" + (ScDlcTowerService.ClearRecords(uid) ? "已清空" : "本来就没什么可清的");
					break;
				default:
					await Fail(ctx, "mode 只接受 dlc / scdlc");
					return;
			}

			await ctx.Response.WriteAsJsonAsync(new { ok = true, output = text + "\n客户端重新进塔页即可。" });
		}

		// ---------------------------------------------------------------- 工具

		private static async Task Fail(HttpContext ctx, string message)
		{
			await ctx.Response.WriteAsJsonAsync(new { ok = false, output = message });
		}

		private static List<int> SeasonsOfWeather(bool second, int weather)
		{
			List<int> ids = new List<int>();
			if (second)
			{
				ScDLCTowerScheduleDataExcel[] all = ScDLCTowerScheduleData.GetInstance().All;
				if (all != null)
				{
					foreach (ScDLCTowerScheduleDataExcel row in all)
					{
						if (row.Weather == weather)
						{
							ids.Add(row.ID);
						}
					}
				}
			}
			else
			{
				DLCTowerScheduleDataExcel[] all = DLCTowerScheduleData.GetInstance().All;
				if (all != null)
				{
					foreach (DLCTowerScheduleDataExcel row in all)
					{
						if (row.Weather == weather)
						{
							ids.Add(row.ID);
						}
					}
				}
			}
			return ids;
		}

		private static List<int> AvailableWeathers(bool second)
		{
			HashSet<int> set = new HashSet<int>();
			if (second)
			{
				ScDLCTowerScheduleDataExcel[] all = ScDLCTowerScheduleData.GetInstance().All;
				if (all != null)
				{
					foreach (ScDLCTowerScheduleDataExcel row in all)
					{
						set.Add(row.Weather);
					}
				}
			}
			else
			{
				DLCTowerScheduleDataExcel[] all = DLCTowerScheduleData.GetInstance().All;
				if (all != null)
				{
					foreach (DLCTowerScheduleDataExcel row in all)
					{
						set.Add(row.Weather);
					}
				}
			}
			return set.Where(w => w != 0).OrderBy(w => w).ToList();
		}

		/// <summary>赛季表里有没有这个 ID（用于区分「填的是赛季 ID」还是「填的是天气 ID」）。</summary>
		private static bool SeasonExists(bool second, int id)
		{
			if (second)
			{
				ScDLCTowerScheduleDataExcel[] all = ScDLCTowerScheduleData.GetInstance().All;
				if (all != null)
				{
					foreach (ScDLCTowerScheduleDataExcel row in all)
					{
						if (row.ID == id) return true;
					}
				}
			}
			else
			{
				DLCTowerScheduleDataExcel[] all = DLCTowerScheduleData.GetInstance().All;
				if (all != null)
				{
					foreach (DLCTowerScheduleDataExcel row in all)
					{
						if (row.ID == id) return true;
					}
				}
			}
			return false;
		}

		private static string SeasonIdRange(bool second)
		{
			int min = 0, max = 0;
			if (second)
			{
				ScDLCTowerScheduleDataExcel[] all = ScDLCTowerScheduleData.GetInstance().All;
				if (all != null)
				{
					foreach (ScDLCTowerScheduleDataExcel row in all)
					{
						if (min == 0 || row.ID < min) min = row.ID;
						if (row.ID > max) max = row.ID;
					}
				}
			}
			else
			{
				DLCTowerScheduleDataExcel[] all = DLCTowerScheduleData.GetInstance().All;
				if (all != null)
				{
					foreach (DLCTowerScheduleDataExcel row in all)
					{
						if (min == 0 || row.ID < min) min = row.ID;
						if (row.ID > max) max = row.ID;
					}
				}
			}
			return min == 0 ? "（无）" : min + " ~ " + max;
		}

		private static string Describe(int id, int weather, int configType, int maxFloor, string start, string end)
		{
			if (id == 0)
			{
				return "（没有可用赛季）";
			}
			return "赛季 " + id + " · 天气 " + weather + " · ConfigType " + configType
				+ " · " + maxFloor + " 层 · " + start + " ~ " + end;
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
