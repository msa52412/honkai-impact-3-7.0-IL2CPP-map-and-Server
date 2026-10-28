using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum AddFriendAction
{
	[ProtoEnum(Name = "ADD_FRIEND_ACTION_ASK")]
	AddFriendActionAsk = 1,
	[ProtoEnum(Name = "ADD_FRIEND_ACTION_AGREE")]
	AddFriendActionAgree,
	[ProtoEnum(Name = "ADD_FRIEND_ACTION_REJECT")]
	AddFriendActionReject
}
