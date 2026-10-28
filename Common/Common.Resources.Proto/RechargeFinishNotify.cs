using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RechargeFinishNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6742
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ORDER_REPEAT")]
		OrderRepeat,
		[ProtoEnum(Name = "ITUNES_RECEIPT_ERROR")]
		ItunesReceiptError,
		[ProtoEnum(Name = "DUPLICATE_FAIL")]
		DuplicateFail
	}

	private IExtension __pbn__extensionData;

	private string __pbn__ProductName;

	private uint? __pbn__PayHcoin;

	private uint? __pbn__FreeHcoin;

	private string __pbn__ProductDesc;

	private uint? __pbn__ProductPrice;

	private string __pbn__ChannelName;

	private string __pbn__ChannelOrderNo;

	private bool? __pbn__IsAddHcoin;

	private uint? __pbn__Mcoin;

	private uint? __pbn__GoodsId;

	private string __pbn__PayCurrency;

	private string __pbn__PayPrice;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "product_name")]
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

	[ProtoMember(3, Name = "pay_hcoin")]
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

	[ProtoMember(4, Name = "free_hcoin")]
	public uint FreeHcoin
	{
		get
		{
			return __pbn__FreeHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__FreeHcoin = value;
		}
	}

	[ProtoMember(5, Name = "product_desc")]
	[DefaultValue("")]
	public string ProductDesc
	{
		get
		{
			return __pbn__ProductDesc ?? "";
		}
		set
		{
			__pbn__ProductDesc = value;
		}
	}

	[ProtoMember(6, Name = "box_reward_list")]
	public List<RewardData> BoxRewardLists { get; } = new List<RewardData>();


	[ProtoMember(7, Name = "product_price")]
	public uint ProductPrice
	{
		get
		{
			return __pbn__ProductPrice.GetValueOrDefault();
		}
		set
		{
			__pbn__ProductPrice = value;
		}
	}

	[ProtoMember(8, Name = "channel_name")]
	[DefaultValue("")]
	public string ChannelName
	{
		get
		{
			return __pbn__ChannelName ?? "";
		}
		set
		{
			__pbn__ChannelName = value;
		}
	}

	[ProtoMember(9, Name = "channel_order_no")]
	[DefaultValue("")]
	public string ChannelOrderNo
	{
		get
		{
			return __pbn__ChannelOrderNo ?? "";
		}
		set
		{
			__pbn__ChannelOrderNo = value;
		}
	}

	[ProtoMember(10, Name = "is_add_hcoin")]
	public bool IsAddHcoin
	{
		get
		{
			return __pbn__IsAddHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAddHcoin = value;
		}
	}

	[ProtoMember(11, Name = "mcoin")]
	public uint Mcoin
	{
		get
		{
			return __pbn__Mcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__Mcoin = value;
		}
	}

	[ProtoMember(12, Name = "goods_id")]
	public uint GoodsId
	{
		get
		{
			return __pbn__GoodsId.GetValueOrDefault();
		}
		set
		{
			__pbn__GoodsId = value;
		}
	}

	[ProtoMember(13, Name = "pay_currency")]
	[DefaultValue("")]
	public string PayCurrency
	{
		get
		{
			return __pbn__PayCurrency ?? "";
		}
		set
		{
			__pbn__PayCurrency = value;
		}
	}

	[ProtoMember(14, Name = "pay_price")]
	[DefaultValue("")]
	public string PayPrice
	{
		get
		{
			return __pbn__PayPrice ?? "";
		}
		set
		{
			__pbn__PayPrice = value;
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

	public bool ShouldSerializePayHcoin()
	{
		return __pbn__PayHcoin.HasValue;
	}

	public void ResetPayHcoin()
	{
		__pbn__PayHcoin = null;
	}

	public bool ShouldSerializeFreeHcoin()
	{
		return __pbn__FreeHcoin.HasValue;
	}

	public void ResetFreeHcoin()
	{
		__pbn__FreeHcoin = null;
	}

	public bool ShouldSerializeProductDesc()
	{
		return __pbn__ProductDesc != null;
	}

	public void ResetProductDesc()
	{
		__pbn__ProductDesc = null;
	}

	public bool ShouldSerializeProductPrice()
	{
		return __pbn__ProductPrice.HasValue;
	}

	public void ResetProductPrice()
	{
		__pbn__ProductPrice = null;
	}

	public bool ShouldSerializeChannelName()
	{
		return __pbn__ChannelName != null;
	}

	public void ResetChannelName()
	{
		__pbn__ChannelName = null;
	}

	public bool ShouldSerializeChannelOrderNo()
	{
		return __pbn__ChannelOrderNo != null;
	}

	public void ResetChannelOrderNo()
	{
		__pbn__ChannelOrderNo = null;
	}

	public bool ShouldSerializeIsAddHcoin()
	{
		return __pbn__IsAddHcoin.HasValue;
	}

	public void ResetIsAddHcoin()
	{
		__pbn__IsAddHcoin = null;
	}

	public bool ShouldSerializeMcoin()
	{
		return __pbn__Mcoin.HasValue;
	}

	public void ResetMcoin()
	{
		__pbn__Mcoin = null;
	}

	public bool ShouldSerializeGoodsId()
	{
		return __pbn__GoodsId.HasValue;
	}

	public void ResetGoodsId()
	{
		__pbn__GoodsId = null;
	}

	public bool ShouldSerializePayCurrency()
	{
		return __pbn__PayCurrency != null;
	}

	public void ResetPayCurrency()
	{
		__pbn__PayCurrency = null;
	}

	public bool ShouldSerializePayPrice()
	{
		return __pbn__PayPrice != null;
	}

	public void ResetPayPrice()
	{
		__pbn__PayPrice = null;
	}
}
