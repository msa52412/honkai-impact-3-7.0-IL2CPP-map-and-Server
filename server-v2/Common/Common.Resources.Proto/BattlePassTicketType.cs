using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum BattlePassTicketType
{
	[ProtoEnum(Name = "BATTLE_PASS_TICKET_NONE")]
	BattlePassTicketNone,
	[ProtoEnum(Name = "BATTLE_PASS_TICKET_NORMAL")]
	BattlePassTicketNormal,
	[ProtoEnum(Name = "BATTLE_PASS_TICKET_ADVANCED")]
	BattlePassTicketAdvanced,
	[ProtoEnum(Name = "BATTLE_PASS_TICKET_LUXURY")]
	BattlePassTicketLuxury
}
