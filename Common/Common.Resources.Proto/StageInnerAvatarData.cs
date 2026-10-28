using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageInnerAvatarData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__AvatarLevel;

	private uint? __pbn__AvatarStar;

	private uint? __pbn__TotalOutput;

	private uint? __pbn__NoRestrictOutput;

	private uint? __pbn__DoRestrictOutput;

	private uint? __pbn__BeRestrictOutput;

	private uint? __pbn__TotalInput;

	private float? __pbn__BattleTime;

	private float? __pbn__TotalTime;

	private uint? __pbn__EnterTimes;

	private uint? __pbn__LeaveTimes;

	private uint? __pbn__DoBreakTimes;

	private uint? __pbn__BeBreakTimes;

	private uint? __pbn__DoHitTimes;

	private uint? __pbn__BeHitTimes;

	private uint? __pbn__ExskillTimes;

	private uint? __pbn__EvadeTimes;

	private uint? __pbn__EvadeEffectTimes;

	private uint? __pbn__AttackSpRecover;

	private uint? __pbn__TotalSpRecover;

	private uint? __pbn__Dps;

	private uint? __pbn__SpecialAttackTimes;

	private uint? __pbn__EvadeSuccessTimes;

	private uint? __pbn__WeaponActiveSkill;

	private uint? __pbn__StageId;

	private uint? __pbn__NormalOutput;

	private uint? __pbn__UltraOutput;

	private uint? __pbn__SwitchinOutput;

	private uint? __pbn__BranchOutput;

	private uint? __pbn__ChargeOutput;

	private uint? __pbn__HpCost;

	private uint? __pbn__HpRecover;

	private uint? __pbn__SpCost;

	private uint? __pbn__ShieldDamage;

	private uint? __pbn__AvatarSubStar;

	private bool? __pbn__IsPlayerAvatar;

	private bool? __pbn__IsTrialAvatar;

	private uint? __pbn__ChargeAttackTimes;

	private uint? __pbn__BranchAttackTimes;

	private uint? __pbn__MaxDamage5;

	private uint? __pbn__MaxDamage15;

	[ProtoMember(1, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(2, Name = "avatar_level")]
	public uint AvatarLevel
	{
		get
		{
			return __pbn__AvatarLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarLevel = value;
		}
	}

	[ProtoMember(3, Name = "avatar_star")]
	public uint AvatarStar
	{
		get
		{
			return __pbn__AvatarStar.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarStar = value;
		}
	}

	[ProtoMember(4, Name = "total_output")]
	public uint TotalOutput
	{
		get
		{
			return __pbn__TotalOutput.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalOutput = value;
		}
	}

	[ProtoMember(5, Name = "no_restrict_output")]
	public uint NoRestrictOutput
	{
		get
		{
			return __pbn__NoRestrictOutput.GetValueOrDefault();
		}
		set
		{
			__pbn__NoRestrictOutput = value;
		}
	}

	[ProtoMember(6, Name = "do_restrict_output")]
	public uint DoRestrictOutput
	{
		get
		{
			return __pbn__DoRestrictOutput.GetValueOrDefault();
		}
		set
		{
			__pbn__DoRestrictOutput = value;
		}
	}

	[ProtoMember(7, Name = "be_restrict_output")]
	public uint BeRestrictOutput
	{
		get
		{
			return __pbn__BeRestrictOutput.GetValueOrDefault();
		}
		set
		{
			__pbn__BeRestrictOutput = value;
		}
	}

	[ProtoMember(8, Name = "total_input")]
	public uint TotalInput
	{
		get
		{
			return __pbn__TotalInput.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalInput = value;
		}
	}

	[ProtoMember(9, Name = "battle_time")]
	public float BattleTime
	{
		get
		{
			return __pbn__BattleTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleTime = value;
		}
	}

	[ProtoMember(10, Name = "total_time")]
	public float TotalTime
	{
		get
		{
			return __pbn__TotalTime.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalTime = value;
		}
	}

	[ProtoMember(11, Name = "enter_times")]
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

	[ProtoMember(12, Name = "leave_times")]
	public uint LeaveTimes
	{
		get
		{
			return __pbn__LeaveTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LeaveTimes = value;
		}
	}

	[ProtoMember(13, Name = "do_break_times")]
	public uint DoBreakTimes
	{
		get
		{
			return __pbn__DoBreakTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__DoBreakTimes = value;
		}
	}

	[ProtoMember(14, Name = "be_break_times")]
	public uint BeBreakTimes
	{
		get
		{
			return __pbn__BeBreakTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__BeBreakTimes = value;
		}
	}

	[ProtoMember(15, Name = "do_hit_times")]
	public uint DoHitTimes
	{
		get
		{
			return __pbn__DoHitTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__DoHitTimes = value;
		}
	}

	[ProtoMember(16, Name = "be_hit_times")]
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

	[ProtoMember(17, Name = "exskill_times")]
	public uint ExskillTimes
	{
		get
		{
			return __pbn__ExskillTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ExskillTimes = value;
		}
	}

	[ProtoMember(18, Name = "evade_times")]
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

	[ProtoMember(19, Name = "evade_effect_times")]
	public uint EvadeEffectTimes
	{
		get
		{
			return __pbn__EvadeEffectTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__EvadeEffectTimes = value;
		}
	}

	[ProtoMember(20, Name = "attack_sp_recover")]
	public uint AttackSpRecover
	{
		get
		{
			return __pbn__AttackSpRecover.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackSpRecover = value;
		}
	}

	[ProtoMember(21, Name = "total_sp_recover")]
	public uint TotalSpRecover
	{
		get
		{
			return __pbn__TotalSpRecover.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalSpRecover = value;
		}
	}

	[ProtoMember(22, Name = "dps")]
	public uint Dps
	{
		get
		{
			return __pbn__Dps.GetValueOrDefault();
		}
		set
		{
			__pbn__Dps = value;
		}
	}

	[ProtoMember(23, Name = "special_attack_times")]
	public uint SpecialAttackTimes
	{
		get
		{
			return __pbn__SpecialAttackTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__SpecialAttackTimes = value;
		}
	}

	[ProtoMember(24, Name = "evade_success_times")]
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

	[ProtoMember(25, Name = "weapon_active_skill")]
	public uint WeaponActiveSkill
	{
		get
		{
			return __pbn__WeaponActiveSkill.GetValueOrDefault();
		}
		set
		{
			__pbn__WeaponActiveSkill = value;
		}
	}

	[ProtoMember(26, Name = "stage_id")]
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

	[ProtoMember(27, Name = "normal_output")]
	public uint NormalOutput
	{
		get
		{
			return __pbn__NormalOutput.GetValueOrDefault();
		}
		set
		{
			__pbn__NormalOutput = value;
		}
	}

	[ProtoMember(28, Name = "ultra_output")]
	public uint UltraOutput
	{
		get
		{
			return __pbn__UltraOutput.GetValueOrDefault();
		}
		set
		{
			__pbn__UltraOutput = value;
		}
	}

	[ProtoMember(29, Name = "switchin_output")]
	public uint SwitchinOutput
	{
		get
		{
			return __pbn__SwitchinOutput.GetValueOrDefault();
		}
		set
		{
			__pbn__SwitchinOutput = value;
		}
	}

	[ProtoMember(30, Name = "branch_output")]
	public uint BranchOutput
	{
		get
		{
			return __pbn__BranchOutput.GetValueOrDefault();
		}
		set
		{
			__pbn__BranchOutput = value;
		}
	}

	[ProtoMember(31, Name = "charge_output")]
	public uint ChargeOutput
	{
		get
		{
			return __pbn__ChargeOutput.GetValueOrDefault();
		}
		set
		{
			__pbn__ChargeOutput = value;
		}
	}

	[ProtoMember(32, Name = "hp_cost")]
	public uint HpCost
	{
		get
		{
			return __pbn__HpCost.GetValueOrDefault();
		}
		set
		{
			__pbn__HpCost = value;
		}
	}

	[ProtoMember(33, Name = "hp_recover")]
	public uint HpRecover
	{
		get
		{
			return __pbn__HpRecover.GetValueOrDefault();
		}
		set
		{
			__pbn__HpRecover = value;
		}
	}

	[ProtoMember(34, Name = "sp_cost")]
	public uint SpCost
	{
		get
		{
			return __pbn__SpCost.GetValueOrDefault();
		}
		set
		{
			__pbn__SpCost = value;
		}
	}

	[ProtoMember(35, Name = "shield_damage")]
	public uint ShieldDamage
	{
		get
		{
			return __pbn__ShieldDamage.GetValueOrDefault();
		}
		set
		{
			__pbn__ShieldDamage = value;
		}
	}

	[ProtoMember(36, Name = "avatar_sub_star")]
	public uint AvatarSubStar
	{
		get
		{
			return __pbn__AvatarSubStar.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarSubStar = value;
		}
	}

	[ProtoMember(37, Name = "is_player_avatar")]
	public bool IsPlayerAvatar
	{
		get
		{
			return __pbn__IsPlayerAvatar.GetValueOrDefault();
		}
		set
		{
			__pbn__IsPlayerAvatar = value;
		}
	}

	[ProtoMember(38, Name = "is_trial_avatar")]
	public bool IsTrialAvatar
	{
		get
		{
			return __pbn__IsTrialAvatar.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTrialAvatar = value;
		}
	}

	[ProtoMember(39, Name = "charge_attack_times")]
	public uint ChargeAttackTimes
	{
		get
		{
			return __pbn__ChargeAttackTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ChargeAttackTimes = value;
		}
	}

	[ProtoMember(40, Name = "branch_attack_times")]
	public uint BranchAttackTimes
	{
		get
		{
			return __pbn__BranchAttackTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__BranchAttackTimes = value;
		}
	}

	[ProtoMember(41, Name = "max_damage_5")]
	public uint MaxDamage5
	{
		get
		{
			return __pbn__MaxDamage5.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxDamage5 = value;
		}
	}

	[ProtoMember(42, Name = "max_damage_15")]
	public uint MaxDamage15
	{
		get
		{
			return __pbn__MaxDamage15.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxDamage15 = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeAvatarLevel()
	{
		return __pbn__AvatarLevel.HasValue;
	}

	public void ResetAvatarLevel()
	{
		__pbn__AvatarLevel = null;
	}

	public bool ShouldSerializeAvatarStar()
	{
		return __pbn__AvatarStar.HasValue;
	}

	public void ResetAvatarStar()
	{
		__pbn__AvatarStar = null;
	}

	public bool ShouldSerializeTotalOutput()
	{
		return __pbn__TotalOutput.HasValue;
	}

	public void ResetTotalOutput()
	{
		__pbn__TotalOutput = null;
	}

	public bool ShouldSerializeNoRestrictOutput()
	{
		return __pbn__NoRestrictOutput.HasValue;
	}

	public void ResetNoRestrictOutput()
	{
		__pbn__NoRestrictOutput = null;
	}

	public bool ShouldSerializeDoRestrictOutput()
	{
		return __pbn__DoRestrictOutput.HasValue;
	}

	public void ResetDoRestrictOutput()
	{
		__pbn__DoRestrictOutput = null;
	}

	public bool ShouldSerializeBeRestrictOutput()
	{
		return __pbn__BeRestrictOutput.HasValue;
	}

	public void ResetBeRestrictOutput()
	{
		__pbn__BeRestrictOutput = null;
	}

	public bool ShouldSerializeTotalInput()
	{
		return __pbn__TotalInput.HasValue;
	}

	public void ResetTotalInput()
	{
		__pbn__TotalInput = null;
	}

	public bool ShouldSerializeBattleTime()
	{
		return __pbn__BattleTime.HasValue;
	}

	public void ResetBattleTime()
	{
		__pbn__BattleTime = null;
	}

	public bool ShouldSerializeTotalTime()
	{
		return __pbn__TotalTime.HasValue;
	}

	public void ResetTotalTime()
	{
		__pbn__TotalTime = null;
	}

	public bool ShouldSerializeEnterTimes()
	{
		return __pbn__EnterTimes.HasValue;
	}

	public void ResetEnterTimes()
	{
		__pbn__EnterTimes = null;
	}

	public bool ShouldSerializeLeaveTimes()
	{
		return __pbn__LeaveTimes.HasValue;
	}

	public void ResetLeaveTimes()
	{
		__pbn__LeaveTimes = null;
	}

	public bool ShouldSerializeDoBreakTimes()
	{
		return __pbn__DoBreakTimes.HasValue;
	}

	public void ResetDoBreakTimes()
	{
		__pbn__DoBreakTimes = null;
	}

	public bool ShouldSerializeBeBreakTimes()
	{
		return __pbn__BeBreakTimes.HasValue;
	}

	public void ResetBeBreakTimes()
	{
		__pbn__BeBreakTimes = null;
	}

	public bool ShouldSerializeDoHitTimes()
	{
		return __pbn__DoHitTimes.HasValue;
	}

	public void ResetDoHitTimes()
	{
		__pbn__DoHitTimes = null;
	}

	public bool ShouldSerializeBeHitTimes()
	{
		return __pbn__BeHitTimes.HasValue;
	}

	public void ResetBeHitTimes()
	{
		__pbn__BeHitTimes = null;
	}

	public bool ShouldSerializeExskillTimes()
	{
		return __pbn__ExskillTimes.HasValue;
	}

	public void ResetExskillTimes()
	{
		__pbn__ExskillTimes = null;
	}

	public bool ShouldSerializeEvadeTimes()
	{
		return __pbn__EvadeTimes.HasValue;
	}

	public void ResetEvadeTimes()
	{
		__pbn__EvadeTimes = null;
	}

	public bool ShouldSerializeEvadeEffectTimes()
	{
		return __pbn__EvadeEffectTimes.HasValue;
	}

	public void ResetEvadeEffectTimes()
	{
		__pbn__EvadeEffectTimes = null;
	}

	public bool ShouldSerializeAttackSpRecover()
	{
		return __pbn__AttackSpRecover.HasValue;
	}

	public void ResetAttackSpRecover()
	{
		__pbn__AttackSpRecover = null;
	}

	public bool ShouldSerializeTotalSpRecover()
	{
		return __pbn__TotalSpRecover.HasValue;
	}

	public void ResetTotalSpRecover()
	{
		__pbn__TotalSpRecover = null;
	}

	public bool ShouldSerializeDps()
	{
		return __pbn__Dps.HasValue;
	}

	public void ResetDps()
	{
		__pbn__Dps = null;
	}

	public bool ShouldSerializeSpecialAttackTimes()
	{
		return __pbn__SpecialAttackTimes.HasValue;
	}

	public void ResetSpecialAttackTimes()
	{
		__pbn__SpecialAttackTimes = null;
	}

	public bool ShouldSerializeEvadeSuccessTimes()
	{
		return __pbn__EvadeSuccessTimes.HasValue;
	}

	public void ResetEvadeSuccessTimes()
	{
		__pbn__EvadeSuccessTimes = null;
	}

	public bool ShouldSerializeWeaponActiveSkill()
	{
		return __pbn__WeaponActiveSkill.HasValue;
	}

	public void ResetWeaponActiveSkill()
	{
		__pbn__WeaponActiveSkill = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeNormalOutput()
	{
		return __pbn__NormalOutput.HasValue;
	}

	public void ResetNormalOutput()
	{
		__pbn__NormalOutput = null;
	}

	public bool ShouldSerializeUltraOutput()
	{
		return __pbn__UltraOutput.HasValue;
	}

	public void ResetUltraOutput()
	{
		__pbn__UltraOutput = null;
	}

	public bool ShouldSerializeSwitchinOutput()
	{
		return __pbn__SwitchinOutput.HasValue;
	}

	public void ResetSwitchinOutput()
	{
		__pbn__SwitchinOutput = null;
	}

	public bool ShouldSerializeBranchOutput()
	{
		return __pbn__BranchOutput.HasValue;
	}

	public void ResetBranchOutput()
	{
		__pbn__BranchOutput = null;
	}

	public bool ShouldSerializeChargeOutput()
	{
		return __pbn__ChargeOutput.HasValue;
	}

	public void ResetChargeOutput()
	{
		__pbn__ChargeOutput = null;
	}

	public bool ShouldSerializeHpCost()
	{
		return __pbn__HpCost.HasValue;
	}

	public void ResetHpCost()
	{
		__pbn__HpCost = null;
	}

	public bool ShouldSerializeHpRecover()
	{
		return __pbn__HpRecover.HasValue;
	}

	public void ResetHpRecover()
	{
		__pbn__HpRecover = null;
	}

	public bool ShouldSerializeSpCost()
	{
		return __pbn__SpCost.HasValue;
	}

	public void ResetSpCost()
	{
		__pbn__SpCost = null;
	}

	public bool ShouldSerializeShieldDamage()
	{
		return __pbn__ShieldDamage.HasValue;
	}

	public void ResetShieldDamage()
	{
		__pbn__ShieldDamage = null;
	}

	public bool ShouldSerializeAvatarSubStar()
	{
		return __pbn__AvatarSubStar.HasValue;
	}

	public void ResetAvatarSubStar()
	{
		__pbn__AvatarSubStar = null;
	}

	public bool ShouldSerializeIsPlayerAvatar()
	{
		return __pbn__IsPlayerAvatar.HasValue;
	}

	public void ResetIsPlayerAvatar()
	{
		__pbn__IsPlayerAvatar = null;
	}

	public bool ShouldSerializeIsTrialAvatar()
	{
		return __pbn__IsTrialAvatar.HasValue;
	}

	public void ResetIsTrialAvatar()
	{
		__pbn__IsTrialAvatar = null;
	}

	public bool ShouldSerializeChargeAttackTimes()
	{
		return __pbn__ChargeAttackTimes.HasValue;
	}

	public void ResetChargeAttackTimes()
	{
		__pbn__ChargeAttackTimes = null;
	}

	public bool ShouldSerializeBranchAttackTimes()
	{
		return __pbn__BranchAttackTimes.HasValue;
	}

	public void ResetBranchAttackTimes()
	{
		__pbn__BranchAttackTimes = null;
	}

	public bool ShouldSerializeMaxDamage5()
	{
		return __pbn__MaxDamage5.HasValue;
	}

	public void ResetMaxDamage5()
	{
		__pbn__MaxDamage5 = null;
	}

	public bool ShouldSerializeMaxDamage15()
	{
		return __pbn__MaxDamage15.HasValue;
	}

	public void ResetMaxDamage15()
	{
		__pbn__MaxDamage15 = null;
	}
}
