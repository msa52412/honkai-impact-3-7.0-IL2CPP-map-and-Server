using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ActivityWorldChatroomType
{
	[ProtoEnum(Name = "ACTIVITY_WORLD_CHATROOM_TYPE_NONE")]
	ActivityWorldChatroomTypeNone,
	[ProtoEnum(Name = "ACTIVITY_WORLD_CHATROOM_TYPE_KINGDOM")]
	ActivityWorldChatroomTypeKingdom,
	[ProtoEnum(Name = "ACTIVITY_WORLD_CHATROOM_TYPE_GLOBAL_WAR")]
	ActivityWorldChatroomTypeGlobalWar,
	[ProtoEnum(Name = "ACTIVITY_WORLD_CHATROOM_TYPE_CHATWORLD_BEAST")]
	ActivityWorldChatroomTypeChatworldBeast,
	[ProtoEnum(Name = "ACTIVITY_WORLD_CHATROOM_TYPE_SLG")]
	ActivityWorldChatroomTypeSlg,
	[ProtoEnum(Name = "ACTIVITY_WORLD_CHATROOM_TYPE_GLOBAL_EXPLORE")]
	ActivityWorldChatroomTypeGlobalExplore
}
