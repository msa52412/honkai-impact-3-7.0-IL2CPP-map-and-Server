using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatworldEnterType
{
	[ProtoEnum(Name = "CHATWORLD_ENTER_TYPE_NONE")]
	ChatworldEnterTypeNone,
	[ProtoEnum(Name = "CHATWORLD_ENTER_TYPE_WORLD")]
	ChatworldEnterTypeWorld,
	[ProtoEnum(Name = "CHATWORLD_ENTER_TYPE_ARMADA")]
	ChatworldEnterTypeArmada
}
