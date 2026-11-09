using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ArmadaPlayerNotifyType
{
	[ProtoEnum(Name = "ARMADA_PLAYER_NOTIFY_JOIN")]
	ArmadaPlayerNotifyJoin = 1,
	[ProtoEnum(Name = "ARMADA_PLAYER_NOTIFY_QUIT")]
	ArmadaPlayerNotifyQuit,
	[ProtoEnum(Name = "ARMADA_PLAYER_NOTIFY_KICK")]
	ArmadaPlayerNotifyKick,
	[ProtoEnum(Name = "ARMADA_PLAYER_NOTIFY_DISSOLVE")]
	ArmadaPlayerNotifyDissolve,
	[ProtoEnum(Name = "ARMADA_PLAYER_NOTIFY_CHANGE_NAME")]
	ArmadaPlayerNotifyChangeName
}
