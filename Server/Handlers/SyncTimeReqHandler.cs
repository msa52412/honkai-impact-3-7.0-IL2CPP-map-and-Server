using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SyncTimeReq)]
internal class SyncTimeReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SyncTimeReq decodedBody = packet.GetDecodedBody<SyncTimeReq>();
		session.Send(Packet.FromProto(new SyncTimeRsp
		{
			retcode = SyncTimeRsp.Retcode.Succ,
			CurTime = (uint)Global.GetUnixInSeconds(),
			Seq = decodedBody.Seq
		}, CmdId.SyncTimeRsp));
	}
}
