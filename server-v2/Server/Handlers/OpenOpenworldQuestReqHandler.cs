using Common;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld;

/// <summary>
/// 4474 / 4475 —— 「打开委托面板」。
///
/// 服务端原来没有 handler（整份日志里只出现过 1 次，就是 2026-10-28 11:17:02
/// 用户点委托界面的那一下，标记为 not handled）。
///
/// 客户端处理 4475 的逻辑（反汇编 0x1819CED80）：
///   cmp [rsp+0x10], 0        ; retcode == SUCC
///   jne  return false
///   call MonoOpenWorldQuestMapUIManager.Reset(mapID)
/// 也就是和 4547 一样，回 Succ 就会让面板重建。
/// </summary>
[PacketCmdId(CmdId.OpenOpenworldQuestReq)]
internal class OpenOpenworldQuestReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		OpenOpenworldQuestReq req = packet.GetDecodedBody<OpenOpenworldQuestReq>();
		uint mapId = ((req.MapId != 0) ? req.MapId : 1u);
		OwQuestScheme owQuestScheme = OwQuest.GetOrCreate(session.Player.User.Uid, mapId);
		OwQuestService.EnsureSlots(owQuestScheme, session.Player.User);

		bool num = !owQuestScheme.IsOpen;
		owQuestScheme.IsOpen = true;
		if (num)
		{
			owQuestScheme.DayOpenTimes++;
		}

		owQuestScheme.OpenQuestTime = (uint)Global.GetUnixInSeconds();
		owQuestScheme.Save();

		Packet.c.Log($"[OWQuest] OpenOpenworldQuestReq mapId={mapId} -> Succ slots={owQuestScheme.Slots.Count}");
		session.Send(Packet.FromProto(new OpenOpenworldQuestRsp
		{
			retcode = OpenOpenworldQuestRsp.Retcode.Succ
		}, CmdId.OpenOpenworldQuestRsp));
	}
}
