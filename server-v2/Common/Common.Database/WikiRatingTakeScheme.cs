namespace Common.Database;

/// <summary>
/// 一条「已领取的收藏评分奖励」。(OwnerUid, RankId) 唯一 —— 每档只能领一次。
/// </summary>
public class WikiRatingTakeScheme : IRow
{
	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	/// <summary>WikiCollectionRank.RankID，1~7。</summary>
	public uint RankId { get; set; }
}
