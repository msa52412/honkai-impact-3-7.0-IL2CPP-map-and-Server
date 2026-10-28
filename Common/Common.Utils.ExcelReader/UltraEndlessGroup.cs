using System.Linq;

namespace Common.Utils.ExcelReader;

/// <summary>
/// 超弦空间杯数段位表。
/// 客户端 UltraEndlessModule.TopBattleGroupLevel / CupGroupLevel 都依赖这张表：
///   - BattleGroupLevel 必须是 UltraEndlessSchedule.BattleConfigList 里存在的 GroupLevel，
///     否则 BattleConfigID = 0 → SiteMapName 为空 → MonoUltraEndlessZoneMapCmpt 把
///     所有 zone 子节点 SetActive(false)，界面上一张地图都不显示。
///   - CupNum 必须落在某档 NeedCupNum 上，否则 TryGetGroupLevelMetaByCupNum 返回 null。
/// </summary>
public class UltraEndlessGroup : BaseExcelReader<UltraEndlessGroup, UltraEndlessGroupExcel>
{
	public override string FileName => "UltraEndlessGroup.json";

	/// <summary>表内最大 GroupLevel（当前 9）</summary>
	public int MaxGroupLevel => All.Length == 0 ? 0 : All.Max(x => x.GroupLevel);

	/// <summary>按 GroupLevel 取档位</summary>
	public UltraEndlessGroupExcel? ByLevel(int groupLevel)
	{
		return All.FirstOrDefault(x => x.GroupLevel == groupLevel);
	}

	/// <summary>把任意 groupLevel 夹到表内合法区间</summary>
	public uint ClampLevel(uint groupLevel)
	{
		int max = MaxGroupLevel;
		if (max <= 0)
		{
			return groupLevel;
		}

		if (groupLevel < 1u)
		{
			return 1u;
		}

		return groupLevel > (uint)max ? (uint)max : groupLevel;
	}

	/// <summary>取该档位的 NeedCupNum（找不到返回 0）</summary>
	public uint CupNumOf(uint groupLevel)
	{
		UltraEndlessGroupExcel? meta = ByLevel((int)groupLevel);
		return (uint)(meta?.NeedCupNum ?? 0);
	}
}
