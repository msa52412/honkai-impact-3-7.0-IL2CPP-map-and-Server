using System.Collections.Generic;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer;

/// <summary>
/// 通用活动的「下发范围」计算。
///
/// `GeneralActivityGetScheduleRsp` 和 `GeneralActivityGetMainInfoRsp` 里的
/// `schedule_id` 必须一一对应，所以两边都走这里，保证同一个活动拿到同一个 id。
/// </summary>
internal static class GeneralActivityService
{
	/// <summary>
	/// 要下发排期的活动 id 列表（顺序固定，下标 +1 = schedule_id）。
	///
	/// `playerLevel` &gt; 0 且 `generalactivity.json` 的 `filter_by_level` 为 true 时，
	/// 只保留「等级区间包含该玩家」的活动：
	/// GeneralActivity.json 里 135 个活动的 MaxLevel 只有 55/69/70（旧版本内容），
	/// 满级号拿到它们的排期后，活动页会多出一堆「未开启 / 已结束」的空卡。
	/// </summary>
	public static List<uint> TargetActivityIds(uint playerLevel = 0u)
	{
		List<uint> result = new List<uint>();
		GeneralActivityExcel[] all = GeneralActivity.GetInstance().All;

		if (!GeneralActivityTuning.OpenAll)
		{
			foreach (uint id in GeneralActivityTuning.ActivityIds)
			{
				if (id != 0u && !result.Contains(id))
				{
					result.Add(id);
				}
			}
			return result;
		}

		if (all == null)
		{
			return result;
		}

		bool filter = GeneralActivityTuning.FilterByLevel && playerLevel > 0u;
		foreach (GeneralActivityExcel row in all)
		{
			uint id = (uint)row.AcitivityId;
			if (id == 0u || result.Contains(id))
			{
				continue;
			}
			if (filter && !LevelFits(row, playerLevel))
			{
				continue;
			}
			result.Add(id);
		}
		return result;
	}

	/// <summary>等级区间是否包含该玩家（MaxLevel = 0 视为不封顶）。</summary>
	private static bool LevelFits(GeneralActivityExcel row, uint playerLevel)
	{
		if (row.MinLevel > 0 && playerLevel < (uint)row.MinLevel)
		{
			return false;
		}
		if (row.MaxLevel > 0 && playerLevel > (uint)row.MaxLevel)
		{
			return false;
		}
		return true;
	}

	/// <summary>按经验算玩家等级（和 Player.GetDetailData 同一个口径）。</summary>
	public static uint LevelOfExp(int exp)
	{
		return (uint)BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance()
			.CalculateLevel(exp).Level;
	}

	/// <summary>该活动在排期列表里的 schedule_id；不在列表里回 0。</summary>
	public static uint ScheduleIdOf(uint activityId, uint playerLevel = 0u)
	{
		int idx = TargetActivityIds(playerLevel).IndexOf(activityId);
		return idx < 0 ? 0u : (uint)(idx + 1);
	}
}
