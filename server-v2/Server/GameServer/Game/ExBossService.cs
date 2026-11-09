using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PemukulPaku.GameServer.Game;

// 7.0 protocol/state restoration. Rotations use shipped schedules, not live official schedules.
internal static class ExBossService
{
    internal static Func<DateTimeOffset> Clock = () => DateTimeOffset.UtcNow;
    private static readonly object Gate = new();
    private static readonly Dictionary<string, JArray> Tables = new();
    private static readonly Dictionary<Session, Attempt> Attempts = new();
    private static SqlTable<State> Store => Global.Table<State>("ExBossStates", ("key", "TEXT", s => s.Key, true));
    public sealed class State : IRow
    {
        public long Id { get; set; }
        public long CreatedAt { get; set; }
        public string Key { get; set; }
        public uint Uid { get; set; }
        public long Week { get; set; }
        public uint Rank { get; set; }
        public uint Schedule { get; set; }
        public long Day { get; set; }
        public uint Used { get; set; }
        public Dictionary<uint, Boss> Bosses { get; set; } = new();
        public HashSet<int> Rewards { get; set; } = new();
        public bool Settled { get; set; }
        public long Updated { get; set; }
    }
    public sealed class Boss
    {
        public uint Root { get; set; }
        public uint Current { get; set; }
        public uint Score { get; set; }
        public uint BonusScore { get; set; }
        public uint Wins { get; set; }
        public HashSet<uint> Cleared { get; set; } = new();
        public HashSet<uint> Avatars { get; set; } = new();
        public HashSet<uint> Weapons { get; set; } = new();
        public HashSet<uint> Stigmata { get; set; } = new();
        public uint[] LastTeam { get; set; } = Array.Empty<uint>();
        public string BestTeamBlob { get; set; }
        // Protocol lists are getter-only; SQLite's JSON Replace mode cannot populate them.
        [JsonIgnore]
        public LineupBriefInfo BestTeam
        {
            get { if (BestTeamBlob == null) return null; using var stream = new MemoryStream(Convert.FromBase64String(BestTeamBlob)); return ProtoBuf.Serializer.Deserialize<LineupBriefInfo>(stream); }
            set { if (value == null) { BestTeamBlob = null; return; } using var stream = new MemoryStream(); ProtoBuf.Serializer.Serialize(stream,value); BestTeamBlob = Convert.ToBase64String(stream.ToArray()); }
        }
    }
    private sealed class Attempt
    {
        public long Week;
        public uint BossId;
        public uint Root;
        public bool Training;
        public bool Turbo;
        public uint[] Avatars;
        public uint[] Weapons;
        public uint[] Stigmata;
        public LineupBriefInfo Team;
    }
    private static JArray Table(string name)
    {
        if (!Tables.TryGetValue(name, out var data))
            Tables[name] = data = JArray.Parse(File.ReadAllText(Path.Combine("Resources", "ExcelOutputAsset", name + ".json")));
        return data;
    }
    internal static (long Start, long End, long Next) Bounds(DateTimeOffset now)
    {
        var local = now.ToOffset(TimeSpan.FromHours(8));
        var date = new DateTimeOffset(local.Year, local.Month, local.Day, 4, 0, 0, TimeSpan.FromHours(8));
        int days = ((int)local.DayOfWeek - (int)DayOfWeek.Tuesday + 7) % 7;
        var start = date.AddDays(-days);
        if (local < start) start = start.AddDays(-7);
        // 单机版：不留结算期。官服是「周二 04:00 开、下周一 04:00 关」，
        // 周一那 24 小时客户端显示「结算中」进不去；私服要随时能打，
        // 所以把 End 延到下一期开始前一秒，周期首尾相接、Open 恒为真。
        // 上期奖励仍由 Settle() 按 x.Week + 6 天 自动发，和这里改不改无关。
        long end = start.AddDays(7).ToUnixTimeSeconds() - 1;
        return (start.ToUnixTimeSeconds(), end, start.AddDays(7).ToUnixTimeSeconds());
    }
    private static long Day => (Clock().ToUnixTimeSeconds() + 4 * 3600) / 86400;
    private static uint Rank(Player p) => BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance().ExBossRankFromExp(p.User.Exp);
    private static int Level(Player p) => BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance().CalculateLevel(p.User.Exp).Level;
    private static JToken Config(uint rank) => Table("ExBossConfig").Single(x => (uint)x["ID"] == rank);
    private static JToken Schedule(uint id, uint rank) => Table("ExBossMonsterSchedule").Single(x => (uint)x["ID"] == id && (uint)x["ConfigId"] == rank);
    private static uint ScheduleId(long week)
    {
        var ids = Table("ExBossMonsterSchedule").GroupBy(x => (uint)x["ID"])
            .Where(g => new uint[] {101,102,103,104}.All(r => g.Count(x => (uint)x["ConfigId"] == r) == 1))
            .Select(g => g.Key).OrderBy(x => x).ToArray();
        if (ids.Length == 0) throw new InvalidDataException("No complete ExBoss schedules");
        long anchor = DateTimeOffset.Parse("2026-11-03T04:00:00+08:00").ToUnixTimeSeconds();
        long offset = (week - anchor) / 604800;
        return ids[(int)(((ids.Length - 1 + offset) % ids.Length + ids.Length) % ids.Length)];
    }
    // 单机版：开放该区全部 boss，不再按排期每周只开 3 个。
    // 表里 BossId 的编码是 root*10 + level，level 从 1 起（18121~18126），
    // 排期表 BossIdList 给的就是 root+1，这里沿用同一写法。
    private static uint[] BossRoots(uint rank) =>
        BaseExcelReader<ExBossMonsterData, ExBossMonsterDataExcel>.GetInstance().All
            .Where(x => x.ConfigId == rank)
            .Select(x => (uint)(x.BossId / 10 * 10 + 1))
            .Distinct().OrderBy(x => x).ToArray();

