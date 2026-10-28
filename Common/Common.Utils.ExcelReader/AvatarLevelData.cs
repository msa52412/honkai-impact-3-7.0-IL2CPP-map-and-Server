using System.Linq;

namespace Common.Utils.ExcelReader;

public class AvatarLevelData : BaseExcelReader<AvatarLevelData, AvatarLevelDataExcel>
{
	public override string FileName => "AvatarLevelData.json";

	public PlayerLevelData.LevelData CalculateLevel(int level, int exp)
	{
		int num = exp;
		foreach (AvatarLevelDataExcel item in base.All.Where((AvatarLevelDataExcel levelData) => levelData.Level >= level))
		{
			if (num < 1)
			{
				break;
			}
			if (num < item.Exp)
			{
				continue;
			}
			if (level == base.All.OrderByDescending((AvatarLevelDataExcel level) => level.Level).First().Level)
			{
				num = base.All.OrderByDescending((AvatarLevelDataExcel level) => level.Level).First().Exp;
				break;
			}
			level++;
			num -= item.Exp;
		}
		return new PlayerLevelData.LevelData(level, num);
	}

	public int CalculateCost(int startLevel, int endLevel)
	{
		int[] source = (from level in base.All
			where level.Level > startLevel && level.Level < endLevel
			select level.Cost).ToArray();
		return source.Sum();
	}
}
