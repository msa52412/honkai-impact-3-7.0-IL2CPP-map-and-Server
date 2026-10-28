using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageCheatData : IExtensible
{
	[ProtoContract]
	public enum Type
	{
		[ProtoEnum(Name = "STAGE_ID")]
		StageId = 1,
		[ProtoEnum(Name = "STAGE_PROGRESS")]
		StageProgress = 2,
		[ProtoEnum(Name = "STAGE_MAX_SCORE")]
		StageMaxScore = 3,
		[ProtoEnum(Name = "TIME_START")]
		TimeStart = 1001,
		[ProtoEnum(Name = "TIME_USE")]
		TimeUse = 1002,
		[ProtoEnum(Name = "AVG_FRAME_TIME")]
		AvgFrameTime = 1003,
		[ProtoEnum(Name = "PLAYER_LEVEL")]
		PlayerLevel = 1004,
		[ProtoEnum(Name = "STAGE_SCORE")]
		StageScore = 1005,
		[ProtoEnum(Name = "MONSTER_TOTAL_HP")]
		MonsterTotalHp = 1006,
		[ProtoEnum(Name = "MONSTER_TOTAL_LOSS_HP")]
		MonsterTotalLossHp = 1007,
		[ProtoEnum(Name = "MONSTER_HIT_MISS_NUM")]
		MonsterHitMissNum = 1008,
		[ProtoEnum(Name = "MONSTER_HIT_NUM")]
		MonsterHitNum = 1009,
		[ProtoEnum(Name = "NATURE_DAMAGE_BONUS_RATIO_MAX")]
		NatureDamageBonusRatioMax = 1010,
		[ProtoEnum(Name = "AVATAR_ID_1")]
		AvatarId1 = 2001,
		[ProtoEnum(Name = "AVATAR_LEVEL_1")]
		AvatarLevel1 = 2002,
		[ProtoEnum(Name = "AVATAR_COMBAT_1")]
		AvatarCombat1 = 2003,
		[ProtoEnum(Name = "ATTACK_MAX_1")]
		AttackMax1 = 2004,
		[ProtoEnum(Name = "SP_MAX_1")]
		SpMax1 = 2005,
		[ProtoEnum(Name = "SP_BEGIN_1")]
		SpBegin1 = 2006,
		[ProtoEnum(Name = "SP_END_1")]
		SpEnd1 = 2007,
		[ProtoEnum(Name = "SP_GAIN_1")]
		SpGain1 = 2008,
		[ProtoEnum(Name = "SP_USE_1")]
		SpUse1 = 2009,
		[ProtoEnum(Name = "HP_MAX_1")]
		HpMax1 = 2010,
		[ProtoEnum(Name = "HP_BEGIN_1")]
		HpBegin1 = 2011,
		[ProtoEnum(Name = "HP_END_1")]
		HpEnd1 = 2012,
		[ProtoEnum(Name = "HP_GAIN_1")]
		HpGain1 = 2013,
		[ProtoEnum(Name = "HIT_COUNT_1")]
		HitCount1 = 2014,
		[ProtoEnum(Name = "HIT_DAMAGE_1")]
		HitDamage1 = 2015,
		[ProtoEnum(Name = "HIT_NORMAL_DAMAGE_MAX_1")]
		HitNormalDamageMax1 = 2016,
		[ProtoEnum(Name = "HIT_CRITICAL_DAMAGE_MAX_1")]
		HitCriticalDamageMax1 = 2017,
		[ProtoEnum(Name = "BEHIT_COUNT_1")]
		BehitCount1 = 2018,
		[ProtoEnum(Name = "BEHIT_NORMAL_DAMAGE_MAX_1")]
		BehitNormalDamageMax1 = 2019,
		[ProtoEnum(Name = "BEHIT_CRITICAL_DAMAGE_MAX_1")]
		BehitCriticalDamageMax1 = 2020,
		[ProtoEnum(Name = "COMBO_MAX_1")]
		ComboMax1 = 2021,
		[ProtoEnum(Name = "AVATAR_TIME_USE_1")]
		AvatarTimeUse1 = 2022,
		[ProtoEnum(Name = "WEAPON_LEVEL_1")]
		WeaponLevel1 = 2023,
		[ProtoEnum(Name = "STIGMATA_LEVEL_MAX_1")]
		StigmataLevelMax1 = 2024,
		[ProtoEnum(Name = "MP_TEAM_SKILL_TIMES_1")]
		MpTeamSkillTimes1 = 2025,
		[ProtoEnum(Name = "MP_QTE_SKILL_TIMES_1")]
		MpQteSkillTimes1 = 2026,
		[ProtoEnum(Name = "MP_BE_RESCUED_TIMES_1")]
		MpBeRescuedTimes1 = 2027,
		[ProtoEnum(Name = "MP_RESCUE_TIMES_1")]
		MpRescueTimes1 = 2028,
		[ProtoEnum(Name = "MP_AVG_RESCUE_TIME_1")]
		MpAvgRescueTime1 = 2029,
		[ProtoEnum(Name = "MP_CHAT_TIMES_1")]
		MpChatTimes1 = 2030,
		[ProtoEnum(Name = "ULTIMATE_SKILL_TIMES_1")]
		UltimateSkillTimes1 = 2031,
		[ProtoEnum(Name = "ULTIMATE_SKILL_DURATION_1")]
		UltimateSkillDuration1 = 2032,
		[ProtoEnum(Name = "ATTACK_SP_GAIN_1")]
		AttackSpGain1 = 2033,
		[ProtoEnum(Name = "WEAPON_SKILL_TIMES_1")]
		WeaponSkillTimes1 = 2034,
		[ProtoEnum(Name = "SWITCH_IN_SKILL_DAMAGE_MAX_1")]
		SwitchInSkillDamageMax1 = 2035,
		[ProtoEnum(Name = "BRANCH_SKILL_DAMAGE_MAX_1")]
		BranchSkillDamageMax1 = 2036,
		[ProtoEnum(Name = "CHARGE_SKILL_DAMAGE_MAX_1")]
		ChargeSkillDamageMax1 = 2037,
		[ProtoEnum(Name = "ULTIMATE_SKILL_DAMAGE_MAX_1")]
		UltimateSkillDamageMax1 = 2038,
		[ProtoEnum(Name = "ULTIMATE_SKILL_INTERVAL_TIME_MIN_1")]
		UltimateSkillIntervalTimeMin1 = 2039,
		[ProtoEnum(Name = "WEAPON_SKILL_INTERVAL_TIME_MIN_1")]
		WeaponSkillIntervalTimeMin1 = 2040,
		[ProtoEnum(Name = "HIT_INTERVAL_TIME_MIN_1")]
		HitIntervalTimeMin1 = 2041,
		[ProtoEnum(Name = "SP_MIN_1")]
		SpMin1 = 2042,
		[ProtoEnum(Name = "HP_MIN_1")]
		HpMin1 = 2043,
		[ProtoEnum(Name = "STIGMATA_1_1")]
		Stigmata11 = 2044,
		[ProtoEnum(Name = "STIGMATA_1_2")]
		Stigmata12 = 2045,
		[ProtoEnum(Name = "STIGMATA_1_3")]
		Stigmata13 = 2046,
		[ProtoEnum(Name = "WEAPON_ID_1")]
		WeaponId1 = 2047,
		[ProtoEnum(Name = "DRESS_ID_1")]
		DressId1 = 2048,
		[ProtoEnum(Name = "SERVER_AVATAR_ID_1")]
		ServerAvatarId1 = 2049,
		[ProtoEnum(Name = "SERVER_STIGMATA_1_1")]
		ServerStigmata11 = 2050,
		[ProtoEnum(Name = "SERVER_STIGMATA_1_2")]
		ServerStigmata12 = 2051,
		[ProtoEnum(Name = "SERVER_STIGMATA_1_3")]
		ServerStigmata13 = 2052,
		[ProtoEnum(Name = "SERVER_WEAPON_ID_1")]
		ServerWeaponId1 = 2053,
		[ProtoEnum(Name = "SERVER_DRESS_ID_1")]
		ServerDressId1 = 2054,
		[ProtoEnum(Name = "AVATAR_ID_2")]
		AvatarId2 = 2101,
		[ProtoEnum(Name = "AVATAR_LEVEL_2")]
		AvatarLevel2 = 2102,
		[ProtoEnum(Name = "AVATAR_COMBAT_2")]
		AvatarCombat2 = 2103,
		[ProtoEnum(Name = "ATTACK_MAX_2")]
		AttackMax2 = 2104,
		[ProtoEnum(Name = "SP_MAX_2")]
		SpMax2 = 2105,
		[ProtoEnum(Name = "SP_BEGIN_2")]
		SpBegin2 = 2106,
		[ProtoEnum(Name = "SP_END_2")]
		SpEnd2 = 2107,
		[ProtoEnum(Name = "SP_GAIN_2")]
		SpGain2 = 2108,
		[ProtoEnum(Name = "SP_USE_2")]
		SpUse2 = 2109,
		[ProtoEnum(Name = "HP_MAX_2")]
		HpMax2 = 2110,
		[ProtoEnum(Name = "HP_BEGIN_2")]
		HpBegin2 = 2111,
		[ProtoEnum(Name = "HP_END_2")]
		HpEnd2 = 2112,
		[ProtoEnum(Name = "HP_GAIN_2")]
		HpGain2 = 2113,
		[ProtoEnum(Name = "HIT_COUNT_2")]
		HitCount2 = 2114,
		[ProtoEnum(Name = "HIT_DAMAGE_2")]
		HitDamage2 = 2115,
		[ProtoEnum(Name = "HIT_NORMAL_DAMAGE_MAX_2")]
		HitNormalDamageMax2 = 2116,
		[ProtoEnum(Name = "HIT_CRITICAL_DAMAGE_MAX_2")]
		HitCriticalDamageMax2 = 2117,
		[ProtoEnum(Name = "BEHIT_COUNT_2")]
		BehitCount2 = 2118,
		[ProtoEnum(Name = "BEHIT_NORMAL_DAMAGE_MAX_2")]
		BehitNormalDamageMax2 = 2119,
		[ProtoEnum(Name = "BEHIT_CRITICAL_DAMAGE_MAX_2")]
		BehitCriticalDamageMax2 = 2120,
		[ProtoEnum(Name = "COMBO_MAX_2")]
		ComboMax2 = 2121,
		[ProtoEnum(Name = "AVATAR_TIME_USE_2")]
		AvatarTimeUse2 = 2122,
		[ProtoEnum(Name = "WEAPON_LEVEL_2")]
		WeaponLevel2 = 2123,
		[ProtoEnum(Name = "STIGMATA_LEVEL_MAX_2")]
		StigmataLevelMax2 = 2124,
		[ProtoEnum(Name = "MP_TEAM_SKILL_TIMES_2")]
		MpTeamSkillTimes2 = 2125,
		[ProtoEnum(Name = "MP_QTE_SKILL_TIMES_2")]
		MpQteSkillTimes2 = 2126,
		[ProtoEnum(Name = "MP_BE_RESCUED_TIMES_2")]
		MpBeRescuedTimes2 = 2127,
		[ProtoEnum(Name = "MP_RESCUE_TIMES_2")]
		MpRescueTimes2 = 2128,
		[ProtoEnum(Name = "MP_AVG_RESCUE_TIME_2")]
		MpAvgRescueTime2 = 2129,
		[ProtoEnum(Name = "MP_CHAT_TIMES_2")]
		MpChatTimes2 = 2130,
		[ProtoEnum(Name = "ULTIMATE_SKILL_TIMES_2")]
		UltimateSkillTimes2 = 2131,
		[ProtoEnum(Name = "ULTIMATE_SKILL_DURATION_2")]
		UltimateSkillDuration2 = 2132,
		[ProtoEnum(Name = "ATTACK_SP_GAIN_2")]
		AttackSpGain2 = 2133,
		[ProtoEnum(Name = "WEAPON_SKILL_TIMES_2")]
		WeaponSkillTimes2 = 2134,
		[ProtoEnum(Name = "SWITCH_IN_SKILL_DAMAGE_MAX_2")]
		SwitchInSkillDamageMax2 = 2135,
		[ProtoEnum(Name = "BRANCH_SKILL_DAMAGE_MAX_2")]
		BranchSkillDamageMax2 = 2136,
		[ProtoEnum(Name = "CHARGE_SKILL_DAMAGE_MAX_2")]
		ChargeSkillDamageMax2 = 2137,
		[ProtoEnum(Name = "ULTIMATE_SKILL_DAMAGE_MAX_2")]
		UltimateSkillDamageMax2 = 2138,
		[ProtoEnum(Name = "ULTIMATE_SKILL_INTERVAL_TIME_MIN_2")]
		UltimateSkillIntervalTimeMin2 = 2139,
		[ProtoEnum(Name = "WEAPON_SKILL_INTERVAL_TIME_MIN_2")]
		WeaponSkillIntervalTimeMin2 = 2140,
		[ProtoEnum(Name = "HIT_INTERVAL_TIME_MIN_2")]
		HitIntervalTimeMin2 = 2141,
		[ProtoEnum(Name = "SP_MIN_2")]
		SpMin2 = 2142,
		[ProtoEnum(Name = "HP_MIN_2")]
		HpMin2 = 2143,
		[ProtoEnum(Name = "STIGMATA_2_1")]
		Stigmata21 = 2144,
		[ProtoEnum(Name = "STIGMATA_2_2")]
		Stigmata22 = 2145,
		[ProtoEnum(Name = "STIGMATA_2_3")]
		Stigmata23 = 2146,
		[ProtoEnum(Name = "WEAPON_ID_2")]
		WeaponId2 = 2147,
		[ProtoEnum(Name = "DRESS_ID_2")]
		DressId2 = 2148,
		[ProtoEnum(Name = "SERVER_AVATAR_ID_2")]
		ServerAvatarId2 = 2149,
		[ProtoEnum(Name = "SERVER_STIGMATA_2_1")]
		ServerStigmata21 = 2150,
		[ProtoEnum(Name = "SERVER_STIGMATA_2_2")]
		ServerStigmata22 = 2151,
		[ProtoEnum(Name = "SERVER_STIGMATA_2_3")]
		ServerStigmata23 = 2152,
		[ProtoEnum(Name = "SERVER_WEAPON_ID_2")]
		ServerWeaponId2 = 2153,
		[ProtoEnum(Name = "SERVER_DRESS_ID_2")]
		ServerDressId2 = 2154,
		[ProtoEnum(Name = "AVATAR_ID_3")]
		AvatarId3 = 2201,
		[ProtoEnum(Name = "AVATAR_LEVEL_3")]
		AvatarLevel3 = 2202,
		[ProtoEnum(Name = "AVATAR_COMBAT_3")]
		AvatarCombat3 = 2203,
		[ProtoEnum(Name = "ATTACK_MAX_3")]
		AttackMax3 = 2204,
		[ProtoEnum(Name = "SP_MAX_3")]
		SpMax3 = 2205,
		[ProtoEnum(Name = "SP_BEGIN_3")]
		SpBegin3 = 2206,
		[ProtoEnum(Name = "SP_END_3")]
		SpEnd3 = 2207,
		[ProtoEnum(Name = "SP_GAIN_3")]
		SpGain3 = 2208,
		[ProtoEnum(Name = "SP_USE_3")]
		SpUse3 = 2209,
		[ProtoEnum(Name = "HP_MAX_3")]
		HpMax3 = 2210,
		[ProtoEnum(Name = "HP_BEGIN_3")]
		HpBegin3 = 2211,
		[ProtoEnum(Name = "HP_END_3")]
		HpEnd3 = 2212,
		[ProtoEnum(Name = "HP_GAIN_3")]
		HpGain3 = 2213,
		[ProtoEnum(Name = "HIT_COUNT_3")]
		HitCount3 = 2214,
		[ProtoEnum(Name = "HIT_DAMAGE_3")]
		HitDamage3 = 2215,
		[ProtoEnum(Name = "HIT_NORMAL_DAMAGE_MAX_3")]
		HitNormalDamageMax3 = 2216,
		[ProtoEnum(Name = "HIT_CRITICAL_DAMAGE_MAX_3")]
		HitCriticalDamageMax3 = 2217,
		[ProtoEnum(Name = "BEHIT_COUNT_3")]
		BehitCount3 = 2218,
		[ProtoEnum(Name = "BEHIT_NORMAL_DAMAGE_MAX_3")]
		BehitNormalDamageMax3 = 2219,
		[ProtoEnum(Name = "BEHIT_CRITICAL_DAMAGE_MAX_3")]
		BehitCriticalDamageMax3 = 2220,
		[ProtoEnum(Name = "COMBO_MAX_3")]
		ComboMax3 = 2221,
		[ProtoEnum(Name = "AVATAR_TIME_USE_3")]
		AvatarTimeUse3 = 2222,
		[ProtoEnum(Name = "WEAPON_LEVEL_3")]
		WeaponLevel3 = 2223,
		[ProtoEnum(Name = "STIGMATA_LEVEL_MAX_3")]
		StigmataLevelMax3 = 2224,
		[ProtoEnum(Name = "MP_TEAM_SKILL_TIMES_3")]
		MpTeamSkillTimes3 = 2225,
		[ProtoEnum(Name = "MP_QTE_SKILL_TIMES_3")]
		MpQteSkillTimes3 = 2226,
		[ProtoEnum(Name = "MP_BE_RESCUED_TIMES_3")]
		MpBeRescuedTimes3 = 2227,
		[ProtoEnum(Name = "MP_RESCUE_TIMES_3")]
		MpRescueTimes3 = 2228,
		[ProtoEnum(Name = "MP_AVG_RESCUE_TIME_3")]
		MpAvgRescueTime3 = 2229,
		[ProtoEnum(Name = "MP_CHAT_TIMES_3")]
		MpChatTimes3 = 2230,
		[ProtoEnum(Name = "ULTIMATE_SKILL_TIMES_3")]
		UltimateSkillTimes3 = 2231,
		[ProtoEnum(Name = "ULTIMATE_SKILL_DURATION_3")]
		UltimateSkillDuration3 = 2232,
		[ProtoEnum(Name = "ATTACK_SP_GAIN_3")]
		AttackSpGain3 = 2233,
		[ProtoEnum(Name = "WEAPON_SKILL_TIMES_3")]
		WeaponSkillTimes3 = 2234,
		[ProtoEnum(Name = "SWITCH_IN_SKILL_DAMAGE_MAX_3")]
		SwitchInSkillDamageMax3 = 2235,
		[ProtoEnum(Name = "BRANCH_SKILL_DAMAGE_MAX_3")]
		BranchSkillDamageMax3 = 2236,
		[ProtoEnum(Name = "CHARGE_SKILL_DAMAGE_MAX_3")]
		ChargeSkillDamageMax3 = 2237,
		[ProtoEnum(Name = "ULTIMATE_SKILL_DAMAGE_MAX_3")]
		UltimateSkillDamageMax3 = 2238,
		[ProtoEnum(Name = "ULTIMATE_SKILL_INTERVAL_TIME_MIN_3")]
		UltimateSkillIntervalTimeMin3 = 2239,
		[ProtoEnum(Name = "WEAPON_SKILL_INTERVAL_TIME_MIN_3")]
		WeaponSkillIntervalTimeMin3 = 2240,
		[ProtoEnum(Name = "HIT_INTERVAL_TIME_MIN_3")]
		HitIntervalTimeMin3 = 2241,
		[ProtoEnum(Name = "SP_MIN_3")]
		SpMin3 = 2242,
		[ProtoEnum(Name = "HP_MIN_3")]
		HpMin3 = 2243,
		[ProtoEnum(Name = "STIGMATA_3_1")]
		Stigmata31 = 2244,
		[ProtoEnum(Name = "STIGMATA_3_2")]
		Stigmata32 = 2245,
		[ProtoEnum(Name = "STIGMATA_3_3")]
		Stigmata33 = 2246,
		[ProtoEnum(Name = "WEAPON_ID_3")]
		WeaponId3 = 2247,
		[ProtoEnum(Name = "DRESS_ID_3")]
		DressId3 = 2248,
		[ProtoEnum(Name = "SERVER_AVATAR_ID_3")]
		ServerAvatarId3 = 2249,
		[ProtoEnum(Name = "SERVER_STIGMATA_3_1")]
		ServerStigmata31 = 2250,
		[ProtoEnum(Name = "SERVER_STIGMATA_3_2")]
		ServerStigmata32 = 2251,
		[ProtoEnum(Name = "SERVER_STIGMATA_3_3")]
		ServerStigmata33 = 2252,
		[ProtoEnum(Name = "SERVER_WEAPON_ID_3")]
		ServerWeaponId3 = 2253,
		[ProtoEnum(Name = "SERVER_DRESS_ID_3")]
		ServerDressId3 = 2254,
		[ProtoEnum(Name = "ELF_ID")]
		ElfId = 2301,
		[ProtoEnum(Name = "ELF_LEVEL")]
		ElfLevel = 2302,
		[ProtoEnum(Name = "ELF_COMBAT")]
		ElfCombat = 2303,
		[ProtoEnum(Name = "ELF_ATTACK")]
		ElfAttack = 2304,
		[ProtoEnum(Name = "ELF_SP_MAX")]
		ElfSpMax = 2305,
		[ProtoEnum(Name = "ELF_ULTIMATE_SKILL_TIMES")]
		ElfUltimateSkillTimes = 2306,
		[ProtoEnum(Name = "ELF_SP_USE")]
		ElfSpUse = 2307,
		[ProtoEnum(Name = "ELF_HIT_CRITICAL_DAMAGE_MAX")]
		ElfHitCriticalDamageMax = 2308,
		[ProtoEnum(Name = "ELF_HIT_NORMAL_DAMAGE_MAX")]
		ElfHitNormalDamageMax = 2309,
		[ProtoEnum(Name = "ELF_ULTIMATE_SKILL_INTERVAL_TIME_MIN")]
		ElfUltimateSkillIntervalTimeMin = 2310,
		[ProtoEnum(Name = "ELF_HIT_INTERVAL_TIME_MIN")]
		ElfHitIntervalTimeMin = 2311,
		[ProtoEnum(Name = "SERVER_ELF_ID")]
		ServerElfId = 2312,
		[ProtoEnum(Name = "ELF_ID_2")]
		ElfId2 = 2401,
		[ProtoEnum(Name = "ELF_LEVEL_2")]
		ElfLevel2 = 2402,
		[ProtoEnum(Name = "ELF_COMBAT_2")]
		ElfCombat2 = 2403,
		[ProtoEnum(Name = "ELF_ATTACK_2")]
		ElfAttack2 = 2404,
		[ProtoEnum(Name = "ELF_SP_MAX_2")]
		ElfSpMax2 = 2405,
		[ProtoEnum(Name = "ELF_ULTIMATE_SKILL_TIMES_2")]
		ElfUltimateSkillTimes2 = 2406,
		[ProtoEnum(Name = "ELF_SP_USE_2")]
		ElfSpUse2 = 2407,
		[ProtoEnum(Name = "ELF_HIT_CRITICAL_DAMAGE_MAX_2")]
		ElfHitCriticalDamageMax2 = 2408,
		[ProtoEnum(Name = "ELF_HIT_NORMAL_DAMAGE_MAX_2")]
		ElfHitNormalDamageMax2 = 2409,
		[ProtoEnum(Name = "ELF_ULTIMATE_SKILL_INTERVAL_TIME_MIN_2")]
		ElfUltimateSkillIntervalTimeMin2 = 2410,
		[ProtoEnum(Name = "ELF_HIT_INTERVAL_TIME_MIN_2")]
		ElfHitIntervalTimeMin2 = 2411,
		[ProtoEnum(Name = "SERVER_ELF_ID_2")]
		ServerElfId2 = 2412,
		[ProtoEnum(Name = "ELF_ID_3")]
		ElfId3 = 2501,
		[ProtoEnum(Name = "ELF_LEVEL_3")]
		ElfLevel3 = 2502,
		[ProtoEnum(Name = "ELF_COMBAT_3")]
		ElfCombat3 = 2503,
		[ProtoEnum(Name = "ELF_ATTACK_3")]
		ElfAttack3 = 2504,
		[ProtoEnum(Name = "ELF_SP_MAX_3")]
		ElfSpMax3 = 2505,
		[ProtoEnum(Name = "ELF_ULTIMATE_SKILL_TIMES_3")]
		ElfUltimateSkillTimes3 = 2506,
		[ProtoEnum(Name = "ELF_SP_USE_3")]
		ElfSpUse3 = 2507,
		[ProtoEnum(Name = "ELF_HIT_CRITICAL_DAMAGE_MAX_3")]
		ElfHitCriticalDamageMax3 = 2508,
		[ProtoEnum(Name = "ELF_HIT_NORMAL_DAMAGE_MAX_3")]
		ElfHitNormalDamageMax3 = 2509,
		[ProtoEnum(Name = "ELF_ULTIMATE_SKILL_INTERVAL_TIME_MIN_3")]
		ElfUltimateSkillIntervalTimeMin3 = 2510,
		[ProtoEnum(Name = "ELF_HIT_INTERVAL_TIME_MIN_3")]
		ElfHitIntervalTimeMin3 = 2511,
		[ProtoEnum(Name = "SERVER_ELF_ID_3")]
		ServerElfId3 = 2512,
		[ProtoEnum(Name = "IS_MP_VOICE_USED")]
		IsMpVoiceUsed = 3001,
		[ProtoEnum(Name = "MP_VOICE_USE_TIME")]
		MpVoiceUseTime = 3002,
		[ProtoEnum(Name = "SEALED_EVENT_ID")]
		SealedEventId = 4001
	}

	private IExtension __pbn__extensionData;

	private Type? __pbn__type;

	private float? __pbn__Value;

	[ProtoMember(1)]
	[DefaultValue(Type.StageId)]
	public Type type
	{
		get
		{
			return __pbn__type ?? Type.StageId;
		}
		set
		{
			__pbn__type = value;
		}
	}

	[ProtoMember(2, Name = "value")]
	public float Value
	{
		get
		{
			return __pbn__Value.GetValueOrDefault();
		}
		set
		{
			__pbn__Value = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializetype()
	{
		return __pbn__type.HasValue;
	}

	public void Resettype()
	{
		__pbn__type = null;
	}

	public bool ShouldSerializeValue()
	{
		return __pbn__Value.HasValue;
	}

	public void ResetValue()
	{
		__pbn__Value = null;
	}
}
