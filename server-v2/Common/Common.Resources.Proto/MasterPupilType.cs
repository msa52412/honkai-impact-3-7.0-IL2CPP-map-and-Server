using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MasterPupilType
{
	[ProtoEnum(Name = "MASTER_PUPIL_NONE_TYPE")]
	MasterPupilNoneType = 0,
	[ProtoEnum(Name = "MASTER_PUPIL_MASTER_TYPE")]
	MasterPupilMasterType = 1,
	[ProtoEnum(Name = "MASTER_PUPIL_PUPIL_TYPE")]
	MasterPupilPupilType = 2,
	[ProtoEnum(Name = "MASTER_PUPIL_NEITHER_ALL")]
	MasterPupilNeitherAll = 4
}
