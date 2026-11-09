using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// ScDLCTalentLevel.json（241 行）—— 天赋每一级的数值。
///
/// 客户端用 <c>FeverLevelLimit</c> 做门槛：狂热等级不够时这条天赋等级不生效，
/// 所以 GetScDLC 里必须把 fever.level 一起抬上去，否则界面显示「天赋没点」。
/// 实测各天赋最大等级只有 1 / 3 / 5 三档。
/// </summary>
public class ScDLCTalentLevelData : BaseExcelReader<ScDLCTalentLevelData, ScDLCTalentLevelDataExcel>
{
	public override string FileName => "ScDLCTalentLevel.json";

	/// <summary>该天赋在表里的最高等级。</summary>
	public int MaxLevel(long talentId)
	{
		int max = 0;
		foreach (ScDLCTalentLevelDataExcel row in base.All)
		{
			if (row.TalentId == talentId && row.TalentLevel > max)
			{
				max = row.TalentLevel;
			}
		}
		return max;
	}

	/// <summary>某条天赋等级对应的门槛狂热等级；查不到回 0。</summary>
	public int FeverLimit(long talentId, int level)
	{
		foreach (ScDLCTalentLevelDataExcel row in base.All)
		{
			if (row.TalentId == talentId && row.TalentLevel == level)
			{
				return row.FeverLevelLimit;
			}
		}
		return 0;
	}

	/// <summary>表里出现过的最高门槛狂热等级（用来给 tuning 的默认值兜底）。</summary>
	public int MaxFeverLimit()
	{
		int max = 0;
		foreach (ScDLCTalentLevelDataExcel row in base.All)
		{
			if (row.FeverLevelLimit > max)
			{
				max = row.FeverLevelLimit;
			}
		}
		return max;
	}

	public List<ScDLCTalentLevelDataExcel> OfTalent(long talentId)
	{
		List<ScDLCTalentLevelDataExcel> list = new List<ScDLCTalentLevelDataExcel>();
		foreach (ScDLCTalentLevelDataExcel row in base.All)
		{
			if (row.TalentId == talentId)
			{
				list.Add(row);
			}
		}
		return list;
	}
}

public class ScDLCTalentLevelDataExcel
{
	[JsonProperty("TalentID")]
	public long TalentId { get; set; }

	[JsonProperty("TalentLevel")]
	public int TalentLevel { get; set; }

	/// <summary>解锁这一级需要的狂热等级。</summary>
	[JsonProperty("FeverLevelLimit")]
	public int FeverLevelLimit { get; set; }
}
