using System.Linq;

namespace Common.Utils.ExcelReader;

public class DormitoryFurnitureData : BaseExcelReader<DormitoryFurnitureData, DormitoryFurnitureDataExcel>
{
	public override string FileName => "DormitoryFurnitureData.json";

	public DormitoryFurnitureDataExcel? FromId(int id)
	{
		return base.All.Where((DormitoryFurnitureDataExcel DormFurniture) => DormFurniture.FurnitureId == id).FirstOrDefault();
	}
}
