using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum JiguangTagType
{
	[ProtoEnum(Name = "TAG_NONE")]
	TagNone,
	[ProtoEnum(Name = "TAG_PLAYER_LEVEL")]
	TagPlayerLevel,
	[ProtoEnum(Name = "TAG_VIP_LEVEL")]
	TagVipLevel,
	[ProtoEnum(Name = "TAG_BATTLE_PASS")]
	TagBattlePass,
	[ProtoEnum(Name = "TAG_GACHA")]
	TagGacha
}
