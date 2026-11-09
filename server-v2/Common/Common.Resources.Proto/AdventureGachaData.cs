using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AdventureGachaData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TicketHcoinCost;

	private uint? __pbn__TicketMaterialId;

	private bool? __pbn__IsEnableBaodi;

	private uint? __pbn__GachaTimes;

	private bool? __pbn__IsProtectDisplay;

	private uint? __pbn__DisplayMaxTimes;

	private uint? __pbn__NoProtectGachaTimes;

	private bool? __pbn__IsGotSAvatarProtect;

	private double? __pbn__NewSAvatarProb;

	private uint? __pbn__NextSAvatarAssureGachaTimes;

	private uint? __pbn__NextSAvatarAssureNewNeedTimes;

	private uint? __pbn__FreeGachaTimes;

	[ProtoMember(1, Name = "ticket_hcoin_cost")]
	public uint TicketHcoinCost
	{
		get
		{
			return __pbn__TicketHcoinCost.GetValueOrDefault();
		}
		set
		{
			__pbn__TicketHcoinCost = value;
		}
	}

	[ProtoMember(2, Name = "ticket_material_id")]
	public uint TicketMaterialId
	{
		get
		{
			return __pbn__TicketMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__TicketMaterialId = value;
		}
	}

	[ProtoMember(3, Name = "is_enable_baodi")]
	public bool IsEnableBaodi
	{
		get
		{
			return __pbn__IsEnableBaodi.GetValueOrDefault();
		}
		set
		{
			__pbn__IsEnableBaodi = value;
		}
	}

	[ProtoMember(4, Name = "gacha_times")]
	public uint GachaTimes
	{
		get
		{
			return __pbn__GachaTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaTimes = value;
		}
	}

	[ProtoMember(5, Name = "is_protect_display")]
	public bool IsProtectDisplay
	{
		get
		{
			return __pbn__IsProtectDisplay.GetValueOrDefault();
		}
		set
		{
			__pbn__IsProtectDisplay = value;
		}
	}

	[ProtoMember(6, Name = "display_max_times")]
	public uint DisplayMaxTimes
	{
		get
		{
			return __pbn__DisplayMaxTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__DisplayMaxTimes = value;
		}
	}

	[ProtoMember(7, Name = "no_protect_gacha_times")]
	public uint NoProtectGachaTimes
	{
		get
		{
			return __pbn__NoProtectGachaTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__NoProtectGachaTimes = value;
		}
	}

	[ProtoMember(8, Name = "is_got_s_avatar_protect")]
	public bool IsGotSAvatarProtect
	{
		get
		{
			return __pbn__IsGotSAvatarProtect.GetValueOrDefault();
		}
		set
		{
			__pbn__IsGotSAvatarProtect = value;
		}
	}

	[ProtoMember(9, Name = "new_s_avatar_prob")]
	public double NewSAvatarProb
	{
		get
		{
			return __pbn__NewSAvatarProb.GetValueOrDefault();
		}
		set
		{
			__pbn__NewSAvatarProb = value;
		}
	}

	[ProtoMember(10, Name = "next_s_avatar_assure_gacha_times")]
	public uint NextSAvatarAssureGachaTimes
	{
		get
		{
			return __pbn__NextSAvatarAssureGachaTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__NextSAvatarAssureGachaTimes = value;
		}
	}

	[ProtoMember(11, Name = "next_s_avatar_assure_new_need_times")]
	public uint NextSAvatarAssureNewNeedTimes
	{
		get
		{
			return __pbn__NextSAvatarAssureNewNeedTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__NextSAvatarAssureNewNeedTimes = value;
		}
	}

	[ProtoMember(12, Name = "free_gacha_times")]
	public uint FreeGachaTimes
	{
		get
		{
			return __pbn__FreeGachaTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__FreeGachaTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTicketHcoinCost()
	{
		return __pbn__TicketHcoinCost.HasValue;
	}

	public void ResetTicketHcoinCost()
	{
		__pbn__TicketHcoinCost = null;
	}

	public bool ShouldSerializeTicketMaterialId()
	{
		return __pbn__TicketMaterialId.HasValue;
	}

	public void ResetTicketMaterialId()
	{
		__pbn__TicketMaterialId = null;
	}

	public bool ShouldSerializeIsEnableBaodi()
	{
		return __pbn__IsEnableBaodi.HasValue;
	}

	public void ResetIsEnableBaodi()
	{
		__pbn__IsEnableBaodi = null;
	}

	public bool ShouldSerializeGachaTimes()
	{
		return __pbn__GachaTimes.HasValue;
	}

	public void ResetGachaTimes()
	{
		__pbn__GachaTimes = null;
	}

	public bool ShouldSerializeIsProtectDisplay()
	{
		return __pbn__IsProtectDisplay.HasValue;
	}

	public void ResetIsProtectDisplay()
	{
		__pbn__IsProtectDisplay = null;
	}

	public bool ShouldSerializeDisplayMaxTimes()
	{
		return __pbn__DisplayMaxTimes.HasValue;
	}

	public void ResetDisplayMaxTimes()
	{
		__pbn__DisplayMaxTimes = null;
	}

	public bool ShouldSerializeNoProtectGachaTimes()
	{
		return __pbn__NoProtectGachaTimes.HasValue;
	}

	public void ResetNoProtectGachaTimes()
	{
		__pbn__NoProtectGachaTimes = null;
	}

	public bool ShouldSerializeIsGotSAvatarProtect()
	{
		return __pbn__IsGotSAvatarProtect.HasValue;
	}

	public void ResetIsGotSAvatarProtect()
	{
		__pbn__IsGotSAvatarProtect = null;
	}

	public bool ShouldSerializeNewSAvatarProb()
	{
		return __pbn__NewSAvatarProb.HasValue;
	}

	public void ResetNewSAvatarProb()
	{
		__pbn__NewSAvatarProb = null;
	}

	public bool ShouldSerializeNextSAvatarAssureGachaTimes()
	{
		return __pbn__NextSAvatarAssureGachaTimes.HasValue;
	}

	public void ResetNextSAvatarAssureGachaTimes()
	{
		__pbn__NextSAvatarAssureGachaTimes = null;
	}

	public bool ShouldSerializeNextSAvatarAssureNewNeedTimes()
	{
		return __pbn__NextSAvatarAssureNewNeedTimes.HasValue;
	}

	public void ResetNextSAvatarAssureNewNeedTimes()
	{
		__pbn__NextSAvatarAssureNewNeedTimes = null;
	}

	public bool ShouldSerializeFreeGachaTimes()
	{
		return __pbn__FreeGachaTimes.HasValue;
	}

	public void ResetFreeGachaTimes()
	{
		__pbn__FreeGachaTimes = null;
	}
}
