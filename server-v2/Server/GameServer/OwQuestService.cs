using System;
using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer;

/// <summary>
/// 开放世界「委托」的业务层：抽池、槽位、proto 组装。
///
/// 樱色轮回（mapId=1）的委托玩法：
///   * 地图上有 5 个委托槽（MonoOpenWorldQuestMapUIManager.TOTAL_QUEST_SLOT_NUM = 5）
///   * 每个槽从 OpenWorldQuestData 里按稀有度权重抽一个候选
///   * 玩家点「接取」→ 该槽变 DOING；打完成 → FINISH；领奖后清空重新抽
///
/// 为什么之前一进委托界面就转圈：
///   GetOpenworldMapRsp.quest_data 被写死成 { is_open = false, quest_list 空 }。
///   客户端 MonetOpenWorldQuestMapUIManager.Reset(mapID) 拿到不存在的委托数据，
///   就一直停在 loading。4474/4476/4546 三条协议服务端也全没有 handler。
/// </summary>
public static class OwQuestService
{
	/// <summary>槽位数量，与客户端 TOTAL_QUEST_SLOT_NUM 对齐。</summary>
	public const int SlotNum = 5;

	/// <summary>每周刷新次数上限（客户端右上角显示的那个数字）。</summary>
	public const uint MaxRefreshTimes = 3;

	/// <summary>刷新消耗的吼姆硬币（按次数递增）。</summary>
	public const uint BaseRefreshCost = 50;

	/// <summary>
	/// 槽位解锁所需剧情（OpenWorldQuestSlot.unlockStoryID）：
	/// 1~4 号槽是 111，5 号槽是 406。
	/// </summary>
	private static readonly uint[] SlotUnlockStory = { 111u, 111u, 111u, 111u, 406u };

	/// <summary>整张地图的委托池（只要普通委托，活动委托走另一条线）。</summary>
	public static List<OpenWorldQuestDataExcel> Pool(uint mapId)
	{
		return (from q in OpenWorldQuestData.GetInstance().All
			where q.QuestMap == (int)mapId && !q.IsActivityQuest && q.QuestType == 1
			select q).ToList();
	}

	public static OpenWorldQuestDataExcel Def(uint mapId, uint questId)
	{
		if (questId == 0)
		{
			return null;
		}

		return (from q in OpenWorldQuestData.GetInstance().All
			where q.QuestId == (int)questId && q.QuestMap == (int)mapId
			select q).FirstOrDefault();
	}

	/// <summary>稀有度越低越常见：rarity 1 权重 15，rarity 15 权重 1。</summary>
	private static int Weight(int rarity)
	{
		return Math.Max(1, 16 - rarity);
	}

	/// <summary>把某个空槽抽满（已有委托的槽不动）。</summary>
	public static void Roll(OwQuestScheme state, OwQuestSlot slot)
	{
		List<OpenWorldQuestDataExcel> pool = Pool(state.MapId);
		if (pool.Count == 0)
		{
			slot.QuestId = 0u;
			slot.Status = 0u;
			return;
		}

		HashSet<uint> used = new HashSet<uint>(from s in state.Slots
			where s != slot && s.QuestId != 0
			select s.QuestId);
		List<OpenWorldQuestDataExcel> candidates = pool.Where(delegate(OpenWorldQuestDataExcel q)
		{
			HashSet<uint> hashSet = used;
			return !hashSet.Contains((uint)q.QuestId);
		}).ToList();
		if (candidates.Count == 0)
		{
			candidates = pool;
		}

		int num = 0;
		foreach (OpenWorldQuestDataExcel item in candidates)
		{
			num += Weight(item.Rarity);
		}

		int num2 = Random.Shared.Next(num);
		OpenWorldQuestDataExcel openWorldQuestDataExcel = candidates[candidates.Count - 1];
		foreach (OpenWorldQuestDataExcel item2 in candidates)
		{
			num2 -= Weight(item2.Rarity);
			if (num2 < 0)
			{
				openWorldQuestDataExcel = item2;
				break;
			}
		}

		slot.QuestId = (uint)openWorldQuestDataExcel.QuestId;
		slot.Status = 0u;
		slot.BuffId = 0u;
		slot.DebuffId = 0u;
		slot.Progress = 0u;
		// rarity_reward_id 必须留 0。
		//
		// 客户端 MonoOpenworldDailyQuestSlot.SetupRewards 会拿它去查
		// OpenWorldQuestRarityReward 表（反汇编 0x180A1D220 →
		// NewOpenWorldModule.GetRewardListFromRarityRewardID）：
		//   非 0 且表里查不到 → 直接 raise_null_reference_exception，
		//   整个槽的填充就此中断，后面的图标/奖励/挑战全不显示。
		// 而 OpenWorldQuestData 里的 rewardId 是「任务奖励」而不是「稀有度奖励」，
		// 两者不是同一张表的 id，混用必然查不到。
		// 留 0 时客户端走 GetQuestTrueRarity 的 cfg.rarity 分支，星级照常显示。
		slot.RarityRewardId = 0u;
		slot.ChallengeList = openWorldQuestDataExcel.ChallengeList?.Select((int x) => (uint)x).ToArray() ?? Array.Empty<uint>();
		slot.FinishChallengeList = Array.Empty<uint>();
	}

