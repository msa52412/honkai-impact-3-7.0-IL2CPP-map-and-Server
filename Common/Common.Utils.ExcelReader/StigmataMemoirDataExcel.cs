using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class StigmataMemoirDataExcel
{
	[JsonProperty("AvatarID")]
	public uint AvatarId { get; set; }
}
