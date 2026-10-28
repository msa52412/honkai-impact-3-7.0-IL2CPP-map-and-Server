using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarBuffQualityType
{
	[ProtoEnum(Name = "GOD_WAR_BUFF_QUALITY_BASE")]
	GodWarBuffQualityBase = 1,
	[ProtoEnum(Name = "GOD_WAR_BUFF_QUALITY_CORE_ARTIFACT")]
	GodWarBuffQualityCoreArtifact,
	[ProtoEnum(Name = "GOD_WAR_BUFF_QUALITY_CORE")]
	GodWarBuffQualityCore,
	[ProtoEnum(Name = "GOD_WAR_BUFF_QUALITY_AVATAR_SPECIAL")]
	GodWarBuffQualityAvatarSpecial
}
