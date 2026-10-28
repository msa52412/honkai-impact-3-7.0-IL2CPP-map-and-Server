using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SetSelfDescReq)]
internal class SetSelfDescReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SetSelfDescReq decodedBody = packet.GetDecodedBody<SetSelfDescReq>();
		session.Player.User.SelfDesc = decodedBody.SelfDesc;
		GetMainDataRsp getMainDataRsp = new GetMainDataRsp();
		getMainDataRsp.retcode = GetMainDataRsp.Retcode.Succ;
		getMainDataRsp.SelfDesc = decodedBody.SelfDesc;
		getMainDataRsp.TypeLists = new uint[1] { 16u };
		GetMainDataRsp proto = getMainDataRsp;
		SetSelfDescRsp proto2 = new SetSelfDescRsp
		{
			retcode = SetSelfDescRsp.Retcode.Succ
		};
		session.Send(Packet.FromProto(proto, CmdId.GetMainDataRsp), Packet.FromProto(proto2, CmdId.SetSelfDescRsp));
	}
}
