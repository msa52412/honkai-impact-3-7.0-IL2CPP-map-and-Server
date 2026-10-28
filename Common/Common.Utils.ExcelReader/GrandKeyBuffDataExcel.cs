using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class GrandKeyBuffDataExcel
{
	[JsonProperty("grandKeyID")]
	public long GrandKeyId { get; set; }

	[JsonProperty("unlockgrandKeyLevel")]
	public long UnlockgrandKeyLevel { get; set; }

	[JsonProperty("breachLevel")]
	public long BreachLevel { get; set; }

	[JsonProperty("HP")]
	public long Hp { get; set; }

	[JsonProperty("SP")]
	public long Sp { get; set; }

	[JsonProperty("attack")]
	public long Attack { get; set; }

	[JsonProperty("defence")]
	public long Defence { get; set; }

	[JsonProperty("critical")]
	public long Critical { get; set; }

	[JsonProperty("type")]
	public long Type { get; set; }

	[JsonProperty("IsSynchron")]
	public long IsSynchron { get; set; }

	[JsonProperty("side")]
	public long Side { get; set; }

	[JsonProperty("grandKeySkill")]
	public long GrandKeySkill { get; set; }
}
