using System.Collections.Generic;
using System.Linq;

namespace Common.Utils.ExcelReader;

/// <summary>
/// OWEndlessGroup.json 读取器。
///
/// 为什么不复用 User.AbyssGroupLevel：
/// 那是建号默认 8，而且 8 不在这张表里；它同时被超弦空间用，
/// 直接改会波及 UltraEndless。旧深渊的分组单独走 AbyssConfig.OwGroupLevel。
/// </summary>
public class OWEndlessGroup : BaseExcelReader<OWEndlessGroup, OWEndlessGroupExcel>
{
	public override string FileName => "OWEndlessGroup.json";

	/// <summary>按 (GroupLevel, Type) 精确取行 —— 与客户端 GetOWEndlessGroupMetaByKey 同键。</summary>
	public OWEndlessGroupExcel? FromKey(int groupLevel, int type)
	{
		return All.FirstOrDefault((OWEndlessGroupExcel row) => row.GroupLevel == groupLevel && row.Type == type);
	}

	/// <summary>指定 Type 下某个 playerGroup 的整条段位阶梯，按 GroupLevel 升序（低 → 高）。</summary>
	public List<OWEndlessGroupExcel> Ladder(int type, int playerGroup)
	{
		return All.Where((OWEndlessGroupExcel row) => row.Type == type && row.PlayerGroup == playerGroup)
			.OrderBy((OWEndlessGroupExcel row) => row.GroupLevel)
			.ToList();
	}

	/// <summary>指定 Type 的全部档位，按 GroupLevel 升序。</summary>
	public List<OWEndlessGroupExcel> Ladder(int type)
	{
		return All.Where((OWEndlessGroupExcel row) => row.Type == type)
			.OrderBy((OWEndlessGroupExcel row) => row.GroupLevel)
			.ToList();
	}

	/// <summary>
	/// 把任意 GroupLevel 归一到该 Type 下真实存在的档位。
	///
	/// 为什么需要：下发一个表里没有的 GroupLevel，客户端
	/// RefreshPlayerGroup+0x404 会 NRE → 黑屏 + 每帧重发 3419。
	/// 实测界面上的表现是「段位永远显示最低档」—— 客户端查不到行就退到阶梯第一档。
	/// 所以宁可服务端先夹紧，也不要让它拿非法值去查表。
	/// </summary>
	public OWEndlessGroupExcel? Normalize(int groupLevel, int type)
	{
		OWEndlessGroupExcel? exact = FromKey(groupLevel, type);
		if (exact != null)
		{
			return exact;
		}
		List<OWEndlessGroupExcel> ladder = Ladder(type);
		if (ladder.Count == 0)
		{
			return null;
		}
		// 取「不小于目标值的最小档位」，没有就取最高档
		OWEndlessGroupExcel? up = ladder.FirstOrDefault((OWEndlessGroupExcel row) => row.GroupLevel >= groupLevel);
		return up ?? ladder[ladder.Count - 1];
	}
}
