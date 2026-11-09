using System.Collections.Generic;
using System.Linq;

namespace Common.Utils.ExcelReader;

public class ElfSkillData : BaseExcelReader<ElfSkillData, ElfSkillDataExcel>
{
	public override string FileName => "ElfSkillData.json";

	public ElfSkillDataExcel? FromId(uint id)
	{
		return base.All.Where((ElfSkillDataExcel skill) => skill.ElfSkillId == id).FirstOrDefault();
	}

	public List<ElfSkillDataExcel>? FromElfId(uint id)
	{
		return base.All.Where((ElfSkillDataExcel skill) => skill.ElfId == id).ToList();
	}
}
