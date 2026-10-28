using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleKillNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5621
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__KillerUid;

	private uint? __pbn__KilledUid;

	private uint? __pbn__KillerStreakNum;

	private uint? __pbn__BattleTotalKillNum;

	[ProtoMember(1, Name = "killer_uid")]
	public uint KillerUid
	{
		get
		{
			return __pbn__KillerUid.GetValueOrDefault();
		}
		set
		{
			__pbn__KillerUid = value;
		}
	}

	[ProtoMember(2, Name = "killed_uid")]
	public uint KilledUid
	{
		get
		{
			return __pbn__KilledUid.GetValueOrDefault();
		}
		set
		{
			__pbn__KilledUid = value;
		}
	}

	[ProtoMember(3, Name = "killer_streak_num")]
	public uint KillerStreakNum
	{
		get
		{
			return __pbn__KillerStreakNum.GetValueOrDefault();
		}
		set
		{
			__pbn__KillerStreakNum = value;
		}
	}

	[ProtoMember(4, Name = "battle_total_kill_num")]
	public uint BattleTotalKillNum
	{
		get
		{
			return __pbn__BattleTotalKillNum.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleTotalKillNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeKillerUid()
	{
		return __pbn__KillerUid.HasValue;
	}

	public void ResetKillerUid()
	{
		__pbn__KillerUid = null;
	}

	public bool ShouldSerializeKilledUid()
	{
		return __pbn__KilledUid.HasValue;
	}

	public void ResetKilledUid()
	{
		__pbn__KilledUid = null;
	}

	public bool ShouldSerializeKillerStreakNum()
	{
		return __pbn__KillerStreakNum.HasValue;
	}

	public void ResetKillerStreakNum()
	{
		__pbn__KillerStreakNum = null;
	}

	public bool ShouldSerializeBattleTotalKillNum()
	{
		return __pbn__BattleTotalKillNum.HasValue;
	}

	public void ResetBattleTotalKillNum()
	{
		__pbn__BattleTotalKillNum = null;
	}
}
