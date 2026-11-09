using System.Linq;

namespace Common.Utils.ExcelReader;

public class DormitoryHouseData : BaseExcelReader<DormitoryHouseData, DormitoryHouseDataExcel>
{
	public override string FileName => "DormitoryHouseData.json";

	public DormitoryHouseDataExcel? FromId(int id)
	{
		return base.All.Where((DormitoryHouseDataExcel DormHouse) => DormHouse.HouseId == id).FirstOrDefault();
	}
}
