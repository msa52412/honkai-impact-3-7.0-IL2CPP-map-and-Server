using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.NicknameModifyReq)]
internal class NicknameModifyReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		NicknameModifyReq decodedBody = packet.GetDecodedBody<NicknameModifyReq>();
		session.Player.User.Nick = decodedBody.Nickname;
		GetMainDataRsp getMainDataRsp = new GetMainDataRsp();
		getMainDataRsp.retcode = GetMainDataRsp.Retcode.Succ;
		getMainDataRsp.Nickname = session.Player.User.Nick;
		getMainDataRsp.TypeLists = new uint[1] { 2u };
		GetMainDataRsp proto = getMainDataRsp;
		session.Send(Packet.FromProto(proto, CmdId.GetMainDataRsp), Packet.FromProto(new NicknameModifyRsp
		{
			retcode = NicknameModifyRsp.Retcode.Succ
		}, CmdId.NicknameModifyRsp));
	}
}
