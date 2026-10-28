using System.Collections.Generic;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetWarshipDataReq)]
internal class GetWarshipDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetWarshipDataRsp getWarshipDataRsp = new GetWarshipDataRsp
		{
			retcode = GetWarshipDataRsp.Retcode.Succ,
			IsAll = true
		};
		// 舰桥（主界面）天气：固定为 WeatherIdx = 3。
		// ThemeWeatherConfig 里 WeatherID 3 = "Lightning"（雷雨，AtmosphereConfigID 3 = Lightning）。
		// IsWeatherFixed=false 时客户端走实时天气（Sina），0 表示不固定；这里强制固定。
		List<WarshipThemeData> collection = new List<WarshipThemeData>
		{
			new WarshipThemeData
			{
				WarshipId = 0u,
				BgmPlayMode = 0u,
				IsWeatherFixed = true,
				WeatherIdx = 3u
			},
			new WarshipThemeData
			{
				WarshipId = 400002u,
				BgmPlayMode = 0u,
				IsWeatherFixed = true,
				WeatherIdx = 3u
			},
			new WarshipThemeData
			{
				WarshipId = 400003u,
				BgmPlayMode = 0u,
				IsWeatherFixed = true,
				WeatherIdx = 3u
			},
			new WarshipThemeData
			{
				WarshipId = 400004u,
				BgmPlayMode = 0u,
				IsWeatherFixed = true,
				WeatherIdx = 3u
			},
			new WarshipThemeData
			{
				WarshipId = 400006u,
				BgmPlayMode = 0u,
				IsWeatherFixed = true,
				WeatherIdx = 3u
			}
		};
		getWarshipDataRsp.WarshipLists.AddRange(collection);
		session.Send(Packet.FromProto(getWarshipDataRsp, CmdId.GetWarshipDataRsp));
	}
}
