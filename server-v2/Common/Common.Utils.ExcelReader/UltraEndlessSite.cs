using System.Linq;

namespace Common.Utils.ExcelReader;

public class UltraEndlessSite : BaseExcelReader<UltraEndlessSite, UltraEndlessSiteExcel>
{
	public override string FileName => "UltraEndlessSite.json";

	public UltraEndlessSiteExcel? FromId(int id)
	{
		return base.All.Where((UltraEndlessSiteExcel site) => site.SiteId == id).FirstOrDefault();
	}
}
