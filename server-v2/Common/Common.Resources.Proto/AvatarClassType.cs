using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AvatarClassType
{
	[ProtoEnum(Name = "AVATAR_KIANA")]
	AvatarKiana = 1,
	[ProtoEnum(Name = "AVATAR_MEI")]
	AvatarMei,
	[ProtoEnum(Name = "AVATAR_BRONYA")]
	AvatarBronya,
	[ProtoEnum(Name = "AVATAR_HIMEKO")]
	AvatarHimeko,
	[ProtoEnum(Name = "AVATAR_THERESA")]
	AvatarTheresa,
	[ProtoEnum(Name = "AVATAR_FUKA")]
	AvatarFuka,
	[ProtoEnum(Name = "AVATAR_RITA")]
	AvatarRita,
	[ProtoEnum(Name = "AVATAR_DURANDAL")]
	AvatarDurandal,
	[ProtoEnum(Name = "AVATAR_FISCHL")]
	AvatarFischl,
	[ProtoEnum(Name = "AVATAR_PARDOFELIS")]
	AvatarPardofelis,
	[ProtoEnum(Name = "AVATAR_FEATHEREDRABBIT")]
	AvatarFeatheredrabbit
}
