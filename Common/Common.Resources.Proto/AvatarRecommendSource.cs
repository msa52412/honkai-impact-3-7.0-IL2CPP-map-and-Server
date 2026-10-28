using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AvatarRecommendSource
{
	[ProtoEnum(Name = "AVATAR_RECOMMEND_SOURCE_NONE")]
	AvatarRecommendSourceNone,
	[ProtoEnum(Name = "AVATAR_RECOMMEND_SOURCE_DB")]
	AvatarRecommendSourceDb,
	[ProtoEnum(Name = "AVATAR_RECOMMEND_SOURCE_DESIGN_DATA")]
	AvatarRecommendSourceDesignData
}
