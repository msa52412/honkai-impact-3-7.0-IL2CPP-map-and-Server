using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MonopolyDiceType
{
	[ProtoEnum(Name = "MONOPOLY_DICE_NONE")]
	MonopolyDiceNone,
	[ProtoEnum(Name = "MONOPOLY_DICE_NORMAL")]
	MonopolyDiceNormal,
	[ProtoEnum(Name = "MONOPOLY_DICE_BUILDING_PRIORITY")]
	MonopolyDiceBuildingPriority
}
