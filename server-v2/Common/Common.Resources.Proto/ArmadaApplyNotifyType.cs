using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ArmadaApplyNotifyType
{
	[ProtoEnum(Name = "ARMADA_APPLY_NOTIFY_NEW")]
	ArmadaApplyNotifyNew = 1,
	[ProtoEnum(Name = "ARMADA_APPLY_NOTIFY_APPROVE")]
	ArmadaApplyNotifyApprove,
	[ProtoEnum(Name = "ARMADA_APPLY_NOTIFY_REJECT")]
	ArmadaApplyNotifyReject
}
