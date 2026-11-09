using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// ScDLCTalentData.json（87 行）—— 后崩坏书 2 每个角色的天赋树节点。
///
/// TalentType 对应客户端 <c>DLC2TalentType</c>：
///   1 = Passive（属性）/ 2 = Main（普攻）/ 3 = Branch（分支）/ 4 = Combine（连携）/ 5 = Support（支援）
///
/// 每个真实角色 16 个节点：3 条 Passive 链、2 个 Branch、2 个 Main、1 个独立 Passive、1 个 Combine。
/// 9061（Lyle）是纯支援，7 个全是 Support。
/// </summary>
public class ScDLCTalentData : BaseExcelReader<ScDLCTalentData, ScDLCTalentDataExcel>
{
	public override string FileName => "ScDLCTalentData.json";

	public List<ScDLCTalentDataExcel> OfAvatar(long avatarId)
	{
		return base.All.Where((ScDLCTalentDataExcel x) => x.AvatarId == avatarId).ToList();
	}
}

public class ScDLCTalentDataExcel
{
	[JsonProperty("TalentID")]
	public long TalentId { get; set; }

	[JsonProperty("AvatarID")]
	public long AvatarId { get; set; }

	/// <summary>1 Passive / 2 Main / 3 Branch / 4 Combine / 5 Support。</summary>
	[JsonProperty("TalentType")]
	public int TalentType { get; set; }

	/// <summary>前置天赋 id，0 = 根节点。</summary>
	[JsonProperty("ParentTalent")]
	public long ParentTalent { get; set; }

	[JsonProperty("IsSupport")]
	public bool IsSupport { get; set; }

	[JsonProperty("ForceLock")]
	public bool ForceLock { get; set; }
}
