using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class LvUpMaterial
{
	[JsonProperty("MaterialID")]
	public int MaterialId { get; set; }

	[JsonProperty("Num")]
	public int Num { get; set; }
}
