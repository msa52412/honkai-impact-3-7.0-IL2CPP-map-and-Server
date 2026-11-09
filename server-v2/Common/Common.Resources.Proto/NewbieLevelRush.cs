using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class NewbieLevelRush : IExtensible
{
	private IExtension __pbn__extensionData;

	private bool? __pbn__IsPurchased;

	private uint? __pbn__FreeRewardMaxTakeLevel;

	private uint? __pbn__PurchaseRewardMaxTakeLevel;

	[ProtoMember(1, Name = "is_purchased")]
	public bool IsPurchased
	{
		get
		{
			return __pbn__IsPurchased.GetValueOrDefault();
		}
		set
		{
			__pbn__IsPurchased = value;
		}
	}

	[ProtoMember(2, Name = "free_reward_max_take_level")]
	public uint FreeRewardMaxTakeLevel
	{
		get
		{
			return __pbn__FreeRewardMaxTakeLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__FreeRewardMaxTakeLevel = value;
		}
	}

	[ProtoMember(3, Name = "purchase_reward_max_take_level")]
	public uint PurchaseRewardMaxTakeLevel
	{
		get
		{
			return __pbn__PurchaseRewardMaxTakeLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__PurchaseRewardMaxTakeLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsPurchased()
	{
		return __pbn__IsPurchased.HasValue;
	}

	public void ResetIsPurchased()
	{
		__pbn__IsPurchased = null;
	}

	public bool ShouldSerializeFreeRewardMaxTakeLevel()
	{
		return __pbn__FreeRewardMaxTakeLevel.HasValue;
	}

	public void ResetFreeRewardMaxTakeLevel()
	{
		__pbn__FreeRewardMaxTakeLevel = null;
	}

	public bool ShouldSerializePurchaseRewardMaxTakeLevel()
	{
		return __pbn__PurchaseRewardMaxTakeLevel.HasValue;
	}

	public void ResetPurchaseRewardMaxTakeLevel()
	{
		__pbn__PurchaseRewardMaxTakeLevel = null;
	}
}
