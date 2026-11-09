using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 任务数据下发（GetMissionDataRsp 113）。
///
/// 客户端成就页（WikiOverviewPageContext / MonoWikiAchievementPanel）只吃这里的 mission_list：
///   · 「领取」按钮 = MissionDataItem.CanFetchReward = status == 3(MISSION_FINISH) && 有奖励
///   · 顶部「0/340」 = 遍历 GetNormalAchievements()，status == 5(MISSION_CLOSE) 才算已完成
///     （反汇编 FDPCDIDPGMC.PCJGDGJKONE 得到，cmp dword ptr [item+0x14], 5）
///
/// 所以服务端的语义必须是：
///   未领奖 → MISSION_FINISH(3)   可领取
///   已领奖 → MISSION_CLOSE(5)    已完成 / 不再可领（靠 MissionTakes 表记忆）
/// 之前一律发 FINISH，于是成就永远是「0/340 + 全部亮着领取」。
/// </summary>
internal static class MissionDataBuilder
{
	/// <summary>
	/// 运行期开关 run\mission_ach.txt：
	///   "all"（默认）= 所有任务都当已领奖（status=CLOSE）。成就页每种成就都会算进「已完成」，
	///                  包括 6.6 表 thumb 不是成就图标、但客户端仍当成就展示的那些（如 18021）。
	///   "0"           = 只有 MissionTakes 里领过的才算已完成，其余保持可领取。
	/// </summary>
	private static string AutoCloseMode()
	{
		try
		{
			string path = Path.Combine(AppContext.BaseDirectory, "mission_ach.txt");
			if (!File.Exists(path))
			{
				return "all";
			}

			return File.ReadAllText(path).Trim().ToLowerInvariant();
		}
		catch
		{
			return "all";
		}
	}

	private static bool IsAchievement(MissionDataExcel mission)
	{
		return mission.Thumb != null && mission.Thumb.StartsWith("SpriteOutput/AchievementIcons", StringComparison.Ordinal);
	}

	internal static GetMissionDataRsp Build(Player player)
	{
		GetMissionDataRsp rsp = new GetMissionDataRsp
		{
			retcode = GetMissionDataRsp.Retcode.Succ,
			IsAll = true
		};

		HashSet<uint> taken = new HashSet<uint>(MissionTake.TakenIds(player.User.Uid));
		uint creationTime = player.User.GetCreationTime();
		string mode = AutoCloseMode();
		bool closeAll = mode == "all" || mode == "";
		bool closeAchievements = mode == "ach";

		MissionDataExcel[] all = BaseExcelReader<MissionData, MissionDataExcel>.GetInstance().All;
		rsp.MissionLists.AddRange(all.Where(x => x.Type != 3).Select(mission =>
		{
			bool closed = taken.Contains((uint)mission.Id)
				|| closeAll
				|| (closeAchievements && IsAchievement(mission));
			return new Mission
			{
				MissionId = (uint)mission.Id,
				Status = closed ? MissionStatus.MissionClose : MissionStatus.MissionFinish,
				Priority = (uint)mission.Priority,
				Progress = (uint)mission.TotalProgress,
				BeginTime = creationTime,
				EndTime = 2073239999u,
				CycleId = 1u
			};
		}));

		rsp.MissionLists.AddRange(DailyGameplay.Build(player));

		GodWarCompatibility.AddMissingMissions(rsp, closeAll, taken.ToArray());
        UltraEndlessMissions.Apply(player,rsp);
        GodWarProgression.ApplyDeepMissions(player,rsp);
        GodWarProgression.ApplyStoryMissions(player,rsp,closeAll);
        GodWarProgression.ApplyKeepsakeMission(player,rsp);
		return rsp;
	}
}
