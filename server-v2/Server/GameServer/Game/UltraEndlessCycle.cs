using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json;
using UltraEndlessSite = Common.Utils.ExcelReader.UltraEndlessSite;
using UltraEndlessFloor = Common.Utils.ExcelReader.UltraEndlessFloor;
using FrameData = Common.Utils.ExcelReader.FrameData;

namespace PemukulPaku.GameServer.Game;

/// <summary>UTC+8 Monday/Friday 15:00, Wednesday/Sunday 22:00 challenge cutoff; client metadata IDs remain valid.</summary>
internal static class UltraEndlessCycle
{
    public static Func<DateTimeOffset> Clock = () => DateTimeOffset.UtcNow;
    public static bool Enabled => UltraTuning.AutomaticSchedule;
    public sealed record Period(long Key, long End, long Ordinal, uint ScheduleId, int[] Groups, int[] Sites, uint MaxScore)
    {
        public long Close => End + 1800;
        public long NextBegin => AbyssCalendar.At(DateTimeOffset.FromUnixTimeSeconds(Key)).NextBegin;
        public bool Open => Clock().ToUnixTimeSeconds() >= Key && Clock().ToUnixTimeSeconds() < End;
    }
    private sealed class ScheduleMeta
    {
        public uint ScheduleID;
        public LevelMeta[] BattleConfigList;
    }
    private sealed class LevelMeta { public int GroupLevel; }
    private sealed class StageMeta { public int StageID; }
    private sealed class CupRule
    {
        public uint GroupLevel;
        public int PlayerNumInGroup;
        public int OnePlayerNoAttendCup;
        public int[] CupChangeList;
    }
    private static readonly Lazy<ScheduleMeta[]> Schedules = new(() => Read<ScheduleMeta>("UltraEndlessSchedule"));
    private static readonly Lazy<CupRule[]> Rules = new(() => Read<CupRule>("UltraEndlessSettleCup"));
    private static readonly Lazy<HashSet<int>> Stages = new(() => Read<StageMeta>("StageDetail").Select(x => x.StageID).ToHashSet());
    private static readonly object CacheGate = new();
    private static string _cacheKey;
    private static Period _cached;
    private static Timer _timer;
    private static int _ticking;
    private static long _lastBroadcast;
    private static T[] Read<T>(string name) => JsonConvert.DeserializeObject<T[]>(
        File.ReadAllText(Path.Combine("Resources", "ExcelOutputAsset", name + ".json"))) ?? Array.Empty<T>();

