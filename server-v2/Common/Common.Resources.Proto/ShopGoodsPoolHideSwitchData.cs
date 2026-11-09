using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ShopGoodsPoolHideSwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ShopId;

	private uint? __pbn__PoolId;

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

	[ProtoMember(2, Name = "pool_id")]
	public uint PoolId
	{
		get
		{
			return __pbn__PoolId.GetValueOrDefault();
		}
		set
		{
			__pbn__PoolId = value;
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

	public bool ShouldSerializePoolId()
	{
		return __pbn__PoolId.HasValue;
	}

	public void ResetPoolId()
	{
		__pbn__PoolId = null;
	}
}
