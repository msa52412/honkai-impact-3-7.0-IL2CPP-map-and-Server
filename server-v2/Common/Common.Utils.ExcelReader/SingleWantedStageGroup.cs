using System.Linq;

namespace Common.Utils.ExcelReader;

public class SingleWantedStageGroup : BaseExcelReader<SingleWantedStageGroup, SingleWantedStageGroupExcel>
{
	public override string FileName => "SingleWantedStageGroup.json";

	public SingleWantedStageGroupExcel? FromGroupId(int groupId)
	{
		return base.All.FirstOrDefault((SingleWantedStageGroupExcel group) => group.StageGroupId == groupId);
	}
}
