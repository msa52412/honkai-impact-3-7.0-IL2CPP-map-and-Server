using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// WeekDayActivity.json（318 行）—— 「材料远征 / 曜日」活动表。
///
/// 字段名很特别：`weekDayActivityID`（注意大小写）、`levelIDList`、`maxEnterTimes`。
/// 1001~1005 = 周一到周五的材料关，1006 = 周末，1301 = 另一组。
/// 客户端把 `maxEnterTimes` 当作「今日剩余次数」的上限，
/// `enter_times` 给 0 就会显示「次数耗尽」。
/// </summary>
public class WeekDayActivityData : BaseExcelReader<WeekDayActivityData, WeekDayActivityDataExcel>
{
	public override string FileName => "WeekDayActivity.json";

	public WeekDayActivityDataExcel FromActivityId(uint id)
	{
		if (base.All == null)
		{
			return null;
		}
		foreach (WeekDayActivityDataExcel row in base.All)
		{
			if (row.ActivityId == id)
			{
				return row;
			}
		}
		return null;
	}
}

public class WeekDayActivityDataExcel
{
	[JsonProperty("weekDayActivityID")]
	public uint ActivityId { get; set; }

	[JsonProperty("levelIDList")]
	public int[] LevelIdList { get; set; }

	[JsonProperty("maxEnterTimes")]
	public int MaxEnterTimes { get; set; }

	[JsonProperty("seriesID")]
	public int SeriesId { get; set; }

	[JsonProperty("activityType")]
	public int ActivityType { get; set; }

	[JsonProperty("minPlayerLevel")]
	public int MinPlayerLevel { get; set; }
}
