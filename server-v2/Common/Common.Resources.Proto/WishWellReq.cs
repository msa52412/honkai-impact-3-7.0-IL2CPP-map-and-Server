using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WishWellReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4710
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__WishId;

	private GachaType? __pbn__GachaType;

	private bool? __pbn__IsForceSet;

	[ProtoMember(1, Name = "wish_id")]
	public uint WishId
	{
		get
		{
			return __pbn__WishId.GetValueOrDefault();
		}
		set
		{
			__pbn__WishId = value;
		}
	}

	[ProtoMember(2, Name = "cost_equip_list")]
	public List<WishCostEquip> CostEquipLists { get; } = new List<WishCostEquip>();


	[ProtoMember(3, Name = "gacha_type")]
	[DefaultValue(GachaType.GachaHcoin)]
	public GachaType GachaType
	{
		get
		{
			return __pbn__GachaType ?? GachaType.GachaHcoin;
		}
		set
		{
			__pbn__GachaType = value;
		}
	}

	[ProtoMember(4, Name = "is_force_set")]
	public bool IsForceSet
	{
		get
		{
			return __pbn__IsForceSet.GetValueOrDefault();
		}
		set
		{
			__pbn__IsForceSet = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeWishId()
	{
		return __pbn__WishId.HasValue;
	}

	public void ResetWishId()
	{
		__pbn__WishId = null;
	}

	public bool ShouldSerializeGachaType()
	{
		return __pbn__GachaType.HasValue;
	}

	public void ResetGachaType()
	{
		__pbn__GachaType = null;
	}

	public bool ShouldSerializeIsForceSet()
	{
		return __pbn__IsForceSet.HasValue;
	}

	public void ResetIsForceSet()
	{
		__pbn__IsForceSet = null;
	}
}
