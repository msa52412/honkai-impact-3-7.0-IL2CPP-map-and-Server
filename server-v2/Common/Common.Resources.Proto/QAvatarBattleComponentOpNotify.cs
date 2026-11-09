using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleComponentOpNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5611
	}

	[ProtoContract]
	public enum ComponentOpType
	{
		[ProtoEnum(Name = "COMPONENT_OP_NONE")]
		ComponentOpNone,
		[ProtoEnum(Name = "COMPONENT_OP_ADD")]
		ComponentOpAdd,
		[ProtoEnum(Name = "COMPONENT_OP_DEL")]
		ComponentOpDel
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TriggerEntityId;

	private uint? __pbn__ComponentType;

	private ComponentOpType? __pbn__OpType;

	[ProtoMember(1, Name = "trigger_entity_id")]
	public uint TriggerEntityId
	{
		get
		{
			return __pbn__TriggerEntityId.GetValueOrDefault();
		}
		set
		{
			__pbn__TriggerEntityId = value;
		}
	}

	[ProtoMember(2, Name = "component_type")]
	public uint ComponentType
	{
		get
		{
			return __pbn__ComponentType.GetValueOrDefault();
		}
		set
		{
			__pbn__ComponentType = value;
		}
	}

	[ProtoMember(3, Name = "op_type")]
	[DefaultValue(ComponentOpType.ComponentOpNone)]
	public ComponentOpType OpType
	{
		get
		{
			return __pbn__OpType.GetValueOrDefault();
		}
		set
		{
			__pbn__OpType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTriggerEntityId()
	{
		return __pbn__TriggerEntityId.HasValue;
	}

	public void ResetTriggerEntityId()
	{
		__pbn__TriggerEntityId = null;
	}

	public bool ShouldSerializeComponentType()
	{
		return __pbn__ComponentType.HasValue;
	}

	public void ResetComponentType()
	{
		__pbn__ComponentType = null;
	}

	public bool ShouldSerializeOpType()
	{
		return __pbn__OpType.HasValue;
	}

	public void ResetOpType()
	{
		__pbn__OpType = null;
	}
}
