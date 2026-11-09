using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class AvatarStarTypeExcel
{
	[JsonProperty("upgrade")]
	public int Upgrade { get; set; }

	[JsonProperty("IconPath")]
	public string IconPath { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	[JsonProperty("Star")]
	public int Star { get; set; }

	[JsonProperty("SubStar")]
	public int SubStar { get; set; }

	[JsonProperty("avatarType")]
	public int AvatarType { get; set; }

	[JsonProperty("avatarStarUpType")]
	public int AvatarStarUpType { get; set; }
}
