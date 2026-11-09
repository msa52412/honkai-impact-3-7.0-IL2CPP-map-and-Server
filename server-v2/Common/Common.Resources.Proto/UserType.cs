using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum UserType
{
	[ProtoEnum(Name = "USER_TYPE_NONE")]
	UserTypeNone,
	[ProtoEnum(Name = "USER_TYPE_ANDROID")]
	UserTypeAndroid,
	[ProtoEnum(Name = "USER_TYPE_IOS")]
	UserTypeIos,
	[ProtoEnum(Name = "USER_TYPE_PC")]
	UserTypePc,
	[ProtoEnum(Name = "USER_TYPE_CHANNEL")]
	UserTypeChannel,
	[ProtoEnum(Name = "USER_TYPE_WEGAME")]
	UserTypeWegame,
	[ProtoEnum(Name = "USER_TYPE_STEAM")]
	UserTypeSteam
}
