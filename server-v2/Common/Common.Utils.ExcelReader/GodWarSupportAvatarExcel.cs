using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class GodWarSupportAvatarExcel
{
	[JsonProperty("CD")]
	public int Cd { get; set; }

	[JsonProperty("SupportAvatarID")]
	public int SupportAvatarId { get; set; }
}
