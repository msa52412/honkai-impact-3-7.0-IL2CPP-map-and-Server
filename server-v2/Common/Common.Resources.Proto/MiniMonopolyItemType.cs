using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MiniMonopolyItemType
{
	[ProtoEnum(Name = "MINI_MONOPOLY_ITEM_TYPE_NONE")]
	MiniMonopolyItemTypeNone,
	[ProtoEnum(Name = "MINI_MONOPOLY_ITEM_CONTROLLABLE_DICE")]
	MiniMonopolyItemControllableDice
}
