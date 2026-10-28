using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PanelActivityType
{
	[ProtoEnum(Name = "PANEL_ACTIVITY_THEME")]
	PanelActivityTheme = 2,
	[ProtoEnum(Name = "PANEL_ACTIVITY_RECLAIM")]
	PanelActivityReclaim = 4,
	[ProtoEnum(Name = "PANEL_ACTIVITY_KINGDOM_WAR")]
	PanelActivityKingdomWar = 5,
	[ProtoEnum(Name = "PANEL_ACTIVITY_BATTLE_PASS")]
	PanelActivityBattlePass = 6,
	[ProtoEnum(Name = "PANEL_ACTIVITY_GLOBAL_WAR")]
	PanelActivityGlobalWar = 7,
	[ProtoEnum(Name = "PANEL_ACTIVITY_SLG")]
	PanelActivitySlg = 8,
	[ProtoEnum(Name = "PANEL_ACTIVITY_GLOBAL_POLLING")]
	PanelActivityGlobalPolling = 9,
	[ProtoEnum(Name = "PANEL_ACTIVITY_COLLABORATION")]
	PanelActivityCollaboration = 10
}
