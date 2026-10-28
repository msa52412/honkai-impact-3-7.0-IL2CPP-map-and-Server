using System.Linq;

namespace Common.Utils.ExcelReader;

public class AvatarSkillData : BaseExcelReader<AvatarSkillData, AvatarSkillDataExcel>
{
	public override string FileName => "AvatarSkillData.json";

	public AvatarSkillDataExcel? FromId(int id)
	{
		return base.All.Where((AvatarSkillDataExcel subSkill) => subSkill.SkillId == id).FirstOrDefault();
	}
}
