using System.Collections.Generic;
using System.Linq;

namespace Common.Utils.ExcelReader;

public class GeneralActivity : BaseExcelReader<GeneralActivity, GeneralActivityExcel>
{
	public override string FileName => "GeneralActivity.json";

	public List<GeneralActivityExcel> GetAllDistinct()
	{
		return base.All.Distinct(new GeneralActivityDisplayComparer()).ToList();
	}
}
