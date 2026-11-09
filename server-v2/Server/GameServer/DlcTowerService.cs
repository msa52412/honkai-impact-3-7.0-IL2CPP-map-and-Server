using System;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer;

/// <summary>
/// 后崩坏书爬塔（无限塔）服务。
///
/// 服务端原来没有 GetDLCTowerReq 的 handler（日志里每次登录都有一条
/// `GetDLCTowerReq not handled!`），客户端拿不到 MyTowerData，
/// 界面就是「0 层 / 没有赛季」。
///
/// 客户端 DLCTowerData.Refresh(GetDLCTowerRsp)（0x18074DA80）逐字段赋值：
///   scheduleMetaData = DLCTowerSchedule.Get(rsp.schedule_id)   ← 表里查不到就是 null
///   currentFloorMetaData = DLCTowerFloor[(progress+1), schedule.ConfigType]
///   IsTowerFinished = max_progress &gt;= schedule.MaxFloor
///   CanTowerBattle = progress &lt; schedule.MaxFloor
///   FloorScoreList = rsp.floor_max_score_list
/// progress 语义：**已通过层数**，下一层 = progress + 1（客户端 get_CurrentFloor 就是这个）。
///
/// 赛程表 DLCTowerSchedule.json 有 454 个赛季（按周排到 2035），
/// 当前时间正好落在 255 号（2026-10-26 ~ 11-02，ConfigType 1201，23 层），
/// 用 DLCTowerScheduleData.Current() 动态取，跨周也不会失效。
/// </summary>
public static class DlcTowerService
{
	/// <summary>
	/// 当前生效的赛季。
	///
	/// 优先级：GM 指定的赛季 ID &gt; GM 指定的天气（挑一期带该天气的赛季）&gt; 按时间。
	/// 天气只能靠赛季带出来（客户端查 DLCTowerSchedule.Weather），
	/// 所以「换天气」就是「换 schedule_id」，见 <see cref="TowerSchedulePicker"/>。
	/// </summary>
	public static DLCTowerScheduleDataExcel CurrentSchedule()
	{
		DLCTowerScheduleData reader = DLCTowerScheduleData.GetInstance();

		uint forcedId = TowerTuning.DlcScheduleId;
		if (forcedId != 0u)
		{
			DLCTowerScheduleDataExcel forced = reader.Get((int)forcedId);
			if (forced != null)
			{
				return forced;
			}
			Packet.c.Warn("[DLCTower] tower.json 指定的赛季 " + forcedId + " 不在表里，回退");
		}

		uint wantWeather = TowerTuning.DlcWeather;
		if (wantWeather != 0u)
		{
			DLCTowerScheduleDataExcel[] all = reader.All;
			if (all != null && all.Length > 0)
			{
				List<TowerSeason> rows = new List<TowerSeason>(all.Length);
				foreach (DLCTowerScheduleDataExcel row in all)
				{
					rows.Add(new TowerSeason(row.ID, row.Weather, row.StartTime, row.EndTime));
				}
				int picked = TowerSchedulePicker.PickByWeather(rows, (int)wantWeather, DateTime.Now);
				if (picked != 0)
				{
					DLCTowerScheduleDataExcel hit = reader.Get(picked);
					if (hit != null)
					{
						return hit;
					}
				}
				Packet.c.Warn("[DLCTower] 找不到天气 " + wantWeather + " 的赛季，回退按时间选");
			}
		}

		DLCTowerScheduleDataExcel current = reader.Current();
		if (current != null)
		{
			return current;
		}

		DLCTowerScheduleDataExcel[] fallback = reader.All;
		if (fallback == null || fallback.Length == 0)
		{
			return null;
		}
		DLCTowerScheduleDataExcel best = fallback[0];
		foreach (DLCTowerScheduleDataExcel item in fallback)
		{
			if (item.ID > best.ID)
			{
				best = item;
			}
		}
		return best;
	}

	/// <summary>本赛季总层数（客户端 IsTowerFinished 的基准）。</summary>
	public static uint MaxFloor()
	{
		DLCTowerScheduleDataExcel schedule = CurrentSchedule();
		if (schedule == null || schedule.MaxFloor <= 0)
		{
			return 1u;
		}
		return (uint)schedule.MaxFloor;
	}

	/// <summary>取（必要时新建/换赛季重置）该玩家的爬塔存档。</summary>
	public static DlcTowerScheme State(uint uid, out DLCTowerScheduleDataExcel schedule)
	{
		schedule = CurrentSchedule();
		uint scheduleId = schedule != null ? (uint)schedule.ID : 0u;
		bool reset;
		DlcTowerScheme state = DlcTower.GetOrCreate(uid, scheduleId, out reset);
		if (reset)
		{
			Packet.c.Log($"[DLCTower] uid={uid} 赛季切换 -> {scheduleId}，进度重置");
		}
		return state;
	}

	/// <summary>
	/// 挑战中每个 DLC 角色的血量/能量（HP/SP 百分比，100 = 满）。
	/// 服务端不模拟战斗，直接给满值，避免客户端把队伍标成「残血」。
	/// </summary>
	public static List<DLCTowerAvatarInfo> AvatarInfos()
	{
		List<DLCTowerAvatarInfo> list = new List<DLCTowerAvatarInfo>();
		foreach (DLCAvatarDataExcel row in DLCAvatarData.GetInstance().All)
		{
			if (row.DLCAvatarID == 0)
			{
				continue;
			}
			list.Add(new DLCTowerAvatarInfo
			{
				AvatarId = (uint)row.DLCAvatarID,
				HpPercent = 100u,
				SpPercent = 100u
			});
		}
		return list;
	}

