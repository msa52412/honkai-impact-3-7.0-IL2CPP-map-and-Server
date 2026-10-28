using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WarshipComponent : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Type;

	private uint? __pbn__ComponentId;

	[ProtoMember(1, Name = "type")]
	public uint Type
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

	[ProtoMember(2, Name = "component_id")]
	public uint ComponentId
	{
		get
		{
			return __pbn__ComponentId.GetValueOrDefault();
		}
		set
		{
			__pbn__ComponentId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeComponentId()
	{
		return __pbn__ComponentId.HasValue;
	}

	public void ResetComponentId()
	{
		__pbn__ComponentId = null;
	}
}
