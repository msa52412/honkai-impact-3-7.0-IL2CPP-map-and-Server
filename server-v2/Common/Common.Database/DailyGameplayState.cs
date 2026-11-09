using System.Collections.Generic;

namespace Common.Database;

/// <summary>
/// 日常任务 / 每日委托 / 战令经验的状态。
///
/// 挂在 <see cref="EquipmentScheme.DailyGameplay"/> 上，跟装备存档一起序列化成
/// payload 里的 JSON，不走独立表 —— 这几项和背包强相关（领任务奖励要动物品），
/// 拆两张表就没法在一个事务里回滚（见 DailyGameplay.Atomic）。
///
/// 老存档没有这个字段，反序列化成 null，调用方用 <c>??= new()</c> 补上，
/// 所以这里所有集合都做了初始化。
/// </summary>
public class DailyGameplayState
{
	/// <summary>状态所属的「日」序号（东八区，04:00 换天）。跨天时整块清空。</summary>
	public long Day { get; set; }

	/// <summary>状态所属的「周」序号（周一 04:00 换周）。跨周只清周经验。</summary>
	public long Week { get; set; }

	/// <summary>任务 id → 当前进度。</summary>
	public Dictionary<int, uint> Progress { get; set; } = new Dictionary<int, uint>();

	/// <summary>已领取的日常任务 id。</summary>
	public HashSet<uint> Claimed { get; set; } = new HashSet<uint>();

	/// <summary>已领取的每日/每周活跃值宝箱 id。</summary>
	public HashSet<uint> DutyClaimed { get; set; } = new HashSet<uint>();

	/// <summary>当天累计的（战令）经验。</summary>
	public uint DailyExp { get; set; }

	/// <summary>本周累计经验，上限 10000，到顶后不再增加。</summary>
	public uint WeeklyExp { get; set; }

	/// <summary>战令总经验，只增不减，用来算赛季等级。</summary>
	public ulong TotalBattlePassExp { get; set; }

	/// <summary>已经领取过的战令等级奖励（领到这个等级为止，不会重复领）。</summary>
	public uint BattlePassRewardLevel { get; set; }

	/// <summary>活动系列 id → 今天已经打过几次，用来卡每日次数。</summary>
	public Dictionary<int, uint> ActivityUsed { get; set; } = new Dictionary<int, uint>();

	/// <summary>关卡 id → 今天已经打过几次。</summary>
	public Dictionary<uint, uint> StageUsed { get; set; } = new Dictionary<uint, uint>();

	/// <summary>
	/// 已 StageBegin 但还没 StageEnd 的关卡。用来识别「关掉了但没结算」的孤儿关卡，
	/// 避免同一关卡被重复发奖。
	/// </summary>
	public uint PendingStage { get; set; }

	/// <summary>上面那个关卡的开始时间（Unix 秒）。超过 6 小时就认为作废。</summary>
	public long PendingSince { get; set; }

	/// <summary>普通（非日常）关卡挂起时记录的关卡 id。</summary>
	/// <summary>曜日活动（WeekdayGameplay）已通关的关卡 id。</summary>
	public HashSet<uint> Cleared { get; set; } = new HashSet<uint>();

	/// <summary>关卡 id → 该关已完成的挑战条件 id 集合。</summary>
	public Dictionary<uint, HashSet<uint>> Challenges { get; set; } = new Dictionary<uint, HashSet<uint>>();

	public uint GeneralPendingStage { get; set; }

	/// <summary>普通关卡挂起时间（Unix 秒）。</summary>
	public long GeneralPendingSince { get; set; }
}
