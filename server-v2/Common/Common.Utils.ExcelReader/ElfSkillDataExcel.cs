using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class ElfSkillDataExcel
{
	[JsonProperty("ElfID")]
	public long ElfId { get; set; }

	[JsonProperty("MaxLv")]
	public long MaxLv { get; set; }

	[JsonProperty("UnlockStar")]
	public long UnlockStar { get; set; }

	[JsonProperty("hasNoRestrictionAbility")]
	public long HasNoRestrictionAbility { get; set; }

	[JsonProperty("ElfSkillID")]
	public long ElfSkillId { get; set; }
}
