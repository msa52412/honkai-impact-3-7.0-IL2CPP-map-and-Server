using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.UltraEndlessGetMainDataReq)]
internal class UltraEndlessGetMainDataReqHandler : IPacketHandler
{
	/// <summary>本期排期号。和 5206 记分、换期清分用同一个来源（<see cref="UltraEndlessState.EffectiveScheduleId"/>）。
	/// run\ultra_endless.json 的 schedule_id 非 0 时以文件为准。</summary>
	private static uint ScheduleId => UltraEndlessState.EffectiveScheduleId;

	/// <summary>
	/// 下发的场景组（SiteID / 10）。
	/// 客户端「四个场景」来自两条**不同**的渲染路径，读的是同一个 site_list：
	///
	///   1) MonoUltraEndlessZoneMapCmpt.SetupSites —— 地图面板的 4 个 Area 槽。
	///      遍历 zone 节点下的 4 个 Area1..Area4 子节点，每个槽各调一次
	///      UltraEndlessModule.GetSiteIDByNodeName(节点名) 反查 site_list，
	///      查不到（返回 0）就把该节点 SetActive(false)。
	///      该方法实物在 0x1838640B0，是严格的 first-match：
	///      按 site_list 顺序遍历，System::String::Equals 命中就立即 return。
	///      → **所以地图上显示的永远是 site_list 里排最前的那一组**，
	///        下面 SiteGroups 里写多个组号时，只有第一个组的地图会被画出来。
	///
	///   2) MonoUltraEndlessZoneMapCmpt.SetupScrollerSites —— 站点滚动列表。
	///      按 site_list 下标逐个渲染，**全部**站点都会出现。
	///
	/// 原实现用 SiteId &gt; 1019 过滤，正好把 1011~1014（第一组）整组排除，
	/// 于是 GetSiteScore 恒为 0、GetSiteMaxScore 恒为 0，
	/// GetSiteProgress 变成 0/0 = NaN 传给 Image.fillAmount，图标渲染不出来。
	/// 这里改成按组白名单下发，保证每个组的 floor 数据都完整。
	///
	/// 当前选的是第 110 组（配置 id 1101 / 1102 / 1103 / 1104）：
	///     1101 Area1 BuffID=4 物理 StageID=146009  StageData_name_Physical1
	///     1102 Area2 BuffID=4 物理 StageID=146010  StageData_name_Physical2
	///     1103 Area3 BuffID=4 物理 StageID=146011  StageData_name_Physical3
	///     1104 Area4 BuffID=3 雷   StageID=146017  StageData_name_Thunder4
	/// 表顺序里这 4 行连续（Site 表第 36~39 行），首匹配无歧义，
	/// 地图 4 个槽和滚动列表都正好是这 4 个站点。
	///
	/// ★ 运行时可用 run\ultra_endless.json 的 site_groups 覆盖，改文件即生效、不用重启。
	///   这里只是「文件没配时」的默认值。
	/// 默认值定义在 <see cref="UltraEndlessRank.DefaultSiteGroups"/>，
	/// 因为 5201（排行榜）也要按同一批站点算总分。
	/// </summary>
	private static readonly int[] DefaultSiteGroups = UltraEndlessRank.DefaultSiteGroups;

	/// <summary>实际生效的组号：优先读 run\ultra_endless.json，没配就用 <see cref="DefaultSiteGroups"/>。</summary>
	private static int[] ActiveSiteGroups()
	{
		return UltraTuning.SiteGroups ?? DefaultSiteGroups;
	}

	/// <summary>
	/// 默认段位：8 = Redlotus（红莲），NeedCupNum = 1500。
	///
	/// 为什么需要兜底：客户端 GetGroupNameByMode(mode, groupLevel) 直接用 groupLevel
	/// 去索引 UltraEndlessGroup 表，取 GroupName（UltraEndless_NameV2_N）再查 TextMap。
	/// 表里 9 档的实际文本已核对：
	///     1        Forbidden  （禁忌）
	///     2/3/4    Sinful     （原罪）
	///     5/6/7    Agony      （苦痛）
	///     8        Redlotus   （红莲）
	///     9        Nirvana    （寂灭）
	/// 老存档 AbyssGroupLevel 可能是 0（实测 uid 1002 就是 0），
	/// ClampLevel 会把 0 夹成 1 → 界面永远显示最低的「禁忌」。
	/// 所以 0 / 表内查不到的值一律按默认段位走，而不是夹到 1。
	/// </summary>
	private const uint DefaultGroupLevel = 8u;

