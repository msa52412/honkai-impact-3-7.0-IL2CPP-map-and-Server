using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatworldType
{
	[ProtoEnum(Name = "CHATWORLD_TYPE_NONE")]
	ChatworldTypeNone,
	[ProtoEnum(Name = "CHATWORLD_TYPE_PLAIN")]
	ChatworldTypePlain,
	[ProtoEnum(Name = "CHATWORLD_TYPE_KINGDOM_WAR")]
	ChatworldTypeKingdomWar,
	[ProtoEnum(Name = "CHATWORLD_TYPE_BRAWL")]
	ChatworldTypeBrawl,
	[ProtoEnum(Name = "CHATWORLD_TYPE_STANCE")]
	ChatworldTypeStance
}
