using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class DormitoryInteractEventExcel
{
	[JsonProperty("EventID")]
	public uint EventId { get; set; }

	[JsonProperty("InteractEvent")]
	public int InteractEvent { get; set; }

	[JsonProperty("TouchExp")]
	public int TouchExp { get; set; }
}
