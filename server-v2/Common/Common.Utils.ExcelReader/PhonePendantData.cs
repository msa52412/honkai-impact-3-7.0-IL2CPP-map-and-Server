using System.Linq;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// 手机挂件（游戏里叫「个性装饰」）配置 —— Resources\ExcelOutputAsset\PhonePendantData.json，48 条。
///
/// 客户端 MonoPhoneStrap.RefreshStatus() 的解锁判定只有一句：
///     _isUnlock = ((HGDFACBMDIJ)_data).PHKHBEHHNOL;   // item + 0xA8
/// 而这个字节只会在收到 GetPhonePendantDataRsp.phone_pendant_list 时被写：
///     PhonePendantItem::Init(endTime)  →  TimeType == 1(永久) ? true : (endTime &gt;= Now)
/// 模块初始化（MonoPhonePendantModule 构造）只用配置表 new 出全部 48 个 item，此时该字节是 false，
/// 所以服务端不回这条协议时，整页挂件全是锁。
/// </summary>
public class PhonePendantDataExcel
{
	/// <summary>挂件 id（350001 起，回给客户端的 phone_pendant_list.id）。</summary>
	[JsonProperty("PendantId")]
	public int PendantId { get; set; }

	[JsonProperty("Rarity")]
	public int Rarity { get; set; }

	/// <summary>1 = 永久（客户端据此直接判定已解锁，不比较 end_time）。</summary>
	[JsonProperty("TimeType")]
	public int TimeType { get; set; }

	[JsonProperty("TimeDuring")]
	public int TimeDuring { get; set; }

	[JsonProperty("PendantIconPath")]
	public string PendantIconPath { get; set; }
}

public class PhonePendantData : BaseExcelReader<PhonePendantData, PhonePendantDataExcel>
{
	public override string FileName => "PhonePendantData.json";

	public PhonePendantDataExcel? FromId(uint pendantId)
	{
		return base.All.FirstOrDefault(x => x.PendantId == (int)pendantId);
	}

	/// <summary>全部挂件 id（按 id 升序，客户端列表顺序与之一致）。</summary>
	public uint[] AllIds()
	{
		return base.All.Where(x => x.PendantId > 0)
			.Select(x => (uint)x.PendantId)
			.Distinct()
			.OrderBy(x => x)
			.ToArray();
	}
}
