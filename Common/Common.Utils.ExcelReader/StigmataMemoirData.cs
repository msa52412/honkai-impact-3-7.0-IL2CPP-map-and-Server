using System.Linq;

namespace Common.Utils.ExcelReader;

public class StigmataMemoirData : BaseExcelReader<StigmataMemoirData, StigmataMemoirDataExcel>
{
	public override string FileName => "StigmataMemoirData.json";

	public StigmataMemoirDataExcel? FromId(int id)
	{
		return base.All.Where((StigmataMemoirDataExcel smd) => smd.AvatarId == id).FirstOrDefault();
	}
}
