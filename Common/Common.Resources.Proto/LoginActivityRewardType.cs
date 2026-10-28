using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum LoginActivityRewardType
{
	[ProtoEnum(Name = "LOGIN_ACTIVITY_REWARD_NORMAL")]
	LoginActivityRewardNormal,
	[ProtoEnum(Name = "LOGIN_ACTIVITY_REWARD_BONUS")]
	LoginActivityRewardBonus
}
