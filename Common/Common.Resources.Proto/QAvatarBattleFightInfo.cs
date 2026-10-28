using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleFightInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__KillNum;

	private uint? __pbn__DeadNum;

	private uint? __pbn__Rank;

	private bool? __pbn__IsMvp;

	private bool? __pbn__IsWin;

	private uint? __pbn__MaxHp;

	private uint? __pbn__CurHp;

	private ulong? __pbn__RebornTimeMsU64;

	private uint? __pbn__MaxSp;

	private uint? __pbn__CurSp;

	[ProtoMember(1, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(2, Name = "kill_num")]
	public uint KillNum
	{
		get
		{
			return __pbn__KillNum.GetValueOrDefault();
		}
		set
		{
			__pbn__KillNum = value;
		}
	}

	[ProtoMember(3, Name = "dead_num")]
	public uint DeadNum
	{
		get
		{
			return __pbn__DeadNum.GetValueOrDefault();
		}
		set
		{
			__pbn__DeadNum = value;
		}
	}

	[ProtoMember(4, Name = "rank")]
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

	[ProtoMember(5, Name = "is_mvp")]
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

	[ProtoMember(6, Name = "is_win")]
	public bool IsWin
	{
		get
		{
			return __pbn__IsWin.GetValueOrDefault();
		}
		set
		{
			__pbn__IsWin = value;
		}
	}

	[ProtoMember(11, Name = "max_hp")]
	public uint MaxHp
	{
		get
		{
			return __pbn__MaxHp.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxHp = value;
		}
	}

	[ProtoMember(12, Name = "cur_hp")]
	public uint CurHp
	{
		get
		{
			return __pbn__CurHp.GetValueOrDefault();
		}
		set
		{
			__pbn__CurHp = value;
		}
	}

	[ProtoMember(13, Name = "reborn_time_ms_u64")]
	public ulong RebornTimeMsU64
	{
		get
		{
			return __pbn__RebornTimeMsU64.GetValueOrDefault();
		}
		set
		{
			__pbn__RebornTimeMsU64 = value;
		}
	}

	[ProtoMember(14, Name = "max_sp")]
	public uint MaxSp
	{
		get
		{
			return __pbn__MaxSp.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxSp = value;
		}
	}

	[ProtoMember(15, Name = "cur_sp")]
	public uint CurSp
	{
		get
		{
			return __pbn__CurSp.GetValueOrDefault();
		}
		set
		{
			__pbn__CurSp = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeKillNum()
	{
		return __pbn__KillNum.HasValue;
	}

	public void ResetKillNum()
	{
		__pbn__KillNum = null;
	}

	public bool ShouldSerializeDeadNum()
	{
		return __pbn__DeadNum.HasValue;
	}

	public void ResetDeadNum()
	{
		__pbn__DeadNum = null;
	}

	public bool ShouldSerializeRank()
	{
		return __pbn__Rank.HasValue;
	}

	public void ResetRank()
	{
		__pbn__Rank = null;
	}

	public bool ShouldSerializeIsMvp()
	{
		return __pbn__IsMvp.HasValue;
	}

	public void ResetIsMvp()
	{
		__pbn__IsMvp = null;
	}

	public bool ShouldSerializeIsWin()
	{
		return __pbn__IsWin.HasValue;
	}

	public void ResetIsWin()
	{
		__pbn__IsWin = null;
	}

	public bool ShouldSerializeMaxHp()
	{
		return __pbn__MaxHp.HasValue;
	}

	public void ResetMaxHp()
	{
		__pbn__MaxHp = null;
	}

	public bool ShouldSerializeCurHp()
	{
		return __pbn__CurHp.HasValue;
	}

	public void ResetCurHp()
	{
		__pbn__CurHp = null;
	}

	public bool ShouldSerializeRebornTimeMsU64()
	{
		return __pbn__RebornTimeMsU64.HasValue;
	}

	public void ResetRebornTimeMsU64()
	{
		__pbn__RebornTimeMsU64 = null;
	}

	public bool ShouldSerializeMaxSp()
	{
		return __pbn__MaxSp.HasValue;
	}

	public void ResetMaxSp()
	{
		__pbn__MaxSp = null;
	}

	public bool ShouldSerializeCurSp()
	{
		return __pbn__CurSp.HasValue;
	}

	public void ResetCurSp()
	{
		__pbn__CurSp = null;
	}
}
