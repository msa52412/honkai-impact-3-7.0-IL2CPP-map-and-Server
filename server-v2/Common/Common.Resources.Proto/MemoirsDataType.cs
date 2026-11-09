using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MemoirsDataType
{
	[ProtoEnum(Name = "MEMOIRS_DATA_TYPE_UINT")]
	MemoirsDataTypeUint = 1,
	[ProtoEnum(Name = "MEMOIRS_DATA_TYPE_TIMESTAMP")]
	MemoirsDataTypeTimestamp,
	[ProtoEnum(Name = "MEMOIRS_DATA_TYPE_AVATAR_TEAM")]
	MemoirsDataTypeAvatarTeam,
	[ProtoEnum(Name = "MEMOIRS_DATA_TYPE_BOOL")]
	MemoirsDataTypeBool
}
