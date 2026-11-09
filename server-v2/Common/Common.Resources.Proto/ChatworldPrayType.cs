using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatworldPrayType
{
	[ProtoEnum(Name = "CHATWORLD_PRAY_TYPE_NONE")]
	ChatworldPrayTypeNone,
	[ProtoEnum(Name = "CHATWORLD_PRAY_TYPE_NORMAL")]
	ChatworldPrayTypeNormal,
	[ProtoEnum(Name = "CHATWORLD_PRAY_TYPE_CHARACTER")]
	ChatworldPrayTypeCharacter,
	[ProtoEnum(Name = "CHATWORLD_PRAY_TYPE_OLD")]
	ChatworldPrayTypeOld
}
