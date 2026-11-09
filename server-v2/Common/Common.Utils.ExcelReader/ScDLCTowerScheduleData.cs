using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// ScDLCTowerSchedule.json（733 行）—— 后崩坏书 2 爬塔（DLC2 无限塔）的赛季配置。
///
/// 客户端 DLC2TowerData.Refresh(GetScDLCTowerRsp)（0x18244F120）逐字段赋值：
///   this[+0x10] = rsp.schedule_id      ← 随后用它查 ScDLCTowerSchedule，查不到 [+0x48] = null
///   this[+0x1c] = rsp.max_floor
///   SetCurFloor(rsp.cur_floor)
///   this[+0x20] = rsp.avatar_revive_times
///   this[+0x24] = rsp.max_can_sweep_floor
///   this[+0x30] = rsp.max_score
///
/// schedule_id 给错（表里没有）时不会崩，但赛季信息、天气、加成、层数全部缺失，
/// 塔页会显示成「0 层 / 未开放」。表里 733 个赛季按周排到 2036 年，
/// 用 <see cref="Current"/> 取「当前时间所在的那一个」。
/// </summary>
public class ScDLCTowerScheduleData : BaseExcelReader<ScDLCTowerScheduleData, ScDLCTowerScheduleDataExcel>
{
	public override string FileName => "ScDLCTowerSchedule.json";

	private const string TimeFormat = "yyyy-MM-dd HH:mm:ss";

	/// <summary>
	/// 当前生效的赛季：StartTime ≤ now ≤ EndTime 的那条；
	/// 找不到就取 EndTime 最晚的一条，保证界面永远有内容。
	/// </summary>
	public ScDLCTowerScheduleDataExcel Current()
	{
		ScDLCTowerScheduleDataExcel[] all = All;
		if (all == null || all.Length == 0)
		{
			return null;
		}

		DateTime now = DateTime.Now;
		ScDLCTowerScheduleDataExcel latest = null;
		DateTime latestEnd = DateTime.MinValue;

		foreach (ScDLCTowerScheduleDataExcel row in all)
		{
			if (!TryParse(row.StartTime, out DateTime start) || !TryParse(row.EndTime, out DateTime end))
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

	public ScDLCTowerScheduleDataExcel Get(int id)
	{
		ScDLCTowerScheduleDataExcel[] all = All;
		if (all == null)
		{
			return null;
		}
		foreach (ScDLCTowerScheduleDataExcel row in all)
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

public class ScDLCTowerScheduleDataExcel
{
	/// <summary>赛季 id（= 协议里的 schedule_id）。</summary>
	[JsonProperty("ID")]
	public int ID { get; set; }

	[JsonProperty("NextID")]
	public int NextID { get; set; }

	[JsonProperty("StartTime")]
	public string StartTime { get; set; }

	[JsonProperty("EndTime")]
	public string EndTime { get; set; }

	[JsonProperty("SettleTime")]
	public string SettleTime { get; set; }

	/// <summary>本赛季使用的关卡 id。</summary>
	[JsonProperty("LevelID")]
	public int LevelID { get; set; }

	/// <summary>本赛季总层数（客户端 max_floor / IsTowerFinished 的基准）。</summary>
	[JsonProperty("MaxFloor")]
	public int MaxFloor { get; set; }

	[JsonProperty("Weather")]
	public int Weather { get; set; }

	/// <summary>赛季增益 id 列表（客户端 SetupBonus 用）。</summary>
	[JsonProperty("Bonus")]
	public int[] Bonus { get; set; }

	/// <summary>楼层配置分组，决定 ScDLCTowerFloor 里查哪一组行（1200 / 1206 …）。</summary>
	[JsonProperty("ConfigType")]
	public int ConfigType { get; set; }
}
