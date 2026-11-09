using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RanchMonsterWiki : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MonsterId;

	private uint? __pbn__UnlockTime;

	private uint? __pbn__BattleTimes;

	private uint? __pbn__CatchTimes;

	[ProtoMember(1, Name = "monster_id")]
	public uint MonsterId
	{
		get
		{
			return __pbn__MonsterId.GetValueOrDefault();
		}
		set
		{
			__pbn__MonsterId = value;
		}
	}

	[ProtoMember(2, Name = "unlock_time")]
	public uint UnlockTime
	{
		get
		{
			return __pbn__UnlockTime.GetValueOrDefault();
		}
		set
		{
			__pbn__UnlockTime = value;
		}
	}

	[ProtoMember(3, Name = "battle_times")]
	public uint BattleTimes
	{
		get
		{
			return __pbn__BattleTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleTimes = value;
		}
	}

	[ProtoMember(4, Name = "catch_times")]
	public uint CatchTimes
	{
		get
		{
			return __pbn__CatchTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__CatchTimes = value;
		}
	}

	[ProtoMember(5, Name = "skill_id_list")]
	public uint[] SkillIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMonsterId()
	{
		return __pbn__MonsterId.HasValue;
	}

	public void ResetMonsterId()
	{
		__pbn__MonsterId = null;
	}

	public bool ShouldSerializeUnlockTime()
	{
		return __pbn__UnlockTime.HasValue;
	}

	public void ResetUnlockTime()
	{
		__pbn__UnlockTime = null;
	}

	public bool ShouldSerializeBattleTimes()
	{
		return __pbn__BattleTimes.HasValue;
	}

	public void ResetBattleTimes()
	{
		__pbn__BattleTimes = null;
	}

	public bool ShouldSerializeCatchTimes()
	{
		return __pbn__CatchTimes.HasValue;
	}

	public void ResetCatchTimes()
	{
		__pbn__CatchTimes = null;
	}
}
