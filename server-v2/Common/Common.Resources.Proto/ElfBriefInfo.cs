using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ElfBriefInfo : IExtensible
{
	[ProtoContract]
	public class Skill : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__SkillId;

		private uint? __pbn__SkillLevel;

		[ProtoMember(1, Name = "skill_id")]
		public uint SkillId
		{
			get
			{
				return __pbn__SkillId.GetValueOrDefault();
			}
			set
			{
				__pbn__SkillId = value;
			}
		}

		[ProtoMember(2, Name = "skill_level")]
		public uint SkillLevel
		{
			get
			{
				return __pbn__SkillLevel.GetValueOrDefault();
			}
			set
			{
				__pbn__SkillLevel = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeSkillId()
		{
			return __pbn__SkillId.HasValue;
		}

		public void ResetSkillId()
		{
			__pbn__SkillId = null;
		}

		public bool ShouldSerializeSkillLevel()
		{
			return __pbn__SkillLevel.HasValue;
		}

		public void ResetSkillLevel()
		{
			__pbn__SkillLevel = null;
		}
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ElfId;

	private uint? __pbn__ElfLevel;

	private uint? __pbn__ElfStar;

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

	[ProtoMember(2, Name = "elf_level")]
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

	[ProtoMember(3, Name = "elf_star")]
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

	[ProtoMember(4, Name = "equip_talent_id_list")]
	public uint[] EquipTalentIdLists { get; set; }

	[ProtoMember(5, Name = "skill_list")]
	public List<Skill> SkillLists { get; } = new List<Skill>();


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

	public bool ShouldSerializeElfLevel()
	{
		return __pbn__ElfLevel.HasValue;
	}

	public void ResetElfLevel()
	{
		__pbn__ElfLevel = null;
	}

	public bool ShouldSerializeElfStar()
	{
		return __pbn__ElfStar.HasValue;
	}

	public void ResetElfStar()
	{
		__pbn__ElfStar = null;
	}
}
