using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SnsShowPriviledgeState
{
	[ProtoEnum(Name = "SNS_SHOW_NONE")]
	SnsShowNone,
	[ProtoEnum(Name = "SNS_SHOW_OPEN_TO_PUBLIC")]
	SnsShowOpenToPublic,
	[ProtoEnum(Name = "SNS_SHOW_OPEN_TO_FRIENDS")]
	SnsShowOpenToFriends,
	[ProtoEnum(Name = "SNS_SHOW_OPEN_TO_SELF")]
	SnsShowOpenToSelf
}
