using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivityShopBuyReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6468
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ShopBuildingUniqueId;

	private uint? __pbn__GoodsId;

	[ProtoMember(1, Name = "head")]
	public TownCommonHead Head { get; set; }

	[ProtoMember(2, Name = "shop_building_unique_id")]
	public uint ShopBuildingUniqueId
	{
		get
		{
			return __pbn__ShopBuildingUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__ShopBuildingUniqueId = value;
		}
	}

	[ProtoMember(3, Name = "goods_id")]
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

	public bool ShouldSerializeShopBuildingUniqueId()
	{
		return __pbn__ShopBuildingUniqueId.HasValue;
	}

	public void ResetShopBuildingUniqueId()
	{
		__pbn__ShopBuildingUniqueId = null;
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
