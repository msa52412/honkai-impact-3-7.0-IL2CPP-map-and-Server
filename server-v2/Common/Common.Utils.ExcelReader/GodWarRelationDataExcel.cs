using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class GodWarRelationDataExcel
{
	[JsonProperty("AvatarID")]
	public int AvatarId { get; set; }

	[JsonProperty("AvatarLevelID")]
	public int AvatarLevelId { get; set; }

	[JsonProperty("MaxLevel")]
	public long MaxLevel { get; set; }

	[JsonProperty("DisplayLevelCap")]
	public long DisplayLevelCap { get; set; }

	[JsonProperty("Exp")]
	public long Exp { get; set; }

	[JsonProperty("ExpMaterialID")]
	public long ExpMaterialId { get; set; }

	[JsonProperty("LvUpMaterial")]
	public LvUpMaterial LvUpMaterial { get; set; }

	[JsonProperty("LvUpMissionLinkParam")]
	public int[] LvUpMissionLinkParam { get; set; }

	[JsonProperty("RewardID")]
	public int RewardId { get; set; }

	[JsonProperty("AvatarArtifactType")]
	public int AvatarArtifactType { get; set; }

	[JsonProperty("RoleType")]
	public int RoleType { get; set; }

	[JsonProperty("GodWarID")]
	public int GodWarId { get; set; }

	[JsonProperty("RoleID")]
	public int RoleId { get; set; }

	[JsonProperty("Level")]
	public int Level { get; set; }
}
