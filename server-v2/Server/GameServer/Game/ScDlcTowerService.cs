using System;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 后崩坏书 2（客户端内部叫 DLC2 / ScDLC）爬塔服务。
///
/// 为什么要有：开放世界列表点「进入」时，客户端在 GetScDLCReq 之后还会发
/// GetScDLCTowerReq（6332），原来服务端没接（日志 `GetScDLCTowerReq not handled!`），
/// 拿不到塔数据就一直转圈。
///
/// 客户端 DLC2TowerData.Refresh(GetScDLCTowerRsp)（0x18244F120）：
///   this[+0x10] = rsp.schedule_id      ← 再用它查 ScDLCTowerSchedule，查不到赛季就是空
///   this[+0x1c] = rsp.max_floor
///   SetCurFloor(rsp.cur_floor)
///   this[+0x20] = rsp.avatar_revive_times
///   this[+0x24] = rsp.max_can_sweep_floor
///   this[+0x30] = rsp.max_score
/// 以及 DLC2TowerPageContext.OnShowPageGetScDLCTowerRsp（0x181247B00）：
///   **retcode != SUCC 就直接关页**，所以必须回 SUCC。
///
/// cur_floor 语义 = 已通过层数，下一层 = cur_floor + 1。
/// </summary>
public static class ScDlcTowerService
{
	/// <summary>
	/// 当前生效的赛季。
	///
	/// 优先级：GM 指定的赛季 ID &gt; GM 指定的天气（挑一期带该天气的赛季）&gt; 按时间。
	/// 天气只能靠赛季带出来（客户端查 ScDLCTowerSchedule.Weather），
	/// 所以「换天气」就是「换 schedule_id」，见 <see cref="TowerSchedulePicker"/>。
	/// </summary>
	public static ScDLCTowerScheduleDataExcel CurrentSchedule()
	{
		ScDLCTowerScheduleData reader = ScDLCTowerScheduleData.GetInstance();

		uint forcedId = TowerTuning.ScDlcScheduleId;
		if (forcedId != 0u)
		{
			ScDLCTowerScheduleDataExcel forced = reader.Get((int)forcedId);
			if (forced != null)
			{
				return forced;
			}
			Packet.c.Warn("[ScDlcTower] tower.json 指定的赛季 " + forcedId + " 不在表里，回退");
		}

		uint wantWeather = TowerTuning.ScDlcWeather;
		if (wantWeather != 0u)
		{
			ScDLCTowerScheduleDataExcel[] all = reader.All;
			if (all != null && all.Length > 0)
			{
				List<TowerSeason> rows = new List<TowerSeason>(all.Length);
				foreach (ScDLCTowerScheduleDataExcel row in all)
				{
					rows.Add(new TowerSeason(row.ID, row.Weather, row.StartTime, row.EndTime));
				}
				int picked = TowerSchedulePicker.PickByWeather(rows, (int)wantWeather, DateTime.Now);
				if (picked != 0)
				{
					ScDLCTowerScheduleDataExcel hit = reader.Get(picked);
					if (hit != null)
					{
						return hit;
					}
				}
				Packet.c.Warn("[ScDlcTower] 找不到天气 " + wantWeather + " 的赛季，回退按时间选");
			}
		}

		ScDLCTowerScheduleDataExcel current = reader.Current();
		if (current != null)
		{
			return current;
		}

		ScDLCTowerScheduleDataExcel[] fallback = reader.All;
		if (fallback == null || fallback.Length == 0)
		{
			return null;
		}
		ScDLCTowerScheduleDataExcel best = fallback[0];
		foreach (ScDLCTowerScheduleDataExcel item in fallback)
		{
			if (item.ID > best.ID)
			{
				best = item;
			}
		}
		return best;
	}

	/// <summary>本赛季总层数（客户端 max_floor 的基准）。</summary>
	public static uint MaxFloor()
	{
		ScDLCTowerScheduleDataExcel schedule = CurrentSchedule();
		if (schedule == null || schedule.MaxFloor <= 0)
		{
			return 1u;
		}
		return (uint)schedule.MaxFloor;
	}

	/// <summary>取（必要时新建 / 换赛季重置）该玩家的塔存档。</summary>
	public static ScDlcTowerScheme State(uint uid, out ScDLCTowerScheduleDataExcel schedule)
	{
		schedule = CurrentSchedule();
		uint scheduleId = schedule != null ? (uint)schedule.ID : 0u;
		ScDlcTowerScheme state = ScDlcTower.GetOrCreate(uid, scheduleId, out bool reset);
		if (reset)
		{
			Packet.c.Log($"[ScDlcTower] uid={uid} 赛季切换 -> {scheduleId}，进度重置");
		}
		return state;
	}

	/// <summary>
	/// 塔队伍血量/能量。服务端不模拟战斗，直接给满值（100 = 满），
	/// 避免客户端把队伍标成「残血」而不让开战。
	/// </summary>
	public static List<ScDLCTowerAvatarInfo> AvatarInfos()
	{
		List<ScDLCTowerAvatarInfo> list = new List<ScDLCTowerAvatarInfo>();
		ScDLCAvatarDataExcel[] all = BaseExcelReader<ScDLCAvatarData, ScDLCAvatarDataExcel>.GetInstance().All;
		if (all == null)
		{
			return list;
		}
		foreach (ScDLCAvatarDataExcel row in all)
		{
			if (row.AvatarId == 0)
			{
				continue;
			}
			list.Add(new ScDLCTowerAvatarInfo
			{
				AvatarId = (uint)row.AvatarId,
				HpPercent = 100u,
				SpPercent = 100u
			});
		}
		return list;
	}

