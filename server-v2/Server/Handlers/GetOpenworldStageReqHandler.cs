using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld;

[PacketCmdId(CmdId.GetOpenworldStageReq)]
internal class GetOpenworldStageReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetOpenworldStageReq decodedBody = packet.GetDecodedBody<GetOpenworldStageReq>();
		session.Send(Packet.FromProto(new GetOpenworldStageRsp
		{
			retcode = GetOpenworldStageRsp.Retcode.Succ,
			MapId = decodedBody.MapId,
			MechaLostHpPercent = 0u,
			MechaLostSpPercent = 0u,
			MapEnergy = 0u,
			ScDlcFeverScore = 0u,
			ScDlcClimaxScore = 0u
		}, CmdId.GetOpenworldStageRsp));
	}
}
