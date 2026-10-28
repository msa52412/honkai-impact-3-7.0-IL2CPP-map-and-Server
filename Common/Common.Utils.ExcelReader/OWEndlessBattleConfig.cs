using System.Collections.Generic;
using System.Linq;

namespace Common.Utils.ExcelReader;

/// <summary>
/// OWEndlessBattleConfig.json 读取器 —— 决定「这一周期有几张图、哪张 BOSS 图、什么天气」。
///
/// 为什么需要它：客户端 OWEndLessBattleLevelInfo.Refresh 用
/// rsp.battle_config_id 反查这张表拿区域/天气/地图，服务端必须下发一个
/// **该模式（EndlessConfigType）下真实存在**的 ID，否则整块地图数据为空。
/// </summary>
public class OWEndlessBattleConfig : BaseExcelReader<OWEndlessBattleConfig, OWEndlessBattleConfigExcel>
{
	public override string FileName => "OWEndlessBattleConfig.json";

	/// <summary>按主键精确取行。</summary>
	public OWEndlessBattleConfigExcel? FromKey(int battleConfigId)
	{
		return All.FirstOrDefault((OWEndlessBattleConfigExcel row) => row.BattleConfigID == battleConfigId);
	}

	/// <summary>指定模式下的全部行。</summary>
	public List<OWEndlessBattleConfigExcel> ByType(int type)
	{
		return All.Where((OWEndlessBattleConfigExcel row) => row.EndlessConfigType == type).ToList();
	}

	/// <summary>
	/// 给某个模式 + 扰动等级挑一套 4 区域 + BOSS 的配置。
	///
	/// 选择规则（按优先级，全部有表可查，不猜）：
	///   ① 区域数 = 4、BOSS 区非空 —— 用户要求「四个小怪地图 + 一个 BOSS 地图」
	///   ② 天气全局生效 —— EnvironmentIDList 所有元素相同
	///   ③ 计分档位 = 1000 + 扰动等级（表里 1001..1007 正好对应扰动 1..7，
	///      证据：1003 的 MonsterScoreConfig 含 2303，1005 含 2503，1006 含 2603，
	///      1007 含 2703，首位数就是档位）
	///   ④ 同等条件取最小 BattleConfigID —— 这几个是每个档位的「基础盘」，
	///      其余 ID 都是它的区域顺序/天气排列组合
	/// </summary>
	public OWEndlessBattleConfigExcel? PickFourAreaGlobalWeather(int type, int hardLevel)
	{
		List<OWEndlessBattleConfigExcel> pool = All
			.Where((OWEndlessBattleConfigExcel row) => row.EndlessConfigType == type)
			.Where((OWEndlessBattleConfigExcel row) => row.AreaIDList != null && row.AreaIDList.Length >= 4)
			.Where((OWEndlessBattleConfigExcel row) => row.BossAreaIDList != null && row.BossAreaIDList.Length >= 1)
			.ToList();
		if (pool.Count == 0)
		{
			return null;
		}

		int wantScore = 1000 + hardLevel;

		// ① 天气全局 + 计分档位精确匹配
		OWEndlessBattleConfigExcel? hit = pool
			.Where((OWEndlessBattleConfigExcel row) => row.AreaScoreConfigID == wantScore)
			.Where(GlobalWeather)
			.OrderBy((OWEndlessBattleConfigExcel row) => row.BattleConfigID)
			.FirstOrDefault();
		if (hit != null)
		{
			return hit;
		}

		// ② 天气全局即可
		hit = pool.Where(GlobalWeather)
			.OrderBy((OWEndlessBattleConfigExcel row) => row.BattleConfigID)
			.FirstOrDefault();
		if (hit != null)
		{
			return hit;
		}

		// ③ 只要 4 区域 + BOSS
		return pool.OrderBy((OWEndlessBattleConfigExcel row) => row.BattleConfigID).FirstOrDefault();
	}

	/// <summary>
	/// 天气是否全局：所有区域同一个 EnvironmentID。
	/// 表里绝大多数行是「1 个区域有天气、其余 101（无天气）」，那是分区域天气；
	/// 用户要的是全局生效，所以只挑全同的行。
	/// </summary>
	private static bool GlobalWeather(OWEndlessBattleConfigExcel row)
	{
		if (row.EnvironmentIDList == null || row.EnvironmentIDList.Length == 0)
		{
			return false;
		}
		int first = row.EnvironmentIDList[0];
		return row.EnvironmentIDList.All((int env) => env == first);
	}

	/// <summary>打日志用的一行摘要。</summary>
	public static string Describe(OWEndlessBattleConfigExcel row)
	{
		string env = row.EnvironmentIDList == null ? "-" : string.Join("/", row.EnvironmentIDList);
		string area = row.AreaIDList == null ? "-" : string.Join("/", row.AreaIDList);
		string boss = row.BossAreaIDList == null ? "-" : string.Join("/", row.BossAreaIDList);
		return $"BattleConfigID={row.BattleConfigID} type={row.EndlessConfigType} map={row.MapID} "
			+ $"areas=[{area}] boss=[{boss}] env=[{env}] score={row.AreaScoreConfigID}";
	}
}
