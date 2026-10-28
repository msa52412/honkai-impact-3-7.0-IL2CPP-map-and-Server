using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ArmadaImpeachOperation
{
	[ProtoEnum(Name = "START_IMPEACH")]
	StartImpeach = 1,
	[ProtoEnum(Name = "END_IMPEACH")]
	EndImpeach,
	[ProtoEnum(Name = "CANCEL_IMPEACH")]
	CancelImpeach
}
