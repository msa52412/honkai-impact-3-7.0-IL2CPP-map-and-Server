using System;
using System.Collections.Generic;
using System.Linq;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 超弦空间「本期排行榜」的共享算法：分数口径、假玩家阶梯、排序规则。
///
/// 为什么单独抽出来
/// ----------------
/// 超弦空间的分数在两处协议里出现，客户端也是两套 UI 读：
///
///   ① 5203 GetMainDataRsp.endless_player_list  → 主界面排名列表
///      MonoUltraEndlessRankPanelCmpt.SetupView → GetSortedPlayerList() → ComparePlayer
///      每行的分数文本来自 MonoUltraEndlessRankRowCmpt.SetupView：
///          0x1812E1D50  mov eax, [rbp+0x1c]   ; UltraEndlessPlayer.max_stage_score
///          0x1812E1D71  call Text.set_text     ; → _scoreText(+0xA8)
///      排序键 ComparePlayer @ 0x183861E40：
///          0x183861E7C  mov edx, [rbx+0x1c]    ; r.max_stage_score
///          0x183861E81  mov ecx, [rdi+0x1c]    ; l.max_stage_score
///          0x183861E84  sub eax, ecx           ; 降序
///          0x183861F74  ... [rdi+0x20] vs [rbx+0x20]  ; 同分比 score_update_time 升序
///          0x183861F84  ... [rdi+0x10] - [rbx+0x10]   ; 再同分比 uid 升序
///
///   ② 5201 GetTopRankRsp.rank_data.rank_list   → 排行榜页
///      UltraEndlessRankPageContext.RefreshView @ 0x182B154D0
///          call UltraEndlessModule.GetTopRank(scheduleID)   ; 0x183865740
///      而 GetTopRank 是查模块里 +0x88 那个 Dictionary&lt;uint, RankShowData&gt;，
///      写入点是 UltraEndlessModule.OnGetTopRankRsp @ 0x183866F60：
///          0x183866F97  cmp dword [rax+0x10], 0     ; rsp.retcode != 0 → 直接 return
///          0x183866FB4  mov edx, [rax+0x14]         ; ★ key = rsp.schedule_id
///          0x183866FB0  mov r8,  [rax+0x18]         ; ★ value = rsp.rank_data
///          0x183866FB7  call Dictionary.set_Item
///      → **回包的 schedule_id 必须和 5203 下发的 ScheduleId 一模一样**，
///        否则 RefreshView 查字典查不到，页面永远是空的。
///
/// 分数口径
/// --------
/// 客户端的「我的分数」是 GetAllSiteScore() @ 0x1838623F0
///   = Σ 所有 site 的 GetSiteScore(siteID)
///   = Σ 所有 site Σ 该 site 各层 floor_list[].max_score
/// 所以 max_stage_score 要填**本期总分**，不是「单层最高分」。
/// 原来填 MaxStageScore（单层最大值）→ 打完 10 层分数一直停在 2000 不动。
/// </summary>
internal static class UltraEndlessRank
{
	/// <summary>假玩家条数（不含自己）</summary>
	public const int FakePlayerCount = 10;

	/// <summary>假玩家的 uid 段，避开真实玩家和迪拉克那边的 900000 段</summary>
	public const uint FakeUidBase = 910000u;

	/// <summary>红莲段杯数区间 1500..1999，按名次铺开</summary>
	public const uint BaseCupNum = 1500u;
	public const uint CupStep = 45u;

	/// <summary>
	/// 默认场景组（SiteID / 10）。run\ultra_endless.json 的 site_groups 没配时用这个。
	/// 110 组 = 1101/1102/1103/1104，表里连续 4 行，首匹配无歧义。
	/// </summary>
	public static readonly int[] DefaultSiteGroups = { 110 };

	/// <summary>实际生效的组号：优先读 run\ultra_endless.json。</summary>
	public static int[] ActiveSiteGroups()
	{
		return UltraTuning.SiteGroups ?? DefaultSiteGroups;
	}

	/// <summary>排行榜里的一行（假玩家和自己共用）</summary>
	public readonly struct Row
	{
		public readonly uint Uid;
		public readonly uint Score;
		public readonly bool IsSelf;

		public Row(uint uid, uint score, bool isSelf)
		{
			Uid = uid;
			Score = score;
			IsSelf = isSelf;
		}
	}

