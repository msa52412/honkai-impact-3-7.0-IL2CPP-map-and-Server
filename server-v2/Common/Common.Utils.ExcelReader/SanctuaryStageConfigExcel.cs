using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// 家园小队远征关卡表（SanctuaryStageConfig.json，45 行，StageID 161001~161045）。
/// 只取服务端用得到的字段：关卡 id 与经验奖励区间；
/// 那几个 SpriteOutput/... 是客户端图标资源路径，服务端不需要。
/// </summary>
public class SanctuaryStageConfigExcel
{
	[JsonProperty("StageID")]
	public int StageID { get; set; }

	[JsonProperty("RewardExpMin")]
	public int RewardExpMin { get; set; }

	[JsonProperty("RewardExpMax")]
	public int RewardExpMax { get; set; }

	[JsonProperty("StageRarityIcon")]
	public string StageRarityIcon { get; set; }

	[JsonProperty("StageTypeIcon")]
	public string StageTypeIcon { get; set; }

	[JsonProperty("StageBGIcon")]
	public string StageBGIcon { get; set; }
}
