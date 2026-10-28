using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MatchThreeRoomType
{
	[ProtoEnum(Name = "MATCH_THREE_ROOM_NONE")]
	MatchThreeRoomNone,
	[ProtoEnum(Name = "MATCH_THREE_ROOM_MAIN")]
	MatchThreeRoomMain,
	[ProtoEnum(Name = "MATCH_THREE_ROOM_SIDE")]
	MatchThreeRoomSide,
	[ProtoEnum(Name = "MATCH_THREE_ROOM_DAILY")]
	MatchThreeRoomDaily,
	[ProtoEnum(Name = "MATCH_THREE_ROOM_TOWER")]
	MatchThreeRoomTower
}
