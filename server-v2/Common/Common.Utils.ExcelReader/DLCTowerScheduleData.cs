using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// DLCTowerSchedule.json（454 行）—— 后崩坏书爬塔（无限塔）的赛季配置。
///
/// 客户端 DLCTowerData.Refresh(GetDLCTowerRsp)（0x18074DA80）：
///   scheduleMetaData = ExcelMeta.DLCTowerSchedule.Get(rsp.schedule_id)   ← 找不到就 null
///   currentFloorMetaData = DLCTowerFloor[(progress+1), scheduleMetaData.ConfigType]
///   IsTowerFinished = max_progress &gt;= scheduleMetaData.MaxFloor
///   CanTowerBattle = progress &lt; scheduleMetaData.MaxFloor
///
/// 也就是说 schedule_id 一旦给错（表里没有 / ConfigType 对不上），
/// 界面就是「0 层 / 已结束」。表里 454 个赛季按周排到 2035 年，
/// 用 <see cref="Current"/> 取「当前时间所在的那一个」即可。
/// </summary>
public class DLCTowerScheduleData : BaseExcelReader<DLCTowerScheduleData, DLCTowerScheduleDataExcel>
{
	public override string FileName => "DLCTowerSchedule.json";

	private const string TimeFormat = "yyyy-MM-dd HH:mm:ss";

	/// <summary>
	/// 当前应当生效的赛季：先找 StartTime ≤ now ≤ EndTime 的那条；
	/// 找不到（表比现在还旧）就取 EndTime 最晚的一条，保证界面永远有内容。
	/// </summary>
	public DLCTowerScheduleDataExcel Current()
	{
		DLCTowerScheduleDataExcel[] all = All;
		if (all == null || all.Length == 0)
		{
			return null;
		}

		DateTime now = DateTime.Now;
		DLCTowerScheduleDataExcel latest = null;
		DateTime latestEnd = DateTime.MinValue;

		foreach (DLCTowerScheduleDataExcel row in all)
		{
			DateTime start;
			DateTime end;
			if (!TryParse(row.StartTime, out start) || !TryParse(row.EndTime, out end))
			{
				continue;
			}
			if (start <= now && now <= end)
			{
				return row;
			}
			if (end > latestEnd)
			{
				latestEnd = end;
				latest = row;
			}
		}
		return latest;
	}

	public DLCTowerScheduleDataExcel Get(int id)
	{
		DLCTowerScheduleDataExcel[] all = All;
		if (all == null)
		{
			return null;
		}
		foreach (DLCTowerScheduleDataExcel row in all)
		{
			if (row.ID == id)
			{
				return row;
			}
		}
		return null;
	}

	private static bool TryParse(string text, out DateTime value)
	{
		if (string.IsNullOrEmpty(text))
		{
			value = DateTime.MinValue;
			return false;
		}
		return DateTime.TryParseExact(text, TimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out value)
			|| DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.None, out value);
	}
}

public class DLCTowerScheduleDataExcel
{
	[JsonProperty("ID")]
	public int ID { get; set; }

	/// <summary>下一赛季 id（表里自带链表，这里只做记录）。</summary>
	[JsonProperty("NextID")]
	public int NextID { get; set; }

	[JsonProperty("StartTime")]
	public string StartTime { get; set; }

	[JsonProperty("EndTime")]
	public string EndTime { get; set; }

	[JsonProperty("SettleTime")]
	public string SettleTime { get; set; }

	/// <summary>本赛季使用的关卡 id（DLCTowerFloor 的关卡资源）。</summary>
	[JsonProperty("LevelID")]
	public int LevelID { get; set; }

	/// <summary>本赛季总层数（客户端 IsTowerFinished / CanTowerBattle 都用它）。</summary>
	[JsonProperty("MaxFloor")]
	public int MaxFloor { get; set; }

	[JsonProperty("Weather")]
	public int Weather { get; set; }

	/// <summary>赛季增益 id（客户端 GetBonusList）。</summary>
	[JsonProperty("Bonus")]
	public int[] Bonus { get; set; }

	/// <summary>楼层配置分组，决定 DLCTowerFloor 里查哪一组行（1201 / 1202 …）。</summary>
	[JsonProperty("ConfigType")]
	public int ConfigType { get; set; }
}
