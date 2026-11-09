using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatworldEntityType
{
	[ProtoEnum(Name = "CHATWORLD_ENTITY_TYPE_NONE")]
	ChatworldEntityTypeNone,
	[ProtoEnum(Name = "CHATWORLD_ENTITY_TYPE_PLAYER")]
	ChatworldEntityTypePlayer,
	[ProtoEnum(Name = "CHATWORLD_ENTITY_TYPE_SKILL")]
	ChatworldEntityTypeSkill,
	[ProtoEnum(Name = "CHATWORLD_ENTITY_TYPE_ITEM")]
	ChatworldEntityTypeItem,
	[ProtoEnum(Name = "CHATWORLD_ENTITY_TYPE_STANCE")]
	ChatworldEntityTypeStance,
	[ProtoEnum(Name = "CHATWORLD_ENTITY_TYPE_FISH")]
	ChatworldEntityTypeFish,
	[ProtoEnum(Name = "CHATWORLD_ENTITY_TYPE_QUESTION")]
	ChatworldEntityTypeQuestion,
	[ProtoEnum(Name = "CHATWORLD_ENTITY_TYPE_BEAST")]
	ChatworldEntityTypeBeast,
	[ProtoEnum(Name = "CHATWORLD_ENTITY_TYPE_BOX")]
	ChatworldEntityTypeBox,
	[ProtoEnum(Name = "CHATWORLD_ENTITY_TYPE_WOODEN_BOY")]
	ChatworldEntityTypeWoodenBoy
}
