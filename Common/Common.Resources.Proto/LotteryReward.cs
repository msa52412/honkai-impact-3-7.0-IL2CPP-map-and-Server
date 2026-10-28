using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LotteryReward : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__LotteryId;

	private uint? __pbn__LotteryNumber;

	private uint? __pbn__RewardType;

	[ProtoMember(1, Name = "lottery_id")]
	public uint LotteryId
	{
		get
		{
			return __pbn__LotteryId.GetValueOrDefault();
		}
		set
		{
			__pbn__LotteryId = value;
		}
	}

	[ProtoMember(2, Name = "lottery_number")]
	public uint LotteryNumber
	{
		get
		{
			return __pbn__LotteryNumber.GetValueOrDefault();
		}
		set
		{
			__pbn__LotteryNumber = value;
		}
	}

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLotteryId()
	{
		return __pbn__LotteryId.HasValue;
	}

	public void ResetLotteryId()
	{
		__pbn__LotteryId = null;
	}

	public bool ShouldSerializeLotteryNumber()
	{
		return __pbn__LotteryNumber.HasValue;
	}

	public void ResetLotteryNumber()
	{
		__pbn__LotteryNumber = null;
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
