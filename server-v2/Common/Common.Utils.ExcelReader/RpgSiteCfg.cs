using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// RpgSite.json（1664 行）—— RPG 故事的「站点」（角色挑战里每张章节卡片下的关卡节点）。
///
/// `SiteProgressConfig` 是进度配置 id 列表，对应
/// `RpgSiteProgressConfigV2.SiteProgressID`；客户端用
/// `RpgTaleSite.cur_progress.id` 去查它，**填 0 会被当成无效状态**。
/// </summary>
public class RpgSiteCfg : BaseExcelReader<RpgSiteCfg, RpgSiteCfgExcel>
{
	public override string FileName => "RpgSite.json";

	public RpgSiteCfgExcel FromSiteId(uint siteId)
	{
		if (base.All == null)
		{
			return null;
		}
		foreach (RpgSiteCfgExcel row in base.All)
		{
			if (row.SiteId == siteId)
			{
				return row;
			}
		}
		return null;
	}
}

public class RpgSiteCfgExcel
{
	[JsonProperty("SiteID")]
	public uint SiteId { get; set; }

	[JsonProperty("SiteType")]
	public int SiteType { get; set; }

	[JsonProperty("StageID")]
	public int StageId { get; set; }

	[JsonProperty("SiteProgressConfig")]
	public int[] SiteProgressConfig { get; set; }

	[JsonProperty("LimitID")]
	public int LimitId { get; set; }
}
