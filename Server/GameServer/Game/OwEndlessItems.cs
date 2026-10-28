using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 迪拉克之海 / 量子奇点 道具系统的公共逻辑（3420 / 3422 / 3426 / 3428 共用）。
///
/// 协议口径（全部来自客户端反汇编，见 OwEndlessItemState 的注释）：
///
///   3420 GetOpenworldEndlessDataRsp
///        item_list              -> RefreshMyItemListByMainRsp -> RefreshMyItemList（背包全量）
///        effect_item_list       -> 主界面**不用**（只有 3423 / 3427 会刷新效果列表）
///        trap_add_item_list     -> 新增陷阱的提示列表（我们不做提示，留空）
///        trap_item_show_id_list -> 地图上要显示的陷阱道具 ID
///
///   3421 OpenworldEndlessItemUpdateNotify
///        item_list              -> RefreshMyItemList（唯一能让「背包数量」变化的通道）
///
///   3422/3423 UseOpenworldEndlessItemRsp
///        retcode != 0           -> ExcuteUseItemCallback(false, ...) 弹错误
///        retcode == 0           -> 只拿 effect_item_list 去 RefreshEffectItemList
///        battle_id              -> 陷阱落在哪个 battle（客户端不传，服务端决定）
///
///   3426/3427 GetOpenworldEndlessStageInnerDataRsp
///        battle_list[].trap_list  -> RefreshTrapList（关卡内陷阱的唯一来源）
///        effect_item_list         -> RefreshEffectItemListByInnerDataRsp（关卡内生效的增益）
///
/// 所以：**用掉一个道具之后必须补推 3421**，否则客户端界面上数量不变。
/// </summary>
internal static class OwEndlessItems
{
	/// <summary>取（必要时初始化）某个账号在某个深渊模式下的道具状态。</summary>
	public static OwEndlessItemState Get(uint uid, uint endlessType)
	{
		OwEndlessItemState st = OwEndlessItemState.Get(uid, endlessType);
		bool seeded = false;
		lock (st.Gate)
		{
			if (!st.Seeded)
			{
				st.Seed();
				seeded = true;
			}
		}
		if (seeded)
		{
			// 第一次发完就落盘，这样 run\data\abyss_items.json 立刻可见、可手改。
			OwEndlessItemState.Persist();
		}
		return st;
	}

	public static bool IsTrap(uint itemId)
	{
		OWEndlessItemExcel? meta = OWEndlessItem.GetInstance().ById(itemId);
		return meta != null && meta.Type == (int)OpenworldEndlessItemType.OpenworldEndlessItemTrap;
	}

	// ------------------------------------------------------------------ 3420

	public static void FillMainRsp(OwEndlessItemState st, GetOpenworldEndlessDataRsp rsp)
	{
		lock (st.Gate)
		{
			st.PurgeEffects();

			foreach (KeyValuePair<uint, uint> kv in st.Bag)
			{
				if (kv.Value != 0u)
				{
					rsp.ItemLists.Add(new OpenworldEndlessItem { ItemId = kv.Key, Num = kv.Value });
				}
			}

			foreach (KeyValuePair<uint, uint> kv2 in st.Effects)
			{
				if (kv2.Value != 0u)
				{
					rsp.EffectItemLists.Add(new OpenworldEndlessItem { ItemId = kv2.Key, Num = kv2.Value });
				}
			}

			// 地图概览上要标出来的陷阱图标：已经布置过的陷阱道具（去重）
			rsp.TrapItemShowIdLists = st.Traps
				.Select((OwEndlessItemState.Trap t) => t.ItemId)
				.Distinct()
				.ToArray();
		}
	}

	// ------------------------------------------------------------------ 3421

	public static OpenworldEndlessItemUpdateNotify BuildNotify(OwEndlessItemState st)
	{
		OpenworldEndlessItemUpdateNotify ntf = new OpenworldEndlessItemUpdateNotify();
		lock (st.Gate)
		{
			st.PurgeEffects();
			foreach (KeyValuePair<uint, uint> kv in st.Bag)
			{
				if (kv.Value != 0u)
				{
					ntf.ItemLists.Add(new OpenworldEndlessItem { ItemId = kv.Key, Num = kv.Value });
				}
			}
		}
		return ntf;
	}

	// ------------------------------------------------------------------ 3426/3427

