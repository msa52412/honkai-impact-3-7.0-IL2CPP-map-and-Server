using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarSiteType
{
	[ProtoEnum(Name = "GOD_WAR_SITE_TYPE_NORMAL")]
	GodWarSiteTypeNormal = 1,
	[ProtoEnum(Name = "GOD_WAR_SITE_TYPE_REST")]
	GodWarSiteTypeRest,
	[ProtoEnum(Name = "GOD_WAR_SITE_TYPE_BOSS")]
	GodWarSiteTypeBoss,
	[ProtoEnum(Name = "GOD_WAR_SITE_TYPE_FINAL_BOSS")]
	GodWarSiteTypeFinalBoss
}
