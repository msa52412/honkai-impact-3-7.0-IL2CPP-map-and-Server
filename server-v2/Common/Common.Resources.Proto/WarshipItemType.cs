using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum WarshipItemType
{
	[ProtoEnum(Name = "WARSHIP_ITEM_TYPE_NONE")]
	WarshipItemTypeNone,
	[ProtoEnum(Name = "WARSHIP_ITEM_TYPE_WARSHIP")]
	WarshipItemTypeWarship,
	[ProtoEnum(Name = "WARSHIP_ITEM_TYPE_BGM")]
	WarshipItemTypeBgm
}
