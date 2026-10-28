using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum FeatureSwitchActivityType
{
	[ProtoEnum(Name = "FEATURE_SWITCH_ACTIVITY_TYPE_NONE")]
	FeatureSwitchActivityTypeNone,
	[ProtoEnum(Name = "FEATURE_SWITCH_ACTIVITY_TYPE_SLG_GAME")]
	FeatureSwitchActivityTypeSlgGame,
	[ProtoEnum(Name = "FEATURE_SWITCH_ACTIVITY_TYPE_SLG_BATTLE")]
	FeatureSwitchActivityTypeSlgBattle,
	[ProtoEnum(Name = "FEATURE_SWITCH_ACTIVITY_TYPE_SLG_MATCH")]
	FeatureSwitchActivityTypeSlgMatch,
	[ProtoEnum(Name = "FEATURE_SWITCH_ACTIVITY_TYPE_GLOBAL_POLLING")]
	FeatureSwitchActivityTypeGlobalPolling,
	[ProtoEnum(Name = "FEATURE_SWITCH_ACTIVITY_TYPE_SLG_CHAT")]
	FeatureSwitchActivityTypeSlgChat,
	[ProtoEnum(Name = "FEATURE_SWITCH_ACTIVITY_TYPE_SLG_BROADCAST")]
	FeatureSwitchActivityTypeSlgBroadcast,
	[ProtoEnum(Name = "FEATURE_SWITCH_ACTIVITY_TYPE_GLOBAL_EXPLORE_BROADCAST")]
	FeatureSwitchActivityTypeGlobalExploreBroadcast
}
