using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.StageBeginReq)]
internal class StageBeginReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		StageBeginReq decodedBody = packet.GetDecodedBody<StageBeginReq>();
		StageBeginRsp proto = new StageBeginRsp
		{
			retcode = StageBeginRsp.Retcode.Succ,
			StageId = decodedBody.StageId,
			Progress = 0u,
			IsCollectCheatData = false
		};
		session.Send(Packet.FromProto(proto, CmdId.StageBeginRsp));
	}
}
