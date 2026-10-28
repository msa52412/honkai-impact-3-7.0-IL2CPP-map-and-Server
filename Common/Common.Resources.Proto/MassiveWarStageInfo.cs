using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MassiveWarStageInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__SubScheduleId;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__MaxDamageLevelScore;

	private uint? __pbn__MaxRankScore;

	private uint? __pbn__BasicDamageScore;

	private uint? __pbn__MinRankRatio;

	private uint? __pbn__MinDamageLevel;

	private uint? __pbn__MaxTotalDamage;

	[ProtoMember(1, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(2, Name = "sub_schedule_id")]
	public uint SubScheduleId
	{
		get
		{
			return __pbn__SubScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__SubScheduleId = value;
		}
	}

	[ProtoMember(3, Name = "begin_time")]
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

	[ProtoMember(4, Name = "end_time")]
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

	[ProtoMember(5, Name = "max_damage_level_score")]
	public uint MaxDamageLevelScore
	{
		get
		{
			return __pbn__MaxDamageLevelScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxDamageLevelScore = value;
		}
	}

	[ProtoMember(6, Name = "max_rank_score")]
	public uint MaxRankScore
	{
		get
		{
			return __pbn__MaxRankScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxRankScore = value;
		}
	}

	[ProtoMember(7, Name = "basic_damage_score")]
	public uint BasicDamageScore
	{
		get
		{
			return __pbn__BasicDamageScore.GetValueOrDefault();
		}
		set
		{
			__pbn__BasicDamageScore = value;
		}
	}

	[ProtoMember(8, Name = "min_rank_ratio")]
	public uint MinRankRatio
	{
		get
		{
			return __pbn__MinRankRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__MinRankRatio = value;
		}
	}

	[ProtoMember(9, Name = "min_damage_level")]
	public uint MinDamageLevel
	{
		get
		{
			return __pbn__MinDamageLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MinDamageLevel = value;
		}
	}

	[ProtoMember(10, Name = "max_total_damage")]
	public uint MaxTotalDamage
	{
		get
		{
			return __pbn__MaxTotalDamage.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxTotalDamage = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeSubScheduleId()
	{
		return __pbn__SubScheduleId.HasValue;
	}

	public void ResetSubScheduleId()
	{
		__pbn__SubScheduleId = null;
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

	public bool ShouldSerializeMaxDamageLevelScore()
	{
		return __pbn__MaxDamageLevelScore.HasValue;
	}

	public void ResetMaxDamageLevelScore()
	{
		__pbn__MaxDamageLevelScore = null;
	}

	public bool ShouldSerializeMaxRankScore()
	{
		return __pbn__MaxRankScore.HasValue;
	}

	public void ResetMaxRankScore()
	{
		__pbn__MaxRankScore = null;
	}

	public bool ShouldSerializeBasicDamageScore()
	{
		return __pbn__BasicDamageScore.HasValue;
	}

	public void ResetBasicDamageScore()
	{
		__pbn__BasicDamageScore = null;
	}

	public bool ShouldSerializeMinRankRatio()
	{
		return __pbn__MinRankRatio.HasValue;
	}

	public void ResetMinRankRatio()
	{
		__pbn__MinRankRatio = null;
	}

	public bool ShouldSerializeMinDamageLevel()
	{
		return __pbn__MinDamageLevel.HasValue;
	}

	public void ResetMinDamageLevel()
	{
		__pbn__MinDamageLevel = null;
	}

	public bool ShouldSerializeMaxTotalDamage()
	{
		return __pbn__MaxTotalDamage.HasValue;
	}

	public void ResetMaxTotalDamage()
	{
		__pbn__MaxTotalDamage = null;
	}
}
