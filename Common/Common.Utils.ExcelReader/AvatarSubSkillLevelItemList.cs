using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

public class AvatarSubSkillLevelItemList
{
	[JsonProperty("itemMetaID")]
	public int ItemMetaId { get; set; }

	[JsonProperty("itemNum")]
	public int ItemNum { get; set; }
}
