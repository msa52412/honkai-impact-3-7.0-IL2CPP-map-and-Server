using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// OWEndlessBattleConfig.json 的一行 —— 旧深渊「这一周期用哪张图、哪几个区域、什么天气」。
///
/// 2026-10-25 21:4x 为什么必须读这张表
/// ---------------------------------
/// 反汇编 OWEndLessBattleLevelInfo.Refresh (0x15C4E90) 证明客户端整块地图数据
/// 都是从 rsp.battle_config_id 反查这张表得到的：
///
///     _battleConfigID    = rsp.BattleConfigId            // proto +0x50
///     _battleConfig      = NAJIOHCPGNI(battleConfigID, mode)
///                          if (NULL) -> 直接跳到函数末尾，什么都不做
///     _areaList          = _battleConfig.AreaIDList       // +0x28
///     _bossAreaIDList    = _battleConfig.BossAreaIDList   // +0x30
///     _environmentIDList = _battleConfig.EnvironmentIDList// +0x68  ← 天气
///     _mapID             = _battleConfig.MapID            // +0x70
///     _scoreConfigID     = _battleConfig.AreaScoreConfigID// +0x74
///
/// 之前响应里写死 battle_config_id = 1001，而 1001 是
/// EndlessConfigType=3（量子奇点，Map 302，AreaIDList=[3100]，BossAreaIDList=[]），
/// 在迪拉克模式下查不到 → 整块地图数据为空。
///
/// 实测表结构（2026-10-25 扫表）：
///     EndlessConfigType=2（迪拉克）598 行，都是 4 个区域 + 4 个 BOSS 区
///         10001..10004   AreaScore 1001  环境全是 101（无天气）
///         20001..21007   AreaScore 1002
///         30001..31009   AreaScore 1003
///         40001..41050   AreaScore 1004  其中 41011 环境全 125
///         500xx..        AreaScore 1005  其中 52000 环境全 118
///         600xx..        AreaScore 1006  其中 62000 环境全 118
///         700xx..        AreaScore 1007  其中 72000 环境全 118
///     EndlessConfigType=3（量子奇点）299 行，1 个区域、无 BOSS
///     1100 / 2100 / 3100 = 迪拉克单区域教学档（AreaScore 1101..1103）
/// </summary>
public class OWEndlessBattleConfigExcel
{
	/// <summary>天气（OWEndlessEnvironment.EnvironmentID）列表，按区域一一对应。全同 = 全局天气。</summary>
	[JsonProperty("EnvironmentIDList")]
	public int[] EnvironmentIDList { get; set; }

	/// <summary>小怪区域（OWEndlessBattleArea.areaId）列表，顺序就是界面上的区域顺序。</summary>
	[JsonProperty("AreaIDList")]
	public int[] AreaIDList { get; set; }

	/// <summary>BOSS 区域列表，通常与小怪区域一一对应。</summary>
	[JsonProperty("BossAreaIDList")]
	public int[] BossAreaIDList { get; set; }

	/// <summary>场景地图 ID：301 = 迪拉克之海，302 = 量子奇点。</summary>
	[JsonProperty("MapID")]
	public int MapID { get; set; }

	/// <summary>区域计分配置 ID，对应 OWEndlessAreaScoreConfig，档位 = 1000 + 扰动等级。</summary>
	[JsonProperty("AreaScoreConfigID")]
	public int AreaScoreConfigID { get; set; }

	/// <summary>主键 —— 就是 rsp.battle_config_id。</summary>
	[JsonProperty("BattleConfigID")]
	public int BattleConfigID { get; set; }

	/// <summary>深渊类型（EndlessType）：2 迪拉克之海 / 3 量子奇点。</summary>
	[JsonProperty("EndlessConfigType")]
	public int EndlessConfigType { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }
}
