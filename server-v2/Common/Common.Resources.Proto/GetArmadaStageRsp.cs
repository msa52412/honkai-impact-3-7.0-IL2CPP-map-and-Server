using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetArmadaStageRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2668
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__HardGrade;

	private uint? __pbn__HardLevel;

	private uint? __pbn__SummonProgress;

	private uint? __pbn__BossId;

	private uint? __pbn__BossBeginTime;

	private uint? __pbn__BossEndTime;

	private uint? __pbn__BossTotalHp;

	private uint? __pbn__BossLeftHp;

	private uint? __pbn__NextFightTime;

	private uint? __pbn__Hp1Percent;

	private uint? __pbn__Hp2Percent;

	private uint? __pbn__Hp3Percent;

	private bool? __pbn__IsTake;

	private uint? __pbn__OpenBeginTime;

	private uint? __pbn__OpenEndTime;

	private uint? __pbn__LeftFightTimes;

	private uint? __pbn__MaxHardLevel;

	private uint? __pbn__KillRewardMaterialId;

	private uint? __pbn__LastDamageRate;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "hard_grade")]
	public uint HardGrade
	{
		get
		{
			return __pbn__HardGrade.GetValueOrDefault();
		}
		set
		{
			__pbn__HardGrade = value;
		}
	}

	[ProtoMember(3, Name = "hard_level")]
	public uint HardLevel
	{
		get
		{
			return __pbn__HardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HardLevel = value;
		}
	}

	[ProtoMember(4, Name = "summon_progress")]
	public uint SummonProgress
	{
		get
		{
			return __pbn__SummonProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__SummonProgress = value;
		}
	}

	[ProtoMember(5, Name = "boss_id")]
	public uint BossId
	{
		get
		{
			return __pbn__BossId.GetValueOrDefault();
		}
		set
		{
			__pbn__BossId = value;
		}
	}

	[ProtoMember(6, Name = "boss_begin_time")]
	public uint BossBeginTime
	{
		get
		{
			return __pbn__BossBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BossBeginTime = value;
		}
	}

	[ProtoMember(7, Name = "boss_end_time")]
	public uint BossEndTime
	{
		get
		{
			return __pbn__BossEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BossEndTime = value;
		}
	}

	[ProtoMember(8, Name = "boss_total_hp")]
	public uint BossTotalHp
	{
		get
		{
			return __pbn__BossTotalHp.GetValueOrDefault();
		}
		set
		{
			__pbn__BossTotalHp = value;
		}
	}

	[ProtoMember(9, Name = "boss_left_hp")]
	public uint BossLeftHp
	{
		get
		{
			return __pbn__BossLeftHp.GetValueOrDefault();
		}
		set
		{
			__pbn__BossLeftHp = value;
		}
	}

	[ProtoMember(10, Name = "next_fight_time")]
	public uint NextFightTime
	{
		get
		{
			return __pbn__NextFightTime.GetValueOrDefault();
		}
		set
		{
			__pbn__NextFightTime = value;
		}
	}

	[ProtoMember(11, Name = "hp_1_percent")]
	public uint Hp1Percent
	{
		get
		{
			return __pbn__Hp1Percent.GetValueOrDefault();
		}
		set
		{
			__pbn__Hp1Percent = value;
		}
	}

	[ProtoMember(12, Name = "hp_2_percent")]
	public uint Hp2Percent
	{
		get
		{
			return __pbn__Hp2Percent.GetValueOrDefault();
		}
		set
		{
			__pbn__Hp2Percent = value;
		}
	}

	[ProtoMember(13, Name = "hp_3_percent")]
	public uint Hp3Percent
	{
		get
		{
			return __pbn__Hp3Percent.GetValueOrDefault();
		}
		set
		{
			__pbn__Hp3Percent = value;
		}
	}

	[ProtoMember(14, Name = "is_take")]
	public bool IsTake
	{
		get
		{
			return __pbn__IsTake.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTake = value;
		}
	}

	[ProtoMember(15, Name = "open_begin_time")]
	public uint OpenBeginTime
	{
		get
		{
			return __pbn__OpenBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__OpenBeginTime = value;
		}
	}

	[ProtoMember(16, Name = "open_end_time")]
	public uint OpenEndTime
	{
		get
		{
			return __pbn__OpenEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__OpenEndTime = value;
		}
	}

	[ProtoMember(17, Name = "fight_list")]
	public List<ArmadaStageFightInfo> FightLists { get; } = new List<ArmadaStageFightInfo>();


	[ProtoMember(18, Name = "left_fight_times")]
	public uint LeftFightTimes
	{
		get
		{
			return __pbn__LeftFightTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftFightTimes = value;
		}
	}

	[ProtoMember(19, Name = "max_hard_level")]
	public uint MaxHardLevel
	{
		get
		{
			return __pbn__MaxHardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxHardLevel = value;
		}
	}

	[ProtoMember(20, Name = "kill_reward_material_id")]
	public uint KillRewardMaterialId
	{
		get
		{
			return __pbn__KillRewardMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__KillRewardMaterialId = value;
		}
	}

	[ProtoMember(21, Name = "last_damage_rate")]
	public uint LastDamageRate
	{
		get
		{
			return __pbn__LastDamageRate.GetValueOrDefault();
		}
		set
		{
			__pbn__LastDamageRate = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHardGrade()
	{
		return __pbn__HardGrade.HasValue;
	}

	public void ResetHardGrade()
	{
		__pbn__HardGrade = null;
	}

	public bool ShouldSerializeHardLevel()
	{
		return __pbn__HardLevel.HasValue;
	}

	public void ResetHardLevel()
	{
		__pbn__HardLevel = null;
	}

	public bool ShouldSerializeSummonProgress()
	{
		return __pbn__SummonProgress.HasValue;
	}

	public void ResetSummonProgress()
	{
		__pbn__SummonProgress = null;
	}

	public bool ShouldSerializeBossId()
	{
		return __pbn__BossId.HasValue;
	}

	public void ResetBossId()
	{
		__pbn__BossId = null;
	}

	public bool ShouldSerializeBossBeginTime()
	{
		return __pbn__BossBeginTime.HasValue;
	}

	public void ResetBossBeginTime()
	{
		__pbn__BossBeginTime = null;
	}

	public bool ShouldSerializeBossEndTime()
	{
		return __pbn__BossEndTime.HasValue;
	}

	public void ResetBossEndTime()
	{
		__pbn__BossEndTime = null;
	}

	public bool ShouldSerializeBossTotalHp()
	{
		return __pbn__BossTotalHp.HasValue;
	}

	public void ResetBossTotalHp()
	{
		__pbn__BossTotalHp = null;
	}

	public bool ShouldSerializeBossLeftHp()
	{
		return __pbn__BossLeftHp.HasValue;
	}

	public void ResetBossLeftHp()
	{
		__pbn__BossLeftHp = null;
	}

	public bool ShouldSerializeNextFightTime()
	{
		return __pbn__NextFightTime.HasValue;
	}

	public void ResetNextFightTime()
	{
		__pbn__NextFightTime = null;
	}

	public bool ShouldSerializeHp1Percent()
	{
		return __pbn__Hp1Percent.HasValue;
	}

	public void ResetHp1Percent()
	{
		__pbn__Hp1Percent = null;
	}

	public bool ShouldSerializeHp2Percent()
	{
		return __pbn__Hp2Percent.HasValue;
	}

	public void ResetHp2Percent()
	{
		__pbn__Hp2Percent = null;
	}

	public bool ShouldSerializeHp3Percent()
	{
		return __pbn__Hp3Percent.HasValue;
	}

	public void ResetHp3Percent()
	{
		__pbn__Hp3Percent = null;
	}

	public bool ShouldSerializeIsTake()
	{
		return __pbn__IsTake.HasValue;
	}

	public void ResetIsTake()
	{
		__pbn__IsTake = null;
	}

	public bool ShouldSerializeOpenBeginTime()
	{
		return __pbn__OpenBeginTime.HasValue;
	}

	public void ResetOpenBeginTime()
	{
		__pbn__OpenBeginTime = null;
	}

	public bool ShouldSerializeOpenEndTime()
	{
		return __pbn__OpenEndTime.HasValue;
	}

	public void ResetOpenEndTime()
	{
		__pbn__OpenEndTime = null;
	}

	public bool ShouldSerializeLeftFightTimes()
	{
		return __pbn__LeftFightTimes.HasValue;
	}

	public void ResetLeftFightTimes()
	{
		__pbn__LeftFightTimes = null;
	}

	public bool ShouldSerializeMaxHardLevel()
	{
		return __pbn__MaxHardLevel.HasValue;
	}

	public void ResetMaxHardLevel()
	{
		__pbn__MaxHardLevel = null;
	}

	public bool ShouldSerializeKillRewardMaterialId()
	{
		return __pbn__KillRewardMaterialId.HasValue;
	}

	public void ResetKillRewardMaterialId()
	{
		__pbn__KillRewardMaterialId = null;
	}

	public bool ShouldSerializeLastDamageRate()
	{
		return __pbn__LastDamageRate.HasValue;
	}

	public void ResetLastDamageRate()
	{
		__pbn__LastDamageRate = null;
	}
}
