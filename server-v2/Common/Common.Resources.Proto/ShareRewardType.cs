using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ShareRewardType
{
	[ProtoEnum(Name = "SHARE_REWARD_TYPE_NONE")]
	ShareRewardTypeNone,
	[ProtoEnum(Name = "SHARE_REWARD_TYPE_ONCE")]
	ShareRewardTypeOnce,
	[ProtoEnum(Name = "SHARE_REWARD_TYPE_DAILY")]
	ShareRewardTypeDaily
}
