using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldQuestCreateWay
{
	[ProtoEnum(Name = "OPENWORLD_QUEST_CREATE_WAY_RARITY")]
	OpenworldQuestCreateWayRarity = 1,
	[ProtoEnum(Name = "OPENWORLD_QUEST_CREATE_WAY_MAP_LEVEL")]
	OpenworldQuestCreateWayMapLevel
}
