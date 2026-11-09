using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GachaAdventureConditionType
{
	[ProtoEnum(Name = "GACHA_ADVENTURE_CONDITION_NONE")]
	GachaAdventureConditionNone,
	[ProtoEnum(Name = "GACHA_ADVENTURE_CONDITION_CONDITION_ID")]
	GachaAdventureConditionConditionId,
	[ProtoEnum(Name = "GACHA_ADVENTURE_CONDITION_ITEM_ID")]
	GachaAdventureConditionItemId
}
