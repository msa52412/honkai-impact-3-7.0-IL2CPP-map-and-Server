using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AvatarRecommendStyle
{
	[ProtoEnum(Name = "AVATAR_RECOMMEND_STYLE_NONE")]
	AvatarRecommendStyleNone,
	[ProtoEnum(Name = "AVATAR_RECOMMEND_STYLE_HOT")]
	AvatarRecommendStyleHot,
	[ProtoEnum(Name = "AVATAR_RECOMMEND_STYLE_CHOICE")]
	AvatarRecommendStyleChoice,
	[ProtoEnum(Name = "AVATAR_RECOMMEND_STYLE_SINGLE_AVATAR")]
	AvatarRecommendStyleSingleAvatar
}
