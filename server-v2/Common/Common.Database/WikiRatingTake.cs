using System.Collections.Generic;
using System.Linq;

namespace Common.Database;

/// <summary>
/// 收藏评分（WikiCollectionRank）领奖记录。领过的档位必须落库，
/// 否则重登后 <c>has_take_rating_reward_list</c> 又变空，客户端把档位重新画成可领，
/// 点了服务端还会再发一次奖。
/// </summary>
public class WikiRatingTake
{
	public static readonly SqlTable<WikiRatingTakeScheme> collection =
		Global.Table<WikiRatingTakeScheme>("WikiRatingTakes",
			("owner_uid", "INTEGER", x => x.OwnerUid, false),
			("rank_id", "INTEGER", x => x.RankId, false));

	public static uint[] TakenRanks(uint uid)
	{
		return collection.Where(x => x.OwnerUid == uid)
			.Select(x => x.RankId)
			.Distinct()
			.OrderBy(x => x)
			.ToArray();
	}

	public static bool Has(uint uid, uint rankId)
	{
		return collection.Where(x => x.OwnerUid == uid && x.RankId == rankId).ToList().Count > 0;
	}

	public static void Mark(uint uid, uint rankId)
	{
		if (Has(uid, rankId))
		{
			return;
		}

		collection.InsertOne(new WikiRatingTakeScheme
		{
			OwnerUid = uid,
			RankId = rankId
		});
	}
}
