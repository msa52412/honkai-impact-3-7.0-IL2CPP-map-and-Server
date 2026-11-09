using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum IslandVisitRecordType
{
	[ProtoEnum(Name = "ISLAND_RECORD_HELP_FRIEND")]
	IslandRecordHelpFriend = 1,
	[ProtoEnum(Name = "ISLAND_RECORD_BE_HELPED")]
	IslandRecordBeHelped,
	[ProtoEnum(Name = "ISLAND_RECORD_LIKE")]
	IslandRecordLike,
	[ProtoEnum(Name = "ISLAND_RECORD_BE_LIKED")]
	IslandRecordBeLiked
}
