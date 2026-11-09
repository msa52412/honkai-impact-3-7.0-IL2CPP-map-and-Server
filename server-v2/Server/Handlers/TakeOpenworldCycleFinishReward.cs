using System.Linq;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers.Openworld;

/// <summary>
/// 开放世界「周期完成奖励」领取（TakeOpenworldCycleFinishRewardReq）。
///
/// 老实现只回 SUCC，**不写存档** —— 于是 GetOpenworldMapRsp 里的
/// has_take_finish_reward_cycle 一直是 0，客户端每次打开地图都认为
/// 还有周期奖励没领，于是每次都弹「奖励领取」。
///
/// 现在领了就记下来（HasTakeFinishRewardCycle = 请求里的 Cycle），
/// 下次打开地图就不会再弹。配合 OwMapTuning.AutoTakeCycleReward 还能直接免弹。
/// </summary>
[PacketCmdId(CmdId.TakeOpenworldCycleFinishRewardReq)]
internal class TakeOpenworldCycleFinishRewardReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		TakeOpenworldCycleFinishRewardReq decodedBody = packet.GetDecodedBody<TakeOpenworldCycleFinishRewardReq>();

		OpenWorldScheme openWorldScheme = session.Player.OpenWorlds
			.FirstOrDefault((OpenWorldScheme x) => x.MapId == decodedBody.MapId);

		if (decodedBody.MapId == 1)
		{
			var rsp = TakeSakuraCycle(session.Player, openWorldScheme, decodedBody.Cycle);
			session.Send(Packet.FromProto(rsp, CmdId.TakeOpenworldCycleFinishRewardRsp));
			if (rsp.RewardLists.Count > 0)
				DailyGameplay.PushInventory(session);
			return;
		}

		if (openWorldScheme != null)
		{
			uint before = openWorldScheme.HasTakeFinishRewardCycle;
			uint after = decodedBody.Cycle > before ? decodedBody.Cycle : before;
			if (after != before)
			{
				openWorldScheme.HasTakeFinishRewardCycle = after;
				openWorldScheme.Save();
				Packet.c.Log($"[OW] 领取周期奖励 mapId={decodedBody.MapId} cycle={decodedBody.Cycle} -> has_take {before}->{after}");
			}
			else
			{
				Packet.c.Log($"[OW] 领取周期奖励 mapId={decodedBody.MapId} cycle={decodedBody.Cycle} 已记录（has_take={before}）");
			}
		}
		else
		{
			Packet.c.Warn($"[OW] 领取周期奖励 mapId={decodedBody.MapId} 存档里没有这张地图");
		}

		session.Send(Packet.FromProto(new TakeOpenworldCycleFinishRewardRsp
		{
			retcode = TakeOpenworldCycleFinishRewardRsp.Retcode.Succ,
			MapId = decodedBody.MapId,
			Cycle = decodedBody.Cycle
		}, CmdId.TakeOpenworldCycleFinishRewardRsp));
	}

	internal static TakeOpenworldCycleFinishRewardRsp TakeSakuraCycle(Player player, OpenWorldScheme map, uint cycle)
	{
		var rsp = new TakeOpenworldCycleFinishRewardRsp
		{
			MapId = 1, Cycle = cycle, retcode = TakeOpenworldCycleFinishRewardRsp.Retcode.Fail
		};
		lock (player.Equipment)
		{
			var definition = BaseExcelReader<OpenWorldCycleData, OpenWorldCycleDataExcel>.GetInstance().All
				.FirstOrDefault(row => row.CycleMap == 1 && row.Cycle == cycle);
			if (map == null || definition == null || cycle > map.Cycle)
				return rsp;
			uint previous = map.HasTakeFinishRewardCycle;
			if (cycle <= previous)
			{
				rsp.retcode = TakeOpenworldCycleFinishRewardRsp.Retcode.Succ;
				return rsp;
			}
			if (cycle != previous + 1 || definition.Finishreward <= 0)
				return rsp;

			DailyGameplay.Plan plan;
			bool hasWeapon = false, hasStigmata = false;
			try
			{
				plan = DailyGameplay.Reward(definition.Finishreward);
				// The first Sakura cycle has one weapon and one stigmata in RewardData.
				if (cycle == 1)
				{
					hasWeapon = plan.Items.GetValueOrDefault(20053) == 1 && plan.Items.Remove(20053);
					hasStigmata = plan.Items.GetValueOrDefault(30020) == 1 && plan.Items.Remove(30020);
				}
				DailyGameplay.Validate(player, plan);
			}
			catch (System.Exception error)
			{
				Packet.c.Warn("[OW] Sakura cycle reward: " + error.Message);
				return rsp;
			}

			var oldWeapons = player.Equipment.WeaponList;
			var oldStigmata = player.Equipment.StigmataList;
			Common.Resources.Proto.RewardData reward = null;
			if (!DailyGameplay.Atomic(player, () =>
			{
				reward = DailyGameplay.Grant(player, plan);
				if (hasWeapon)
				{
					var weapon = player.Equipment.AddWeapon(20053);
					reward.ItemLists.Add(new RewardItemData { Id = weapon.Id, Level = weapon.Level, Num = 1 });
				}
				if (hasStigmata)
				{
					var stigmata = player.Equipment.AddStigmata(30020);
					reward.ItemLists.Add(new RewardItemData { Id = stigmata.Id, Level = stigmata.Level, Num = 1 });
				}
				map.HasTakeFinishRewardCycle = cycle;
				map.Save();
			}))
			{
				map.HasTakeFinishRewardCycle = previous;
				player.Equipment.WeaponList = oldWeapons;
				player.Equipment.StigmataList = oldStigmata;
				return rsp;
			}
			rsp.RewardLists.Add(reward);
			rsp.retcode = TakeOpenworldCycleFinishRewardRsp.Retcode.Succ;
			return rsp;
		}
	}
}
