using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum TownBuffType
{
	[ProtoEnum(Name = "TOWN_BUFF_ADD_STRENGTH_RATIO")]
	TownBuffAddStrengthRatio = 1,
	[ProtoEnum(Name = "TOWN_BUFF_ADD_STRENGTH")]
	TownBuffAddStrength,
	[ProtoEnum(Name = "TOWN_BUFF_ADD_LUCK")]
	TownBuffAddLuck,
	[ProtoEnum(Name = "TOWN_BUFF_COIN_RATIO")]
	TownBuffCoinRatio,
	[ProtoEnum(Name = "TOWN_BUFF_ADD_MATERIAL")]
	TownBuffAddMaterial,
	[ProtoEnum(Name = "TOWN_BUFF_ADD_DAMAGE_RATIO")]
	TownBuffAddDamageRatio,
	[ProtoEnum(Name = "TOWN_BUFF_CHANGE_TERRAIN")]
	TownBuffChangeTerrain,
	[ProtoEnum(Name = "TOWN_BUFF_CHANGE_GROUND")]
	TownBuffChangeGround,
	[ProtoEnum(Name = "TOWN_BUFF_ADD_DAMAGE_RECEIVED")]
	TownBuffAddDamageReceived,
	[ProtoEnum(Name = "TOWN_BUFF_RPG_EVENT")]
	TownBuffRpgEvent,
	[ProtoEnum(Name = "TOWN_BUFF_HEAL_HP_BY_RATIO")]
	TownBuffHealHpByRatio,
	[ProtoEnum(Name = "TOWN_BUFF_HEAL")]
	TownBuffHeal,
	[ProtoEnum(Name = "TOWN_BUFF_CHECK_IN")]
	TownBuffCheckIn,
	[ProtoEnum(Name = "TOWN_BUFF_CHECK_OUT")]
	TownBuffCheckOut,
	[ProtoEnum(Name = "TOWN_BUFF_POSTPONE_BOSS")]
	TownBuffPostponeBoss,
	[ProtoEnum(Name = "TOWN_BUFF_CREATE_ENTITY")]
	TownBuffCreateEntity,
	[ProtoEnum(Name = "TOWN_BUFF_REPLACE_ENTITY")]
	TownBuffReplaceEntity,
	[ProtoEnum(Name = "TOWN_BUFF_ADD_BRICK")]
	TownBuffAddBrick,
	[ProtoEnum(Name = "TOWN_BUFF_LOSE_GAME")]
	TownBuffLoseGame,
	[ProtoEnum(Name = "TOWN_BUFF_RESET_GROUND")]
	TownBuffResetGround,
	[ProtoEnum(Name = "TOWN_BUFF_ADD_SPEED")]
	TownBuffAddSpeed,
	[ProtoEnum(Name = "TOWN_BUFF_CHECK_IN_OPTIONAL")]
	TownBuffCheckInOptional,
	[ProtoEnum(Name = "TOWN_BUFF_ADD_MONSTER")]
	TownBuffAddMonster,
	[ProtoEnum(Name = "TOWN_BUFF_ADD_EVENT")]
	TownBuffAddEvent,
	[ProtoEnum(Name = "TOWN_BUFF_WIN_GAME")]
	TownBuffWinGame,
	[ProtoEnum(Name = "TOWN_BUFF_RESET_TERRAIN")]
	TownBuffResetTerrain,
	[ProtoEnum(Name = "TOWN_BUFF_BATTLE_SKILL")]
	TownBuffBattleSkill,
	[ProtoEnum(Name = "TOWN_BUFF_CHANGE_BUILDING")]
	TownBuffChangeBuilding,
	[ProtoEnum(Name = "TOWN_BUFF_REDUCE_BOSS_INITIAL_HP_RATIO")]
	TownBuffReduceBossInitialHpRatio,
	[ProtoEnum(Name = "TOWN_BUFF_ATTACK_ENEMY")]
	TownBuffAttackEnemy
}
