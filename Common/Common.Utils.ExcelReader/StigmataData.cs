using System.Linq;

namespace Common.Utils.ExcelReader;

public class StigmataData : BaseExcelReader<StigmataData, StigmataDataExcel>
{
	public override string FileName => "StigmataData.json";

	public StigmataDataExcel? FromId(int id)
	{
		return base.All.Where((StigmataDataExcel stigmata) => stigmata.Id == id).FirstOrDefault();
	}
}
