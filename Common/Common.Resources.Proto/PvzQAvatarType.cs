using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PvzQAvatarType
{
	[ProtoEnum(Name = "PVZ_QAVATAR_ATTACK")]
	PvzQavatarAttack = 1,
	[ProtoEnum(Name = "PVZ_QAVATAR_ASSIST")]
	PvzQavatarAssist
}
