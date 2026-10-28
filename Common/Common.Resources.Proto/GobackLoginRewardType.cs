using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GobackLoginRewardType
{
	[ProtoEnum(Name = "GOBACK_LOGIN_REWARD_COMMON")]
	GobackLoginRewardCommon = 1,
	[ProtoEnum(Name = "GOBACK_LOGIN_REWARD_BONUS")]
	GobackLoginRewardBonus
}
