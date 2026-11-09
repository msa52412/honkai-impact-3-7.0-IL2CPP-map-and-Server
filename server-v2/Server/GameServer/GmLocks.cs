using System;
using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 单机版的「数值锁定」：体力固定、补给卡固定。
///
/// 为什么做成"每次下发前刷一遍"而不是拦截消耗点：
/// 消耗散落在 StageBegin / WeekdayGameplay / UseMaterial 等一堆地方，
/// 逐个改容易漏。而客户端显示的永远是服务端下发的值，只要在
/// GetMainDataRsp / GetEquipmentDataRsp 组装前把值改回去，
/// 玩家看到的就是"锁住"的效果，下一次同步又会补满。
///
/// 注意这是**真改存档对象**（不是只改回包），否则背包一刷新就露馅。
/// </summary>
internal static class GmLocks
{
	/// <summary>体力锁定的值。设成负数表示不锁。</summary>
	internal const int StaminaLock = 167;

	/// <summary>补给卡锁定到的数量。</summary>
	internal const uint TicketLock = 999u;

	/// <summary>重铸/词缀类材料的锁定数量。</summary>
	internal const uint MaterialLock = 9999u;

	/// <summary>
	/// 补给卡 id 段。MaterialData 里 1101~1119 的 ItemType 全是 <c>gacha_item</c>，
	/// 也就是扩充/精准/人偶/服装/家园这些补给卡。1101 开始 19 个刚好覆盖。
	/// </summary>
	/// <summary>
	/// 补给卡 id 段。MaterialData 里 1101~1115 的 ItemType 全是 <c>gacha_item</c>，
	/// 也就是扩充/精准/人偶/服装/家园这些补给卡。再往上（1116+）表里没有，别碰。
	/// </summary>
	private static readonly uint[] TicketIds =
		Enumerable.Range(1101, 15).Select(x => (uint)x).ToArray();

	internal static bool IsTicket(uint id) => TicketIds.Contains(id);

	/// <summary>体力是否处于锁定状态（锁定后既不恢复也不消耗）。</summary>
	internal static bool StaminaLocked => StaminaLock >= 0;

	/// <summary>把体力顶回锁定值。</summary>
	internal static void Stamina(Player p)
	{
		if (p?.User == null || StaminaLock < 0)
		{
			return;
		}
		if (p.User.Stamina != StaminaLock)
		{
			p.User.Stamina = StaminaLock;
		}
	}

	/// <summary>
	/// 扣体力。锁定状态下直接吞掉这次扣减，调用方不用判断。
	/// 用它替代 <c>p.User.Stamina -= cost</c>。
	/// </summary>
	internal static void CostStamina(Player p, long cost)
	{
		if (StaminaLocked || p?.User == null)
		{
			return;
		}
		if (p.User.Stamina < cost)
		{
			throw new InvalidOperationException("stamina underflow");
		}
		p.User.Stamina -= (int)cost;
	}

	/// <summary>体力是否够。锁定状态下永远够。</summary>
	internal static bool HasStamina(Player p, long cost) =>
		StaminaLocked || (p?.User != null && p.User.Stamina >= cost);

