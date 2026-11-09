using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarSiteStatus
{
	[ProtoEnum(Name = "GOD_WAR_SITE_STATUS_INVALID")]
	GodWarSiteStatusInvalid,
	[ProtoEnum(Name = "GOD_WAR_SITE_STATUS_HIDE")]
	GodWarSiteStatusHide,
	[ProtoEnum(Name = "GOD_WAR_SITE_STATUS_SHOW")]
	GodWarSiteStatusShow,
	[ProtoEnum(Name = "GOD_WAR_SITE_STATUS_UNLOCKED")]
	GodWarSiteStatusUnlocked,
	[ProtoEnum(Name = "GOD_WAR_SITE_STATUS_CLOSED")]
	GodWarSiteStatusClosed
}
