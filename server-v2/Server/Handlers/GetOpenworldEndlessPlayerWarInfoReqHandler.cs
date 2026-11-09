using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 3443/3444 —— 查看某人的战争信息（谁给他放了入侵怪 / 他放过谁）。
///
/// 服务端没有入侵系统，回空列表 + 目标 uid 即可。客户端对空列表是安全的：
/// 它只是没东西可铺，不会像空 GroupInfo 那样抛 NRE。
/// </summary>
[PacketCmdId(CmdId.GetOpenworldEndlessPlayerWarInfoReq)]
internal class GetOpenworldEndlessPlayerWarInfoReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetOpenworldEndlessPlayerWarInfoReq req = packet.GetDecodedBody<GetOpenworldEndlessPlayerWarInfoReq>();
		Packet.c.Log($"[Abyss] PlayerWarInfo target={req.TargetUid}");

		session.Send(Packet.FromProto(new GetOpenworldEndlessPlayerWarInfoRsp
		{
			retcode = GetOpenworldEndlessPlayerWarInfoRsp.Retcode.Succ,
			TargetUid = req.TargetUid
		}, CmdId.GetOpenworldEndlessPlayerWarInfoRsp));
	}
}
