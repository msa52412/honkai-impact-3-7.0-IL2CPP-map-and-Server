using System.Linq;
using Common.Resources.Proto;
using PemukulPaku.GameServer.MPModule;

namespace PemukulPaku.GameServer.Handlers.Three;

[PacketCmdId(CmdId.GetTeamListReq)]
internal class GetTeamListReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetTeamListReq Data = packet.GetDecodedBody<GetTeamListReq>();
		GetTeamListRsp getTeamListRsp = new GetTeamListRsp
		{
			retcode = GetTeamListRsp.Retcode.Succ
		};
		getTeamListRsp.TeamDataLists.AddRange(from x in Lobby.GetInstance().Teams.Values
			where Data.StageIdLists.Contains(x.StageId)
			select x.GetMpTeamData());
		session.Send(Packet.FromProto(getTeamListRsp, CmdId.GetTeamListRsp));
	}
}
