using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum NinjaSiteType
{
	[ProtoEnum(Name = "NINJA_SITE_NORMAL")]
	NinjaSiteNormal = 1,
	[ProtoEnum(Name = "NINJA_SITE_BOSS")]
	NinjaSiteBoss,
	[ProtoEnum(Name = "NINJA_SITE_CHALLENGE")]
	NinjaSiteChallenge
}
