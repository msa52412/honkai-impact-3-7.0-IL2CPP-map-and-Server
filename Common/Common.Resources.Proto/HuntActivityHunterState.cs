using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum HuntActivityHunterState
{
	[ProtoEnum(Name = "HUNTER_STATE_NONE")]
	HunterStateNone,
	[ProtoEnum(Name = "HUNTER_STATE_PARTOL")]
	HunterStatePartol,
	[ProtoEnum(Name = "HUNTER_STATE_CHASE")]
	HunterStateChase,
	[ProtoEnum(Name = "HUNTER_STATE_BATTLE")]
	HunterStateBattle,
	[ProtoEnum(Name = "HUNTER_STATE_DEAD")]
	HunterStateDead
}
