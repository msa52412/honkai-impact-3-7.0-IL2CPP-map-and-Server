using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatworldFishHardLevel
{
	[ProtoEnum(Name = "CHATWORLD_FISH_HARD_LEVEL_NONE")]
	ChatworldFishHardLevelNone,
	[ProtoEnum(Name = "CHATWORLD_FISH_HARD_LEVEL_EASY")]
	ChatworldFishHardLevelEasy,
	[ProtoEnum(Name = "CHATWORLD_FISH_HARD_LEVEL_NORMAL")]
	ChatworldFishHardLevelNormal,
	[ProtoEnum(Name = "CHATWORLD_FISH_HARD_LEVEL_HARD")]
	ChatworldFishHardLevelHard
}
