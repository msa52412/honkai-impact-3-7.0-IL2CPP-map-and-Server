using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ResetGodWarChallengeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6158
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "FEATURE_CLOSE")]
		FeatureClose,
		[ProtoEnum(Name = "FORCE_SETTLE")]
		ForceSettle,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GodWarId;

	private uint? __pbn__TaleId;

	private bool? __pbn__IsChallengeSucc;

	private uint? __pbn__ChallengeScore;

	private uint? __pbn__PunishLevel;

	private uint? __pbn__TotalScore;

	private uint? __pbn__Grade;

	private uint? __pbn__SiteIdx;

	private bool? __pbn__IsUseVirtualAvatar;

	private uint? __pbn__ChallengeCostTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "god_war_id")]
	public uint GodWarId
	{
		get
		{
			return __pbn__GodWarId.GetValueOrDefault();
		}
		set
		{
			__pbn__GodWarId = value;
		}
	}

	[ProtoMember(3, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
		}
	}

	[ProtoMember(4, Name = "avatar_list")]
	public List<GodWarChallengeAvatar> AvatarLists { get; } = new List<GodWarChallengeAvatar>();


	[ProtoMember(5, Name = "buff_list")]
	public List<GodWarBuff> BuffLists { get; } = new List<GodWarBuff>();


	[ProtoMember(6, Name = "is_challenge_succ")]
	public bool IsChallengeSucc
	{
		get
		{
			return __pbn__IsChallengeSucc.GetValueOrDefault();
		}
		set
		{
			__pbn__IsChallengeSucc = value;
		}
	}

	[ProtoMember(7, Name = "challenge_score")]
	public uint ChallengeScore
	{
		get
		{
			return __pbn__ChallengeScore.GetValueOrDefault();
		}
		set
		{
			__pbn__ChallengeScore = value;
		}
	}

	[ProtoMember(8, Name = "score_id_settle_times_list")]
	public List<GodWarScoreEventSettle> ScoreIdSettleTimesLists { get; } = new List<GodWarScoreEventSettle>();


	[ProtoMember(9, Name = "punish_level")]
	public uint PunishLevel
	{
		get
		{
			return __pbn__PunishLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__PunishLevel = value;
		}
	}

	[ProtoMember(11, Name = "total_score")]
	public uint TotalScore
	{
		get
		{
			return __pbn__TotalScore.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalScore = value;
		}
	}

	[ProtoMember(12, Name = "grade")]
	public uint Grade
	{
		get
		{
			return __pbn__Grade.GetValueOrDefault();
		}
		set
		{
			__pbn__Grade = value;
		}
	}

	[ProtoMember(13, Name = "reward_material_list")]
	public List<Material> RewardMaterialLists { get; } = new List<Material>();


	[ProtoMember(14, Name = "site_idx")]
	public uint SiteIdx
	{
		get
		{
			return __pbn__SiteIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__SiteIdx = value;
		}
	}

	[ProtoMember(15, Name = "first_win_reward_list")]
	public List<RewardData> FirstWinRewardLists { get; } = new List<RewardData>();


	[ProtoMember(16, Name = "is_use_virtual_avatar")]
	public bool IsUseVirtualAvatar
	{
		get
		{
			return __pbn__IsUseVirtualAvatar.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUseVirtualAvatar = value;
		}
	}

	[ProtoMember(17, Name = "challenge_cost_time")]
	public uint ChallengeCostTime
	{
		get
		{
			return __pbn__ChallengeCostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ChallengeCostTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGodWarId()
	{
		return __pbn__GodWarId.HasValue;
	}

	public void ResetGodWarId()
	{
		__pbn__GodWarId = null;
	}

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}

	public bool ShouldSerializeIsChallengeSucc()
	{
		return __pbn__IsChallengeSucc.HasValue;
	}

	public void ResetIsChallengeSucc()
	{
		__pbn__IsChallengeSucc = null;
	}

	public bool ShouldSerializeChallengeScore()
	{
		return __pbn__ChallengeScore.HasValue;
	}

	public void ResetChallengeScore()
	{
		__pbn__ChallengeScore = null;
	}

	public bool ShouldSerializePunishLevel()
	{
		return __pbn__PunishLevel.HasValue;
	}

	public void ResetPunishLevel()
	{
		__pbn__PunishLevel = null;
	}

	public bool ShouldSerializeTotalScore()
	{
		return __pbn__TotalScore.HasValue;
	}

	public void ResetTotalScore()
	{
		__pbn__TotalScore = null;
	}

	public bool ShouldSerializeGrade()
	{
		return __pbn__Grade.HasValue;
	}

	public void ResetGrade()
	{
		__pbn__Grade = null;
	}

	public bool ShouldSerializeSiteIdx()
	{
		return __pbn__SiteIdx.HasValue;
	}

	public void ResetSiteIdx()
	{
		__pbn__SiteIdx = null;
	}

	public bool ShouldSerializeIsUseVirtualAvatar()
	{
		return __pbn__IsUseVirtualAvatar.HasValue;
	}

	public void ResetIsUseVirtualAvatar()
	{
		__pbn__IsUseVirtualAvatar = null;
	}

	public bool ShouldSerializeChallengeCostTime()
	{
		return __pbn__ChallengeCostTime.HasValue;
	}

	public void ResetChallengeCostTime()
	{
		__pbn__ChallengeCostTime = null;
	}
}
