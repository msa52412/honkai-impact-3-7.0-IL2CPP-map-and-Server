using System.Linq;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// 收藏评分档位表 —— Resources\ExcelOutputAsset\WikiCollectionRank.json。
///
/// 收藏界面右上角的「收藏评分」按累计收藏分匹配档位：
///   1 档 1 分、2 档 1500 分、3 档 3500 分 …… 7 档 16000 分，
/// 每档一个奖励（Reward = RewardData.RewardID）。
/// 领奖请求 TakeWikiRatingRewardReq 只带 rating_id 和客户端自己算出的 rating_score，
/// 所以服务端要拿这张表反查奖励，并用 RankScore 校验分数够不够。
/// </summary>
public class WikiCollectionRankExcel
{
	/// <summary>档位 id（1~7），对应 TakeWikiRatingRewardReq.rating_id。</summary>
	[JsonProperty("RankID")]
	public int RankId { get; set; }

	[JsonProperty("RankLevel")]
	public int RankLevel { get; set; }

	/// <summary>达到该档位所需的收藏分。</summary>
	[JsonProperty("RankScore")]
	public int RankScore { get; set; }

	/// <summary>奖励 id（RewardData.RewardID）。</summary>
	[JsonProperty("Reward")]
	public int Reward { get; set; }
}

public class WikiCollectionRank : BaseExcelReader<WikiCollectionRank, WikiCollectionRankExcel>
{
	public override string FileName => "WikiCollectionRank.json";

	public WikiCollectionRankExcel? FromId(uint rankId)
	{
		return base.All.FirstOrDefault(x => x.RankId == (int)rankId);
	}

	/// <summary>按收藏分从高到低找第一个「够分」的档位，只用于打日志。</summary>
	public WikiCollectionRankExcel? MatchByScore(uint score)
	{
		return base.All.Where(x => x.RankScore <= (int)score)
			.OrderByDescending(x => x.RankScore)
			.FirstOrDefault();
	}
}
