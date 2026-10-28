using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class GodWarRoleStoryDataExcel
{
	[JsonProperty("RoleID")]
	public int RoleId { get; set; }

	[JsonProperty("NeedRelationLevel")]
	public int NeedRelationLevel { get; set; }

	[JsonProperty("StoryType")]
	public int StoryType { get; set; }

	[JsonProperty("RewardID")]
	public int RewardId { get; set; }

	[JsonProperty("StoryID")]
	public int StoryId { get; set; }
}
