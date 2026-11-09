using System;
using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace Common.Database;

public class ElfsScheme : IRow
{
	public class Elf : Common.Resources.Proto.Elf
	{
		public new List<ElfSkill> SkillLists { get; set; } = new List<ElfSkill>();

	}

	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	public List<Elf> Elves { get; set; }

	public ElfFragment[] ElfFragments { get; set; }

	public void Save()
	{
		Elfs.collection.ReplaceOne(this);
	}

	public void AddElf(int elfId)
	{
		ElfDataExcel elfDataExcel = BaseExcelReader<ElfData, ElfDataExcel>.GetInstance().FromId(elfId);
		if (elfDataExcel == null)
		{
			throw new ArgumentException("Invalid ElfId");
		}
		Console.Write("adding to elf:" + elfId);
		if (Elves.Where((Elf elf) => elf.ElfId == elfId).FirstOrDefault() == null)
		{
			List<ElfSkillDataExcel> list = BaseExcelReader<ElfSkillData, ElfSkillDataExcel>.GetInstance().FromElfId((uint)elfId);
			List<ElfSkill> elfSkills = new List<ElfSkill>();
			list?.ForEach(delegate(ElfSkillDataExcel skill)
			{
				ElfSkill item2 = new ElfSkill
				{
					SkillId = (uint)skill.ElfSkillId,
					SkillLevel = (uint)skill.MaxLv
				};
				elfSkills.Add(item2);
			});
			Elf item = new Elf
			{
				ElfId = (uint)elfDataExcel.ElfId,
				Level = 80u,
				Exp = 0u,
				Star = 7u,
				SkillLists = elfSkills
			};
			Elves.Add(item);
			Save();
		}
	}

	public ElfSkill? SkillLevelUp(uint elfid, uint skillid)
	{
		Elf elf = Elves.Where((Elf e) => e.ElfId == elfid).FirstOrDefault();
		if (elf != null)
		{
			ElfSkill elfSkill = elf.SkillLists.Where((ElfSkill s) => s.SkillId == skillid).FirstOrDefault();
			if (elfSkill != null)
			{
				elfSkill.SkillLevel++;
				return elfSkill;
			}
			ElfSkill elfSkill2 = new ElfSkill();
			elfSkill2.SkillId = skillid;
			elfSkill2.SkillLevel = 1u;
			elf.SkillLists.Add(elfSkill2);
			return elfSkill2;
		}
		return null;
	}

	public void LevelUpElf(int id)
	{
		Elves.ForEach(delegate(Elf elf)
		{
			if (elf.ElfId == id)
			{
				elf.Level++;
			}
		});
	}

	public void SetElfLevel(int id, uint level)
	{
		Elves.ForEach(delegate(Elf elf)
		{
			if (elf.ElfId == id)
			{
				elf.Level = level;
			}
		});
	}

	public void AddFragment(int elfId, int num)
	{
		ElfDataExcel elfDataExcel = BaseExcelReader<ElfData, ElfDataExcel>.GetInstance().FromId(elfId);
		if (elfDataExcel == null)
		{
			throw new ArgumentException("Invalid elfId");
		}
		int num2 = Array.FindIndex(ElfFragments, (ElfFragment ef) => ef.ElfId == elfId);
		if (num2 > 0)
		{
			ElfFragments[num2].FragmentNum += (uint)num;
			return;
		}
		ElfFragment element = new ElfFragment
		{
			ElfId = (uint)elfId,
			FragmentNum = (uint)num
		};
		ElfFragments = ElfFragments.Append(element).ToArray();
	}
}
