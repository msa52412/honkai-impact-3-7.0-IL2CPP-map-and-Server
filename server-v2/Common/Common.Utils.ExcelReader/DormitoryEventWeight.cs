using System.Collections.Generic;
using System.Linq;

namespace Common.Utils.ExcelReader;

public class DormitoryEventWeight : BaseExcelReader<DormitoryEventWeight, DormitoryEventWeightExcel>
{
	public override string FileName => "DormitoryEventWeight.json";

	public List<DormitoryEventWeightExcel> FromRoleId(int roleId)
	{
		return base.All.Where((DormitoryEventWeightExcel x) => x.RoleId == roleId).ToList();
	}
}
