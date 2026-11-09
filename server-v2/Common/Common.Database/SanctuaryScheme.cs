namespace Common.Database;

/// <summary>
/// 家园小队（Sanctuary / 圣域）存档，一对一，一个玩家一行。
///
/// 为什么单独一张表
/// ----------------
/// 宿舍（Dorm，cmd 601~642）和家园小队（Sanctuary，cmd 4305~4314）是两套独立系统：
///   * Dorm 管房间 / 家具 / 装修 / 放人，DormHouse.Level 在客户端是「装修等级」；
///   * Sanctuary 管小队等级 / 经验 / 远征 / 打工，等级来自 SanctuaryActivityData.level。
/// 之前服务端完全没有 Sanctuary 侧实现，日志里
///     `SanctuaryGetMainInfoReq not handled!`
/// 客户端拿不到 SanctuaryActivityData，界面就一直显示「等级.1 / 经验 0/2147483647」
/// （2147483647 = int.MaxValue，是查不到等级配置时的兜底值）。
///
/// 字段与 proto SanctuaryActivityData 一一对应，另外多了 OwnerUid / LastSyncTime。
/// </summary>
public class SanctuaryScheme : IRow
{
	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	/// <summary>活动 id，固定 1001（SanctuaryPlayerGroup.json 三行都是它）。</summary>
	public uint ActivityId { get; set; }

	/// <summary>家园小队等级，1~20（TextMap 里 Asrama Lv. 只到 20）。</summary>
	public uint Level { get; set; }

	/// <summary>当前等级内的经验进度（不是累计经验）。</summary>
	public uint Exp { get; set; }

	/// <summary>已领取等级奖励的水位线。等级奖励表在本版资源里不存在，先让它跟随 Level，避免红点永远消不掉。</summary>
	public uint HasTakeRewardLevel { get; set; }

	/// <summary>打工计时起点（Unix 秒）。SanctuaryBuildingActivity.json 是空表，暂时恒为 0。</summary>
	public uint ProductCounterTime { get; set; }

	/// <summary>打工已存产物数量。同上，暂时恒为 0。</summary>
	public uint ProductStorageNum { get; set; }

	/// <summary>是否已经初始化过（首次下发前置真，之后按存档走）。</summary>
	public bool IsInit { get; set; }

	/// <summary>玩家等级分组（101 / 102 / 103），由 SanctuaryPlayerGroup.json 按玩家等级算出来。</summary>
	public uint PlayerLevelGroupId { get; set; }

	/// <summary>已解锁的远征关卡 id 列表。SanctuaryStageConfig.json 有 45 关（161001~161045）。</summary>
	public uint[] StageIdLists { get; set; } = System.Array.Empty<uint>();

	/// <summary>是否有新关卡（客户端用来打「NEW」标）。</summary>
	public bool IsNewStage { get; set; }

	/// <summary>累计经验（= 阵容折算 + BonusExp），每次 Prepare 重算，只用于日志/排查。</summary>
	public uint TotalExp { get; set; }

	/// <summary>
	/// 玩法获得的额外经验（远征结算 SanctuaryStageEndNotify 等）。
	/// 和阵容折算分开存：阵容那部分是每次重算的，改 run\sanctuary.json 的 exp_scale 立刻生效；
	/// 只有这里才是「攒下来」的，不会被重算冲掉。
	/// </summary>
	public uint BonusExp { get; set; }

	/// <summary>最后一次按阵容折算的时间（Unix 秒），仅用于日志排查。</summary>
	public uint LastSyncTime { get; set; }

	public void Save()
	{
		Sanctuary.collection.ReplaceOne(this);
	}
}
