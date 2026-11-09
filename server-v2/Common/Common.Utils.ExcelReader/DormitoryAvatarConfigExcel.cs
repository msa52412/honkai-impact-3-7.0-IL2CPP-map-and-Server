using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class DormitoryAvatarConfigExcel
{
	[JsonProperty("RoleID")]
	public int RoleId { get; set; }

	[JsonProperty("AvatarID")]
	public int AvatarId { get; set; }

	[JsonProperty("Name")]
	public string Name { get; set; }
}
