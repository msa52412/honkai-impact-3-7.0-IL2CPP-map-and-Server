using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class Material
{
	[JsonProperty("ID")]
	public int Id { get; set; }

	[JsonProperty("Num")]
	public int Num { get; set; }
}
