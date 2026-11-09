using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Database;
using Common.Resources.Proto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PemukulPaku.GameServer;

namespace HttpServer.Controllers
{
	/// <summary>
	/// GM 面板的「EVA 舰桥」强开开关。
	///
	/// 照 docs\舰桥隐藏主题-EVA联动排查.md 第五节那条最短路径做的：
	/// 主界面加载哪个场景只看 `User.WarshipId`，而 `SetWarshipReqHandler`
	/// **没有任何校验**，直接 `User.WarshipId = req.WarshipId`。
	/// 所以只要把在线玩家这个值改成 400001，再把 `GetMainDataRsp.WarshipTheme`
	/// 推给他，客户端当场就会按 id 去查 EntryTheme → SpaceShipConfig(100)
	/// → `Stage/MainMenu_SpaceShip/EVA/MainMenu_SpaceShip`，**不需要重新登录**。
	///
	/// 之前那版只改数据库、不推包，客户端的 `Player.User` 还是旧实例，
	/// 重登也要等下一次 GetMainData 才刷 —— 表现就是「改了没反应」。
	///
	///   GET  /gm/api/warship     查当前在线玩家的 WarshipId
	///   POST /gm/api/warship     { uid, warship_id } 改 + 立即推送
	/// </summary>
	public static class GmWarshipController
	{
		/// <summary>EVA 联动主题的协议 id（EntryThemeItemData.ThemeItemID）。</summary>
		private const int EvaWarshipId = 400001;

		private sealed class SaveRequest
		{
			public uint uid { get; set; }
			public int warship_id { get; set; }
		}

		public static void AddHandlers(WebApplication app)
		{
			app.MapGet("/gm/api/warship", (HttpContext ctx) => Guard(ctx, () => List(ctx)));
			app.MapPost("/gm/api/warship", (HttpContext ctx) => Guard(ctx, () => Save(ctx)));
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
				c.Warn("EVA 舰桥接口异常：" + ex);
				if (!ctx.Response.HasStarted)
				{
					ctx.Response.StatusCode = 200;
					await ctx.Response.WriteAsJsonAsync(new
					{
						ok = false,
						output = "内部错误：" + ex.GetType().Name + " - " + ex.Message
					});
				}
			}
		}

		/// <summary>按 uid 找在线会话；找不到返回 null。</summary>
		private static Session? FindLive(uint uid)
		{
			try
			{
				foreach (KeyValuePair<string, Session> kv in PemukulPaku.GameServer.Server.GetInstance().Sessions)
				{
					try
					{
						if (kv.Value.Player != null && kv.Value.Player.User.Uid == uid)
						{
							return kv.Value;
						}
					}
					catch
					{
						// 会话刚建立/刚断开，Player 不完整，跳过
					}
				}
			}
			catch
			{
				// GameServer 还没起
			}
			return null;
		}

		private static async Task List(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			HashSet<uint> live = new HashSet<uint>();
			try
			{
				foreach (KeyValuePair<string, Session> kv in PemukulPaku.GameServer.Server.GetInstance().Sessions)
				{
					try
					{
						if (kv.Value.Player != null) { live.Add(kv.Value.Player.User.Uid); }
					}
					catch { }
				}
			}
			catch { }

			List<object> players = new List<object>();
			foreach (UserScheme u in User.collection.All().OrderBy(x => x.Uid))
			{
				players.Add(new
				{
					uid = u.Uid,
					nick = u.Nick ?? string.Empty,
					warship_id = u.WarshipId,
					online = live.Contains(u.Uid),
					eva = u.WarshipId == EvaWarshipId
				});
			}

			await ctx.Response.WriteAsJsonAsync(new { ok = true, eva_id = EvaWarshipId, players });
		}

		private static async Task Save(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			SaveRequest req = await ctx.Request.ReadFromJsonAsync<SaveRequest>();
			if (req == null || req.uid == 0u)
			{
				await ctx.Response.WriteAsJsonAsync(new { ok = false, output = "缺少 uid" });
				return;
			}

			Common.Utils.Logger c = new Common.Utils.Logger("GM", ConsoleColor.Magenta, traceOnError: false);
			Session? live = FindLive(req.uid);
			string note;

			if (live != null)
			{
				// 在线：改运行时对象 + 立刻推包，客户端当场换场景（不用重登）
				int old = live.Player.User.WarshipId;
				live.Player.User.WarshipId = req.warship_id;
				live.Player.User.Save();

				GetMainDataRsp main = new GetMainDataRsp
				{
					retcode = GetMainDataRsp.Retcode.Succ
				};
				main.WarshipTheme = WarshipSettings.ThemeOf(req.uid, (uint)req.warship_id);
				// 和 SetWarshipReqHandler 保持一致，这个字段决定客户端刷哪些模块
				main.TypeLists = new uint[1] { 38u };

				live.Send(
					Packet.FromProto(main, CmdId.GetMainDataRsp),
					Packet.FromProto(new SetWarshipRsp { retcode = SetWarshipRsp.Retcode.Succ }, CmdId.SetWarshipRsp));

				note = $"在线玩家 uid={req.uid} WarshipId {old} -> {req.warship_id}，"
					+ "GetMainDataRsp 已推送，客户端应该当场切场景。";
				c.Log($"[Warship] uid={req.uid} {old} -> {req.warship_id}（在线，已推包）");
			}
			else
			{
				// 离线：只能写库，下次登录生效
				UserScheme? user = User.collection.FirstOrDefault(x => x.Uid == req.uid);
				if (user == null)
				{
					await ctx.Response.WriteAsJsonAsync(new { ok = false, output = "找不到 uid=" + req.uid });
					return;
				}
				int old = user.WarshipId;
				user.WarshipId = req.warship_id;
				user.Save();
				note = $"玩家不在线，uid={req.uid} WarshipId {old} -> {req.warship_id} 已写入存档，"
					+ "下次登录生效。";
				c.Log($"[Warship] uid={req.uid} {old} -> {req.warship_id}（离线，只写库）");
			}

			await ctx.Response.WriteAsJsonAsync(new { ok = true, output = note });
		}
	}
}
