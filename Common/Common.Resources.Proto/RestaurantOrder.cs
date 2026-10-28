using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RestaurantOrder : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MaterialId;

	private uint? __pbn__Price;

	[ProtoMember(1, Name = "material_id")]
	public uint MaterialId
	{
		get
		{
			return __pbn__MaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__MaterialId = value;
		}
	}

	[ProtoMember(2, Name = "price")]
	public uint Price
	{
		get
		{
			return __pbn__Price.GetValueOrDefault();
		}
		set
		{
			__pbn__Price = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMaterialId()
	{
		return __pbn__MaterialId.HasValue;
	}

	public void ResetMaterialId()
	{
		__pbn__MaterialId = null;
	}

	public bool ShouldSerializePrice()
	{
		return __pbn__Price.HasValue;
	}

	public void ResetPrice()
	{
		__pbn__Price = null;
	}
}
