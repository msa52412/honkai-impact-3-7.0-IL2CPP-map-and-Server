using System.Linq;

namespace Common.Utils.ExcelReader;

public class ElfSkillTreeData : BaseExcelReader<ElfSkillTreeData, ElfSkillTreeDataExcel>
{
	public override string FileName => "ElfSkillTreeData.json";

	public ElfSkillTreeDataExcel? FromId(uint id)
	{
		return base.All.Where((ElfSkillTreeDataExcel skillTree) => skillTree.ElfSkillId == id).FirstOrDefault();
	}
}
