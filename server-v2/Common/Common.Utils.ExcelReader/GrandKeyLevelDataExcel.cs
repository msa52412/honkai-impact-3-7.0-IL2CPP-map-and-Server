using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// GrandKeyLevel.json 的一行：某把神之键在某一级的解锁/升级条件。
///
/// 文件实证（run\Resources\ExcelOutputAsset\GrandKeyLevel.json，44 行，无 null）：
/// {
///   "grandKeyID": 201,
///   "level": 1,
///   "unlockWeaponLevel": 30,        // 解锁/升到这一级需要对应武器达到的等级
///   "playerLevel": 0,               // 需要的玩家等级（0 = 不限制）
///   "upgradeMaterial": [{"ID":100,"Num":28000}, ...]
/// }
///
/// 注意：这 44 行里 201~207 有 1..10 级，204/206/207/208 只有 1 级
/// （和 GrandKey.json 的 maxGrandKeyLv 一致）。
/// </summary>
public class GrandKeyLevelDataExcel
{
	[JsonProperty("grandKeyID")]
	public long GrandKeyId { get; set; }

	[JsonProperty("level")]
	public long Level { get; set; }

	[JsonProperty("unlockWeaponLevel")]
	public long UnlockWeaponLevel { get; set; }

	[JsonProperty("playerLevel")]
	public long PlayerLevel { get; set; }

	[JsonProperty("upgradeMaterial")]
	public GrandKeyMaterial[] UpgradeMaterial { get; set; }
}

/// <summary>GrandKeyLevel.upgradeMaterial 的一个元素。</summary>
public class GrandKeyMaterial
{
	[JsonProperty("ID")]
	public long Id { get; set; }

	[JsonProperty("Num")]
	public long Num { get; set; }
}
