using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Two;

[PacketCmdId(CmdId.SetFrameUseReq)]
internal class SetFrameUseReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SetFrameUseReq decodedBody = packet.GetDecodedBody<SetFrameUseReq>();
		session.Player.User.FrameId = (int)decodedBody.FrameId;
		GetMainDataRsp getMainDataRsp = new GetMainDataRsp();
		getMainDataRsp.retcode = GetMainDataRsp.Retcode.Succ;
		getMainDataRsp.UseFrameId = decodedBody.FrameId;
		getMainDataRsp.TypeLists = new uint[1] { 26u };
		GetMainDataRsp proto = getMainDataRsp;
		session.Send(Packet.FromProto(proto, CmdId.GetMainDataRsp), Packet.FromProto(new SetFrameUseRsp
		{
			FrameId = decodedBody.FrameId,
			retcode = SetFrameUseRsp.Retcode.Succ
		}, CmdId.SetFrameUseRsp));
	}
}
