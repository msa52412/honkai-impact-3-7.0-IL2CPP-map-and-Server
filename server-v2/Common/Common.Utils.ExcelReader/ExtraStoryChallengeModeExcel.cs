using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class ExtraStoryChallengeModeExcel
{
	[JsonProperty("firstAreaID")]
	public int firstAreaId { get; set; }
}
