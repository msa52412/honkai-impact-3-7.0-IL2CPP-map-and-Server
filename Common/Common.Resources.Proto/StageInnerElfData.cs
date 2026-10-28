using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageInnerElfData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ElfId;

	private uint? __pbn__ElfStar;

	private uint? __pbn__ElfLevel;

	private uint? __pbn__AttackTimes;

	private uint? __pbn__HitTimes;

	private uint? __pbn__TotalOutput;

	private uint? __pbn__NormalOutput;

	private uint? __pbn__UltraOutput;

	private uint? __pbn__TotalSpRecover;

	private uint? __pbn__SkillUseTimes;

	[ProtoMember(1, Name = "elf_id")]
	public uint ElfId
	{
		get
		{
			return __pbn__ElfId.GetValueOrDefault();
		}
		set
		{
			__pbn__ElfId = value;
		}
	}

	[ProtoMember(2, Name = "elf_star")]
	public uint ElfStar
	{
		get
		{
			return __pbn__ElfStar.GetValueOrDefault();
		}
		set
		{
			__pbn__ElfStar = value;
		}
	}

	[ProtoMember(3, Name = "elf_level")]
	public uint ElfLevel
	{
		get
		{
			return __pbn__ElfLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ElfLevel = value;
		}
	}

	[ProtoMember(4, Name = "attack_times")]
	public uint AttackTimes
	{
		get
		{
			return __pbn__AttackTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__AttackTimes = value;
		}
	}

	[ProtoMember(5, Name = "hit_times")]
	public uint HitTimes
	{
		get
		{
			return __pbn__HitTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__HitTimes = value;
		}
	}

	[ProtoMember(6, Name = "total_output")]
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

	[ProtoMember(7, Name = "normal_output")]
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

	[ProtoMember(8, Name = "ultra_output")]
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

	[ProtoMember(9, Name = "total_sp_recover")]
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

	[ProtoMember(10, Name = "skill_use_times")]
	public uint SkillUseTimes
	{
		get
		{
			return __pbn__SkillUseTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__SkillUseTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeElfId()
	{
		return __pbn__ElfId.HasValue;
	}

	public void ResetElfId()
	{
		__pbn__ElfId = null;
	}

	public bool ShouldSerializeElfStar()
	{
		return __pbn__ElfStar.HasValue;
	}

	public void ResetElfStar()
	{
		__pbn__ElfStar = null;
	}

	public bool ShouldSerializeElfLevel()
	{
		return __pbn__ElfLevel.HasValue;
	}

	public void ResetElfLevel()
	{
		__pbn__ElfLevel = null;
	}

	public bool ShouldSerializeAttackTimes()
	{
		return __pbn__AttackTimes.HasValue;
	}

	public void ResetAttackTimes()
	{
		__pbn__AttackTimes = null;
	}

	public bool ShouldSerializeHitTimes()
	{
		return __pbn__HitTimes.HasValue;
	}

	public void ResetHitTimes()
	{
		__pbn__HitTimes = null;
	}

	public bool ShouldSerializeTotalOutput()
	{
		return __pbn__TotalOutput.HasValue;
	}

	public void ResetTotalOutput()
	{
		__pbn__TotalOutput = null;
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

	public bool ShouldSerializeTotalSpRecover()
	{
		return __pbn__TotalSpRecover.HasValue;
	}

	public void ResetTotalSpRecover()
	{
		__pbn__TotalSpRecover = null;
	}

	public bool ShouldSerializeSkillUseTimes()
	{
		return __pbn__SkillUseTimes.HasValue;
	}

	public void ResetSkillUseTimes()
	{
		__pbn__SkillUseTimes = null;
	}
}
