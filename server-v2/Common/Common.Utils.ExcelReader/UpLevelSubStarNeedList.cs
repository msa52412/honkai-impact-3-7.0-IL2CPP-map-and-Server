using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class UpLevelSubStarNeedList
{
	[JsonProperty("level")]
	public int Level { get; set; }

	[JsonProperty("starNeed")]
	public int StarNeed { get; set; }

	[JsonProperty("subStarNeed")]
	public int SubStarNeed { get; set; }
}
