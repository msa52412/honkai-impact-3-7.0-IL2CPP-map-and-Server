using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class GodWarSiteExcel
{
	[JsonProperty("SiteIndex")]
	public int SiteIndex { get; set; }

	[JsonProperty("SiteMaxIndex")]
	public int SiteMaxIndex { get; set; }

	[JsonProperty("SiteEvent")]
	public int SiteEvent { get; set; }

	[JsonProperty("BuffEvent")]
	public int BuffEvent { get; set; }

	[JsonProperty("SiteID")]
	public int SiteId { get; set; }
}
