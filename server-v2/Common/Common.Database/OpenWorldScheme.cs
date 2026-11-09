
using System.Collections.Generic;

namespace Common.Database;

/// <summary>开放世界里某个区域实体的状态（传送点 / 宝箱 / 门）。</summary>
public class OpenWorldEntityStateRow
{
	public uint AreaId { get; set; }

	public uint EntityId { get; set; }

	public uint EntityState { get; set; }
}

public class OpenWorldScheme : IRow
{
	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	public uint MapId { get; set; }

	public uint Cycle { get; set; }

	public uint QuestLevel { get; set; }

	public uint HasTakeFinishRewardCycle { get; set; }

	public string SpawnPoint { get; set; }

	// ---- 樱之仆从（SakuraServant，樱色轮回地图 MapId=3）----
	// 这些都挂在 OpenWorldScheme 上而不是单独建表：地图状态和采集进度是一起变的，
	// 分表就要跨表事务，而这里的写入路径（TakeOpenworldEventReward）已经在锁里了。

	/// <summary>「只结算一次」类采集点已经领过的 event id。</summary>
	public List<uint> SakuraGatheredOnce { get; set; } = new List<uint>();

	/// <summary>「每日重置」类采集点是哪一天领的（东八区日序号）。</summary>
	public long SakuraGatherDay { get; set; }

	/// <summary>当天已领的采集点。跨天时 <see cref="SakuraGatherDay"/> 一变这份就作废。</summary>
	public List<uint> SakuraGatheredToday { get; set; } = new List<uint>();

	/// <summary>伙伴（樱）的等级与经验。</summary>
	public uint PartnerLevel { get; set; }

	public uint PartnerExp { get; set; }

	/// <summary>已完成的伙伴关键任务 id，升级条件之一。</summary>
	public List<uint> CompletedPartnerQuests { get; set; } = new List<uint>();

	/// <summary>当前挂着的伙伴任务槽（客户端地图上的那个感叹号）。</summary>
	public OwQuestSlot PartnerQuest { get; set; }

	/// <summary>
	/// 客户端 ReportOpenworldAreaEntityStateReq 上报过的实体状态。
	/// 老存档反序列化成 null，取值时用 <see cref="EntityStateList"/> 兜底。
	/// </summary>
	public List<OpenWorldEntityStateRow> EntityStates { get; set; }

	/// <summary>取状态列表（老存档为 null 时补一个空列表）。</summary>
	public List<OpenWorldEntityStateRow> States()
	{
		return EntityStates ??= new List<OpenWorldEntityStateRow>();
	}

	/// <summary>写入/覆盖一个实体状态。返回是否有改动。</summary>
	public bool SetEntityState(uint areaId, uint entityId, uint state)
	{
		List<OpenWorldEntityStateRow> list = States();
		foreach (OpenWorldEntityStateRow row in list)
		{
			if (row.AreaId == areaId && row.EntityId == entityId)
			{
				if (row.EntityState == state)
				{
					return false;
				}
				row.EntityState = state;
				return true;
			}
		}
		list.Add(new OpenWorldEntityStateRow
		{
			AreaId = areaId,
			EntityId = entityId,
			EntityState = state
		});
		return true;
	}

	public void Save()
	{
		OpenWorld.collection.ReplaceOne(this);
	}

}
