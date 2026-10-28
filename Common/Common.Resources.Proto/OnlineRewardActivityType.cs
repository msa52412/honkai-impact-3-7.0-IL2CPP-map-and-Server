using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OnlineRewardActivityType
{
	[ProtoEnum(Name = "ONLINE_REWARD_ACTIVITY_TYPE_DAILY")]
	OnlineRewardActivityTypeDaily = 1,
	[ProtoEnum(Name = "ONLINE_REWARD_ACTIVITY_TYPE_ACTIVITY")]
	OnlineRewardActivityTypeActivity
}
