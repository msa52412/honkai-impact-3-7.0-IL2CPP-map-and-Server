using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MonsterCardGetActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5921
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__HasTakeDailyRewardTimes;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "card_list")]
	public List<MonsterCard> CardLists { get; } = new List<MonsterCard>();


	[ProtoMember(4, Name = "has_take_daily_reward_times")]
	public uint HasTakeDailyRewardTimes
	{
		get
		{
			return __pbn__HasTakeDailyRewardTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeDailyRewardTimes = value;
		}
	}

	[ProtoMember(5, Name = "random_talent_confirm_data")]
	public MonsterCardRandomTalentConfirmData RandomTalentConfirmData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeHasTakeDailyRewardTimes()
	{
		return __pbn__HasTakeDailyRewardTimes.HasValue;
	}

	public void ResetHasTakeDailyRewardTimes()
	{
		__pbn__HasTakeDailyRewardTimes = null;
	}
}
