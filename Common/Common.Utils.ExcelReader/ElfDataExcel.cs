using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class ElfDataExcel
{
	[JsonProperty("Index")]
	public int Index { get; set; }

	[JsonProperty("ElfRegistryKey")]
	public string ElfRegistryKey { get; set; }

	[JsonProperty("UnlockStar")]
	public int UnlockStar { get; set; }

	[JsonProperty("Rarity")]
	public int Rarity { get; set; }

	[JsonProperty("elfCardID")]
	public int ElfCardId { get; set; }

	[JsonProperty("elfFragmentID")]
	public int ElfFragmentId { get; set; }

	[JsonProperty("CarryPlayerLevel")]
	public int CarryPlayerLevel { get; set; }

	[JsonProperty("TrialStageActivityList")]
	public int[] TrialStageActivityList { get; set; }

	[JsonProperty("CaptainSkillIDs")]
	public int[] CaptainSkillIDs { get; set; }

	[JsonProperty("ElfID")]
	public int ElfId { get; set; }
}
