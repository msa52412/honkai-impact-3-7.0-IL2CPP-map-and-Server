using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeGobackLoginRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 367
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "HAS_TAKE")]
		HasTake,
		[ProtoEnum(Name = "LOGIN_DAYS_LACK")]
		LoginDaysLack,
		[ProtoEnum(Name = "NOT_GOBACK")]
		NotGoback,
		[ProtoEnum(Name = "REWARD_TYPE_ERROR")]
		RewardTypeError,
		[ProtoEnum(Name = "SELECT_REWARD_ERROR")]
		SelectRewardError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TakeDays;

	private uint? __pbn__RewardType;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "take_days")]
	public uint TakeDays
	{
		get
		{
			return __pbn__TakeDays.GetValueOrDefault();
		}
		set
		{
			__pbn__TakeDays = value;
		}
	}

	[ProtoMember(3, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(4, Name = "reward_type")]
	public uint RewardType
	{
		get
		{
			return __pbn__RewardType.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardType = value;
		}
	}

	[ProtoMember(5, Name = "reward_id_list")]
	public uint[] RewardIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTakeDays()
	{
		return __pbn__TakeDays.HasValue;
	}

	public void ResetTakeDays()
	{
		__pbn__TakeDays = null;
	}

	public bool ShouldSerializeRewardType()
	{
		return __pbn__RewardType.HasValue;
	}

	public void ResetRewardType()
	{
		__pbn__RewardType = null;
	}
}