	/// <summary>
	/// 委托等级表（questLevel 升序）。OpenWorldQuestLevelData 一共 30 行，1..30。
	/// levelupNeedStar = 从本级升到下一级需要的星数。
	/// </summary>
	private static OpenWorldQuestLevelDataExcel[] Levels()
	{
		return OpenWorldQuestLevelData.GetInstance().All
			.OrderBy((OpenWorldQuestLevelDataExcel x) => x.QuestLevel)
			.ToArray();
	}

	/// <summary>表里的最高等级（正常情况下 30）。</summary>
	public static uint MaxLevel
	{
		get
		{
			OpenWorldQuestLevelDataExcel[] levels = Levels();
			return (uint)((levels.Length != 0) ? levels[levels.Length - 1].QuestLevel : 30);
		}
	}

	/// <summary>累计星数能换到的等级。</summary>
	public static uint LevelByStar(uint totalStar)
	{
		uint level = 1u;
		uint used = 0u;
		foreach (OpenWorldQuestLevelDataExcel row in Levels())
		{
			if (row.QuestLevel != level || level >= MaxLevel)
			{
				break;
			}
			uint need = (uint)Math.Max(1, row.LevelupNeedStar);
			if (totalStar < used + need)
			{
				break;
			}
			used += need;
			level++;
		}
		return level;
	}

	/// <summary>升到 targetLevel 需要的累计星数（满级 30 → 141 星）。</summary>
	public static uint StarForLevel(uint targetLevel)
	{
		uint star = 0u;
		foreach (OpenWorldQuestLevelDataExcel row in Levels())
		{
			if (row.QuestLevel >= targetLevel)
			{
				break;
			}
			star += (uint)Math.Max(1, row.LevelupNeedStar);
		}
		return star;
	}

	/// <summary>调参目标等级（已按表上限收敛）。</summary>
	public static uint TargetLevel => Math.Min(OwMapTuning.QuestLevel, MaxLevel);

	/// <summary>调参目标星数（quest_star 为 0 时按表算满级所需）。</summary>
	public static uint TargetStar
	{
		get
		{
			uint star = OwMapTuning.QuestStar;
			return (star != 0u) ? star : StarForLevel(TargetLevel);
		}
	}

	/// <summary>
	/// 把存档里的冒险等级/星数拉到调参目标（run\openworld.json 的 quest_level / quest_star）。
	///
	/// 客户端 NewOpenWorldModule.GetCurrentQuestLevel 直接读 rsp 里的 quest_level，
	/// 不按星数反算，所以「等级」和「星数」必须一起抬，否则面板是 Lv.30 + 0 星进度条。
	/// </summary>
	private static bool ApplyLevelTuning(OwQuestScheme state)
	{
		uint target = TargetLevel;
		uint star = TargetStar;
		bool changed = false;
		if (state.QuestLevel < target)
		{
			state.QuestLevel = target;
			changed = true;
		}
		if (state.TotalStar < star)
		{
			state.TotalStar = star;
			changed = true;
		}
		return changed;
	}

