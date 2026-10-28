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
		// 舰桥天气固定为雷雨：WeatherID 3 = Lightning。
		getMainDataRsp.WarshipTheme = new WarshipThemeData
		{
			WarshipId = (uint)session.Player.User.WarshipId,
			IsWeatherFixed = true,
			WeatherIdx = 3u
		};
		getMainDataRsp.TypeLists = new uint[1] { 38u };
		GetMainDataRsp proto = getMainDataRsp;
		SetWarshipRsp proto2 = new SetWarshipRsp
		{
			retcode = SetWarshipRsp.Retcode.Succ
		};
		session.Send(Packet.FromProto(proto, CmdId.GetMainDataRsp), Packet.FromProto(proto2, CmdId.SetWarshipRsp));
	}
}
