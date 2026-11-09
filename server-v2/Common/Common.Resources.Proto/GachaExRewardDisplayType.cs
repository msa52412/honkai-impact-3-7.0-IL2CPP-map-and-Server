using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GachaExRewardDisplayType
{
	[ProtoEnum(Name = "GACHA_EX_REWARD_DISPLAY_NONE")]
	GachaExRewardDisplayNone,
	[ProtoEnum(Name = "GACHA_EX_REWARD_DISPLAY_BRIEF")]
	GachaExRewardDisplayBrief,
	[ProtoEnum(Name = "GACHA_EX_REWARD_DISPLAY_DETAIL")]
	GachaExRewardDisplayDetail,
	[ProtoEnum(Name = "GACHA_EX_REWARD_DISPLAY_SHINING")]
	GachaExRewardDisplayShining
}
