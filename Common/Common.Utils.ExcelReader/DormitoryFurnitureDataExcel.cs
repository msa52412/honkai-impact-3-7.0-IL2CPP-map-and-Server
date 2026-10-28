using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class DormitoryFurnitureDataExcel
{
	[JsonProperty("FurnitureID")]
	public uint FurnitureId { get; set; }
}
