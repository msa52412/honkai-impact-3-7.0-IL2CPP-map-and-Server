using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class EquipmentLevelDataExcel
{
	[JsonProperty("Type1")]
	public int[] Type1 { get; set; }

	[JsonProperty("weaponUpgradeCost")]
	public int WeaponUpgradeCost { get; set; }

	[JsonProperty("weaponEvoCost")]
	public int WeaponEvoCost { get; set; }

	[JsonProperty("stigmataUpgradeCost")]
	public int StigmataUpgradeCost { get; set; }

	[JsonProperty("stigmataEvoCost")]
	public int StigmataEvoCost { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	[JsonProperty("level")]
	public int Level { get; set; }
}
