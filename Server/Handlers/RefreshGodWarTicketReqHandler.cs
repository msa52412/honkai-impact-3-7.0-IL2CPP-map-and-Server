using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.RefreshGodWarTicketReq)]
internal class RefreshGodWarTicketReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		RefreshGodWarTicketReq decodedBody = packet.GetDecodedBody<RefreshGodWarTicketReq>();
		session.Send(Packet.FromProto(new RefreshGodWarTicketRsp
		{
			retcode = RefreshGodWarTicketRsp.Retcode.Succ,
			GodWarId = decodedBody.GodWarId
		}, CmdId.RefreshGodWarTicketRsp));
	}
}
