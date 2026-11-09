using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;

namespace Common.Database;

/// <summary>
/// 舰桥（主界面）设置：BGM 播放模式 + 天气是否锁定。
///
/// 为什么要有这张表
/// ----------------
/// 客户端主界面右上角有个「实时天气」开关。切换时发 `SetWarshipSettingReq`：
///   setting_type = 1 (BGM_PLAY_MODE) -> bgm_play_mode
///   setting_type = 2 (BGM_WEATHER)    -> is_weather_fixed + weather_idx
/// 服务端原来**完全没实现**这个包（日志里一条条 `not handled!`），
/// 而 `GetWarshipDataRsp` 又把 IsWeatherFixed 写死成 true、WeatherIdx 写死成 3（雷雨），
/// 所以玩家开了实时天气、一重登就变回「锁定雷」。
///
/// 现在按玩家落库，GetWarshipDataRsp 读这里的值。
/// </summary>
public class WarshipSettings
{
	public class WarshipSettingsScheme : IRow
	{
		public long Id { get; set; }

		public long CreatedAt { get; set; }

		public uint OwnerUid { get; set; }

		public List<WarshipThemeData> Themes { get; set; } = new List<WarshipThemeData>();

		public void Save()
		{
			collection.ReplaceOne(this);
		}
	}

	/// <summary>
	/// 把所有玩家的所有舰桥天气设成同一档。
	///
	/// 为什么 GM 面板要直接改这个：客户端主界面的天气场景是拿 `WarshipThemeData.WeatherIdx`
	/// 去查 `ThemeWeatherConfig` 的（1=Sunny 2=Cloudy 3=Lightning 4/8/11=Cerah 5/6=Malam 7=Hujan），
	/// 而它几乎不主动去拉 `/game_weather/...`。所以想让游戏里天气真的变，只能改这里。
	/// </summary>
	public static void ApplyWeatherToAll(bool isFixed, uint weatherIdx)
	{
		foreach (WarshipSettingsScheme scheme in collection.All())
		{
			scheme.Themes ??= new List<WarshipThemeData>();
			foreach (uint id in DefaultWarshipIds)
			{
				WarshipThemeData theme = scheme.Themes.FirstOrDefault(t => t.WarshipId == id);
				if (theme == null)
				{
					theme = new WarshipThemeData { WarshipId = id, BgmPlayMode = 0u };
					scheme.Themes.Add(theme);
				}
				theme.IsWeatherFixed = isFixed;
				theme.WeatherIdx = weatherIdx;
			}
			scheme.Save();
		}
	}

	/// <summary>
	/// 客端的 WeatherType（0=None 1=晴 2=多云 3=阴 4=雨 5=雪 6=风 7=雷 8=极端）
	/// -> ThemeWeatherConfig 的 WeatherID。
	/// 主题表里只有 Sunny/Cloudy/Lightning/Cerah/Malam/Hujan 这几种，剩下的就近映射。
	/// </summary>
	public static uint WeatherIdOf(int weatherType)
	{
		switch (weatherType)
		{
			case 1: return 1u;   // 晴      -> Sunny
			case 2: return 2u;   // 多云    -> Cloudy
			case 3: return 2u;   // 阴      -> Cloudy（没有独立的「阴」）
			case 4: return 7u;   // 雨      -> Hujan
			case 5: return 2u;   // 雪      -> Cloudy（没有雪）
			case 6: return 2u;   // 风      -> Cloudy
			case 7: return 3u;   // 雷      -> Lightning
			case 8: return 3u;   // 极端    -> Lightning
			default: return 1u;  // None    -> Sunny
		}
	}

	/// <summary>
	/// 取某玩家某舰桥的天气/设置，组装成可以直接塞进回包的副本。
	///
	/// 主界面天气有**三处**会下发 WarshipThemeData，顺序很关键：
	///   1. GetMainDataRsp.WarshipTheme   —— 登录最先到，界面第一帧就用它
	///   2. GetWarshipDataRsp.WarshipLists —— 进舰桥页面时补
	///   3. SetWarshipRsp（内含一份 GetMainDataRsp）—— 切舰桥时
	/// 以前 1 和 3 都写死 `true / 3`（雷雨），于是登录会有几秒雷、切一下舰桥又变回雷。
	/// 三处必须读同一份数据。
	/// </summary>
	public static WarshipThemeData ThemeOf(uint uid, uint warshipId)
	{
		WarshipThemeData stored = Ensure(FromUid(uid), warshipId);
		return new WarshipThemeData
		{
			WarshipId = warshipId,
			BgmPlayMode = stored.BgmPlayMode,
			IsWeatherFixed = stored.IsWeatherFixed,
			WeatherIdx = stored.WeatherIdx
		};
	}

	/// <summary>默认给出来的舰桥 id（和客户端主界面能选的一致）。</summary>
	public static readonly uint[] DefaultWarshipIds = { 0u, 400002u, 400003u, 400004u, 400006u };

	public static readonly SqlTable<WarshipSettingsScheme> collection =
		Global.Table<WarshipSettingsScheme>("WarshipSettings",
			("owner_uid", "INTEGER", (WarshipSettingsScheme x) => x.OwnerUid, true));

	public static WarshipSettingsScheme FromUid(uint uid)
	{
		WarshipSettingsScheme? exist = collection.All()
			.FirstOrDefault(x => x.OwnerUid == uid);
		if (exist != null)
		{
			exist.Themes ??= new List<WarshipThemeData>();
			return exist;
		}
		WarshipSettingsScheme created = new WarshipSettingsScheme
		{
			OwnerUid = uid,
			Themes = new List<WarshipThemeData>()
		};
		collection.InsertOne(created);
		return created;
	}

	/// <summary>
	/// 取某舰桥的设置；没有就补一条默认的。
	/// 默认 IsWeatherFixed=false = 走实时天气（客户端会去拉 get_weather），
	/// WeatherIdx=1 只是兜底，固定天气时才用。
	/// </summary>
	public static WarshipThemeData Ensure(WarshipSettingsScheme scheme, uint warshipId)
	{
		WarshipThemeData? theme = scheme.Themes.FirstOrDefault(t => t.WarshipId == warshipId);
		if (theme != null)
		{
			return theme;
		}
		theme = new WarshipThemeData
		{
			WarshipId = warshipId,
			BgmPlayMode = 0u,
			IsWeatherFixed = false,
			WeatherIdx = 1u
		};
		scheme.Themes.Add(theme);
		scheme.Save();
		return theme;
	}
}
