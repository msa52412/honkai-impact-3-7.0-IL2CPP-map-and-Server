using System.Linq;

namespace Common.Utils.ExcelReader;

public class MaterialData : BaseExcelReader<MaterialData, MaterialDataExcel>
{
	public override string FileName => "MaterialData.json";

	public MaterialDataExcel? FromId(uint id)
	{
		return base.All.Where((MaterialDataExcel material) => material.Id == id).FirstOrDefault();
	}
}
