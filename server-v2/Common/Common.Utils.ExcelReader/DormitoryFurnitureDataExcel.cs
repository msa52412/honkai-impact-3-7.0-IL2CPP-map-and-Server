using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class DormitoryFurnitureDataExcel
{
	[JsonProperty("FurnitureID")]
	public uint FurnitureId { get; set; }

	[JsonProperty("Rarity")]
	public int Rarity { get; set; }

	[JsonProperty("InitialUnLock")]
	public int InitialUnLock { get; set; }

	[JsonProperty("ComfortType")]
	public int ComfortType { get; set; }

	[JsonProperty("ComfortValue")]
	public int ComfortValue { get; set; }

	[JsonProperty("HcoinCost")]
	public int HcoinCost { get; set; }

	[JsonProperty("HcoinUnlock")]
	public int HcoinUnlock { get; set; }

	[JsonProperty("KeepLimit")]
	public int KeepLimit { get; set; }

	[JsonProperty("CostItemList")]
	public LevelUpMaterialList[] CostItemList { get; set; } = new LevelUpMaterialList[0];

	[JsonProperty("UnlockItemList")]
	public LevelUpMaterialList[] UnlockItemList { get; set; } = new LevelUpMaterialList[0];

	[JsonProperty("DeleteMaterialList")]
	public LevelUpMaterialList[] DeleteMaterialList { get; set; } = new LevelUpMaterialList[0];
}
