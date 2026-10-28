using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 超弦空间（UltraEndless）的分数存档。
///
/// 为什么需要
/// ----------
/// 客户端 5206 ReportSiteFloorReq 每打完一层就上报 (site_id, floor, score)，
/// 但服务端原来只打日志不存 —— 于是
///   ① 5203 下发的 floor_list[].max_score 恒为 0
///      → GetSiteScore = 0 → GetSiteProgress = 0/满分 → 地图上的进度条永远是空的
///   ② 排名列表里自己那行只能塞一个假分数
///
/// 客户端语义（从伪代码核对过）
/// ----------------------------
///   UltraEndlessModule.GetSiteScore(siteID)    = Σ site.floor_list[].max_score
///   UltraEndlessModule.GetSiteMaxScore(siteID) = Σ UltraEndlessFloor[stageId].MaxScore  ← 表里的满分
///   GetSiteProgress = 前者 / 后者
/// 所以协议里的 max_score 必须填「玩家在该层的最高分」，不是表里的满分。
/// 满分由客户端自己查 UltraEndlessFloor 表算，服务端不用管。
///
/// 落盘
/// ----
/// run\data\ultra_endless_state.json，键 = uid。和旧深渊的 abyss_state.json 一个思路：
/// 内存 + 每次变化落盘，不动 SQLite schema。
///
/// 换期（ScheduleId 变化）等于换赛季，分数重来。
/// </summary>
internal sealed class UltraEndlessState
{
	/// <summary>
	/// 默认排期号。run\ultra_endless.json 的 schedule_id 为 0 时用这个值。
	/// 改期时优先改文件，不用重新编译。
	/// </summary>
	public const uint DefaultScheduleId = 1028u;

	/// <summary>
	/// 实际生效的排期号 —— 5203 下发、5206 记分、换期清分**全部**走这一处，
	/// 保证三边永远一致：
	///   ① run\ultra_endless.json 的 schedule_id 非 0 → 用文件值
	///   ② 文件没配（0） → 用 <see cref="DefaultScheduleId"/>
	///
	/// 客户端内部是 (ushort) 截断，合法区间 0..65535，超出查不到 UltraEndlessSchedule 表。
	/// </summary>
	public static uint EffectiveScheduleId
	{
		get
		{
			uint fromFile = UltraTuning.ScheduleId;
			if (fromFile == 0u)
			{
				return DefaultScheduleId;
			}
			if (fromFile > ushort.MaxValue)
			{
				// 客户端 get_ScheduleMeta 读的是 (ushort)*(rsp+0x14)，只留低 16 位。
				// 超出范围会被截断成另一个期号 → UltraEndlessSchedule 查不到 →
				// BattleConfigID=0 → SiteMapName 空 → 地图一个 zone 都不激活。
				// 所以这里挡掉，退回默认期号，并在日志里喊一声。
				Packet.c.Warn($"[Ultra] ultra_endless.json 的 schedule_id={fromFile} 超出 0..65535，"
					+ $"已忽略并使用默认 {DefaultScheduleId}");
				return DefaultScheduleId;
			}
			return fromFile;
		}
	}

	private static readonly ConcurrentDictionary<uint, UltraEndlessState> Store = new();

	private static readonly object FileGate = new();
	private static bool _loaded;

	private static string StatePath
	{
		get { return Path.Combine(Directory.GetCurrentDirectory(), "data", "ultra_endless_state.json"); }
	}

	private sealed class Dto
	{
		public uint ScheduleId { get; set; }

		/// <summary>siteId → floorId → 该层历史最高分</summary>
		public Dictionary<uint, Dictionary<uint, uint>> Sites { get; set; } = new();
	}

	/// <summary>所有读写都在这把锁里。</summary>
	public readonly object Gate = new();

	public uint Uid;
	public uint ScheduleId = DefaultScheduleId;

	/// <summary>siteId → floorId → 该层历史最高分</summary>
	public readonly Dictionary<uint, Dictionary<uint, uint>> BestScores = new();

	public static UltraEndlessState Get(uint uid)
	{
		EnsureLoaded();
		UltraEndlessState state = Store.GetOrAdd(uid, _ => new UltraEndlessState { Uid = uid });
		uint scheduleId = EffectiveScheduleId;
		lock (state.Gate)
		{
			// 换期就把旧分数清掉，免得上个赛季的分带到这个赛季。
			// 判据用 EffectiveScheduleId（文件优先），所以改 ultra_endless.json 的
			// schedule_id 就等于换期，玩家下次进界面/上报时自动清零。
			if (state.ScheduleId != scheduleId)
			{
				Packet.c.Log($"[Ultra] uid={uid} 换期 {state.ScheduleId} -> {scheduleId}，清空旧分数");
				state.ScheduleId = scheduleId;
				state.BestScores.Clear();
			}
		}
		return state;
	}

	// ------------------------------------------------------------------ 查询

