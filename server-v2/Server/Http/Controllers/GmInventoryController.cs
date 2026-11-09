using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PemukulPaku.GameServer;
using PemukulPaku.GameServer.Game;

// ExcelReader 里也有个 Material（表行），这里要的是协议里的那个
using Material = Common.Resources.Proto.Material;

namespace HttpServer.Controllers
{
	/// <summary>
	/// GM 面板的「填充仓库」：把 <c>MaterialData</c> 全表里玩家还没有的材料，
	/// 各发一个（数量可指定），已有的**跳过不动**。
	///
	///   GET  /gm/api/fillmaterials            统计：全表多少种 / 每个玩家已有多少 / 缺多少
	///   POST /gm/api/fillmaterials { uid, num }  补齐；uid 省略则作用于当前在线玩家
	///
	/// 为什么只补缺的：材料表有 5062 条，其中大量是活动/历史版本留下的条目。
	/// 玩家已经有的那些数量可能被自己刷高了，绝不能覆盖成 1，所以只做「缺才加」。
	///
	/// 补齐后会立刻推一次 <c>GetEquipmentDataRsp</c>，在线玩家不用重登就能在背包里看到。
	/// </summary>
	public static class GmInventoryController
	{
		private sealed class FillRequest
		{
			public uint uid { get; set; }
			public int num { get; set; }
		}

		public static void AddHandlers(WebApplication app)
		{
			app.MapGet("/gm/api/fillmaterials", (HttpContext ctx) => Guard(ctx, () => Preview(ctx)));
			app.MapPost("/gm/api/fillmaterials", (HttpContext ctx) => Guard(ctx, () => Fill(ctx)));
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
				c.Warn("填充仓库接口异常：" + ex);
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

		/// <summary>材料表全量（缓存一次就够，表是只读的）。</summary>
		private static MaterialDataExcel[] AllMaterials =>
			BaseExcelReader<MaterialData, MaterialDataExcel>.GetInstance().All;

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

		private static Session? FindAnyLive()
		{
			try
			{
				foreach (KeyValuePair<string, Session> kv in PemukulPaku.GameServer.Server.GetInstance().Sessions)
				{
					try
					{
						if (kv.Value.Player != null)
						{
							return kv.Value;
						}
					}
					catch { }
				}
			}
			catch { }
			return null;
		}

		private static HashSet<uint> Owned(EquipmentScheme equipment) =>
			new HashSet<uint>((equipment.MaterialList ?? Array.Empty<Material>()).Select(x => x.Id));

		private static async Task Preview(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			MaterialDataExcel[] all = AllMaterials;
			HashSet<uint> liveUids = new HashSet<uint>();
			try
			{
				foreach (KeyValuePair<string, Session> kv in PemukulPaku.GameServer.Server.GetInstance().Sessions)
				{
					try
					{
						if (kv.Value.Player != null) { liveUids.Add(kv.Value.Player.User.Uid); }
					}
					catch { }
				}
			}
			catch { }

			List<object> players = new List<object>();
			foreach (UserScheme u in User.collection.All().OrderBy(x => x.Uid))
			{
				HashSet<uint> have;
				try
				{
					have = Owned(Equipment.FromUid(u.Uid));
				}
				catch
				{
					continue;
				}
				int missing = all.Count(m => !have.Contains((uint)m.Id));
				players.Add(new
				{
					uid = u.Uid,
					nick = u.Nick ?? string.Empty,
					online = liveUids.Contains(u.Uid),
					owned = have.Count,
					missing
				});
			}

			await ctx.Response.WriteAsJsonAsync(new
			{
				ok = true,
				total = all.Length,
				players
			});
		}

		private static async Task Fill(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			FillRequest req = await ctx.Request.ReadFromJsonAsync<FillRequest>();
			int num = (req != null && req.num > 0) ? req.num : 1;
			uint uid = req?.uid ?? 0u;

			Session? live = uid == 0u ? FindAnyLive() : FindLive(uid);
			if (uid == 0u)
			{
				if (live == null)
				{
					await ctx.Response.WriteAsJsonAsync(new { ok = false, output = "没有在线玩家 —— 先登录游戏再说，或者显式传 uid" });
					return;
				}
				uid = live.Player.User.Uid;
			}

			EquipmentScheme equipment = live != null ? live.Player.Equipment : Equipment.FromUid(uid);
			MaterialDataExcel[] all = AllMaterials;
			HashSet<uint> have = Owned(equipment);

			int added = 0;
			foreach (MaterialDataExcel m in all)
			{
				if (have.Contains((uint)m.Id))
				{
					continue;
				}
				equipment.AddMaterial(m.Id, num);
				added++;
			}

			if (added > 0)
			{
				equipment.Save();
			}

			Common.Utils.Logger c = new Common.Utils.Logger("GM", ConsoleColor.Magenta, traceOnError: false);
			string note = $"uid={uid} 材料表 {all.Length} 种，已有 {have.Count} 种，"
				+ (added > 0 ? $"新补 {added} 种（每种 {num} 个）" : "没有需要补的");

			if (live != null)
			{
				// 在线：先按锁定逻辑把补给卡顶回 999，再整包推一次背包，客户端当场刷新
				GmLocks.Materials(live.Player);
				live.Send(Packet.FromProto(BuildEquipmentData(live.Player), CmdId.GetEquipmentDataRsp));
				note += "；已推送 GetEquipmentDataRsp，背包立即刷新。";
			}
			else
			{
				note += "；玩家不在线，已写入存档，下次登录生效。";
			}

			c.Log("[FillMaterials] " + note);
			await ctx.Response.WriteAsJsonAsync(new { ok = true, output = note });
		}

		/// <summary>和 GetEquipmentDataReqHandler 拼的内容保持一致，少几个字段客户端也能认。</summary>
		private static GetEquipmentDataRsp BuildEquipmentData(Player player)
		{
			EquipmentScheme equipment = player.Equipment;
			GetEquipmentDataRsp rsp = new GetEquipmentDataRsp
			{
				retcode = GetEquipmentDataRsp.Retcode.Succ,
				VitalityValue = 0u,
				IsAll = true
			};
			rsp.MaterialLists.AddRange((equipment.MaterialList ?? Array.Empty<Material>()).ToList());
			rsp.MechaLists.AddRange((equipment.MechaList ?? Array.Empty<Mecha>()).ToList());
			rsp.StigmataLists.AddRange((equipment.StigmataList ?? Array.Empty<Stigmata>()).ToList());
			rsp.WeaponLists.AddRange((equipment.WeaponList ?? Array.Empty<Weapon>()).ToList());
			GodWarProgression.ApplyDeepCurrency(player, rsp);
			return rsp;
		}
	}
}
