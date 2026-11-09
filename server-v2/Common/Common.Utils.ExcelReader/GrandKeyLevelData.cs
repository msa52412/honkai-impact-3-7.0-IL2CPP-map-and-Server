using System.Collections.Generic;
using System.Linq;

namespace Common.Utils.ExcelReader;

/// <summary>
/// 神之键等级表（GrandKeyLevel.json）读取器。懒加载，第一次用到才读文件。
/// </summary>
public class GrandKeyLevelData : BaseExcelReader<GrandKeyLevelData, GrandKeyLevelDataExcel>
{
	public override string FileName => "GrandKeyLevel.json";

	public GrandKeyLevelDataExcel? FromId(int grandKeyId, int level)
	{
		return base.All.Where((GrandKeyLevelDataExcel x) => x.GrandKeyId == grandKeyId && x.Level == level).FirstOrDefault();
	}

	/// <summary>该神之键的最大等级（按表里最大的 level；表里没有就返回 1）。</summary>
	public int MaxLevel(int grandKeyId)
	{
		List<GrandKeyLevelDataExcel> list = base.All.Where((GrandKeyLevelDataExcel x) => x.GrandKeyId == grandKeyId).ToList();
		if (list.Count == 0)
		{
			return 1;
		}
		return (int)list.Max((GrandKeyLevelDataExcel x) => x.Level);
	}
}
