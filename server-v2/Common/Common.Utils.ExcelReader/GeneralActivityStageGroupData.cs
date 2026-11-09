using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// GeneralActivityStageGroup.json（530 行）—— 通用活动的关卡组。
///
/// 一个活动可以有多个关卡组，`GeneralActivityStage.stage_group_schedule_list` 里的
/// `stage_group_id` 就是这里的 `StageGroupID`。
/// 小队联机（活动 50000001）只有一个组，StageGroupID = 50000001。
/// </summary>
public class GeneralActivityStageGroupData : BaseExcelReader<GeneralActivityStageGroupData, GeneralActivityStageGroupDataExcel>
{
	public override string FileName => "GeneralActivityStageGroup.json";

	public List<GeneralActivityStageGroupDataExcel> OfActivity(uint activityId)
	{
		List<GeneralActivityStageGroupDataExcel> list = new List<GeneralActivityStageGroupDataExcel>();
		if (base.All == null)
		{
			return list;
		}
		foreach (GeneralActivityStageGroupDataExcel row in base.All)
		{
			if (row.ActivityId == activityId)
			{
				list.Add(row);
			}
		}
		return list;
	}
}

public class GeneralActivityStageGroupDataExcel
{
	[JsonProperty("AcitivityID")]
	public uint ActivityId { get; set; }

	[JsonProperty("StageGroupID")]
	public uint StageGroupId { get; set; }

	[JsonProperty("StageIDList")]
	public int[] StageIdList { get; set; }

	[JsonProperty("ChallengeNum")]
	public int ChallengeNum { get; set; }
}
