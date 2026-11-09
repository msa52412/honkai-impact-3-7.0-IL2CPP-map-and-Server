using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// RpgTale.json（47 行）—— 「RPG 故事」定义（角色挑战 / 编年史这类页面走的是这套）。
///
/// `AreaList` 是这张 tale 包含的区域 id，区域再到 RpgArea.SiteList 取站点。
/// </summary>
public class RpgTaleCfg : BaseExcelReader<RpgTaleCfg, RpgTaleCfgExcel>
{
	public override string FileName => "RpgTale.json";

	public RpgTaleCfgExcel FromTaleId(uint taleId)
	{
		if (base.All == null)
		{
			return null;
		}
		foreach (RpgTaleCfgExcel row in base.All)
		{
			if (row.TaleId == taleId)
			{
				return row;
			}
		}
		return null;
	}
}

public class RpgTaleCfgExcel
{
	[JsonProperty("TaleID")]
	public uint TaleId { get; set; }

	[JsonProperty("AreaList")]
	public int[] AreaList { get; set; }

	[JsonProperty("StageList")]
	public int[] StageList { get; set; }

	[JsonProperty("TicketIDList")]
	public int[] TicketIdList { get; set; }

	[JsonProperty("MinLevel")]
	public int MinLevel { get; set; }

	[JsonProperty("MaxLevel")]
	public int MaxLevel { get; set; }
}
