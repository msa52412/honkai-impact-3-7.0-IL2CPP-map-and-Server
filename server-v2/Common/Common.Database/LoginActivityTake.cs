using System.Collections.Generic;
using System.Linq;

namespace Common.Database;

/// <summary>
/// 授勋领奖记录表。领过的天数必须落库 —— 否则重登一次
/// <see cref="Common.Resources.Proto.LoginActivityData.has_take_days_reward_list"/> 又变空，
/// 客户端会把 7 个格子重新画成「可领取」，点了服务端还会再发一次奖。
/// </summary>
public class LoginActivityTake
{
	public static readonly SqlTable<LoginActivityTakeScheme> collection =
		Global.Table<LoginActivityTakeScheme>("LoginActivityTakes",
			("owner_uid", "INTEGER", x => x.OwnerUid, false),
			("activity_id", "INTEGER", x => x.ActivityId, false),
			("take_days", "INTEGER", x => x.TakeDays, false),
			("reward_type", "INTEGER", x => x.RewardType, false));

	public static List<LoginActivityTakeScheme> OfUser(uint uid)
	{
		return collection.Where(x => x.OwnerUid == uid).ToList();
	}

	/// <summary>某个活动下已领取的天数列表（按 reward_type 分）。</summary>
	public static uint[] TakenDays(uint uid, uint activityId, uint rewardType)
	{
		return OfUser(uid)
			.Where(x => x.ActivityId == activityId && x.RewardType == rewardType)
			.Select(x => x.TakeDays)
			.Distinct()
			.OrderBy(x => x)
			.ToArray();
	}

	public static bool Has(uint uid, uint activityId, uint takeDays, uint rewardType)
	{
		return OfUser(uid).Any(x => x.ActivityId == activityId
			&& x.TakeDays == takeDays && x.RewardType == rewardType);
	}

	public static void Mark(uint uid, uint activityId, uint takeDays, uint rewardType)
	{
		if (Has(uid, activityId, takeDays, rewardType))
		{
			return;
		}

		collection.InsertOne(new LoginActivityTakeScheme
		{
			OwnerUid = uid,
			ActivityId = activityId,
			TakeDays = takeDays,
			RewardType = rewardType
		});
	}
}
