using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class DormitoryRoomDataExcel
{
	[JsonProperty("RoomType")]
	public int RoomType { get; set; }

	[JsonProperty("RoomSquare")]
	public int RoomSquare { get; set; }

	[JsonProperty("AvatarLimit")]
	public int AvatarLimit { get; set; }

	[JsonProperty("RoomNameText")]
	public HashName RoomNameText { get; set; }

	[JsonProperty("InitialLimitListt")]
	public int[] InitialLimitList { get; set; }

	[JsonProperty("RoomID")]
	public int RoomId { get; set; }
}
