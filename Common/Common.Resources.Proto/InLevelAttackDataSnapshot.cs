using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InLevelAttackDataSnapshot : IExtensible
{
	private IExtension __pbn__extensionData;

	private float? __pbn__attackeranidamageRatio;

	private uint? __pbn__AttackerClass;

	private uint? __pbn__AttackerNature;

	private uint? __pbn__AttackerCategory;

	private float? __pbn__AttackerCritChance;

	private float? __pbn__AttackerCritDamageRatio;

	private uint? __pbn__AttackerLevel;

	private float? __pbn__AttackerShieldDamageRatio;

	private float? __pbn__AttackerShieldDamageDelta;

	private float? __pbn__AttackerAttackPercentage;

	private float? __pbn__AttackerAttackValue;

	private float? __pbn__AddedAttackRatio;

	private float? __pbn__AddedDamageRatio;

	private float? __pbn__AddedDamageRatio2;

	private float? __pbn__AttackerAddedAttackValue;

	private float? __pbn__AttackerAllDamageReduceRatio;

	private float? __pbn__AttackerFireDamage;

	private float? __pbn__AttackerFireDamagePercentage;

	private float? __pbn__AddedAttackerFireDamageRatio;

	private float? __pbn__AttackerThunderDamage;

	private float? __pbn__AttackerThunderDamagePercentage;

	private float? __pbn__AddedAttackerThunderDamageRatio;

	private float? __pbn__AttackerIceDamage;

	private float? __pbn__AttackerIceDamagePercentage;

	private float? __pbn__AddedAttackerIceDamageRatio;

	private float? __pbn__AttackeeAniDefenceRatio;

	private uint? __pbn__AttackeeNature;

	private uint? __pbn__AttackeeClass;

	private float? __pbn__AttackeeAddedDamageTakeRatio;

	private float? __pbn__AttackeeNormalDamageTakenRatio;

	private float? __pbn__AttackeeFireDamageTakenRatio;

	private float? __pbn__AttackeeIceDamageTakenRatio;

	private float? __pbn__AttackeeThunderDamageTakenRatio;

	private float? __pbn__FireDamage;

	private float? __pbn__ThunderDamage;

	private float? __pbn__IceDamage;

	private float? __pbn__AniDamageRatio;

	private float? __pbn__RetreatVelocity;

	private uint? __pbn__FrameHalt;

	private bool? __pbn__IsAnimEventAttack;

	private bool? __pbn__IsInComboCount;

	private uint? __pbn__AttackCategoryTag;

	private uint? __pbn__HitType;

	private uint? __pbn__HitFlag;

	private uint? __pbn__HitLevel;

	private uint? __pbn__HitEffect;

	private uint? __pbn__HitEffectAux;

	private float? __pbn__AddedAttackRatio2;

	private float? __pbn__AddedAttackerFireDamageRatio2;

	private float? __pbn__AddedAttackerThunderDamageRatio2;

	private float? __pbn__AddedAttackerIceDamageRatio2;

	private float? __pbn__AddedAttackerBurnDamageRatio;

	private float? __pbn__AddedAttackerBurnDamageRatio2;

	private float? __pbn__AddedAttackerBurnDamageDelta;

	private float? __pbn__AddedAttackerBleedDamageRatio;

	private float? __pbn__AddedAttackerBleedDamageRatio2;

	private float? __pbn__AddedAttackerBleedDamageDelta;

	[ProtoMember(1, Name = "attacker_ani_damage_Ratio")]
	public float attackeranidamageRatio
	{
		get
		{
			return __pbn__attackeranidamageRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__attackeranidamageRatio = value;
		}
	}

	[ProtoMember(2, Name = "attacker_class")]
	public uint AttackerClass
	{
		get
		{
			return __pbn__AttackerClass.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerClass = value;
		}
	}

	[ProtoMember(3, Name = "attacker_nature")]
	public uint AttackerNature
	{
		get
		{
			return __pbn__AttackerNature.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerNature = value;
		}
	}

	[ProtoMember(4, Name = "attacker_category")]
	public uint AttackerCategory
	{
		get
		{
			return __pbn__AttackerCategory.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerCategory = value;
		}
	}

	[ProtoMember(5, Name = "attacker_crit_chance")]
	public float AttackerCritChance
	{
		get
		{
			return __pbn__AttackerCritChance.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerCritChance = value;
		}
	}

	[ProtoMember(6, Name = "attacker_crit_damage_ratio")]
	public float AttackerCritDamageRatio
	{
		get
		{
			return __pbn__AttackerCritDamageRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerCritDamageRatio = value;
		}
	}

	[ProtoMember(7, Name = "attacker_level")]
	public uint AttackerLevel
	{
		get
		{
			return __pbn__AttackerLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerLevel = value;
		}
	}

	[ProtoMember(8, Name = "attacker_shield_damage_ratio")]
	public float AttackerShieldDamageRatio
	{
		get
		{
			return __pbn__AttackerShieldDamageRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerShieldDamageRatio = value;
		}
	}

	[ProtoMember(9, Name = "attacker_shield_damage_delta")]
	public float AttackerShieldDamageDelta
	{
		get
		{
			return __pbn__AttackerShieldDamageDelta.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerShieldDamageDelta = value;
		}
	}

	[ProtoMember(10, Name = "attacker_attack_percentage")]
	public float AttackerAttackPercentage
	{
		get
		{
			return __pbn__AttackerAttackPercentage.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerAttackPercentage = value;
		}
	}

	[ProtoMember(11, Name = "attacker_attack_value")]
	public float AttackerAttackValue
	{
		get
		{
			return __pbn__AttackerAttackValue.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerAttackValue = value;
		}
	}

	[ProtoMember(12, Name = "added_attack_ratio")]
	public float AddedAttackRatio
	{
		get
		{
			return __pbn__AddedAttackRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AddedAttackRatio = value;
		}
	}

	[ProtoMember(13, Name = "added_damage_ratio")]
	public float AddedDamageRatio
	{
		get
		{
			return __pbn__AddedDamageRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AddedDamageRatio = value;
		}
	}

	[ProtoMember(14, Name = "added_damage_ratio2")]
	public float AddedDamageRatio2
	{
		get
		{
			return __pbn__AddedDamageRatio2.GetValueOrDefault();
		}
		set
		{
			__pbn__AddedDamageRatio2 = value;
		}
	}

	[ProtoMember(15, Name = "attacker_added_attack_value")]
	public float AttackerAddedAttackValue
	{
		get
		{
			return __pbn__AttackerAddedAttackValue.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerAddedAttackValue = value;
		}
	}

	[ProtoMember(16, Name = "attacker_all_damage_reduce_ratio")]
	public float AttackerAllDamageReduceRatio
	{
		get
		{
			return __pbn__AttackerAllDamageReduceRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerAllDamageReduceRatio = value;
		}
	}

	[ProtoMember(17, Name = "attacker_fire_damage")]
	public float AttackerFireDamage
	{
		get
		{
			return __pbn__AttackerFireDamage.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerFireDamage = value;
		}
	}

	[ProtoMember(18, Name = "attacker_fire_damage_percentage")]
	public float AttackerFireDamagePercentage
	{
		get
		{
			return __pbn__AttackerFireDamagePercentage.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerFireDamagePercentage = value;
		}
	}

	[ProtoMember(19, Name = "added_attacker_fire_damage_ratio")]
	public float AddedAttackerFireDamageRatio
	{
		get
		{
			return __pbn__AddedAttackerFireDamageRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AddedAttackerFireDamageRatio = value;
		}
	}

	[ProtoMember(20, Name = "attacker_thunder_damage")]
	public float AttackerThunderDamage
	{
		get
		{
			return __pbn__AttackerThunderDamage.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerThunderDamage = value;
		}
	}

	[ProtoMember(21, Name = "attacker_thunder_damage_percentage")]
	public float AttackerThunderDamagePercentage
	{
		get
		{
			return __pbn__AttackerThunderDamagePercentage.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerThunderDamagePercentage = value;
		}
	}

	[ProtoMember(22, Name = "added_attacker_thunder_damage_ratio")]
	public float AddedAttackerThunderDamageRatio
	{
		get
		{
			return __pbn__AddedAttackerThunderDamageRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AddedAttackerThunderDamageRatio = value;
		}
	}

	[ProtoMember(23, Name = "attacker_ice_damage")]
	public float AttackerIceDamage
	{
		get
		{
			return __pbn__AttackerIceDamage.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerIceDamage = value;
		}
	}

	[ProtoMember(24, Name = "attacker_ice_damage_percentage")]
	public float AttackerIceDamagePercentage
	{
		get
		{
			return __pbn__AttackerIceDamagePercentage.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackerIceDamagePercentage = value;
		}
	}

	[ProtoMember(25, Name = "added_attacker_ice_damage_ratio")]
	public float AddedAttackerIceDamageRatio
	{
		get
		{
			return __pbn__AddedAttackerIceDamageRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AddedAttackerIceDamageRatio = value;
		}
	}

	[ProtoMember(26, Name = "attackee_ani_defence_ratio")]
	public float AttackeeAniDefenceRatio
	{
		get
		{
			return __pbn__AttackeeAniDefenceRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackeeAniDefenceRatio = value;
		}
	}

	[ProtoMember(27, Name = "attackee_nature")]
	public uint AttackeeNature
	{
		get
		{
			return __pbn__AttackeeNature.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackeeNature = value;
		}
	}

	[ProtoMember(28, Name = "attackee_class")]
	public uint AttackeeClass
	{
		get
		{
			return __pbn__AttackeeClass.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackeeClass = value;
		}
	}

	[ProtoMember(29, Name = "attackee_added_damage_take_ratio")]
	public float AttackeeAddedDamageTakeRatio
	{
		get
		{
			return __pbn__AttackeeAddedDamageTakeRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackeeAddedDamageTakeRatio = value;
		}
	}

	[ProtoMember(30, Name = "attackee_normal_damage_taken_ratio")]
	public float AttackeeNormalDamageTakenRatio
	{
		get
		{
			return __pbn__AttackeeNormalDamageTakenRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackeeNormalDamageTakenRatio = value;
		}
	}

	[ProtoMember(31, Name = "attackee_fire_damage_taken_ratio")]
	public float AttackeeFireDamageTakenRatio
	{
		get
		{
			return __pbn__AttackeeFireDamageTakenRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackeeFireDamageTakenRatio = value;
		}
	}

	[ProtoMember(32, Name = "attackee_ice_damage_taken_ratio")]
	public float AttackeeIceDamageTakenRatio
	{
		get
		{
			return __pbn__AttackeeIceDamageTakenRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackeeIceDamageTakenRatio = value;
		}
	}

	[ProtoMember(33, Name = "attackee_thunder_damage_taken_ratio")]
	public float AttackeeThunderDamageTakenRatio
	{
		get
		{
			return __pbn__AttackeeThunderDamageTakenRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackeeThunderDamageTakenRatio = value;
		}
	}

	[ProtoMember(34, Name = "fire_damage")]
	public float FireDamage
	{
		get
		{
			return __pbn__FireDamage.GetValueOrDefault();
		}
		set
		{
			__pbn__FireDamage = value;
		}
	}

	[ProtoMember(35, Name = "thunder_damage")]
	public float ThunderDamage
	{
		get
		{
			return __pbn__ThunderDamage.GetValueOrDefault();
		}
		set
		{
			__pbn__ThunderDamage = value;
		}
	}

	[ProtoMember(36, Name = "ice_damage")]
	public float IceDamage
	{
		get
		{
			return __pbn__IceDamage.GetValueOrDefault();
		}
		set
		{
			__pbn__IceDamage = value;
		}
	}

	[ProtoMember(37, Name = "ani_damage_ratio")]
	public float AniDamageRatio
	{
		get
		{
			return __pbn__AniDamageRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AniDamageRatio = value;
		}
	}

	[ProtoMember(38, Name = "retreat_velocity")]
	public float RetreatVelocity
	{
		get
		{
			return __pbn__RetreatVelocity.GetValueOrDefault();
		}
		set
		{
			__pbn__RetreatVelocity = value;
		}
	}

	[ProtoMember(39, Name = "frame_halt")]
	public uint FrameHalt
	{
		get
		{
			return __pbn__FrameHalt.GetValueOrDefault();
		}
		set
		{
			__pbn__FrameHalt = value;
		}
	}

	[ProtoMember(40, Name = "is_anim_event_attack")]
	public bool IsAnimEventAttack
	{
		get
		{
			return __pbn__IsAnimEventAttack.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAnimEventAttack = value;
		}
	}

	[ProtoMember(41, Name = "is_in_combo_count")]
	public bool IsInComboCount
	{
		get
		{
			return __pbn__IsInComboCount.GetValueOrDefault();
		}
		set
		{
			__pbn__IsInComboCount = value;
		}
	}

	[ProtoMember(42, Name = "attack_category_tag")]
	public uint AttackCategoryTag
	{
		get
		{
			return __pbn__AttackCategoryTag.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackCategoryTag = value;
		}
	}

	[ProtoMember(43, Name = "hit_type")]
	public uint HitType
	{
		get
		{
			return __pbn__HitType.GetValueOrDefault();
		}
		set
		{
			__pbn__HitType = value;
		}
	}

	[ProtoMember(44, Name = "hit_flag")]
	public uint HitFlag
	{
		get
		{
			return __pbn__HitFlag.GetValueOrDefault();
		}
		set
		{
			__pbn__HitFlag = value;
		}
	}

	[ProtoMember(45, Name = "hit_level")]
	public uint HitLevel
	{
		get
		{
			return __pbn__HitLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HitLevel = value;
		}
	}

	[ProtoMember(46, Name = "hit_effect")]
	public uint HitEffect
	{
		get
		{
			return __pbn__HitEffect.GetValueOrDefault();
		}
		set
		{
			__pbn__HitEffect = value;
		}
	}

	[ProtoMember(47, Name = "hit_effect_aux")]
	public uint HitEffectAux
	{
		get
		{
			return __pbn__HitEffectAux.GetValueOrDefault();
		}
		set
		{
			__pbn__HitEffectAux = value;
		}
	}

	[ProtoMember(48, Name = "added_attack_ratio2")]
	public float AddedAttackRatio2
	{
		get
		{
			return __pbn__AddedAttackRatio2.GetValueOrDefault();
		}
		set
		{
			__pbn__AddedAttackRatio2 = value;
		}
	}

	[ProtoMember(49, Name = "added_attacker_fire_damage_ratio2")]
	public float AddedAttackerFireDamageRatio2
	{
		get
		{
			return __pbn__AddedAttackerFireDamageRatio2.GetValueOrDefault();
		}
		set
		{
			__pbn__AddedAttackerFireDamageRatio2 = value;
		}
	}

	[ProtoMember(50, Name = "added_attacker_thunder_damage_ratio2")]
	public float AddedAttackerThunderDamageRatio2
	{
		get
		{
			return __pbn__AddedAttackerThunderDamageRatio2.GetValueOrDefault();
		}
		set
		{
			__pbn__AddedAttackerThunderDamageRatio2 = value;
		}
	}

	[ProtoMember(51, Name = "added_attacker_ice_damage_ratio2")]
	public float AddedAttackerIceDamageRatio2
	{
		get
		{
			return __pbn__AddedAttackerIceDamageRatio2.GetValueOrDefault();
		}
		set
		{
			__pbn__AddedAttackerIceDamageRatio2 = value;
		}
	}

	[ProtoMember(52, Name = "added_attacker_burn_damage_ratio")]
	public float AddedAttackerBurnDamageRatio
	{
		get
		{
			return __pbn__AddedAttackerBurnDamageRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AddedAttackerBurnDamageRatio = value;
		}
	}

	[ProtoMember(53, Name = "added_attacker_burn_damage_ratio2")]
	public float AddedAttackerBurnDamageRatio2
	{
		get
		{
			return __pbn__AddedAttackerBurnDamageRatio2.GetValueOrDefault();
		}
		set
		{
			__pbn__AddedAttackerBurnDamageRatio2 = value;
		}
	}

	[ProtoMember(54, Name = "added_attacker_burn_damage_delta")]
	public float AddedAttackerBurnDamageDelta
	{
		get
		{
			return __pbn__AddedAttackerBurnDamageDelta.GetValueOrDefault();
		}
		set
		{
			__pbn__AddedAttackerBurnDamageDelta = value;
		}
	}

	[ProtoMember(55, Name = "added_attacker_bleed_damage_ratio")]
	public float AddedAttackerBleedDamageRatio
	{
		get
		{
			return __pbn__AddedAttackerBleedDamageRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AddedAttackerBleedDamageRatio = value;
		}
	}

	[ProtoMember(56, Name = "added_attacker_bleed_damage_ratio2")]
	public float AddedAttackerBleedDamageRatio2
	{
		get
		{
			return __pbn__AddedAttackerBleedDamageRatio2.GetValueOrDefault();
		}
		set
		{
			__pbn__AddedAttackerBleedDamageRatio2 = value;
		}
	}

	[ProtoMember(57, Name = "added_attacker_bleed_damage_delta")]
	public float AddedAttackerBleedDamageDelta
	{
		get
		{
			return __pbn__AddedAttackerBleedDamageDelta.GetValueOrDefault();
		}
		set
		{
			__pbn__AddedAttackerBleedDamageDelta = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeattackeranidamageRatio()
	{
		return __pbn__attackeranidamageRatio.HasValue;
	}

	public void ResetattackeranidamageRatio()
	{
		__pbn__attackeranidamageRatio = null;
	}

	public bool ShouldSerializeAttackerClass()
	{
		return __pbn__AttackerClass.HasValue;
	}

	public void ResetAttackerClass()
	{
		__pbn__AttackerClass = null;
	}

	public bool ShouldSerializeAttackerNature()
	{
		return __pbn__AttackerNature.HasValue;
	}

	public void ResetAttackerNature()
	{
		__pbn__AttackerNature = null;
	}

	public bool ShouldSerializeAttackerCategory()
	{
		return __pbn__AttackerCategory.HasValue;
	}

	public void ResetAttackerCategory()
	{
		__pbn__AttackerCategory = null;
	}

	public bool ShouldSerializeAttackerCritChance()
	{
		return __pbn__AttackerCritChance.HasValue;
	}

	public void ResetAttackerCritChance()
	{
		__pbn__AttackerCritChance = null;
	}

	public bool ShouldSerializeAttackerCritDamageRatio()
	{
		return __pbn__AttackerCritDamageRatio.HasValue;
	}

	public void ResetAttackerCritDamageRatio()
	{
		__pbn__AttackerCritDamageRatio = null;
	}

	public bool ShouldSerializeAttackerLevel()
	{
		return __pbn__AttackerLevel.HasValue;
	}

	public void ResetAttackerLevel()
	{
		__pbn__AttackerLevel = null;
	}

	public bool ShouldSerializeAttackerShieldDamageRatio()
	{
		return __pbn__AttackerShieldDamageRatio.HasValue;
	}

	public void ResetAttackerShieldDamageRatio()
	{
		__pbn__AttackerShieldDamageRatio = null;
	}

	public bool ShouldSerializeAttackerShieldDamageDelta()
	{
		return __pbn__AttackerShieldDamageDelta.HasValue;
	}

	public void ResetAttackerShieldDamageDelta()
	{
		__pbn__AttackerShieldDamageDelta = null;
	}

	public bool ShouldSerializeAttackerAttackPercentage()
	{
		return __pbn__AttackerAttackPercentage.HasValue;
	}

	public void ResetAttackerAttackPercentage()
	{
		__pbn__AttackerAttackPercentage = null;
	}

	public bool ShouldSerializeAttackerAttackValue()
	{
		return __pbn__AttackerAttackValue.HasValue;
	}

	public void ResetAttackerAttackValue()
	{
		__pbn__AttackerAttackValue = null;
	}

	public bool ShouldSerializeAddedAttackRatio()
	{
		return __pbn__AddedAttackRatio.HasValue;
	}

	public void ResetAddedAttackRatio()
	{
		__pbn__AddedAttackRatio = null;
	}

	public bool ShouldSerializeAddedDamageRatio()
	{
		return __pbn__AddedDamageRatio.HasValue;
	}

	public void ResetAddedDamageRatio()
	{
		__pbn__AddedDamageRatio = null;
	}

	public bool ShouldSerializeAddedDamageRatio2()
	{
		return __pbn__AddedDamageRatio2.HasValue;
	}

	public void ResetAddedDamageRatio2()
	{
		__pbn__AddedDamageRatio2 = null;
	}

	public bool ShouldSerializeAttackerAddedAttackValue()
	{
		return __pbn__AttackerAddedAttackValue.HasValue;
	}

	public void ResetAttackerAddedAttackValue()
	{
		__pbn__AttackerAddedAttackValue = null;
	}

	public bool ShouldSerializeAttackerAllDamageReduceRatio()
	{
		return __pbn__AttackerAllDamageReduceRatio.HasValue;
	}

	public void ResetAttackerAllDamageReduceRatio()
	{
		__pbn__AttackerAllDamageReduceRatio = null;
	}

	public bool ShouldSerializeAttackerFireDamage()
	{
		return __pbn__AttackerFireDamage.HasValue;
	}

	public void ResetAttackerFireDamage()
	{
		__pbn__AttackerFireDamage = null;
	}

	public bool ShouldSerializeAttackerFireDamagePercentage()
	{
		return __pbn__AttackerFireDamagePercentage.HasValue;
	}

	public void ResetAttackerFireDamagePercentage()
	{
		__pbn__AttackerFireDamagePercentage = null;
	}

	public bool ShouldSerializeAddedAttackerFireDamageRatio()
	{
		return __pbn__AddedAttackerFireDamageRatio.HasValue;
	}

	public void ResetAddedAttackerFireDamageRatio()
	{
		__pbn__AddedAttackerFireDamageRatio = null;
	}

	public bool ShouldSerializeAttackerThunderDamage()
	{
		return __pbn__AttackerThunderDamage.HasValue;
	}

	public void ResetAttackerThunderDamage()
	{
		__pbn__AttackerThunderDamage = null;
	}

	public bool ShouldSerializeAttackerThunderDamagePercentage()
	{
		return __pbn__AttackerThunderDamagePercentage.HasValue;
	}

	public void ResetAttackerThunderDamagePercentage()
	{
		__pbn__AttackerThunderDamagePercentage = null;
	}

	public bool ShouldSerializeAddedAttackerThunderDamageRatio()
	{
		return __pbn__AddedAttackerThunderDamageRatio.HasValue;
	}

	public void ResetAddedAttackerThunderDamageRatio()
	{
		__pbn__AddedAttackerThunderDamageRatio = null;
	}

	public bool ShouldSerializeAttackerIceDamage()
	{
		return __pbn__AttackerIceDamage.HasValue;
	}

	public void ResetAttackerIceDamage()
	{
		__pbn__AttackerIceDamage = null;
	}

	public bool ShouldSerializeAttackerIceDamagePercentage()
	{
		return __pbn__AttackerIceDamagePercentage.HasValue;
	}

	public void ResetAttackerIceDamagePercentage()
	{
		__pbn__AttackerIceDamagePercentage = null;
	}

	public bool ShouldSerializeAddedAttackerIceDamageRatio()
	{
		return __pbn__AddedAttackerIceDamageRatio.HasValue;
	}

	public void ResetAddedAttackerIceDamageRatio()
	{
		__pbn__AddedAttackerIceDamageRatio = null;
	}

	public bool ShouldSerializeAttackeeAniDefenceRatio()
	{
		return __pbn__AttackeeAniDefenceRatio.HasValue;
	}

	public void ResetAttackeeAniDefenceRatio()
	{
		__pbn__AttackeeAniDefenceRatio = null;
	}

	public bool ShouldSerializeAttackeeNature()
	{
		return __pbn__AttackeeNature.HasValue;
	}

	public void ResetAttackeeNature()
	{
		__pbn__AttackeeNature = null;
	}

	public bool ShouldSerializeAttackeeClass()
	{
		return __pbn__AttackeeClass.HasValue;
	}

	public void ResetAttackeeClass()
	{
		__pbn__AttackeeClass = null;
	}

	public bool ShouldSerializeAttackeeAddedDamageTakeRatio()
	{
		return __pbn__AttackeeAddedDamageTakeRatio.HasValue;
	}

	public void ResetAttackeeAddedDamageTakeRatio()
	{
		__pbn__AttackeeAddedDamageTakeRatio = null;
	}

	public bool ShouldSerializeAttackeeNormalDamageTakenRatio()
	{
		return __pbn__AttackeeNormalDamageTakenRatio.HasValue;
	}

	public void ResetAttackeeNormalDamageTakenRatio()
	{
		__pbn__AttackeeNormalDamageTakenRatio = null;
	}

	public bool ShouldSerializeAttackeeFireDamageTakenRatio()
	{
		return __pbn__AttackeeFireDamageTakenRatio.HasValue;
	}

	public void ResetAttackeeFireDamageTakenRatio()
	{
		__pbn__AttackeeFireDamageTakenRatio = null;
	}

	public bool ShouldSerializeAttackeeIceDamageTakenRatio()
	{
		return __pbn__AttackeeIceDamageTakenRatio.HasValue;
	}

	public void ResetAttackeeIceDamageTakenRatio()
	{
		__pbn__AttackeeIceDamageTakenRatio = null;
	}

	public bool ShouldSerializeAttackeeThunderDamageTakenRatio()
	{
		return __pbn__AttackeeThunderDamageTakenRatio.HasValue;
	}

	public void ResetAttackeeThunderDamageTakenRatio()
	{
		__pbn__AttackeeThunderDamageTakenRatio = null;
	}

	public bool ShouldSerializeFireDamage()
	{
		return __pbn__FireDamage.HasValue;
	}

	public void ResetFireDamage()
	{
		__pbn__FireDamage = null;
	}

	public bool ShouldSerializeThunderDamage()
	{
		return __pbn__ThunderDamage.HasValue;
	}

	public void ResetThunderDamage()
	{
		__pbn__ThunderDamage = null;
	}

	public bool ShouldSerializeIceDamage()
	{
		return __pbn__IceDamage.HasValue;
	}

	public void ResetIceDamage()
	{
		__pbn__IceDamage = null;
	}

	public bool ShouldSerializeAniDamageRatio()
	{
		return __pbn__AniDamageRatio.HasValue;
	}

	public void ResetAniDamageRatio()
	{
		__pbn__AniDamageRatio = null;
	}

	public bool ShouldSerializeRetreatVelocity()
	{
		return __pbn__RetreatVelocity.HasValue;
	}

	public void ResetRetreatVelocity()
	{
		__pbn__RetreatVelocity = null;
	}

	public bool ShouldSerializeFrameHalt()
	{
		return __pbn__FrameHalt.HasValue;
	}

	public void ResetFrameHalt()
	{
		__pbn__FrameHalt = null;
	}

	public bool ShouldSerializeIsAnimEventAttack()
	{
		return __pbn__IsAnimEventAttack.HasValue;
	}

	public void ResetIsAnimEventAttack()
	{
		__pbn__IsAnimEventAttack = null;
	}

	public bool ShouldSerializeIsInComboCount()
	{
		return __pbn__IsInComboCount.HasValue;
	}

	public void ResetIsInComboCount()
	{
		__pbn__IsInComboCount = null;
	}

	public bool ShouldSerializeAttackCategoryTag()
	{
		return __pbn__AttackCategoryTag.HasValue;
	}

	public void ResetAttackCategoryTag()
	{
		__pbn__AttackCategoryTag = null;
	}

	public bool ShouldSerializeHitType()
	{
		return __pbn__HitType.HasValue;
	}

	public void ResetHitType()
	{
		__pbn__HitType = null;
	}

	public bool ShouldSerializeHitFlag()
	{
		return __pbn__HitFlag.HasValue;
	}

	public void ResetHitFlag()
	{
		__pbn__HitFlag = null;
	}

	public bool ShouldSerializeHitLevel()
	{
		return __pbn__HitLevel.HasValue;
	}

	public void ResetHitLevel()
	{
		__pbn__HitLevel = null;
	}

	public bool ShouldSerializeHitEffect()
	{
		return __pbn__HitEffect.HasValue;
	}

	public void ResetHitEffect()
	{
		__pbn__HitEffect = null;
	}

	public bool ShouldSerializeHitEffectAux()
	{
		return __pbn__HitEffectAux.HasValue;
	}

	public void ResetHitEffectAux()
	{
		__pbn__HitEffectAux = null;
	}

	public bool ShouldSerializeAddedAttackRatio2()
	{
		return __pbn__AddedAttackRatio2.HasValue;
	}

	public void ResetAddedAttackRatio2()
	{
		__pbn__AddedAttackRatio2 = null;
	}

	public bool ShouldSerializeAddedAttackerFireDamageRatio2()
	{
		return __pbn__AddedAttackerFireDamageRatio2.HasValue;
	}

	public void ResetAddedAttackerFireDamageRatio2()
	{
		__pbn__AddedAttackerFireDamageRatio2 = null;
	}

	public bool ShouldSerializeAddedAttackerThunderDamageRatio2()
	{
		return __pbn__AddedAttackerThunderDamageRatio2.HasValue;
	}

	public void ResetAddedAttackerThunderDamageRatio2()
	{
		__pbn__AddedAttackerThunderDamageRatio2 = null;
	}

	public bool ShouldSerializeAddedAttackerIceDamageRatio2()
	{
		return __pbn__AddedAttackerIceDamageRatio2.HasValue;
	}

	public void ResetAddedAttackerIceDamageRatio2()
	{
		__pbn__AddedAttackerIceDamageRatio2 = null;
	}

	public bool ShouldSerializeAddedAttackerBurnDamageRatio()
	{
		return __pbn__AddedAttackerBurnDamageRatio.HasValue;
	}

	public void ResetAddedAttackerBurnDamageRatio()
	{
		__pbn__AddedAttackerBurnDamageRatio = null;
	}

	public bool ShouldSerializeAddedAttackerBurnDamageRatio2()
	{
		return __pbn__AddedAttackerBurnDamageRatio2.HasValue;
	}

	public void ResetAddedAttackerBurnDamageRatio2()
	{
		__pbn__AddedAttackerBurnDamageRatio2 = null;
	}

	public bool ShouldSerializeAddedAttackerBurnDamageDelta()
	{
		return __pbn__AddedAttackerBurnDamageDelta.HasValue;
	}

	public void ResetAddedAttackerBurnDamageDelta()
	{
		__pbn__AddedAttackerBurnDamageDelta = null;
	}

	public bool ShouldSerializeAddedAttackerBleedDamageRatio()
	{
		return __pbn__AddedAttackerBleedDamageRatio.HasValue;
	}

	public void ResetAddedAttackerBleedDamageRatio()
	{
		__pbn__AddedAttackerBleedDamageRatio = null;
	}

	public bool ShouldSerializeAddedAttackerBleedDamageRatio2()
	{
		return __pbn__AddedAttackerBleedDamageRatio2.HasValue;
	}

	public void ResetAddedAttackerBleedDamageRatio2()
	{
		__pbn__AddedAttackerBleedDamageRatio2 = null;
	}

	public bool ShouldSerializeAddedAttackerBleedDamageDelta()
	{
		return __pbn__AddedAttackerBleedDamageDelta.HasValue;
	}

	public void ResetAddedAttackerBleedDamageDelta()
	{
		__pbn__AddedAttackerBleedDamageDelta = null;
	}
}
