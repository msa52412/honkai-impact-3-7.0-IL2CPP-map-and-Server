using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class LevelUpMaterialList
{
	[JsonProperty("MaterialID")]
	public uint MaterialId { get; set; }

	[JsonProperty("Number")]
	public uint Number { get; set; }
}
