using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class AvatarLevelDataExcel
{
	[JsonProperty("exp")]
	public int Exp { get; set; }

	[JsonProperty("cost")]
	public int Cost { get; set; }

	[JsonProperty("avatarAssistConf")]
	public double AvatarAssistConf { get; set; }

	[JsonProperty("subSkillScoin")]
	public int SubSkillScoin { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	[JsonProperty("level")]
	public int Level { get; set; }
}
