using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ArmadaPlayerStatus
{
	[ProtoEnum(Name = "ARMADA_PLAYER_NOT_JOIN")]
	ArmadaPlayerNotJoin = 1,
	[ProtoEnum(Name = "ARMADA_PLAYER_APPLY")]
	ArmadaPlayerApply,
	[ProtoEnum(Name = "ARMADA_PLAYER_CHOOSE_CONTACT")]
	ArmadaPlayerChooseContact,
	[ProtoEnum(Name = "ARMADA_PLAYER_JOINED")]
	ArmadaPlayerJoined
}
