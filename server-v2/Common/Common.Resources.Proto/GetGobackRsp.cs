using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetGobackRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 365
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

	private uint? __pbn__GobackEndTime;

	private uint? __pbn__LoginDays;

	private uint? __pbn__Score;

	private uint? __pbn__ScheduleId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "goback_end_time")]
	public uint GobackEndTime
	{
		get
		{
			return __pbn__GobackEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__GobackEndTime = value;
		}
	}

	[ProtoMember(3, Name = "login_days")]
	public uint LoginDays
	{
		get
		{
			return __pbn__LoginDays.GetValueOrDefault();
		}
		set
		{
			__pbn__LoginDays = value;
		}
	}

	[ProtoMember(4, Name = "has_take_days_reward_list")]
	public uint[] HasTakeDaysRewardLists { get; set; }

	[ProtoMember(5, Name = "has_take_days_bonus_reward_list")]
	public uint[] HasTakeDaysBonusRewardLists { get; set; }

	[ProtoMember(6, Name = "goback_activity_list")]
	public List<GobackActivity> GobackActivityLists { get; } = new List<GobackActivity>();


	[ProtoMember(7, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(8, Name = "has_take_score_reward_list")]
	public uint[] HasTakeScoreRewardLists { get; set; }

	[ProtoMember(9, Name = "schedule_id")]
	public uint ScheduleId
	{
		get
		{
			return __pbn__ScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScheduleId = value;
		}
	}

	[ProtoMember(10, Name = "login_reward_list")]
	public List<GobackActivityLoginReward> LoginRewardLists { get; } = new List<GobackActivityLoginReward>();


	[ProtoMember(11, Name = "fund")]
	public GobackFund Fund { get; set; }

	[ProtoMember(12, Name = "goback_grow_up_activity")]
	public GobackGrowUpActivity GobackGrowUpActivity { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGobackEndTime()
	{
		return __pbn__GobackEndTime.HasValue;
	}

	public void ResetGobackEndTime()
	{
		__pbn__GobackEndTime = null;
	}

	public bool ShouldSerializeLoginDays()
	{
		return __pbn__LoginDays.HasValue;
	}

	public void ResetLoginDays()
	{
		__pbn__LoginDays = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
	}
}
