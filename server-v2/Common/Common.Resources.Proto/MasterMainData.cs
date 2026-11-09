using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MasterMainData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Exp;

	private uint? __pbn__Level;

	private uint? __pbn__LastDismissTime;

	private uint? __pbn__Score;

	private uint? __pbn__PeriodMasterMedalNum;

	[ProtoMember(1, Name = "exp")]
	public uint Exp
	{
		get
		{
			return __pbn__Exp.GetValueOrDefault();
		}
		set
		{
			__pbn__Exp = value;
		}
	}

	[ProtoMember(2, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(3, Name = "reward_level")]
	public uint[] RewardLevels { get; set; }

	[ProtoMember(4, Name = "last_dismiss_time")]
	public uint LastDismissTime
	{
		get
		{
			return __pbn__LastDismissTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastDismissTime = value;
		}
	}

	[ProtoMember(5, Name = "score")]
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

	[ProtoMember(6, Name = "period_master_medal_num")]
	public uint PeriodMasterMedalNum
	{
		get
		{
			return __pbn__PeriodMasterMedalNum.GetValueOrDefault();
		}
		set
		{
			__pbn__PeriodMasterMedalNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeExp()
	{
		return __pbn__Exp.HasValue;
	}

	public void ResetExp()
	{
		__pbn__Exp = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeLastDismissTime()
	{
		return __pbn__LastDismissTime.HasValue;
	}

	public void ResetLastDismissTime()
	{
		__pbn__LastDismissTime = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializePeriodMasterMedalNum()
	{
		return __pbn__PeriodMasterMedalNum.HasValue;
	}

	public void ResetPeriodMasterMedalNum()
	{
		__pbn__PeriodMasterMedalNum = null;
	}
}
