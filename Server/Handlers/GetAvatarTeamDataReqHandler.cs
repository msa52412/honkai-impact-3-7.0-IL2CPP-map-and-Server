using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetAvatarTeamDataReq)]
internal class GetAvatarTeamDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetAvatarTeamDataRsp getAvatarTeamDataRsp = new GetAvatarTeamDataRsp
		{
			retcode = GetAvatarTeamDataRsp.Retcode.Succ
		};
		getAvatarTeamDataRsp.AvatarTeamLists.AddRange(session.Player.User.AvatarTeamList);
		getAvatarTeamDataRsp.CustomAvatarTeamLists.AddRange(session.Player.User.CustomAvatarTeamList);
		session.Send(Packet.FromProto(getAvatarTeamDataRsp, CmdId.GetAvatarTeamDataRsp));
	}
}
