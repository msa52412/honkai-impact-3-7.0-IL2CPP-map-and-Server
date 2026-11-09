using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MassiveWarBattleInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleBeginTime;

	private uint? __pbn__StageId;

	private uint? __pbn__BattleUniqId;

	private uint? __pbn__AutoLoseHpTimeId;

	private uint? __pbn__BattleStatus;

	private uint? __pbn__TotalMember;

	private uint? __pbn__SelfRank;

	private uint? __pbn__MonsterHardLevel;

	private uint? __pbn__SelfBuffProgress;

	[ProtoMember(1, Name = "battle_begin_time")]
	public uint BattleBeginTime
	{
		get
		{
			return __pbn__BattleBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleBeginTime = value;
		}
	}

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

	[ProtoMember(3, Name = "battle_uniq_id")]
	public uint BattleUniqId
	{
		get
		{
			return __pbn__BattleUniqId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleUniqId = value;
		}
	}

	[ProtoMember(4, Name = "auto_lose_hp_time_id")]
	public uint AutoLoseHpTimeId
	{
		get
		{
			return __pbn__AutoLoseHpTimeId.GetValueOrDefault();
		}
		set
		{
			__pbn__AutoLoseHpTimeId = value;
		}
	}

	[ProtoMember(5, Name = "battle_status")]
	public uint BattleStatus
	{
		get
		{
			return __pbn__BattleStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleStatus = value;
		}
	}

	[ProtoMember(6, Name = "total_member")]
	public uint TotalMember
	{
		get
		{
			return __pbn__TotalMember.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalMember = value;
		}
	}

	[ProtoMember(7, Name = "self_rank")]
	public uint SelfRank
	{
		get
		{
			return __pbn__SelfRank.GetValueOrDefault();
		}
		set
		{
			__pbn__SelfRank = value;
		}
	}

	[ProtoMember(8, Name = "self_monster_damage_list")]
	public List<MassiveWarMonsterDamage> SelfMonsterDamageLists { get; } = new List<MassiveWarMonsterDamage>();


	[ProtoMember(9, Name = "monster_list")]
	public List<MassiveWarBattleMonster> MonsterLists { get; } = new List<MassiveWarBattleMonster>();


	[ProtoMember(10, Name = "top_member_list")]
	public List<MassiveWarBattleMember> TopMemberLists { get; } = new List<MassiveWarBattleMember>();


	[ProtoMember(11, Name = "monster_hard_level")]
	public uint MonsterHardLevel
	{
		get
		{
			return __pbn__MonsterHardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MonsterHardLevel = value;
		}
	}

	[ProtoMember(12, Name = "self_buff_progress")]
	public uint SelfBuffProgress
	{
		get
		{
			return __pbn__SelfBuffProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__SelfBuffProgress = value;
		}
	}

	[ProtoMember(13, Name = "field_progress")]
	public MassiveWarFieldProgress FieldProgress { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleBeginTime()
	{
		return __pbn__BattleBeginTime.HasValue;
	}

	public void ResetBattleBeginTime()
	{
		__pbn__BattleBeginTime = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeBattleUniqId()
	{
		return __pbn__BattleUniqId.HasValue;
	}

	public void ResetBattleUniqId()
	{
		__pbn__BattleUniqId = null;
	}

	public bool ShouldSerializeAutoLoseHpTimeId()
	{
		return __pbn__AutoLoseHpTimeId.HasValue;
	}

	public void ResetAutoLoseHpTimeId()
	{
		__pbn__AutoLoseHpTimeId = null;
	}

	public bool ShouldSerializeBattleStatus()
	{
		return __pbn__BattleStatus.HasValue;
	}

	public void ResetBattleStatus()
	{
		__pbn__BattleStatus = null;
	}

	public bool ShouldSerializeTotalMember()
	{
		return __pbn__TotalMember.HasValue;
	}

	public void ResetTotalMember()
	{
		__pbn__TotalMember = null;
	}

	public bool ShouldSerializeSelfRank()
	{
		return __pbn__SelfRank.HasValue;
	}

	public void ResetSelfRank()
	{
		__pbn__SelfRank = null;
	}

	public bool ShouldSerializeMonsterHardLevel()
	{
		return __pbn__MonsterHardLevel.HasValue;
	}

	public void ResetMonsterHardLevel()
	{
		__pbn__MonsterHardLevel = null;
	}

	public bool ShouldSerializeSelfBuffProgress()
	{
		return __pbn__SelfBuffProgress.HasValue;
	}

	public void ResetSelfBuffProgress()
	{
		__pbn__SelfBuffProgress = null;
	}
}
