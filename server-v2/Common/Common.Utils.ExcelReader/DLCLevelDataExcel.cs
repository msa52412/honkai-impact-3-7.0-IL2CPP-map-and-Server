using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>DLCLevelData.json 里用得到的字段（后崩坏书调查等级）。</summary>
public class DLCLevelDataExcel
{
	[JsonProperty("level")]
	public int Level { get; set; }

	/// <summary>从本等级升到下一级需要的经验。</summary>
	[JsonProperty("exp")]
	public int Exp { get; set; }

	[JsonProperty("reward")]
	public int Reward { get; set; }
}
