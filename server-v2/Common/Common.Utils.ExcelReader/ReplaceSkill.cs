using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class ReplaceSkill
{
	[JsonProperty("NewSkillID")]
	public int NewSkillId { get; set; }

	[JsonProperty("OldSkillID")]
	public int OldSkillId { get; set; }
}
