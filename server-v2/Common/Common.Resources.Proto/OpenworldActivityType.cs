using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldActivityType
{
	[ProtoEnum(Name = "OPENWORLD_BOSS_HUNT_ACTIVITY")]
	OpenworldBossHuntActivity = 1
}