	/// <summary>
	/// 本期「全清最高分」= Σ 本期所有站点 Σ 各层表里的 MaxScore。
	/// 客户端算同一个值：UltraEndlessModule.GetSiteMaxScore(siteID)。
	/// 用来把假玩家的阶梯分数铺在真实可达到的区间里，
	/// 免得出现「打完还是垫底」或者「随便打打就第一」。
	/// </summary>
	public static uint MaxPossibleScore()
	{
		int[] groups = ActiveSiteGroups();
		UltraEndlessFloor floorReader = BaseExcelReader<UltraEndlessFloor, UltraEndlessFloorExcel>.GetInstance();
		UltraEndlessSiteExcel[] allSites = BaseExcelReader<UltraEndlessSite, UltraEndlessSiteExcel>.GetInstance().All;

		long total = 0L;
		foreach (UltraEndlessSiteExcel site in allSites)
		{
			if (!groups.Contains(site.SiteId / 10))
			{
				continue;
			}
			foreach (UltraEndlessFloorExcel floor in floorReader.GetFloorDatasFromStageId(site.StageId))
			{
				total += floor.MaxScore;
			}
		}
		return total > 0L ? (uint)total : 0u;
	}

	/// <summary>
	/// 玩家本期总分 = Σ 本期所有站点 Σ 各层最高分。
	/// 和 5203 下发的 site_list[].floor_list[].max_score 之和完全一致
	/// （也等于客户端 GetAllSiteScore）。
	/// </summary>
	public static uint TotalScore(UltraEndlessState state)
	{
		int[] groups = ActiveSiteGroups();
		UltraEndlessFloor floorReader = BaseExcelReader<UltraEndlessFloor, UltraEndlessFloorExcel>.GetInstance();
		UltraEndlessSiteExcel[] allSites = BaseExcelReader<UltraEndlessSite, UltraEndlessSiteExcel>.GetInstance().All;

		long total = 0L;
		foreach (UltraEndlessSiteExcel site in allSites)
		{
			if (!groups.Contains(site.SiteId / 10))
			{
				continue;
			}
			uint siteId = (uint)site.SiteId;
			foreach (UltraEndlessFloorExcel floor in floorReader.GetFloorDatasFromStageId(site.StageId))
			{
				total += state.FloorScore(siteId, (uint)floor.FloorId);
			}
		}
		return total > 0L ? (uint)total : 0u;
	}

	/// <summary>
	/// 阶梯第一名。故意取「全清最高分 - 1」：
	/// 玩家真把每一层都打满时能独占第一，不会和假玩家同分
	/// （同分时客户端是按 score_update_time、uid 再比的，名次会变得莫名其妙）。
	/// </summary>
	private static uint LadderTop(uint maxPossible)
	{
		return maxPossible > 1u ? maxPossible - 1u : 24000u;
	}

	/// <summary>
	/// 每名递减多少分。取「全清最高分 / 12」，
	/// 这样 10 个假玩家正好铺满「满分的 92% ~ 17%」这一段，
	/// 玩家每打完一个站点（约 4000~6000 分）就能往上挪 2~3 名。
	/// 下限 1000，避免站点少的时候阶梯挤在一起。
	/// </summary>
	private static uint LadderStep(uint maxPossible)
	{
		uint basis = maxPossible > 0u ? maxPossible : 24000u;
		return Math.Max(1000u, basis / 12u);
	}

	/// <summary>
	/// 生成「假玩家 + 自己」并按客户端 ComparePlayer 的规则排好序。
	/// 返回顺序即名次顺序（下标 + 1 = 名次）。
	/// </summary>
	/// <param name="selfUid">自己的 uid</param>
	/// <param name="selfScore">自己的本期总分（<see cref="TotalScore"/>）</param>
	public static List<Row> BuildRows(uint selfUid, uint selfScore)
	{
		uint maxPossible = MaxPossibleScore();
		uint top = LadderTop(maxPossible);
		uint step = LadderStep(maxPossible);

		List<Row> rows = new List<Row>(FakePlayerCount + 1);
		for (int i = 0; i < FakePlayerCount; i++)
		{
			uint drop = step * (uint)i;
			uint score = top > drop ? top - drop : 0u;
			rows.Add(new Row(FakeUidBase + (uint)(i + 1), score, false));
		}

		// 没打过分就是 0 分。客户端 MonoUltraEndlessRankRowCmpt.SetupView
		// 对 score == 0 的行不画名次（显示「未参与」），这正是真实行为，
		// 不要再塞一个假保底分 —— 以前塞 12000，玩家一打第一层就掉到 1000，
		// 看起来像「分数反而少了」。
		rows.Add(new Row(selfUid, selfScore, true));

		rows.Sort(Compare);
		return rows;
	}

	/// <summary>和 UltraEndlessModule.ComparePlayer @ 0x183861E40 一致</summary>
	private static int Compare(Row a, Row b)
	{
		int diff = (int)b.Score - (int)a.Score;
		if (diff != 0)
		{
			return diff;
		}
		if (a.IsSelf != b.IsSelf)
		{
			return a.IsSelf ? -1 : 1;
		}
		return a.Uid.CompareTo(b.Uid);
	}
}
