using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class GrandKeyDataExcel
{
	[JsonProperty("maxGrandKeyLv")]
	public long MaxGrandKeyLv { get; set; }

	[JsonProperty("positionNum")]
	public long PositionNum { get; set; }

	[JsonProperty("mainWeaponIdList")]
	public long[] MainWeaponIdList { get; set; }

	[JsonProperty("SubWeaponmainIDlist")]
	public long[] SubWeaponmainIDlist { get; set; }

	/// <summary>
	/// 超限图标列表。实测 201 有 1 个、202 有 3 个、204 有 3 个……
	/// 数组长度就是这把神之键的可超限档数（超限等级 0..Length-1）。
	/// </summary>
	[JsonProperty("breachIconList")]
	public string[] BreachIconList { get; set; }

	[JsonProperty("grandKeyID")]
	public long GrandKeyId { get; set; }
}
