using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class AvatarMemoirDataExcel
{
	[JsonProperty("AvatarID")]
	public uint AvatarId { get; set; }
}