    private static State Get(Player p)
    {
        long week = Bounds(Clock()).Start;
        if (Store.Where(s => s.Uid == p.User.Uid).Any(s => s.Week > week))
            throw new InvalidOperationException("Clock precedes saved ExBoss period; refusing rollback");
        string key = p.User.Uid + ":" + week;
        var state = Store.FirstOrDefault(s => s.Key == key);
        if (state == null)
        {
            state = new State { Key = key, Uid = p.User.Uid, Week = week, Rank = Rank(p), Schedule = ScheduleId(week), Day = Day };
            foreach (uint root in BossRoots(state.Rank))
                state.Bosses[root] = new Boss { Root = root, Current = root };
            Store.InsertOne(state);
        }
        else
        {
            // 老存档里只有当期那 3 个 boss，这里补齐到全量；已打的分数/阵容保留。
            bool added = false;
            foreach (uint root in BossRoots(state.Rank))
                if (!state.Bosses.ContainsKey(root))
                {
                    state.Bosses[root] = new Boss { Root = root, Current = root };
                    added = true;
                }
            if (added) Store.ReplaceOne(state);
        }
        if (state.Day != Day) { state.Day = Day; state.Used = 0; Store.ReplaceOne(state); }
        return state;
    }
    // 单机版：难度全开。原来这里还有一道
    //   .Where(x => x.MonsterLevel <= 5 || BossScoreRewardList.Any(...))
    // 而 7.0 这批排期的 BossScoreRewardList 是空数组，等于把 SSS(level 6) 全禁了。
    private static ExBossMonsterDataExcel[] Chain(State state, Boss boss) =>
        BaseExcelReader<ExBossMonsterData, ExBossMonsterDataExcel>.GetInstance().All
            .Where(x => x.ConfigId == state.Rank && x.BossId >= boss.Root && x.BossId < boss.Root + 10)
            .OrderBy(x => x.MonsterLevel).ToArray();
    private static Boss Find(State state, uint id) => state.Bosses.Values.FirstOrDefault(b => Chain(state, b).Any(x => x.BossId == id));
    private static bool Open => Clock().ToUnixTimeSeconds() < Bounds(Clock()).End;
    private static uint Total(State s) => (uint)s.Bosses.Values.Sum(b => (long)b.Score);
    private static uint Wins(State s) => (uint)s.Bosses.Values.Sum(b => (long)b.Wins);
    private static uint SweepLevel(State s)
    {
        uint level = 0;
        foreach (var previous in Store.Where(x => x.Uid == s.Uid && x.Rank == 104 && x.Week == s.Week - 604800))
            foreach (uint root in Schedule(previous.Schedule,previous.Rank)["UpBossIdList"].Values<uint>())
                if (previous.Bosses.TryGetValue(root,out var boss))
                    foreach (var meta in Chain(previous,boss).Where(x => boss.Cleared.Contains((uint)x.BossId)))
                        if (meta.MonsterLevel >= 5) level = Math.Max(level,meta.MonsterLevel - 1);
        return Math.Min(level,5);
    }
    private static ExBossInfo Info(State s)
    {
        var result = new ExBossInfo { EnterTimes = s.Used, ScheduleId = s.Schedule, NowScheduleId = s.Schedule, RankId = s.Rank, MaxSweepLevel = SweepLevel(s) };
        foreach (var b in s.Bosses.Values)
        {
            result.BossIdLists.Add(new ExBossIdInfo { BossId = b.Current, Score = b.Score, BossTimesScore = (uint)Chain(s,b).Count(x=>b.Cleared.Contains((uint)x.BossId)),
                AvatarIdLists = b.Avatars.ToArray(), WeaponIdLists = b.Weapons.ToArray(), StigmataIdLists = b.Stigmata.ToArray(), LastAvatarIdLists = b.LastTeam });
            // Native GetSingleBossHistoryMaxScoreByID compares the exact selected
            // difficulty ID, not the root. Root-only entries always display zero at SSS.
            foreach(var meta in Chain(s,b))
                result.SingleBossScoreRewardLists.Add(new ExBossSingleBossScoreReward { BossId = (uint)meta.BossId, MaxSettleScore = meta.MonsterLevel==6?b.BonusScore:0 });
        }
        return result;
    }
    internal static GetExBossScheduleRsp GetSchedule(Session session)
    {
        lock (Gate)
        {
            var s = Get(session.Player); var bounds = Bounds(Clock());
            return new GetExBossScheduleRsp { retcode = GetExBossScheduleRsp.Retcode.Succ, BeginTime = (uint)bounds.Start,
                EndTime = (uint)bounds.End, MinLevel = 38, RankId = s.Rank, ScheduleId = s.Schedule };
        }
    }
    internal static GetExBossInfoRsp GetInfo(Session session)
    {
        lock (Gate)
        {
            Settle(session);
            return new GetExBossInfoRsp { retcode = GetExBossInfoRsp.Retcode.Succ, BossInfo = Info(Get(session.Player)) };
        }
    }
    private static bool TurboEnabled(State s, Boss b, uint id) => Schedule(s.Schedule, s.Rank)["BossTurboModeList"]
        .Any(x => (bool)x["IsTurboFeatureOn"] && (int)x["Index"] == s.Bosses.Keys.ToList().IndexOf(b.Root) + 1
            && (uint)x["EffectLevel"] == Chain(s,b).Single(x => x.BossId == id).MonsterLevel);
    internal static ExBossStageBeginRsp Begin(Session session, ExBossStageBeginReq req)
    {
        lock (Gate)
        {
            var p = session.Player; var s = Get(p); var b = Find(s, req.BossId);
            ExBossStageBeginRsp Fail(ExBossStageBeginRsp.Retcode c) => new() { retcode = c };
            if (Level(p) < 38) return Fail(ExBossStageBeginRsp.Retcode.FeatureLimit);
            if (!Open) return Fail(ExBossStageBeginRsp.Retcode.NotOpen);
            if (b == null) return Fail(ExBossStageBeginRsp.Retcode.NoAvailableBoss);
            var chain = Chain(s,b); int pos = Array.FindIndex(chain,x => x.BossId == req.BossId);
            if (!req.IsTraining && pos > 0 && !b.Cleared.Contains((uint)chain[pos - 1].BossId)) return Fail(ExBossStageBeginRsp.Retcode.PreBossNotFinish);
            if (req.IsTurbo && !TurboEnabled(s,b,req.BossId)) return Fail(ExBossStageBeginRsp.Retcode.NotAllowTurbo);
            if (!req.IsTraining && s.Used >= (uint)Config(s.Rank)["challengeTimes"]) return Fail(ExBossStageBeginRsp.Retcode.EnterTimesLack);
            uint[] ids = (req.AvatarIdLists ?? Array.Empty<uint>()).Where(x => x != 0).ToArray();
            if (ids.Length < 1 || ids.Length > 3 || ids.Any(id => !p.AvatarList.Any(a => a.AvatarId == id && a.Star > 0))) return Fail(ExBossStageBeginRsp.Retcode.AvatarError);
            if (ids.Distinct().Count() != ids.Length) return Fail(ExBossStageBeginRsp.Retcode.DupAvatar);
            var team = UltraEndlessLineup.Capture(p, ids, req.ElfIdLists);
            if (team == null) return Fail(ExBossStageBeginRsp.Retcode.ElfError);
            var avatars = p.AvatarList.Where(a => ids.Contains(a.AvatarId)).ToArray();
            uint[] weapons = avatars.Select(a => a.WeaponUniqueId).Where(x => x != 0).Distinct().ToArray();
            uint[] stigmata = avatars.SelectMany(a => new[] { a.StigmataUniqueId1,a.StigmataUniqueId2,a.StigmataUniqueId3 }).Where(x => x != 0).Distinct().ToArray();
            if (!req.IsTraining)
            {
                if (b.Avatars.Union(ids).Count() > 6) return Fail(ExBossStageBeginRsp.Retcode.LackLockSlot);
                // 单机版开放全部 boss 后不再做「同一角色/武器/圣痕不能被两个 boss 同时占用」
                // 的检查 —— 69 个 boss 谁也凑不出 69 套阵容。单个 boss 最多锁 6 个的限制保留。
                var avatarData = BaseExcelReader<AvatarData,AvatarDataExcel>.GetInstance().All;
                int Base(uint id) => avatarData.FirstOrDefault(a => a.AvatarId == id)?.BaseAvatarId ?? 0;
                if (ids.Any(a => b.Avatars.Union(ids).Any(other => a != other && (Base(a) == other || Base(other) == a))))
                    return Fail(ExBossStageBeginRsp.Retcode.DupAvatar);
            }
            Attempts[session] = new Attempt { Week = s.Week, BossId = req.BossId, Root = b.Root, Training = req.IsTraining,
                Turbo = req.IsTurbo, Avatars = ids, Weapons = weapons, Stigmata = stigmata, Team = team };
            return new ExBossStageBeginRsp { retcode = ExBossStageBeginRsp.Retcode.Succ, StageTransactionStr = Guid.NewGuid().ToString("N") };
        }
    }
    // State and the reward grants share one SQLite transaction. No background polling.
    private static void Atomic(Player p, Action body)
    {
        string user = JsonConvert.SerializeObject(p.User), equipment = JsonConvert.SerializeObject(p.Equipment), avatars = JsonConvert.SerializeObject(p.AvatarList);
        try { Global.Store.InTransaction(body); }
        catch
        {
            p.User = JsonConvert.DeserializeObject<UserScheme>(user);
            p.Equipment = JsonConvert.DeserializeObject<EquipmentScheme>(equipment);
            p.AvatarList = JsonConvert.DeserializeObject<AvatarScheme[]>(avatars);
            throw;
        }
    }
    internal static ExBossStageEndRsp End(Session session, ExBossStageEndReq req)
    {
        lock (Gate)
        {
            var result = new ExBossStageEndRsp { retcode = ExBossStageEndRsp.Retcode.NotBegin, BossId = req.BossId, EndStatus = req.EndStatus };
            if (!Attempts.TryGetValue(session, out var attempt) || attempt.BossId != req.BossId) return result;
            var s = Get(session.Player);
            if (attempt.Week != s.Week || !Open) { Attempts.Remove(session); result.retcode = ExBossStageEndRsp.Retcode.NotOpen; return result; }
            if (!Enum.IsDefined(typeof(StageEndStatus), req.EndStatus)) return result;
            var notices = new List<(CmdId, object)>();
            if (!attempt.Training && req.EndStatus != StageEndStatus.StageExit)
            {
                var b = s.Bosses[attempt.Root]; var chain = Chain(s,b); var meta = chain.Single(x => x.BossId == req.BossId);
                if (s.Used >= (uint)Config(s.Rank)["challengeTimes"]) { Attempts.Remove(session); return result; }
                s.Used++; b.Avatars.UnionWith(attempt.Avatars); b.Weapons.UnionWith(attempt.Weapons); b.Stigmata.UnionWith(attempt.Stigmata); b.LastTeam = attempt.Avatars;
                if (req.EndStatus == StageEndStatus.StageWin)
                {
                    if(b.Cleared.Add(req.BossId))b.Wins += (uint)Math.Max(1,meta.TimesScore); int pos = Array.IndexOf(chain,meta);
                    b.Current = Math.Max(b.Current, (uint)chain[Math.Min(pos + 1,chain.Length - 1)].BossId);
                }
                // The client computes time-dilation scoring; clamp reports to the table-defined maximum.
                uint cap = (uint)Math.Ceiling((meta.MonsterBaseScore + meta.ExtraTimeScore) *
                    (Schedule(s.Schedule,s.Rank)["UpBossIdList"].Values<uint>().Contains(b.Root) ? 1.2 : 1));
                uint score = Math.Min(req.Score,cap);
                if (meta.MonsterLevel == 6) b.BonusScore = Math.Max(b.BonusScore,score);
                if (score > b.Score) { b.Score = score; b.BestTeam = attempt.Team; s.Updated = Clock().ToUnixTimeSeconds(); }
                Atomic(session.Player, () => { GrantMilestones(session.Player,s,notices); Store.ReplaceOne(s); session.Player.User.Save(); session.Player.Equipment.Save(); });
            }
            Attempts.Remove(session);
            result.retcode = ExBossStageEndRsp.Retcode.Succ;
            // The caller sends the end response before these updates.
            PendingNotices[session] = notices;
            return result;
        }
    }
    private static readonly Dictionary<Session,List<(CmdId,object)>> PendingNotices = new();
    internal static void Forget(Session session)
    {
        lock (Gate) { Attempts.Remove(session); PendingNotices.Remove(session); }
    }
    internal static void Flush(Session session)
    {
        lock (Gate)
        {
            if (PendingNotices.Remove(session, out var notices)&&notices.Count>0){foreach (var (cmd, proto) in notices) Send(session,cmd,proto);DailyGameplay.PushInventory(session);if(File.Exists("Resources/ExcelOutputAsset/AdventureData.json"))session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq(),CmdId.GetAvatarDataReq));}
        }
    }
    private static void Send(Session session, CmdId cmd, object proto)
    {
        if (proto is TakeExBossScoreRewardNotify a) session.Send(Packet.FromProto(a,cmd));
        else if (proto is ExBossTakeBossScoreRewardNotify b) session.Send(Packet.FromProto(b,cmd));
        else if (proto is TakeExBossRankRewardNotify c) session.Send(Packet.FromProto(c,cmd));
    }
    private static IEnumerable<JToken> RewardRows(State s, string name)
    {
        var rows = Table(name).Where(x => (uint)x["configId"] == s.Rank).ToArray();
        var exact = rows.Where(x => (uint)x["scheduleId"] == s.Schedule).ToArray();
        if (exact.Length > 0) return exact;
        var common = rows.Where(x => (uint)x["scheduleId"] == 0).ToArray();
        if (common.Length > 0) return common;
        // Tables only ship some reward schedule IDs. Use the latest shipped reward definition for this bracket.
        uint last = rows.Max(x => (uint)x["scheduleId"]);
        return rows.Where(x => (uint)x["scheduleId"] == last);
    }
    private static Common.Resources.Proto.RewardData Grant(Player p, int id, string medalSource = null, long? earnedAt = null)
    {
        var cfg = BaseExcelReader<Common.Utils.ExcelReader.RewardData, RewardDataExcel>.GetInstance().FromId(id)
            ?? throw new InvalidDataException("Missing ExBoss reward " + id);
        var copy = JsonConvert.DeserializeObject<RewardDataExcel>(JsonConvert.SerializeObject(cfg));
        var fragments = new List<RewardItemData>();
        for (int i = 1; i <= 6; i++)
        {
            var property = typeof(RewardDataExcel).GetProperty("RewardItem" + i + "Id");
            int item = (int)property.GetValue(copy);
            if (item > 0 && (Table("MedalData").Any(x => (int)x["medalID"] == item) || Table("FrameData").Any(x => (int)x["id"] == item)))
            {
                if (CompetitiveMedalService.Controlled((uint)item))
                {
                    if (medalSource == null || !earnedAt.HasValue) throw new InvalidOperationException("Competitive medal needs a settlement receipt");
                    CompetitiveMedalService.Grant(p.User.Uid,(uint)item,medalSource,earnedAt.Value);
                }
                // Existing server policy exposes all medals and frames as owned. Record the
                // reward receipt through State.Settled; do not add a medal to material storage.
                uint amount = (uint)Math.Max(1,(int)typeof(RewardDataExcel).GetProperty("RewardItem" + i + "Num").GetValue(copy));
                fragments.Add(new RewardItemData { Id = (uint)item, Num = amount }); property.SetValue(copy,0); continue;
            }
            var meta = BaseExcelReader<AvatarData,AvatarDataExcel>.GetInstance().All.FirstOrDefault(x => x.AvatarFragmentId == item && item > 0);
            if (meta == null) continue;
            uint num = (uint)Math.Max(1,(int)typeof(RewardDataExcel).GetProperty("RewardItem" + i + "Num").GetValue(copy));
            var avatar = p.AvatarList.FirstOrDefault(x => x.AvatarId == meta.AvatarId);
            if (avatar == null)
            {
                // A fragment balance must not unlock the character or grant a starter weapon.
                avatar = new AvatarScheme { OwnerUid = p.User.Uid, AvatarId = (uint)meta.AvatarId, Star = 0, Level = 1,
                    DressId = (uint)meta.DefaultDressId, DressLists = new uint[] { (uint)meta.DefaultDressId } };
                Common.Database.Avatar.collection.InsertOne(avatar);
                p.AvatarList = p.AvatarList.Append(avatar).ToArray();
            }
            avatar.Fragment += num; avatar.Save();
            fragments.Add(new RewardItemData { Id = (uint)item, Num = num }); property.SetValue(copy,0);
        }
        var result = RewardService.Grant(p,copy); result.ItemLists.AddRange(fragments); return result;
    }
    private static void GrantMilestones(Player p, State s, List<(CmdId, object)> notices)
    {
        var scoreNotice = new TakeExBossScoreRewardNotify();
        foreach (var row in RewardRows(s,"ExBossScoreReward"))
        {
            bool reached = (bool)Schedule(s.Schedule,s.Rank)["IsTimesScore"] ? Wins(s) >= (uint)row["TimesScore"] : Total(s) >= (uint)row["score"];
            int id = (int)row["ID"];
            if (reached && s.Rewards.Add(id)) scoreNotice.RewardLists.Add(Grant(p,(int)row["rewardId"]));
        }
        if (scoreNotice.RewardLists.Count > 0) notices.Add((CmdId.TakeExBossScoreRewardNotify,scoreNotice));
        var bossNotice = new ExBossTakeBossScoreRewardNotify();
        foreach (var rule in Schedule(s.Schedule,s.Rank)["BossScoreRewardList"])
        {
            var boss = s.Bosses.Values.ElementAt((int)rule["Index"] - 1);
            foreach (var row in Table("ExBossMonsterScoreReward").Where(x => (int)x["RewardConfigID"] == (int)rule["RewardConfigID"] && boss.BonusScore >= (uint)x["Score"]))
            {
                int key = -((int)rule["Index"] * 10000 + (int)row["RewardConfigID"] * 100 + (int)row["ScoreLineNode"]);
                if (s.Rewards.Add(key)) bossNotice.RewardLists.Add(Grant(p,(int)row["RewardID"]));
            }
        }
        if (bossNotice.RewardLists.Count > 0) notices.Add((CmdId.ExBossTakeBossScoreRewardNotify,bossNotice));
    }
    private static List<State> Ranking(State s, uint bossId)
    {
        uint group = bossId == 0 ? 0 : Find(s,bossId)?.Root ?? uint.MaxValue;
        uint Score(State a) => group == 0 ? Total(a) : a.Bosses.TryGetValue(group,out var b) ? b.Score : 0;
        return Store.Where(x => x.Week == s.Week && x.Rank == s.Rank && x.Schedule == s.Schedule)
            .Where(x => Score(x) > 0).OrderByDescending(Score).ThenBy(x => x.Updated).ThenBy(x => x.Uid).ToList();
    }
    internal static GetExBossRankRsp GetRank(Session session, GetExBossRankReq req)
    {
        lock (Gate)
        {
            var s = Get(session.Player); bool total = req.BossId == 0 || req.BossId >= uint.MaxValue - 1;
            var b = total ? null : Find(s,req.BossId);
            var data = new RankShowData { IsFeatureClosed = false, MyRankType = 1, MyScore = total ? Total(s) : b?.Score ?? 0 };
            var rows = Ranking(s,total ? 0 : req.BossId); int own = rows.FindIndex(x => x.Uid == s.Uid);
            data.MyRank = own < 0 ? 0 : (uint)(own + 1);
            foreach (var r in rows.Take(100))
            {
                var user = Common.Database.User.FromUid(r.Uid); if (user == null) continue;
                var rank = new UserRankData { Uid = r.Uid, Rank = (uint)rows.IndexOf(r) + 1, Score = total ? Total(r) : r.Bosses.TryGetValue(b?.Root ?? 0,out var rb) ? rb.Score : 0,
                    NickName = string.IsNullOrEmpty(user.Nick) ? user.Name : user.Nick, AvatarId = (uint)user.AssistantAvatarId, FrameId = (uint)user.FrameId, CustomHeadId = (uint)user.CustomHeadId };
                var team = total ? r.Bosses.Values.OrderByDescending(x => x.Score).FirstOrDefault()?.BestTeam : r.Bosses.GetValueOrDefault(b?.Root ?? 0)?.BestTeam;
                if (team != null) { rank.AvatarInfoLists.AddRange(team.AvatarInfoLists); rank.ElfInfoLists.AddRange(team.ElfInfoLists); }
                data.RankLists.Add(rank);
            }
            return new GetExBossRankRsp { retcode = GetExBossRankRsp.Retcode.Succ, BossId = req.BossId, RankId = s.Rank, RankData = data };
        }
    }
    private static void Settle(Session session)
    {
        CompetitiveMedalService.Prepare();
        long now = Clock().ToUnixTimeSeconds();
        // 和 Bounds() 保持一致：本期的分数算满 7 天才结算，不然「周一还开着」的时候
        // 上期已经在周一 04:00 结算掉了，会出现「还能打但分数算进已结算的期」。
        foreach (var s in Store.Where(x => x.Uid == session.Player.User.Uid && !x.Settled && x.Week + 7 * 86400 <= now))
        {
            var rows = Ranking(s,0); int index = rows.FindIndex(x => x.Uid == s.Uid);
            var notice = new TakeExBossRankRewardNotify { Score = Total(s), Rank = index < 0 ? 0 : (uint)index + 1 };
            Atomic(session.Player, () =>
            {
                if (index >= 0)
                {
                    // Local single-player rule: 0.1% (10 permyriad); multiplayer uses the real percentile.
                    int percentile = rows.Count == 1 ? 10 : (int)Math.Ceiling((index + 1) * 10000.0 / rows.Count);
                    var rule = RewardRows(s,"ExBossRankReward").OrderBy(x => (int)x["rank"]).FirstOrDefault(x => (int)x["rank"] >= percentile);
                    if (rule != null) notice.RewardLists.Add(Grant(session.Player,(int)rule["rewardId"],"exboss:"+s.Week,s.Week+6*86400));
                }
                s.Settled = true; Store.ReplaceOne(s); session.Player.User.Save(); session.Player.Equipment.Save();
            });
            if (notice.RewardLists.Count > 0){session.Send(Packet.FromProto(notice,CmdId.TakeExBossRankRewardNotify));DailyGameplay.PushInventory(session);}
        }
    }
    internal static ExBossSweepRsp Sweep(Session session, ExBossSweepReq req)
    {
        lock (Gate)
        {
            var s = Get(session.Player); var b = Find(s,req.BossId);
            var result = new ExBossSweepRsp { BossId = req.BossId, retcode = ExBossSweepRsp.Retcode.Fail };
            if (!Open) result.retcode = ExBossSweepRsp.Retcode.NotOpen;
            else if (!(bool)Config(s.Rank)["IsSweepOn"]) result.retcode = ExBossSweepRsp.Retcode.LevelLimit;
            else if (s.Used >= (uint)Config(s.Rank)["challengeTimes"]) result.retcode = ExBossSweepRsp.Retcode.EnterTimesLack;
            else if (b != null)
            {
                var chain = Chain(s,b); var target = chain.Single(x => x.BossId == req.BossId);
                if (target.MonsterLevel > SweepLevel(s)) result.retcode = ExBossSweepRsp.Retcode.LevelLimit;
                else if (b.Cleared.Contains(req.BossId)) result.retcode = ExBossSweepRsp.Retcode.Finished;
                else
                {
                    // v5.9: last week's UP SS/SSS clearance unlocks S/SS sweeping for every boss.
                    bool eligible = SweepLevel(s) >= target.MonsterLevel;
                    if (!eligible) result.retcode = ExBossSweepRsp.Retcode.LevelLimit;
                    else
                    {
                        var pending = chain.Where(x => x.MonsterLevel <= target.MonsterLevel && !b.Cleared.Contains((uint)x.BossId)).ToArray();
                        if (s.Used + pending.Length > (uint)Config(s.Rank)["challengeTimes"]) result.retcode = ExBossSweepRsp.Retcode.EnterTimesLack;
                        else
                        {
                            var notices = new List<(CmdId,object)>();
                            s.Used += (uint)pending.Length;
                            foreach(var m in pending) { b.Cleared.Add((uint)m.BossId); b.Wins += (uint)Math.Max(1,m.TimesScore); }
                            b.Current = Math.Max(b.Current,(uint)chain[Math.Min(Array.IndexOf(chain,target)+1,chain.Length-1)].BossId);
                            uint score = (uint)Math.Ceiling(target.MonsterBaseScore * (Schedule(s.Schedule,s.Rank)["UpBossIdList"].Values<uint>().Contains(b.Root) ? 1.2 : 1));
                            if (score > b.Score) { b.Score = score; b.BestTeam = null; s.Updated = Clock().ToUnixTimeSeconds(); }
                            Atomic(session.Player, () => { GrantMilestones(session.Player,s,notices); Store.ReplaceOne(s); session.Player.User.Save(); session.Player.Equipment.Save(); });
                            PendingNotices[session] = notices; result.retcode = ExBossSweepRsp.Retcode.Succ;
                        }
                    }
                }
            }
            return result;
        }
    }
}
