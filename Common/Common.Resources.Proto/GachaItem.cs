using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GachaItem : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ItemId;

	private uint? __pbn__Level;

	private uint? __pbn__Num;

	private uint? __pbn__SplitFragmentNum;

	private bool? __pbn__IsRareDrop;

	private uint? __pbn__GiftItemId;

	private uint? __pbn__GiftLevel;

	private uint? __pbn__GiftNum;

	private bool? __pbn__IsClientDisplayTransfer;

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

	[ProtoMember(4, Name = "split_fragment_num")]
	public uint SplitFragmentNum
	{
		get
		{
			return __pbn__SplitFragmentNum.GetValueOrDefault();
		}
		set
		{
			__pbn__SplitFragmentNum = value;
		}
	}

	[ProtoMember(5, Name = "is_rare_drop")]
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

	[ProtoMember(6, Name = "gift_item_id")]
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

	[ProtoMember(7, Name = "gift_level")]
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

	[ProtoMember(8, Name = "gift_num")]
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

	[ProtoMember(9, Name = "transfer_item_list")]
	public List<GenericItemNum> TransferItemLists { get; } = new List<GenericItemNum>();


	[ProtoMember(10, Name = "is_client_display_transfer")]
	public bool IsClientDisplayTransfer
	{
		get
		{
			return __pbn__IsClientDisplayTransfer.GetValueOrDefault();
		}
		set
		{
			__pbn__IsClientDisplayTransfer = value;
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

	public bool ShouldSerializeSplitFragmentNum()
	{
		return __pbn__SplitFragmentNum.HasValue;
	}

	public void ResetSplitFragmentNum()
	{
		__pbn__SplitFragmentNum = null;
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

	public bool ShouldSerializeIsClientDisplayTransfer()
	{
		return __pbn__IsClientDisplayTransfer.HasValue;
	}

	public void ResetIsClientDisplayTransfer()
	{
		__pbn__IsClientDisplayTransfer = null;
	}
}
