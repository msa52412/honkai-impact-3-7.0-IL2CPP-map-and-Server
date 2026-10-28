using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GlobalExploreActionType
{
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ACTION_NONE")]
	GlobalExploreActionNone,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ACTION_DICE")]
	GlobalExploreActionDice,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ACTION_EXCHANGE")]
	GlobalExploreActionExchange,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ACTION_BATTLE")]
	GlobalExploreActionBattle,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ACTION_TELEPORT")]
	GlobalExploreActionTeleport,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ACTION_AREA_TELEPORT")]
	GlobalExploreActionAreaTeleport,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ACTION_ACCEPT_QUEST")]
	GlobalExploreActionAcceptQuest,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ACTION_USE_HAMMER")]
	GlobalExploreActionUseHammer,
	[ProtoEnum(Name = "GLOBAL_EXPLORE_ACTION_USE_SPADE")]
	GlobalExploreActionUseSpade
}
