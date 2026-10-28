using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatworldInteractType
{
	[ProtoEnum(Name = "CHATWORLD_INTERACT_TYPE_NONE")]
	ChatworldInteractTypeNone,
	[ProtoEnum(Name = "CHATWORLD_INTERACT_TYPE_HOLD")]
	ChatworldInteractTypeHold,
	[ProtoEnum(Name = "CHATWORLD_INTERACT_TYPE_UNHOLD")]
	ChatworldInteractTypeUnhold
}
