using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DormDataType
{
	[ProtoEnum(Name = "DORM_DATA_ALL")]
	DormDataAll = 0,
	[ProtoEnum(Name = "DORM_DATA_DECORATION")]
	DormDataDecoration = 1,
	[ProtoEnum(Name = "DORM_DATA_EVENT")]
	DormDataEvent = 3,
	[ProtoEnum(Name = "DORM_DATA_SNS")]
	DormDataSns = 4
}
