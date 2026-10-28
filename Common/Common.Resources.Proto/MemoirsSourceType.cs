using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MemoirsSourceType
{
	[ProtoEnum(Name = "MEMOIRS_SOURCE_TYPE_DB")]
	MemoirsSourceTypeDb = 1,
	[ProtoEnum(Name = "MEMOIRS_SOURCE_TYPE_BIN")]
	MemoirsSourceTypeBin
}
