using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetBbqActivityDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4251
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

	private uint? __pbn__BbqLevel;

	private uint? __pbn__BbqExp;

	private uint? __pbn__MaxRewardTakenLevel;

	private uint? __pbn__CanUseBbqCount;

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

	[ProtoMember(3, Name = "bbq_level")]
	public uint BbqLevel
	{
		get
		{
			return __pbn__BbqLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__BbqLevel = value;
		}
	}

	[ProtoMember(4, Name = "bbq_exp")]
	public uint BbqExp
	{
		get
		{
			return __pbn__BbqExp.GetValueOrDefault();
		}
		set
		{
			__pbn__BbqExp = value;
		}
	}

	[ProtoMember(5, Name = "max_reward_taken_level")]
	public uint MaxRewardTakenLevel
	{
		get
		{
			return __pbn__MaxRewardTakenLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxRewardTakenLevel = value;
		}
	}

	[ProtoMember(6, Name = "can_use_bbq_count")]
	public uint CanUseBbqCount
	{
		get
		{
			return __pbn__CanUseBbqCount.GetValueOrDefault();
		}
		set
		{
			__pbn__CanUseBbqCount = value;
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

	public bool ShouldSerializeBbqLevel()
	{
		return __pbn__BbqLevel.HasValue;
	}

	public void ResetBbqLevel()
	{
		__pbn__BbqLevel = null;
	}

	public bool ShouldSerializeBbqExp()
	{
		return __pbn__BbqExp.HasValue;
	}

	public void ResetBbqExp()
	{
		__pbn__BbqExp = null;
	}

	public bool ShouldSerializeMaxRewardTakenLevel()
	{
		return __pbn__MaxRewardTakenLevel.HasValue;
	}

	public void ResetMaxRewardTakenLevel()
	{
		__pbn__MaxRewardTakenLevel = null;
	}

	public bool ShouldSerializeCanUseBbqCount()
	{
		return __pbn__CanUseBbqCount.HasValue;
	}

	public void ResetCanUseBbqCount()
	{
		__pbn__CanUseBbqCount = null;
	}
}
