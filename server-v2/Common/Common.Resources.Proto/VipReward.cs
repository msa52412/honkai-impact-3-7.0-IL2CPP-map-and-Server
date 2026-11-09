using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VipReward : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__VipLevel;

	private uint? __pbn__PayHcoin;

	private uint? __pbn__RewardBatch;

	[ProtoMember(1, Name = "vip_level")]
	public uint VipLevel
	{
		get
		{
			return __pbn__VipLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__VipLevel = value;
		}
	}

	[ProtoMember(2, Name = "pay_hcoin")]
	public uint PayHcoin
	{
		get
		{
			return __pbn__PayHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__PayHcoin = value;
		}
	}

	[ProtoMember(3, Name = "reward_id_list")]
	public uint[] RewardIdLists { get; set; }

	[ProtoMember(4, Name = "reward_batch")]
	public uint RewardBatch
	{
		get
		{
			return __pbn__RewardBatch.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardBatch = value;
		}
	}

	[ProtoMember(5, Name = "special_reward_id_list")]
	public uint[] SpecialRewardIdLists { get; set; }

	[ProtoMember(6, Name = "is_special_shine_list")]
	public uint[] IsSpecialShineLists { get; set; }

	[ProtoMember(7, Name = "taken_reward_id_list")]
	public uint[] TakenRewardIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeVipLevel()
	{
		return __pbn__VipLevel.HasValue;
	}

	public void ResetVipLevel()
	{
		__pbn__VipLevel = null;
	}

	public bool ShouldSerializePayHcoin()
	{
		return __pbn__PayHcoin.HasValue;
	}

	public void ResetPayHcoin()
	{
		__pbn__PayHcoin = null;
	}

	public bool ShouldSerializeRewardBatch()
	{
		return __pbn__RewardBatch.HasValue;
	}

	public void ResetRewardBatch()
	{
		__pbn__RewardBatch = null;
	}
}
