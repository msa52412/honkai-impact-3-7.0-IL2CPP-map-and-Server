using System.Linq;
using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetGodWarReq)]
internal class GetGodWarReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetGodWarRsp proto = new GetGodWarRsp
		{
			retcode = GetGodWarRsp.Retcode.Succ
		};
		GodWar godWar = new GodWar
		{
			GodWarId = 1u,
			BeginTime = 0u,
			EndTime = (uint)((int)Global.GetUnixInSeconds() * 12) / 10u,
			MaxSupportPoint = 95u,
			LobbyId = 1u,
			CurChapterId = 1u,
			RoleInfo = new GodWarRoleInfo()
		};
		godWar.ChapterLists.AddRange(new uint[3] { 1u, 2u, 3u }.Select((uint x) => new GodWarChapter
		{
			ChapterId = x
		}));
		session.Send(Packet.FromProto(proto, CmdId.GetGodWarRsp));
	}
}
