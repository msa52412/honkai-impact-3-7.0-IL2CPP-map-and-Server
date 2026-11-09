using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarRoleType
{
	[ProtoEnum(Name = "GOD_WAR_ROLE_NONE")]
	GodWarRoleNone,
	[ProtoEnum(Name = "GOD_WAR_ROLE_MEMORY_DEPRECATED")]
	GodWarRoleMemoryDeprecated,
	[ProtoEnum(Name = "GOD_WAR_ROLE_AVATAR")]
	GodWarRoleAvatar,
	[ProtoEnum(Name = "GOD_WAR_ROLE_MEMORY")]
	GodWarRoleMemory
}