	/// <summary>段位名（仅用于日志，便于人工核对下发档位）</summary>
	private static string LevelName(uint level)
	{
		return level switch
		{
			1u => "Forbidden/禁忌",
			2u or 3u or 4u => "Sinful/原罪",
			5u or 6u or 7u => "Agony/苦痛",
			8u => "Redlotus/红莲",
			9u => "Nirvana/寂灭",
			_ => "?"
		};
	}

	/// <summary>
	/// 解析本次下发的段位（含存档修正）。5203 和 5201 共用，
	/// 保证主界面和排行榜页显示的是同一个段位。
	/// </summary>
	/// <param name="session">会话</param>
	/// <param name="rawLevel">输出：解析前的存档值（仅用于日志）</param>
	internal static uint ResolveGroupLevel(Session session, out uint rawLevel)
	{
		if (UltraEndlessCycle.Enabled)
		{
			rawLevel = UltraEndlessCycle.GroupForCups(UltraEndlessState.Get(session.Player.User.Uid).CupNum);
			return rawLevel;
		}
		UltraEndlessGroup groupReader = BaseExcelReader<UltraEndlessGroup, UltraEndlessGroupExcel>.GetInstance();
		rawLevel = session.Player.User.AbyssGroupLevel;
		// run\ultra_endless.json 的 group_level 非 0 时强制覆盖（只影响本次下发，不写存档）。
		uint overrideLevel = UltraTuning.GroupLevel;
		if (overrideLevel != 0u)
		{
			rawLevel = overrideLevel;
		}
		// 存档里的 0 / 越界值 → 默认段位（红莲），而不是夹到最低的 1。
		// 详见 DefaultGroupLevel 的注释。
		bool levelFallback = groupReader.ByLevel((int)rawLevel) == null;
		uint groupLevel = groupReader.ClampLevel(levelFallback ? DefaultGroupLevel : rawLevel);
		if (levelFallback && overrideLevel == 0u)
		{
			// 顺手把存档修好，下次进界面不再依赖兜底。
			// 文件覆盖模式不动存档，免得一次尝试把玩家段位写死。
			session.Player.User.AbyssGroupLevel = groupLevel;
			session.Player.User.Save();
			Packet.c.Log($"[Ultra] 段位存档值 {rawLevel} 非法，已修正为 {groupLevel}（{LevelName(groupLevel)}）");
		}
		return groupLevel;
	}

