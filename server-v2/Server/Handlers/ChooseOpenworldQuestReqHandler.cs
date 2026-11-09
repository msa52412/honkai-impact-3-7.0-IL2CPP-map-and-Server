using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers.Openworld;

/// <summary>
/// 4478 / 4479 —— 接取委托（把某个候选槽变成 DOING）。
/// </summary>
[PacketCmdId(CmdId.ChooseOpenworldQuestReq)]
internal class ChooseOpenworldQuestReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		ChooseOpenworldQuestReq req = packet.GetDecodedBody<ChooseOpenworldQuestReq>();
		uint mapId = ((req.MapId != 0) ? req.MapId : 1u);
		OwQuestScheme owQuestScheme = OwQuest.GetOrCreate(session.Player.User.Uid, mapId);
		OwQuestService.EnsureSlots(owQuestScheme, session.Player.User);

		OwQuestSlot owQuestSlot = OwQuestService.FindSlot(owQuestScheme, req.QuestId);
		var map = session.Player.OpenWorlds?.FirstOrDefault(row => row.MapId == mapId);
		var partnerQuest = SakuraServant.CurrentQuest(map);
		if (partnerQuest?.QuestId == req.QuestId)
		{
			var status = partnerQuest.Status == 0 ? ChooseOpenworldQuestRsp.Retcode.Succ : ChooseOpenworldQuestRsp.Retcode.HasChoose;
			if (status == ChooseOpenworldQuestRsp.Retcode.Succ)
			{
				partnerQuest.Status = 1;
				map.PartnerQuest = partnerQuest;
				map.Save();
			}
			session.Send(Packet.FromProto(new ChooseOpenworldQuestRsp { retcode = status,
				Quest = OwQuestService.ToProto(owQuestScheme, partnerQuest) }, CmdId.ChooseOpenworldQuestRsp));
			return;
		}
		if (owQuestSlot == null)
		{
			Packet.c.Log($"[OWQuest] Choose mapId={mapId} quest={req.QuestId} -> QUEST_NOT_FOUND");
			session.Send(Packet.FromProto(new ChooseOpenworldQuestRsp
			{
				retcode = ChooseOpenworldQuestRsp.Retcode.QuestNotFound
			}, CmdId.ChooseOpenworldQuestRsp));
			return;
		}

		if (owQuestSlot.Status == 1)
		{
			session.Send(Packet.FromProto(new ChooseOpenworldQuestRsp
			{
				retcode = ChooseOpenworldQuestRsp.Retcode.HasChoose,
				Quest = OwQuestService.ToProto(owQuestScheme, owQuestSlot)
			}, CmdId.ChooseOpenworldQuestRsp));
			return;
		}

		owQuestSlot.Status = 1u;
		owQuestSlot.Progress = 0u;
		owQuestScheme.CycleQuestTakenNum++;
		owQuestScheme.Save();

		Packet.c.Log($"[OWQuest] Choose mapId={mapId} quest={req.QuestId} pos={owQuestSlot.Position} -> Doing");
		session.Send(Packet.FromProto(new ChooseOpenworldQuestRsp
		{
			retcode = ChooseOpenworldQuestRsp.Retcode.Succ,
			Quest = OwQuestService.ToProto(owQuestScheme, owQuestSlot)
		}, CmdId.ChooseOpenworldQuestRsp));
	}
}

/// <summary>
/// 4480 / 4481 —— 刷新委托池（客户端右上角那个按钮）。
/// 有免费次数就扣次数，没有就按 next_refresh_cost 收吼姆硬币（这里不做扣费，只回状态）。
/// </summary>
[PacketCmdId(CmdId.RefreshOpenworldQuestReq)]
internal class RefreshOpenworldQuestReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		RefreshOpenworldQuestReq req = packet.GetDecodedBody<RefreshOpenworldQuestReq>();
		uint mapId = ((req.MapId != 0) ? req.MapId : 1u);
		OwQuestScheme owQuestScheme = OwQuest.GetOrCreate(session.Player.User.Uid, mapId);
		OwQuestService.EnsureSlots(owQuestScheme, session.Player.User);

		if (owQuestScheme.Slots.Any((OwQuestSlot s) => s.Status == 1))
		{
			session.Send(Packet.FromProto(new RefreshOpenworldQuestRsp
			{
				retcode = RefreshOpenworldQuestRsp.Retcode.HasChoose,
				MapId = mapId,
				RefreshLeftTimes = owQuestScheme.RefreshLeftTimes,
				NextRefreshCost = OwQuestService.NextRefreshCost(owQuestScheme)
			}, CmdId.RefreshOpenworldQuestRsp));
			return;
		}

		if (owQuestScheme.RefreshLeftTimes > 0)
		{
			owQuestScheme.RefreshLeftTimes--;
		}

		foreach (OwQuestSlot item in owQuestScheme.Slots.Where((OwQuestSlot s) => s.Status != 3))
		{
			OwQuestService.Roll(owQuestScheme, item);
		}

		owQuestScheme.Save();

		RefreshOpenworldQuestRsp proto = new RefreshOpenworldQuestRsp
		{
			retcode = RefreshOpenworldQuestRsp.Retcode.Succ,
			MapId = mapId,
			RefreshLeftTimes = owQuestScheme.RefreshLeftTimes,
			NextRefreshCost = OwQuestService.NextRefreshCost(owQuestScheme)
		};
		foreach (OwQuestSlot item2 in owQuestScheme.Slots)
		{
			if (item2.QuestId != 0)
			{
				proto.QuestLists.Add(OwQuestService.ToProto(owQuestScheme, item2));
			}
		}

		Packet.c.Log($"[OWQuest] Refresh mapId={mapId} -> Succ left={owQuestScheme.RefreshLeftTimes}");
		session.Send(Packet.FromProto(proto, CmdId.RefreshOpenworldQuestRsp));
	}
}
