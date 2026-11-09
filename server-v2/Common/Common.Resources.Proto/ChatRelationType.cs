using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatRelationType
{
	[ProtoEnum(Name = "CHAT_RELATION_TYPE_NONE")]
	ChatRelationTypeNone,
	[ProtoEnum(Name = "CHAT_RELATION_TYPE_FRIEND")]
	ChatRelationTypeFriend,
	[ProtoEnum(Name = "CHAT_RELATION_TYPE_MASTER_PUPIL")]
	ChatRelationTypeMasterPupil
}
