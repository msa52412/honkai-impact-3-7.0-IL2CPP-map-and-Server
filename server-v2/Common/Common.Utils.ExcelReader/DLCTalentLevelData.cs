using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// DLCTalentLevelData.json（276 行）—— 每个天赋每一级的数值与解锁条件。
///
/// 客户端 OHHANBBFFIA 构造（0x181EA5E80）里：
///   this.KINNKFHBBHL（该天赋的等级上限）= DLCTalentLevelMetaDataReaderExtend::GetTalentMaxLevel(talentID)
///   level = protoTalent != null ? protoTalent.level : 1
/// 也就是说最大值来自这张表的 max(TalentLevel)，当前值完全看服务端下发的 proto。
///
/// 上限分布：类型 1 → 1/2/3/4 级，类型 2 → 1 级，类型 3 → 3 级，类型 4 → 8 级。
/// LevelUpDLCLv 是升到该级所需的调查等级（满级 30）。
/// </summary>
public class DLCTalentLevelData : BaseExcelReader<DLCTalentLevelData, DLCTalentLevelDataExcel>
{
	public override string FileName => "DLCTalentLevelData.json";

	/// <summary>某天赋的最高等级；表里没有则返回 0。</summary>
	public int MaxLevelOf(int talentId)
	{
		int max = 0;
		DLCTalentLevelDataExcel[] all = All;
		if (all == null)
		{
			return 0;
		}
		foreach (DLCTalentLevelDataExcel row in all)
		{
			if (row.DLCTalentID == talentId && row.TalentLevel > max)
			{
				max = row.TalentLevel;
			}
		}
		return max;
	}

	/// <summary>
	/// 调查等级 dlcLevel 下该天赋能到的最高等级（受 LevelUpDLCLv 限制）。
	/// 表里某级 LevelUpDLCLv 缺省 0 时视为不限制。
	/// </summary>
	public int MaxLevelOf(int talentId, uint dlcLevel)
	{
		int max = 0;
		DLCTalentLevelDataExcel[] all = All;
		if (all == null)
		{
			return 0;
		}
		foreach (DLCTalentLevelDataExcel row in all)
		{
			if (row.DLCTalentID != talentId)
			{
				continue;
			}
			if (row.LevelUpDLCLv > 0 && (uint)row.LevelUpDLCLv > dlcLevel)
			{
				continue;
			}
			if (row.TalentLevel > max)
			{
				max = row.TalentLevel;
			}
		}
		return max;
	}
}

public class DLCTalentLevelDataExcel
{
	[JsonProperty("DLCTalentID")]
	public int DLCTalentID { get; set; }

	[JsonProperty("TalentLevel")]
	public int TalentLevel { get; set; }

	/// <summary>升到本级所需的调查等级（1~30）。</summary>
	[JsonProperty("LevelUpDLCLv")]
	public int LevelUpDLCLv { get; set; }

	[JsonProperty("HPAdd")]
	public int HPAdd { get; set; }

	[JsonProperty("SPAdd")]
	public int SPAdd { get; set; }

	[JsonProperty("ATKAdd")]
	public int ATKAdd { get; set; }

	[JsonProperty("LevelUpPreTalent")]
	public List<DLCTalentPre> LevelUpPreTalent { get; set; }
}

public class DLCTalentPre
{
	[JsonProperty("ID")]
	public int ID { get; set; }

	[JsonProperty("Level")]
	public int Level { get; set; }
}
