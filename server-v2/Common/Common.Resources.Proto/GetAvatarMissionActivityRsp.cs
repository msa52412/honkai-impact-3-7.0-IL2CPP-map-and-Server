using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetAvatarMissionActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3001
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__AvatarSampleId;

	private uint? __pbn__ChooseAvatarTime;

	private uint? __pbn__ResetChooseTimes;

	private uint? __pbn__LastRefreshTime;

	private uint? __pbn__DailyContractPoint;

	private bool? __pbn__IsDailyRewardTaken;

	private uint? __pbn__ActivityFinishDays;

	private uint? __pbn__DailyRewardNum;

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

	[ProtoMember(3, Name = "avatar_sample_id")]
	public uint AvatarSampleId
	{
		get
		{
			return __pbn__AvatarSampleId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarSampleId = value;
		}
	}

	[ProtoMember(4, Name = "choose_avatar_time")]
	public uint ChooseAvatarTime
	{
		get
		{
			return __pbn__ChooseAvatarTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ChooseAvatarTime = value;
		}
	}

	[ProtoMember(5, Name = "reset_choose_times")]
	public uint ResetChooseTimes
	{
		get
		{
			return __pbn__ResetChooseTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ResetChooseTimes = value;
		}
	}

	[ProtoMember(6, Name = "last_refresh_time")]
	public uint LastRefreshTime
	{
		get
		{
			return __pbn__LastRefreshTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastRefreshTime = value;
		}
	}

	[ProtoMember(7, Name = "daily_contract_point")]
	public uint DailyContractPoint
	{
		get
		{
			return __pbn__DailyContractPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyContractPoint = value;
		}
	}

	[ProtoMember(8, Name = "is_daily_reward_taken")]
	public bool IsDailyRewardTaken
	{
		get
		{
			return __pbn__IsDailyRewardTaken.GetValueOrDefault();
		}
		set
		{
			__pbn__IsDailyRewardTaken = value;
		}
	}

	[ProtoMember(9, Name = "activity_finish_days")]
	public uint ActivityFinishDays
	{
		get
		{
			return __pbn__ActivityFinishDays.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityFinishDays = value;
		}
	}

	[ProtoMember(10, Name = "taken_reward_days_list")]
	public uint[] TakenRewardDaysLists { get; set; }

	[ProtoMember(11, Name = "daily_reward_num")]
	public uint DailyRewardNum
	{
		get
		{
			return __pbn__DailyRewardNum.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyRewardNum = value;
		}
	}

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

	public bool ShouldSerializeAvatarSampleId()
	{
		return __pbn__AvatarSampleId.HasValue;
	}

	public void ResetAvatarSampleId()
	{
		__pbn__AvatarSampleId = null;
	}

	public bool ShouldSerializeChooseAvatarTime()
	{
		return __pbn__ChooseAvatarTime.HasValue;
	}

	public void ResetChooseAvatarTime()
	{
		__pbn__ChooseAvatarTime = null;
	}

	public bool ShouldSerializeResetChooseTimes()
	{
		return __pbn__ResetChooseTimes.HasValue;
	}

	public void ResetResetChooseTimes()
	{
		__pbn__ResetChooseTimes = null;
	}

	public bool ShouldSerializeLastRefreshTime()
	{
		return __pbn__LastRefreshTime.HasValue;
	}

	public void ResetLastRefreshTime()
	{
		__pbn__LastRefreshTime = null;
	}

	public bool ShouldSerializeDailyContractPoint()
	{
		return __pbn__DailyContractPoint.HasValue;
	}

	public void ResetDailyContractPoint()
	{
		__pbn__DailyContractPoint = null;
	}

	public bool ShouldSerializeIsDailyRewardTaken()
	{
		return __pbn__IsDailyRewardTaken.HasValue;
	}

	public void ResetIsDailyRewardTaken()
	{
		__pbn__IsDailyRewardTaken = null;
	}

	public bool ShouldSerializeActivityFinishDays()
	{
		return __pbn__ActivityFinishDays.HasValue;
	}

	public void ResetActivityFinishDays()
	{
		__pbn__ActivityFinishDays = null;
	}

	public bool ShouldSerializeDailyRewardNum()
	{
		return __pbn__DailyRewardNum.HasValue;
	}

	public void ResetDailyRewardNum()
	{
		__pbn__DailyRewardNum = null;
	}
}
