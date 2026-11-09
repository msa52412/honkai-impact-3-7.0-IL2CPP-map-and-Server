using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageEndReqBody : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private StageEndStatus? __pbn__EndStatus;

	private bool? __pbn__IsMvp;

	private uint? __pbn__AvatarExpReward;

	private uint? __pbn__ScoinReward;

	private uint? __pbn__StagePassTime;

	private uint? __pbn__MaxComboNum;

	private uint? __pbn__Score;

	private uint? __pbn__ClientAverageRtt;

	private uint? __pbn__ClientMaxRtt;

	private uint? __pbn__BeHitTimes;

	private uint? __pbn__TeamId;

	private uint? __pbn__MvpScore;

	private uint? __pbn__EvadeTimes;

	private uint? __pbn__EvadeSuccessTimes;

	private uint? __pbn__BossDamage;

	private uint? __pbn__BattleScore;

	private uint? __pbn__Rank;

	private uint? __pbn__DungeonsEvaluation;

	private byte[] __pbn__SecurityCmdReply;

	private uint? __pbn__StageEndFloor;

	[ProtoMember(1, Name = "stage_id")]
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

	[ProtoMember(2, Name = "end_status")]
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

	[ProtoMember(3, Name = "is_mvp")]
	public bool IsMvp
	{
		get
		{
			return __pbn__IsMvp.GetValueOrDefault();
		}
		set
		{
			__pbn__IsMvp = value;
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

	[ProtoMember(6, Name = "challenge_index_list")]
	public uint[] ChallengeIndexLists { get; set; }

	[ProtoMember(7, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	[ProtoMember(8, Name = "cheat_data_list")]
	public List<StageCheatData> CheatDataLists { get; } = new List<StageCheatData>();


	[ProtoMember(10, Name = "stage_pass_time")]
	public uint StagePassTime
	{
		get
		{
			return __pbn__StagePassTime.GetValueOrDefault();
		}
		set
		{
			__pbn__StagePassTime = value;
		}
	}

	[ProtoMember(11, Name = "max_combo_num")]
	public uint MaxComboNum
	{
		get
		{
			return __pbn__MaxComboNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxComboNum = value;
		}
	}

	[ProtoMember(12, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(13, Name = "client_average_rtt")]
	public uint ClientAverageRtt
	{
		get
		{
			return __pbn__ClientAverageRtt.GetValueOrDefault();
		}
		set
		{
			__pbn__ClientAverageRtt = value;
		}
	}

	[ProtoMember(14, Name = "client_max_rtt")]
	public uint ClientMaxRtt
	{
		get
		{
			return __pbn__ClientMaxRtt.GetValueOrDefault();
		}
		set
		{
			__pbn__ClientMaxRtt = value;
		}
	}

	[ProtoMember(15, Name = "be_hit_times")]
	public uint BeHitTimes
	{
		get
		{
			return __pbn__BeHitTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__BeHitTimes = value;
		}
	}

	[ProtoMember(16, Name = "team_id")]
	public uint TeamId
	{
		get
		{
			return __pbn__TeamId.GetValueOrDefault();
		}
		set
		{
			__pbn__TeamId = value;
		}
	}

	[ProtoMember(17, Name = "mvp_score")]
	public uint MvpScore
	{
		get
		{
			return __pbn__MvpScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MvpScore = value;
		}
	}

	[ProtoMember(18, Name = "avatar_list")]
	public List<ChallengeModeAvatar> AvatarLists { get; } = new List<ChallengeModeAvatar>();


	[ProtoMember(19, Name = "evade_times")]
	public uint EvadeTimes
	{
		get
		{
			return __pbn__EvadeTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__EvadeTimes = value;
		}
	}

	[ProtoMember(20, Name = "evade_success_times")]
	public uint EvadeSuccessTimes
	{
		get
		{
			return __pbn__EvadeSuccessTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__EvadeSuccessTimes = value;
		}
	}

	[ProtoMember(21, Name = "boss_damage")]
	public uint BossDamage
	{
		get
		{
			return __pbn__BossDamage.GetValueOrDefault();
		}
		set
		{
			__pbn__BossDamage = value;
		}
	}

	[ProtoMember(23, Name = "battle_score")]
	public uint BattleScore
	{
		get
		{
			return __pbn__BattleScore.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleScore = value;
		}
	}

	[ProtoMember(24, Name = "rank")]
	public uint Rank
	{
		get
		{
			return __pbn__Rank.GetValueOrDefault();
		}
		set
		{
			__pbn__Rank = value;
		}
	}

	[ProtoMember(25, Name = "mp_stage_record")]
	public MpStageRecord MpStageRecord { get; set; }

	[ProtoMember(26, Name = "dungeons_evaluation")]
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

	[ProtoMember(27, Name = "fight_info")]
	public StageFightInfo FightInfo { get; set; }

	[ProtoMember(28, Name = "mp_stage_damage_info")]
	public MpStageDamageInfo MpStageDamageInfo { get; set; }

	[ProtoMember(29, Name = "security_cmd_reply")]
	public byte[] SecurityCmdReply
	{
		get
		{
			return __pbn__SecurityCmdReply;
		}
		set
		{
			__pbn__SecurityCmdReply = value;
		}
	}

	[ProtoMember(30, Name = "buff_id_list")]
	public uint[] BuffIdLists { get; set; }

	[ProtoMember(31, Name = "stage_end_floor")]
	public uint StageEndFloor
	{
		get
		{
			return __pbn__StageEndFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__StageEndFloor = value;
		}
	}

	[ProtoMember(32, Name = "god_war_status")]
	public GodWarStatus GodWarStatus { get; set; }

	[ProtoMember(33, Name = "sc_dlc_status")]
	public ScDLCStatus ScDlcStatus { get; set; }

	[ProtoMember(34, Name = "ow_chapter_status")]
	public OwChapterStatus OwChapterStatus { get; set; }

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

	public bool ShouldSerializeEndStatus()
	{
		return __pbn__EndStatus.HasValue;
	}

	public void ResetEndStatus()
	{
		__pbn__EndStatus = null;
	}

	public bool ShouldSerializeIsMvp()
	{
		return __pbn__IsMvp.HasValue;
	}

	public void ResetIsMvp()
	{
		__pbn__IsMvp = null;
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

	public bool ShouldSerializeStagePassTime()
	{
		return __pbn__StagePassTime.HasValue;
	}

	public void ResetStagePassTime()
	{
		__pbn__StagePassTime = null;
	}

	public bool ShouldSerializeMaxComboNum()
	{
		return __pbn__MaxComboNum.HasValue;
	}

	public void ResetMaxComboNum()
	{
		__pbn__MaxComboNum = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeClientAverageRtt()
	{
		return __pbn__ClientAverageRtt.HasValue;
	}

	public void ResetClientAverageRtt()
	{
		__pbn__ClientAverageRtt = null;
	}

	public bool ShouldSerializeClientMaxRtt()
	{
		return __pbn__ClientMaxRtt.HasValue;
	}

	public void ResetClientMaxRtt()
	{
		__pbn__ClientMaxRtt = null;
	}

	public bool ShouldSerializeBeHitTimes()
	{
		return __pbn__BeHitTimes.HasValue;
	}

	public void ResetBeHitTimes()
	{
		__pbn__BeHitTimes = null;
	}

	public bool ShouldSerializeTeamId()
	{
		return __pbn__TeamId.HasValue;
	}

	public void ResetTeamId()
	{
		__pbn__TeamId = null;
	}

	public bool ShouldSerializeMvpScore()
	{
		return __pbn__MvpScore.HasValue;
	}

	public void ResetMvpScore()
	{
		__pbn__MvpScore = null;
	}

	public bool ShouldSerializeEvadeTimes()
	{
		return __pbn__EvadeTimes.HasValue;
	}

	public void ResetEvadeTimes()
	{
		__pbn__EvadeTimes = null;
	}

	public bool ShouldSerializeEvadeSuccessTimes()
	{
		return __pbn__EvadeSuccessTimes.HasValue;
	}

	public void ResetEvadeSuccessTimes()
	{
		__pbn__EvadeSuccessTimes = null;
	}

	public bool ShouldSerializeBossDamage()
	{
		return __pbn__BossDamage.HasValue;
	}

	public void ResetBossDamage()
	{
		__pbn__BossDamage = null;
	}

	public bool ShouldSerializeBattleScore()
	{
		return __pbn__BattleScore.HasValue;
	}

	public void ResetBattleScore()
	{
		__pbn__BattleScore = null;
	}

	public bool ShouldSerializeRank()
	{
		return __pbn__Rank.HasValue;
	}

	public void ResetRank()
	{
		__pbn__Rank = null;
	}

	public bool ShouldSerializeDungeonsEvaluation()
	{
		return __pbn__DungeonsEvaluation.HasValue;
	}

	public void ResetDungeonsEvaluation()
	{
		__pbn__DungeonsEvaluation = null;
	}

	public bool ShouldSerializeSecurityCmdReply()
	{
		return __pbn__SecurityCmdReply != null;
	}

	public void ResetSecurityCmdReply()
	{
		__pbn__SecurityCmdReply = null;
	}

	public bool ShouldSerializeStageEndFloor()
	{
		return __pbn__StageEndFloor.HasValue;
	}

	public void ResetStageEndFloor()
	{
		__pbn__StageEndFloor = null;
	}
}
