using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class UpLevelArtifactNeedList
{
	[JsonProperty("ArtifactLevel")]
	public int ArtifactLevel { get; set; }

	[JsonProperty("SubSkillLevel")]
	public int SubSkillLevel { get; set; }
}
