using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum LoginRewardActivityType
{
	[ProtoEnum(Name = "LOGIN_REWARD_ACTIVITY_NONE")]
	LoginRewardActivityNone,
	[ProtoEnum(Name = "LOGIN_REWARD_ACTIVITY_NORMAL")]
	LoginRewardActivityNormal,
	[ProtoEnum(Name = "LOGIN_REWARD_ACTIVITY_NEWBIE")]
	LoginRewardActivityNewbie
}
