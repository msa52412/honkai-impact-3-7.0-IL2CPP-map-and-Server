using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class HashName
{
	[JsonProperty("hash")]
	public long Hash { get; set; }
}
