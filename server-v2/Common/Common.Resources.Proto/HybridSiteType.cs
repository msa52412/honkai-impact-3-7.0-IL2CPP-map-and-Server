using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum HybridSiteType
{
	[ProtoEnum(Name = "HYBRID_SITE_TYPE_STORE")]
	HybridSiteTypeStore,
	[ProtoEnum(Name = "HYBRID_SITE_TYPE_CYCLE")]
	HybridSiteTypeCycle,
	[ProtoEnum(Name = "HYBRID_SITE_TYPE_EX")]
	HybridSiteTypeEx,
	[ProtoEnum(Name = "HYBRID_SITE_TYPE_BOSS")]
	HybridSiteTypeBoss,
	[ProtoEnum(Name = "HYBRID_SITE_TYPE_COLLECTION")]
	HybridSiteTypeCollection,
	[ProtoEnum(Name = "HYBRID_SITE_TYPE_MP")]
	HybridSiteTypeMp,
	[ProtoEnum(Name = "HYBRID_SITE_TYPE_CHALLENGE")]
	HybridSiteTypeChallenge,
	[ProtoEnum(Name = "HYBRID_SITE_TYPE_ZONE_CYCLE")]
	HybridSiteTypeZoneCycle,
	[ProtoEnum(Name = "HYBRID_SITE_TYPE_ZONE_CHALLENGE")]
	HybridSiteTypeZoneChallenge,
	[ProtoEnum(Name = "HYBRID_SITE_TYPE_EX_BOSS")]
	HybridSiteTypeExBoss,
	[ProtoEnum(Name = "HYBRID_SITE_TYPE_TOWER")]
	HybridSiteTypeTower,
	[ProtoEnum(Name = "HYBRID_SITE_TYPE_CG")]
	HybridSiteTypeCg,
	[ProtoEnum(Name = "HYBRID_SITE_TYPE_GOD_WAR")]
	HybridSiteTypeGodWar,
	[ProtoEnum(Name = "HYBRID_SITE_TYPE_BW_WORLD_BRANCH")]
	HybridSiteTypeBwWorldBranch
}
