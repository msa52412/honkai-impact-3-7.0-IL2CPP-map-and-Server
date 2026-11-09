using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class PlayerLevelDataExcel
{
	[JsonProperty("exp")]
	public int Exp { get; set; }

	[JsonProperty("stamina")]
	public int Stamina { get; set; }

	[JsonProperty("numFriends")]
	public int NumFriends { get; set; }

	[JsonProperty("avatarLevelLimit")]
	public int AvatarLevelLimit { get; set; }

	[JsonProperty("staminaBonus")]
	public int StaminaBonus { get; set; }

	[JsonProperty("sweepStaminaLimit")]
	public int SweepStaminaLimit { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	[JsonProperty("level")]
	public int Level { get; set; }
}
