using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QCandyBattlePropState
{
	[ProtoEnum(Name = "QCANDY_BATTLE_PROP_CANNOT_TRIGGER")]
	QcandyBattlePropCannotTrigger,
	[ProtoEnum(Name = "QCANDY_BATTLE_PROP_CAN_TRIGGER")]
	QcandyBattlePropCanTrigger
}
