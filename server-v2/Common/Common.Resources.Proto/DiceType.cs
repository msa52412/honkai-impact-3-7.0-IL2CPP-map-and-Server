using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DiceType
{
	[ProtoEnum(Name = "DICE_TYPE_NONE")]
	DiceTypeNone,
	[ProtoEnum(Name = "DICE_TYPE_RANDOM")]
	DiceTypeRandom,
	[ProtoEnum(Name = "DICE_TYPE_FIXED")]
	DiceTypeFixed,
	[ProtoEnum(Name = "DICE_TYPE_FORWARD")]
	DiceTypeForward
}
