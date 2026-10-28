using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AvatarAttrType
{
	[ProtoEnum(Name = "AVATAR_HP")]
	AvatarHp = 1,
	[ProtoEnum(Name = "AVATAR_SP")]
	AvatarSp,
	[ProtoEnum(Name = "AVATAR_ATTACK")]
	AvatarAttack,
	[ProtoEnum(Name = "AVATAR_DEFENSE")]
	AvatarDefense,
	[ProtoEnum(Name = "AVATAR_CRITICAL")]
	AvatarCritical
}