	/// <summary>补齐 5 个槽并保证每个已解锁的槽都有一个候选委托。</summary>
	public static void EnsureSlots(OwQuestScheme state, UserScheme user)
	{
		if (state.Slots == null)
		{
			state.Slots = new List<OwQuestSlot>();
		}

		bool flag = ApplyLevelTuning(state);
		for (int i = 0; i < SlotNum; i++)
		{
			uint position = (uint)(i + 1);
			OwQuestSlot owQuestSlot = state.Slots.FirstOrDefault((OwQuestSlot s) => s.Position == position);
			if (owQuestSlot == null)
			{
				owQuestSlot = new OwQuestSlot
				{
					Position = position
				};
				state.Slots.Add(owQuestSlot);
				flag = true;
			}

			bool flag2 = IsSlotUnlocked(user, SlotUnlockStory[i]);
			if (!flag2)
			{
				if (owQuestSlot.QuestId != 0 || owQuestSlot.Status != 3)
				{
					owQuestSlot.QuestId = 0u;
					owQuestSlot.Status = 3u;
					flag = true;
				}
			}
			else if (owQuestSlot.QuestId == 0)
			{
				Roll(state, owQuestSlot);
				flag = true;
			}
		}

		state.Slots.Sort((OwQuestSlot a, OwQuestSlot b) => a.Position.CompareTo(b.Position));
		if (flag)
		{
			state.Save();
		}
	}

	/// <summary>
	/// 槽位是否解锁。判定规则：剧情列表里明确「没打完」才算锁；
	/// 列表里根本没有这条剧情（服务端没下发过）时按已解锁处理，
	/// 否则新号一进来 5 个槽全锁，界面上还是空的。
	/// </summary>
	public static bool IsSlotUnlocked(UserScheme user, uint storyId)
	{
		if (user?.OpenWorldStory == null)
		{
			return true;
		}

		// UserScheme 里这个是嵌套类，必须带外層名字。
		UserScheme.OpenWorldStoryScheme openWorldStoryScheme = user.OpenWorldStory.FirstOrDefault((UserScheme.OpenWorldStoryScheme x) => x.StoryId == storyId);
		if (openWorldStoryScheme == null)
		{
			return true;
		}

		return openWorldStoryScheme.IsDone;
	}

	public static OpenworldQuest ToProto(OwQuestScheme state, OwQuestSlot slot)
	{
		return new OpenworldQuest
		{
			Id = slot.QuestId,
			Status = (OpenworldQuestStatus)slot.Status,
			BuffId = slot.BuffId,
			DebuffId = slot.DebuffId,
			Position = slot.Position,
			Progress = slot.Progress,
			// 恒发 0：旧存档里存过「拿 rewardId 冒充 rarity_reward_id」的脏值，
			// 只要非 0 且客户端查不到表就会抛异常，这里直接忽略存档值。
			RarityRewardId = 0u,
			SpecialTimeoutSec = 0u,
			QuestActivityId = 0u,
			FinishChallengeLists = slot.FinishChallengeList ?? Array.Empty<uint>()
		};
	}

	/// <summary>组装 GetOpenworldMapRsp.quest_data / GetOpenworldSelectQuestRsp 用的完整面板数据。</summary>
	public static OpenworldQuestData BuildQuestData(OwQuestScheme state)
	{
		OpenworldQuestData openworldQuestData = new OpenworldQuestData
		{
			Level = state.QuestLevel,
			Star = state.TotalStar,
			IsOpen = state.IsOpen,
			DayOpenTimes = state.DayOpenTimes,
			RefreshLeftTimes = state.RefreshLeftTimes,
			NextRefreshCost = NextRefreshCost(state),
			// OpenWorldQuestLevelData 的 questLevel 是 1..30，不是 15。
			MaxQuestLevel = MaxLevel,
			IsCanAbandon = true,
			ChallengeScore = 0u,
			IsQuestFinish = false,
			OpenQuestTime = state.OpenQuestTime,
			FinishKeyQuestLists = Array.Empty<uint>()
		};
		foreach (OwQuestSlot item in state.Slots.OrderBy((OwQuestSlot s) => s.Position))
		{
			if (item.QuestId != 0)
			{
				openworldQuestData.QuestLists.Add(ToProto(state, item));
			}
		}

		return openworldQuestData;
	}

	public static uint NextRefreshCost(OwQuestScheme state)
	{
		if (state.RefreshLeftTimes != 0)
		{
			return 0u;
		}

		return BaseRefreshCost;
	}

	public static OwQuestSlot FindSlot(OwQuestScheme state, uint questId)
	{
		return state.Slots.FirstOrDefault((OwQuestSlot s) => s.QuestId == questId);
	}
}
