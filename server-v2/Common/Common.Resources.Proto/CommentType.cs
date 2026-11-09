using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CommentType
{
	[ProtoEnum(Name = "COMMENT_OK")]
	CommentOk = 1,
	[ProtoEnum(Name = "COMMENT_CANCEL")]
	CommentCancel,
	[ProtoEnum(Name = "COMMENT_IGNORE")]
	CommentIgnore
}
