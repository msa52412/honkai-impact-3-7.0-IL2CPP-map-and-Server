using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatworldBuffType
{
	[ProtoEnum(Name = "CHATWORLD_BUFF_TYPE_NONE")]
	ChatworldBuffTypeNone,
	[ProtoEnum(Name = "CHATWORLD_BUFF_TYPE_DAMAGE_FREE")]
	ChatworldBuffTypeDamageFree,
	[ProtoEnum(Name = "CHATWORLD_BUFF_TYPE_SPEED")]
	ChatworldBuffTypeSpeed
}
