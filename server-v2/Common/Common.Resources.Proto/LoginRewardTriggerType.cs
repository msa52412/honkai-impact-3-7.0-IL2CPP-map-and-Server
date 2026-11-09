using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum LoginRewardTriggerType
{
	[ProtoEnum(Name = "LOGIN_REWARD_TRIGGER_TYPE_NONE")]
	LoginRewardTriggerTypeNone,
	[ProtoEnum(Name = "LOGIN_REWARD_TRIGGER_TYPE_ONCE")]
	LoginRewardTriggerTypeOnce,
	[ProtoEnum(Name = "LOGIN_REWARD_TRIGGER_TYPE_ALWAYS")]
	LoginRewardTriggerTypeAlways
}
