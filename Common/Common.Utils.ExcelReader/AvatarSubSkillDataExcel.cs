using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class AvatarSubSkillDataExcel
{
	[JsonProperty("name")]
	public HashName Name { get; set; }

	[JsonProperty("info")]
	public HashName Info { get; set; }

	[JsonProperty("brief")]
	public HashName Brief { get; set; }

	[JsonProperty("showOrder")]
	public int ShowOrder { get; set; }

	[JsonProperty("skillId")]
	public int SkillId { get; set; }

	[JsonProperty("ignoreLeader")]
	public bool IgnoreLeader { get; set; }

	[JsonProperty("iconPath")]
	public string IconPath { get; set; }

	[JsonProperty("unlockStar")]
	public int UnlockStar { get; set; }

	[JsonProperty("unlockSubStar")]
	public int UnlockSubStar { get; set; }

	[JsonProperty("unlockLv")]
	public int UnlockLv { get; set; }

	[JsonProperty("unlockLvAdd")]
	public int UnlockLvAdd { get; set; }

	[JsonProperty("maxLv")]
	public int MaxLv { get; set; }

	[JsonProperty("upLevelSubStarNeedList")]
	public UpLevelSubStarNeedList[] UpLevelSubStarNeedList { get; set; }

	[JsonProperty("scoinCalc")]
	public bool ScoinCalc { get; set; }

	[JsonProperty("unlockScoin")]
	public int UnlockScoin { get; set; }

	[JsonProperty("scoinLvAdd")]
	public int ScoinLvAdd { get; set; }

	[JsonProperty("itemType")]
	public int ItemType { get; set; }

	[JsonProperty("skillToggle")]
	public bool SkillToggle { get; set; }

	[JsonProperty("paramBase_1")]
	public double ParamBase1 { get; set; }

	[JsonProperty("paramAdd_1")]
	public double ParamAdd1 { get; set; }

	[JsonProperty("paramBase_2")]
	public double ParamBase2 { get; set; }

	[JsonProperty("paramAdd_2")]
	public double ParamAdd2 { get; set; }

	[JsonProperty("paramBase_3")]
	public double ParamBase3 { get; set; }

	[JsonProperty("paramAdd_3")]
	public double ParamAdd3 { get; set; }

	[JsonProperty("canTry")]
	public bool CanTry { get; set; }

	[JsonProperty("ArtifactSkillID")]
	public int ArtifactSkillId { get; set; }

	[JsonProperty("UpLevelArtifactNeedList")]
	public UpLevelArtifactNeedList[] UpLevelArtifactNeedList { get; set; }

	[JsonProperty("TagList")]
	public TagList[] TagList { get; set; }

	[JsonProperty("DataImpl")]
	public object DataImpl { get; set; }

	[JsonProperty("avatarSubSkillId")]
	public int AvatarSubSkillId { get; set; }
}
