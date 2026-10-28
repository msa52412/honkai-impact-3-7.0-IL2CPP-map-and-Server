using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TimeLimitAddUpReward : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AddUpValueNeed;

	private uint? __pbn__RewardId;

	private bool? __pbn__IsRewardGet;

	[ProtoMember(1, Name = "add_up_value_need")]
	public uint AddUpValueNeed
	{
		get
		{
			return __pbn__AddUpValueNeed.GetValueOrDefault();
		}
		set
		{
			__pbn__AddUpValueNeed = value;
		}
	}

	[ProtoMember(2, Name = "reward_id")]
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

	[ProtoMember(3, Name = "is_reward_get")]
	public bool IsRewardGet
	{
		get
		{
			return __pbn__IsRewardGet.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRewardGet = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAddUpValueNeed()
	{
		return __pbn__AddUpValueNeed.HasValue;
	}

	public void ResetAddUpValueNeed()
	{
		__pbn__AddUpValueNeed = null;
	}

	public bool ShouldSerializeRewardId()
	{
		return __pbn__RewardId.HasValue;
	}

	public void ResetRewardId()
	{
		__pbn__RewardId = null;
	}

	public bool ShouldSerializeIsRewardGet()
	{
		return __pbn__IsRewardGet.HasValue;
	}

	public void ResetIsRewardGet()
	{
		__pbn__IsRewardGet = null;
	}
}
