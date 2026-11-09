using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ThemeData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ThemeId;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__BonusDropTimes;

	private uint? __pbn__TotalBonusScore;

	[ProtoMember(1, Name = "theme_id")]
	public uint ThemeId
	{
		get
		{
			return __pbn__ThemeId.GetValueOrDefault();
		}
		set
		{
			__pbn__ThemeId = value;
		}
	}

	[ProtoMember(2, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(3, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(4, Name = "bonus_drop_times")]
	public uint BonusDropTimes
	{
		get
		{
			return __pbn__BonusDropTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__BonusDropTimes = value;
		}
	}

	[ProtoMember(5, Name = "total_bonus_score")]
	public uint TotalBonusScore
	{
		get
		{
			return __pbn__TotalBonusScore.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalBonusScore = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeThemeId()
	{
		return __pbn__ThemeId.HasValue;
	}

	public void ResetThemeId()
	{
		__pbn__ThemeId = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeBonusDropTimes()
	{
		return __pbn__BonusDropTimes.HasValue;
	}

	public void ResetBonusDropTimes()
	{
		__pbn__BonusDropTimes = null;
	}

	public bool ShouldSerializeTotalBonusScore()
	{
		return __pbn__TotalBonusScore.HasValue;
	}

	public void ResetTotalBonusScore()
	{
		__pbn__TotalBonusScore = null;
	}
}
