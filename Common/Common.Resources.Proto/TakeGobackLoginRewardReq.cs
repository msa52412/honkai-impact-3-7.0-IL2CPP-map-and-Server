using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeGobackLoginRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 366
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TakeDays;

	private uint? __pbn__RewardType;

	[ProtoMember(1, Name = "take_days")]
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

	[ProtoMember(2, Name = "reward_type")]
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

	[ProtoMember(3, Name = "reward_list")]
	public uint[] RewardLists { get; set; }

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
