using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TimeLimitAddUpType
{
	[ProtoEnum(Name = "ADD_UP_VIP_POINT_ALL")]
	AddUpVipPointAll = 1,
	[ProtoEnum(Name = "ADD_UP_HCOIN_COST")]
	AddUpHcoinCost,
	[ProtoEnum(Name = "ADD_UP_SCOIN_COST")]
	AddUpScoinCost,
	[ProtoEnum(Name = "ADD_UP_STAMINA_COST")]
	AddUpStaminaCost,
	[ProtoEnum(Name = "ADD_UP_HCOIN_GACHA")]
	AddUpHcoinGacha,
	[ProtoEnum(Name = "ADD_UP_EXP")]
	AddUpExp,
	[ProtoEnum(Name = "ADD_UP_VIP_POINT")]
	AddUpVipPoint
}
