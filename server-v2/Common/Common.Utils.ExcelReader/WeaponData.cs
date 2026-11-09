using System.Linq;

namespace Common.Utils.ExcelReader;

public class WeaponData : BaseExcelReader<WeaponData, WeaponDataExcel>
{
	public override string FileName => "WeaponData.json";

	public WeaponDataExcel? FromId(int id)
	{
		return base.All.Where((WeaponDataExcel weapon) => weapon.Id == id).FirstOrDefault();
	}
}
