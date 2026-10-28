using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AvatarElementTag
{
	[ProtoEnum(Name = "AVATAR_ELEMENT_TAG_FIRE")]
	AvatarElementTagFire = 1004,
	[ProtoEnum(Name = "AVATAR_ELEMENT_TAG_ICE")]
	AvatarElementTagIce,
	[ProtoEnum(Name = "AVATAR_ELEMENT_TAG_LIGHTNING")]
	AvatarElementTagLightning
}
