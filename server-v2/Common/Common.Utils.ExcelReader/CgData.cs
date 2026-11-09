using System.Linq;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// 影像（CG）收藏配置 —— Resources\ExcelOutputAsset\CgData.json，共 1718 条。
/// 客户端「收藏 → 影像」按 CgCategory 分栏（0 主线 / 1 活动 / 2 …/ 1001 特殊），
/// 解锁状态由 GetPhotoDataRsp.photo_id_list 决定。
/// </summary>
public class CgDataExcel
{
	/// <summary>CG id（回给客户端的 photo_id_list 元素）。</summary>
	[JsonProperty("CgID")]
	public int CgId { get; set; }

	[JsonProperty("CgCategory")]
	public int CgCategory { get; set; }

	[JsonProperty("CgSubCategory")]
	public int CgSubCategory { get; set; }

	/// <summary>收藏分权重，客户端算收藏评分用。</summary>
	[JsonProperty("WikiCgScore")]
	public int WikiCgScore { get; set; }

	/// <summary>是否默认解锁（片头 CG 之类）。</summary>
	[JsonProperty("initialUnlock")]
	public bool InitialUnlock { get; set; }

	/// <summary>视频资源路径，例如 "start" / "Video/…"。</summary>
	[JsonProperty("CgPath")]
	public string CgPath { get; set; }
}

public class CgData : BaseExcelReader<CgData, CgDataExcel>
{
	public override string FileName => "CgData.json";

	public CgDataExcel? FromId(uint cgId)
	{
		return base.All.FirstOrDefault(x => x.CgId == (int)cgId);
	}

	/// <summary>全部 CG id。</summary>
	public uint[] AllIds()
	{
		return base.All.Where(x => x.CgId > 0).Select(x => (uint)x.CgId).ToArray();
	}

	/// <summary>按 CgCategory 取 CG id。</summary>
	public uint[] IdsOfCategory(int category)
	{
		return base.All.Where(x => x.CgCategory == category && x.CgId > 0)
			.Select(x => (uint)x.CgId).ToArray();
	}
}