	/// <summary>某个 site 的总分 = Σ 各层最高分（和客户端 GetSiteScore 完全一致）</summary>
	public uint SiteScore(uint siteId)
	{
		if (!BestScores.TryGetValue(siteId, out Dictionary<uint, uint>? perFloor))
		{
			return 0u;
		}

		uint sum = 0u;
		foreach (uint score in perFloor.Values)
		{
			sum += score;
		}
		return sum;
	}

	/// <summary>某一层的最高分</summary>
	public uint FloorScore(uint siteId, uint floor)
	{
		if (!BestScores.TryGetValue(siteId, out Dictionary<uint, uint>? perFloor))
		{
			return 0u;
		}
		return perFloor.TryGetValue(floor, out uint score) ? score : 0u;
	}

	/// <summary>
	/// 排名用的 max_stage_score = 所有层里的单层最高分。
	/// 客户端 UltraEndlessPlayer.max_stage_score 就是这个含义（按它降序排名）。
	/// </summary>
	public uint MaxStageScore
	{
		get
		{
			uint max = 0u;
			foreach (Dictionary<uint, uint> perFloor in BestScores.Values)
			{
				foreach (uint score in perFloor.Values)
				{
					if (score > max)
					{
						max = score;
					}
				}
			}
			return max;
		}
	}

	// ------------------------------------------------------------------ 写入

	/// <summary>
	/// 记录一次上报。只有刷出更高的分才覆盖，返回是否刷新了纪录。
	/// 调用方应持有 <see cref="Gate"/>。
	/// </summary>
	public bool Report(uint siteId, uint floor, uint score)
	{
		if (score == 0u)
		{
			// 0 分不覆盖已有纪录，否则一次失败重打会把成绩清零。
			return false;
		}

		if (!BestScores.TryGetValue(siteId, out Dictionary<uint, uint>? perFloor))
		{
			perFloor = new Dictionary<uint, uint>();
			BestScores[siteId] = perFloor;
		}

		if (perFloor.TryGetValue(floor, out uint old) && old >= score)
		{
			return false;
		}

		perFloor[floor] = score;
		return true;
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

				Dictionary<string, Dto>? raw =
					JsonConvert.DeserializeObject<Dictionary<string, Dto>>(File.ReadAllText(StatePath));
				if (raw == null)
				{
					return;
				}

				int count = 0;
				foreach (KeyValuePair<string, Dto> pair in raw)
				{
					if (!uint.TryParse(pair.Key, out uint uid) || pair.Value == null)
					{
						continue;
					}

					UltraEndlessState st = new UltraEndlessState
					{
						Uid = uid,
						ScheduleId = pair.Value.ScheduleId
					};
					if (pair.Value.Sites != null)
					{
						foreach (KeyValuePair<uint, Dictionary<uint, uint>> sitePair in pair.Value.Sites)
						{
							if (sitePair.Value == null)
							{
								continue;
							}
							st.BestScores[sitePair.Key] = new Dictionary<uint, uint>(sitePair.Value);
						}
					}
					Store[uid] = st;
					count++;
				}

				Packet.c.Log($"[Ultra] 已从 data\\ultra_endless_state.json 恢复 {count} 条超弦空间分数");
			}
			catch (Exception ex)
			{
				Packet.c.Warn("[Ultra] 读取 ultra_endless_state.json 失败，忽略：" + ex.Message);
			}
		}
	}

	/// <summary>
	/// 把当前所有 uid 的分数写回 data\ultra_endless_state.json。
	///
	/// 注意：**不要在 lock(state.Gate) 里调用**（Persist 自己拿 FileGate）。
	/// 所以这里只做无锁读取，字典拷贝失败就退化成空表，宁可丢一条也不抛异常。
	/// </summary>
	public static void Persist()
	{
		EnsureLoaded();

		lock (FileGate)
		{
			try
			{
				Dictionary<string, Dto> raw = new();

				foreach (KeyValuePair<uint, UltraEndlessState> kv in Store)
				{
					UltraEndlessState s = kv.Value;
					Dictionary<uint, Dictionary<uint, uint>> sites = new();
					try
					{
						foreach (KeyValuePair<uint, Dictionary<uint, uint>> sitePair in s.BestScores)
						{
							sites[sitePair.Key] = new Dictionary<uint, uint>(sitePair.Value);
						}
					}
					catch (Exception)
					{
						sites = new Dictionary<uint, Dictionary<uint, uint>>();
					}

					raw[kv.Key.ToString()] = new Dto
					{
						ScheduleId = s.ScheduleId,
						Sites = sites
					};
				}

				string dir = Path.GetDirectoryName(StatePath) ?? ".";
				Directory.CreateDirectory(dir);
				File.WriteAllText(StatePath, JsonConvert.SerializeObject(raw, Formatting.Indented));
			}
			catch (Exception ex)
			{
				Packet.c.Warn("[Ultra] 写 ultra_endless_state.json 失败：" + ex.Message);
			}
		}
	}
}
