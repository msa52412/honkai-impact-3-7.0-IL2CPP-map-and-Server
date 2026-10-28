using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaGrid : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private ArmadaCabinType? __pbn__Type;

	private uint? __pbn__CabinId;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "type")]
	[DefaultValue(ArmadaCabinType.ArmadaCabinNone)]
	public ArmadaCabinType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(3, Name = "cabin_id")]
	public uint CabinId
	{
		get
		{
			return __pbn__CabinId.GetValueOrDefault();
		}
		set
		{
			__pbn__CabinId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeCabinId()
	{
		return __pbn__CabinId.HasValue;
	}

	public void ResetCabinId()
	{
		__pbn__CabinId = null;
	}
}
