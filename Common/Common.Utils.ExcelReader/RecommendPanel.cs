using System.Linq;

namespace Common.Utils.ExcelReader;

public class RecommendPanel : BaseExcelReader<RecommendPanel, RecommendPanelExcel>
{
	public override string FileName => "RecommendPanel.json";

	public RecommendPanelExcel? FromId(int id)
	{
		return base.All.Where((RecommendPanelExcel x) => x.PanelId == id).FirstOrDefault();
	}
}
