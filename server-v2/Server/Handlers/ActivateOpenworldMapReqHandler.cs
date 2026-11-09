using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld;

/// <summary>
/// 4504 / 4505 —— 开放世界页面底部那个「权限激活」按钮。
///
/// 服务端原来**没有这个 handler**（整份日志里 0 次命中），点了不回包，
/// 客户端只能等超时，按钮也就一直是灰的。
///
/// 这里只回 Succ + 回显 map_id：地图是否「已激活」由 4497/4498 的
/// status 字段决定（见 OwMapTuning），不落库，所以不需要额外的存档字段。
/// </summary>
[PacketCmdId(CmdId.ActivateOpenworldMapReq)]
internal class ActivateOpenworldMapReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		ActivateOpenworldMapReq req = packet.GetDecodedBody<ActivateOpenworldMapReq>();
		Packet.c.Log($"[OW] ActivateOpenworldMapReq mapId={req.MapId} -> Succ");
		session.Send(Packet.FromProto(new ActivateOpenworldMapRsp
		{
			retcode = ActivateOpenworldMapRsp.Retcode.Succ,
			MapId = req.MapId
		}, CmdId.ActivateOpenworldMapRsp));
	}
}
