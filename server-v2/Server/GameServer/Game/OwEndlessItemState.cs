using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using Common;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 迪拉克之海 / 量子奇点 的「游戏内道具」状态：背包、生效效果、已布置的陷阱。
///
/// 为什么要服务端存
/// ----------------
/// 客户端反汇编（UserAssembly.dll, 7.0）已经确认：
///   OWEndlessModule.RefreshMyItemList(0x18189BD50)      清空 _itemList(+0x80)，只保留 num != 0 的项
///   OWEndlessModule.RefreshEffectItemList(0x18189BB30)  清空 _effectItemList(+0x88)，按 num 次重复加入 item_id
///   OWEndlessModule.OnUseOpenworldEndlessItemRsp(0x18189B7F0)
///       retcode==0 时**只**刷新 effect_item_list，背包不动
///   OWEndlessModule.OnOpenworldEndlessItemUpdateNotify(0x18189AC70)
///       拿 notify.item_list 去 RefreshMyItemList —— 3421 是背包的全量刷新通道
///
/// 所以：背包 = 服务端唯一真相，3423 之后必须再推一包 3421，否则「用掉一个」界面上数量不变。
///
/// 落盘：run\data\abyss_items.json（按 uid / type 两层，和 abyss_state.json 同构）
///   文件可以直接手改（改初始道具、清陷阱都行），服务端只在启动时读一次。
/// </summary>
internal sealed class OwEndlessItemState
{
	private static readonly ConcurrentDictionary<(uint Uid, uint Type), OwEndlessItemState> Store = new();

	private static readonly object FileGate = new();
	private static bool _loaded;

	private static string StatePath
	{
		get { return Path.Combine(Directory.GetCurrentDirectory(), "data", "abyss_items.json"); }
	}

	/// <summary>一个已布置的陷阱。客户端 3427 的 battle_list[].trap_list 就是它。</summary>
	internal sealed class Trap
	{
		public uint BattleId;
		public uint ItemId;
		public uint UniqueId;
		public uint SetUid;
	}

	private sealed class TrapDto
	{
		public uint BattleId { get; set; }

		public uint ItemId { get; set; }

		public uint UniqueId { get; set; }

		public uint SetUid { get; set; }
	}

	private sealed class Dto
	{
		public bool Seeded { get; set; }

		public Dictionary<string, uint> Bag { get; set; } = new Dictionary<string, uint>();

		public Dictionary<string, uint> Effects { get; set; } = new Dictionary<string, uint>();

		public List<TrapDto> Traps { get; set; } = new List<TrapDto>();

		/// <summary>最近一次进入的地图（3426 area_id），陷阱要落在这张图的 battle 上。</summary>
		public uint LastAreaId { get; set; }

		/// <summary>最近一次开始的战斗（3438 battle_id），关卡内用道具时陷阱就落在这里。</summary>
		public uint LastBattleId { get; set; }
	}

	/// <summary>读写锁。所有对 Bag / Effects / Traps 的访问都必须持锁。</summary>
	public readonly object Gate = new();

	public uint Type;

	/// <summary>背包：item_id -> 数量（数量 0 的项直接删掉，别下发）。</summary>
	public readonly Dictionary<uint, uint> Bag = new Dictionary<uint, uint>();

	/// <summary>生效中的效果：item_id -> 层数（客户端按 num 次展开）。</summary>
	public readonly Dictionary<uint, uint> Effects = new Dictionary<uint, uint>();

	/// <summary>
	/// 效果的过期时间（UTC 毫秒）：item_id -> expire_ms。
	///
	/// 为什么要过期
	/// ------------
	/// 客户端 LevelDesignManager.CheckItemEffectOverMaxAmount(0x1819C49B0) 是拿
	/// EffectItemList 里「同 type 的条目数」和 OWEndlessItemEffect.effectAmount 比的：
	///   type 2 药水 effectAmount=1、type 5 钥匙 1、type 6 陷阱 buff 1
	/// 如果服务端把效果永久留在列表里，客户端第二次点同类道具会自己弹「已达上限」，
	/// 请求根本发不出来。真实服务器里这些是带持续时间的 buff，到期就不在列表里了。
	/// 这里用 TTL 近似（默认 60 秒 = 道具描述里的 CD），读列表时先清一遍过期的。
	/// </summary>
	public readonly Dictionary<uint, long> EffectExpire = new Dictionary<uint, long>();

	/// <summary>效果默认存活时间（毫秒）。道具描述里 CD 是 60 秒。</summary>
	public const long EffectTtlMs = 60000L;

	/// <summary>已布置的陷阱（含 battle_id，3427 按 battle 过滤）。BattleId=0 表示「还不知道落在哪」。</summary>
	public readonly List<Trap> Traps = new List<Trap>();

	public uint LastAreaId;

	/// <summary>最近一次开始的战斗（3438）。关卡内用道具时陷阱优先落在这里。</summary>
	public uint LastBattleId;

	/// <summary>是否已经发过初始道具（落盘后不再重复发，除非背包被用空）。</summary>
	public bool Seeded;

