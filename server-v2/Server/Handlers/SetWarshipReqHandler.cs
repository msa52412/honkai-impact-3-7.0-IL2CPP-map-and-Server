using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SetWarshipReq)]
internal class SetWarshipReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SetWarshipReq decodedBody = packet.GetDecodedBody<SetWarshipReq>();
		session.Player.User.WarshipId = (int)decodedBody.WarshipId;
		GetMainDataRsp getMainDataRsp = new GetMainDataRsp();
		getMainDataRsp.retcode = GetMainDataRsp.Retcode.Succ;
                // 舰桥天气跟着 WarshipSettings 走（以前写死 true / 3 雷雨，切一次舰桥就被重置）
                getMainDataRsp.WarshipTheme = Common.Database.WarshipSettings.ThemeOf(
                        session.Player.User.Uid, (uint)session.Player.User.WarshipId);
		getMainDataRsp.TypeLists = new uint[1] { 38u };
		GetMainDataRsp proto = getMainDataRsp;
		SetWarshipRsp proto2 = new SetWarshipRsp
		{
			retcode = SetWarshipRsp.Retcode.Succ
		};
		session.Send(Packet.FromProto(proto, CmdId.GetMainDataRsp), Packet.FromProto(proto2, CmdId.SetWarshipRsp));
	}
}
