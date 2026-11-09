using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class UnlockMaterial
{
	[JsonProperty("Amount")]
	public int Amount { get; set; }

	[JsonProperty("ItemID")]
	public int ItemId { get; set; }
}
