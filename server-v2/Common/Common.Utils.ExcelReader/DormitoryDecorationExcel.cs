using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class DormitoryDecorationExcel
{
	[JsonProperty("DecorationLevel")]
	public int DecorationLevel { get; set; }

	[JsonProperty("TargetHouse")]
	public int TargetHouse { get; set; }

	[JsonProperty("TargetRoom")]
	public int TargetRoom { get; set; }

	[JsonProperty("DecorationPhase")]
	public int DecorationPhase { get; set; }

	[JsonProperty("NeedComfort")]
	public int NeedComfort { get; set; }

	[JsonProperty("NeedTime")]
	public int NeedTime { get; set; }

	[JsonProperty("MaterialList")]
	public LevelUpMaterialList[] MaterialList { get; set; } = new LevelUpMaterialList[0];
}
