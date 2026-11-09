using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json;
using UltraEndlessSite = Common.Utils.ExcelReader.UltraEndlessSite;
using UltraEndlessFloor = Common.Utils.ExcelReader.UltraEndlessFloor;

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
			if (UltraEndlessCycle.Enabled) return UltraEndlessCycle.Current.ScheduleId;
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
			if(!UltraEndlessValidation.Schedule(fromFile)){Packet.c.Warn($"[Ultra] 排期 {fromFile} 缺少有效地图元数据，使用默认期号。");return DefaultScheduleId;}
			return fromFile;
		}
	}

	private static readonly ConcurrentDictionary<uint, UltraEndlessState> Store = new();

	private static readonly object FileGate = new();
	private static bool _loaded;
	private static volatile bool _saveNeeded;

	private static string StatePath
	{
		get { return Path.Combine(Directory.GetCurrentDirectory(), "data", "ultra_endless_state.json"); }
	}

	private sealed class Dto
	{
		public uint ScheduleId { get; set; }

		/// <summary>siteId → floorId → 该层历史最高分</summary>
		public Dictionary<uint, Dictionary<uint, uint>> Sites { get; set; } = new();
		public Dictionary<uint, Dictionary<uint, LineupBriefInfo>> Lineups { get; set; } = new();
		public long PeriodKey { get; set; }
        public long LastSettledPeriodKey { get; set; }
        public bool LegacyCalendarCarry { get; set; }
		public int[] PeriodSites { get; set; }
		public uint[] OpponentScores { get; set; }
		public bool CupsInitialized { get; set; }
		public uint CupNum { get; set; }
		public uint BufferCup { get; set; }
		public bool RedBufferGiven { get; set; }
		public bool Participated { get; set; }
		public uint ScoreUpdatedAt { get; set; }
		public UltraEndlessSettleInfo LastSettlement { get; set; }
	}

	/// <summary>所有读写都在这把锁里。</summary>
	public readonly object Gate = new();

	public uint Uid;
	public uint ScheduleId = DefaultScheduleId;
	public long PeriodKey;
    public long LastSettledPeriodKey;
    public bool LegacyCalendarCarry;
	public int[] PeriodSites = Array.Empty<int>();
	public uint[] OpponentScores = Array.Empty<uint>();
	public bool CupsInitialized;
	public uint CupNum;
	public uint BufferCup;
	public bool RedBufferGiven;
	public bool Participated;
	public uint ScoreUpdatedAt;
	public UltraEndlessSettleInfo LastSettlement = new();

	/// <summary>siteId → floorId → 该层历史最高分</summary>
	public readonly Dictionary<uint, Dictionary<uint, uint>> BestScores = new();
	public readonly Dictionary<uint, Dictionary<uint, LineupBriefInfo>> BestLineups = new();

	public static UltraEndlessState Get(uint uid)
	{
		EnsureLoaded();
		UltraEndlessState state = Store.GetOrAdd(uid, _ => new UltraEndlessState { Uid = uid });
		if (UltraEndlessCycle.Enabled)
		{
			state.Advance(UltraEndlessCycle.Current);
            if (_saveNeeded) Persist();
			return state;
		}
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
				state.BestLineups.Clear();
			}
		}
		return state;
	}

	public uint ScheduledScore()
	{
		lock (Gate)
		{
			var all = BaseExcelReader<UltraEndlessSite, UltraEndlessSiteExcel>.GetInstance().All;
			var floors = BaseExcelReader<UltraEndlessFloor, UltraEndlessFloorExcel>.GetInstance();
			long total = 0;
			foreach (var site in all.Where(s => PeriodSites.Contains(s.SiteId)))
				foreach (var floor in floors.GetFloorDatasFromStageId(site.StageId))
					if (BestScores.TryGetValue((uint)site.SiteId, out var values) && values.TryGetValue((uint)floor.FloorId, out uint value))
						total += value;
			return (uint)Math.Min(uint.MaxValue, total);
		}
	}
	private void Adopt(UltraEndlessCycle.Period period, bool clear)
	{
		PeriodKey = period.Key;
		ScheduleId = period.ScheduleId;
		PeriodSites = period.Sites;
		OpponentScores = UltraEndlessCycle.OpponentScores(period);
		if (clear)
		{
			BestScores.Clear();
			BestLineups.Clear();
			Participated = false;
			ScoreUpdatedAt = 0;
		}
	}
	private void Advance(UltraEndlessCycle.Period current)
	{
		bool changed = false;
		lock (Gate)
		{
            // Old installations used Thursday starts. Carry the same round's
            // progress to Friday; never clear it or settle an extra fake round.
            if (PeriodKey > 0)
            {
                var saved = DateTimeOffset.FromUnixTimeSeconds(PeriodKey).ToOffset(TimeSpan.FromHours(8));
                if (saved.DayOfWeek == DayOfWeek.Thursday && saved.TimeOfDay == TimeSpan.FromHours(15))
                {
                    PeriodKey += 86400;
                    LegacyCalendarCarry = true;
                    changed = true;
                }
            }
            if (PeriodKey > current.Key)
            {
                if (!LegacyCalendarCarry || PeriodKey != current.NextBegin)
                    throw new InvalidOperationException("系统时间早于已保存的深渊期次，未回退存档。");
                _saveNeeded = true;
                return;
            }
            if (LegacyCalendarCarry) { LegacyCalendarCarry = false; changed = true; }
			if (!CupsInitialized)
			{
				var user = User.collection.All().FirstOrDefault(u => u.Uid == Uid);
				uint group = user?.AbyssGroupLevel ?? 8u;
				var groupReader = BaseExcelReader<UltraEndlessGroup, UltraEndlessGroupExcel>.GetInstance();
				if (groupReader.ByLevel((int)group) == null) group = 8;
				CupNum = UltraEndlessCycle.InitialCups(group);
				BufferCup = group >= 8 ? 280u : 180u;
				RedBufferGiven = group >= 8;
				CupsInitialized = true;
				LastSettlement.BufferCup = BufferCup;
				LastSettlement.CupNum = CupNum;
				changed = true;
			}
			if (PeriodKey == 0)
			{
				// First enable imports the current progress without retroactive penalties.
				Adopt(current, false);
				Participated = BestScores.Any(s => s.Value.Count > 0);
				if (Participated) ScoreUpdatedAt = (uint)current.Key + 1;
                // Joining for the first time in a closed gap does not invent
                // a prior missed round or remove cups before the first entry.
                else if (UltraEndlessCycle.Clock().ToUnixTimeSeconds() >= current.Close)
                    LastSettledPeriodKey = current.Key;
				changed = true;
			}
			while (PeriodKey < current.Key)
			{
				var previous = UltraEndlessCycle.FromKey(PeriodKey);
				SettlePeriod(previous);
				Adopt(UltraEndlessCycle.FromKey(previous.NextBegin), true);
				changed = true;
			}
            if (UltraEndlessCycle.Clock().ToUnixTimeSeconds() >= current.Close && LastSettledPeriodKey != PeriodKey)
            {
                SettlePeriod(current);
                changed = true;
            }
			if (!PeriodSites.SequenceEqual(current.Sites))
			{
				// A deliberate GM weather override changes the active sites, not cups.
				PeriodSites = current.Sites;
				OpponentScores = UltraEndlessCycle.OpponentScores(current);
				changed = true;
			}
		}
		if (changed) _saveNeeded = true;
		if (changed || _saveNeeded) Persist();
	}

    // Called under Gate. Persist the marker with the cup result so reads in
    // the settlement gap cannot repeatedly change cups or award medals.
    private void SettlePeriod(UltraEndlessCycle.Period previous)
    {
        if (LastSettledPeriodKey == PeriodKey) return;
        var frozen = AbyssSettlementRewards.Frozen(Uid, PeriodKey);
        if (frozen != null)
        {
            LastSettlement = frozen; CupNum = frozen.CupNum; BufferCup = frozen.BufferCup;
            RedBufferGiven |= UltraEndlessCycle.GroupForCups(CupNum) >= 8;
            LastSettledPeriodKey = PeriodKey;
            return;
        }
        uint savedCups = CupNum, savedBuffer = BufferCup;
        bool savedRed = RedBufferGiven;
        var savedInfo = LastSettlement;
        try
        {

				uint score = ScheduledScore();
				uint group = UltraEndlessCycle.GroupForCups(CupNum);
				var rows = UltraEndlessCycle.Rows(Uid, score, ScoreUpdatedAt, PeriodKey, OpponentScores);
				int rank = Participated ? rows.FindIndex(r => r.IsSelf) + 1 : rows.Count;
				int delta = UltraEndlessCycle.CupDelta(group, rows.Count, rank, Participated);

				uint before = CupNum, bufferBefore = BufferCup;
				if (delta < 0)
				{
					uint loss = (uint)-delta;
					uint protectedLoss = Math.Min(BufferCup, loss);
					BufferCup -= protectedLoss;
					loss -= protectedLoss;
					CupNum = CupNum > loss ? CupNum - loss : 0;
				}
				else CupNum = (uint)Math.Min(uint.MaxValue, (long)CupNum + delta);
				if (!RedBufferGiven && UltraEndlessCycle.GroupForCups(CupNum) >= 8)
				{
					BufferCup += 100;
					RedBufferGiven = true;
				}
				LastSettlement = new UltraEndlessSettleInfo
				{
					ScheduleId = ScheduleId, GroupLevel = group, MaxStageScore = score,
					GroupMemberNum = (uint)rows.Count, Rank = (uint)rank,
					CupNumBefore = before, CupNumAfterScheduleSettle = CupNum,
					CupNumBeforeSeasonSettle = CupNum, CupNumAfterSeasonSettle = CupNum,
					CupNum = CupNum, BufferCupBeforeScheduleSettle = bufferBefore, BufferCup = BufferCup
				};
            AbyssSettlementRewards.Queue(Uid, PeriodKey, previous.Close, LastSettlement, Participated);
            LastSettledPeriodKey = PeriodKey;
        }
        catch
        {
            CupNum = savedCups; BufferCup = savedBuffer; RedBufferGiven = savedRed; LastSettlement = savedInfo;
            throw;
        }
    }

	/// <summary>
	/// GM：清掉某玩家的超弦空间全部分数（内存 + 落盘）。
	///
	/// 下次 <see cref="Get"/> 会新建一个空 state（ScheduleId 取当前生效期号），
	/// 客户端再拉 5203 就是「所有站点 0 分」。
	/// 返回是否真的删掉了一条。
	/// </summary>
	public static bool Reset(uint uid)
	{
		EnsureLoaded();
		if (UltraEndlessCycle.Enabled)
		{
			var current = Get(uid);
			lock (current.Gate)
			{
				current.BestScores.Clear();
				current.BestLineups.Clear();
				current.Participated = false;
				current.ScoreUpdatedAt = 0;
			}
			Persist();
			return true;
		}
		bool removed = Store.TryRemove(uid, out _);
		Persist();
		Packet.c.Log($"[Ultra] GM 清零 uid={uid} -> {(removed ? "已删除" : "本来就没有")}");
		return removed;
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
	/// <summary>Attach only to the matching best score. An equal score can backfill an old save.</summary>
	public bool RecordLineup(uint siteId, uint floor, uint score, LineupBriefInfo lineup, bool newBest)
	{
		if (score == 0 || lineup == null || lineup.AvatarInfoLists.Count == 0 || FloorScore(siteId, floor) != score)
			return false;
		if (!BestLineups.TryGetValue(siteId, out var floors)) BestLineups[siteId] = floors = new();
		if (!newBest && floors.ContainsKey(floor)) return false;
		floors[floor] = UltraEndlessLineup.Copy(lineup);
		return true;
	}

	/// <summary>Show the boss best-score formation when the boss has a score.</summary>
	public LineupBriefInfo RankLineup()
	{
		lock (Gate)
		{
			var sites = UltraEndlessCycle.Enabled ? PeriodSites : UltraEndlessSites.Active().Select(s => s.SiteId).ToArray();
            var siteTable = BaseExcelReader<UltraEndlessSite, UltraEndlessSiteExcel>.GetInstance();
            foreach (int site in sites.Reverse())
            {
                if (siteTable.FromId(site)?.SiteNodeName != "Area3" ||
                    !BestScores.TryGetValue((uint)site, out var bossScores))
                    continue;

                var bossFloor = bossScores.Where(entry => entry.Value > 0)
                    .OrderByDescending(entry => entry.Key).FirstOrDefault();
                if (bossFloor.Value == 0)
                    continue;

                return BestLineups.TryGetValue((uint)site, out var bossLineups) &&
                    bossLineups.TryGetValue(bossFloor.Key, out var bossLineup) &&
                    bossLineup != null && bossLineup.AvatarInfoLists.Count > 0
                    ? UltraEndlessLineup.Copy(bossLineup) : null;
            }
			foreach (int site in sites.Reverse())
			{
				if (!BestScores.TryGetValue((uint)site, out var scores)) continue;
				if (!BestLineups.TryGetValue((uint)site, out var lineups)) continue;
				foreach (var floor in scores.Where(p => p.Value > 0).OrderByDescending(p => p.Key))
					if (lineups.TryGetValue(floor.Key, out var lineup) && lineup != null && lineup.AvatarInfoLists.Count > 0)
						return UltraEndlessLineup.Copy(lineup);
			}
			return null;
		}
	}

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
		// A higher score without a valid report must not retain the previous score's team.
		if (BestLineups.TryGetValue(siteId, out var lineups)) lineups.Remove(floor);
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
						ScheduleId = pair.Value.ScheduleId,
						PeriodKey = pair.Value.PeriodKey, LastSettledPeriodKey = pair.Value.LastSettledPeriodKey, LegacyCalendarCarry = pair.Value.LegacyCalendarCarry, PeriodSites = pair.Value.PeriodSites ?? Array.Empty<int>(),
						OpponentScores = pair.Value.OpponentScores ?? Array.Empty<uint>(),
						CupsInitialized = pair.Value.CupsInitialized, CupNum = pair.Value.CupNum,
						BufferCup = pair.Value.BufferCup, RedBufferGiven = pair.Value.RedBufferGiven,
						Participated = pair.Value.Participated, ScoreUpdatedAt = pair.Value.ScoreUpdatedAt,
						LastSettlement = pair.Value.LastSettlement ?? new UltraEndlessSettleInfo()
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
					if (pair.Value.Lineups != null)
						foreach (var site in pair.Value.Lineups)
							if (site.Value != null) st.BestLineups[site.Key] = site.Value
								.Where(p => p.Value != null).ToDictionary(p => p.Key, p => UltraEndlessLineup.Copy(p.Value));
					count++;
				}

				Packet.c.Log($"[Ultra] 已从 data\\ultra_endless_state.json 恢复 {count} 条超弦空间分数");
			}
			catch (Exception ex)
			{
				Packet.c.Warn("[Ultra] 读取 ultra_endless_state.json 失败，忽略：" + ex.Message);
				if (UltraEndlessCycle.Enabled)
				{
					_loaded = false;
					throw new InvalidDataException("自动排期存档读取失败，已停止处理以免覆盖奖杯和进度。", ex);
				}
			}
		}
	}

	/// <summary>
	/// 把当前所有 uid 的分数写回 data\ultra_endless_state.json。
	///
	/// 注意：**不要在 lock(state.Gate) 里调用**（Persist 自己拿 FileGate）。
	/// 逐个锁定用户快照，临时文件写完并刷盘后原子替换；杯数和期次随进度一起保存。
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
					lock (s.Gate)
					{
						raw[kv.Key.ToString()] = new Dto
						{
							ScheduleId = s.ScheduleId,
							Sites = s.BestScores.ToDictionary(p => p.Key, p => new Dictionary<uint, uint>(p.Value)),
							Lineups = s.BestLineups.ToDictionary(p => p.Key, p => p.Value.ToDictionary(f => f.Key, f => UltraEndlessLineup.Copy(f.Value))),
							PeriodKey = s.PeriodKey, LastSettledPeriodKey = s.LastSettledPeriodKey, LegacyCalendarCarry = s.LegacyCalendarCarry, PeriodSites = s.PeriodSites, OpponentScores = s.OpponentScores,
							CupsInitialized = s.CupsInitialized, CupNum = s.CupNum, BufferCup = s.BufferCup,
							RedBufferGiven = s.RedBufferGiven, Participated = s.Participated,
							ScoreUpdatedAt = s.ScoreUpdatedAt, LastSettlement = s.LastSettlement
						};
					}
				}

				string dir = Path.GetDirectoryName(StatePath) ?? ".";
				Directory.CreateDirectory(dir);
				string temp = StatePath + ".tmp";
				using (var writer = new StreamWriter(temp, false, new System.Text.UTF8Encoding(false)))
				{
					writer.Write(JsonConvert.SerializeObject(raw, Formatting.Indented));
					writer.Flush();
					((FileStream)writer.BaseStream).Flush(true);
				}
				File.Move(temp, StatePath, true);
				_saveNeeded = false;
			}
			catch (Exception ex)
			{
				Packet.c.Warn("[Ultra] 写 ultra_endless_state.json 失败：" + ex.Message);
				_saveNeeded = true;
				if (UltraEndlessCycle.Enabled) throw;
			}
		}
	}
}


