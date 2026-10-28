using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DropItem : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ItemId;

	private uint? __pbn__Level;

	private uint? __pbn__Num;

	private uint? __pbn__Rarity;

	private bool? __pbn__IsBaodi;

	private bool? __pbn__IsRareDrop;

	private uint? __pbn__GiftItemId;

	private uint? __pbn__GiftLevel;

	private uint? __pbn__GiftNum;

	private bool? __pbn__IsTreasure;

	private bool? __pbn__IsEquipDrop;

	private bool? __pbn__IsStageClearDrop;

	private bool? __pbn__IsAvatarDrop;

	private bool? __pbn__IsThemeWantedDoubleDrop;

	[ProtoMember(1, Name = "item_id")]
	public uint ItemId
	{
		get
		{
			return __pbn__ItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemId = value;
		}
	}

	[ProtoMember(2, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(3, Name = "num")]
	public uint Num
	{
		get
		{
			return __pbn__Num.GetValueOrDefault();
		}
		set
		{
			__pbn__Num = value;
		}
	}

	[ProtoMember(4, Name = "rarity")]
	public uint Rarity
	{
		get
		{
			return __pbn__Rarity.GetValueOrDefault();
		}
		set
		{
			__pbn__Rarity = value;
		}
	}

	[ProtoMember(5, Name = "is_baodi")]
	public bool IsBaodi
	{
		get
		{
			return __pbn__IsBaodi.GetValueOrDefault();
		}
		set
		{
			__pbn__IsBaodi = value;
		}
	}

	[ProtoMember(6, Name = "is_rare_drop")]
	public bool IsRareDrop
	{
		get
		{
			return __pbn__IsRareDrop.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRareDrop = value;
		}
	}

	[ProtoMember(7, Name = "gift_item_id")]
	public uint GiftItemId
	{
		get
		{
			return __pbn__GiftItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__GiftItemId = value;
		}
	}

	[ProtoMember(8, Name = "gift_level")]
	public uint GiftLevel
	{
		get
		{
			return __pbn__GiftLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__GiftLevel = value;
		}
	}

	[ProtoMember(9, Name = "gift_num")]
	public uint GiftNum
	{
		get
		{
			return __pbn__GiftNum.GetValueOrDefault();
		}
		set
		{
			__pbn__GiftNum = value;
		}
	}

	[ProtoMember(10, Name = "is_treasure")]
	public bool IsTreasure
	{
		get
		{
			return __pbn__IsTreasure.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTreasure = value;
		}
	}

	[ProtoMember(11, Name = "is_equip_drop")]
	public bool IsEquipDrop
	{
		get
		{
			return __pbn__IsEquipDrop.GetValueOrDefault();
		}
		set
		{
			__pbn__IsEquipDrop = value;
		}
	}

	[ProtoMember(12, Name = "is_stage_clear_drop")]
	public bool IsStageClearDrop
	{
		get
		{
			return __pbn__IsStageClearDrop.GetValueOrDefault();
		}
		set
		{
			__pbn__IsStageClearDrop = value;
		}
	}

	[ProtoMember(13, Name = "is_avatar_drop")]
	public bool IsAvatarDrop
	{
		get
		{
			return __pbn__IsAvatarDrop.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAvatarDrop = value;
		}
	}

	[ProtoMember(14, Name = "is_theme_wanted_double_drop")]
	public bool IsThemeWantedDoubleDrop
	{
		get
		{
			return __pbn__IsThemeWantedDoubleDrop.GetValueOrDefault();
		}
		set
		{
			__pbn__IsThemeWantedDoubleDrop = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeItemId()
	{
		return __pbn__ItemId.HasValue;
	}

	public void ResetItemId()
	{
		__pbn__ItemId = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeNum()
	{
		return __pbn__Num.HasValue;
	}

	public void ResetNum()
	{
		__pbn__Num = null;
	}

	public bool ShouldSerializeRarity()
	{
		return __pbn__Rarity.HasValue;
	}

	public void ResetRarity()
	{
		__pbn__Rarity = null;
	}

	public bool ShouldSerializeIsBaodi()
	{
		return __pbn__IsBaodi.HasValue;
	}

	public void ResetIsBaodi()
	{
		__pbn__IsBaodi = null;
	}

	public bool ShouldSerializeIsRareDrop()
	{
		return __pbn__IsRareDrop.HasValue;
	}

	public void ResetIsRareDrop()
	{
		__pbn__IsRareDrop = null;
	}

	public bool ShouldSerializeGiftItemId()
	{
		return __pbn__GiftItemId.HasValue;
	}

	public void ResetGiftItemId()
	{
		__pbn__GiftItemId = null;
	}

	public bool ShouldSerializeGiftLevel()
	{
		return __pbn__GiftLevel.HasValue;
	}

	public void ResetGiftLevel()
	{
		__pbn__GiftLevel = null;
	}

	public bool ShouldSerializeGiftNum()
	{
		return __pbn__GiftNum.HasValue;
	}

	public void ResetGiftNum()
	{
		__pbn__GiftNum = null;
	}

	public bool ShouldSerializeIsTreasure()
	{
		return __pbn__IsTreasure.HasValue;
	}

	public void ResetIsTreasure()
	{
		__pbn__IsTreasure = null;
	}

	public bool ShouldSerializeIsEquipDrop()
	{
		return __pbn__IsEquipDrop.HasValue;
	}

	public void ResetIsEquipDrop()
	{
		__pbn__IsEquipDrop = null;
	}

	public bool ShouldSerializeIsStageClearDrop()
	{
		return __pbn__IsStageClearDrop.HasValue;
	}

	public void ResetIsStageClearDrop()
	{
		__pbn__IsStageClearDrop = null;
	}

	public bool ShouldSerializeIsAvatarDrop()
	{
		return __pbn__IsAvatarDrop.HasValue;
	}

	public void ResetIsAvatarDrop()
	{
		__pbn__IsAvatarDrop = null;
	}

	public bool ShouldSerializeIsThemeWantedDoubleDrop()
	{
		return __pbn__IsThemeWantedDoubleDrop.HasValue;
	}

	public void ResetIsThemeWantedDoubleDrop()
	{
		__pbn__IsThemeWantedDoubleDrop = null;
	}
}
