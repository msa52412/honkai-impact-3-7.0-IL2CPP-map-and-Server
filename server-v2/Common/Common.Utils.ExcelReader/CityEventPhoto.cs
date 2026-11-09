using System.Linq;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// 活动相册（照片）配置 —— Resources\ExcelOutputAsset\CityEventPhoto.json，共 2640 条。
///
/// 客户端 GetPhotoDataReq.type 用的就是这里的 photoType（PhotoType 枚举：
/// 1 城市活动 / 3 RPG剧情 / 4 农场 / 5 王国战争 / 6 DLC / 9 章节隐藏 / 13 开放世界章节 / 15 开放世界狩猎 …）。
/// 回包里的 photo_id_list 放 PhotoID，客户端据此把相册格子点亮。
/// </summary>
public class CityEventPhotoExcel
{
	/// <summary>照片 id（回给客户端的 photo_id_list 元素）。</summary>
	[JsonProperty("PhotoID")]
	public int PhotoId { get; set; }

	/// <summary>所属相册分类，对应 PhotoType 枚举。</summary>
	[JsonProperty("photoType")]
	public int PhotoType { get; set; }

	/// <summary>是否默认解锁。</summary>
	[JsonProperty("isDefault")]
	public bool IsDefault { get; set; }

	/// <summary>关联活动 id，0 = 不挂活动。</summary>
	[JsonProperty("activityID")]
	public int ActivityId { get; set; }
}

public class CityEventPhoto : BaseExcelReader<CityEventPhoto, CityEventPhotoExcel>
{
	public override string FileName => "CityEventPhoto.json";

	/// <summary>全部照片 id（type=0 即 PHOTO_TYPE_ALL 时用）。</summary>
	public uint[] AllIds()
	{
		return base.All.Where(x => x.PhotoId > 0).Select(x => (uint)x.PhotoId).Distinct().ToArray();
	}

	/// <summary>某个 PhotoType 下的全部照片 id。</summary>
	public uint[] IdsOfType(uint photoType)
	{
		return base.All.Where(x => x.PhotoType == (int)photoType && x.PhotoId > 0)
			.Select(x => (uint)x.PhotoId).Distinct().ToArray();
	}
}
