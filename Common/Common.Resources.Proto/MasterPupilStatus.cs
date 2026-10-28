using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MasterPupilStatus
{
	[ProtoEnum(Name = "MASTER_PUPIL_STATUS_NONE")]
	MasterPupilStatusNone,
	[ProtoEnum(Name = "MASTER_PUPIL_STATUS_DURING")]
	MasterPupilStatusDuring,
	[ProtoEnum(Name = "MASTER_PUPIL_STATUS_FINISH")]
	MasterPupilStatusFinish,
	[ProtoEnum(Name = "MASTER_PUPIL_STATUS_DISMISS")]
	MasterPupilStatusDismiss
}
