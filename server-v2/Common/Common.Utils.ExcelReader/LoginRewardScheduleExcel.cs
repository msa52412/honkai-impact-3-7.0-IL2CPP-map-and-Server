using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// 登录授勋（七日授勋）活动配置 —— 对应 Resources\ExcelOutputAsset\LoginRewardSchedule.json。
///
/// 客户端「授勋」界面拿 GetLoginActivityRsp 里的 id 反查这张表：
///   * rewardList 的 7 个元素 = 第 1~7 天的奖励（每个是 RewardData.RewardID）；
///   * keyRewardList = 需要高亮/额外展示的天数；
///   * playerLevel / endTime 决定能不能进、能不能领。
/// </summary>
public class LoginRewardScheduleExcel
{
	[JsonProperty("Type")]
	public int Type { get; set; }

	/// <summary>1 = 常规登录活动，2 = 新手七日授勋（7.0 里唯一没过期的是 581）。</summary>
	[JsonProperty("ActivityType")]
	public int ActivityType { get; set; }

	/// <summary>活动结束时间，本地时间字符串，例如 "2035-01-28 09:59:59"。</summary>
	[JsonProperty("endTime")]
	public string EndTime { get; set; }

	[JsonProperty("playerLevel")]
	public int PlayerLevel { get; set; }

	/// <summary>第 1~7 天的奖励 id（RewardData.RewardID）。</summary>
	[JsonProperty("rewardList")]
	public int[] RewardList { get; set; }

	[JsonProperty("keyRewardList")]
	public int[] KeyRewardList { get; set; }

	[JsonProperty("ID")]
	public int Id { get; set; }
}
