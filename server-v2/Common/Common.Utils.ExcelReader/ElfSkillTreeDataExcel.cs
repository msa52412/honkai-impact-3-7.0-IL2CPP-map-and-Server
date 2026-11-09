using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class ElfSkillTreeDataExcel
{
	[JsonProperty("LevelUpPreSkill")]
	public LevelUpPreSkill[] LevelUpPreSkill { get; set; }

	[JsonProperty("LevelUpStar")]
	public uint LevelUpStar { get; set; }

	[JsonProperty("NeedElfLevel")]
	public uint NeedElfLevel { get; set; }

	[JsonProperty("LevelUpMaterialList")]
	public LevelUpMaterialList[] LevelUpMaterialList { get; set; }

	[JsonProperty("ElfSkillID")]
	public uint ElfSkillId { get; set; }

	[JsonProperty("elfSkillLv")]
	public uint ElfSkillLv { get; set; }
}
