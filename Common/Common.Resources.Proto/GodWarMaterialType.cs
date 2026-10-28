using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarMaterialType
{
	[ProtoEnum(Name = "GOD_WAR_MATERIAL_CHALLENGE_COIN")]
	GodWarMaterialChallengeCoin = 1,
	[ProtoEnum(Name = "GOD_WAR_MATERIAL_AVATAR_EXP")]
	GodWarMaterialAvatarExp,
	[ProtoEnum(Name = "GOD_WAR_MATERIAL_ROLE_GIFT")]
	GodWarMaterialRoleGift,
	[ProtoEnum(Name = "GOD_WAR_MATERIAL_TALENT_POINT")]
	GodWarMaterialTalentPoint,
	[ProtoEnum(Name = "GOD_WAR_MATERIAL_AVATAR_UNLOCK")]
	GodWarMaterialAvatarUnlock,
	[ProtoEnum(Name = "GOD_WAR_MATERIAL_SUPPORT_AVATAR_UNLOCK")]
	GodWarMaterialSupportAvatarUnlock,
	[ProtoEnum(Name = "GOD_WAR_MATERIAL_DECORATION_UNLOCK")]
	GodWarMaterialDecorationUnlock,
	[ProtoEnum(Name = "GOD_WAR_MATERIAL_ROLE_UPGRADE")]
	GodWarMaterialRoleUpgrade,
	[ProtoEnum(Name = "GOD_WAR_MATERIAL_AVATAR_UPGRADE")]
	GodWarMaterialAvatarUpgrade
}
