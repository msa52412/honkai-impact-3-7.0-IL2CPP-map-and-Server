using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ShopGoodsInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__GoodsId;

	private uint? __pbn__SellNum;

	[ProtoMember(1, Name = "goods_id")]
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

	[ProtoMember(2, Name = "sell_num")]
	public uint SellNum
	{
		get
		{
			return __pbn__SellNum.GetValueOrDefault();
		}
		set
		{
			__pbn__SellNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGoodsId()
	{
		return __pbn__GoodsId.HasValue;
	}

	public void ResetGoodsId()
	{
		__pbn__GoodsId = null;
	}

	public bool ShouldSerializeSellNum()
	{
		return __pbn__SellNum.HasValue;
	}

	public void ResetSellNum()
	{
		__pbn__SellNum = null;
	}
}
