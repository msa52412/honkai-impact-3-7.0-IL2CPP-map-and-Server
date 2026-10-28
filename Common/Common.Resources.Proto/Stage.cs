using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Stage : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__Progress;

	private uint? __pbn__EnterTimes;

	private uint? __pbn__ResetTimes;

	private uint? __pbn__BonusEndTime;

	private uint? __pbn__BonusEnterTimes;

	private uint? __pbn__BonusTotalTimes;

	private uint? __pbn__MinStageTime;

	private uint? __pbn__MaxScore;

	private uint? __pbn__StaminaDiscount;

	private uint? __pbn__TotalScore;

	private uint? __pbn__MpRaidPassAvatarUid;

	private uint? __pbn__MpRaidWeaponUid;

	private bool? __pbn__IsDone;

	private uint? __pbn__AssistEnterTimes;

	private uint? __pbn__MaxBattleScore;

	private uint? __pbn__MaxRank;

	private string __pbn__ClientData;

	private uint? __pbn__FirstStaminaDiscount;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "progress")]
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

	[ProtoMember(3, Name = "enter_times")]
	public uint EnterTimes
	{
		get
		{
			return __pbn__EnterTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__EnterTimes = value;
		}
	}

	[ProtoMember(4, Name = "reset_times")]
	public uint ResetTimes
	{
		get
		{
			return __pbn__ResetTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ResetTimes = value;
		}
	}

	[ProtoMember(5, Name = "challenge_index_list")]
	public uint[] ChallengeIndexLists { get; set; }

	[ProtoMember(6, Name = "bonus_end_time")]
	public uint BonusEndTime
	{
		get
		{
			return __pbn__BonusEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BonusEndTime = value;
		}
	}

	[ProtoMember(7, Name = "bonus_enter_times")]
	public uint BonusEnterTimes
	{
		get
		{
			return __pbn__BonusEnterTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__BonusEnterTimes = value;
		}
	}

	[ProtoMember(8, Name = "bonus_total_times")]
	public uint BonusTotalTimes
	{
		get
		{
			return __pbn__BonusTotalTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__BonusTotalTimes = value;
		}
	}

	[ProtoMember(9, Name = "min_stage_time")]
	public uint MinStageTime
	{
		get
		{
			return __pbn__MinStageTime.GetValueOrDefault();
		}
		set
		{
			__pbn__MinStageTime = value;
		}
	}

	[ProtoMember(10, Name = "max_score")]
	public uint MaxScore
	{
		get
		{
			return __pbn__MaxScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxScore = value;
		}
	}

	[ProtoMember(11, Name = "stamina_discount")]
	public uint StaminaDiscount
	{
		get
		{
			return __pbn__StaminaDiscount.GetValueOrDefault();
		}
		set
		{
			__pbn__StaminaDiscount = value;
		}
	}

	[ProtoMember(12, Name = "total_score")]
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

	[ProtoMember(13, Name = "mp_raid_pass_avatar_uid")]
	public uint MpRaidPassAvatarUid
	{
		get
		{
			return __pbn__MpRaidPassAvatarUid.GetValueOrDefault();
		}
		set
		{
			__pbn__MpRaidPassAvatarUid = value;
		}
	}

	[ProtoMember(14, Name = "mp_raid_weapon_uid")]
	public uint MpRaidWeaponUid
	{
		get
		{
			return __pbn__MpRaidWeaponUid.GetValueOrDefault();
		}
		set
		{
			__pbn__MpRaidWeaponUid = value;
		}
	}

	[ProtoMember(15, Name = "mp_raid_stigmata_uid_set")]
	public uint[] MpRaidStigmataUidSets { get; set; }

	[ProtoMember(16, Name = "is_done")]
	public bool IsDone
	{
		get
		{
			return __pbn__IsDone.GetValueOrDefault();
		}
		set
		{
			__pbn__IsDone = value;
		}
	}

	[ProtoMember(17, Name = "assist_enter_times")]
	public uint AssistEnterTimes
	{
		get
		{
			return __pbn__AssistEnterTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__AssistEnterTimes = value;
		}
	}

	[ProtoMember(18, Name = "backup_challenge_index_list")]
	public uint[] BackupChallengeIndexLists { get; set; }

	[ProtoMember(19, Name = "max_battle_score")]
	public uint MaxBattleScore
	{
		get
		{
			return __pbn__MaxBattleScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxBattleScore = value;
		}
	}

	[ProtoMember(20, Name = "max_rank")]
	public uint MaxRank
	{
		get
		{
			return __pbn__MaxRank.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxRank = value;
		}
	}

	[ProtoMember(21, Name = "client_data")]
	[DefaultValue("")]
	public string ClientData
	{
		get
		{
			return __pbn__ClientData ?? "";
		}
		set
		{
			__pbn__ClientData = value;
		}
	}

	[ProtoMember(22, Name = "first_stamina_discount")]
	public uint FirstStaminaDiscount
	{
		get
		{
			return __pbn__FirstStaminaDiscount.GetValueOrDefault();
		}
		set
		{
			__pbn__FirstStaminaDiscount = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeProgress()
	{
		return __pbn__Progress.HasValue;
	}

	public void ResetProgress()
	{
		__pbn__Progress = null;
	}

	public bool ShouldSerializeEnterTimes()
	{
		return __pbn__EnterTimes.HasValue;
	}

	public void ResetEnterTimes()
	{
		__pbn__EnterTimes = null;
	}

	public bool ShouldSerializeResetTimes()
	{
		return __pbn__ResetTimes.HasValue;
	}

	public void ResetResetTimes()
	{
		__pbn__ResetTimes = null;
	}

	public bool ShouldSerializeBonusEndTime()
	{
		return __pbn__BonusEndTime.HasValue;
	}

	public void ResetBonusEndTime()
	{
		__pbn__BonusEndTime = null;
	}

	public bool ShouldSerializeBonusEnterTimes()
	{
		return __pbn__BonusEnterTimes.HasValue;
	}

	public void ResetBonusEnterTimes()
	{
		__pbn__BonusEnterTimes = null;
	}

	public bool ShouldSerializeBonusTotalTimes()
	{
		return __pbn__BonusTotalTimes.HasValue;
	}

	public void ResetBonusTotalTimes()
	{
		__pbn__BonusTotalTimes = null;
	}

	public bool ShouldSerializeMinStageTime()
	{
		return __pbn__MinStageTime.HasValue;
	}

	public void ResetMinStageTime()
	{
		__pbn__MinStageTime = null;
	}

	public bool ShouldSerializeMaxScore()
	{
		return __pbn__MaxScore.HasValue;
	}

	public void ResetMaxScore()
	{
		__pbn__MaxScore = null;
	}

	public bool ShouldSerializeStaminaDiscount()
	{
		return __pbn__StaminaDiscount.HasValue;
	}

	public void ResetStaminaDiscount()
	{
		__pbn__StaminaDiscount = null;
	}

	public bool ShouldSerializeTotalScore()
	{
		return __pbn__TotalScore.HasValue;
	}

	public void ResetTotalScore()
	{
		__pbn__TotalScore = null;
	}

	public bool ShouldSerializeMpRaidPassAvatarUid()
	{
		return __pbn__MpRaidPassAvatarUid.HasValue;
	}

	public void ResetMpRaidPassAvatarUid()
	{
		__pbn__MpRaidPassAvatarUid = null;
	}

	public bool ShouldSerializeMpRaidWeaponUid()
	{
		return __pbn__MpRaidWeaponUid.HasValue;
	}

	public void ResetMpRaidWeaponUid()
	{
		__pbn__MpRaidWeaponUid = null;
	}

	public bool ShouldSerializeIsDone()
	{
		return __pbn__IsDone.HasValue;
	}

	public void ResetIsDone()
	{
		__pbn__IsDone = null;
	}

	public bool ShouldSerializeAssistEnterTimes()
	{
		return __pbn__AssistEnterTimes.HasValue;
	}

	public void ResetAssistEnterTimes()
	{
		__pbn__AssistEnterTimes = null;
	}

	public bool ShouldSerializeMaxBattleScore()
	{
		return __pbn__MaxBattleScore.HasValue;
	}

	public void ResetMaxBattleScore()
	{
		__pbn__MaxBattleScore = null;
	}

	public bool ShouldSerializeMaxRank()
	{
		return __pbn__MaxRank.HasValue;
	}

	public void ResetMaxRank()
	{
		__pbn__MaxRank = null;
	}

	public bool ShouldSerializeClientData()
	{
		return __pbn__ClientData != null;
	}

	public void ResetClientData()
	{
		__pbn__ClientData = null;
	}

	public bool ShouldSerializeFirstStaminaDiscount()
	{
		return __pbn__FirstStaminaDiscount.HasValue;
	}

	public void ResetFirstStaminaDiscount()
	{
		__pbn__FirstStaminaDiscount = null;
	}
}
