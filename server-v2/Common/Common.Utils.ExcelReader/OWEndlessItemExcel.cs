using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// OWEndlessItem.json 的一行 —— 迪拉克之海 / 量子奇点的「游戏内道具」定义。
///
/// 实测（2026-10-27 全表 28 条）：
///   type 1  INVASION   150010                     干扰器，maxUseRange=minUseRange=3
///   type 2  BUFF       150001..150009             药水（物理/元素伤害、SP 回复，S/M/L 三档）
///   type 3  TRAP       150021..150028             陷阱，RelateBuff 指到 150031..150038
///   type 4  COUNTER    150040                     净化（解 debuff）
///   type 5  CONSUME    150020                     钥匙
///   type 6  TRAP_BUFF  150031..150038             陷阱对应的效果项（不在背包里）
///
/// 客户端对应实现：
///   OWEndlessModule.GetItemType(0x181898720)   按 item_id 查本表 reading `type`
///   OWEndlessModule.GetItemNum (0x1818985C0)   在内存背包里取数量
///   LevelDesignManager.CheckItemEffectOverMaxAmount(0x1819C49B0)
///       用 item.type 去查 OWEndlessItemEffect（type=3 时改查 RelateBuff 的 type=6），
///       拿 effectAmount 当「同类效果最多同时存在几个」。
/// </summary>
public class OWEndlessItemExcel
{
	/// <summary>道具 ID（= 协议 item_id）。</summary>
	[JsonProperty("ID")]
	public int ID { get; set; }

	/// <summary>语义类型，见 OpenworldEndlessItemType：1 入侵 / 2 增益 / 3 陷阱 / 4 净化 / 5 消耗 / 6 陷阱效果。</summary>
	[JsonProperty("type")]
	public int Type { get; set; }

	/// <summary>稀有度，只影响 UI 边框。</summary>
	[JsonProperty("rarity")]
	public int Rarity { get; set; }

	/// <summary>使用距离上限（仅 type=1 干扰器有值，=3）。</summary>
	[JsonProperty("maxUseRange")]
	public int MaxUseRange { get; set; }

	/// <summary>使用距离下限（仅 type=1 干扰器有值，=3）。</summary>
	[JsonProperty("minUseRange")]
	public int MinUseRange { get; set; }

	/// <summary>计分加成，全表为 0。</summary>
	[JsonProperty("paramScore")]
	public int ParamScore { get; set; }

	/// <summary>关联的 buff / 效果道具 ID。type=3 的陷阱指到 150031..150038。</summary>
	[JsonProperty("RelateBuff")]
	public int RelateBuff { get; set; }

	/// <summary>效果参数名（药水用），如 OWEndlessItem_PhysicalDamageUp_Small。</summary>
	[JsonProperty("paramStr")]
	public string ParamStr { get; set; }

	/// <summary>大图标路径。</summary>
	[JsonProperty("iconPath")]
	public string IconPath { get; set; }

	/// <summary>小图标路径。</summary>
	[JsonProperty("smallIconPath")]
	public string SmallIconPath { get; set; }

	/// <summary>提示文案 key（EL_HINT_xxx）。</summary>
	[JsonProperty("showHint")]
	public string ShowHint { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }
}
