using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarEventRandomType
{
	[ProtoEnum(Name = "GOD_WAR_EVENT_RANDOM_STAGE")]
	GodWarEventRandomStage = 1,
	[ProtoEnum(Name = "GOD_WAR_EVENT_RANDOM_PLOT")]
	GodWarEventRandomPlot,
	[ProtoEnum(Name = "GOD_WAR_EVENT_RANDOM_REWARD")]
	GodWarEventRandomReward,
	[ProtoEnum(Name = "GOD_WAR_EVENT_RANDOM_BUFF")]
	GodWarEventRandomBuff,
	[ProtoEnum(Name = "GOD_WAR_EVENT_RANDOM_TELEPORT")]
	GodWarEventRandomTeleport,
	[ProtoEnum(Name = "GOD_WAR_EVENT_RANDOM_LOBBY")]
	GodWarEventRandomLobby,
	[ProtoEnum(Name = "GOD_WAR_EVENT_RANDOM_MATERIAL")]
	GodWarEventRandomMaterial,
	[ProtoEnum(Name = "GOD_WAR_EVENT_RANDOM_TRIGGER")]
	GodWarEventRandomTrigger
}
