using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// DLCTalentData.json（99 行 = 3 个角色 × 33 个天赋）—— 后崩坏书角色天赋/技能配置。
///
/// 客户端打开天赋界面时，列表**完全来自** GetDLCAvatarRsp.avatar_list[].talent_list
/// （反汇编 0x1806276A0 GetTalentListByType 遍历的就是 proto 的 talent_list），
/// 表只用来补图标、名字和等级上限，所以服务端不下发的天赋在界面上根本不存在。
///
/// TalentType（客户端 DLCTalentType 同序）：
///   1 = 属性/被动天赋（21 个，上限 1~4 级，DLCTalentLevelData 决定）
///   2 = 必杀技（2 个，上限 1 级）
///   3 = 分支技能 A/B（6 个，上限 3 级）
///   4 = 武器/特技（4 个，上限 8 级）
/// </summary>
public class DLCTalentData : BaseExcelReader<DLCTalentData, DLCTalentDataExcel>
{
	public override string FileName => "DLCTalentData.json";

	/// <summary>取某个角色的全部天赋（按 DLCTalentID 升序，保证 UI 顺序稳定）。</summary>
	public List<DLCTalentDataExcel> ListByAvatar(int avatarId)
	{
		List<DLCTalentDataExcel> list = new List<DLCTalentDataExcel>();
		DLCTalentDataExcel[] all = All;
		if (all == null)
		{
			return list;
		}
		foreach (DLCTalentDataExcel row in all)
		{
			if (row.AvatarID == avatarId && row.DLCTalentID != 0)
			{
				list.Add(row);
			}
		}
		list.Sort((DLCTalentDataExcel a, DLCTalentDataExcel b) => a.DLCTalentID.CompareTo(b.DLCTalentID));
		return list;
	}
}

public class DLCTalentDataExcel
{
	[JsonProperty("AvatarID")]
	public int AvatarID { get; set; }

	/// <summary>1 属性 / 2 必杀 / 3 分支技能 / 4 武器特技。</summary>
	[JsonProperty("TalentType")]
	public int TalentType { get; set; }

	[JsonProperty("UniqueTag")]
	public int UniqueTag { get; set; }

	[JsonProperty("TalentName")]
	public HashRef TalentName { get; set; }

	[JsonProperty("TalentShortName")]
	public HashRef TalentShortName { get; set; }

	[JsonProperty("IconPath")]
	public string IconPath { get; set; }

	[JsonProperty("CGID")]
	public int CGID { get; set; }

	/// <summary>true = 该天赋需要在若干天赋之间二选一（切换），见 IsNeedSwitch。</summary>
	[JsonProperty("IsNeedSwitch")]
	public bool IsNeedSwitch { get; set; }

	[JsonProperty("IsMultiDisplay")]
	public bool IsMultiDisplay { get; set; }

	[JsonProperty("DisplayPriority")]
	public int DisplayPriority { get; set; }

	[JsonProperty("DLCTalentID")]
	public int DLCTalentID { get; set; }
}

/// <summary>表里的 TextMap 引用字段统一是 {"hash": n}。</summary>
public class HashRef
{
	[JsonProperty("hash")]
	public long Hash { get; set; }
}