	/// <summary>
	/// 把关卡内数据补齐：生效效果 + 每个 battle 的陷阱。
	/// 必须在 handler 已经把 battle_list 填好之后调用。
	/// </summary>
	public static void FillInnerRsp(OwEndlessItemState st, GetOpenworldEndlessStageInnerDataRsp rsp, uint areaId)
	{
		lock (st.Gate)
		{
			if (areaId != 0u)
			{
				st.LastAreaId = areaId;
			}

			// 关卡内不会收到「随时更新」的效果推送，所以每次 3427 都先把过期的清掉，
			// 否则过期的 type-6 陷阱 buff 会让客户端一直判定「同类效果已达上限」。
			st.PurgeEffects();

			// 在总览图上用掉的陷阱还不知道落点（BattleId=0）：落到本次进入区域的空位里。
			// 这是私服里唯一能保证「用掉的陷阱一定能被看到」的做法——客户端 3422 根本
			// 不带 battle_id（proto 里没有这个字段），服务端只能自己挑。
			AssignPendingTraps(st, rsp);

			foreach (KeyValuePair<uint, uint> kv in st.Effects)
			{
				if (kv.Value != 0u)
				{
					rsp.EffectItemLists.Add(new OpenworldEndlessItem { ItemId = kv.Key, Num = kv.Value });
				}
			}

			foreach (OpenworldEndlessBattle battle in rsp.BattleLists)
			{
				foreach (OwEndlessItemState.Trap trap in st.Traps)
				{
					if (trap.BattleId != battle.BattleId)
					{
						continue;
					}
					battle.TrapLists.Add(new OpenworldEndlessTrap
					{
						ItemId = trap.ItemId,
						BattleId = trap.BattleId,
						UniqueId = trap.UniqueId,
						SetUid = trap.SetUid
					});
				}
			}
		}
	}

	/// <summary>
	/// 把 BattleId=0 的悬空陷阱分配到本次区域里还有空位的 battle 上。
	/// 空位上限取 OWEndlessBattleArea.trapMaxNum（普通区 2~3，BOSS 区 0）。
	/// </summary>
	private static void AssignPendingTraps(OwEndlessItemState st, GetOpenworldEndlessStageInnerDataRsp rsp)
	{
		List<OwEndlessItemState.Trap> pending = st.Traps
			.Where((OwEndlessItemState.Trap t) => t.BattleId == 0u)
			.ToList();
		if (pending.Count == 0)
		{
			return;
		}

		foreach (OwEndlessItemState.Trap trap in pending)
		{
			foreach (OpenworldEndlessBattle battle in rsp.BattleLists)
			{
				OWEndlessBattleAreaExcel? row = OWEndlessBattleArea.GetInstance()
					.All.FirstOrDefault((OWEndlessBattleAreaExcel r) => r.BattleID == (int)battle.BattleId);
				if (row == null || row.TrapMaxNum <= 0)
				{
					continue;
				}

				int used = st.Traps.Count((OwEndlessItemState.Trap t) => t.BattleId == (uint)row.BattleID);
				if (used >= row.TrapMaxNum)
				{
					continue;
				}

				trap.BattleId = (uint)row.BattleID;
				Packet.c.Log($"[Abyss] 悬空陷阱 unique={trap.UniqueId} item={trap.ItemId} "
					+ $"落到 battle={row.BattleID}({row.BattleArea} area={row.AreaId}) 已放 {used + 1}/{row.TrapMaxNum}");
				break;
			}
		}
	}

	// ------------------------------------------------------------------ 3422/3423

	public sealed class UseResult
	{
		public UseOpenworldEndlessItemRsp.Retcode Code = UseOpenworldEndlessItemRsp.Retcode.Fail;

		public uint ItemId;

		public uint BattleId;

		public uint TargetId;

		public uint NewInvasionId;
	}

