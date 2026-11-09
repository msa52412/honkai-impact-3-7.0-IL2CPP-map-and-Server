using System.Linq;

namespace Common.Utils.ExcelReader;

public class DormitoryDecoration : BaseExcelReader<DormitoryDecoration, DormitoryDecorationExcel>
{
	public override string FileName => "DormitoryDecoration.json";

	/// <summary>某个宿舍升到指定装修等级所需的配置（舒适度门槛 / 耗时 / 材料）。</summary>
	public DormitoryDecorationExcel? FromLevel(int houseId, int level)
	{
		return base.All.Where((DormitoryDecorationExcel x) => x.TargetHouse == houseId && x.DecorationLevel == level).FirstOrDefault();
	}

	public int MaxLevel(int houseId)
	{
		return base.All.Where((DormitoryDecorationExcel x) => x.TargetHouse == houseId).Select((DormitoryDecorationExcel x) => x.DecorationLevel).DefaultIfEmpty(1).Max();
	}
}
