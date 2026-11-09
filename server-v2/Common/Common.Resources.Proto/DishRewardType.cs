using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DishRewardType
{
	[ProtoEnum(Name = "DISH_REWARD_TYPE_NONE")]
	DishRewardTypeNone,
	[ProtoEnum(Name = "DISH_REWARD_TYPE_PREPARE")]
	DishRewardTypePrepare,
	[ProtoEnum(Name = "DISH_REWARD_TYPE_DAILY")]
	DishRewardTypeDaily,
	[ProtoEnum(Name = "DISH_REWARD_TYPE_LIMITED")]
	DishRewardTypeLimited
}
