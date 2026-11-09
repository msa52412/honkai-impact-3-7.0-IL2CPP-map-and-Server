using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SpecialItemId
{
	[ProtoEnum(Name = "ITEM_GREEN_STONE")]
	ItemGreenStone = 4,
	[ProtoEnum(Name = "ITEM_PURPLE_JADE")]
	ItemPurpleJade = 5,
	[ProtoEnum(Name = "ITEM_FRIENDS_POINT")]
	ItemFriendsPoint = 6,
	[ProtoEnum(Name = "ITEM_SCOIN")]
	ItemScoin = 100,
	[ProtoEnum(Name = "ITEM_FIGHT_OPENWORLD_STAGE_BOSS")]
	ItemFightOpenworldStageBoss = 901,
	[ProtoEnum(Name = "ITEM_ARMADA_WAR_ENERGE_ITEM_ID")]
	ItemArmadaWarEnergeItemId = 914,
	[ProtoEnum(Name = "ITEM_DLC_COIN")]
	ItemDlcCoin = 801,
	[ProtoEnum(Name = "ITEM_DLC_REVIVE_COIN")]
	ItemDlcReviveCoin = 803,
	[ProtoEnum(Name = "ITEM_SC_DLC_REVIVE_COIN")]
	ItemScDlcReviveCoin = 835,
	[ProtoEnum(Name = "ITEM_STAMINA_MEDICINE")]
	ItemStaminaMedicine = 7033,
	[ProtoEnum(Name = "ITEM_BENG_HUAI_FRAGMENTS")]
	ItemBengHuaiFragments = 7032,
	[ProtoEnum(Name = "ITEM_WEEK_DAY_ACTIVITY_COIN")]
	ItemWeekDayActivityCoin = 7034,
	[ProtoEnum(Name = "ITEM_SPEED_UP_STAGE")]
	ItemSpeedUpStage = 9007,
	[ProtoEnum(Name = "ITEM_ROGUE_STAGE_COIN")]
	ItemRogueStageCoin = 9008
}
