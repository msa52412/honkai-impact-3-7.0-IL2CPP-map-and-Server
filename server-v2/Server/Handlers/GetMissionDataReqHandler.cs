using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetMissionDataReq)]
internal class GetMissionDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		session.Send(Packet.FromProto(MissionDataBuilder.Build(session.Player), CmdId.GetMissionDataRsp));
        DailyGameplay.PushDuty(session);
	}
}
