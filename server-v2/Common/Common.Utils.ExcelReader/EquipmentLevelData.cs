using System.Linq;

namespace Common.Utils.ExcelReader;

public class EquipmentLevelData : BaseExcelReader<EquipmentLevelData, EquipmentLevelDataExcel>
{
	public override string FileName => "EquipmentLevelData.json";

	public EquipmentLevelDataExcel? FromLevel(int level)
	{
		return base.All.Where((EquipmentLevelDataExcel levelData) => levelData.Level == level).FirstOrDefault();
	}

	public PlayerLevelData.LevelData CalculateLevel(int level, int exp, int expType)
	{
		int num = exp;
		foreach (EquipmentLevelDataExcel item in base.All.Where((EquipmentLevelDataExcel levelData) => levelData.Level >= level))
		{
			if (num < 1)
			{
				break;
			}
			if (num < item.Type1[expType])
			{
				continue;
			}
			if (level == base.All.OrderByDescending((EquipmentLevelDataExcel level) => level.Level).First().Level)
			{
				num = base.All.OrderByDescending((EquipmentLevelDataExcel level) => level.Level).First().Type1[expType];
				break;
			}
			level++;
			num -= item.Type1[expType];
		}
		return new PlayerLevelData.LevelData(level, num);
	}
}
