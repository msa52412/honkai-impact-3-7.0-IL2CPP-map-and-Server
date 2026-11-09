using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DressType
{
	[ProtoEnum(Name = "DRESS_BATTLE")]
	DressBattle = 1,
	[ProtoEnum(Name = "DRESS_SUIT")]
	DressSuit = 2,
	[ProtoEnum(Name = "DRESS_ARTIFACT")]
	DressArtifact = 4
}
