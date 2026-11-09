using System.Linq;

namespace Common.Utils.ExcelReader;

public class RewardData : BaseExcelReader<RewardData, RewardDataExcel>
{
	public override string FileName => "RewardData.json";

	public RewardDataExcel? FromId(int id)
	{
		return base.All.Where((RewardDataExcel x) => x.RewardId == id).FirstOrDefault();
	}
}
