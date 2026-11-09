using System.Linq;

namespace Common.Utils.ExcelReader;

public class DormitoryTalkEvent : BaseExcelReader<DormitoryTalkEvent, DormitoryTalkEventExcel>
{
	public override string FileName => "DormitoryTalkEvent.json";

	public DormitoryTalkEventExcel? FromPlotId(uint plotId)
	{
		return base.All.Where((DormitoryTalkEventExcel x) => x.PlotId == plotId).FirstOrDefault();
	}
}
