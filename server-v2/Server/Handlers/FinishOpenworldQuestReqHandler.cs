using System;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld;

/// <summary>
/// 4482 / 4483 —— 领取委托奖励。领完这个槽空出来并重新抽一个。
/// </summary>
[PacketCmdId(CmdId.TakeOpenworldQuestRewardReq)]
internal class TakeOpenworldQuestRewardReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		TakeOpenworldQuestRewardReq req = packet.GetDecodedBody<TakeOpenworldQuestRewardReq>();
		uint mapId = ((req.MapId != 0) ? req.MapId : 1u);
		OwQuestScheme owQuestScheme = OwQuest.GetOrCreate(session.Player.User.Uid, mapId);
		OwQuestService.EnsureSlots(owQuestScheme, session.Player.User);

		OwQuestSlot owQuestSlot = OwQuestService.FindSlot(owQuestScheme, req.QuestId);
		if (owQuestSlot == null)
		{
			session.Send(Packet.FromProto(new TakeOpenworldQuestRewardRsp
			{
				retcode = TakeOpenworldQuestRewardRsp.Retcode.QuestNotFound
			}, CmdId.TakeOpenworldQuestRewardRsp));
			return;
		}

		if (owQuestSlot.Status != 2)
		{
			session.Send(Packet.FromProto(new TakeOpenworldQuestRewardRsp
			{
				retcode = TakeOpenworldQuestRewardRsp.Retcode.NotFinish
			}, CmdId.TakeOpenworldQuestRewardRsp));
			return;
		}

		uint oldLevel = owQuestScheme.QuestLevel;
		Common.Utils.ExcelReader.OpenWorldQuestDataExcel def = OwQuestService.Def(mapId, req.QuestId);
		uint addStar = (uint)((def != null) ? Math.Max(1, def.RewardStar) : 1);
		owQuestScheme.TotalStar += addStar;
		// 原来写死 1 + star/10 且上限 15，和表（1..30，累计 141 星满级）完全对不上，
		// 领一次奖就把等级算回个位数。现在按 OpenWorldQuestLevelData 反算，且只升不降。
		owQuestScheme.QuestLevel = Math.Max(owQuestScheme.QuestLevel, OwQuestService.LevelByStar(owQuestScheme.TotalStar));

		OwQuestSlot slot = owQuestSlot;
		OwQuestService.Roll(owQuestScheme, slot);
		owQuestScheme.Save();

		TakeOpenworldQuestRewardRsp proto = new TakeOpenworldQuestRewardRsp
		{
			retcode = TakeOpenworldQuestRewardRsp.Retcode.Succ,
			AddStar = addStar,
			ChallengeScore = 0u,
			OldQuestLevel = oldLevel
		};
		Packet.c.Log($"[OWQuest] TakeReward mapId={mapId} quest={req.QuestId} +{addStar}star star={owQuestScheme.TotalStar} lv={owQuestScheme.QuestLevel}");
		session.Send(Packet.FromProto(proto, CmdId.TakeOpenworldQuestRewardRsp));
	}
}

/// <summary>
/// 4484 / 4485 —— 放弃委托。槽位清空并重新抽。
/// </summary>
[PacketCmdId(CmdId.AbandonOpenworldQuestReq)]
internal class AbandonOpenworldQuestReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		AbandonOpenworldQuestReq req = packet.GetDecodedBody<AbandonOpenworldQuestReq>();
		uint mapId = ((req.MapId != 0) ? req.MapId : 1u);
		OwQuestScheme owQuestScheme = OwQuest.GetOrCreate(session.Player.User.Uid, mapId);
		OwQuestService.EnsureSlots(owQuestScheme, session.Player.User);

		OwQuestSlot owQuestSlot = OwQuestService.FindSlot(owQuestScheme, req.QuestId);
		if (owQuestSlot == null)
		{
			session.Send(Packet.FromProto(new AbandonOpenworldQuestRsp
			{
				retcode = AbandonOpenworldQuestRsp.Retcode.NotFound
			}, CmdId.AbandonOpenworldQuestRsp));
			return;
		}

		if (owQuestSlot.Status != 1)
		{
			session.Send(Packet.FromProto(new AbandonOpenworldQuestRsp
			{
				retcode = AbandonOpenworldQuestRsp.Retcode.NotDoing
			}, CmdId.AbandonOpenworldQuestRsp));
			return;
		}

		OwQuestSlot slot = owQuestSlot;
		OwQuestService.Roll(owQuestScheme, slot);
		owQuestScheme.Save();

		Packet.c.Log($"[OWQuest] Abandon mapId={mapId} quest={req.QuestId} -> Succ");
		session.Send(Packet.FromProto(new AbandonOpenworldQuestRsp
		{
			retcode = AbandonOpenworldQuestRsp.Retcode.Succ
		}, CmdId.AbandonOpenworldQuestRsp));
	}
}

/// <summary>
/// 4488 / 4489 —— 委托打完了（从关卡结算回来）。
/// </summary>
[PacketCmdId(CmdId.FinishOpenworldQuestReq)]
internal class FinishOpenworldQuestReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		FinishOpenworldQuestReq req = packet.GetDecodedBody<FinishOpenworldQuestReq>();
		uint mapId = ((req.MapId != 0) ? req.MapId : 1u);
		OwQuestScheme owQuestScheme = OwQuest.GetOrCreate(session.Player.User.Uid, mapId);
		OwQuestService.EnsureSlots(owQuestScheme, session.Player.User);

		OwQuestSlot owQuestSlot = OwQuestService.FindSlot(owQuestScheme, req.QuestId);
		if (owQuestSlot == null)
		{
			session.Send(Packet.FromProto(new FinishOpenworldQuestRsp
			{
				retcode = FinishOpenworldQuestRsp.Retcode.QuestNotExist
			}, CmdId.FinishOpenworldQuestRsp));
			return;
		}

		if (owQuestSlot.Status != 1)
		{
			session.Send(Packet.FromProto(new FinishOpenworldQuestRsp
			{
				retcode = FinishOpenworldQuestRsp.Retcode.QuestNotDoing
			}, CmdId.FinishOpenworldQuestRsp));
			return;
		}

		owQuestSlot.Status = 2u;
		owQuestSlot.FinishChallengeList = req.FinishChallengeLists ?? Array.Empty<uint>();
		owQuestSlot.Progress = 1u;
		owQuestScheme.Save();

		Packet.c.Log($"[OWQuest] Finish mapId={mapId} quest={req.QuestId} cost={req.CostTime}s -> Finish");
		session.Send(Packet.FromProto(new FinishOpenworldQuestRsp
		{
			retcode = FinishOpenworldQuestRsp.Retcode.Succ,
			QuestId = req.QuestId,
			CostTime = req.CostTime,
			FinishChallengeLists = req.FinishChallengeLists ?? Array.Empty<uint>()
		}, CmdId.FinishOpenworldQuestRsp));
	}
}
