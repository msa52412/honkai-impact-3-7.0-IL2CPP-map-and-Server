using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum FishRewardSrc
{
	[ProtoEnum(Name = "FISH_REWARD_SRC_NONE")]
	FishRewardSrcNone,
	[ProtoEnum(Name = "FISH_REWARD_SRC_NORMAL")]
	FishRewardSrcNormal,
	[ProtoEnum(Name = "FISH_REWARD_SRC_GET_MISSED")]
	FishRewardSrcGetMissed,
	[ProtoEnum(Name = "FISH_REWARD_SRC_ASSIST")]
	FishRewardSrcAssist,
	[ProtoEnum(Name = "FISH_REWARD_SRC_LIKE")]
	FishRewardSrcLike
}
