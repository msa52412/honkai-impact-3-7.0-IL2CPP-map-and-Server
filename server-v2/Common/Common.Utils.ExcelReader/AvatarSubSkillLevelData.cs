using System.Collections.Generic;
using System.Linq;

namespace Common.Utils.ExcelReader;

internal class AvatarSubSkillLevelData : BaseExcelReader<AvatarSubSkillLevelData, AvatarSubSkillLevelDataExcel>
{
	public override string FileName => "AvatarSubSkillLevelDataV2.json";

	public List<AvatarSubSkillLevelDataExcel> FromType(int type)
	{
		return base.All.Where((AvatarSubSkillLevelDataExcel d) => d.ItemType == type).ToList();
	}
}
