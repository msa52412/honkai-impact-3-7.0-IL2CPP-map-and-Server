using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CustomHeadType
{
	[ProtoEnum(Name = "CUSTOM_HEAD_TYPE_NONE")]
	CustomHeadTypeNone,
	[ProtoEnum(Name = "CUSTOM_HEAD_TYPE_AVATAR")]
	CustomHeadTypeAvatar,
	[ProtoEnum(Name = "CUSTOM_HEAD_TYPE_DRESS")]
	CustomHeadTypeDress,
	[ProtoEnum(Name = "CUSTOM_HEAD_TYPE_NORMAL")]
	CustomHeadTypeNormal
}
