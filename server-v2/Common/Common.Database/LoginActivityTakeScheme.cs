namespace Common.Database;

/// <summary>
/// 一条「已领取的授勋奖励」。主键之外的四个字段一起才唯一：
/// (OwnerUid, ActivityId, TakeDays, RewardType) —— 同一天只能领一次普通奖、一次额外奖。
/// </summary>
public class LoginActivityTakeScheme : IRow
{
	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	/// <summary>LoginRewardSchedule.ID，例如 581。</summary>
	public uint ActivityId { get; set; }

	/// <summary>第几天（从 1 开始）。</summary>
	public uint TakeDays { get; set; }

	/// <summary>0 = 普通（LoginRewardSchedule），1 = 额外（LoginBonusRewardSchedule）。</summary>
	public uint RewardType { get; set; }
}
