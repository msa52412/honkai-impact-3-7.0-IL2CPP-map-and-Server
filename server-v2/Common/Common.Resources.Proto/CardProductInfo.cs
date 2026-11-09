using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CardProductInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__ProductName;

	private uint? __pbn__ExpireTime;

	private uint? __pbn__RewardNum;

	private uint? __pbn__Hcoin;

	private uint? __pbn__TakeRewardDays;

	private uint? __pbn__LastDailyRewardTime;

	private uint? __pbn__BonusNeedDays;

	private uint? __pbn__BonusHcoin;

	private uint? __pbn__BonusMaxSaveDays;

	private uint? __pbn__CardLeftDays;

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

	[ProtoMember(2, Name = "expire_time")]
	public uint ExpireTime
	{
		get
		{
			return __pbn__ExpireTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpireTime = value;
		}
	}

	[ProtoMember(3, Name = "reward_num")]
	public uint RewardNum
	{
		get
		{
			return __pbn__RewardNum.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardNum = value;
		}
	}

	[ProtoMember(4, Name = "hcoin")]
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

	[ProtoMember(5, Name = "take_reward_days")]
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

	[ProtoMember(6, Name = "last_daily_reward_time")]
	public uint LastDailyRewardTime
	{
		get
		{
			return __pbn__LastDailyRewardTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastDailyRewardTime = value;
		}
	}

	[ProtoMember(7, Name = "bonus_need_days")]
	public uint BonusNeedDays
	{
		get
		{
			return __pbn__BonusNeedDays.GetValueOrDefault();
		}
		set
		{
			__pbn__BonusNeedDays = value;
		}
	}

	[ProtoMember(8, Name = "bonus_hcoin")]
	public uint BonusHcoin
	{
		get
		{
			return __pbn__BonusHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__BonusHcoin = value;
		}
	}

	[ProtoMember(9, Name = "bonus_max_save_days")]
	public uint BonusMaxSaveDays
	{
		get
		{
			return __pbn__BonusMaxSaveDays.GetValueOrDefault();
		}
		set
		{
			__pbn__BonusMaxSaveDays = value;
		}
	}

	[ProtoMember(10, Name = "card_left_days")]
	public uint CardLeftDays
	{
		get
		{
			return __pbn__CardLeftDays.GetValueOrDefault();
		}
		set
		{
			__pbn__CardLeftDays = value;
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

	public bool ShouldSerializeExpireTime()
	{
		return __pbn__ExpireTime.HasValue;
	}

	public void ResetExpireTime()
	{
		__pbn__ExpireTime = null;
	}

	public bool ShouldSerializeRewardNum()
	{
		return __pbn__RewardNum.HasValue;
	}

	public void ResetRewardNum()
	{
		__pbn__RewardNum = null;
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

	public bool ShouldSerializeLastDailyRewardTime()
	{
		return __pbn__LastDailyRewardTime.HasValue;
	}

	public void ResetLastDailyRewardTime()
	{
		__pbn__LastDailyRewardTime = null;
	}

	public bool ShouldSerializeBonusNeedDays()
	{
		return __pbn__BonusNeedDays.HasValue;
	}

	public void ResetBonusNeedDays()
	{
		__pbn__BonusNeedDays = null;
	}

	public bool ShouldSerializeBonusHcoin()
	{
		return __pbn__BonusHcoin.HasValue;
	}

	public void ResetBonusHcoin()
	{
		__pbn__BonusHcoin = null;
	}

	public bool ShouldSerializeBonusMaxSaveDays()
	{
		return __pbn__BonusMaxSaveDays.HasValue;
	}

	public void ResetBonusMaxSaveDays()
	{
		__pbn__BonusMaxSaveDays = null;
	}

	public bool ShouldSerializeCardLeftDays()
	{
		return __pbn__CardLeftDays.HasValue;
	}

	public void ResetCardLeftDays()
	{
		__pbn__CardLeftDays = null;
	}
}
