using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class LevelUpMaterialList
{
	[JsonProperty("MaterialID")]
	public uint MaterialId { get; set; }

	/// <summary>装修配置（DormitoryDecoration）用的字段名。</summary>
	[JsonProperty("Number")]
	public uint Number { get; set; }

	/// <summary>家具配置（DormitoryFurnitureData 的 Cost/Unlock/DeleteItemList）用的字段名。</summary>
	[JsonProperty("MaterialNumber")]
	public uint MaterialNumber { get; set; }

	public uint Count => MaterialNumber != 0u ? MaterialNumber : Number;
}
