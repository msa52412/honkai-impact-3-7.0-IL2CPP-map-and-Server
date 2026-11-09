using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PvzSiteType
{
	[ProtoEnum(Name = "PVZ_SITE_NORMAL")]
	PvzSiteNormal = 1,
	[ProtoEnum(Name = "PVZ_SITE_DAILY")]
	PvzSiteDaily,
	[ProtoEnum(Name = "PVZ_SITE_CHALLENGE")]
	PvzSiteChallenge
}
