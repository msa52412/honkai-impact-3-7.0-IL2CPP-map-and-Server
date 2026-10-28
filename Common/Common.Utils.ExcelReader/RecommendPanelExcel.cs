using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class RecommendPanelExcel
{
	[JsonProperty("SortID")]
	public int SortId { get; set; }

	[JsonProperty("HangMissionList")]
	public int[] HangMissionList { get; set; }

	[JsonProperty("LinkType")]
	public int LinkType { get; set; }

	[JsonProperty("DisplayRewardItemList")]
	public int[] DisplayRewardItemList { get; set; }

	[JsonProperty("IsRetainGoBtn")]
	public bool IsRetainGoBtn { get; set; }

	[JsonProperty("ShowEndCountdown")]
	public bool ShowEndCountdown { get; set; }

	[JsonProperty("PanelID")]
	public int PanelId { get; set; }
}