	private static uint _trapSeq;

	public static OwEndlessItemState Get(uint uid, uint type)
	{
		EnsureLoaded();
		return Store.GetOrAdd((uid, type), delegate
		{
			OwEndlessItemState st = new OwEndlessItemState();
			st.Type = type;
			return st;
		});
	}

	public static OwEndlessItemState? Find(uint uid, uint type)
	{
		EnsureLoaded();
		OwEndlessItemState? found;
		return Store.TryGetValue((uid, type), out found) ? found : null;
	}

	// ------------------------------------------------------------------ 读取

	private static void EnsureLoaded()
	{
		if (_loaded)
		{
			return;
		}

		lock (FileGate)
		{
			if (_loaded)
			{
				return;
			}
			_loaded = true;

			try
			{
				if (!File.Exists(StatePath))
				{
					return;
				}

				Dictionary<string, Dictionary<string, Dto>>? raw =
					JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dto>>>(
						File.ReadAllText(StatePath));
				if (raw == null)
				{
					return;
				}

				int count = 0;
				foreach (KeyValuePair<string, Dictionary<string, Dto>> uidPair in raw)
				{
					uint uid;
					if (!uint.TryParse(uidPair.Key, out uid) || uidPair.Value == null)
					{
						continue;
					}

					foreach (KeyValuePair<string, Dto> typePair in uidPair.Value)
					{
						uint type;
						if (!uint.TryParse(typePair.Key, out type))
						{
							continue;
						}

						Dto d = typePair.Value;
						if (d == null)
						{
							continue;
						}

						OwEndlessItemState st = new OwEndlessItemState();
						st.Type = type;
						st.LastAreaId = d.LastAreaId;
						st.LastBattleId = d.LastBattleId;
						st.Seeded = d.Seeded;

						if (d.Bag != null)
						{
							foreach (KeyValuePair<string, uint> kv in d.Bag)
							{
								uint itemId;
								if (uint.TryParse(kv.Key, out itemId) && kv.Value != 0u)
								{
									st.Bag[itemId] = kv.Value;
								}
							}
						}

						if (d.Effects != null)
						{
							foreach (KeyValuePair<string, uint> kv2 in d.Effects)
							{
								uint itemId2;
								if (uint.TryParse(kv2.Key, out itemId2) && kv2.Value != 0u)
								{
									st.Effects[itemId2] = kv2.Value;
								}
							}
						}

						if (d.Traps != null)
						{
							foreach (TrapDto t in d.Traps)
							{
								if (t != null)
								{
									st.Traps.Add(new Trap
									{
										BattleId = t.BattleId,
										ItemId = t.ItemId,
										UniqueId = t.UniqueId,
										SetUid = t.SetUid
									});
									if (t.UniqueId > _trapSeq)
									{
										_trapSeq = t.UniqueId;
									}
								}
							}
						}

						if (d.Seeded && st.Bag.Count == 0 && st.Effects.Count == 0 && st.Traps.Count == 0)
						{
							// 玩家把东西用光了：Seeded 置回 false，下次进界面重新发，私服不做经济。
							st.Seed();
						}
						else if (!d.Seeded)
						{
							st.Seed();
						}

						Store[(uid, type)] = st;
						count++;
					}
				}

				Packet.c.Log("[Abyss] 已从 data\\abyss_items.json 恢复 " + count + " 份背包");
			}
			catch (Exception ex)
			{
				Packet.c.Warn("[Abyss] 读取 abyss_items.json 失败，忽略：" + ex.Message);
			}
		}
	}

	/// <summary>
	/// 全量写回 data\abyss_items.json。
	///
	/// 约定：**不要在外面持 state.Gate 的时候调**（它自己拿 FileGate，反序会死锁）。
	/// </summary>
	public static void Persist()
	{
		EnsureLoaded();

		lock (FileGate)
		{
			try
			{
				Dictionary<string, Dictionary<string, Dto>> raw = new Dictionary<string, Dictionary<string, Dto>>();

				foreach (KeyValuePair<(uint Uid, uint Type), OwEndlessItemState> kv in Store)
				{
					string uidKey = kv.Key.Uid.ToString();
					Dictionary<string, Dto> per;
					if (!raw.TryGetValue(uidKey, out per))
					{
						per = new Dictionary<string, Dto>();
						raw[uidKey] = per;
					}

					OwEndlessItemState s = kv.Value;
					Dto d = new Dto();
					d.Seeded = s.Seeded;
					d.LastAreaId = s.LastAreaId;
					d.LastBattleId = s.LastBattleId;

					foreach (KeyValuePair<uint, uint> b in s.Bag)
					{
						if (b.Value != 0u)
						{
							d.Bag[b.Key.ToString()] = b.Value;
						}
					}

					foreach (KeyValuePair<uint, uint> e in s.Effects)
					{
						if (e.Value != 0u)
						{
							d.Effects[e.Key.ToString()] = e.Value;
						}
					}

					foreach (Trap t in s.Traps)
					{
						d.Traps.Add(new TrapDto
						{
							BattleId = t.BattleId,
							ItemId = t.ItemId,
							UniqueId = t.UniqueId,
							SetUid = t.SetUid
						});
					}

					per[kv.Key.Type.ToString()] = d;
				}

				string? dir = Path.GetDirectoryName(StatePath);
				if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
				{
					Directory.CreateDirectory(dir);
				}

				File.WriteAllText(StatePath, JsonConvert.SerializeObject(raw, Formatting.Indented));
			}
			catch (Exception ex)
			{
				Packet.c.Warn("[Abyss] 写入 abyss_items.json 失败：" + ex.Message);
			}
		}
	}

