using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarShop : IExtensible
{
	private IExtension __pbn__extensionData;

	private bool? __pbn__IsCanChangeState;

	private bool? __pbn__IsCanRecover;

	private uint? __pbn__ChangeStateEventId;

	private uint? __pbn__RecoverEventId;

	private uint? __pbn__SpecialDiscount;

	private uint? __pbn__RefreshShopCost;

	private uint? __pbn__RefreshShopRandom;

	private uint? __pbn__ShoppingCost;

	[ProtoMember(1, Name = "goods_list")]
	public List<GodWarGoods> GoodsLists { get; } = new List<GodWarGoods>();


	[ProtoMember(2, Name = "is_can_change_state")]
	public bool IsCanChangeState
	{
		get
		{
			return __pbn__IsCanChangeState.GetValueOrDefault();
		}
		set
		{
			__pbn__IsCanChangeState = value;
		}
	}

	[ProtoMember(3, Name = "is_can_recover")]
	public bool IsCanRecover
	{
		get
		{
			return __pbn__IsCanRecover.GetValueOrDefault();
		}
		set
		{
			__pbn__IsCanRecover = value;
		}
	}

	[ProtoMember(4, Name = "change_state_event_id")]
	public uint ChangeStateEventId
	{
		get
		{
			return __pbn__ChangeStateEventId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChangeStateEventId = value;
		}
	}

	[ProtoMember(5, Name = "recover_event_id")]
	public uint RecoverEventId
	{
		get
		{
			return __pbn__RecoverEventId.GetValueOrDefault();
		}
		set
		{
			__pbn__RecoverEventId = value;
		}
	}

	[ProtoMember(6, Name = "special_discount")]
	public uint SpecialDiscount
	{
		get
		{
			return __pbn__SpecialDiscount.GetValueOrDefault();
		}
		set
		{
			__pbn__SpecialDiscount = value;
		}
	}

	[ProtoMember(7, Name = "refresh_shop_cost")]
	public uint RefreshShopCost
	{
		get
		{
			return __pbn__RefreshShopCost.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshShopCost = value;
		}
	}

	[ProtoMember(8, Name = "refresh_shop_random")]
	public uint RefreshShopRandom
	{
		get
		{
			return __pbn__RefreshShopRandom.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshShopRandom = value;
		}
	}

	[ProtoMember(9, Name = "shopping_cost")]
	public uint ShoppingCost
	{
		get
		{
			return __pbn__ShoppingCost.GetValueOrDefault();
		}
		set
		{
			__pbn__ShoppingCost = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsCanChangeState()
	{
		return __pbn__IsCanChangeState.HasValue;
	}

	public void ResetIsCanChangeState()
	{
		__pbn__IsCanChangeState = null;
	}

	public bool ShouldSerializeIsCanRecover()
	{
		return __pbn__IsCanRecover.HasValue;
	}

	public void ResetIsCanRecover()
	{
		__pbn__IsCanRecover = null;
	}

	public bool ShouldSerializeChangeStateEventId()
	{
		return __pbn__ChangeStateEventId.HasValue;
	}

	public void ResetChangeStateEventId()
	{
		__pbn__ChangeStateEventId = null;
	}

	public bool ShouldSerializeRecoverEventId()
	{
		return __pbn__RecoverEventId.HasValue;
	}

	public void ResetRecoverEventId()
	{
		__pbn__RecoverEventId = null;
	}

	public bool ShouldSerializeSpecialDiscount()
	{
		return __pbn__SpecialDiscount.HasValue;
	}

	public void ResetSpecialDiscount()
	{
		__pbn__SpecialDiscount = null;
	}

	public bool ShouldSerializeRefreshShopCost()
	{
		return __pbn__RefreshShopCost.HasValue;
	}

	public void ResetRefreshShopCost()
	{
		__pbn__RefreshShopCost = null;
	}

	public bool ShouldSerializeRefreshShopRandom()
	{
		return __pbn__RefreshShopRandom.HasValue;
	}

	public void ResetRefreshShopRandom()
	{
		__pbn__RefreshShopRandom = null;
	}

	public bool ShouldSerializeShoppingCost()
	{
		return __pbn__ShoppingCost.HasValue;
	}

	public void ResetShoppingCost()
	{
		__pbn__ShoppingCost = null;
	}
}
