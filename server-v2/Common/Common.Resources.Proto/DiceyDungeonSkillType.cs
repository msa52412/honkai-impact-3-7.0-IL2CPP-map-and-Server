using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DiceyDungeonSkillType
{
	[ProtoEnum(Name = "DICEY_DUNGEON_SKILL_NONE")]
	DiceyDungeonSkillNone,
	[ProtoEnum(Name = "DICEY_DUNGEON_SKILL_INITIATIVE")]
	DiceyDungeonSkillInitiative,
	[ProtoEnum(Name = "DICEY_DUNGEON_SKILL_PASSIVE")]
	DiceyDungeonSkillPassive
}
