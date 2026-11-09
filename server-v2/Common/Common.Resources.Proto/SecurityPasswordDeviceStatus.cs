using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SecurityPasswordDeviceStatus
{
	[ProtoEnum(Name = "SECURITY_PASSWORD_DEVICE_UNLOCKED")]
	SecurityPasswordDeviceUnlocked,
	[ProtoEnum(Name = "SECURITY_PASSWORD_DEVICE_LOCKED")]
	SecurityPasswordDeviceLocked
}