	/// <summary>构造 6333 GetScDLCTowerRsp。</summary>
	public static GetScDLCTowerRsp BuildRsp(uint uid, out ScDlcTowerScheme state, out ScDLCTowerScheduleDataExcel schedule)
	{
		state = State(uid, out schedule);
		uint maxFloor = MaxFloor();

		GetScDLCTowerRsp rsp = new GetScDLCTowerRsp
		{
			retcode = GetScDLCTowerRsp.Retcode.Succ,
			ScheduleId = state.ScheduleId,
			CurFloor = Math.Min(state.CurFloor, maxFloor),
			MaxFloor = maxFloor,
			MaxScore = state.MaxScore,
			AvatarReviveTimes = state.ReviveTimes,
			ScDlcFeverScore = 0u,
			ScDlcClimaxScore = 0u,
			MaxCanSweepFloor = Math.Min(state.MaxCanSweepFloor, maxFloor)
		};

		rsp.AvatarInfoLists.AddRange(AvatarInfos());
		return rsp;
	}

	/// <summary>
	/// 6334 开始一层：回要加载的层号。
	///
	/// **cur_floor 是「当前层」，不是「已通过层数」**：
	/// 实测清完第 1 层后客户端上报的 floor = 2（它把当前层推到了 2），
	/// 所以这里直接回 CurFloor，不能再 +1，否则会跳过第 2 层。
	/// 初始 0 表示还没开始，回 1。
	/// </summary>
	public static uint Begin(uint uid)
	{
		ScDlcTowerScheme state = State(uid, out ScDLCTowerScheduleDataExcel schedule);
		uint maxFloor = MaxFloor();
		uint floor = state.CurFloor == 0u ? 1u : Math.Min(state.CurFloor, maxFloor);
		Packet.c.Log($"[ScDlcTower] StageBegin uid={uid} schedule={state.ScheduleId}"
			+ $"({schedule?.ConfigType}) floor={floor}/{maxFloor}");
		return floor;
	}

	/// <summary>
	/// 6336 一层结束：客户端上报本层 floor 与得分，floor 超过当前进度才推进
	/// （失败时客户端会回传同样的 floor，不会倒退）。
	/// </summary>
	public static void End(uint uid, ScDLCTowerStageEndReq req, out uint floor, out uint settleFloor, out uint score)
	{
		ScDlcTowerScheme state = State(uid, out ScDLCTowerScheduleDataExcel schedule);
		uint maxFloor = MaxFloor();

		// 客户端一般会带上本层 floor；不带（=0）就按「打完当前这层」推一格，
		// 否则进度永远停在原地，下一层还是同一层。
		uint reported = req.Floor == 0u
			? Math.Min(state.CurFloor + 1u, maxFloor)
			: Math.Min(req.Floor, maxFloor);
		if (reported > state.CurFloor)
		{
			state.CurFloor = reported;
		}
		if (state.CurFloor > state.MaxFloorReached)
		{
			state.MaxFloorReached = state.CurFloor;
		}
		if (req.Score > state.MaxScore)
		{
			state.MaxScore = req.Score;
		}
		state.MaxCanSweepFloor = state.CurFloor;
		state.Save();

		floor = reported;
		settleFloor = state.CurFloor;
		score = req.Score;
		Packet.c.Log($"[ScDlcTower] StageEnd uid={uid} schedule={state.ScheduleId}"
			+ $"({schedule?.ConfigType}) reported={reported} => cur={state.CurFloor}/{maxFloor}"
			+ $" max={state.MaxFloorReached} score={state.MaxScore}");
	}

	/// <summary>
	/// GM「清空记录」：层数 / 历史最高 / 最高分 / 复活次数 / 可扫荡层数 全部归零，
	/// 等于从没打过这座塔。返回 false 表示本来就没什么可清的（没有存档，或者已经是全 0）。
	///
	/// 注意别和「换赛季」混为一谈：换赛季（schedule_id 变了）会保留 MaxFloorReached / MaxScore，
	/// 那是为了不让「历史最高」凭空掉下去；GM 清空是连历史一起抹掉。
	/// </summary>
	public static bool ClearRecords(uint uid)
	{
		ScDlcTowerScheme state = ScDlcTower.Get(uid);
		if (state == null)
		{
			return false;
		}
		bool dirty = state.CurFloor != 0u || state.MaxFloorReached != 0u || state.MaxScore != 0u
			|| state.ReviveTimes != 0u || state.MaxCanSweepFloor != 0u;
		if (!dirty)
		{
			return false;
		}
		state.CurFloor = 0u;
		state.MaxFloorReached = 0u;
		state.MaxScore = 0u;
		state.ReviveTimes = 0u;
		state.MaxCanSweepFloor = 0u;
		state.Save();
		Packet.c.Log($"[ScDlcTower] GM 清空记录 uid={uid} schedule={state.ScheduleId} -> 全部归零");
		return true;
	}
}
