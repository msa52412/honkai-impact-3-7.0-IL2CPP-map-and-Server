using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class DormitoryTalkEventExcel
{
	[JsonProperty("plotId")]
	public uint PlotId { get; set; }

	[JsonProperty("startDialogId")]
	public int StartDialogId { get; set; }

	[JsonProperty("finishDialogIdList")]
	public int[] FinishDialogIdList { get; set; } = new int[0];

	[JsonProperty("reward")]
	public int Reward { get; set; }

	[JsonProperty("touchExp")]
	public int TouchExp { get; set; }
}
