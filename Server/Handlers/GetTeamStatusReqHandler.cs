using System.Linq;
using Common.Resources.Proto;
using PemukulPaku.GameServer.MPModule;

namespace PemukulPaku.GameServer.Handlers.Three;

[PacketCmdId(CmdId.GetTeamStatusReq)]
internal class GetTeamStatusReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetTeamStatusReq Data = packet.GetDecodedBody<GetTeamStatusReq>();
		GetTeamStatusRsp getTeamStatusRsp = new GetTeamStatusRsp
		{
			retcode = GetTeamStatusRsp.Retcode.Succ
		};
		getTeamStatusRsp.TeamDataLists.AddRange(from x in Lobby.GetInstance().Teams.Values
			where Data.TeamIdLists.Contains(x.LeaderUid)
			select x.GetMpTeamData());
		session.Send(Packet.FromProto(getTeamStatusRsp, CmdId.GetTeamStatusRsp));
	}
}
