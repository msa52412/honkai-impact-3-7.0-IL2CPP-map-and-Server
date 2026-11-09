using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Database;

/// <summary>
/// 单个委托槽（地图上的一格委托位）。
/// 对应协议里的 OpenworldQuest，但这里只存"这个槽当前是什么状态"，
/// 名称/图标/奖励这些静态内容全部回表查，不落库。
/// </summary>
public class OwQuestSlot
{
	/// <summary>槽位号 1..5，与 OpenWorldQuestSlot.SlotPosition 对齐。</summary>
	public uint Position { get; set; }

	/// <summary>委托 ID（OpenWorldQuestData.questId）。0 = 空槽。</summary>
	public uint QuestId { get; set; }

	/// <summary>OpenworldQuestStatus：0 NONE / 1 DOING / 2 FINISH / 3 CLOSE。</summary>
	public uint Status { get; set; }

	public uint BuffId { get; set; }

	public uint DebuffId { get; set; }

	public uint Progress { get; set; }

	public uint RarityRewardId { get; set; }

	public uint[] FinishChallengeList { get; set; } = Array.Empty<uint>();

	public uint[] ChallengeList { get; set; } = Array.Empty<uint>();
}

/// <summary>
/// 一个玩家在一张开放世界地图上的委托存档。
///
/// 樱色轮回（mapId=1）的委托机制：地图上有 5 个委托槽，每个槽从
/// OpenWorldQuestData 的池子里抽一个候选，玩家选一个接取（DOING），
/// 打完交任务（FINISH）领奖后槽位空出来重新抽。
/// </summary>
public class OwQuestScheme : IRow
{
	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	public uint MapId { get; set; }

	public List<OwQuestSlot> Slots { get; set; } = new List<OwQuestSlot>();

	/// <summary>本周内还能刷新多少次（客户端右上角那个数字）。</summary>
	public uint RefreshLeftTimes { get; set; } = 3;

	/// <summary>委托等级（OpenWorldQuestMapLevel.questLevel），影响奖励档位。</summary>
	public uint QuestLevel { get; set; } = 1;

	/// <summary>累计星数（TakeOpenworldQuestReward 的 add_star 累加到这里）。</summary>
	public uint TotalStar { get; set; }

	/// <summary>本周已接取的活动委托数量（GetOpenworldQuestActivityRsp.cycle_quest_taken_num）。</summary>
	public uint CycleQuestTakenNum { get; set; }

	/// <summary>
	/// 委托面板是否已「打开」（OpenworldQuestData.is_open）。
	///
	/// 这个字段是那个「转圈」的直接嫌疑：GetOpenworldMapRsp 原来把它写死成 false，
	/// 客户端认为委托面板没开，就一直显示加载中。4474 OpenOpenworldQuestReq
	/// 成功后应当把它置 true。
	/// </summary>
	public bool IsOpen { get; set; }

	/// <summary>今天已经打开过几次（OpenworldQuestData.day_open_times）。</summary>
	public uint DayOpenTimes { get; set; }

	/// <summary>上次打开的时间戳（OpenworldQuestData.open_quest_time）。</summary>
	public uint OpenQuestTime { get; set; }

	public void Save()
	{
		OwQuest.collection.ReplaceOne(this);
	}
}

public class OwQuest
{
	public static readonly SqlTable<OwQuestScheme> collection = Global.Table<OwQuestScheme>("OpenWorldQuests", ("owner_uid", "INTEGER", x => x.OwnerUid, false), ("map_id", "INTEGER", x => x.MapId, false));

	public static OwQuestScheme Get(uint uid, uint mapId)
	{
		uint uid2 = uid;
		uint mapId2 = mapId;
		return (from x in collection.All()
			where x.OwnerUid == uid2 && x.MapId == mapId2
			select x).FirstOrDefault();
	}

	public static OwQuestScheme GetOrCreate(uint uid, uint mapId)
	{
		OwQuestScheme owQuestScheme = Get(uid, mapId);
		if (owQuestScheme != null)
		{
			if (owQuestScheme.Slots == null)
			{
				owQuestScheme.Slots = new List<OwQuestSlot>();
			}

			return owQuestScheme;
		}

		OwQuestScheme owQuestScheme2 = new OwQuestScheme
		{
			OwnerUid = uid,
			MapId = mapId,
			Slots = new List<OwQuestSlot>(),
			RefreshLeftTimes = 3u,
			QuestLevel = 1u,
			// 默认按「已开启」处理：客户端 MonoOpenWorldQuestMapUIManager 拿到
			// is_open=false 时会停在 loading，进委托界面就是那个转圈。
			IsOpen = true
		};
		collection.InsertOne(owQuestScheme2);
		return owQuestScheme2;
	}
}
