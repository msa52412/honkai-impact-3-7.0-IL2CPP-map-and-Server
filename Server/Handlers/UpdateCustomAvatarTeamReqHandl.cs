using System.Linq;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.UpdateCustomAvatarTeamReq)]
internal class UpdateCustomAvatarTeamReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		UpdateCustomAvatarTeamReq Data = packet.GetDecodedBody<UpdateCustomAvatarTeamReq>();
		UpdateCustomAvatarTeamRsp proto = new UpdateCustomAvatarTeamRsp
		{
			retcode = UpdateCustomAvatarTeamRsp.Retcode.Succ
		};
		CustomAvatarTeam customAvatarTeam = session.Player.User.CustomAvatarTeamList.FirstOrDefault((CustomAvatarTeam x) => x.TeamId == Data.Team.TeamId);
		if (customAvatarTeam != null)
		{
			customAvatarTeam.AvatarIdLists = Data.Team.AvatarIdLists;
			customAvatarTeam.ElfIdLists = Data.Team.ElfIdLists;
			customAvatarTeam.Name = Data.Team.Name;
		}
		else
		{
			session.Player.User.CustomAvatarTeamList.Add(Data.Team);
		}
		GetAvatarTeamDataRsp getAvatarTeamDataRsp = new GetAvatarTeamDataRsp
		{
			retcode = GetAvatarTeamDataRsp.Retcode.Succ
		};
		getAvatarTeamDataRsp.CustomAvatarTeamLists.Add(Data.Team);
		session.Send(Packet.FromProto(getAvatarTeamDataRsp, CmdId.GetAvatarTeamDataRsp), Packet.FromProto(proto, CmdId.UpdateCustomAvatarTeamRsp));
	}
}