	/// <summary>构造 3575 GetDLCTowerRsp。</summary>
	public static GetDLCTowerRsp BuildRsp(uint uid, out DlcTowerScheme state, out DLCTowerScheduleDataExcel schedule)
	{
		state = State(uid, out schedule);

		GetDLCTowerRsp rsp = new GetDLCTowerRsp
		{
			retcode = GetDLCTowerRsp.Retcode.Succ,
			ScheduleId = state.ScheduleId,
			Progress = state.Progress,
			MaxProgress = Math.Max(state.MaxProgress, state.Progress),
			TowerCostTime = state.CostTime,
			AvatarReviveTimes = state.ReviveTimes
		};

		foreach (DlcTowerFloorScore item in state.FloorScores)
		{
			rsp.FloorMaxScoreLists.Add(new DLCTowerFloorMaxScore
			{
				Floor = item.Floor,
				MaxScore = item.MaxScore
			});
		}

		rsp.AvatarInfoLists.AddRange(AvatarInfos());
		return rsp;
	}

	/// <summary>3563 开始挑战：回当前进度，客户端据此加载 progress+1 层。</summary>
	public static uint Begin(uint uid)
	{
		DlcTowerScheme state = State(uid, out DLCTowerScheduleDataExcel schedule);
		uint progress = Math.Min(state.Progress, MaxFloor());
		Packet.c.Log($"[DLCTower] StageBegin uid={uid} schedule={state.ScheduleId} progress={progress}/{MaxFloor()} floor={progress + 1}");
		return progress;
	}

	/// <summary>
	/// 3565 结束挑战：只有 StageWin（1）才推进进度，其余状态（失败/退出/条件不满足）保持原样，
	/// 但血量/复活次数/耗时要照实落库。
	/// </summary>
	public static void End(uint uid, DLCTowerStageEndReq req, out uint progress, out uint score, out bool isNewRecord)
	{
		DlcTowerScheme state = State(uid, out DLCTowerScheduleDataExcel schedule);
		uint maxFloor = MaxFloor();

		// 客户端上传的是「打完这层之后的已通过层数」。
		uint reported = req.Progress;
		if (reported == 0u)
		{
			// 有些版本不上传 progress，那就按「打完当前这层」推一格。
			reported = state.Progress + 1u;
		}
		if (reported > maxFloor)
		{
			reported = maxFloor;
		}

		bool win = req.EndStatus == StageEndStatus.StageWin;
		if (win && reported > state.Progress)
		{
			state.Progress = reported;
		}

		if (state.Progress > state.MaxProgress)
		{
			state.MaxProgress = state.Progress;
		}

		state.CostTime += req.StagePassTime;
		isNewRecord = false;

		// 分数：客户端算的是剩余时间，服务端没有战斗数据，
		// 用「本层耗时越短分越高」的粗略公式，仅用于列表展示。
		score = 0u;
		if (win)
		{
			uint floor = reported;
			uint oldScore = state.ScoreOf(floor);
			score = Math.Max(100u, 1000u - Math.Min(900u, req.StagePassTime));
			if (score > oldScore)
			{
				state.SetScore(floor, score);
				isNewRecord = oldScore > 0u;
			}
			else
			{
				score = oldScore;
			}
		}

		state.Save();

		progress = state.Progress;
		Packet.c.Log($"[DLCTower] StageEnd uid={uid} status={req.EndStatus} win={win} reported={req.Progress} "
			+ $"=> progress={state.Progress}/{maxFloor} max={state.MaxProgress} score={score} newRecord={isNewRecord} "
			+ $"time={req.StagePassTime} schedule={state.ScheduleId}({(schedule != null ? schedule.ConfigType : 0)})");
	}

	/// <summary>3576 重置进度：清空已通过层数与分数，保留历史最高层。</summary>
	public static uint Reset(uint uid)
	{
		DlcTowerScheme state = State(uid, out _);
		state.Progress = 0u;
		state.CostTime = 0u;
		state.ReviveTimes = 0u;
		state.FloorScores = new List<DlcTowerFloorScore>();
		state.Save();
		Packet.c.Log($"[DLCTower] ResetProgress uid={uid} -> progress=0 keepMax={state.MaxProgress}");
		return 0u;
	}

	/// <summary>
	/// GM「清空记录」：连历史最高层和每层分数一起抹掉，等于从没打过。
	///
	/// 和上面的 <see cref="Reset"/> 不一样 —— Reset 是游戏内的 3576 请求，
	/// 只清当前进度、保留 MaxProgress；GM 要的是「回到全新状态」。
	/// 返回 false 表示本来就没什么可清的（没有存档，或者已经是全 0）。
	/// </summary>
	public static bool ClearRecords(uint uid)
	{
		DlcTowerScheme state = DlcTower.Get(uid);
		if (state == null)
		{
			return false;
		}
		bool dirty = state.Progress != 0u || state.MaxProgress != 0u || state.CostTime != 0u
			|| state.ReviveTimes != 0u || (state.FloorScores != null && state.FloorScores.Count > 0);
		if (!dirty)
		{
			return false;
		}
		state.Progress = 0u;
		state.MaxProgress = 0u;
		state.CostTime = 0u;
		state.ReviveTimes = 0u;
		state.FloorScores = new List<DlcTowerFloorScore>();
		state.Save();
		Packet.c.Log($"[DLCTower] GM 清空记录 uid={uid} schedule={state.ScheduleId} -> 全部归零");
		return true;
	}
}
