using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ArmadaManageNotifyType
{
	[ProtoEnum(Name = "ARMADA_MANAGE_NOTIFY_LEADER")]
	ArmadaManageNotifyLeader = 1,
	[ProtoEnum(Name = "ARMADA_MANAGE_NOTIFY_VICE_LEADER_SET")]
	ArmadaManageNotifyViceLeaderSet,
	[ProtoEnum(Name = "ARMADA_MANAGE_NOTIFY_VICE_LEADER_CANCEL")]
	ArmadaManageNotifyViceLeaderCancel
}