	/// <summary>
	/// 固定数量的材料：id → 目标数量。每次背包同步都精确顶到这个值。
	///
	/// 名字对应关系来自 MaterialData.displayTitle 的 hash 反查 TextMap
	/// （Table 里存的不是字符串，是 hash）。
	///
	/// 这一批是从**圣痕词缀系统**的源码 + 配置表反推出来的全部消耗品：
	///   `StigmataRefining.Promote/Refine` 只会读这几张表的 cost 字段 ——
	///   `AffixTreeNode.refineCost_0to1 / _1 / _1to2 / _2 / _lock1 / _core`
	///   和 `SpecialAffixData.refineCost`，没有别的入口。
	/// 单次最大消耗是普通晶体核心 300 个，9999 足够覆盖任何一次操作。
	/// </summary>
	private static readonly Dictionary<uint, uint> FixedMaterials = new Dictionary<uint, uint>
	{
		// ---- 基础材料 ----
		{ 8101u, 9999u },   // Normal Crystal Core        普通晶体核心（词缀刷新主材料）
		{ 8001u, 9999u },   // Wafer Dataflow             晶片数据流（核心词缀）
		{ 8003u, 9999u },   // Crystal Expansion Tank     晶体扩容罐（开第二槽）
		{ 8009u, 9999u },   // Wafer Stabilizer           晶片稳定器（锁定词缀）

		// ---- 指定词缀重铸装置（SpecialAffixData）----
		{ 8601u, 9999u },   // Reroll Device: ATK         攻击词缀重铸装置
		{ 8602u, 9999u },   // Reroll Device: Support     支援词缀重铸装置
		{ 8305u, 9999u },   // Reroll Device: BIO ATK     生物·攻击
		{ 8306u, 9999u },   // Reroll Device: PSY ATK     异能·攻击
		{ 8307u, 9999u },   // Reroll Device: MECH ATK    机械·攻击
		{ 8308u, 9999u },   // Reroll Device: QUA ATK     量子·攻击
		{ 8311u, 9999u },   // Reroll Device: IMG ATK     虚数·攻击
		{ 121013u, 9999u }, // ATK Reroll Device: BIO
		{ 121014u, 9999u }, // ATK Reroll Device: PSY
		{ 121015u, 9999u }, // ATK Reroll Device: MECH
		{ 121016u, 9999u }, // ATK Reroll Device: QUA
		{ 121048u, 9999u }, // ATK Reroll Device: IMG
		{ 121050u, 9999u }, // Energy Device              能量装置
		// 备用：这几个没被 SpecialAffixData 引用，但客户端可能是另一版映射，
		// 一起锁上省得再来一遍（都是同一族重铸装置，多给不影响别的系统）。
		{ 8309u, 9999u },   // Reroll Device: ATK Option Type A
		{ 8310u, 9999u },   // Reroll Device: ATK Option Type B
		{ 121049u, 9999u }, // Pure Blue Crystal
		{ 121051u, 9999u }  // SP Cost Reducer
	};

	/// <summary>
	/// 整段锁定的 id 区间。8xxx 是「晶体核心 / 晶片数据流 / 各类重铸装置」，
	/// 1210xx 是 7.x 新编号的重铸装置。**客户端和服务端对「能量专攻」那一行
	/// 到底指向哪个 id 有出入**，与其一个个对表，不如整段锁死 ——
	/// 这一段里全是同一族的词缀养成材料，多给不会影响别的系统。
	/// </summary>
	private static readonly (uint From, uint To)[] FixedRanges =
	{
		(8000u, 8999u),
		(121000u, 121099u)
	};

	/// <summary>
	/// 所有材料锁定一起刷：
	///   · 补给卡 1101~1115 → 只补不减，至少 999
	///   · <see cref="FixedMaterials"/> → 精确顶到目标值
	/// 背包里本来没有的直接开一条，否则锁定看不见。
	/// </summary>
	internal static void Materials(Player p) => Materials(p?.Equipment);

	/// <summary>
	/// 只要拿到 <see cref="EquipmentScheme"/> 就能刷 —— 词缀系统那几个方法
	/// 手里只有 equipment，没有 Player，扣完材料直接调这个把数量顶回去，
	/// 客户端就不会看到「刚刷新完晶体核心少了一截」。
	/// </summary>
	internal static void Materials(EquipmentScheme? equipment)
	{
		Material[]? list = equipment?.MaterialList;
		if (equipment == null || list == null)
		{
			return;
		}

		List<Material> items = list.ToList();
		bool changed = false;

		// 补给卡：玩家自己刷到更多就保持更多，只补不减
		foreach (uint id in TicketIds)
		{
			int i = items.FindIndex(x => x.Id == id);
			if (i < 0)
			{
				items.Add(new Material { Id = id, Num = TicketLock });
				changed = true;
			}
			else if (items[i].Num < TicketLock)
			{
				items[i].Num = TicketLock;
				changed = true;
			}
		}

		// 固定数量材料：不论多了少了都拉回目标值
		foreach (KeyValuePair<uint, uint> kv in FixedMaterials)
		{
			int i = items.FindIndex(x => x.Id == kv.Key);
			if (i < 0)
			{
				items.Add(new Material { Id = kv.Key, Num = kv.Value });
				changed = true;
			}
			else if (items[i].Num != kv.Value)
			{
				items[i].Num = kv.Value;
				changed = true;
			}
		}

		// 整段锁定：只对已经在背包里的条目生效（空条目由 MaterialData 全表补）
		foreach ((uint from, uint to) in FixedRanges)
		{
			foreach (Material m in items)
			{
				if (m.Id >= from && m.Id <= to && m.Num != MaterialLock)
				{
					m.Num = MaterialLock;
					changed = true;
				}
			}
		}

		if (!changed)
		{
			return;
		}

		equipment.MaterialList = items.ToArray();
		try { equipment.Save(); }
		catch { /* 存档写失败不影响本次回包，下次同步再补 */ }
	}
}
