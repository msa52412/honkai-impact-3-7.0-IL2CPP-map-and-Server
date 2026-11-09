using Common.Resources.Proto;
using PemukulPaku.GameServer.MPModule;

namespace PemukulPaku.GameServer.Handlers.One;

[PacketCmdId(CmdId.CreateLobbyReq)]
internal class CreateLobbyReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		CreateLobbyReq decodedBody = packet.GetDecodedBody<CreateLobbyReq>();
		Team team = Lobby.GetInstance().CreateTeam(new Team(decodedBody.StageId, decodedBody.MinLevel, decodedBody.LobbyEnterType, in session, decodedBody.TeamName));
		CreateLobbyRsp proto = new CreateLobbyRsp
		{
			retcode = CreateLobbyRsp.Retcode.Succ,
			LobbyId = team.LeaderUid,
			StageId = team.StageId,
			MinLevel = team.MinLevel,
			LobbyEnterType = team.LobbyEnterType,
			MaxLevel = 0u,
			TeamName = team.Name
		};
		session.Send(Packet.FromProto(proto, CmdId.CreateLobbyRsp));
	}
}
