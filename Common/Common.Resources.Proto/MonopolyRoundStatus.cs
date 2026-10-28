using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum MonopolyRoundStatus
{
	[ProtoEnum(Name = "MONOPOLY_ROUND_STATUS_NONE")]
	MonopolyRoundStatusNone,
	[ProtoEnum(Name = "MONOPOLY_ROUND_STATUS_START")]
	MonopolyRoundStatusStart,
	[ProtoEnum(Name = "MONOPOLY_ROUND_STATUS_AFTER_DICE")]
	MonopolyRoundStatusAfterDice,
	[ProtoEnum(Name = "MONOPOLY_ROUND_STATUS_AFTER_CONSTRUCT")]
	MonopolyRoundStatusAfterConstruct
}
