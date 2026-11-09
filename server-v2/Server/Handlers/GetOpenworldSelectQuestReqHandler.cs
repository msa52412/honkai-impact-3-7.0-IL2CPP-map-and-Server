using Common;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld;

/// <summary>
/// 4476 / 4477 —— 拉「这张地图现在有哪些委托可以接」。
///
/// 客户端处理 4477 的逻辑（反汇编 0x1819CE960）：
///   retcode == SUCC 时，用 quest_list 直接 new OpenworldSelectDailyQuestPageContext(list, mapID)
/// 也就是「接取委托」那个列表页。服务端原来没有 handler，全日志 0 次命中。
///
/// 回的是 5 个槽位当前的委托（status NONE = 可接，DOING = 已接，CLOSE = 槽未解锁）。
/// </summary>
[PacketCmdId(CmdId.GetOpenworldSelectQuestReq)]
internal class GetOpenworldSelectQuestReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetOpenworldSelectQuestReq req = packet.GetDecodedBody<GetOpenworldSelectQuestReq>();
		uint mapId = ((req.MapId != 0) ? req.MapId : 1u);
		OwQuestScheme owQuestScheme = OwQuest.GetOrCreate(session.Player.User.Uid, mapId);
		OwQuestService.EnsureSlots(owQuestScheme, session.Player.User);

		GetOpenworldSelectQuestRsp proto = new GetOpenworldSelectQuestRsp
		{
			retcode = GetOpenworldSelectQuestRsp.Retcode.Succ
		};
		foreach (OwQuestSlot slot in owQuestScheme.Slots)
		{
			if (slot.QuestId != 0)
			{
				proto.QuestLists.Add(OwQuestService.ToProto(owQuestScheme, slot));
			}
		}

		Packet.c.Log($"[OWQuest] GetOpenworldSelectQuestReq mapId={mapId} -> Succ count={proto.QuestLists.Count}");
		session.Send(Packet.FromProto(proto, CmdId.GetOpenworldSelectQuestRsp));
	}
}
