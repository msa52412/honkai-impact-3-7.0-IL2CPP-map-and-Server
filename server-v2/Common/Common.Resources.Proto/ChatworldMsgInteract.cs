using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldMsgInteract : IExtensible
{
	private IExtension __pbn__extensionData;

	private ChatworldInteractType? __pbn__InteractType;

	private uint? __pbn__TargetNpcId;

	[ProtoMember(1, Name = "interact_type")]
	[DefaultValue(ChatworldInteractType.ChatworldInteractTypeNone)]
	public ChatworldInteractType InteractType
	{
		get
		{
			return __pbn__InteractType.GetValueOrDefault();
		}
		set
		{
			__pbn__InteractType = value;
		}
	}

	[ProtoMember(2, Name = "target_npc_id")]
	public uint TargetNpcId
	{
		get
		{
			return __pbn__TargetNpcId.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetNpcId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeInteractType()
	{
		return __pbn__InteractType.HasValue;
	}

	public void ResetInteractType()
	{
		__pbn__InteractType = null;
	}

	public bool ShouldSerializeTargetNpcId()
	{
		return __pbn__TargetNpcId.HasValue;
	}

	public void ResetTargetNpcId()
	{
		__pbn__TargetNpcId = null;
	}
}
