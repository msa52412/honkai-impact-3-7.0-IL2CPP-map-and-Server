using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SlgBossType
{
	[ProtoEnum(Name = "SLG_BOSS_TYPE_NONE")]
	SlgBossTypeNone,
	[ProtoEnum(Name = "SLG_BOSS_TYPE_SMALL_BOSS")]
	SlgBossTypeSmallBoss,
	[ProtoEnum(Name = "SLG_BOSS_TYPE_BIG_BOSS")]
	SlgBossTypeBigBoss
}
