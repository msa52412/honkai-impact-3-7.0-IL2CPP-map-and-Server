using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ShopGoodsPoolUnlockType
{
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_PLAYER_LEVEL")]
	ShopGoodsPoolUnlockPlayerLevel = 1,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_MP_LEVEL")]
	ShopGoodsPoolUnlockMpLevel,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_MAX_QUEST_LEVEL")]
	ShopGoodsPoolUnlockMaxQuestLevel,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_OPENWORLD_ACTIVITY_LEVEL")]
	ShopGoodsPoolUnlockOpenworldActivityLevel,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_EX_BOSS_MONSTER_LEVEL")]
	ShopGoodsPoolUnlockExBossMonsterLevel,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_EX_BOSS_SCORE")]
	ShopGoodsPoolUnlockExBossScore,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_ELF")]
	ShopGoodsPoolUnlockElf,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_GLOBALWAR_POINT_NUM")]
	ShopGoodsPoolUnlockGlobalwarPointNum,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_GLOBALWAR_POINT_ID")]
	ShopGoodsPoolUnlockGlobalwarPointId,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_ADVENTURE_LEVEL")]
	ShopGoodsPoolUnlockAdventureLevel,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_DAILY_DUTY_POINT")]
	ShopGoodsPoolUnlockDailyDutyPoint,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_AVATAR")]
	ShopGoodsPoolUnlockAvatar,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_STAGE")]
	ShopGoodsPoolUnlockStage,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_KINGDOM_WAR_CURRENCY")]
	ShopGoodsPoolUnlockKingdomWarCurrency,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_MISSION")]
	ShopGoodsPoolUnlockMission,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_SANCTUARY_LEVEL")]
	ShopGoodsPoolUnlockSanctuaryLevel,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_GOBACK_LEVEL")]
	ShopGoodsPoolUnlockGobackLevel,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_AVATAR_LOCKED")]
	ShopGoodsPoolUnlockAvatarLocked,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_HAS_GOT_ITEM")]
	ShopGoodsPoolUnlockHasGotItem,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_AVATAR_DETAIL")]
	ShopGoodsPoolUnlockAvatarDetail,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_ELF_DETAIL")]
	ShopGoodsPoolUnlockElfDetail,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_BATTLE_PASS_TICKET")]
	ShopGoodsPoolUnlockBattlePassTicket,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_PRIVILEGE_DETAIL")]
	ShopGoodsPoolUnlockPrivilegeDetail,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_HAS_NOT_GOT_ITEM")]
	ShopGoodsPoolUnlockHasNotGotItem,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_BP_LEVEL")]
	ShopGoodsPoolUnlockBpLevel,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_PLAYER_LEVEL_LOCK_ID")]
	ShopGoodsPoolUnlockPlayerLevelLockId,
	[ProtoEnum(Name = "SHOP_GOODS_POOL_UNLOCK_GOBACK_NEWBIE_LEVEL")]
	ShopGoodsPoolUnlockGobackNewbieLevel
}
