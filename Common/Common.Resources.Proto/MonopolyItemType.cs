using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MonopolyItemType
{
	[ProtoEnum(Name = "MONOPOLY_ITEM_CONTROLABLE_DICE")]
	MonopolyItemControlableDice = 1,
	[ProtoEnum(Name = "MONOPOLY_ITEM_ROADBLOCK")]
	MonopolyItemRoadblock = 2,
	[ProtoEnum(Name = "MONOPOLY_ITEM_EXCHANGE_POSITION")]
	MonopolyItemExchangePosition = 4,
	[ProtoEnum(Name = "MONOPOLY_ITEM_LEVELUP_BUILDING")]
	MonopolyItemLevelupBuilding = 7,
	[ProtoEnum(Name = "MONOPOLY_ITEM_CHANGE_DIRECTION")]
	MonopolyItemChangeDirection = 8
}