	/// <summary>
	/// 使用道具。所有判定都在 state.Gate 里做，出口统一由调用方补 3421。
	/// </summary>
	public static UseResult Use(uint uid, uint endlessType, uint itemId, uint num, uint targetUid)
	{
		UseResult res = new UseResult { ItemId = itemId, TargetId = targetUid };
		OWEndlessItemExcel? meta = OWEndlessItem.GetInstance().ById(itemId);
		if (meta == null)
		{
			Packet.c.Warn($"[Abyss] UseItem item={itemId} 不在 OWEndlessItem 表里");
			res.Code = UseOpenworldEndlessItemRsp.Retcode.Fail;
			return res;
		}

		OwEndlessItemState st = Get(uid, endlessType);
		uint take = num == 0u ? 1u : num;

		lock (st.Gate)
		{
			if (st.Count(itemId) < take)
			{
				Packet.c.Log($"[Abyss] UseItem item={itemId} 库存不足 have={st.Count(itemId)} need={take}");
				res.Code = UseOpenworldEndlessItemRsp.Retcode.ItemLack;
				return res;
			}

			switch (meta.Type)
			{
			case (int)OpenworldEndlessItemType.OpenworldEndlessItemTrap:
			{
				OWEndlessBattleAreaExcel? battle = PickTrapBattle(st);
				int place;
				if (battle == null)
				{
					// 还没有任何「当前区域」上下文（例如刚重启、在总览图上直接使用）：
					// 先记一个悬空陷阱，等下一次 3426/3427 进入区域时由 AssignPendingTraps 落点。
					st.Take(itemId, take);
					for (uint i = 0u; i < take; i++)
					{
						st.Traps.Add(new OwEndlessItemState.Trap
						{
							BattleId = 0u,
							ItemId = itemId,
							UniqueId = st.NextTrapUniqueId(),
							SetUid = uid
						});
					}
					AddRelateBuff(st, meta);
					res.Code = UseOpenworldEndlessItemRsp.Retcode.Succ;
					res.BattleId = 0u;
					Packet.c.Log($"[Abyss] UseItem 陷阱 item={itemId} 暂无区域上下文，先挂起 {take} 个，"
						+ $"等进入区域后再落点 剩 {st.Count(itemId)}");
					return res;
				}

				int max = battle.TrapMaxNum;
				int used = st.Traps.Count((OwEndlessItemState.Trap t) => t.BattleId == (uint)battle.BattleID);
				uint canPlace = (uint)Math.Max(0, max - used);
				uint placeNum = Math.Min(take, canPlace);
				if (placeNum == 0u)
				{
					res.Code = UseOpenworldEndlessItemRsp.Retcode.TrapFull;
					return res;
				}

				place = (int)placeNum;
				st.Take(itemId, placeNum);
				for (uint i = 0u; i < placeNum; i++)
				{
					st.Traps.Add(new OwEndlessItemState.Trap
					{
						BattleId = (uint)battle.BattleID,
						ItemId = itemId,
						UniqueId = st.NextTrapUniqueId(),
						SetUid = uid
					});
				}

				// 陷阱的 RelateBuff（150031..150038，type=6）就是「使用后自己身上的效果」：
				// 道具描述写的是 Heal Self（停止流血 + 每秒 3% HP / 3 SP，10 秒），
				// 客户端 CheckItemEffectOverMaxAmount 对 type=3 也确实是改查 type=6 的条目数，
				// 说明真实服务器会把它写进 effect_item_list —— 不写就没有 buff 图标、技能也不生效。
				AddRelateBuff(st, meta);

				res.Code = UseOpenworldEndlessItemRsp.Retcode.Succ;
				res.BattleId = (uint)battle.BattleID;
				Packet.c.Log($"[Abyss] UseItem 陷阱 item={itemId} -> battle={battle.BattleID}"
					+ $"({battle.BattleArea} area={battle.AreaId}) 已放 {used + place}/{max} 剩 {st.Count(itemId)}"
					+ $" buff={meta.RelateBuff}");
				return res;
			}
			case (int)OpenworldEndlessItemType.OpenworldEndlessItemInvasion:
			{
				if (targetUid == 0u)
				{
					res.Code = UseOpenworldEndlessItemRsp.Retcode.TargetError;
					return res;
				}

				// 私服没有跨玩家对战，只扣道具并回一个 Succ：
				// 客户端拿到 new_invasion_id=0 就不会进入入侵流程。
				st.Take(itemId, take);
				res.Code = UseOpenworldEndlessItemRsp.Retcode.Succ;
				res.NewInvasionId = 0u;
				Packet.c.Log($"[Abyss] UseItem 干扰器 item={itemId} target={targetUid} 私服无 PVP，仅扣道具");
				return res;
			}
			default:
			{
				// type 2 药水 / type 4 净化 / type 5 钥匙 / type 6 陷阱效果：
				// 上限按 CheckItemEffectOverMaxAmount 的口径（同 type 的条目数 < effectAmount），
				// 满了就替换——客户端自己也会先判一次，走到这里说明还有名额。
				int limit = Math.Max(1, OWEndlessItemEffect.GetInstance().EffectAmount(meta.Type));
				RemoveEffectsOfType(st, meta.Type);
				st.Take(itemId, take);
				st.SetEffect(itemId, (uint)Math.Min((int)take, limit), OwEndlessItemState.EffectTtlMs);
				res.Code = UseOpenworldEndlessItemRsp.Retcode.Succ;
				Packet.c.Log($"[Abyss] UseItem item={itemId} type={meta.Type} num={take} "
					+ $"effect上限={limit} 剩={st.Count(itemId)}");
				return res;
			}
			}
		}
	}

