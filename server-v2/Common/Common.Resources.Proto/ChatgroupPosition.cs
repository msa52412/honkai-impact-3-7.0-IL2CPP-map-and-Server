using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatgroupPosition
{
	[ProtoEnum(Name = "CHATGROUP_POSITION_NONE")]
	ChatgroupPositionNone,
	[ProtoEnum(Name = "CHATGROUP_POSITION_CREATER")]
	ChatgroupPositionCreater,
	[ProtoEnum(Name = "CHATGROUP_POSITION_MEMBER")]
	ChatgroupPositionMember
}
