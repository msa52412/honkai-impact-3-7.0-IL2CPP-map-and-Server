using System.Collections.Generic;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 舰桥数据（主界面）。里面的 WarshipThemeData 决定主界面天气：
///   IsWeatherFixed = true  -> 用 WeatherIdx 指定的那一种（ThemeWeatherConfig 的 WeatherID）
///   IsWeatherFixed = false -> 走实时天气，客户端会去请求 /game_weather/weather/get_weather
///
/// 以前这里把两者写死成 `true / 3`（3 = 雷雨），所以玩家在主界面永远看到雷，
/// 而且在客户端开了「实时天气」也会被这套写死的值顶回去。
/// 现在从 WarshipSettings 表读，默认 IsWeatherFixed=false（实时）。
/// </summary>
[PacketCmdId(CmdId.GetWarshipDataReq)]
internal class GetWarshipDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		WarshipSettings.WarshipSettingsScheme scheme = WarshipSettings.FromUid(session.Player.User.Uid);

		GetWarshipDataRsp rsp = new GetWarshipDataRsp
		{
			retcode = GetWarshipDataRsp.Retcode.Succ,
			IsAll = true
		};

		List<WarshipThemeData> list = new List<WarshipThemeData>();
		foreach (uint id in WarshipSettings.DefaultWarshipIds)
		{
			WarshipThemeData stored = WarshipSettings.Ensure(scheme, id);
			list.Add(new WarshipThemeData
			{
				WarshipId = id,
				BgmPlayMode = stored.BgmPlayMode,
				IsWeatherFixed = stored.IsWeatherFixed,
				WeatherIdx = stored.WeatherIdx
			});
		}

		rsp.WarshipLists.AddRange(list);
		session.Send(Packet.FromProto(rsp, CmdId.GetWarshipDataRsp));
	}
}
