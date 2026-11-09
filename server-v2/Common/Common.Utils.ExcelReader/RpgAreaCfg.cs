using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// RpgArea.json（149 行）—— RPG 故事的「区域」，每个区域挂一组站点 `SiteList`。
/// </summary>
public class RpgAreaCfg : BaseExcelReader<RpgAreaCfg, RpgAreaCfgExcel>
{
	public override string FileName => "RpgArea.json";

	public RpgAreaCfgExcel FromAreaId(uint areaId)
	{
		if (base.All == null)
		{
			return null;
		}
		foreach (RpgAreaCfgExcel row in base.All)
		{
			if (row.AreaId == areaId)
			{
				return row;
			}
		}
		return null;
	}
}

public class RpgAreaCfgExcel
{
	[JsonProperty("AreaID")]
	public uint AreaId { get; set; }

	[JsonProperty("SiteList")]
	public int[] SiteList { get; set; }

	[JsonProperty("ZoneID")]
	public int ZoneId { get; set; }

	[JsonProperty("AreaType")]
	public int AreaType { get; set; }
}
