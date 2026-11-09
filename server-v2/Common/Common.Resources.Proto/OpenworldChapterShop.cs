using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterShop : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ShopId;

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

	[ProtoMember(2, Name = "goods_list")]
	public List<OpenworldChapterShopGoods> GoodsLists { get; } = new List<OpenworldChapterShopGoods>();


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
}
