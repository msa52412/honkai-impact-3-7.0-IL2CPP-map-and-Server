using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldEntityState : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__EntityId;

	private uint? __pbn__EntityState;

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

	[ProtoMember(2, Name = "entity_state")]
	public uint EntityState
	{
		get
		{
			return __pbn__EntityState.GetValueOrDefault();
		}
		set
		{
			__pbn__EntityState = value;
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

	public bool ShouldSerializeEntityState()
	{
		return __pbn__EntityState.HasValue;
	}

	public void ResetEntityState()
	{
		__pbn__EntityState = null;
	}
}
