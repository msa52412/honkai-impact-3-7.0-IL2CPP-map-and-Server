using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class LevelUpPreSkill
{
	[JsonProperty("Lv")]
	public uint Lv { get; set; }

	[JsonProperty("SkillID")]
	public uint SkillId { get; set; }
}
