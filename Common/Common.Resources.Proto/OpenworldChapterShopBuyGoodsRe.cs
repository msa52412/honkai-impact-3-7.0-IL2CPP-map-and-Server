using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterShopBuyGoodsReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4577
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ShopId;

	private uint? __pbn__GoodsId;

	[ProtoMember(1, Name = "shop_id")]
	public uint ShopId
	{
		get
		{
			return __pbn__ShopId.GetValueOrDefault();
		}
		set
		{
			__pbn__ShopId = value;
		}
	}

	[ProtoMember(2, Name = "goods_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeShopId()
	{
		return __pbn__ShopId.HasValue;
	}

	public void ResetShopId()
	{
		__pbn__ShopId = null;
	}

	public bool ShouldSerializeGoodsId()
	{
		return __pbn__GoodsId.HasValue;
	}

	public void ResetGoodsId()
	{
		__pbn__GoodsId = null;
	}
}
