using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.ReportBirthdayReq)]
internal class ReportBirthdayReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		ReportBirthdayReq decodedBody = packet.GetDecodedBody<ReportBirthdayReq>();
		session.Player.User.BirthDate = (int)decodedBody.Birthday;
		GetMainDataRsp getMainDataRsp = new GetMainDataRsp();
		getMainDataRsp.retcode = GetMainDataRsp.Retcode.Succ;
		getMainDataRsp.Birthday = decodedBody.Birthday;
		getMainDataRsp.TypeLists = new uint[1] { 21u };
		GetMainDataRsp proto = getMainDataRsp;
		ReportBirthdayRsp proto2 = new ReportBirthdayRsp
		{
			retcode = ReportBirthdayRsp.Retcode.Succ
		};
		session.Send(Packet.FromProto(proto, CmdId.GetMainDataRsp), Packet.FromProto(proto2, CmdId.ReportBirthdayRsp));
	}
}
