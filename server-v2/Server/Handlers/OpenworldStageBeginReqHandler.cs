using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld;

[PacketCmdId(CmdId.OpenworldStageBeginReq)]
internal class OpenworldStageBeginReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		OpenworldStageBeginReq decodedBody = packet.GetDecodedBody<OpenworldStageBeginReq>();
		session.Send(Packet.FromProto(new OpenworldStageBeginRsp
		{
			retcode = OpenworldStageBeginRsp.Retcode.Succ,
			MapId = decodedBody.MapId
		}, CmdId.OpenworldStageBeginRsp));
	}
}
