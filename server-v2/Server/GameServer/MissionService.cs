using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;
using ProtoRewardData = Common.Resources.Proto.RewardData;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 任务 / 成就领奖（GetMissionRewardReq 114 → GetMissionRewardRsp 115）。
///
/// 客户端入口：成就页（MonoWikiAchievementPanel）每行的「领取」按钮
///   → OnFetchRewardButtonClicked(item) → MissionModule.RequestGetMissionReward(item.Id)
///   → 本协议。回包成功后 OnMissionRewardGot 弹奖励框并 RefreshUI()（重新拉列表）。
///
/// 服务端这边任务列表是「全量 FINISH」硬造的（见 GetMissionDataReqHandler），
/// 所以校验只看「配置存在 + 没领过」，不校验进度。
/// </summary>
internal static class MissionService
{
	private static MissionData Reader => BaseExcelReader<MissionData, MissionDataExcel>.GetInstance();

	internal static uint[] TakenIds(uint uid)
	{
		return MissionTake.TakenIds(uid);
	}

	/// <summary>
	/// 领取一批任务奖励。返回 null 表示成功，否则是对应的 retcode。
	/// </summary>
	internal static GetMissionRewardRsp.Retcode Take(Player player, uint[] missionIds, out ProtoRewardData reward, out uint[] grantedIds)
	{
		if(missionIds!=null&&missionIds.Contains(GodWarProgression.OldDustMission))
            return GodWarProgression.TakeKeepsakeMission(player,missionIds,out reward,out grantedIds);
        if(missionIds!=null&&missionIds.Any(GodWarProgression.IsStoryMission))return GodWarProgression.TakeStoryMissions(player,missionIds,out reward,out grantedIds);
        if(missionIds!=null&&missionIds.Any(GodWarProgression.IsDeepMission))return GodWarProgression.TakeDeepMissions(player,missionIds,out reward,out grantedIds);
        if(missionIds!=null&&missionIds.Any(UltraEndlessMissions.Handles))return UltraEndlessMissions.Take(player,missionIds,out reward,out grantedIds);
        if (missionIds != null && missionIds.Any(x => Reader.FromId((int)x)?.Type == 3))
            return DailyGameplay.Take(player, missionIds, out reward, out grantedIds);
        reward = new ProtoRewardData();
		List<uint> granted = new List<uint>();
		grantedIds = new uint[0];

		if (missionIds == null || missionIds.Length == 0)
		{
			return GetMissionRewardRsp.Retcode.MissionIdError;
		}

        // Unknown/missing reward data must never be acknowledged as a successful claim.
        if(missionIds.Length>100 || missionIds.Distinct().Count()!=missionIds.Length || missionIds.Any(id=>Reader.FromId((int)id)==null))
            return GetMissionRewardRsp.Retcode.MissionIdError;
        if(missionIds.Any(id=>Reader.FromId((int)id).RewardId>0 && BaseExcelReader<Common.Utils.ExcelReader.RewardData,RewardDataExcel>.GetInstance().FromId(Reader.FromId((int)id).RewardId)==null))
            return GetMissionRewardRsp.Retcode.MissionIdError;

		foreach (uint missionId in missionIds)
		{
			MissionDataExcel? cfg = Reader.FromId((int)missionId);
			if (cfg == null)
			{
				// 7.0 客户端表比服务端（6.6 导出）新，任务 id 可能查不到。
				// 这里不报错，客户端才能把按钮消掉；只是拿不到奖励配置。
				Packet.c.Warn($"[Mission] 领奖：MissionData 里没有 mission_id={missionId}（客户端表比服务端新？）");
				granted.Add(missionId);
				continue;
			}

			if (MissionTake.Has(player.User.Uid, missionId))
			{
				Packet.c.Log($"[Mission] 领奖：mission_id={missionId} 已经领过了，跳过");
				continue;
			}

			if (cfg.RewardId > 0)
			{
				RewardDataExcel? rewardCfg = BaseExcelReader<Common.Utils.ExcelReader.RewardData, RewardDataExcel>.GetInstance().FromId(cfg.RewardId);
				if (rewardCfg != null)
				{
					ProtoRewardData one = RewardService.Grant(player, rewardCfg);
					Merge(reward, one);
				}
				else
				{
					Packet.c.Warn($"[Mission] 领奖：RewardData 里找不到 rewardId={cfg.RewardId}（mission_id={missionId}）");
				}
			}

			MissionTake.Mark(player.User.Uid, missionId);
			granted.Add(missionId);
		}

		grantedIds = granted.ToArray();
		player.User.Save();
		player.Equipment.Save();

		Packet.c.Log($"[Mission] uid={player.User.Uid} 领奖 mission_ids=[{string.Join(",", missionIds)}] 成功发放=[{string.Join(",", grantedIds)}]");
		return GetMissionRewardRsp.Retcode.Succ;
	}

	private static void Merge(ProtoRewardData target, ProtoRewardData add)
	{
		target.Exp += add.Exp;
		target.Scoin += add.Scoin;
		target.Hcoin += add.Hcoin;
		target.Stamina += add.Stamina;
		target.FriendsPoint += add.FriendsPoint;

		foreach (RewardItemData item in add.ItemLists)
		{
			RewardItemData? exist = target.ItemLists.FirstOrDefault(x => x.Id == item.Id);
			if (exist == null)
			{
				target.ItemLists.Add(new RewardItemData
				{
					Id = item.Id,
					Num = item.Num
				});
			}
			else
			{
				exist.Num += item.Num;
			}
		}
	}
}
