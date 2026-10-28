using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageEndRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 46
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ = 0,
		[ProtoEnum(Name = "FAIL")]
		Fail = 1,
		[ProtoEnum(Name = "STAGE_ERROR")]
		StageError = 2,
		[ProtoEnum(Name = "DROP_ERROR")]
		DropError = 3,
		[ProtoEnum(Name = "CHALLENGE_ERROR")]
		ChallengeError = 4,
		[ProtoEnum(Name = "STAGE_TIME_ERROR")]
		StageTimeError = 5,
		[ProtoEnum(Name = "ALREADY_OPEN_DUNGEONS")]
		AlreadyOpenDungeons = 6,
		[ProtoEnum(Name = "NOT_MEET_FAST")]
		NotMeetFast = 22,
		[ProtoEnum(Name = "COST_MATERIAL_LACK")]
		CostMaterialLack = 40,
		[ProtoEnum(Name = "NOT_IN_TEAM")]
		NotInTeam = 62,
		[ProtoEnum(Name = "SLG_BATTLE_NOT_OPEN")]
		SlgBattleNotOpen = 63
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__PlayerExpReward;

	private uint? __pbn__AvatarExpReward;

	private uint? __pbn__ScoinReward;

	private uint? __pbn__FriendsPointAdd;

	private uint? __pbn__Progress;

	private uint? __pbn__MpExpReward;

	private uint? __pbn__ExpConvertScoin;

	private uint? __pbn__BondAddFriendsPoint;

	private uint? __pbn__WorldBossScore;

	private uint? __pbn__RankScoin;

	private uint? __pbn__DungeonsEvaluation;

	private StageEndStatus? __pbn__EndStatus;

	private uint? __pbn__BonusScore;

	private bool? __pbn__IsNewMaxScore;

	private bool? __pbn__IsThemeWantedExtraDropTakenByFirstPass;

	private uint? __pbn__GodWarChallengeScore;

	private uint? __pbn__OldMaxScore;

	private uint? __pbn__StageScore;

	private bool? __pbn__IsFirstWin;

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

	[ProtoMember(3, Name = "player_exp_reward")]
	public uint PlayerExpReward
	{
		get
		{
			return __pbn__PlayerExpReward.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerExpReward = value;
		}
	}

	[ProtoMember(4, Name = "avatar_exp_reward")]
	public uint AvatarExpReward
	{
		get
		{
			return __pbn__AvatarExpReward.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarExpReward = value;
		}
	}

	[ProtoMember(5, Name = "scoin_reward")]
	public uint ScoinReward
	{
		get
		{
			return __pbn__ScoinReward.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoinReward = value;
		}
	}

	[ProtoMember(6, Name = "challenge_list")]
	public List<StageChallengeData> ChallengeLists { get; } = new List<StageChallengeData>();


	[ProtoMember(7, Name = "friends_point_add")]
	public uint FriendsPointAdd
	{
		get
		{
			return __pbn__FriendsPointAdd.GetValueOrDefault();
		}
		set
		{
			__pbn__FriendsPointAdd = value;
		}
	}

	[ProtoMember(8, Name = "progress")]
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

	[ProtoMember(9, Name = "special_challenge_list")]
	public List<StageSpecialChallengeData> SpecialChallengeLists { get; } = new List<StageSpecialChallengeData>();


	[ProtoMember(10, Name = "mvp_reward")]
	public MpStageMvpReward MvpReward { get; set; }

	[ProtoMember(11, Name = "mp_drop_item_list")]
	public List<DropItem> MpDropItemLists { get; } = new List<DropItem>();


	[ProtoMember(12, Name = "mp_exp_reward")]
	public uint MpExpReward
	{
		get
		{
			return __pbn__MpExpReward.GetValueOrDefault();
		}
		set
		{
			__pbn__MpExpReward = value;
		}
	}

	[ProtoMember(13, Name = "exp_convert_scoin")]
	public uint ExpConvertScoin
	{
		get
		{
			return __pbn__ExpConvertScoin.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpConvertScoin = value;
		}
	}

	[ProtoMember(14, Name = "bond_add_friends_point")]
	public uint BondAddFriendsPoint
	{
		get
		{
			return __pbn__BondAddFriendsPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__BondAddFriendsPoint = value;
		}
	}

	[ProtoMember(16, Name = "buff_reward")]
	public AccountBuffReward BuffReward { get; set; }

	[ProtoMember(17, Name = "world_boss_score")]
	public uint WorldBossScore
	{
		get
		{
			return __pbn__WorldBossScore.GetValueOrDefault();
		}
		set
		{
			__pbn__WorldBossScore = value;
		}
	}

	[ProtoMember(18, Name = "rank_scoin")]
	public uint RankScoin
	{
		get
		{
			return __pbn__RankScoin.GetValueOrDefault();
		}
		set
		{
			__pbn__RankScoin = value;
		}
	}

	[ProtoMember(19, Name = "line_enhance_reward_data")]
	public RewardData LineEnhanceRewardData { get; set; }

	[ProtoMember(20, Name = "dungeons_evaluation")]
	public uint DungeonsEvaluation
	{
		get
		{
			return __pbn__DungeonsEvaluation.GetValueOrDefault();
		}
		set
		{
			__pbn__DungeonsEvaluation = value;
		}
	}

	[ProtoMember(21, Name = "dungeons_drop_item_list")]
	public List<DropItem> DungeonsDropItemLists { get; } = new List<DropItem>();


	[ProtoMember(22, Name = "end_status")]
	[DefaultValue(StageEndStatus.StageWin)]
	public StageEndStatus EndStatus
	{
		get
		{
			return __pbn__EndStatus ?? StageEndStatus.StageWin;
		}
		set
		{
			__pbn__EndStatus = value;
		}
	}

	[ProtoMember(24, Name = "bonus_score")]
	public uint BonusScore
	{
		get
		{
			return __pbn__BonusScore.GetValueOrDefault();
		}
		set
		{
			__pbn__BonusScore = value;
		}
	}

	[ProtoMember(25, Name = "bonus_drop_list")]
	public List<DropItem> BonusDropLists { get; } = new List<DropItem>();


	[ProtoMember(26, Name = "gratuity_drop_list")]
	public List<DropItem> GratuityDropLists { get; } = new List<DropItem>();


	[ProtoMember(27, Name = "extra_reward_data_list")]
	public List<RewardData> ExtraRewardDataLists { get; } = new List<RewardData>();


	[ProtoMember(28, Name = "extra_drop_list")]
	public List<DropItem> ExtraDropLists { get; } = new List<DropItem>();


	[ProtoMember(29, Name = "is_new_max_score")]
	public bool IsNewMaxScore
	{
		get
		{
			return __pbn__IsNewMaxScore.GetValueOrDefault();
		}
		set
		{
			__pbn__IsNewMaxScore = value;
		}
	}

	[ProtoMember(31, Name = "theme_wanted_drop_item_list")]
	public List<DropItem> ThemeWantedDropItemLists { get; } = new List<DropItem>();


	[ProtoMember(32, Name = "theme_wanted_show_extra_drop_item_list")]
	public List<DropItem> ThemeWantedShowExtraDropItemLists { get; } = new List<DropItem>();


	[ProtoMember(33, Name = "is_theme_wanted_extra_drop_taken_by_first_pass")]
	public bool IsThemeWantedExtraDropTakenByFirstPass
	{
		get
		{
			return __pbn__IsThemeWantedExtraDropTakenByFirstPass.GetValueOrDefault();
		}
		set
		{
			__pbn__IsThemeWantedExtraDropTakenByFirstPass = value;
		}
	}

	[ProtoMember(34, Name = "god_war_challenge_score")]
	public uint GodWarChallengeScore
	{
		get
		{
			return __pbn__GodWarChallengeScore.GetValueOrDefault();
		}
		set
		{
			__pbn__GodWarChallengeScore = value;
		}
	}

	[ProtoMember(35, Name = "old_max_score")]
	public uint OldMaxScore
	{
		get
		{
			return __pbn__OldMaxScore.GetValueOrDefault();
		}
		set
		{
			__pbn__OldMaxScore = value;
		}
	}

	[ProtoMember(36, Name = "stage_score")]
	public uint StageScore
	{
		get
		{
			return __pbn__StageScore.GetValueOrDefault();
		}
		set
		{
			__pbn__StageScore = value;
		}
	}

	[ProtoMember(37, Name = "is_first_win")]
	public bool IsFirstWin
	{
		get
		{
			return __pbn__IsFirstWin.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFirstWin = value;
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

	public bool ShouldSerializePlayerExpReward()
	{
		return __pbn__PlayerExpReward.HasValue;
	}

	public void ResetPlayerExpReward()
	{
		__pbn__PlayerExpReward = null;
	}

	public bool ShouldSerializeAvatarExpReward()
	{
		return __pbn__AvatarExpReward.HasValue;
	}

	public void ResetAvatarExpReward()
	{
		__pbn__AvatarExpReward = null;
	}

	public bool ShouldSerializeScoinReward()
	{
		return __pbn__ScoinReward.HasValue;
	}

	public void ResetScoinReward()
	{
		__pbn__ScoinReward = null;
	}

	public bool ShouldSerializeFriendsPointAdd()
	{
		return __pbn__FriendsPointAdd.HasValue;
	}

	public void ResetFriendsPointAdd()
	{
		__pbn__FriendsPointAdd = null;
	}

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeMpExpReward()
	{
		return __pbn__MpExpReward.HasValue;
	}

	public void ResetMpExpReward()
	{
		__pbn__MpExpReward = null;
	}

	public bool ShouldSerializeExpConvertScoin()
	{
		return __pbn__ExpConvertScoin.HasValue;
	}

	public void ResetExpConvertScoin()
	{
		__pbn__ExpConvertScoin = null;
	}

	public bool ShouldSerializeBondAddFriendsPoint()
	{
		return __pbn__BondAddFriendsPoint.HasValue;
	}

	public void ResetBondAddFriendsPoint()
	{
		__pbn__BondAddFriendsPoint = null;
	}

	public bool ShouldSerializeWorldBossScore()
	{
		return __pbn__WorldBossScore.HasValue;
	}

	public void ResetWorldBossScore()
	{
		__pbn__WorldBossScore = null;
	}

	public bool ShouldSerializeRankScoin()
	{
		return __pbn__RankScoin.HasValue;
	}

	public void ResetRankScoin()
	{
		__pbn__RankScoin = null;
	}

	public bool ShouldSerializeDungeonsEvaluation()
	{
		return __pbn__DungeonsEvaluation.HasValue;
	}

	public void ResetDungeonsEvaluation()
	{
		__pbn__DungeonsEvaluation = null;
	}

	public bool ShouldSerializeEndStatus()
	{
		return __pbn__EndStatus.HasValue;
	}

	public void ResetEndStatus()
	{
		__pbn__EndStatus = null;
	}

	public bool ShouldSerializeBonusScore()
	{
		return __pbn__BonusScore.HasValue;
	}

	public void ResetBonusScore()
	{
		__pbn__BonusScore = null;
	}

	public bool ShouldSerializeIsNewMaxScore()
	{
		return __pbn__IsNewMaxScore.HasValue;
	}

	public void ResetIsNewMaxScore()
	{
		__pbn__IsNewMaxScore = null;
	}

	public bool ShouldSerializeIsThemeWantedExtraDropTakenByFirstPass()
	{
		return __pbn__IsThemeWantedExtraDropTakenByFirstPass.HasValue;
	}

	public void ResetIsThemeWantedExtraDropTakenByFirstPass()
	{
		__pbn__IsThemeWantedExtraDropTakenByFirstPass = null;
	}

	public bool ShouldSerializeGodWarChallengeScore()
	{
		return __pbn__GodWarChallengeScore.HasValue;
	}

	public void ResetGodWarChallengeScore()
	{
		__pbn__GodWarChallengeScore = null;
	}

	public bool ShouldSerializeOldMaxScore()
	{
		return __pbn__OldMaxScore.HasValue;
	}

	public void ResetOldMaxScore()
	{
		__pbn__OldMaxScore = null;
	}

	public bool ShouldSerializeStageScore()
	{
		return __pbn__StageScore.HasValue;
	}

	public void ResetStageScore()
	{
		__pbn__StageScore = null;
	}

	public bool ShouldSerializeIsFirstWin()
	{
		return __pbn__IsFirstWin.HasValue;
	}

	public void ResetIsFirstWin()
	{
		__pbn__IsFirstWin = null;
	}
}
