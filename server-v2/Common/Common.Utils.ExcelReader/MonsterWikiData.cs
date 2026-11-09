using System.Linq;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// 敌人图鉴配置 —— Resources\ExcelOutputAsset\MonsterWikiData.json，共 119 条。
/// 客户端的「收藏 → 敌人」进度 = 已获得 id 里能对上 MonsterWikiID 的条数 / 总条数。
/// </summary>
public class MonsterWikiDataExcel
{
	[JsonProperty("MonsterWikiID")]
	public int MonsterWikiId { get; set; }

	[JsonProperty("MonsterID")]
	public int MonsterId { get; set; }

	/// <summary>图鉴分页（1xx = 普通敌人 / 2xx = BOSS …）。</summary>
	[JsonProperty("Page")]
	public int Page { get; set; }
}

public class MonsterWikiData : BaseExcelReader<MonsterWikiData, MonsterWikiDataExcel>
{
	public override string FileName => "MonsterWikiData.json";

	/// <summary>全部图鉴 id。</summary>
	public uint[] AllIds()
	{
		return base.All.Where(x => x.MonsterWikiId > 0).Select(x => (uint)x.MonsterWikiId).Distinct().ToArray();
	}
}
