using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldBeastGetActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2466
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

	private uint? __pbn__DailySeniorChallengeTimes;

	private uint? __pbn__DailyJuniorChallengeTimes;

	private uint? __pbn__DailySeniorTreasureTimes;

	private uint? __pbn__DailyJuniorTreasureTimes;

	private uint? __pbn__RefreshBeginTime;

	private uint? __pbn__RefreshEndTime;

	private uint? __pbn__RefreshInterval;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "daily_senior_challenge_times")]
	public uint DailySeniorChallengeTimes
	{
		get
		{
			return __pbn__DailySeniorChallengeTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__DailySeniorChallengeTimes = value;
		}
	}

	[ProtoMember(3, Name = "daily_junior_challenge_times")]
	public uint DailyJuniorChallengeTimes
	{
		get
		{
			return __pbn__DailyJuniorChallengeTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyJuniorChallengeTimes = value;
		}
	}

	[ProtoMember(4, Name = "daily_senior_treasure_times")]
	public uint DailySeniorTreasureTimes
	{
		get
		{
			return __pbn__DailySeniorTreasureTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__DailySeniorTreasureTimes = value;
		}
	}

	[ProtoMember(5, Name = "daily_junior_treasure_times")]
	public uint DailyJuniorTreasureTimes
	{
		get
		{
			return __pbn__DailyJuniorTreasureTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyJuniorTreasureTimes = value;
		}
	}

	[ProtoMember(6, Name = "refresh_begin_time")]
	public uint RefreshBeginTime
	{
		get
		{
			return __pbn__RefreshBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshBeginTime = value;
		}
	}

	[ProtoMember(7, Name = "refresh_end_time")]
	public uint RefreshEndTime
	{
		get
		{
			return __pbn__RefreshEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshEndTime = value;
		}
	}

	[ProtoMember(8, Name = "refresh_interval")]
	public uint RefreshInterval
	{
		get
		{
			return __pbn__RefreshInterval.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshInterval = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDailySeniorChallengeTimes()
	{
		return __pbn__DailySeniorChallengeTimes.HasValue;
	}

	public void ResetDailySeniorChallengeTimes()
	{
		__pbn__DailySeniorChallengeTimes = null;
	}

	public bool ShouldSerializeDailyJuniorChallengeTimes()
	{
		return __pbn__DailyJuniorChallengeTimes.HasValue;
	}

	public void ResetDailyJuniorChallengeTimes()
	{
		__pbn__DailyJuniorChallengeTimes = null;
	}

	public bool ShouldSerializeDailySeniorTreasureTimes()
	{
		return __pbn__DailySeniorTreasureTimes.HasValue;
	}

	public void ResetDailySeniorTreasureTimes()
	{
		__pbn__DailySeniorTreasureTimes = null;
	}

	public bool ShouldSerializeDailyJuniorTreasureTimes()
	{
		return __pbn__DailyJuniorTreasureTimes.HasValue;
	}

	public void ResetDailyJuniorTreasureTimes()
	{
		__pbn__DailyJuniorTreasureTimes = null;
	}

	public bool ShouldSerializeRefreshBeginTime()
	{
		return __pbn__RefreshBeginTime.HasValue;
	}

	public void ResetRefreshBeginTime()
	{
		__pbn__RefreshBeginTime = null;
	}

	public bool ShouldSerializeRefreshEndTime()
	{
		return __pbn__RefreshEndTime.HasValue;
	}

	public void ResetRefreshEndTime()
	{
		__pbn__RefreshEndTime = null;
	}

	public bool ShouldSerializeRefreshInterval()
	{
		return __pbn__RefreshInterval.HasValue;
	}

	public void ResetRefreshInterval()
	{
		__pbn__RefreshInterval = null;
	}
}
