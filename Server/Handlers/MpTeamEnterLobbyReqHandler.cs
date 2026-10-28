using Common.Resources.Proto;
using PemukulPaku.GameServer.MPModule;

namespace PemukulPaku.GameServer.Handlers.One;

[PacketCmdId(CmdId.MpTeamEnterLobbyReq)]
public class MpTeamEnterLobbyReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		MpTeamEnterLobbyReq decodedBody = packet.GetDecodedBody<MpTeamEnterLobbyReq>();
		Lobby.GetInstance().Teams.TryGetValue(session.Player.User.Uid, out var value);
		MpTeamEnterLobbyRsp mpTeamEnterLobbyRsp = new MpTeamEnterLobbyRsp
		{
			retcode = MpTeamEnterLobbyRsp.Retcode.NotInTeam
		};
		if (value != null)
		{
			value.StageId = decodedBody.StageId;
			Lobby.GetInstance().SyncTeam(session.Player.User.Uid);
			mpTeamEnterLobbyRsp.retcode = MpTeamEnterLobbyRsp.Retcode.Succ;
			mpTeamEnterLobbyRsp.StageId = value.StageId;
			mpTeamEnterLobbyRsp.TeamId = value.LeaderUid;
			mpTeamEnterLobbyRsp.TeamName = value.Name;
		}
		session.Send(Packet.FromProto(mpTeamEnterLobbyRsp, CmdId.MpTeamEnterLobbyRsp));
	}
}
