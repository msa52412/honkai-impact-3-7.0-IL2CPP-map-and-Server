using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class MaterialUseDataExcel
{
	[JsonProperty("useType")]
	public int UseType { get; set; }

	[JsonProperty("MultiUse")]
	public int MultiUse { get; set; }

	[JsonProperty("MaterialUseConfirmType")]
	public int MaterialUseConfirmType { get; set; }

	[JsonProperty("paraStr")]
	public string[] ParaStr { get; set; }

	[JsonProperty("useTip")]
	public HashName UseTip { get; set; }

	[JsonProperty("useMin")]
	public int UseMin { get; set; }

	[JsonProperty("useMax")]
	public int UseMax { get; set; }

	[JsonProperty("AdditionDesc")]
	public string AdditionDesc { get; set; }

	[JsonProperty("EventID")]
	public int EventId { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	[JsonProperty("useID")]
	public int UseId { get; set; }
}
