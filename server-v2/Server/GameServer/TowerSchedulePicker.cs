using System;
using System.Collections.Generic;
using System.Globalization;

namespace PemukulPaku.GameServer;

/// <summary>赛季表里挑行要用到的最小字段集（两套爬塔的 Excel 类字段名一样但类型不同，这里统一一下）。</summary>
internal readonly struct TowerSeason
{
	public readonly int Id;
	public readonly int Weather;
	public readonly string StartTime;
	public readonly string EndTime;

	public TowerSeason(int id, int weather, string startTime, string endTime)
	{
		Id = id;
		Weather = weather;
		StartTime = startTime;
		EndTime = endTime;
	}
}

/// <summary>
/// 按天气挑赛季。
///
/// 爬塔的天气只能靠「赛季」带出来（客户端查 schedule.Weather），所以「切天气」= 切赛季。
/// 一个天气在 454 / 733 期赛季里会出现上百次，选哪一期：
///
///   1. 当前时间正好命中某一期 → 直接用（最自然）
///   2. 否则取**开始时间在未来的最早一期**（读起来像「即将开始」，不会显示成已结束）
///   3. 全都过去了 → 取最后一期
///
/// 不选「离现在最近的过去那期」，是因为客户端会按赛季起止时间显示状态，
/// 给一期三个月前的赛季很容易被画成「已结束」，看着像没生效。
/// </summary>
internal static class TowerSchedulePicker
{
	private const string TimeFormat = "yyyy-MM-dd HH:mm:ss";

	public static int PickByWeather(IEnumerable<TowerSeason> rows, int weather, DateTime now)
	{
		if (weather == 0 || rows == null)
		{
			return 0;
		}

		int futureId = 0;
		DateTime futureStart = DateTime.MaxValue;
		int lastId = 0;
		DateTime lastEnd = DateTime.MinValue;

		foreach (TowerSeason row in rows)
		{
			if (row.Weather != weather)
			{
				continue;
			}

			DateTime start;
			DateTime end;
			bool hasStart = TryParse(row.StartTime, out start);
			bool hasEnd = TryParse(row.EndTime, out end);

			if (hasStart && hasEnd && start <= now && now <= end)
			{
				return row.Id;      // 1. 当前命中
			}

			if (hasStart && start > now && start < futureStart)
			{
				futureStart = start;    // 2. 未来最近的一期
				futureId = row.Id;
			}

			if (hasEnd && end > lastEnd)
			{
				lastEnd = end;          // 3. 全都过去了就取最后一期
				lastId = row.Id;
			}
		}

		return futureId != 0 ? futureId : lastId;
	}

	public static bool TryParse(string text, out DateTime value)
	{
		value = DateTime.MinValue;
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		return DateTime.TryParseExact(text, TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out value)
			|| DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.None, out value);
	}

	/// <summary>把「这期赛季长什么样」拼成一句话，给 GM 面板回显。</summary>
	public static string Describe(int id, int weather, int configType, int maxFloor, string start, string end)
	{
		return $"赛季 {id} · 天气 {weather} · ConfigType {configType} · {maxFloor} 层 · {start} ~ {end}";
	}
}
