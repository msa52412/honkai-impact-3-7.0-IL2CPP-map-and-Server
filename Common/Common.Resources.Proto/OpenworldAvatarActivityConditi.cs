using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldAvatarActivityConditionType
{
	[ProtoEnum(Name = "OPENWORLD_AVATAR_ACTIVITY_CONDITION_NONE")]
	OpenworldAvatarActivityConditionNone,
	[ProtoEnum(Name = "OPENWORLD_AVATAR_ACTIVITY_CONDITION_LEVEL")]
	OpenworldAvatarActivityConditionLevel,
	[ProtoEnum(Name = "OPENWORLD_AVATAR_ACTIVITY_CONDITION_TALENT")]
	OpenworldAvatarActivityConditionTalent,
	[ProtoEnum(Name = "OPENWORLD_AVATAR_ACTIVITY_CONDITION_STORY")]
	OpenworldAvatarActivityConditionStory,
	[ProtoEnum(Name = "OPENWORLD_AVATAR_ACTIVITY_CONDITION_EVENT")]
	OpenworldAvatarActivityConditionEvent,
	[ProtoEnum(Name = "OPENWORLD_AVATAR_ACTIVITY_CONDITION_QUEST")]
	OpenworldAvatarActivityConditionQuest,
	[ProtoEnum(Name = "OPENWORLD_AVATAR_ACTIVITY_CONDITION_CULTIVATE")]
	OpenworldAvatarActivityConditionCultivate
}
