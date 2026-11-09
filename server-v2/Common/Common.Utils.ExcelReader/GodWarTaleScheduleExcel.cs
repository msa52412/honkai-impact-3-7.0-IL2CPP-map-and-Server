using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class GodWarTaleScheduleExcel
{
	[JsonProperty("TaleIDList")]
	public int[] TaleIdList { get; set; }

	[JsonProperty("BeginTime")]
	public TimeStampForBaked BeginTime { get; set; }

	[JsonProperty("EndTime")]
	public TimeStampForBaked EndTime { get; set; }

	[JsonProperty("TaleScheduleID")]
	public int TaleScheduleId { get; set; }
}
