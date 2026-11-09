using System.Linq;

namespace Common.Utils.ExcelReader;

/// <summary>
/// SanctuaryPlayerGroup.json 的读取器。
/// 表只覆盖 Lv30~85，私服号经常顶到 88 或者被改到 1 级，
/// 所以两头都要兜底：越界时取最近的一档，而不是返回 null
/// （返回 null 会让 player_level_group_id 落成 0，客户端查分组配置直接查空）。
/// </summary>
public class SanctuaryPlayerGroup : BaseExcelReader<SanctuaryPlayerGroup, SanctuaryPlayerGroupExcel>
{
	public override string FileName => "SanctuaryPlayerGroup.json";

	/// <summary>按玩家等级取分组行；表为空时返回 null（调用方需要自己兜底）。</summary>
	public SanctuaryPlayerGroupExcel? ForPlayerLevel(int playerLevel)
	{
		if (base.All == null || base.All.Length == 0)
		{
			return null;
		}
		SanctuaryPlayerGroupExcel? hit = base.All.FirstOrDefault(
			(SanctuaryPlayerGroupExcel r) => playerLevel >= r.MinPlayerLevel && playerLevel <= r.MaxPlayerLevel);
		if (hit != null)
		{
			return hit;
		}
		// 低于表下界 → 最低档；高于上界 → 最高档。
		return playerLevel > base.All.Max((SanctuaryPlayerGroupExcel r) => r.MaxPlayerLevel)
			? base.All.OrderByDescending((SanctuaryPlayerGroupExcel r) => r.MaxPlayerLevel).First()
			: base.All.OrderBy((SanctuaryPlayerGroupExcel r) => r.MinPlayerLevel).First();
	}

	/// <summary>按 PlayerGroupID 反查分组行（存档里已有分组时用来校验它还在不在表里）。</summary>
	public SanctuaryPlayerGroupExcel? ForGroupId(uint groupId)
	{
		if (base.All == null)
		{
			return null;
		}
		return base.All.FirstOrDefault((SanctuaryPlayerGroupExcel r) => r.PlayerGroupId == (int)groupId);
	}

	/// <summary>本活动 id（三行都是 1001，取第一行的即可）。</summary>
	public uint ActivityId()
	{
		SanctuaryPlayerGroupExcel? first = base.All?.FirstOrDefault();
		return first == null ? 0u : (uint)first.ActivityId;
	}
}
