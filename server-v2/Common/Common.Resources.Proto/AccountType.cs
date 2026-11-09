using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AccountType
{
	[ProtoEnum(Name = "ACCOUNT_NONE")]
	AccountNone,
	[ProtoEnum(Name = "ACCOUNT_MIHOYO")]
	AccountMihoyo,
	[ProtoEnum(Name = "ACCOUNT_BILIBILI")]
	AccountBilibili,
	[ProtoEnum(Name = "ACCOUNT_UC")]
	AccountUc,
	[ProtoEnum(Name = "ACCOUNT_360")]
	Account360,
	[ProtoEnum(Name = "ACCOUNT_YYB")]
	AccountYyb,
	[ProtoEnum(Name = "ACCOUNT_XIAOMI")]
	AccountXiaomi,
	[ProtoEnum(Name = "ACCOUNT_HUAWEI")]
	AccountHuawei,
	[ProtoEnum(Name = "ACCOUNT_OPPO")]
	AccountOppo,
	[ProtoEnum(Name = "ACCOUNT_VIVO")]
	AccountVivo,
	[ProtoEnum(Name = "ACCOUNT_MEIZU")]
	AccountMeizu,
	[ProtoEnum(Name = "ACCOUNT_COOLPAD")]
	AccountCoolpad,
	[ProtoEnum(Name = "ACCOUNT_LENOVO")]
	AccountLenovo,
	[ProtoEnum(Name = "ACCOUNT_GIONEE")]
	AccountGionee,
	[ProtoEnum(Name = "ACCOUNT_BAIDU")]
	AccountBaidu,
	[ProtoEnum(Name = "ACCOUNT_WANDOUJIA")]
	AccountWandoujia,
	[ProtoEnum(Name = "ACCOUNT_LESHI")]
	AccountLeshi,
	[ProtoEnum(Name = "ACCOUNT_GOOGLE")]
	AccountGoogle,
	[ProtoEnum(Name = "ACCOUNT_FACEBOOK")]
	AccountFacebook,
	[ProtoEnum(Name = "ACCOUNT_ITUNES_GC")]
	AccountItunesGc,
	[ProtoEnum(Name = "ACCOUNT_SIGNIN_APPLE")]
	AccountSigninApple,
	[ProtoEnum(Name = "ACCOUNT_WEGAME")]
	AccountWegame,
	[ProtoEnum(Name = "ACCOUNT_BYTEDANCE")]
	AccountBytedance,
	[ProtoEnum(Name = "ACCOUNT_STEAM")]
	AccountSteam,
	[ProtoEnum(Name = "ACCOUNT_HOYOLAB")]
	AccountHoyolab
}
