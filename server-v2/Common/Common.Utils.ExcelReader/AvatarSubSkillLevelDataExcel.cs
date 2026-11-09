using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class AvatarSubSkillLevelDataExcel
{
	[JsonProperty("ItemList_1")]
	public AvatarSubSkillLevelItemList[] ItemList1 { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	[JsonProperty("ItemType")]
	public int ItemType { get; set; }

	[JsonProperty("SkillLevel")]
	public int SkillLevel { get; set; }
}
