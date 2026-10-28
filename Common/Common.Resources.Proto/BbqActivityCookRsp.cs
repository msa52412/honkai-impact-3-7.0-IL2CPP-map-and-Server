using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BbqActivityCookRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4253
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "ITEMS_LACK")]
		ItemsLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__BbqLevel;

	private uint? __pbn__BbqExp;

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

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(4, Name = "bbq_level")]
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

	[ProtoMember(5, Name = "bbq_exp")]
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

	public bool ShouldSerializeCanUseBbqCount()
	{
		return __pbn__CanUseBbqCount.HasValue;
	}

	public void ResetCanUseBbqCount()
	{
		__pbn__CanUseBbqCount = null;
	}
}
