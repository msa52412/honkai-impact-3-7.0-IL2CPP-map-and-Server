using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class UltraEndlessSiteExcel
{
	[JsonProperty("StageID")]
	public int StageId { get; set; }

	[JsonProperty("BuffID")]
	public int BuffId { get; set; }

	[JsonProperty("SiteNodeName")]
	public string SiteNodeName { get; set; }

	[JsonProperty("PreSiteList")]
	public int[] PreSiteList { get; set; }

	[JsonProperty("SiteName")]
	public string SiteName { get; set; }

	[JsonProperty("LevelEndWebLink")]
	public string LevelEndWebLink { get; set; }

	[JsonProperty("HardLevelGroup")]
	public int HardLevelGroup { get; set; }

	[JsonProperty("WebLink")]
	public string WebLink { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	[JsonProperty("SiteID")]
	public int SiteId { get; set; }
}
