using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum GodWarFlagType
{
	[ProtoEnum(Name = "GOD_WAR_FLAG_GROUP")]
	GodWarFlagGroup = 1,
	[ProtoEnum(Name = "GOD_WAR_FLAG_OVERALL")]
	GodWarFlagOverall = 2,
	[ProtoEnum(Name = "GOD_WAR_FLAG_AVATAR")]
	GodWarFlagAvatar = 3,
	[ProtoEnum(Name = "GOD_WAR_FLAG_BUFF")]
	GodWarFlagBuff = 4,
	[ProtoEnum(Name = "GOD_WAR_FLAG_CUR_SITE")]
	GodWarFlagCurSite = 5,
	[ProtoEnum(Name = "GOD_WAR_FLAG_CUR_SITE_TYPE")]
	GodWarFlagCurSiteType = 6,
	[ProtoEnum(Name = "GOD_WAR_FLAG_EXTRA_ITEM")]
	GodWarFlagExtraItem = 7,
	[ProtoEnum(Name = "GOD_WAR_FLAG_SETTLE_EVENT")]
	GodWarFlagSettleEvent = 8,
	[ProtoEnum(Name = "GOD_WAR_FLAG_PUNISH_LEVEL")]
	GodWarFlagPunishLevel = 9,
	[ProtoEnum(Name = "GOD_WAR_FLAG_PUNISH_BUFF")]
	GodWarFlagPunishBuff = 10,
	[ProtoEnum(Name = "GOD_WAR_FLAG_MISSION_FINISH")]
	GodWarFlagMissionFinish = 11,
	[ProtoEnum(Name = "GOD_WAR_FLAG_LOBBY_NPC")]
	GodWarFlagLobbyNpc = 12,
	[ProtoEnum(Name = "GOD_WAR_FLAG_BITMAP")]
	GodWarFlagBitmap = 13,
	[ProtoEnum(Name = "GOD_WAR_FLAG_CUR_TALE")]
	GodWarFlagCurTale = 14,
	[ProtoEnum(Name = "GOD_WAR_FLAG_STAGE_TAG")]
	GodWarFlagStageTag = 15,
	[ProtoEnum(Name = "GOD_WAR_FLAG_MISSION_STATUS")]
	GodWarFlagMissionStatus = 16,
	[ProtoEnum(Name = "GOD_WAR_FLAG_SHOPPING_COST")]
	GodWarFlagShoppingCost = 17,
	[ProtoEnum(Name = "GOD_WAR_FLAG_BUFF_LEVEL")]
	GodWarFlagBuffLevel = 18,
	[ProtoEnum(Name = "GOD_WAR_FLAG_MATERIAL_NUM")]
	GodWarFlagMaterialNum = 1002,
	[ProtoEnum(Name = "GOD_WAR_FLAG_TALENT_LEVEL")]
	GodWarFlagTalentLevel = 1003,
	[ProtoEnum(Name = "GOD_WAR_FLAG_TALENT_TOTAL_LEVEL")]
	GodWarFlagTalentTotalLevel = 1004,
	[ProtoEnum(Name = "GOD_WAR_FLAG_TALENT_UNLOCK_NUM")]
	GodWarFlagTalentUnlockNum = 1005,
	[ProtoEnum(Name = "GOD_WAR_FLAG_ROLE_RELATION_LEVEL")]
	GodWarFlagRoleRelationLevel = 1006,
	[ProtoEnum(Name = "GOD_WAR_FLAG_SUPPORT_LEVEL")]
	GodWarFlagSupportLevel = 1007
}
