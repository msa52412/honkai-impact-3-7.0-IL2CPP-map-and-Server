using System.Linq;

namespace Common.Utils.ExcelReader;

public class DormitoryInteractEvent : BaseExcelReader<DormitoryInteractEvent, DormitoryInteractEventExcel>
{
	public override string FileName => "DormitoryInteractEvent.json";

	public DormitoryInteractEventExcel? FromEventId(uint eventId)
	{
		return base.All.Where((DormitoryInteractEventExcel x) => x.EventId == eventId).FirstOrDefault();
	}
}
