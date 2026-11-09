using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldGrantRewardNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2455
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityType;

	private uint? __pbn__ActivityId;

	private uint? __pbn__RewardId;

	private uint? __pbn__ExtraInt;

	[ProtoMember(1, Name = "activity_type")]
	public uint ActivityType
	{
		get
		{
			return __pbn__ActivityType.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityType = value;
		}
	}

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

	[ProtoMember(3, Name = "reward_id")]
	public uint RewardId
	{
		get
		{
			return __pbn__RewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardId = value;
		}
	}

	[ProtoMember(4, Name = "extra_int")]
	public uint ExtraInt
	{
		get
		{
			return __pbn__ExtraInt.GetValueOrDefault();
		}
		set
		{
			__pbn__ExtraInt = value;
		}
	}

	[ProtoMember(5, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityType()
	{
		return __pbn__ActivityType.HasValue;
	}

	public void ResetActivityType()
	{
		__pbn__ActivityType = null;
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeRewardId()
	{
		return __pbn__RewardId.HasValue;
	}

	public void ResetRewardId()
	{
		__pbn__RewardId = null;
	}

	public bool ShouldSerializeExtraInt()
	{
		return __pbn__ExtraInt.HasValue;
	}

	public void ResetExtraInt()
	{
		__pbn__ExtraInt = null;
	}
}
