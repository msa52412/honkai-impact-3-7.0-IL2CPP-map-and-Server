using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldQuestRewardType
{
	[ProtoEnum(Name = "OW_QUEST_REWARD_A_AVATAR_FRAGMENT")]
	OwQuestRewardAAvatarFragment = 1,
	[ProtoEnum(Name = "OW_QUEST_REWARD_SP_AVATAR_FRAGMENT")]
	OwQuestRewardSpAvatarFragment,
	[ProtoEnum(Name = "OW_QUEST_REWARD_PURPLEJADE")]
	OwQuestRewardPurplejade,
	[ProtoEnum(Name = "OW_QUEST_REWARD_NORMAL")]
	OwQuestRewardNormal,
	[ProtoEnum(Name = "OW_QUEST_REWARD_ARTIFACT")]
	OwQuestRewardArtifact
}
