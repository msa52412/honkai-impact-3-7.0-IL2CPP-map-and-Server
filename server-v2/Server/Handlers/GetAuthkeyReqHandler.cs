using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetAuthkeyReq)]
internal class GetAuthkeyReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetAuthkeyReq decodedBody = packet.GetDecodedBody<GetAuthkeyReq>();
		GetAuthkeyRsp proto = new GetAuthkeyRsp
		{
			retcode = GetAuthkeyRsp.Retcode.Succ,
			AuthAppid = decodedBody.AuthAppid,
			Authkey = "0",
			SignType = 2u,
			AuthkeyVer = 1u
		};
		session.Send(Packet.FromProto(proto, CmdId.GetAuthkeyRsp));
	}
}
