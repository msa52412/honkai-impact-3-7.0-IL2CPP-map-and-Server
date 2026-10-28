using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ReclaimActivityDataType
{
	[ProtoEnum(Name = "RECLAIM_ACTIVITY_DATA_ALL")]
	ReclaimActivityDataAll,
	[ProtoEnum(Name = "RECLAIM_ACTIVITY_DATA_AVATAR")]
	ReclaimActivityDataAvatar,
	[ProtoEnum(Name = "RECLAIM_ACTIVITY_DATA_LEVEL")]
	ReclaimActivityDataLevel
}
