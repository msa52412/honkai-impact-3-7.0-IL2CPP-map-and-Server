using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalShopGoodsInfoNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6716
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "shop_goods_list")]
	public List<ShopGoodsInfo> ShopGoodsLists { get; } = new List<ShopGoodsInfo>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