	public void Handle(Session session, Packet packet)
	{
		// ── 组等级：必须是 UltraEndlessSchedule.BattleConfigList 里存在的 GroupLevel ──
		// 客户端链路：GroupLevel → ScheduleMeta.BattleConfigList[level].BattleConfigID
		//            → UltraEndlessBattleConfig[BattleConfigID].SiteMapName（"Zone0"）
		//            → MonoUltraEndlessZoneMapCmpt.SetupSiteMap 按名字激活 zone 节点。
		// GroupLevel 对不上时 BattleConfigID = 0，SiteMapName 为空串，
		// 所有 zone 子节点都会被 SetActive(false)，一张地图都不显示。
		UltraEndlessGroup groupReader = BaseExcelReader<UltraEndlessGroup, UltraEndlessGroupExcel>.GetInstance();
		uint rawLevel = session.Player.User.AbyssGroupLevel;
		uint groupLevel = ResolveGroupLevel(session, out rawLevel);

		uint topGroupLevel = (uint)groupReader.MaxGroupLevel;
		if (topGroupLevel == 0u)
		{
			topGroupLevel = groupLevel;
		}

		// CupNum 必须与 GroupLevel 同档，否则 TryGetGroupLevelMetaByCupNum 会算出另一档，
		// 主界面组名/星级与 BattleGroupLevel 不一致。
		var automaticState = UltraEndlessCycle.Enabled ? UltraEndlessState.Get(session.Player.User.Uid) : null;
		uint cupNum = automaticState?.CupNum ?? groupReader.CupNumOf(groupLevel);
		var period = UltraEndlessCycle.Enabled ? UltraEndlessCycle.Current : null;

		Common.Utils.ExcelReader.UltraEndlessFloor floorReader = BaseExcelReader<Common.Utils.ExcelReader.UltraEndlessFloor, UltraEndlessFloorExcel>.GetInstance();

		UltraEndlessGetMainDataRsp rsp = new UltraEndlessGetMainDataRsp
		{
			retcode = UltraEndlessGetMainDataRsp.Retcode.Succ,
			ScheduleId = ScheduleId,
			GroupLevel = groupLevel,
			TopGroupLevel = topGroupLevel,
			CupNum = cupNum,
			MainData = new UltraEndlessMainData
			{
				ScheduleId = ScheduleId,
				EffectTime = period == null ? 0u : (uint)period.Key,
				BeginTime = period == null ? 0u : (uint)period.Key,
				EndTime = period == null ? (uint)(Global.GetUnixInSeconds() + 604800) : (uint)period.End,
				CloseTime = period == null ? (uint)(Global.GetUnixInSeconds() + 606000) : (uint)period.Close,
				CurSeasonId = 1u
			},
			DynamicHardLevel = UltraTuning.DynamicHardLevel ?? session.Player.User.AbyssDynamicHard,
			LastSettleInfo = automaticState?.LastSettlement ?? new UltraEndlessSettleInfo()
		};

		// 分数存档：5206 记下来的每层最高分。
		// 客户端 GetSiteScore = Σ floor_list[].max_score，这里必须回填真实分，
		// 否则地图进度条恒为 0（0/满分），排名里自己也没有真实分数。
		UltraEndlessState scoreState = UltraEndlessState.Get(session.Player.User.Uid);

		int[] siteGroups = ActiveSiteGroups();
		foreach (UltraEndlessSiteExcel siteData in UltraEndlessSites.Active())
		{
			uint siteId = (uint)siteData.SiteId;
			List<UltraEndlessFloorExcel> floors = floorReader.GetFloorDatasFromStageId(siteData.StageId);
			Common.Resources.Proto.UltraEndlessSite site = new Common.Resources.Proto.UltraEndlessSite
			{
				SiteId = siteId
			};
			site.FloorLists.AddRange(floors.Select((UltraEndlessFloorExcel x) => new Common.Resources.Proto.UltraEndlessFloor
			{
				Floor = (uint)x.FloorId,
				MaxScore = scoreState.FloorScore(siteId, (uint)x.FloorId)
			}));
			rsp.MainData.SiteLists.Add(site);
		}

		// ── 排名列表 ──
		// 客户端 MonoUltraEndlessRankPanelCmpt.SetupView 开头两句是：
		//     module = UltraEndlessModule.GetSortedPlayerList();   // = endless_player_list 排序后
		//     if (!CheckSelfRankData()) return;                    // 直接返回，一个字都不画
		// 而 CheckSelfRankData 的实现是：
		//     if (module == null || module.Count < 1) return false;
		//     selfIndex = module.FindIndex(x => x.uid == 自己的 uid);
		//     return selfIndex != -1;
		// 也就是说「自己不在 endless_player_list 里」→ 整张榜空白。
		// 所以必须把自己也放进列表，另外补 10 个阶梯分数的假玩家把榜单铺满。
		//
		// 排序规则（UltraEndlessModule.ComparePlayer）：
		//     max_stage_score 降序 → score_update_time 升序 → uid 升序
		//
		// 每一行的昵称/头像/头像框来自 brief_data_list：
		//     MonoUltraEndlessRankRowCmpt.SetupView 里 GetFriendBriefData(playerInfo.uid)
		//     查不到就退化成空串 + 默认头像，所以每个 uid 都要配一条 brief。
		// 分数口径：客户端排名行显示的是 UltraEndlessPlayer.max_stage_score
		// （MonoUltraEndlessRankRowCmpt.SetupView 里 mov eax,[rbp+0x1c] → _scoreText），
		// 而客户端自己算「我的分数」用的是 GetAllSiteScore() = Σ 所有站点 Σ 各层最高分。
		// 两边必须是同一个数，所以这里下发**本期总分**。
		// 以前下发的是 MaxStageScore（单层最高分），打完 10 层分数一直停在 2000 不动。
		uint selfScore = UltraEndlessRank.TotalScore(scoreState);
		AppendRankList(session, rsp, groupLevel, selfScore, scoreState.RankLineup());

		string detail = string.Join(" ", rsp.MainData.SiteLists
			.Select(x => $"{x.SiteId}({x.FloorLists.Count}f/{x.FloorLists.Sum(f => (long)f.MaxScore)}p)"));
		// 日志里的 rank 原来是 rsp.EndlessPlayerLists.Count（永远 11），看着像名次其实不是。
		// 这里改成「自己的名次 / 榜单总人数」，方便对照排行榜页。
		int selfIndex = rsp.EndlessPlayerLists.FindIndex(x => x.Uid == session.Player.User.Uid);
		Packet.c.Log($"[Ultra] 5203 GetMainData: schedule={ScheduleId} rawLevel={rawLevel} "
			+ $"level={groupLevel}({LevelName(groupLevel)}) cup={cupNum} "
			+ $"hard={rsp.DynamicHardLevel} top={topGroupLevel} "
			+ $"groups=[{string.Join(",", siteGroups)}] "
			+ $"sites={rsp.MainData.SiteLists.Count} "
			+ $"selfScore={selfScore}/{UltraEndlessRank.MaxPossibleScore()} "
			+ $"rank={(selfIndex < 0 ? 0 : selfIndex + 1)}/{rsp.EndlessPlayerLists.Count}"
			+ $"(brief={rsp.BriefDataLists.Count}) -> {detail}");

		session.Send(Packet.FromProto(rsp, CmdId.UltraEndlessGetMainDataRsp));
        AbyssSettlementRewards.Notify(session);
	}

