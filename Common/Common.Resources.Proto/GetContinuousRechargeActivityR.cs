using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetContinuousRechargeActivityReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4167
	}

	private IExtension __pbn__extensionData;

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
[ProtoContract]
public class GetContinuousRechargeActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4168
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

	private uint? __pbn__Progress;

	private uint? __pbn__TodayVipPoint;

	private uint? __pbn__LastRefreshTime;

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

	[ProtoMember(3, Name = "progress")]
	public uint Progress
	{
		get
		{
			return __pbn__Progress.GetValueOrDefault();
		}
		set
		{
			__pbn__Progress = value;
		}
	}

	[ProtoMember(4, Name = "today_vip_point")]
	public uint TodayVipPoint
	{
		get
		{
			return __pbn__TodayVipPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayVipPoint = value;
		}
	}

	[ProtoMember(5, Name = "has_take_reward_progress_list")]
	public uint[] HasTakeRewardProgressLists { get; set; }

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

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeTodayVipPoint()
	{
		return __pbn__TodayVipPoint.HasValue;
	}

	public void ResetTodayVipPoint()
	{
		__pbn__TodayVipPoint = null;
	}

	public bool ShouldSerializeLastRefreshTime()
	{
		return __pbn__LastRefreshTime.HasValue;
	}

	public void ResetLastRefreshTime()
	{
		__pbn__LastRefreshTime = null;
	}
}