	// ------------------------------------------------------------------ 初始化

	/// <summary>
	/// 第一次进界面发一套初始道具。
	///
	/// 官方服的来源是 OWEndless 段位奖励 / 商店，私服没有那套经济，所以直接按
	/// OWEndlessItemEffect.bagAmount 的上限给（陷阱 3、其余 3，钥匙 1）。
	/// 想改数量：直接编辑 run\data\abyss_items.json，或改这里的 SeedAmount。
	/// </summary>
	public void Seed()
	{
		Bag.Clear();
		Effects.Clear();
		EffectExpire.Clear();
		Traps.Clear();
		Seeded = true;

		OWEndlessItem reader = OWEndlessItem.GetInstance();

		foreach (OWEndlessItemExcel row in reader.All)
		{
			uint itemId = (uint)row.ID;
			uint amount = SeedAmount(row);
			if (amount != 0u)
			{
				Bag[itemId] = amount;
			}
		}

		Packet.c.Log($"[Abyss] 初始化道具背包 type={Type} items={Bag.Count}");
	}

	private static uint SeedAmount(OWEndlessItemExcel row)
	{
		switch (row.Type)
		{
		case (int)OpenworldEndlessItemType.OpenworldEndlessItemInvasion:
			return 3u;   // 干扰器
		case (int)OpenworldEndlessItemType.OpenworldEndlessItemBuff:
			return 3u;   // 药水
		case (int)OpenworldEndlessItemType.OpenworldEndlessItemTrap:
			return 3u;   // 陷阱：bagAmount 就是 3
		case (int)OpenworldEndlessItemType.OpenworldEndlessItemCounter:
			return 2u;   // 净化
		case (int)OpenworldEndlessItemType.OpenworldEndlessItemConsume:
			return 1u;   // 钥匙
		default:
			// type 6 TRAP_BUFF 是陷阱的「效果项」，不进背包。
			return 0u;
		}
	}

	// ------------------------------------------------------------------ 背包操作（调用方须持 Gate）

	public uint Count(uint itemId)
	{
		uint n;
		return Bag.TryGetValue(itemId, out n) ? n : 0u;
	}

	/// <summary>加道具，按 OWEndlessItemEffect.bagAmount 截断。返回实际加进去的数量。</summary>
	public uint Add(uint itemId, uint num)
	{
		if (num == 0u)
		{
			return 0u;
		}

		OWEndlessItemExcel? meta = OWEndlessItem.GetInstance().ById(itemId);
		if (meta == null)
		{
			return 0u;
		}

		uint cap = (uint)Math.Max(1, OWEndlessItemEffect.GetInstance().BagAmount(meta.Type));
		uint have = Count(itemId);
		uint room = have >= cap ? 0u : cap - have;
		uint add = Math.Min(num, room);
		if (add != 0u)
		{
			Bag[itemId] = have + add;
		}
		return add;
	}

	/// <summary>扣道具。数量不够时返回 false 且不动背包。</summary>
	public bool Take(uint itemId, uint num)
	{
		uint have = Count(itemId);
		if (have < num)
		{
			return false;
		}

		if (have == num)
		{
			Bag.Remove(itemId);
		}
		else
		{
			Bag[itemId] = have - num;
		}
		return true;
	}

	public uint NextTrapUniqueId()
	{
		_trapSeq++;
		if (_trapSeq == 0u)
		{
			_trapSeq = 1u;
		}
		return _trapSeq;
	}

	// ------------------------------------------------------------------ 效果（调用方须持 Gate）

	private static long NowMs()
	{
		return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
	}

	/// <summary>清掉已经过期的效果。所有「读 EffectItemList」的地方都先调一次。</summary>
	public void PurgeEffects()
	{
		if (EffectExpire.Count == 0)
		{
			return;
		}

		long now = NowMs();
		List<uint> drop = new List<uint>();
		foreach (KeyValuePair<uint, long> kv in EffectExpire)
		{
			if (kv.Value <= now)
			{
				drop.Add(kv.Key);
			}
		}

		foreach (uint id in drop)
		{
			EffectExpire.Remove(id);
			Effects.Remove(id);
		}
	}

	/// <summary>写入 / 刷新一个效果（层数 + 到期时间）。</summary>
	public void SetEffect(uint itemId, uint level, long ttlMs)
	{
		Effects[itemId] = level;
		EffectExpire[itemId] = NowMs() + Math.Max(1000L, ttlMs);
	}
}
