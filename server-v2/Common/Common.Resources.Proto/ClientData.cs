using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ClientData : IExtensible
{
	private IExtension __pbn__extensionData;

	private ClientDataType? __pbn__Type;

	private uint? __pbn__Id;

	private byte[] __pbn__Data;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(ClientDataType.ClientDataNone)]
	public ClientDataType Type
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

	[ProtoMember(2, Name = "id")]
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

	[ProtoMember(3, Name = "data")]
	public byte[] Data
	{
		get
		{
			return __pbn__Data;
		}
		set
		{
			__pbn__Data = value;
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

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeData()
	{
		return __pbn__Data != null;
	}

	public void ResetData()
	{
		__pbn__Data = null;
	}
}
