using System.Linq;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// 勋章配置 —— Resources\ExcelOutputAsset\MedalData.json，共 130 枚。
/// 「收藏 → 勋章」页吃 GetMedalDataRsp.medal_list，之前这个请求没有 handler，所以一栏全黑。
/// </summary>
public class MedalDataExcel
{
	/// <summary>勋章 id（Medal.id）。</summary>
	[JsonProperty("medalID")]
	public int MedalId { get; set; }

	[JsonProperty("rarity")]
	public int Rarity { get; set; }

	[JsonProperty("indexID")]
	public int IndexId { get; set; }

	/// <summary>限时勋章的过期时间，空串/0 = 永久。</summary>
	[JsonProperty("EndTime")]
	public string EndTime { get; set; }

	/// <summary>
	/// 时限类型：1 = 永久，2 / 3 = 限时。
	/// 本表实测只有 1（105 枚）和 2（25 枚），没有 3 —— 判定里仍保留 3 以兼容其他版本。
	/// </summary>
	[JsonProperty("timeType")]
	public int TimeType { get; set; }

	/// <summary>限时天数（原表字段）。本地服不用它，统一按 <see cref="MedalService.LimitedDays"/> 发。</summary>
	[JsonProperty("during")]
	public int During { get; set; }

	/// <summary>是否限时勋章（timeType 2 或 3）。</summary>
	public bool IsLimited => TimeType == 2 || TimeType == 3;

	/// <summary>是否在收藏页展示。</summary>
	[JsonProperty("show")]
	public bool Show { get; set; }

	[JsonProperty("IsCollaboration")]
	public bool IsCollaboration { get; set; }
}

public class MedalData : BaseExcelReader<MedalData, MedalDataExcel>
{
	public override string FileName => "MedalData.json";

	public MedalDataExcel? FromId(uint medalId)
	{
		return base.All.FirstOrDefault(x => x.MedalId == (int)medalId);
	}

	/// <summary>全部勋章 id（收藏 → 勋章的进度用）。</summary>
	public uint[] AllIds()
	{
		return base.All.Where(x => x.MedalId > 0).Select(x => (uint)x.MedalId).Distinct().ToArray();
	}
}
