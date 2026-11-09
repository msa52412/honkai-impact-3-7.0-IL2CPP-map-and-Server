using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum RaffleActivityType
{
	[ProtoEnum(Name = "RAFFLE_ACTIVITY_NONE")]
	RaffleActivityNone,
	[ProtoEnum(Name = "RAFFLE_ACTIVITY_MONOPOLY")]
	RaffleActivityMonopoly
}
