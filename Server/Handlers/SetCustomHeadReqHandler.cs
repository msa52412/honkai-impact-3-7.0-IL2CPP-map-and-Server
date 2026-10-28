using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Three;

[PacketCmdId(CmdId.SetCustomHeadReq)]
internal class SetCustomHeadReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SetCustomHeadReq decodedBody = packet.GetDecodedBody<SetCustomHeadReq>();
		session.Player.User.CustomHeadId = (int)decodedBody.Id;
		GetMainDataRsp getMainDataRsp = new GetMainDataRsp();
		getMainDataRsp.retcode = GetMainDataRsp.Retcode.Succ;
		getMainDataRsp.CustomHeadId = decodedBody.Id;
		getMainDataRsp.TypeLists = new uint[1] { 36u };
		GetMainDataRsp proto = getMainDataRsp;
		session.Send(Packet.FromProto(proto, CmdId.GetMainDataRsp), Packet.FromProto(new SetCustomHeadRsp
		{
			retcode = SetCustomHeadRsp.Retcode.Succ
		}, CmdId.SetCustomHeadRsp));
	}
}
