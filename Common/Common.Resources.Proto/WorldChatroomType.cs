using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum WorldChatroomType
{
	[ProtoEnum(Name = "WORLD_CHATROOM_TYPE_NONE")]
	WorldChatroomTypeNone,
	[ProtoEnum(Name = "WORLD_CHATROOM_TYPE_NORMAL")]
	WorldChatroomTypeNormal,
	[ProtoEnum(Name = "WORLD_CHATROOM_TYPE_ACTIVITY")]
	WorldChatroomTypeActivity
}
