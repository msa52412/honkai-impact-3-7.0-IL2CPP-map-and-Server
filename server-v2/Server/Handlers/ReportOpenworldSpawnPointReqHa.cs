using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld;

[PacketCmdId(CmdId.ReportOpenworldSpawnPointReq)]
internal class ReportOpenworldSpawnPointReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		ReportOpenworldSpawnPointReq Data = packet.GetDecodedBody<ReportOpenworldSpawnPointReq>();
		ReportOpenworldSpawnPointRsp proto = new ReportOpenworldSpawnPointRsp
		{
			retcode = ReportOpenworldSpawnPointRsp.Retcode.Succ,
			MapId = Data.MapId,
			PointInfo = Data.PointInfo
		};
		OpenWorldScheme openWorldScheme = session.Player.OpenWorlds.FirstOrDefault((OpenWorldScheme x) => x.MapId == Data.MapId);
		if (openWorldScheme != null)
		{
			openWorldScheme.SpawnPoint = Data.PointInfo;
		}
		session.Send(Packet.FromProto(proto, CmdId.ReportOpenworldSpawnPointRsp));
	}
}
