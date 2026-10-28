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
/// 旧深渊（迪拉克之海 / 量子奇点）的运行时进度。
///
/// 2026-10-26 改成「内存 + 落盘」
/// ----------------------------
/// 原来纯内存，注释里写着「重启后退回第一组，客户端仍能正常玩」。
/// 实际影响比预期大：改一次服务端就要重启，一重启分数/已完成的组全丢，
/// 用户看到的就是「打完的分没了，退出后也没保存」。
/// 所以现在每次分数/进度变化都写一份 run\data\abyss_state.json，
/// 服务端启动后第一次访问时读回来。不改 SQLite schema，也不动 v1 存档。
///
/// 键用 (uid, type)：同一账号可以在迪拉克和量子之间切换，
/// 两套 SplitBattles 完全不同，不能共用一个光标。
/// </summary>
internal sealed class OwEndlessState
{
	private static readonly ConcurrentDictionary<(uint Uid, uint Type), OwEndlessState> Store = new();

	// ---- 落盘 ----
	private static readonly object FileGate = new();
	private static bool _loaded;

	private static string StatePath
	{
		get { return Path.Combine(Directory.GetCurrentDirectory(), "data", "abyss_state.json"); }
	}

	private sealed class Dto
	{
		public uint BattleConfigId { get; set; }

		public int SplitIndex { get; set; }

		public uint CurGroupId { get; set; }

		public uint MonsterProgress { get; set; }

		public uint Score { get; set; }

		public uint MaxScore { get; set; }

		public uint GroupLevel { get; set; }

		public List<uint> FinishedGroups { get; set; } = new List<uint>();
	}

	/// <summary>所有读写都在这把锁里，避免 3424/3448 并发把光标推乱。</summary>
	public readonly object Gate = new();

	public uint Type;

	/// <summary>本周期用的战斗配置；变了就说明档位/模式换了，进度重置。</summary>
	public uint BattleConfigId;

	/// <summary>当前打到 SplitBattles 的第几场。</summary>
	public int SplitIndex;

	public uint CurGroupId;

	/// <summary>当前组内进度，0..10000（客户端 GetEndlessProgress 用这个比例）。</summary>
	public uint MonsterProgress;

	public uint Score;
	public uint MaxScore;
	public uint LastBattleId;
	public uint GroupLevel;

	public readonly List<uint> FinishedGroups = new();

	public static OwEndlessState Get(uint uid, uint type)
	{
		EnsureLoaded();
		return Store.GetOrAdd((uid, type), _ => new OwEndlessState { Type = type });
	}

	public static OwEndlessState? Find(uint uid, uint type)
	{
		EnsureLoaded();
		return Store.TryGetValue((uid, type), out OwEndlessState? state) ? state : null;
	}

