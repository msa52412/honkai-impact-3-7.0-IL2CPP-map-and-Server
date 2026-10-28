using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RpgTaleSiteStatus
{
	[ProtoEnum(Name = "RPG_TALE_SITE_STATUS_INVALID")]
	RpgTaleSiteStatusInvalid,
	[ProtoEnum(Name = "RPG_TALE_SITE_STATUS_HIDE")]
	RpgTaleSiteStatusHide,
	[ProtoEnum(Name = "RPG_TALE_SITE_STATUS_SHOW")]
	RpgTaleSiteStatusShow,
	[ProtoEnum(Name = "RPG_TALE_SITE_STATUS_CONTENT_UNLOCKED")]
	RpgTaleSiteStatusContentUnlocked,
	[ProtoEnum(Name = "RPG_TALE_SITE_STATUS_PASSED")]
	RpgTaleSiteStatusPassed,
	[ProtoEnum(Name = "RPG_TALE_SITE_STATUS_CLOSED")]
	RpgTaleSiteStatusClosed,
	[ProtoEnum(Name = "RPG_TALE_SITE_STATUS_CD")]
	RpgTaleSiteStatusCd,
	[ProtoEnum(Name = "RPG_TALE_SITE_STATUS_DISCARD")]
	RpgTaleSiteStatusDiscard
}
