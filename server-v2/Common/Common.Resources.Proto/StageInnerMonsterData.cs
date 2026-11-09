using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageInnerMonsterData : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__MonsterName;

	private string __pbn__MonsterType;

	private uint? __pbn__MonsterLevel;

	private uint? __pbn__MonsterNum;

	private uint? __pbn__AvgOutput;

	private uint? __pbn__AvgLiveTime;

	private uint? __pbn__Dps;

	private uint? __pbn__HitAvatarTimes;

	private uint? __pbn__BreakAvatarTimes;

	[ProtoMember(1, Name = "monster_name")]
	[DefaultValue("")]
	public string MonsterName
	{
		get
		{
			return __pbn__MonsterName ?? "";
		}
		set
		{
			__pbn__MonsterName = value;
		}
	}

	[ProtoMember(2, Name = "monster_type")]
	[DefaultValue("")]
	public string MonsterType
	{
		get
		{
			return __pbn__MonsterType ?? "";
		}
		set
		{
			__pbn__MonsterType = value;
		}
	}

	[ProtoMember(3, Name = "monster_level")]
	public uint MonsterLevel
	{
		get
		{
			return __pbn__MonsterLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MonsterLevel = value;
		}
	}

	[ProtoMember(4, Name = "monster_num")]
	public uint MonsterNum
	{
		get
		{
			return __pbn__MonsterNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MonsterNum = value;
		}
	}

	[ProtoMember(5, Name = "avg_output")]
	public uint AvgOutput
	{
		get
		{
			return __pbn__AvgOutput.GetValueOrDefault();
		}
		set
		{
			__pbn__AvgOutput = value;
		}
	}

	[ProtoMember(6, Name = "avg_live_time")]
	public uint AvgLiveTime
	{
		get
		{
			return __pbn__AvgLiveTime.GetValueOrDefault();
		}
		set
		{
			__pbn__AvgLiveTime = value;
		}
	}

	[ProtoMember(7, Name = "dps")]
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

	[ProtoMember(8, Name = "hit_avatar_times")]
	public uint HitAvatarTimes
	{
		get
		{
			return __pbn__HitAvatarTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__HitAvatarTimes = value;
		}
	}

	[ProtoMember(9, Name = "break_avatar_times")]
	public uint BreakAvatarTimes
	{
		get
		{
			return __pbn__BreakAvatarTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__BreakAvatarTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMonsterName()
	{
		return __pbn__MonsterName != null;
	}

	public void ResetMonsterName()
	{
		__pbn__MonsterName = null;
	}

	public bool ShouldSerializeMonsterType()
	{
		return __pbn__MonsterType != null;
	}

	public void ResetMonsterType()
	{
		__pbn__MonsterType = null;
	}

	public bool ShouldSerializeMonsterLevel()
	{
		return __pbn__MonsterLevel.HasValue;
	}

	public void ResetMonsterLevel()
	{
		__pbn__MonsterLevel = null;
	}

	public bool ShouldSerializeMonsterNum()
	{
		return __pbn__MonsterNum.HasValue;
	}

	public void ResetMonsterNum()
	{
		__pbn__MonsterNum = null;
	}

	public bool ShouldSerializeAvgOutput()
	{
		return __pbn__AvgOutput.HasValue;
	}

	public void ResetAvgOutput()
	{
		__pbn__AvgOutput = null;
	}

	public bool ShouldSerializeAvgLiveTime()
	{
		return __pbn__AvgLiveTime.HasValue;
	}

	public void ResetAvgLiveTime()
	{
		__pbn__AvgLiveTime = null;
	}

	public bool ShouldSerializeDps()
	{
		return __pbn__Dps.HasValue;
	}

	public void ResetDps()
	{
		__pbn__Dps = null;
	}

	public bool ShouldSerializeHitAvatarTimes()
	{
		return __pbn__HitAvatarTimes.HasValue;
	}

	public void ResetHitAvatarTimes()
	{
		__pbn__HitAvatarTimes = null;
	}

	public bool ShouldSerializeBreakAvatarTimes()
	{
		return __pbn__BreakAvatarTimes.HasValue;
	}

	public void ResetBreakAvatarTimes()
	{
		__pbn__BreakAvatarTimes = null;
	}
}
