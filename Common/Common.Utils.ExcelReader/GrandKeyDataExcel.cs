using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class GrandKeyDataExcel
{
	[JsonProperty("maxGrandKeyLv")]
	public long MaxGrandKeyLv { get; set; }

	[JsonProperty("positionNum")]
	public long PositionNum { get; set; }

	[JsonProperty("mainWeaponIdList")]
	public long[] MainWeaponIdList { get; set; }

	[JsonProperty("SubWeaponmainIDlist")]
	public long[] SubWeaponmainIDlist { get; set; }

	[JsonProperty("grandKeyID")]
	public long GrandKeyId { get; set; }
}
