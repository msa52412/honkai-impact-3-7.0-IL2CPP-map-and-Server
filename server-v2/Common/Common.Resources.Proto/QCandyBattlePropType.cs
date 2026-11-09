using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QCandyBattlePropType
{
	[ProtoEnum(Name = "QCANDY_BATTLE_PROP_TYPE_NONE")]
	QcandyBattlePropTypeNone,
	[ProtoEnum(Name = "QCANDY_BATTLE_PROP_TYPE_HAS_CD")]
	QcandyBattlePropTypeHasCd,
	[ProtoEnum(Name = "QCANDY_BATTLE_PROP_TYPE_AUTO_RECOVER")]
	QcandyBattlePropTypeAutoRecover
}
