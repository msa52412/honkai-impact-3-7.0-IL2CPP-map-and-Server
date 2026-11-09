using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>OpenWorldQuestLevelData.json 里用得到的字段。</summary>
public class OpenWorldQuestLevelDataExcel
{
	[JsonProperty("questLevel")]
	public int QuestLevel { get; set; }

	/// <summary>从本等级升到下一级需要多少星（最高级那一行用不到）。</summary>
	[JsonProperty("levelupNeedStar")]
	public int LevelupNeedStar { get; set; }

	[JsonProperty("OWQuestLevelEffectMask")]
	public int EffectMask { get; set; }
}