    public static Random RandomFor(string key)
    {
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(UltraTuning.RotationSeed + "|" + key));
        return new Random(System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(hash));
    }
    private static void Shuffle<T>(T[] items, Random random)
    {
        for (int i = items.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (items[i], items[j]) = (items[j], items[i]);
        }
    }
    public static (long Begin, long End, long Ordinal) Bounds(DateTimeOffset now)
    {
        var period = AbyssCalendar.At(now);
        return (period.Begin, period.End, period.Ordinal);
    }
    public static int[] RotationPool()
    {
        var all = BaseExcelReader<UltraEndlessSite, UltraEndlessSiteExcel>.GetInstance().All;
        var floors = BaseExcelReader<UltraEndlessFloor, UltraEndlessFloorExcel>.GetInstance();
        var requested = UltraTuning.RotationGroups;
        var groups = requested != null && requested.Length > 0 ? requested : all
            .Where(s => s.SiteNodeName == "Area1" && (s.PreSiteList == null || s.PreSiteList.Length == 0))
            .Select(s => s.SiteId / 10).Distinct().ToArray();
        return groups.Distinct().Where(g => UltraEndlessSites.TryResolveGroup(g, all, out var chain) && UltraEndlessValidation.Playable(chain) &&
            chain.All(s => Stages.Value.Contains(s.StageId) && floors.GetFloorDatasFromStageId(s.StageId).Count > 0))
            .OrderBy(g => g).ToArray();
    }
    public static Period Current => At(Clock());
    public static Period At(DateTimeOffset time)
    {
        var bounds = Bounds(time);
        string configKey = $"{bounds.Begin}|{UltraTuning.RotationAnchor}|{UltraTuning.RotationSeed}|" +
            $"{UltraTuning.ManualOverridePeriod}|{string.Join(",", UltraTuning.RawSiteGroups ?? new[] { 110 })}|" +
            string.Join(",", UltraTuning.RotationGroups ?? Array.Empty<int>());
        lock (CacheGate)
        {
            if (_cacheKey == configKey) return _cached;
            long anchor = UltraTuning.RotationAnchor > 0 ? UltraTuning.RotationAnchor : Bounds(DateTimeOffset.UnixEpoch).Begin;
            long offset = bounds.Ordinal - Bounds(DateTimeOffset.FromUnixTimeSeconds(anchor)).Ordinal;
            int[] groups;
            if (offset <= 0 || UltraTuning.ManualOverridePeriod == bounds.Begin)
                groups = UltraTuning.RawSiteGroups ?? UltraEndlessRank.DefaultSiteGroups;
            else
            {
                var pool = RotationPool();
                if (pool.Length == 0) throw new InvalidOperationException("自动深渊排期没有可用的完整配置。");
                Shuffle(pool, RandomFor("rotation-order"));
                // Start with a different environment from the installation period if possible.
                var initial = UltraTuning.RawSiteGroups?.FirstOrDefault() ?? 110;
                if (pool.Length > 1 && pool[0] == initial) (pool[0], pool[1]) = (pool[1], pool[0]);
                groups = new[] { pool[(int)((offset - 1) % pool.Length)] };
            }
            var all = BaseExcelReader<UltraEndlessSite, UltraEndlessSiteExcel>.GetInstance().All;
            var sites = UltraEndlessSites.ResolveGroups(groups, all);
            var floors = BaseExcelReader<UltraEndlessFloor, UltraEndlessFloorExcel>.GetInstance();
            uint maximum = (uint)sites.Sum(s => floors.GetFloorDatasFromStageId(s.StageId).Sum(f => (long)f.MaxScore));
            var metas = Schedules.Value.Where(s => s.BattleConfigList != null &&
                Enumerable.Range(1, 9).All(level => s.BattleConfigList.Any(l => l.GroupLevel == level)))
                .OrderBy(s => s.ScheduleID).ToArray();
            if (metas.Length == 0) throw new InvalidOperationException("缺少可用的客户端深渊排期元数据。");
            int start = Array.FindIndex(metas, s => s.ScheduleID == UltraEndlessState.DefaultScheduleId);
            if (start < 0) start = 0;
            uint scheduleId = metas[(int)((start + Math.Max(0, offset)) % metas.Length)].ScheduleID;
            _cached = new Period(bounds.Begin, bounds.End, bounds.Ordinal, scheduleId, groups,
                sites.Select(s => s.SiteId).ToArray(), maximum);
            _cacheKey = configKey;
            return _cached;
        }
    }
    public static Period FromKey(long key) => At(DateTimeOffset.FromUnixTimeSeconds(key));
    public static uint GroupForCups(uint cups) => (uint)BaseExcelReader<UltraEndlessGroup, UltraEndlessGroupExcel>
        .GetInstance().All.Where(g => g.NeedCupNum <= cups).OrderBy(g => g.NeedCupNum).Last().GroupLevel;
    public static uint InitialCups(uint group) => BaseExcelReader<UltraEndlessGroup, UltraEndlessGroupExcel>
        .GetInstance().CupNumOf(group == 0 ? 8u : group);
    public static int CupDelta(uint group, int members, int rank, bool participated)
    {
        var rule = Rules.Value.Single(r => r.GroupLevel == group && r.PlayerNumInGroup == members);
        return !participated ? rule.OnePlayerNoAttendCup : rule.CupChangeList[Math.Clamp(rank, 1, members) - 1];
    }
    public static uint[] OpponentScores(Period period)
    {
        int count = UltraTuning.MemberCount - 1;
        var scores = new uint[count];
        var random = RandomFor($"scores|{period.Key}|{string.Join(",", period.Sites)}");
        for (int i = 0; i < count; i++)
        {
            uint score = i < 2 ? 18000u : i == 2 ? 0u : i >= count - 2
                ? (uint)random.Next(21700, 21951) : (uint)random.Next(2101, 21701);
            while (i >= 3 && i < count - 2 && score == 18000)
                score = (uint)random.Next(2101, 21701);
            if (period.MaxScore < 22000) score = (uint)((long)score * period.MaxScore / 22000);
            scores[i] = score;
        }
        Shuffle(scores, random);
        return scores;
    }
    public static List<UltraEndlessRank.Row> Rows(uint selfUid, uint score, uint updatedAt, long periodKey, uint[] opponents)
    {
        var rows = opponents.Select((s, i) => new UltraEndlessRank.Row(
            UltraEndlessRank.FakeUidBase + (uint)i + 1, s, false, (uint)periodKey + (uint)i + 1)).ToList();
        rows.Add(new UltraEndlessRank.Row(selfUid, score, true,
            score == 0 && updatedAt == 0 ? uint.MaxValue : updatedAt));
        return rows.OrderByDescending(r => r.Score).ThenBy(r => r.UpdatedAt).ThenBy(r => r.Uid).ToList();
    }
    public sealed record Identity(string Nickname, uint Avatar, uint Head, uint Frame);
    public static uint BotCups(uint group, uint uid)
    {
        var levels = BaseExcelReader<UltraEndlessGroup, UltraEndlessGroupExcel>.GetInstance().All;
        uint minimum = InitialCups(group);
        var nextLevel = levels.Where(g => g.GroupLevel > group).OrderBy(g => g.GroupLevel).FirstOrDefault();
        uint next = nextLevel == null ? minimum + 1000 : (uint)nextLevel.NeedCupNum;
        return minimum + (uint)RandomFor($"cups|{Current.Key}|{uid}").Next((int)Math.Max(1, next - minimum));
    }
    private static long _identityPeriod;
    private static string _identitySeed;
    private static Identity[] _identities;
    public static Identity Bot(uint uid)
    {
        var period = Current;
        long key = period.Key * 4 + (Clock().ToUnixTimeSeconds() < period.End ? 0 : Clock().ToUnixTimeSeconds() < period.Close ? 1 : 2);
        lock (CacheGate)
        {
            if (_identities == null || _identityPeriod != key || _identitySeed != UltraTuning.RotationSeed)
            {
                var random = RandomFor($"identities|{key}");
                var required = new[] { "杨卧起坐", "零点酒吧白衣渡江", "白壁仙尊", "神州平板", "元素女皇琪亚娜", "天命第一偶像绿托" };
                var extras = new[] { "今天也要保红莲", "量子海摸鱼员", "芽衣的饭团", "舰桥扫地机器人", "爱门永存", "识宝天下第一", "布洛妮娅在加班", "月下等你下班", "西琳今天不膨胀", "帕朵进货中", "格蕾修画画中", "伊甸请客", "苏师傅闭眼通关", "凯文泡面要加蛋", "梅比乌斯实验室", "渡鸦的小岛", "迷迭香下午茶", "胡狼的体检报告", "普罗米修斯计算中", "深渊最后一分钟", "水晶攒给下版本", "这次一定不凹分", "猫猫只想睡觉", "希儿帮我开个门" };
                Shuffle(extras, random);
                var names = required.Concat(extras.Take(UltraTuning.MemberCount - 1 - required.Length)).ToArray();
                Shuffle(names, random);
                var heads = BaseExcelReader<CustomHeadData, CustomHeadDataExcel>.GetInstance().All
                    .Where(h => h.Show && h.HeadId > 0 && !string.IsNullOrEmpty(h.IconPath)).ToArray();
                var frames = BaseExcelReader<FrameData, FrameDataExcel>.GetInstance().All
                    .Where(f => f.IsShow && f.Id > 0 && !string.IsNullOrEmpty(f.IconPath)).ToArray();
                if (heads.Length == 0 || frames.Length == 0) throw new InvalidOperationException("缺少有效头像/相框数据。");
                _identities = names.Select(n =>
                {
                    var head = heads[random.Next(heads.Length)];
                    return new Identity(n, head.HeadType == 1 && head.HeadParaInt > 0 ? (uint)head.HeadParaInt : 101u,
                        (uint)head.HeadId, (uint)frames[random.Next(frames.Length)].Id);
                }).ToArray();
                _identityPeriod = key;
                _identitySeed = UltraTuning.RotationSeed;
            }
            return _identities[(int)(uid - UltraEndlessRank.FakeUidBase - 1) % _identities.Length];
        }
    }
    public static void Start()
    {
        _timer ??= new Timer(_ =>
        {
            if (!Enabled || Interlocked.Exchange(ref _ticking, 1) != 0) return;
            try
            {
                Tick(Server.GetInstance().Sessions.Values);
            }
            catch (Exception error) { Packet.c.Warn("[Ultra] 自动换期失败：" + error.Message); }
            finally { Volatile.Write(ref _ticking, 0); }
        }, null, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(10));
    }
    public static void Tick(IEnumerable<Session> sessions)
    {
        if (!Enabled) return;
        var period = Current;
        long key = period.Key * 4 + (Clock().ToUnixTimeSeconds() < period.End ? 0 : Clock().ToUnixTimeSeconds() < period.Close ? 1 : 2);
        foreach (var user in User.collection.All()) UltraEndlessState.Get(user.Uid);
        if (_lastBroadcast != 0 && _lastBroadcast != key)
            foreach (var session in sessions)
                if (session.Player?.User != null)
                    new Handlers.UltraEndlessGetMainDataReqHandler().Handle(session,
                        Packet.FromProto(new UltraEndlessGetMainDataReq(), CmdId.UltraEndlessGetMainDataReq));
        _lastBroadcast = key;
    }
}
