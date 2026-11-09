using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class Elf : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ElfId;

	private uint? __pbn__Star;

	private uint? __pbn__Level;

	private uint? __pbn__Exp;

	private uint? __pbn__CompensateLevel;

	private uint? __pbn__TotalCompensateExp;

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

	[ProtoMember(2, Name = "star")]
	public uint Star
	{
		get
		{
			return __pbn__Star.GetValueOrDefault();
		}
		set
		{
			__pbn__Star = value;
		}
	}

	[ProtoMember(3, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(4, Name = "exp")]
	public uint Exp
	{
		get
		{
			return __pbn__Exp.GetValueOrDefault();
		}
		set
		{
			__pbn__Exp = value;
		}
	}

	[ProtoMember(5, Name = "equip_talent_id_list")]
	public uint[] EquipTalentIdLists { get; set; }

	[ProtoMember(6, Name = "skill_list")]
	public List<ElfSkill> SkillLists { get; } = new List<ElfSkill>();


	[ProtoMember(7, Name = "compensate_level")]
	public uint CompensateLevel
	{
		get
		{
			return __pbn__CompensateLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__CompensateLevel = value;
		}
	}

	[ProtoMember(8, Name = "total_compensate_exp")]
	public uint TotalCompensateExp
	{
		get
		{
			return __pbn__TotalCompensateExp.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalCompensateExp = value;
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

	public bool ShouldSerializeStar()
	{
		return __pbn__Star.HasValue;
	}

	public void ResetStar()
	{
		__pbn__Star = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeExp()
	{
		return __pbn__Exp.HasValue;
	}

	public void ResetExp()
	{
		__pbn__Exp = null;
	}

	public bool ShouldSerializeCompensateLevel()
	{
		return __pbn__CompensateLevel.HasValue;
	}

	public void ResetCompensateLevel()
	{
		__pbn__CompensateLevel = null;
	}

	public bool ShouldSerializeTotalCompensateExp()
	{
		return __pbn__TotalCompensateExp.HasValue;
	}

	public void ResetTotalCompensateExp()
	{
		__pbn__TotalCompensateExp = null;
	}
}
