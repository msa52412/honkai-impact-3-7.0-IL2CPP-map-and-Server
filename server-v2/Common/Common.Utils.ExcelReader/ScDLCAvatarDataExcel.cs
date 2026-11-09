using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// ScDLCAvatar.json（后崩坏书 2 的角色表，6 行）：
///   9011 Adam / 9021 Mei / 9031 Carol / 9041 Bronya / 9051 Timido / 9061 Lyle(IsFake)
/// </summary>
public class ScDLCAvatarDataExcel
{
	[JsonProperty("AvatarID")]
	public long AvatarId { get; set; }

	[JsonProperty("IsFake")]
	public bool IsFake { get; set; }

	[JsonProperty("NPCID")]
	public long NpcId { get; set; }

	[JsonProperty("TalentPoint")]
	public long TalentPoint { get; set; }
}
