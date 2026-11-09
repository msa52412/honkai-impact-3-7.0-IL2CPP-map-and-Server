using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 舰桥设置（SetWarshipSettingReq）—— 主界面右上角那个「实时天气」开关走的就是这里。
///
/// 老实现完全没有（日志里一条条 `SetWarshipSettingReq not handled!`），
/// 玩家开了实时天气，服务端不存，重登时 GetWarshipDataRsp 又回写死的
/// IsWeatherFixed=true / WeatherIdx=3（雷雨）→ 表现为「重置回锁定雷」。
///
/// setting_type 只有两种，别混着写：
///   1 BGM_PLAY_MODE -> bgm_play_mode
///   2 BGM_WEATHER   -> is_weather_fixed + weather_idx
/// </summary>
[PacketCmdId(CmdId.SetWarshipSettingReq)]
internal class SetWarshipSettingReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SetWarshipSettingReq req = packet.GetDecodedBody<SetWarshipSettingReq>();
		WarshipSettings.WarshipSettingsScheme scheme = WarshipSettings.FromUid(session.Player.User.Uid);
		WarshipThemeData theme = WarshipSettings.Ensure(scheme, req.WarshipId);

		if (req.SettingType == WarshipSettingType.WarshipSettingBgmPlayMode)
		{
			theme.BgmPlayMode = (uint)req.BgmPlayMode;
		}
		else
		{
			theme.IsWeatherFixed = req.IsWeatherFixed;
			theme.WeatherIdx = req.WeatherIdx;
		}
		scheme.Save();

		Packet.c.Log("[Warship] 设置 warship=" + req.WarshipId
			+ " type=" + req.SettingType
			+ " -> bgm=" + theme.BgmPlayMode
			+ " weatherFixed=" + theme.IsWeatherFixed
			+ " weatherIdx=" + theme.WeatherIdx);

		session.Send(Packet.FromProto(new SetWarshipSettingRsp
		{
			retcode = SetWarshipSettingRsp.Retcode.Succ
		}, CmdId.SetWarshipSettingRsp));

		// 推一份最新的舰桥数据，界面立刻刷新（不用重登）
		session.ProcessPacket(Packet.FromProto(new GetWarshipDataReq(), CmdId.GetWarshipDataReq));
	}
}
