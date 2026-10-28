using System.Linq;

namespace Common.Utils.ExcelReader;

public class AvatarSubSkillData : BaseExcelReader<AvatarSubSkillData, AvatarSubSkillDataExcel>
{
	public override string FileName => "AvatarSubSkillData.json";

	public AvatarSubSkillDataExcel? FromId(int id)
	{
		return base.All.Where((AvatarSubSkillDataExcel subSkill) => subSkill.AvatarSubSkillId == id).FirstOrDefault();
	}
}
