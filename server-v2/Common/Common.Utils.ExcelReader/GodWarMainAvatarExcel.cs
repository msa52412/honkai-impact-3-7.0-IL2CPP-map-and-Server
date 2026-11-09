using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class GodWarMainAvatarExcel
{
	[JsonProperty("AvatarAbilityID")]
	public int[] AvatarAbilityId { get; set; }

	[JsonProperty("AvatarMissionList")]
	public int[] AvatarMissionList { get; set; }

	[JsonProperty("AvatarChallengeStageIDList")]
	public int[] AvatarChallengeStageIdList { get; set; }

	[JsonProperty("MainAvatarID")]
	public int MainAvatarId { get; set; }
}
