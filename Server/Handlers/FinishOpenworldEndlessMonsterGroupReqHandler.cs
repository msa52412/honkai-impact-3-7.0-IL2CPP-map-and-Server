using System;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 3424/3425 —— 旧深渊「打完一个怪物组」的上报与推进。
///
/// 客户端语义（反汇编 QTInLevelData.OnFinishMonsterGroupRsp）：
///     if (rsp.retcode != 0) 什么都不做；
///     if (rsp.type == 3)    monsterGroup = rsp.quantum_endless.cur_monster_group;
/// 也就是说：**下一场打谁完全由服务端这个字段决定**。
///
/// 所以这里必须按客户端的 SplitBattles 顺序推进：
///     index = SplitBattles.indexOf(req.group_id)
///     next  = SplitBattles[index + 1].groupScoreId
/// 并且把本场分数累进 state.Score（客户端只做飘分动画，不会自己算）。
///
/// 2026-10-26 补两件事
/// ------------------
/// ① 回包之后**再推一条 3445 OpenworldEndlessPlayerUpdateNotify**。
///    客户端 OnOpenworldEndlessPlayerUpdateNotify -> EndlessBaseData.Refresh(notify)
///    才是「自己那一行的分数/名次」的刷新通道。原来只回 3425 不推 3445，
///    界面上的分数要等下一次 3420 重新拉才变，观感就是「打完没提示」。
/// ② 把进度落盘（OwEndlessState.Persist）。原来纯内存，服务端一重启分数就没了。
/// </summary>
[PacketCmdId(CmdId.FinishOpenworldEndlessMonsterGroupReq)]
internal class FinishOpenworldEndlessMonsterGroupReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		FinishOpenworldEndlessMonsterGroupReq req = packet.GetDecodedBody<FinishOpenworldEndlessMonsterGroupReq>();
		uint type = (uint)req.Type;
		if (type == 0u)
		{
			type = (uint)EndlessType.EndlessTypeOpenworld;
		}

		(OwEndlessState state, OWEndlessSplitBattle[] splits) =
			OwEndlessState.Ensure(session.Player.User.Uid, (int)type);

		uint groupId;
		bool isBoss = false;
		int bossLayer = -1;
		uint[] newlyFinished;
		uint oldScore;
		uint newScore;
		uint maxScore;
		uint nextGroup;
		uint[] finished;
		uint groupLevel;
		uint gain;
		uint battleConfigId;

		lock (state.Gate)
		{
			groupId = req.GroupId != 0u ? req.GroupId : state.CurGroupId;
			if (groupId == 0u && splits.Length > 0)
			{
				groupId = OwEndlessConfig.ReportedGroupId(
					(int)type, state.BattleConfigId, splits[Math.Min(state.SplitIndex, splits.Length - 1)]);
			}

			oldScore = state.Score;
			battleConfigId = state.BattleConfigId;
			gain = state.Advance((int)type, battleConfigId, splits, groupId, req.ExtraScore,
				out isBoss, out bossLayer, out newlyFinished, out nextGroup);
			newScore = state.Score;
			maxScore = state.MaxScore;
			finished = state.FinishedGroups.ToArray();
			groupLevel = state.GroupLevel;
		}

		FinishOpenworldEndlessMonsterGroupRsp rsp = new FinishOpenworldEndlessMonsterGroupRsp
		{
			retcode = FinishOpenworldEndlessMonsterGroupRsp.Retcode.Succ,
			GroupId = groupId,
			Score = newScore,
			OldScore = oldScore,
			BattleId = req.BattleId,
			TransactionStr = req.TransactionStr,
			IsBoss = isBoss,
			GroupLevel = groupLevel,
			Type = (EndlessType)type,
			IsSkipLevel = req.IsSkipLevel,
			FinalBossId = req.FinalBossId,
			FinishMonsterGroupLists = finished
		};

		if (type == (uint)EndlessType.EndlessTypeQuantum)
		{
			rsp.QuantumEndless = new QuantumEndless
			{
				CurMonsterGroup = nextGroup,
				MonsterProgress = 0u,
				FinishMonsterNum = (uint)finished.Length
			};
		}

		// 2026-10-26 关键修正：客户端只用 monster_group_list 里 progress > 9999 的项
		// 来判定「哪些组已打完」（OWEndLessBattleLevelInfo::RefreshFinishGroupList）。
		// finish_monster_group_list 是给别处看的，弹窗和地图完成态都不读它。
		// 原来这里只放「下一组」且 progress=0，导致：
		//   ① 结算弹窗匹配不到刚打完的组 → SetupView 空转 → 没有分数提示
		//   ② 退出重进所有区域仍显示未完成 → 看起来像分数没保存
		// 另外 BOSS 项的 id 必须用 BossScoreConfig 的（131/132/133/134），
		// 客户端 layerInfo.bossGroup.groupScoreId 就是这个值，用 3605 永远对不上。
		foreach (OpenworldEndlessMonsterGroup grp in
			OwEndlessState.BuildGroupList((int)type, battleConfigId, nextGroup, 0u, finished))
		{
			rsp.MonsterGroupLists.Add(grp);
		}

		Packet.c.Log($"[Abyss] FinishGroup type={(int)type} group={groupId} boss={isBoss}"
			+ (bossLayer >= 0 ? $"(layer {bossLayer})" : string.Empty)
			+ $" extra={req.ExtraScore} gain={gain} score={oldScore}->{newScore} next={nextGroup} "
			+ $"newly={newlyFinished.Length} finished={finished.Length}/{splits.Length} "
			+ $"groups={rsp.MonsterGroupLists.Count} "
			+ $"battle={req.BattleId} skip={req.IsSkipLevel} finalBoss={req.FinalBossId}");
		if (isBoss)
		{
			Packet.c.Log($"[Abyss]   BOSS 层结算：本层及以下应有 {OwEndlessConfig.ScoreUpToLayer((int)type, battleConfigId, bossLayer)} 分，"
				+ $"当前 {newScore}；本次新完成 [{string.Join(",", newlyFinished)}]");
		}

		session.Send(Packet.FromProto(rsp, CmdId.FinishOpenworldEndlessMonsterGroupRsp));

		// ---- 3445 分数更新推送（可用 abyss_ow.json 的 push_score_notify 关掉）----
		if (OwTuning.PushScoreNotify)
		{
			UserScheme user = session.Player.User;
			string nick = string.IsNullOrEmpty(user.Nick) ? user.Name : user.Nick;
			uint avatarId = (uint)(user.AssistantAvatarId != 0 ? user.AssistantAvatarId : 101);
			PlayerLevelData.LevelData levelData = BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>
				.GetInstance().CalculateLevel(user.Exp);

			session.Send(Packet.FromProto(new OpenworldEndlessPlayerUpdateNotify
			{
				Type = (EndlessType)type,
				PlayerData = new OpenworldEndlessPlayer
				{
					Uid = user.Uid,
					Score = newScore,
					MaxScore = maxScore,
					ScoreTime = (uint)Global.GetUnixInSeconds(),
					GroupLevel = groupLevel,
					RewardProgress = 0u,
					UnfinishedMonsterMaxProgress = 0u,
					GreedyMaxFinishFloor = 0u
				},
				BriefData = new PlayerFriendBriefData
				{
					Uid = user.Uid,
					Nickname = nick,
					Level = (uint)levelData.Level,
					AvatarId = avatarId,
					CustomHeadId = (uint)user.CustomHeadId,
					FrameId = (uint)user.FrameId,
					MpLevel = 0u,
					OnlineStatus = 1u
				}
			}, CmdId.OpenworldEndlessPlayerUpdateNotify));
		}

		// ---- 落盘（必须在 lock 外面，见 OwEndlessState.Persist 的注释） ----
		OwEndlessState.Persist();
	}
}
