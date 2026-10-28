using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AvatarID
{
	[ProtoEnum(Name = "AVATAR_KIANA_PT")]
	AvatarKianaPt = 101,
	[ProtoEnum(Name = "AVATAR_KIANA_FX")]
	AvatarKianaFx = 102,
	[ProtoEnum(Name = "AVATAR_KIANA_YN")]
	AvatarKianaYn = 103,
	[ProtoEnum(Name = "AVATAR_KIANA_QN")]
	AvatarKianaQn = 104,
	[ProtoEnum(Name = "AVATAR_MEI_CK")]
	AvatarMeiCk = 201,
	[ProtoEnum(Name = "AVATAR_MEI_WS")]
	AvatarMeiWs = 202,
	[ProtoEnum(Name = "AVATAR_MEI_DH")]
	AvatarMeiDh = 203,
	[ProtoEnum(Name = "AVATAR_MEI_LD")]
	AvatarMeiLd = 204,
	[ProtoEnum(Name = "AVATAR_SAKURA_YS")]
	AvatarSakuraYs = 211,
	[ProtoEnum(Name = "AVATAR_SAKURA_ZF")]
	AvatarSakuraZf = 212,
	[ProtoEnum(Name = "AVATAR_BRONYA_ZP")]
	AvatarBronyaZp = 301,
	[ProtoEnum(Name = "AVATAR_BRONYA_BQ")]
	AvatarBronyaBq = 302,
	[ProtoEnum(Name = "AVATAR_BRONYA_JJ")]
	AvatarBronyaJj = 303,
	[ProtoEnum(Name = "AVATAR_BRONYA_BX")]
	AvatarBronyaBx = 304,
	[ProtoEnum(Name = "AVATAR_HIMEKO_VV")]
	AvatarHimekoVv = 401,
	[ProtoEnum(Name = "AVATAR_HIMEKO_NX")]
	AvatarHimekoNx = 402,
	[ProtoEnum(Name = "AVATAR_HIMEKO_KZ")]
	AvatarHimekoKz = 403,
	[ProtoEnum(Name = "AVATAR_HIMEKO_BZ")]
	AvatarHimekoBz = 404,
	[ProtoEnum(Name = "AVATAR_THERESA_YD")]
	AvatarTheresaYd = 501,
	[ProtoEnum(Name = "AVATAR_THERESA_TP")]
	AvatarTheresaTp = 502,
	[ProtoEnum(Name = "AVATAR_THERESA_FY")]
	AvatarTheresaFy = 503,
	[ProtoEnum(Name = "AVATAR_THERESA_JX")]
	AvatarTheresaJx = 504,
	[ProtoEnum(Name = "AVATAR_RITA_NP")]
	AvatarRitaNp = 701
}
