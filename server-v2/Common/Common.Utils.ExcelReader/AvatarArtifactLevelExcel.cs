using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class AvatarArtifactLevelExcel
{
	[JsonProperty("Level")]
	public int Level { get; set; }

	[JsonProperty("UnlockAvatarStar")]
	public int UnlockAvatarStar { get; set; }

	[JsonProperty("UnlockAvatarLevel")]
	public int UnlockAvatarLevel { get; set; }

	[JsonProperty("UnlockMaterial")]
	public UnlockMaterial[] UnlockMaterial { get; set; }

	[JsonProperty("ID")]
	public int Id { get; set; }
}
