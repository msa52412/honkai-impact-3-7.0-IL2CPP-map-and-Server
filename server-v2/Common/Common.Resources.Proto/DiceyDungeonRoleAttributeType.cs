using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DiceyDungeonRoleAttributeType
{
	[ProtoEnum(Name = "DICEY_DUNGEON_ROLE_ATTR_STRENGTH")]
	DiceyDungeonRoleAttrStrength = 1,
	[ProtoEnum(Name = "DICEY_DUNGEON_ROLE_ATTR_AGILITY")]
	DiceyDungeonRoleAttrAgility,
	[ProtoEnum(Name = "DICEY_DUNGEON_ROLE_ATTR_INTELLIGENCE")]
	DiceyDungeonRoleAttrIntelligence
}