	// ------------------------------------------------------------------ 落盘

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
					if (!uint.TryParse(uidPair.Key, out uint uid) || uidPair.Value == null)
					{
						continue;
					}
					foreach (KeyValuePair<string, Dto> typePair in uidPair.Value)
					{
						if (!uint.TryParse(typePair.Key, out uint type))
						{
							continue;
						}
						Dto d = typePair.Value;
						if (d == null)
						{
							continue;
						}

						OwEndlessState st = new OwEndlessState
						{
							Type = type,
							BattleConfigId = d.BattleConfigId,
							SplitIndex = d.SplitIndex,
							CurGroupId = d.CurGroupId,
							MonsterProgress = d.MonsterProgress,
							Score = d.Score,
							MaxScore = d.MaxScore,
							GroupLevel = d.GroupLevel
						};
						if (d.FinishedGroups != null)
						{
							st.FinishedGroups.AddRange(d.FinishedGroups);
						}
						Store[(uid, type)] = st;
						count++;
					}
				}

				Packet.c.Log($"[Abyss] 已从 data\\abyss_state.json 恢复 {count} 条旧深渊进度");
			}
			catch (Exception ex)
			{
				Packet.c.Warn("[Abyss] 读取 abyss_state.json 失败，忽略：" + ex.Message);
			}
		}
	}

	/// <summary>
	/// 把当前所有 (uid, type) 的进度写回 data\abyss_state.json。
	///
	/// 注意：**不要在 lock(state.Gate) 里调用**。Persist 自己拿 FileGate，
	/// 若反过来又去抢 state.Gate，会和「持 state.Gate 等 FileGate」的调用方互锁。
	/// 所以这里只做无锁读取：uint 字段读撕裂不影响正确性，
	/// List 拷贝失败就退化成空表，宁可丢一条也不抛异常。
	/// </summary>
	public static void Persist()
	{
		EnsureLoaded();

		lock (FileGate)
		{
			try
			{
				Dictionary<string, Dictionary<string, Dto>> raw = new();

				foreach (KeyValuePair<(uint Uid, uint Type), OwEndlessState> kv in Store)
				{
					string uidKey = kv.Key.Uid.ToString();
					if (!raw.TryGetValue(uidKey, out Dictionary<string, Dto>? per))
					{
						per = new Dictionary<string, Dto>();
						raw[uidKey] = per;
					}

					OwEndlessState s = kv.Value;
					uint[] finished;
					try
					{
						finished = s.FinishedGroups.ToArray();
					}
					catch (Exception)
					{
						finished = Array.Empty<uint>();
					}

					per[kv.Key.Type.ToString()] = new Dto
					{
						BattleConfigId = s.BattleConfigId,
						SplitIndex = s.SplitIndex,
						CurGroupId = s.CurGroupId,
						MonsterProgress = s.MonsterProgress,
						Score = s.Score,
						MaxScore = s.MaxScore,
						GroupLevel = s.GroupLevel,
						FinishedGroups = new List<uint>(finished)
					};
				}

				string? dir = Path.GetDirectoryName(StatePath);
				if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
				{
					Directory.CreateDirectory(dir);
				}

				File.WriteAllText(StatePath,
					JsonConvert.SerializeObject(raw, Formatting.Indented));
			}
			catch (Exception ex)
			{
				Packet.c.Warn("[Abyss] 写入 abyss_state.json 失败：" + ex.Message);
			}
		}
	}

	// ------------------------------------------------------------------ 逻辑

	/// <summary>
	/// 取状态并保证它已经对齐到一套真实的战斗配置。
	/// 用于 3424 / 3448 这类「客户端可能没先发 3419」的包 —— 服务端重启后
	/// 内存状态是空的，不能因为 BattleConfigId=0 就返回空序列。
	/// </summary>
	public static (OwEndlessState State, OWEndlessSplitBattle[] Splits) Ensure(uint uid, int type)
	{
		OWEndlessGroupExcel group = OwEndlessConfig.ResolveGroup(type, (int)OwTuning.GroupLevel);
		uint hard = OwEndlessConfig.BattleHardLevel(type, (uint)group.GroupHardLevel);
		uint cfgId = OwEndlessConfig.ResolveRequestedBattleConfigId(type, hard);
		OWEndlessSplitBattle[] splits = OwEndlessConfig.SplitBattles(type, cfgId);
		OwEndlessState state = Get(uid, (uint)type);
		lock (state.Gate)
		{
			state.Align(type, cfgId, splits);
			state.GroupLevel = (uint)group.GroupLevel;
		}
		return (state, splits);
	}

	/// <summary>
	/// 对齐到当前战斗配置。
	/// 配置没变时只修光标越界；配置变了（换档/换模式）则整条进度重来，
	/// 因为不同 AreaScoreConfig 的 groupScoreId 完全不同。
	///
	/// 2026-10-26 去掉「光标越界就清空 FinishedGroups」：现在打完一层 BOSS 会把
	/// 该层所有组一次性标完成，光标很容易顶到末尾，再清空就等于把分数和完成态全丢了
	/// —— 正是用户反馈的「打完 boss 没保存」。全部打完时只把 CurGroupId 置 0。
	/// </summary>
	public void Align(int type, uint battleConfigId, OWEndlessSplitBattle[] splits)
	{
		if (BattleConfigId != battleConfigId)
		{
			BattleConfigId = battleConfigId;
			SplitIndex = 0;
			CurGroupId = 0u;
			MonsterProgress = 0u;
			Score = 0u;
			MaxScore = 0u;
			FinishedGroups.Clear();
		}

		if (splits.Length == 0)
		{
			SplitIndex = 0;
			CurGroupId = 0u;
			return;
		}

		if (SplitIndex < 0)
		{
			SplitIndex = 0;
		}
		if (SplitIndex > splits.Length)
		{
			SplitIndex = splits.Length;
		}

		// 光标只用来推「下一场」，全部打完时置 0（客户端拿到 cur_monster_group=0
		// 自己会处理），但已完成集合与分数必须保留。
		CurGroupId = SplitIndex < splits.Length
			? OwEndlessConfig.ReportedGroupId(type, battleConfigId, splits[SplitIndex])
			: 0u;

		NormalizeFinished(type, battleConfigId);
	}

	/// <summary>
	/// 规范化「已完成集合」，并按表重算分数。每次 Align 都会跑，幂等。
	///
	/// 做两件事：
	///   ① 补全残缺的 BOSS 完成态。旧逻辑里 BOSS 组 id 对不上 SplitBattles，
	///      只往 FinishedGroups 里塞了个裸的 131，却没有该层的小怪组。
	///      这里只要发现某层 BOSS 在集合里，就把「本层及以下」全部组补齐。
	///   ② Score 一律由 FinishedGroups 重算。旧逻辑 `Score += gain` 是无条件的，
	///      同一个 BOSS 打两次会加两次分（实测 1100 -> 2800 -> 4500 里多算了 1700）。
	///      重算能自愈这类虚高，也让「分数」永远等于「已通关组的分值和」。
	/// </summary>
	public void NormalizeFinished(int type, uint battleConfigId)
	{
		int[]? boss = OwEndlessConfig.AreaScoreConfig(type, battleConfigId)?.BossScoreConfig;
		if (boss != null)
		{
			for (int layer = boss.Length - 1; layer >= 0; layer--)
			{
				if (!FinishedGroups.Contains((uint)boss[layer]))
				{
					continue;
				}
				foreach (uint gid in OwEndlessConfig.GroupsUpToLayer(type, battleConfigId, layer))
				{
					if (!FinishedGroups.Contains(gid))
					{
						FinishedGroups.Add(gid);
					}
				}
			}
		}

		uint sum = 0u;
		foreach (uint gid in FinishedGroups)
		{
			sum += OwEndlessConfig.GroupScore(gid);
		}
		if (sum != Score)
		{
			Packet.c.Log($"[Abyss] 分数按已完成组重算：{Score} -> {sum}"
				+ $"（{FinishedGroups.Count} 组已完成）");
			Score = sum;
		}
		if (Score > MaxScore)
		{
			MaxScore = Score;
		}
	}

	/// <summary>
	/// 构造 monster_group_list —— 这是客户端判断「哪些组已打完」的**唯一**依据。
	///
	/// 反汇编实证（2026-10-26，bodies_merged.tsv）：
	///   OWEndLessBattleLevelInfo::RefreshFinishGroupList (0x1815C4A00)
	///       finishGroupIDList = { mg.group_id | mg.progress > 9999 }
	///   OWEndLessLayerInfo::SetGroupState (0x181C77B60)
	///       mg.isFinished = finishGroupIDList.Contains(mg.groupScoreId)
	///
	/// 两个结论：
	///   ① 3425 里的 finish_monster_group_list，客户端**根本不读**；
	///   ② 只有 monster_group_list 里 progress &gt; 9999 的组才算完成。
	///
	/// 而且 BOSS 场的组 id 必须用 BossScoreConfig 的（131/132/133/134），
	/// 因为客户端 `layerInfo.bossGroup.groupScoreId` 就是这个 id，
	/// 用 SplitBattles 里的 3605 去标它永远对不上 —— BOSS 层就永远不算通关。
	/// </summary>
	public static List<OpenworldEndlessMonsterGroup> BuildGroupList(
		int type, uint battleConfigId, uint curGroupId, uint curProgress,
		IReadOnlyCollection<uint>? finished)
	{
		List<OpenworldEndlessMonsterGroup> list = new List<OpenworldEndlessMonsterGroup>();
		HashSet<uint> done = finished == null
			? new HashSet<uint>()
			: new HashSet<uint>(finished);
		uint cur = curProgress > 10000u ? 10000u : curProgress;

		OWEndlessSplitBattle[] splits =
			OwEndlessConfig.AreaScoreConfig(type, battleConfigId)?.SplitBattles
			?? Array.Empty<OWEndlessSplitBattle>();

		// BOSS 场在客户端那边有两个 id：
		//   · layerInfo.bossGroup.groupScoreId  = BossScoreConfig[layer]（131 等）→ 决定 BOSS 是否通关
		//   · 该层 monsterGroupList 里那一项     = SplitBattles 的占位 id（3605 等）→ 决定「本区域全清」
		// 两个都要标成已完成，否则 SetupView 里的 allBattleFinished 永远是 false。
		// 注意只影响下发，不影响计分（分数只认 GroupsUpToLayer 的计分 id）。
		HashSet<uint> effective = new HashSet<uint>(done);
		foreach (OWEndlessSplitBattle split in splits)
		{
			if (split.IsBoss
				&& done.Contains(OwEndlessConfig.ReportedGroupId(type, battleConfigId, split)))
			{
				effective.Add((uint)split.GroupScoreId);
			}
		}

		HashSet<uint> emitted = new HashSet<uint>();

		void Emit(uint gid)
		{
			if (gid == 0u || !emitted.Add(gid))
			{
				return;
			}
			uint progress = effective.Contains(gid) ? 10000u : (gid == curGroupId ? cur : 0u);
			list.Add(new OpenworldEndlessMonsterGroup
			{
				GroupId = gid,
				Progress = progress,
				OldProgress = progress
			});
		}

		foreach (OWEndlessSplitBattle split in splits)
		{
			Emit(OwEndlessConfig.ReportedGroupId(type, battleConfigId, split));
			if (split.IsBoss)
			{
				Emit((uint)split.GroupScoreId);
			}
		}

		// 当前组不在表里（刚换过配置 / 光标异常）时补一条，
		// 否则地图上当前场次的进度条会是空的。
		Emit(curGroupId);

		return list;
	}

	/// <summary>
	/// 结算一场，返回本次实际获得的分数。
	///
	/// 迪拉克计分规则（表数据实证，2026-10-26）：
	///   · 普通组：只加自己那份；**重复上报已完成的组不再加分**
	///     （原来无条件 Score += gain，同一个 BOSS 打两次会加两次分）。
	///   · BOSS 组（group_id ∈ BossScoreConfig）：把「本层及以下」所有组一次性
	///     标记完成，凡是没计过分的都补上 —— 这就是「通关 BOSS 层获取本层及
	///     之下的全部分数」。层号取自 BossScoreConfig 的下标。
	/// </summary>
	public uint Advance(int type, uint battleConfigId, OWEndlessSplitBattle[] splits,
		uint groupId, uint extraScore, out bool isBoss, out int bossLayer,
		out uint[] newlyFinished, out uint nextGroup)
	{
		List<uint> added = new List<uint>();
		uint gain = 0u;

		bossLayer = OwEndlessConfig.LayerOfBoss(type, battleConfigId, groupId);
		isBoss = bossLayer >= 0;

		if (isBoss)
		{
			foreach (uint gid in OwEndlessConfig.GroupsUpToLayer(type, battleConfigId, bossLayer))
			{
				if (FinishedGroups.Contains(gid))
				{
					continue;
				}
				FinishedGroups.Add(gid);
				added.Add(gid);
				gain += OwEndlessConfig.GroupScore(gid);
			}
			gain += extraScore;

			if (added.Count == 0)
			{
				Packet.c.Log($"[Abyss] BOSS {groupId}(layer {bossLayer}) 已完成过，"
					+ "本次不加分（防重复刷分）");
			}
		}
		else if (FinishedGroups.Contains(groupId))
		{
			Packet.c.Log($"[Abyss] 组 {groupId} 已完成过，本次不加分（防重复刷分）");
		}
		else
		{
			FinishedGroups.Add(groupId);
			added.Add(groupId);
			gain = OwEndlessConfig.GroupScore(groupId) + extraScore;
		}

		Score += gain;
		if (Score > MaxScore)
		{
			MaxScore = Score;
		}
		newlyFinished = added.ToArray();

		// ---- 光标：推到「本次完成的最后一个组」之后，且只前进不回退 ----
		int cursor = SplitIndex;
		foreach (uint gid in added)
		{
			int ix = OwEndlessConfig.IndexOf(splits, gid);
			if (ix >= 0 && ix + 1 > cursor)
			{
				cursor = ix + 1;
			}
		}
		if (cursor > splits.Length)
		{
			cursor = splits.Length;
		}
		SplitIndex = cursor;
		MonsterProgress = 0u;

		// ---- 下一场 = 第一个还没完成的组（BOSS 项用 BossScoreConfig 的 id 判断）----
		nextGroup = 0u;
		for (int i = 0; i < splits.Length; i++)
		{
			uint gid = OwEndlessConfig.ReportedGroupId(type, battleConfigId, splits[i]);
			if (!FinishedGroups.Contains(gid))
			{
				nextGroup = gid;
				break;
			}
		}
		CurGroupId = nextGroup;
		return gain;
	}
}
