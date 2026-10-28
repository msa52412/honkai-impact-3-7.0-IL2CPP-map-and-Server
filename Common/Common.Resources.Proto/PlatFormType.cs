using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PlatFormType
{
	[ProtoEnum(Name = "PLATFORM_NONE")]
	PlatformNone = 0,
	[ProtoEnum(Name = "PLATFORM_ANDROID")]
	PlatformAndroid = 100001,
	[ProtoEnum(Name = "PLATFORM_IOS")]
	PlatformIos = 200002,
	[ProtoEnum(Name = "PLATFORM_PC")]
	PlatformPc = 300003
}
