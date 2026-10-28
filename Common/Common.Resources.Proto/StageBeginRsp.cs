using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageBeginRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 44
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ = 0,
		[ProtoEnum(Name = "FAIL")]
		Fail = 1,
		[ProtoEnum(Name = "STAGE_NOT_EXIST")]
		StageNotExist = 2,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack = 3,
		[ProtoEnum(Name = "STAMINA_LACK")]
		StaminaLack = 4,
		[ProtoEnum(Name = "AVATAR_ERROR")]
		AvatarError = 5,
		[ProtoEnum(Name = "EQUIPMENT_FULL")]
		EquipmentFull = 6,
		[ProtoEnum(Name = "ENTER_TIMES_LACK")]
		EnterTimesLack = 7,
		[ProtoEnum(Name = "ASSISTANT_PLAYER_FROZEN")]
		AssistantPlayerFrozen = 8,
		[ProtoEnum(Name = "ACTIVITY_LEVEL_LACK")]
		ActivityLevelLack = 9,
		[ProtoEnum(Name = "ACTIVITY_ENTER_TIMES_LACK")]
		ActivityEnterTimesLack = 10,
		[ProtoEnum(Name = "ACTIVITY_NOT_BEGIN")]
		ActivityNotBegin = 11,
		[ProtoEnum(Name = "ACTIVITY_END")]
		ActivityEnd = 12,
		[ProtoEnum(Name = "AVATAR_NUM_ERROR")]
		AvatarNumError = 13,
		[ProtoEnum(Name = "CHALLENGE_NUM_LACK")]
		ChallengeNumLack = 14,
		[ProtoEnum(Name = "FRIEND_NUM_MAX")]
		FriendNumMax = 15,
		[ProtoEnum(Name = "EXTRA_STORY_LEVEL_LACK")]
		ExtraStoryLevelLack = 16,
		[ProtoEnum(Name = "EXTRA_STORY_NOT_BEGIN")]
		ExtraStoryNotBegin = 17,
		[ProtoEnum(Name = "EXTRA_STORY_END")]
		ExtraStoryEnd = 18,
		[ProtoEnum(Name = "NOT_MEET_RESTRICT")]
		NotMeetRestrict = 19,
		[ProtoEnum(Name = "EQUIPMENT_MATERIAL_LIMIT")]
		EquipmentMaterialLimit = 20,
		[ProtoEnum(Name = "EQUIPMENT_FRAGMENT_LIMIT")]
		EquipmentFragmentLimit = 21,
		[ProtoEnum(Name = "NOT_MEET_FAST")]
		NotMeetFast = 22,
		[ProtoEnum(Name = "PRE_MP_STAGE_NOT_FINISH")]
		PreMpStageNotFinish = 23,
		[ProtoEnum(Name = "STAGE_TYPE_ERROR")]
		StageTypeError = 24,
		[ProtoEnum(Name = "MP_LEVEL_LACK")]
		MpLevelLack = 25,
		[ProtoEnum(Name = "ACTIVITY_UNLOCK")]
		ActivityUnlock = 26,
		[ProtoEnum(Name = "NOT_MEET_EXTRA_STORY_CHALLENGE_MODE")]
		NotMeetExtraStoryChallengeMode = 28,
		[ProtoEnum(Name = "EXTRA_STORY_EQUIPMENT_USED")]
		ExtraStoryEquipmentUsed = 29,
		[ProtoEnum(Name = "EXTRA_STORY_AVATAR_USED")]
		ExtraStoryAvatarUsed = 30,
		[ProtoEnum(Name = "EXTRA_STORY_AVATAR_LIMIT")]
		ExtraStoryAvatarLimit = 31,
		[ProtoEnum(Name = "STAGE_FINISH_ONCE")]
		StageFinishOnce = 32,
		[ProtoEnum(Name = "SEASON_TICKET_LACK")]
		SeasonTicketLack = 33,
		[ProtoEnum(Name = "PRE_STAGE_NOT_FINISH")]
		PreStageNotFinish = 34,
		[ProtoEnum(Name = "CHALLENGE_MODE_NEED_RESET")]
		ChallengeModeNeedReset = 35,
		[ProtoEnum(Name = "ALREADY_OPEN_DUNGEONS")]
		AlreadyOpenDungeons = 36,
		[ProtoEnum(Name = "DUNGEONS_AREA_LOCKED")]
		DungeonsAreaLocked = 37,
		[ProtoEnum(Name = "DUNGEONS_TEST_STAGE_FIRST")]
		DungeonsTestStageFirst = 38,
		[ProtoEnum(Name = "DUNGEONS_TEST_STAGE_DONE")]
		DungeonsTestStageDone = 39,
		[ProtoEnum(Name = "COST_MATERIAL_LACK")]
		CostMaterialLack = 40,
		[ProtoEnum(Name = "WEEKLY_ENTER_TIMES_LACK")]
		WeeklyEnterTimesLack = 41,
		[ProtoEnum(Name = "RECLAIM_BOOM_ENERGY_LACK")]
		ReclaimBoomEnergyLack = 42,
		[ProtoEnum(Name = "RECLAIM_LEVEL_LACK")]
		ReclaimLevelLack = 43,
		[ProtoEnum(Name = "OPENWORLD_BOSS_MP_INVALID")]
		OpenworldBossMpInvalid = 44,
		[ProtoEnum(Name = "SCORE_LACK")]
		ScoreLack = 45,
		[ProtoEnum(Name = "COUPLE_TOWER_FLOOR_LACK")]
		CoupleTowerFloorLack = 46,
		[ProtoEnum(Name = "GENERAL_ACTIVITY_CONDITION_LIMIT")]
		GeneralActivityConditionLimit = 47,
		[ProtoEnum(Name = "EQUIPMENT_DISMATCH_LOCKED")]
		EquipmentDismatchLocked = 48,
		[ProtoEnum(Name = "WEAPON_LOCKED")]
		WeaponLocked = 49,
		[ProtoEnum(Name = "STIGMATA_LOCKED")]
		StigmataLocked = 50,
		[ProtoEnum(Name = "ELF_ERROR")]
		ElfError = 51,
		[ProtoEnum(Name = "NOT_IN_SQUAD")]
		NotInSquad = 52,
		[ProtoEnum(Name = "ELF_CARRY_LEVEL_LIMIT")]
		ElfCarryLevelLimit = 53,
		[ProtoEnum(Name = "CAMPAIGN_PHASE_ERROR")]
		CampaignPhaseError = 54,
		[ProtoEnum(Name = "MISSION_LOCKED")]
		MissionLocked = 55,
		[ProtoEnum(Name = "HAS_TAKE_CAMPAIGN_REWARD")]
		HasTakeCampaignReward = 56,
		[ProtoEnum(Name = "KINGDOM_WAR_NOT_OPEN")]
		KingdomWarNotOpen = 57,
		[ProtoEnum(Name = "KINGDOM_WAR_POINT_ERROR")]
		KingdomWarPointError = 58,
		[ProtoEnum(Name = "KINGDOM_WAR_LEVEL_LACK")]
		KingdomWarLevelLack = 59,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed = 61,
		[ProtoEnum(Name = "NOT_IN_TEAM")]
		NotInTeam = 62,
		[ProtoEnum(Name = "EXTRA_MATERIAL_LACK")]
		ExtraMaterialLack = 63,
		[ProtoEnum(Name = "FEATURE_LIMIT")]
		FeatureLimit = 64,
		[ProtoEnum(Name = "STAGE_MISMATCH")]
		StageMismatch = 65,
		[ProtoEnum(Name = "GRATUITY_STAGE_NOT_EXIST")]
		GratuityStageNotExist = 66,
		[ProtoEnum(Name = "NOT_IN_SAME_ARMADA")]
		NotInSameArmada = 67,
		[ProtoEnum(Name = "ASSIST_STAMINA_LACK")]
		AssistStaminaLack = 68,
		[ProtoEnum(Name = "ARMADA_ACTIVITY_NOT_OPEN")]
		ArmadaActivityNotOpen = 69,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada = 70,
		[ProtoEnum(Name = "LOGIN_DAYS_LACK")]
		LoginDaysLack = 71,
		[ProtoEnum(Name = "NOT_NEWBIE")]
		NotNewbie = 72,
		[ProtoEnum(Name = "IN_CD")]
		InCd = 73,
		[ProtoEnum(Name = "DAILY_RAID_NOT_ACTIVE")]
		DailyRaidNotActive = 74,
		[ProtoEnum(Name = "CHAPTER_PRE_ZONE_NOT_FINISHED")]
		ChapterPreZoneNotFinished = 75,
		[ProtoEnum(Name = "BEAST_TIME_NOT_ENOUGH")]
		BeastTimeNotEnough = 76,
		[ProtoEnum(Name = "SINGLE_WANTED_BOX_UNPICK")]
		SingleWantedBoxUnpick = 77,
		[ProtoEnum(Name = "EXCLUSIVE_STAGE_CONFLICT")]
		ExclusiveStageConflict = 78,
		[ProtoEnum(Name = "THEME_WANTED_LEADER_TICKET_LACK")]
		ThemeWantedLeaderTicketLack = 79,
		[ProtoEnum(Name = "THEME_WANTED_MEMBER_LOCKED")]
		ThemeWantedMemberLocked = 80,
		[ProtoEnum(Name = "THEME_WANTED_LOCK")]
		ThemeWantedLock = 81,
		[ProtoEnum(Name = "THEME_WANTED_TICKET_LACK")]
		ThemeWantedTicketLack = 82,
		[ProtoEnum(Name = "SLG_BATTLE_NOT_OPEN")]
		SlgBattleNotOpen = 83,
		[ProtoEnum(Name = "SLG_BATTLE_POINT_ERROR")]
		SlgBattlePointError = 84,
		[ProtoEnum(Name = "SLG_BATTLE_POINT_NOT_OPEN")]
		SlgBattlePointNotOpen = 85,
		[ProtoEnum(Name = "UNLOCK_TIME_LIMIT")]
		UnlockTimeLimit = 86,
		[ProtoEnum(Name = "BEGIN_FLOOR_ERROR")]
		BeginFloorError = 87,
		[ProtoEnum(Name = "OPENWORLD_STORY_NOT_FINISH")]
		OpenworldStoryNotFinish = 88,
		[ProtoEnum(Name = "MONSTER_CARD_MONSTER_ERROR")]
		MonsterCardMonsterError = 89,
		[ProtoEnum(Name = "MONSTER_CARD_COST_LIMIT")]
		MonsterCardCostLimit = 90,
		[ProtoEnum(Name = "CHAPTER_OW_FAME_LEVEL_LACK")]
		ChapterOwFameLevelLack = 91,
		[ProtoEnum(Name = "BUFF_ASSIST_STAGE_NOT_OPEN")]
		BuffAssistStageNotOpen = 92,
		[ProtoEnum(Name = "WAVE_RUSH_STAGE_NOT_OPEN")]
		WaveRushStageNotOpen = 93,
		[ProtoEnum(Name = "WAVE_RUSH_PRE_SITE_NOT_FINISHED")]
		WaveRushPreSiteNotFinished = 94,
		[ProtoEnum(Name = "WAVE_RUSH_DAILY_STAGE_NOT_OPEN")]
		WaveRushDailyStageNotOpen = 95,
		[ProtoEnum(Name = "AI_CYBER_CONDITION_NOT_MEET")]
		AiCyberConditionNotMeet = 96,
		[ProtoEnum(Name = "AI_CYBER_AREA_ERROR")]
		AiCyberAreaError = 97,
		[ProtoEnum(Name = "AI_CYBER_DAILY_STAGE_NOT_MATCH")]
		AiCyberDailyStageNotMatch = 98,
		[ProtoEnum(Name = "FAKE_AVATAR_NUM_ERROR")]
		FakeAvatarNumError = 99,
		[ProtoEnum(Name = "FAKE_AVATAR_ERROR")]
		FakeAvatarError = 100,
		[ProtoEnum(Name = "PLATFORM_SHOOTER_DAILY_CHALLENGE_MISMATCH")]
		PlatformShooterDailyChallengeMismatch = 101,
		[ProtoEnum(Name = "MIRAGE_STAGE_MISMATCH")]
		MirageStageMismatch = 102,
		[ProtoEnum(Name = "MIRAGE_NOT_OPEN")]
		MirageNotOpen = 103,
		[ProtoEnum(Name = "EXTRA_ITEM_ERROR")]
		ExtraItemError = 104,
		[ProtoEnum(Name = "CHAPTER_BW_WORLD_EXP_MATERIAL_FULL")]
		ChapterBwWorldExpMaterialFull = 105,
		[ProtoEnum(Name = "CHAPTER_BW_WORLD_RUNE_FULL")]
		ChapterBwWorldRuneFull = 106,
		[ProtoEnum(Name = "REWARD_LINE_ERROR")]
		RewardLineError = 107,
		[ProtoEnum(Name = "CHAPTER_BW_WORLD_EQUIP_RUNE_ERROR")]
		ChapterBwWorldEquipRuneError = 108
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__Progress;

	private bool? __pbn__IsCollectCheatData;

	private string __pbn__SignKey;

	private uint? __pbn__Tag;

	private bool? __pbn__IsTriggerBaodi;

	private byte[] __pbn__SecurityCmdBuffer;

	private string __pbn__StageTransactionStr;

	private uint? __pbn__GodWarSkillId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(3, Name = "progress")]
	public uint Progress
	{
		get
		{
			return __pbn__Progress.GetValueOrDefault();
		}
		set
		{
			__pbn__Progress = value;
		}
	}

	[ProtoMember(4, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	[ProtoMember(5, Name = "is_collect_cheat_data")]
	public bool IsCollectCheatData
	{
		get
		{
			return __pbn__IsCollectCheatData.GetValueOrDefault();
		}
		set
		{
			__pbn__IsCollectCheatData = value;
		}
	}

	[ProtoMember(6, Name = "sign_key")]
	[DefaultValue("")]
	public string SignKey
	{
		get
		{
			return __pbn__SignKey ?? "";
		}
		set
		{
			__pbn__SignKey = value;
		}
	}

	[ProtoMember(7, Name = "tag")]
	public uint Tag
	{
		get
		{
			return __pbn__Tag.GetValueOrDefault();
		}
		set
		{
			__pbn__Tag = value;
		}
	}

	[ProtoMember(8, Name = "equip_buff_list")]
	public List<AvatarEquipBuff> EquipBuffLists { get; } = new List<AvatarEquipBuff>();


	[ProtoMember(9, Name = "battle_buff_list")]
	public List<string> BattleBuffLists { get; } = new List<string>();


	[ProtoMember(10, Name = "battle_attr_list")]
	public List<string> BattleAttrLists { get; } = new List<string>();


	[ProtoMember(11, Name = "is_trigger_baodi")]
	public bool IsTriggerBaodi
	{
		get
		{
			return __pbn__IsTriggerBaodi.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTriggerBaodi = value;
		}
	}

	[ProtoMember(12, Name = "security_cmd_buffer")]
	public byte[] SecurityCmdBuffer
	{
		get
		{
			return __pbn__SecurityCmdBuffer;
		}
		set
		{
			__pbn__SecurityCmdBuffer = value;
		}
	}

	[ProtoMember(13, Name = "stage_transaction_str")]
	[DefaultValue("")]
	public string StageTransactionStr
	{
		get
		{
			return __pbn__StageTransactionStr ?? "";
		}
		set
		{
			__pbn__StageTransactionStr = value;
		}
	}

	[ProtoMember(14, Name = "god_war_skill_id")]
	public uint GodWarSkillId
	{
		get
		{
			return __pbn__GodWarSkillId.GetValueOrDefault();
		}
		set
		{
			__pbn__GodWarSkillId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeIsCollectCheatData()
	{
		return __pbn__IsCollectCheatData.HasValue;
	}

	public void ResetIsCollectCheatData()
	{
		__pbn__IsCollectCheatData = null;
	}

	public bool ShouldSerializeSignKey()
	{
		return __pbn__SignKey != null;
	}

	public void ResetSignKey()
	{
		__pbn__SignKey = null;
	}

	public bool ShouldSerializeTag()
	{
		return __pbn__Tag.HasValue;
	}

	public void ResetTag()
	{
		__pbn__Tag = null;
	}

	public bool ShouldSerializeIsTriggerBaodi()
	{
		return __pbn__IsTriggerBaodi.HasValue;
	}

	public void ResetIsTriggerBaodi()
	{
		__pbn__IsTriggerBaodi = null;
	}

	public bool ShouldSerializeSecurityCmdBuffer()
	{
		return __pbn__SecurityCmdBuffer != null;
	}

	public void ResetSecurityCmdBuffer()
	{
		__pbn__SecurityCmdBuffer = null;
	}

	public bool ShouldSerializeStageTransactionStr()
	{
		return __pbn__StageTransactionStr != null;
	}

	public void ResetStageTransactionStr()
	{
		__pbn__StageTransactionStr = null;
	}

	public bool ShouldSerializeGodWarSkillId()
	{
		return __pbn__GodWarSkillId.HasValue;
	}

	public void ResetGodWarSkillId()
	{
		__pbn__GodWarSkillId = null;
	}
}
