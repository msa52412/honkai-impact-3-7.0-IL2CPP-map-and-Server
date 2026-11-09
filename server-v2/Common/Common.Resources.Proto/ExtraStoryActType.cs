using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ExtraStoryActType
{
	[ProtoEnum(Name = "EXTRA_STORY_ACT_MAIN")]
	ExtraStoryActMain = 1,
	[ProtoEnum(Name = "EXTRA_STORY_ACT_HIDDEN")]
	ExtraStoryActHidden,
	[ProtoEnum(Name = "EXTRA_STORY_ACT_ENTROPY")]
	ExtraStoryActEntropy,
	[ProtoEnum(Name = "EXTRA_STORY_ACT_ACTIVITY")]
	ExtraStoryActActivity,
	[ProtoEnum(Name = "EXTRA_STORY_ACT_FANTASY")]
	ExtraStoryActFantasy,
	[ProtoEnum(Name = "EXTRA_STORY_ACT_ROGUE")]
	ExtraStoryActRogue
}
