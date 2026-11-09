using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RpgTaleSiteContentType
{
	[ProtoEnum(Name = "RPG_TALE_SITE_CONTENT_INVALID")]
	RpgTaleSiteContentInvalid,
	[ProtoEnum(Name = "RPG_TALE_SITE_CONTENT_NONE")]
	RpgTaleSiteContentNone,
	[ProtoEnum(Name = "RPG_TALE_SITE_CONTENT_MATRIX")]
	RpgTaleSiteContentMatrix,
	[ProtoEnum(Name = "RPG_TALE_SITE_CONTENT_PLOT")]
	RpgTaleSiteContentPlot,
	[ProtoEnum(Name = "RPG_TALE_SITE_CONTENT_TIPS")]
	RpgTaleSiteContentTips,
	[ProtoEnum(Name = "RPG_TALE_SITE_CONTENT_LINK")]
	RpgTaleSiteContentLink
}
