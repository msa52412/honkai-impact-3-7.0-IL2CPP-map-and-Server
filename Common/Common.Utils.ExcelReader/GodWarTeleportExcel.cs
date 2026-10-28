using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class GodWarTeleportExcel
{
	[JsonProperty("TeleportType")]
	public int TeleportType { get; set; }

	[JsonProperty("TeleportEvent")]
	public int TeleportEvent { get; set; }

	[JsonProperty("TeleportID")]
	public int TeleportID { get; set; }
}
