using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MasterPupilOpType
{
	[ProtoEnum(Name = "MASTER_PUPIL_OP_NONE")]
	MasterPupilOpNone,
	[ProtoEnum(Name = "MASTER_PUPIL_OP_AGREE")]
	MasterPupilOpAgree,
	[ProtoEnum(Name = "MASTER_PUPIL_OP_REJECT")]
	MasterPupilOpReject,
	[ProtoEnum(Name = "MASTER_PUPIL_OP_DISMISS")]
	MasterPupilOpDismiss
}
