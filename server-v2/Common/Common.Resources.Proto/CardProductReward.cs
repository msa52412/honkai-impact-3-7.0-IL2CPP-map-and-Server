using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CardProductReward : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__ProductName;

	private uint? __pbn__Hcoin;

	private uint? __pbn__TakeRewardDays;

	[ProtoMember(1, Name = "product_name")]
	[DefaultValue("")]
	public string ProductName
	{
		get
		{
			return __pbn__ProductName ?? "";
		}
		set
		{
			__pbn__ProductName = value;
		}
	}

	[ProtoMember(2, Name = "hcoin")]
	public uint Hcoin
	{
		get
		{
			return __pbn__Hcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__Hcoin = value;
		}
	}

	[ProtoMember(3, Name = "take_reward_days")]
	public uint TakeRewardDays
	{
		get
		{
			return __pbn__TakeRewardDays.GetValueOrDefault();
		}
		set
		{
			__pbn__TakeRewardDays = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeProductName()
	{
		return __pbn__ProductName != null;
	}

	public void ResetProductName()
	{
		__pbn__ProductName = null;
	}

	public bool ShouldSerializeHcoin()
	{
		return __pbn__Hcoin.HasValue;
	}

	public void ResetHcoin()
	{
		__pbn__Hcoin = null;
	}

	public bool ShouldSerializeTakeRewardDays()
	{
		return __pbn__TakeRewardDays.HasValue;
	}

	public void ResetTakeRewardDays()
	{
		__pbn__TakeRewardDays = null;
	}
}
