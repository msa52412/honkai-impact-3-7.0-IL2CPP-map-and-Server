using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SanctuaryStageEndNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4314
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TotalAddExp;

	private uint? __pbn__BonusAddExp;

	private uint? __pbn__OldLevel;

	private uint? __pbn__OldExp;

	[ProtoMember(1, Name = "total_add_exp")]
	public uint TotalAddExp
	{
		get
		{
			return __pbn__TotalAddExp.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalAddExp = value;
		}
	}

	[ProtoMember(2, Name = "bonus_add_exp")]
	public uint BonusAddExp
	{
		get
		{
			return __pbn__BonusAddExp.GetValueOrDefault();
		}
		set
		{
			__pbn__BonusAddExp = value;
		}
	}

	[ProtoMember(3, Name = "old_level")]
	public uint OldLevel
	{
		get
		{
			return __pbn__OldLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__OldLevel = value;
		}
	}

	[ProtoMember(4, Name = "old_exp")]
	public uint OldExp
	{
		get
		{
			return __pbn__OldExp.GetValueOrDefault();
		}
		set
		{
			__pbn__OldExp = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTotalAddExp()
	{
		return __pbn__TotalAddExp.HasValue;
	}

	public void ResetTotalAddExp()
	{
		__pbn__TotalAddExp = null;
	}

	public bool ShouldSerializeBonusAddExp()
	{
		return __pbn__BonusAddExp.HasValue;
	}

	public void ResetBonusAddExp()
	{
		__pbn__BonusAddExp = null;
	}

	public bool ShouldSerializeOldLevel()
	{
		return __pbn__OldLevel.HasValue;
	}

	public void ResetOldLevel()
	{
		__pbn__OldLevel = null;
	}

	public bool ShouldSerializeOldExp()
	{
		return __pbn__OldExp.HasValue;
	}

	public void ResetOldExp()
	{
		__pbn__OldExp = null;
	}
}
