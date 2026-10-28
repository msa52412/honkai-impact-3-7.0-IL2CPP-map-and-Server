using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PrivilegeRightType
{
	[ProtoEnum(Name = "PRIVILEGE_RIGHT_DAILY_FREE_GACHA")]
	PrivilegeRightDailyFreeGacha = 1,
	[ProtoEnum(Name = "PRIVILEGE_RIGHT_CARD_DAILY_REWARD")]
	PrivilegeRightCardDailyReward,
	[ProtoEnum(Name = "PRIVILEGE_RIGHT_DAILY_ONE_FREE_GACHA")]
	PrivilegeRightDailyOneFreeGacha,
	[ProtoEnum(Name = "PRIVILEGE_RIGHT_DAILY_TEN_FREE_GACHA")]
	PrivilegeRightDailyTenFreeGacha
}
