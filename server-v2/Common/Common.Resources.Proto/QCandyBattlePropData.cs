using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattlePropData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__EntityId;

	private uint? __pbn__Uuid;

	[ProtoMember(1, Name = "entity_id")]
	public uint EntityId
	{
		get
		{
			return __pbn__EntityId.GetValueOrDefault();
		}
		set
		{
			__pbn__EntityId = value;
		}
	}

	[ProtoMember(2, Name = "uuid")]
	public uint Uuid
	{
		get
		{
			return __pbn__Uuid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uuid = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEntityId()
	{
		return __pbn__EntityId.HasValue;
	}

	public void ResetEntityId()
	{
		__pbn__EntityId = null;
	}

	public bool ShouldSerializeUuid()
	{
		return __pbn__Uuid.HasValue;
	}

	public void ResetUuid()
	{
		__pbn__Uuid = null;
	}
}
