using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarShapeType
{
	[ProtoEnum(Name = "QAVATAR_SHAPE_NONE")]
	QavatarShapeNone,
	[ProtoEnum(Name = "QAVATAR_SHAPE_AABB")]
	QavatarShapeAabb,
	[ProtoEnum(Name = "QAVATAR_SHAPE_CIRCLE")]
	QavatarShapeCircle,
	[ProtoEnum(Name = "QAVATAR_SHAPE_OAB")]
	QavatarShapeOab
}
