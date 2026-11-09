using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.ClientReportReq)]
internal class ClientReportReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		ClientReportReq decodedBody = packet.GetDecodedBody<ClientReportReq>();
		if (Global.config.VerboseLevel > VerboseLevel.Silent)
		{
			session.c.Warn("ClientReport | " + decodedBody.ReportType + " =  " + decodedBody.ReportValue);
		}
		session.Send(Packet.FromProto(new ClientReportRsp
		{
			retcode = ClientReportRsp.Retcode.Succ
		}, CmdId.ClientReportRsp));
	}
}
