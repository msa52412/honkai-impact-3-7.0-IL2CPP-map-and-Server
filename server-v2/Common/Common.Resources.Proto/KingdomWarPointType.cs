using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum KingdomWarPointType
{
	[ProtoEnum(Name = "KINGDOM_WAR_POINT_NONE")]
	KingdomWarPointNone,
	[ProtoEnum(Name = "KINGDOM_WAR_POINT_NORMAL")]
	KingdomWarPointNormal,
	[ProtoEnum(Name = "KINGDOM_WAR_POINT_CHATWORLD")]
	KingdomWarPointChatworld,
	[ProtoEnum(Name = "KINGDOM_WAR_POINT_MAIN_CITY")]
	KingdomWarPointMainCity,
	[ProtoEnum(Name = "KINGDOM_WAR_POINT_BOSS")]
	KingdomWarPointBoss
}
