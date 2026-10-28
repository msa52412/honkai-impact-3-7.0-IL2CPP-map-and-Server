using System.Linq;

namespace Common.Utils.ExcelReader;

public class GrandKeyData : BaseExcelReader<GrandKeyData, GrandKeyDataExcel>
{
	public override string FileName => "GrandKey.json";

	public GrandKeyDataExcel? FromId(int id)
	{
		return base.All.Where((GrandKeyDataExcel grandKey) => grandKey.GrandKeyId == id).FirstOrDefault();
	}
}
