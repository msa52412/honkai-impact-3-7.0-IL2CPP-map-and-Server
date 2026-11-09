using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetTimeLimitAddUpDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6736
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

	private TimeLimitAddUpType? __pbn__Type;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__AddUpValue;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "type")]
	[DefaultValue(TimeLimitAddUpType.AddUpVipPointAll)]
	public TimeLimitAddUpType Type
	{
		get
		{
			return __pbn__Type ?? TimeLimitAddUpType.AddUpVipPointAll;
		}
		set
		{
			__pbn__Type = value;
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

	[ProtoMember(5, Name = "add_up_value")]
	public uint AddUpValue
	{
		get
		{
			return __pbn__AddUpValue.GetValueOrDefault();
		}
		set
		{
			__pbn__AddUpValue = value;
		}
	}

	[ProtoMember(6, Name = "reward_list")]
	public List<TimeLimitAddUpReward> RewardLists { get; } = new List<TimeLimitAddUpReward>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
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

	public bool ShouldSerializeAddUpValue()
	{
		return __pbn__AddUpValue.HasValue;
	}

	public void ResetAddUpValue()
	{
		__pbn__AddUpValue = null;
	}
}