	/// <summary>
	/// 往 rsp 里填 endless_player_list + brief_data_list。
	///
	/// 假玩家阶梯和自己那一行的排序统一走 <see cref="UltraEndlessRank.BuildRows"/>，
	/// 和 5201 排行榜页用的是同一份数据，两处名次/分数不会打架。
	/// </summary>
	private static void AppendRankList(Session session, UltraEndlessGetMainDataRsp rsp, uint groupLevel, uint selfScore, LineupBriefInfo lineup)
	{
		Common.Database.UserScheme user = session.Player.User;
		uint avatarId = (uint)((user.AssistantAvatarId != 0) ? user.AssistantAvatarId : 101);
		string nick = string.IsNullOrEmpty(user.Nick) ? user.Name : user.Nick;
		uint playerLevel = (uint)BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>
			.GetInstance().CalculateLevel(user.Exp).Level;

		uint total = (uint)UltraEndlessRank.FakePlayerCount + 1u;
		uint now = (uint)Global.GetUnixInSeconds();

		List<UltraEndlessRank.Row> rows = UltraEndlessRank.BuildRows(user.Uid, selfScore);

		for (int i = 0; i < rows.Count; i++)
		{
			uint rank = (uint)i + 1u;
			UltraEndlessRank.Row row = rows[i];
			uint rowUid = row.Uid;
			uint rowScore = row.Score;
			bool isSelf = row.IsSelf;
			var bot = !isSelf && UltraEndlessCycle.Enabled ? UltraEndlessCycle.Bot(rowUid) : null;

			rsp.EndlessPlayerLists.Add(new UltraEndlessPlayer
			{
				Uid = rowUid,
				GroupLevel = groupLevel,
				CupNum = isSelf && UltraEndlessCycle.Enabled ? rsp.CupNum :
					UltraEndlessCycle.Enabled ? UltraEndlessCycle.BotCups(groupLevel, rowUid) :
					UltraEndlessRank.BaseCupNum + (total - rank) * UltraEndlessRank.CupStep,
				MaxStageScore = rowScore,
				ScoreUpdateTime = UltraEndlessCycle.Enabled ? row.UpdatedAt : now - (rank - 1u) * 180u,
				LineupInfo = isSelf ? lineup : null
			});

			rsp.BriefDataLists.Add(new PlayerFriendBriefData
			{
				Uid = rowUid,
				Nickname = isSelf ? nick : bot?.Nickname ?? $"挑战者{rank:00}",
				Level = isSelf ? playerLevel : 88u,
				AvatarId = isSelf ? avatarId : bot?.Avatar ?? 101u,
				CustomHeadId = isSelf ? (uint)user.CustomHeadId : bot?.Head ?? 161001u,
				FrameId = isSelf ? (uint)user.FrameId : bot?.Frame ?? 200001u,
				OnlineStatus = 1u
			});
		}
	}
}
