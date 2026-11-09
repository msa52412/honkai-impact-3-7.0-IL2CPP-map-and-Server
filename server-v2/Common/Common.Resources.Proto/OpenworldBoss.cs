using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldBoss : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BossId;

	private uint? __pbn__LeftHp;

	private uint? __pbn__ClueProgress;

	private uint? __pbn__SearchTime;

	private uint? __pbn__BattleTime;

	private bool? __pbn__IsTakeKillReward;

	private uint? __pbn__LocationIdx;

	private uint? __pbn__MpLeftHp;

	private uint? __pbn__MinKillTime;

	[ProtoMember(1, Name = "boss_id")]
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

	[ProtoMember(2, Name = "left_hp")]
	public uint LeftHp
	{
		get
		{
			return __pbn__LeftHp.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftHp = value;
		}
	}

	[ProtoMember(3, Name = "clue_progress")]
	public uint ClueProgress
	{
		get
		{
			return __pbn__ClueProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__ClueProgress = value;
		}
	}

	[ProtoMember(4, Name = "search_time")]
	public uint SearchTime
	{
		get
		{
			return __pbn__SearchTime.GetValueOrDefault();
		}
		set
		{
			__pbn__SearchTime = value;
		}
	}

	[ProtoMember(5, Name = "battle_time")]
	public uint BattleTime
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

	[ProtoMember(6, Name = "is_take_kill_reward")]
	public bool IsTakeKillReward
	{
		get
		{
			return __pbn__IsTakeKillReward.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeKillReward = value;
		}
	}

	[ProtoMember(7, Name = "location_idx")]
	public uint LocationIdx
	{
		get
		{
			return __pbn__LocationIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__LocationIdx = value;
		}
	}

	[ProtoMember(9, Name = "boss_killed_avatar_list")]
	public uint[] BossKilledAvatarLists { get; set; }

	[ProtoMember(10, Name = "mp_left_hp")]
	public uint MpLeftHp
	{
		get
		{
			return __pbn__MpLeftHp.GetValueOrDefault();
		}
		set
		{
			__pbn__MpLeftHp = value;
		}
	}

	[ProtoMember(11, Name = "min_kill_time")]
	public uint MinKillTime
	{
		get
		{
			return __pbn__MinKillTime.GetValueOrDefault();
		}
		set
		{
			__pbn__MinKillTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBossId()
	{
		return __pbn__BossId.HasValue;
	}

	public void ResetBossId()
	{
		__pbn__BossId = null;
	}

	public bool ShouldSerializeLeftHp()
	{
		return __pbn__LeftHp.HasValue;
	}

	public void ResetLeftHp()
	{
		__pbn__LeftHp = null;
	}

	public bool ShouldSerializeClueProgress()
	{
		return __pbn__ClueProgress.HasValue;
	}

	public void ResetClueProgress()
	{
		__pbn__ClueProgress = null;
	}

	public bool ShouldSerializeSearchTime()
	{
		return __pbn__SearchTime.HasValue;
	}

	public void ResetSearchTime()
	{
		__pbn__SearchTime = null;
	}

	public bool ShouldSerializeBattleTime()
	{
		return __pbn__BattleTime.HasValue;
	}

	public void ResetBattleTime()
	{
		__pbn__BattleTime = null;
	}

	public bool ShouldSerializeIsTakeKillReward()
	{
		return __pbn__IsTakeKillReward.HasValue;
	}

	public void ResetIsTakeKillReward()
	{
		__pbn__IsTakeKillReward = null;
	}

	public bool ShouldSerializeLocationIdx()
	{
		return __pbn__LocationIdx.HasValue;
	}

	public void ResetLocationIdx()
	{
		__pbn__LocationIdx = null;
	}

	public bool ShouldSerializeMpLeftHp()
	{
		return __pbn__MpLeftHp.HasValue;
	}

	public void ResetMpLeftHp()
	{
		__pbn__MpLeftHp = null;
	}

	public bool ShouldSerializeMinKillTime()
	{
		return __pbn__MinKillTime.HasValue;
	}

	public void ResetMinKillTime()
	{
		__pbn__MinKillTime = null;
	}
}
