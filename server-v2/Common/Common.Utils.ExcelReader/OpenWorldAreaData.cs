using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// OpenWorldArea.json（93 行）—— 开放世界「区域」配置。
///
/// 关键字段 <c>switchPoint</c>：区域之间的切换点，格式 "目标areaId:出生点名"，
/// 多个用逗号分隔，例如 area 6010 的
///   "6011:MapTransition_Front,6012:MapTransition_Front"
/// 意思是「从 6010 可以走到 6011 / 6012，入口名叫 MapTransition_Front」。
///
/// 客户端地图页的可传送点来自 <c>OpenWorldScheme.SpawnPoint</c>（"出生点名:areaId" 列表），
/// 只靠客户端上报的话，没去过的区域永远进不去 —— 见 OwSpawnPoints。
/// </summary>
public class OpenWorldAreaData : BaseExcelReader<OpenWorldAreaData, OpenWorldAreaDataExcel>
{
	public override string FileName => "OpenWorldArea.json";

	public List<OpenWorldAreaDataExcel> OfMap(uint mapId)
	{
		return base.All.Where((OpenWorldAreaDataExcel x) => x.MapId == mapId).ToList();
	}
}

public class OpenWorldAreaDataExcel
{
	[JsonProperty("mapId")]
	public uint MapId { get; set; }

	[JsonProperty("areaId")]
	public uint AreaId { get; set; }

	/// <summary>切换点，格式 "目标areaId:出生点名"，逗号分隔。</summary>
	[JsonProperty("switchPoint")]
	public string SwitchPoint { get; set; }

	/// <summary>解锁所需剧情 id，0 = 无门槛。</summary>
	[JsonProperty("unlockStoryId")]
	public uint UnlockStoryId { get; set; }

	[JsonProperty("isMarkOnly")]
	public bool IsMarkOnly { get; set; }

	[JsonProperty("levelID")]
	public uint LevelId { get; set; }
}
