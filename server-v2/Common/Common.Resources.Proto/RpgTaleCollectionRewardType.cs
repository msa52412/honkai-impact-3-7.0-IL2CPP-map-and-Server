using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RpgTaleCollectionRewardType
{
	[ProtoEnum(Name = "RPG_TALE_COLLECTION_REWARD_NONE")]
	RpgTaleCollectionRewardNone,
	[ProtoEnum(Name = "RPG_TALE_COLLECTION_REWARD_QAVATAR_STAR")]
	RpgTaleCollectionRewardQavatarStar,
	[ProtoEnum(Name = "RPG_TALE_COLLECTION_REWARD_EVENT")]
	RpgTaleCollectionRewardEvent,
	[ProtoEnum(Name = "RPG_TALE_COLLECTION_REWARD_SHOP")]
	RpgTaleCollectionRewardShop,
	[ProtoEnum(Name = "RPG_TALE_COLLECTION_REWARD_TOTAL_STAGE_SCORE")]
	RpgTaleCollectionRewardTotalStageScore,
	[ProtoEnum(Name = "RPG_TALE_COLLECTION_REWARD_STAGE_CHALLENGE_NUM")]
	RpgTaleCollectionRewardStageChallengeNum,
	[ProtoEnum(Name = "RPG_TALE_COLLECTION_REWARD_LINKED_ACTIVITY_SCORE")]
	RpgTaleCollectionRewardLinkedActivityScore,
	[ProtoEnum(Name = "RPG_TALE_COLLECTION_REWARD_STAGE_SCORE")]
	RpgTaleCollectionRewardStageScore
}
