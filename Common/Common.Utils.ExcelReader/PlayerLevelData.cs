using System.Linq;

namespace Common.Utils.ExcelReader;

public class PlayerLevelData : BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>
{
	public struct LevelData
	{
		public int Level { get; set; }

		public int Exp { get; set; }

		public LevelData(int level, int exp)
		{
			Level = level;
			Exp = exp;
		}
	}

	public override string FileName => "PlayerLevelData.json";

	public PlayerLevelDataExcel? FromLevel(int level)
	{
		return base.All.Where((PlayerLevelDataExcel levelData) => levelData.Level == level).FirstOrDefault();
	}

	public LevelData CalculateLevel(int exp)
	{
		int num = 1;
		int num2 = exp;
		PlayerLevelDataExcel[] all = base.All;
		foreach (PlayerLevelDataExcel playerLevelDataExcel in all)
		{
			if (num2 < 1)
			{
				break;
			}
			if (num2 >= playerLevelDataExcel.Exp)
			{
				num++;
				num2 -= playerLevelDataExcel.Exp;
			}
		}
		return new LevelData(num, num2);
	}

	public uint ExBossRankFromExp(int exp)
	{
		int level = CalculateLevel(exp).Level;
		if (level >= 81)
		{
			return 104u;
		}
		if (level >= 70)
		{
			return 103u;
		}
		if (level >= 56)
		{
			return 102u;
		}
		return 101u;
	}

	public LevelData CalculateExpForLevel(int level)
	{
		int num = 0;
		PlayerLevelDataExcel[] all = base.All;
		foreach (PlayerLevelDataExcel playerLevelDataExcel in all)
		{
			if (playerLevelDataExcel.Level >= level)
			{
				break;
			}
			num += playerLevelDataExcel.Exp;
		}
		return new LevelData(level, num);
	}

	public int GetMaxPossibleExp()
	{
		return base.All.Select((PlayerLevelDataExcel x) => x.Exp).Sum() - 1;
	}
}
