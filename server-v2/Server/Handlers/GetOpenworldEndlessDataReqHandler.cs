using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 迪拉克之海 / 量子奇点 —— 旧版开放世界深渊的数据入口。
///
/// 2026-10-25 17:45 修订：把「模式数据」从写死改成查表
/// ------------------------------------------------
/// 用户进入界面后看到三处不对：
///   ① 玩家ID 显示不正常
///   ② 当前所处区域（原罪/禁忌/苦痛/红莲/无限）为最低
///   ③ 扰动幅度为 1
///
/// 三处都能在响应里定位到具体字段：
///
///   ② 区域  ← GroupLevel。原来下发 21，但 21 属于 OWEndlessGroup 里
///             playerGroup 2 的阶梯（21~24，只有 4 档）；界面上那 5 个段位
///             图标 IconGroup_3..7 对应的是 playerGroup 3 的 31~35。
///             客户端按 5 档阶梯查不到 21 → 退到第一档 → 永远「原罪」。
///             现在按表取值并夹紧，默认 35（无限）。
///
///   ③ 扰动幅度 ← HardLevel。原来写死 1u，界面照实显示 1。
///             正确值就是 OWEndlessGroup.groupHardLevel（35 → 7）。
///
///   ① 玩家ID ← brief_data_list 原来是空的。客户端渲染排行/自己那一行时
///             拿不到昵称头像，只能退回显示裸 uid，看起来就是「ID 不正常」。
///             现在补一条 PlayerFriendBriefData（昵称/等级/头像/头像框）。
///
/// 顺带把 CurTopGroupLevel / TopGroupPlayerNum / TopGroupPromoteUnlockPlayerNum
/// 也改成从同一行表里取，保证「当前档位」和「晋级条件」自洽。
///
/// 注意：这里**不碰任何存档字段**。AbyssGroupLevel（建号默认 8）留给超弦空间，
/// 旧深渊的分组只走 AbyssConfig.OwGroupLevel，热切换命令 `abyss owlevel <n>`。
/// </summary>
[PacketCmdId(CmdId.GetOpenworldEndlessDataReq)]
internal class GetOpenworldEndlessDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetOpenworldEndlessDataReq req = packet.GetDecodedBody<GetOpenworldEndlessDataReq>();
		UserScheme user = session.Player.User;

		// 这行日志是最重要的观测点：客户端到底在要哪一套深渊。
		Packet.c.Log($"[Abyss] GetOpenworldEndlessDataReq type={(int)req.Type} level={req.Level} finishFront={req.IsFinishFront}");

		uint now = (uint)Global.GetUnixInSeconds();

		EndlessType type = req.Type;
		if (type == EndlessType.EndlessTypeNone)
		{
			// 空的时候按 OPENWORLD（迪拉克之海）应答，这是旧版的主形态。
			type = EndlessType.EndlessTypeOpenworld;
		}

		// ---- 档位：查 OWEndlessGroup，非法值夹紧到最近的合法档 ----
		// 目标值优先取 run\abyss_ow.json 的覆盖（改文件即生效，不用重启）。
		// 2026-10-26：白名单必须按模式区分 —— 量子还有 11~14 三段，迪拉克没有。
		int want = (int)OwTuning.GroupLevel;
		OWEndlessGroupExcel group = OwEndlessConfig.ResolveGroup((int)type, want);
		uint groupLevel = (uint)group.GroupLevel;
		uint hardLevel = (uint)group.GroupHardLevel;

		// 同一条阶梯的最高档 —— 界面用它显示「顶层」和晋级说明。
		int topLevel = OWEndlessGroup.GetInstance()
			.Ladder((int)type, group.PlayerGroup)
			.Select((OWEndlessGroupExcel row) => row.GroupLevel)
			.DefaultIfEmpty(group.GroupLevel)
			.Max();
		uint curTopOverride = OwTuning.CurTopGroupLevel;
		if (curTopOverride != 0u)
		{
			// 覆盖值也必须落在该模式的客户端白名单里：
			// 当前 abyss_ow.json 是 34（迪拉克红莲），直接丢给量子会查空行。
			uint[] safeLevels = OwEndlessConfig.SafeLevels((int)type);
			uint topSafe = safeLevels
				.Where((uint lv) => lv >= curTopOverride)
				.DefaultIfEmpty(safeLevels.Max())
				.Min();
			if (topSafe != curTopOverride)
			{
				Packet.c.Log($"[Abyss] CurTopGroupLevel {curTopOverride} 不在 type={(int)type} 白名单内，夹紧到 {topSafe}");
			}
			topLevel = (int)topSafe;
		}

		uint avatarId = (uint)((user.AssistantAvatarId != 0) ? user.AssistantAvatarId : 101);
		string nick = string.IsNullOrEmpty(user.Nick) ? user.Name : user.Nick;
		PlayerLevelData.LevelData levelData = BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>
			.GetInstance().CalculateLevel(user.Exp);

		// 其余数值也允许用调参文件覆盖 —— 17:53 那次客户端静默拒绝了整份数据，
		// 而相比 17:32 可用的那份，除了 GroupLevel 还动了 hard_level / cur_top /
		// top_group_player_num / promote_unlock_num / brief 五处，
		// 必须能逐项关掉做二分，否则只能瞎猜。
		uint topNum = OwTuning.TopGroupPlayerNum != 0u
			? OwTuning.TopGroupPlayerNum : (uint)group.MaxCapacity;
		uint promoteNum = OwTuning.PromoteUnlockNum != 0u
			? OwTuning.PromoteUnlockNum : (uint)group.PromoteRank;
		uint hardOut = OwEndlessConfig.EffectiveHardLevel((int)type, hardLevel);
		// hardOut 是界面上「能级 / 扰动幅度」显示的数，可以随便大；
		// 但选盘（AreaScoreConfigID = 2000 + 能级）只在 2001..2008 里有行，必须夹住。
		uint battleHard = OwEndlessConfig.BattleHardLevel((int)type, hardLevel);
		if (battleHard != hardOut)
		{
			Packet.c.Log($"[Abyss] 能级 {hardOut} 仅用于界面显示，选盘按能级 {battleHard}");
		}

		// ---- 地图：哪几张区域图、哪张 BOSS 图、什么天气 ----
		// 客户端 OWEndLessBattleLevelInfo.Refresh 用 rsp.battle_config_id 反查
		// OWEndlessBattleConfig，拿到 _areaList / _bossAreaIDList / _environmentIDList / _mapID。
		// 2026-10-26 关键修正：覆盖值只有在该行属于本次 type 时才采用。
		// 原来 62136（Type=2 的行）会被原样塞进量子响应，客户端 ByKey(62136, 3)
		// 查不到 → QTInLevelData.Refresh 里 battleConfig/areaScoreConfig 全 null。
		uint battleConfigId = OwEndlessConfig.ResolveRequestedBattleConfigId((int)type, battleHard);
		OWEndlessBattleConfigExcel? battleCfg = OwEndlessConfig.BattleConfig(battleConfigId);
		if (battleCfg == null)
		{
			Packet.c.Warn($"[Abyss] OWEndlessBattleConfig 里找不到 type={(int)type} 的可用配置");
		}
		else
		{
			Packet.c.Log("[Abyss] 地图 " + OWEndlessBattleConfig.Describe(battleCfg)
				+ (OwEndlessConfig.IsOldAbyss((uint)type)
					? " | " + OWEndlessAreaScoreConfig.Describe(
						OWEndlessAreaScoreConfig.GetInstance().FromKey(battleCfg.AreaScoreConfigID, (int)type)
						?? new OWEndlessAreaScoreConfigExcel())
					: string.Empty));
		}

		// ---- 本周期进度（内存态，按 uid + type 分开） ----
		OwEndlessState state = OwEndlessState.Get(user.Uid, (uint)type);
		uint stateScore;
		uint stateMaxScore;
		uint stateProgress;
		uint stateGroup;
		uint[] stateFinished;
		OWEndlessSplitBattle[] stateSplits;
		lock (state.Gate)
		{
			OWEndlessSplitBattle[] splits = OwEndlessConfig.SplitBattles((int)type, battleConfigId);
			state.Align((int)type, battleConfigId, splits);
			state.GroupLevel = groupLevel;
			stateSplits = splits;
			stateScore = state.Score;
			stateMaxScore = state.MaxScore;
			stateProgress = state.MonsterProgress;
			stateGroup = state.CurGroupId;
			stateFinished = state.FinishedGroups.ToArray();
		}


		// 本期时间窗：原来是 BeginTime=0 / EndTime=now+7d，
		// 客户端排期界面因此显示「1970-…」。改由 OwTuning.Period 统一给。
		OwTuning.Period(out uint beginTime, out uint endTime, out uint closeTime);

		GetOpenworldEndlessDataRsp rsp = new GetOpenworldEndlessDataRsp
		{
			retcode = GetOpenworldEndlessDataRsp.Retcode.Succ,
			Type = type,
			GroupId = 1u,
			GroupLevel = groupLevel,
			BeginTime = beginTime,
			EndTime = endTime,
			CloseTime = closeTime,
			RandomSeed = 1u,
			BattleConfigId = battleConfigId,
			HardLevel = hardOut,
			BossExtraScore = (uint)group.ExtraScore,
			CurTopGroupLevel = (uint)topLevel,
			TopGroupPlayerNum = topNum,
			TopGroupPromoteUnlockPlayerNum = promoteNum,
			InvasionId = OwTuning.InvasionId,
			IsJustJoin = 0u
		};

		// ---- 分组名单 ----
		// 客户端不是「只画自己」：MonoOWEndlessAvatarLayout 把 endless_data_list
		// 里所有人按 score 排序铺成升降级区（_promoteLine/_remainLine/_demoteLine），
		// 每行再用 GroupInfo.GetNickName(uid) 去 brief_data_list 里找昵称。
		// 所以名单必须成规模下发，只给 1 条时升级区/保级区的分界线和名次都算不出来。
		uint rosterSize = OwTuning.RosterSize;
		if (rosterSize == 0u)
		{
			rosterSize = 20u;
		}
		if (rosterSize > 50u)
		{
			rosterSize = 50u;
		}

		// 自己放第 5 名：在 promote 区（前 7）里，方便肉眼确认名次/升降级箭头对不对。
		// 2026-10-26：一旦本周期真的打过（state.Score > 0），自己那行就用真实分，
		// 否则量子进关后返回界面会看到分数「跳回去」。
		uint selfRank = rosterSize >= 5u ? 5u : rosterSize;
		uint selfScore = stateScore != 0u ? stateScore : 20000u - (selfRank - 1u) * 500u;

		for (uint rank = 1u; rank <= rosterSize; rank++)
		{
			bool isSelf = rank == selfRank;
			uint rowUid = isSelf ? user.Uid : 900000u + rank;
			uint rowScore = isSelf ? selfScore : 20000u - (rank - 1u) * 500u;
			string rowNick = isSelf ? nick : $"挑战者{rank:00}";
			uint rowAvatar = isSelf ? avatarId : 101u;
			uint rowHead = isSelf ? (uint)user.CustomHeadId : 161001u;
			uint rowFrame = isSelf ? (uint)user.FrameId : 200001u;

			rsp.EndlessDataLists.Add(new OpenworldEndlessPlayer
			{
				Uid = rowUid,
				Score = rowScore,
				MaxScore = isSelf ? System.Math.Max(rowScore, stateMaxScore) : rowScore,
				ScoreTime = now - (rank - 1u) * 60u,
				GroupLevel = groupLevel,
				RewardProgress = 0u,
				UnfinishedMonsterMaxProgress = 0u,
				GreedyMaxFinishFloor = 0u
			});

			// brief_mask / brief_self_only 是二分开关：客户端「brief 非空 -> 拒绝整份响应 ->
			// 原地重发」时，用它把字段和条数拆开，定位到底是哪个值不可接受。
			if (OwTuning.SendBrief && (!OwTuning.BriefSelfOnly || isSelf))
			{
				uint mask = OwTuning.BriefMask;
				PlayerFriendBriefData brief = new PlayerFriendBriefData { Uid = rowUid };
				if (mask == 0u || (mask & 1u) != 0u)
				{
					brief.Nickname = rowNick;
				}
				if (mask == 0u || (mask & 2u) != 0u)
				{
					brief.Level = (uint)levelData.Level;
				}
				if (mask == 0u || (mask & 4u) != 0u)
				{
					brief.AvatarId = rowAvatar;
				}
				if (mask == 0u || (mask & 8u) != 0u)
				{
					brief.CustomHeadId = rowHead;
				}
				if (mask == 0u || (mask & 16u) != 0u)
				{
					brief.FrameId = rowFrame;
				}
				if (mask == 0u || (mask & 32u) != 0u)
				{
					brief.MpLevel = 0u;
				}
				if (mask == 0u || (mask & 64u) != 0u)
				{
					brief.OnlineStatus = 1u;
				}
				rsp.BriefDataLists.Add(brief);
			}
		}

		// 量子奇点走同一个响应，但额外带 quantum_endless 这一块。
		// 客户端 QTInLevelData.Refresh 只在 rsp.type == 3 时执行，并直接采用
		// cur_monster_group 作为当前 groupScoreId、monster_progress 作为组内进度。
		// 之前这里固定回 0，等于每次都把客户端按回第一场。
		if (type == EndlessType.EndlessTypeQuantum)
		{
			rsp.QuantumEndless = new QuantumEndless
			{
				CurMonsterGroup = stateGroup,
				MonsterProgress = stateProgress,
				FinishMonsterNum = (uint)stateFinished.Length
			};
		}
		else if (type == EndlessType.EndlessTypeGreedy)
		{
			rsp.GreedyEndless = new GreedyEndless
			{
				IsNeedEnroll = false,
				FloorConfigId = 0u,
				RewardConfigId = 0u,
				CurFloor = 0u,
				MaxFinishFloor = 0u
			};
		}

		// 已完成的 group 列表：客户端 EndlessBaseData.RefreshFinishGroup 按它建集合，
		// 用来隐藏已经打过的场 / 画对勾。空列表会让界面把每场都当没打。
		rsp.FinishMonsterGroupLists = stateFinished;

		// 2026-10-26 关键修正：客户端判断「组是否打完」**只认** monster_group_list
		// 里 progress > 9999 的项，finish_monster_group_list 它压根不读
		// （OWEndLessBattleLevelInfo::RefreshFinishGroupList 反汇编实证，
		//   详见 OwEndlessState.BuildGroupList 的注释）。
		// 原来这里只下发「当前组」一条且 progress=0，等于告诉客户端「一场都没打完」：
		//   · 打完一波怪不弹分数（弹窗按 isFinished 找刚打完的组，找不到就空转）
		//   · 退出重进地图上所有区域仍是未完成
		// 所以改成把全部组都铺出来，已完成的给 10000。
		// 2026-11-08：forceCurrentPlayable=true —— 全通后光标停在最后一场 BOSS，
		// 这里必须把那一场按「未完成」下发，否则客户端地图上没有可挑战节点，
		// 表现就是「最后一层不能反复打」。
		foreach (OpenworldEndlessMonsterGroup grp in
			OwEndlessState.BuildGroupList((int)type, battleConfigId, stateGroup, stateProgress,
				stateFinished, forceCurrentPlayable: true))
		{
			rsp.MonsterGroupLists.Add(grp);
		}

		// 回包关键值必须打出来 —— 客户端查表失败时不会有任何提示，
		// 只能靠这一行核对「下发的 GroupLevel / HardLevel 到底对不对」。
		Packet.c.Log($"[Abyss] GetOpenworldEndlessDataRsp type={(int)type} GroupId=1 "
			+ $"BattleConfigId={battleConfigId} "
			+ $"areas={(battleCfg?.AreaIDList?.Length ?? 0)} bossAreas={(battleCfg?.BossAreaIDList?.Length ?? 0)} "
			+ $"GroupLevel={groupLevel}(playerGroup {group.PlayerGroup}) "
			+ $"HardLevel={hardOut} CurTopGroupLevel={topLevel} "
			+ $"TopGroupPlayerNum={topNum} PromoteRank={promoteNum} InvasionId={rsp.InvasionId} "
			+ $"roster={rsp.EndlessDataLists.Count} brief={rsp.BriefDataLists.Count} "
			+ $"selfRank={selfRank}/{rosterSize} selfScore={selfScore} "
			+ $"score={stateScore} curGroup={stateGroup} progress={stateProgress} "
			+ $"finished={stateFinished.Length}/{stateSplits.Length} groups={rsp.MonsterGroupLists.Count} "
			+ $"uid={user.Uid} nick={nick} lv={levelData.Level}");

		// ---- 游戏内道具（背包 / 生效效果 / 陷阱图标）----
		// 客户端的 RefreshMyItemListByMainRsp 只转发 item_list；effect_item_list 在主界面不会被应用，
		// 但 3427 / 3423 会用它，所以这里照样带上一份，方便客户端切页时状态一致。
		OwEndlessItemState items = OwEndlessItems.Get(user.Uid, (uint)type);
		OwEndlessItems.FillMainRsp(items, rsp);
		Packet.c.Log($"[Abyss] 道具 type={(int)type} uid={user.Uid} "
			+ $"bag={rsp.ItemLists.Count} effect={rsp.EffectItemLists.Count} "
			+ $"traps={items.Traps.Count}");

		session.Send(Packet.FromProto(rsp, CmdId.GetOpenworldEndlessDataRsp));
	}
}
