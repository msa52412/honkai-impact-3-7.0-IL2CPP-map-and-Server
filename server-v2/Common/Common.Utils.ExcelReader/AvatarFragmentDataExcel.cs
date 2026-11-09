using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// AvatarFragmentData.json 的一行 —— 角色碎片（女武神碎片）。
///
/// 为什么需要它
/// ------------
/// 抽卡池里的 <c>kind=avatarFragment</c> 掉落是这批 id（10101、10803 …），
/// 它们**不在** MaterialData 里，存档统一塞 MaterialList 也就没人校验；
/// 但补给结算要判断「这一抽是不是稀有掉落」（<c>GachaItem.IsRareDrop</c>，
/// 客户端据此放彩光/特写），就得知道碎片的品级。
///
/// 品级口径：<see cref="GachaDisplayRarity"/> —— 这是表里专门给补给展示用的字段，
/// 实测 S 级碎片 = 15、A 级 = 4、B 级 = 3。
/// 注意不要用 <see cref="Rarity"/> / <see cref="MaxRarity"/>，那两列几乎全是 4，区分不出品级。
/// </summary>
public class AvatarFragmentDataExcel
{
	[JsonProperty("ID")]
	public int Id { get; set; }

	/// <summary>碎片自身星级（基本都是 4，别拿来判品级）。</summary>
	[JsonProperty("rarity")]
	public int Rarity { get; set; }

	[JsonProperty("maxRarity")]
	public int MaxRarity { get; set; }

	/// <summary>补给展示品级：15 = S 级角色碎片，4 = A 级，3 = B 级。</summary>
	[JsonProperty("GachaDisplayRarity")]
	public int GachaDisplayRarity { get; set; }

	[JsonProperty("displayTitle")]
	public HashName DisplayTitle { get; set; }

	[JsonProperty("displayDescription")]
	public HashName DisplayDescription { get; set; }

	[JsonProperty("iconPath")]
	public string IconPath { get; set; }

	[JsonProperty("imagePath")]
	public string ImagePath { get; set; }

	/// <summary>溢出的碎片换回什么材料（通常是 3129）。</summary>
	[JsonProperty("returnMaterialID")]
	public int ReturnMaterialId { get; set; }

	[JsonProperty("returnNum")]
	public int ReturnNum { get; set; }

	[JsonProperty("quantityLimit")]
	public int QuantityLimit { get; set; }

	[JsonProperty("BaseType")]
	public int BaseType { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }
}
