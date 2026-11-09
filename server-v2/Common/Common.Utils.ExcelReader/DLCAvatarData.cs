using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>DLCAvatarData.json（3 行）—— 后崩坏书可操作角色。</summary>
public class DLCAvatarData : BaseExcelReader<DLCAvatarData, DLCAvatarDataExcel>
{
	public override string FileName => "DLCAvatarData.json";
}

public class DLCAvatarDataExcel
{
	[JsonProperty("DLCAvatarID")]
	public int DLCAvatarID { get; set; }

	[JsonProperty("index")]
	public int Index { get; set; }

	/// <summary>解锁所需的 DLC 剧情 id，0 = 无条件。</summary>
	[JsonProperty("UnlockHintStoryID")]
	public int UnlockHintStoryID { get; set; }
}
