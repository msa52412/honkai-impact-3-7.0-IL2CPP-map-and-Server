using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Database;

/// <summary>某一层的历史最高分（GetDLCTowerRsp.floor_max_score_list 的一项）。</summary>
public class DlcTowerFloorScore
{
	public uint Floor { get; set; }

	public uint MaxScore { get; set; }
}

/// <summary>
/// 一个玩家的爬塔存档（每个赛季一条）。
///
/// 协议字段对应关系（客户端 DLCTowerData.Refresh(GetDLCTowerRsp) 0x18074DA80 逐字段赋值）：
///   rsp.schedule_id        → scheduleID
///   rsp.progress           → CurrentDoneFloor   （已通过层数，下一层 = progress + 1）
///   rsp.max_progress       → CurrentMaxFloor    （历史最高，IsTowerFinished 用它比 MaxFloor）
///   rsp.tower_cost_time    → costTime
///   rsp.avatar_revive_times→ AvatarReviveTimes
///   rsp.floor_max_score_list → FloorScoreList
/// </summary>
public class DlcTowerScheme : IRow
{
	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	/// <summary>DLCTowerSchedule.ID。换赛季时整条重置。</summary>
	public uint ScheduleId { get; set; }

	/// <summary>当前进度：已通过的层数。</summary>
	public uint Progress { get; set; }

	/// <summary>本赛季历史最高层（打过就算，重置进度不清）。</summary>
	public uint MaxProgress { get; set; }

	public uint CostTime { get; set; }

	public uint ReviveTimes { get; set; }

	public List<DlcTowerFloorScore> FloorScores { get; set; } = new List<DlcTowerFloorScore>();

	public void Save()
	{
		DlcTower.collection.ReplaceOne(this);
	}

	public uint ScoreOf(uint floor)
	{
		foreach (DlcTowerFloorScore item in FloorScores)
		{
			if (item.Floor == floor)
			{
				return item.MaxScore;
			}
		}
		return 0u;
	}

	public void SetScore(uint floor, uint score)
	{
		if (FloorScores == null)
		{
			FloorScores = new List<DlcTowerFloorScore>();
		}
		foreach (DlcTowerFloorScore item in FloorScores)
		{
			if (item.Floor == floor)
			{
				if (score > item.MaxScore)
				{
					item.MaxScore = score;
				}
				return;
			}
		}
		FloorScores.Add(new DlcTowerFloorScore
		{
			Floor = floor,
			MaxScore = score
		});
	}
}

public class DlcTower
{
	public static readonly SqlTable<DlcTowerScheme> collection = Global.Table<DlcTowerScheme>("DlcTowers", ("owner_uid", "INTEGER", x => x.OwnerUid, false));

	public static DlcTowerScheme Get(uint uid)
	{
		return (from x in collection.All()
			where x.OwnerUid == uid
			select x).FirstOrDefault();
	}

	/// <summary>
	/// 取存档；如果存档来自旧赛季（ScheduleId 不同），按新赛季重置进度，
	/// 但保留历史最高层（MaxProgress），这样「已通关」的观感不会掉。
	/// </summary>
	public static DlcTowerScheme GetOrCreate(uint uid, uint scheduleId, out bool reset)
	{
		reset = false;
		DlcTowerScheme scheme = Get(uid);
		if (scheme == null)
		{
			scheme = new DlcTowerScheme
			{
				OwnerUid = uid,
				ScheduleId = scheduleId,
				Progress = 0u,
				MaxProgress = 0u,
				CostTime = 0u,
				ReviveTimes = 0u,
				FloorScores = new List<DlcTowerFloorScore>()
			};
			collection.InsertOne(scheme);
			return scheme;
		}

		if (scheme.ScheduleId != scheduleId)
		{
			scheme.ScheduleId = scheduleId;
			scheme.Progress = 0u;
			scheme.CostTime = 0u;
			scheme.ReviveTimes = 0u;
			scheme.FloorScores = new List<DlcTowerFloorScore>();
			scheme.Save();
			reset = true;
		}

		if (scheme.FloorScores == null)
		{
			scheme.FloorScores = new List<DlcTowerFloorScore>();
		}
		return scheme;
	}
}
