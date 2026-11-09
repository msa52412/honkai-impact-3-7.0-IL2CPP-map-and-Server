using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class CustomHeadDataExcel
{
	[JsonProperty("indexID")]
	public int IndexId { get; set; }

	[JsonProperty("Page")]
	public int Page { get; set; }

	[JsonProperty("headTitle")]
	public string HeadTitle { get; set; }

	[JsonProperty("headDescription")]
	public string HeadDescription { get; set; }

	[JsonProperty("iconPath")]
	public string IconPath { get; set; }

	[JsonProperty("imagePath")]
	public string ImagePath { get; set; }

	[JsonProperty("Show")]
	public bool Show { get; set; }

	[JsonProperty("HeadType")]
	public int HeadType { get; set; }

	[JsonProperty("HeadParaInt")]
	public int HeadParaInt { get; set; }

	[JsonProperty("TimeType")]
	public int TimeType { get; set; }

	[JsonProperty("During")]
	public int During { get; set; }

	[JsonProperty("EndTime")]
	public string EndTime { get; set; }

	[JsonProperty("BgColorPath")]
	public string BgColorPath { get; set; }

	[JsonProperty("rarity")]
	public int Rarity { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	[JsonProperty("headID")]
	public int HeadId { get; set; }
}
