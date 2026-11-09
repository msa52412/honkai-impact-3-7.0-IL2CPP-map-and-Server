using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// OWEndlessItemEffect.json 的一行 —— 按道具 **type** 给出背包上限与效果上限。
///
/// 实测（2026-10-27 全表 6 条）：
///   typeID 1  limitType 2  bagAmount 99  effectAmount 1
///   typeID 2  limitType 2  bagAmount 99  effectAmount 1
///   typeID 3  limitType 1  bagAmount  3  effectAmount 2
///   typeID 4  limitType 2  bagAmount 99  effectAmount 3
///   typeID 5  limitType 2  bagAmount 99  effectAmount 1
///   typeID 6  limitType 2  bagAmount 99  effectAmount 1
///
/// 客户端唯一读取点：LevelDesignManager.CheckItemEffectOverMaxAmount(0x1819C49B0)
///   先按 item.type 查本表（type=3 时改查 item.RelateBuff 的 type=6），
///   再用 effectAmount 和「当前 EffectItemList 里同 type 的条目数」比较。
/// 服务端照抄这个口径即可，否则客户端会先弹「已达上限」把请求拦掉。
/// </summary>
public class OWEndlessItemEffectExcel
{
	/// <summary>对应的道具 type（1..6）。</summary>
	[JsonProperty("typeID")]
	public int TypeID { get; set; }

	/// <summary>1 = 按单个道具计，2 = 按类型计。客户端只拿它做 UI 分组，判定都用 effectAmount。</summary>
	[JsonProperty("limitType")]
	public int LimitType { get; set; }

	/// <summary>背包堆叠上限（type=3 陷阱是 3，其余 99）。</summary>
	[JsonProperty("bagAmount")]
	public int BagAmount { get; set; }

	/// <summary>同类效果最多同时存在几个（烧瓶 1、净化 3、陷阱 2）。</summary>
	[JsonProperty("effectAmount")]
	public int EffectAmount { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }
}
