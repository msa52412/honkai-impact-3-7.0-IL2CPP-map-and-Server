using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum DeviceType
{
	[ProtoEnum(Name = "DEVICE_TYPE_NONE")]
	DeviceTypeNone,
	[ProtoEnum(Name = "DEVICE_TYPE_ANDROID")]
	DeviceTypeAndroid,
	[ProtoEnum(Name = "DEVICE_TYPE_IOS")]
	DeviceTypeIos,
	[ProtoEnum(Name = "DEVICE_TYPE_PC")]
	DeviceTypePc
}
