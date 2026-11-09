using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalExploreMoveType
{
	[ProtoEnum(Name = "GLOBAL_EXPLORE_MOVE_WALK")]
	GlobalExploreMoveWalk = 1,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_MOVE_TELEPORT")]
	GlobalExploreMoveTeleport,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_EVENT_TELEPORT")]
	GlobalExploreEventTeleport,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_GM_TELEPORT")]
	GlobalExploreGmTeleport
}
