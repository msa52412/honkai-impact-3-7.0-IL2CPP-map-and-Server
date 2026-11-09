using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleEntity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__EntityId;

	private QCandyBattleEntityType? __pbn__Type;

	private uint? __pbn__PropId;

	private uint? __pbn__OwnerEntityId;

	private uint? __pbn__State;

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

	[ProtoMember(2, Name = "type")]
	[DefaultValue(QCandyBattleEntityType.QcandyBattleEntityTypeNone)]
	public QCandyBattleEntityType Type
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

	[ProtoMember(3, Name = "component_set")]
	public QCandyBattleComponentSet ComponentSet { get; set; }

	[ProtoMember(4, Name = "prop_id")]
	public uint PropId
	{
		get
		{
			return __pbn__PropId.GetValueOrDefault();
		}
		set
		{
			__pbn__PropId = value;
		}
	}

	[ProtoMember(5, Name = "owner_entity_id")]
	public uint OwnerEntityId
	{
		get
		{
			return __pbn__OwnerEntityId.GetValueOrDefault();
		}
		set
		{
			__pbn__OwnerEntityId = value;
		}
	}

	[ProtoMember(6, Name = "state")]
	public uint State
	{
		get
		{
			return __pbn__State.GetValueOrDefault();
		}
		set
		{
			__pbn__State = value;
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

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializePropId()
	{
		return __pbn__PropId.HasValue;
	}

	public void ResetPropId()
	{
		__pbn__PropId = null;
	}

	public bool ShouldSerializeOwnerEntityId()
	{
		return __pbn__OwnerEntityId.HasValue;
	}

	public void ResetOwnerEntityId()
	{
		__pbn__OwnerEntityId = null;
	}

	public bool ShouldSerializeState()
	{
		return __pbn__State.HasValue;
	}

	public void ResetState()
	{
		__pbn__State = null;
	}
}
