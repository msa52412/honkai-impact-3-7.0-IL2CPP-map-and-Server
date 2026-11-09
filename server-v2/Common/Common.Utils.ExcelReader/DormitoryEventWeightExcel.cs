using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class DormitoryEventWeightExcel
{
	[JsonProperty("ID")]
	public uint Id { get; set; }

	[JsonProperty("RoleID")]
	public int RoleId { get; set; }

	[JsonProperty("AvatarID")]
	public int AvatarId { get; set; }

	[JsonProperty("EventID")]
	public uint EventId { get; set; }

	[JsonProperty("Weight")]
	public int Weight { get; set; }
}
