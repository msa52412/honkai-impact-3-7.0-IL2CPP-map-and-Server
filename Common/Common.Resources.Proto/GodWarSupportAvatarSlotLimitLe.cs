using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarSupportAvatarSlotLimitLevel
{
	[ProtoEnum(Name = "GOD_WAR_SUPPORT_AVATAR_SLOT_SPECIFIC")]
	GodWarSupportAvatarSlotSpecific = 1,
	[ProtoEnum(Name = "GOD_WAR_SUPPORT_AVATAR_SLOT_NORMAL")]
	GodWarSupportAvatarSlotNormal,
	[ProtoEnum(Name = "GOD_WAR_SUPPORT_AVATAR_SLOT_LOCK")]
	GodWarSupportAvatarSlotLock
}
