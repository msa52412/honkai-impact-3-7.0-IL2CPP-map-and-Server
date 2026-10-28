using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DiceyDungeonType
{
	[ProtoEnum(Name = "DICEY_DUNGEON_NONE")]
	DiceyDungeonNone,
	[ProtoEnum(Name = "DICEY_DUNGEON_STORY")]
	DiceyDungeonStory,
	[ProtoEnum(Name = "DICEY_DUNGEON_SCHEDULE")]
	DiceyDungeonSchedule,
	[ProtoEnum(Name = "DICEY_DUNGEON_ENDLESS")]
	DiceyDungeonEndless
}
