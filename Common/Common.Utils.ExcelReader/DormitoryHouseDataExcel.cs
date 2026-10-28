using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class DormitoryHouseDataExcel
{
	[JsonProperty("TotalRoomList")]
	public int[] TotalRoomList { get; set; }

	[JsonProperty("HouseFacilityList")]
	public List<HouseFacility> HouseFacilityList { get; set; }

	[JsonProperty("UnlockLv")]
	public int UnlockLv { get; set; }

	[JsonProperty("UnlockMaterial")]
	public int UnlockMaterial { get; set; }

	[JsonProperty("HouseNameText")]
	public HashName HouseNameText { get; set; }

	[JsonProperty("HouseID")]
	public int HouseId { get; set; }
}
