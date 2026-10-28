using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MonopolyShopGoods : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__GoodsId;

	private uint? __pbn__BuyTimes;

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

	[ProtoMember(2, Name = "buy_times")]
	public uint BuyTimes
	{
		get
		{
			return __pbn__BuyTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__BuyTimes = value;
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

	public bool ShouldSerializeBuyTimes()
	{
		return __pbn__BuyTimes.HasValue;
	}

	public void ResetBuyTimes()
	{
		__pbn__BuyTimes = null;
	}
}
