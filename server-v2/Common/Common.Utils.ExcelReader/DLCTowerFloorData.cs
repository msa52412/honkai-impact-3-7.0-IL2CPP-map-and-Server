using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// DLCTowerFloor.json（364 行）—— 爬塔每层的配置，按 ConfigType 分组
/// （1: 30 层；1001/1002/1003/1005/1006: 各 30 层；1101~1205: 各 23 层）。
///
/// 客户端 DLCTowerFloorMetaData.Get(floor, schedule.ConfigType) 决定本层的
/// 推荐等级、限时、怪物波次、CheckPoint。查不到就当作没有这一层，
/// 所以 floor 和 ConfigType 必须和赛季表对齐（当前赛季 255 → ConfigType 1201、23 层）。
/// </summary>
public class DLCTowerFloorData : BaseExcelReader<DLCTowerFloorData, DLCTowerFloorDataExcel>
{
	public override string FileName => "DLCTowerFloor.json";

	/// <summary>取某配置组下的某层；找不到时回落到 ConfigType = 1 的同层。</summary>
	public DLCTowerFloorDataExcel Get(int floor, int configType)
	{
		DLCTowerFloorDataExcel[] all = All;
		if (all == null)
		{
			return null;
		}

		DLCTowerFloorDataExcel fallback = null;
		foreach (DLCTowerFloorDataExcel row in all)
		{
			if (row.Floor != floor)
			{
				continue;
			}
			if (row.ConfigType == configType)
			{
				return row;
			}
			if (row.ConfigType == 1)
			{
				fallback = row;
			}
		}
		return fallback;
	}

	/// <summary>该配置组下的层数（含首尾）。</summary>
	public int CountOf(int configType)
	{
		DLCTowerFloorDataExcel[] all = All;
		if (all == null)
		{
			return 0;
		}
		int n = 0;
		foreach (DLCTowerFloorDataExcel row in all)
		{
			if (row.ConfigType == configType)
			{
				n++;
			}
		}
		return n;
	}
}

public class DLCTowerFloorDataExcel
{
	[JsonProperty("Floor")]
	public int Floor { get; set; }

	[JsonProperty("ConfigType")]
	public int ConfigType { get; set; }

	[JsonProperty("RecommendLevel")]
	public int RecommendLevel { get; set; }

	[JsonProperty("WarningLevel")]
	public int WarningLevel { get; set; }

	[JsonProperty("HardLevel")]
	public int HardLevel { get; set; }

	/// <summary>0 = 不限时，1 = 限时（Parameter[0] 是秒数）。</summary>
	[JsonProperty("SpawnType")]
	public int SpawnType { get; set; }

	[JsonProperty("Parameter")]
	public int[] Parameter { get; set; }

	/// <summary>通关剩余时间换算成的奖励时间。</summary>
	[JsonProperty("TimeBonus")]
	public int TimeBonus { get; set; }

	/// <summary>1 = 该层是存档点（死了从这里重来）。</summary>
	[JsonProperty("IsCheckPoint")]
	public int IsCheckPoint { get; set; }

	[JsonProperty("Reward")]
	public int Reward { get; set; }

	[JsonProperty("MaxCoin")]
	public int MaxCoin { get; set; }

	[JsonProperty("WaveListPool")]
	public string[] WaveListPool { get; set; }
}
