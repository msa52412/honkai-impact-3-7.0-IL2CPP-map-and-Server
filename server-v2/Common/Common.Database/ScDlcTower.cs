using System.Collections.Generic;
using System.Linq;

namespace Common.Database;

/// <summary>
/// 一个玩家的后崩坏书 2（DLC2）爬塔存档（每个赛季一条）。
///
/// 协议字段对应关系（客户端 DLC2TowerData.Refresh(GetScDLCTowerRsp) 0x18244F120）：
///   rsp.schedule_id         → this[+0x10]  （再拿它查 ScDLCTowerSchedule）
///   rsp.cur_floor           → SetCurFloor()
///   rsp.max_floor           → this[+0x1c]
///   rsp.max_score           → this[+0x30]
///   rsp.avatar_revive_times → this[+0x20]
///   rsp.max_can_sweep_floor → this[+0x24]
///
/// cur_floor 语义：**已通过的层数**，下一层 = cur_floor + 1（与 DLC1 爬塔的 progress 一致）。
/// </summary>
public class ScDlcTowerScheme : IRow
{
	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	/// <summary>ScDLCTowerSchedule.ID。换赛季时整条重置。</summary>
	public uint ScheduleId { get; set; }

	/// <summary>当前进度：已通过的层数。</summary>
	public uint CurFloor { get; set; }

	/// <summary>本赛季历史最高层（重置进度不清）。</summary>
	public uint MaxFloorReached { get; set; }

	/// <summary>本赛季最高分。</summary>
	public uint MaxScore { get; set; }

	public uint ReviveTimes { get; set; }

	/// <summary>可扫荡到的层数（= 已通过层数）。</summary>
	public uint MaxCanSweepFloor { get; set; }

	public void Save()
	{
		ScDlcTower.collection.ReplaceOne(this);
	}
}

public class ScDlcTower
{
	public static readonly SqlTable<ScDlcTowerScheme> collection =
		Global.Table<ScDlcTowerScheme>("ScDlcTowers", ("owner_uid", "INTEGER", x => x.OwnerUid, false));

	public static ScDlcTowerScheme Get(uint uid)
	{
		return (from x in collection.All()
			where x.OwnerUid == uid
			select x).FirstOrDefault();
	}

	/// <summary>
	/// 取存档；旧赛季（ScheduleId 不同）就按新赛季重置进度，
	/// 但保留 MaxFloorReached / MaxScore，避免「历史最高」凭空掉下去。
	/// </summary>
	public static ScDlcTowerScheme GetOrCreate(uint uid, uint scheduleId, out bool reset)
	{
		reset = false;
		ScDlcTowerScheme scheme = Get(uid);
		if (scheme == null)
		{
			scheme = new ScDlcTowerScheme
			{
				OwnerUid = uid,
				ScheduleId = scheduleId,
				CurFloor = 0u,
				MaxFloorReached = 0u,
				MaxScore = 0u,
				ReviveTimes = 0u,
				MaxCanSweepFloor = 0u
			};
			collection.InsertOne(scheme);
			return scheme;
		}

		if (scheme.ScheduleId != scheduleId)
		{
			scheme.ScheduleId = scheduleId;
			scheme.CurFloor = 0u;
			scheme.ReviveTimes = 0u;
			scheme.MaxCanSweepFloor = 0u;
			scheme.Save();
			reset = true;
		}

		return scheme;
	}
}
