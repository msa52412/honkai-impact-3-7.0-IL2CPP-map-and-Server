using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetGrandKeyReq)]
internal class GrandKeyReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetGrandKeyReq decodedBody = packet.GetDecodedBody<GetGrandKeyReq>();
		GetGrandKeyRsp getGrandKeyRsp = new GetGrandKeyRsp();
		getGrandKeyRsp.retcode = GetGrandKeyRsp.Retcode.Fail;
		session.Send(Packet.FromProto(getGrandKeyRsp, CmdId.GetGrandKeyRsp));
	}
}
