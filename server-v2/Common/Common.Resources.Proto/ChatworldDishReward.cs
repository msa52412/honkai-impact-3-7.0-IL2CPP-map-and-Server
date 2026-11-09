using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldDishReward : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__RewardId;

	private uint? __pbn__TakenTimes;

	private uint? __pbn__LastTakeTime;

	[ProtoMember(1, Name = "reward_id")]
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

	[ProtoMember(2, Name = "taken_times")]
	public uint TakenTimes
	{
		get
		{
			return __pbn__TakenTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__TakenTimes = value;
		}
	}

	[ProtoMember(3, Name = "last_take_time")]
	public uint LastTakeTime
	{
		get
		{
			return __pbn__LastTakeTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastTakeTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRewardId()
	{
		return __pbn__RewardId.HasValue;
	}

	public void ResetRewardId()
	{
		__pbn__RewardId = null;
	}

	public bool ShouldSerializeTakenTimes()
	{
		return __pbn__TakenTimes.HasValue;
	}

	public void ResetTakenTimes()
	{
		__pbn__TakenTimes = null;
	}

	public bool ShouldSerializeLastTakeTime()
	{
		return __pbn__LastTakeTime.HasValue;
	}

	public void ResetLastTakeTime()
	{
		__pbn__LastTakeTime = null;
	}
}
