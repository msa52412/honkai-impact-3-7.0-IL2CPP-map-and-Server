using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ShopGoodsSwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__GoodsId;

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
}
