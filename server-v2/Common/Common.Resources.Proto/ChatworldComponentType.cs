using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatworldComponentType
{
	[ProtoEnum(Name = "CHATWORLD_COMPONENT_TYPE_NONE")]
	ChatworldComponentTypeNone,
	[ProtoEnum(Name = "CHATWORLD_COMPONENT_TYPE_MOVE")]
	ChatworldComponentTypeMove,
	[ProtoEnum(Name = "CHATWORLD_COMPONENT_TYPE_BUFF")]
	ChatworldComponentTypeBuff,
	[ProtoEnum(Name = "CHATWORLD_COMPONENT_TYPE_SKILL")]
	ChatworldComponentTypeSkill,
	[ProtoEnum(Name = "CHATWORLD_COMPONENT_TYPE_HEALTH")]
	ChatworldComponentTypeHealth,
	[ProtoEnum(Name = "CHATWORLD_COMPONENT_TYPE_COLLISION")]
	ChatworldComponentTypeCollision
}
