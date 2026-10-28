using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class AvatarArtifactDataExcel
{
	[JsonProperty("showPlayLevel")]
	public int ShowPlayLevel { get; set; }

	[JsonProperty("UnlockAvatarLevel")]
	public int UnlockAvatarLevel { get; set; }

	[JsonProperty("LevelList")]
	public int[] LevelList { get; set; }

	[JsonProperty("ReplaceSkillList")]
	public ReplaceSkill[] ReplaceSkillList { get; set; }

	[JsonProperty("ShowSkillList")]
	public ShowSkill[] ShowSkillList { get; set; }

	[JsonProperty("TagUnlockList")]
	public int[] TagUnlockList { get; set; }

	[JsonProperty("ArtifactID")]
	public int ArtifactId { get; set; }
}