	/// <summary>
	/// 把陷阱的 RelateBuff 写进效果列表（type=6 只保留一个：新的替换旧的）。
	/// meta.RelateBuff=0 的陷阱没有关联 buff，直接跳过。
	/// </summary>
	private static void AddRelateBuff(OwEndlessItemState st, OWEndlessItemExcel meta)
	{
		if (meta.RelateBuff <= 0)
		{
			return;
		}

		uint buffId = (uint)meta.RelateBuff;
		OWEndlessItemExcel? buffMeta = OWEndlessItem.GetInstance().ById(buffId);
		if (buffMeta == null)
		{
			Packet.c.Warn($"[Abyss] 陷阱 {meta.ID} 的 RelateBuff={buffId} 不在 OWEndlessItem 表里，忽略");
			return;
		}

		RemoveEffectsOfType(st, buffMeta.Type);
		st.SetEffect(buffId, 1u, OwEndlessItemState.EffectTtlMs);
	}

	/// <summary>把 effect_item_list 里同 type 的条目清掉（替换式生效）。</summary>
	private static void RemoveEffectsOfType(OwEndlessItemState st, int type)
	{
		List<uint> drop = new List<uint>();
		foreach (uint id in st.Effects.Keys)
		{
			OWEndlessItemExcel? meta = OWEndlessItem.GetInstance().ById(id);
			if (meta != null && meta.Type == type)
			{
				drop.Add(id);
			}
		}
		foreach (uint id2 in drop)
		{
			st.Effects.Remove(id2);
			st.EffectExpire.Remove(id2);
		}
	}

	/// <summary>
	/// 选一个还能放陷阱的 battle：
	///   优先 3438 记下的当前战斗（关卡内使用就是这里），其次 3426 记住的当前区域，
	///   最后才是任何 trapMaxNum &gt; 0 的区域；完全没有上下文时返回 null（由调用方挂起）。
	///   OWEndlessBattleArea.battleID 全局唯一（3011=1..8, 3012=9..15, ...），
	///   所以直接把它当 3427 的 battle_id 用。
	/// </summary>
	private static OWEndlessBattleAreaExcel? PickTrapBattle(OwEndlessItemState st)
	{
		if (st.LastBattleId == 0u && st.LastAreaId == 0u)
		{
			return null;
		}

		List<OWEndlessBattleAreaExcel> rows = OWEndlessBattleArea.GetInstance().All
			.Where((OWEndlessBattleAreaExcel r) => r.TrapMaxNum > 0)
			.OrderBy((OWEndlessBattleAreaExcel r) => r.BattleID == (int)st.LastBattleId ? 0
				: (r.AreaId == (int)st.LastAreaId ? 1 : 2))
			.ThenBy((OWEndlessBattleAreaExcel r) => r.BattleID)
			.ToList();

		foreach (OWEndlessBattleAreaExcel row in rows)
		{
			int used = st.Traps.Count((OwEndlessItemState.Trap t) => t.BattleId == (uint)row.BattleID);
			if (used < row.TrapMaxNum)
			{
				return row;
			}
		}
		return null;
	}

	// ------------------------------------------------------------------ 3428/3429

	/// <summary>踩到（或触发）陷阱：从状态里移除，回 Succ。</summary>
	public static bool TriggerTrap(uint uid, uint endlessType, uint uniqueId, uint itemId)
	{
		OwEndlessItemState? st = OwEndlessItemState.Find(uid, endlessType);
		if (st == null)
		{
			return true;
		}

		lock (st.Gate)
		{
			int removed = st.Traps.RemoveAll((OwEndlessItemState.Trap t) => t.UniqueId == uniqueId);
			if (removed == 0)
			{
				// 客户端可能因为重连重复上报，找不到也算成功，避免弹网络错误。
				Packet.c.Log($"[Abyss] TriggerTrap unique={uniqueId} item={itemId} 未找到，按已触发处理");
				return true;
			}
			Packet.c.Log($"[Abyss] TriggerTrap unique={uniqueId} item={itemId} 已移除，剩 {st.Traps.Count} 个陷阱");
		}
		return true;
	}
}
