using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// OWEndlessBattleArea.json 的一行 —— 旧深渊里「某个区域包含哪几场战斗」。
///
/// 实测（2026-10-25 扫表，32 行）：
///     3011  AreaA..AreaH  battleID 1..8
///     3012  AreaA..AreaG  battleID 9..15
///     3013  AreaA..AreaG  battleID 16..22
///     3014  AreaA..AreaF  battleID 23..28
///     3015  AreaBoss      battleID 29
///     3016  AreaBoss      battleID 30
///     3017  AreaBoss      battleID 31
///     3018  AreaBoss      battleID 32
///
/// battleID 全局唯一（跨区域连续编号），客户端
/// GetBattleIDByAreaNameAreaID(areaId, areaName) 就是按 (areaId, battleArea) 查这张表。
/// 服务端下发 GetOpenworldEndlessStageInnerDataRsp.battle_list 时填的 battle_id
/// 必须来自这里，否则客户端 RefreshSceneGearDict / RefreshTrapList 建出来的字典键对不上。
/// </summary>
public class OWEndlessBattleAreaExcel
{
	/// <summary>区域名：AreaA..AreaH 是小怪战，AreaBoss 是 BOSS 战。</summary>
	[JsonProperty("battleArea")]
	public string BattleArea { get; set; }

	/// <summary>所属区域（OWEndlessBattleConfig.AreaIDList / BossAreaIDList 里的值）。</summary>
	[JsonProperty("areaId")]
	public int AreaId { get; set; }

	/// <summary>该区域允许的陷阱上限。</summary>
	[JsonProperty("trapMaxNum")]
	public int TrapMaxNum { get; set; }

	/// <summary>战斗 ID —— 下发到 rsp.battle_list[].battle_id。</summary>
	[JsonProperty("battleID")]
	public int BattleID { get; set; }

	/// <summary>区域名文本（hash 形式，客户端自己解析）。</summary>
	[JsonProperty("battleAreaName")]
	public object BattleAreaName { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }
}
