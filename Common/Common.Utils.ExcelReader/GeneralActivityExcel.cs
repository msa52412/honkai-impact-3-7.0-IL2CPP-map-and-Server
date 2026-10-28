using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class GeneralActivityExcel
{
	[JsonProperty("Series")]
	public int Series { get; set; }

	[JsonProperty("AcitivityType")]
	public int AcitivityType { get; set; }

	[JsonProperty("MinLevel")]
	public int MinLevel { get; set; }

	[JsonProperty("MaxLevel")]
	public int MaxLevel { get; set; }

	[JsonProperty("LinkData")]
	public int LinkData { get; set; }

	[JsonProperty("DisplayData")]
	public int DisplayData { get; set; }

	[JsonProperty("ScoreData")]
	public int ScoreData { get; set; }

	[JsonProperty("RankData")]
	public int RankData { get; set; }

	[JsonProperty("ShowRank")]
	public int ShowRank { get; set; }

	[JsonProperty("TeamListID")]
	public int TeamListId { get; set; }

	[JsonProperty("PreCondType")]
	public int PreCondType { get; set; }

	[JsonProperty("PreCondParaStr")]
	public string PreCondParaStr { get; set; }

	[JsonProperty("PreUnlockHint")]
	public HashName PreUnlockHint { get; set; }

	[JsonProperty("ActivityBuffID")]
	public int ActivityBuffId { get; set; }

	[JsonProperty("TicketIDList")]
	public int[] TicketIdList { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	[JsonProperty("AcitivityID")]
	public int AcitivityId { get; set; }
}
