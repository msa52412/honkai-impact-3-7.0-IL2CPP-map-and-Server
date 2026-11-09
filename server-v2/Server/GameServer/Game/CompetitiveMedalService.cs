using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json.Linq;

namespace PemukulPaku.GameServer.Game;

// Only competitive reward medals lose the old all-owned policy. Other medals stay unchanged.
internal static class CompetitiveMedalService
{
    internal static Func<DateTimeOffset> Clock = () => DateTimeOffset.UtcNow;
    private static readonly Lazy<Dictionary<uint,JToken>> Meta = new(() => Read("MedalData").ToDictionary(x => (uint)x["medalID"]));
    private static readonly Lazy<HashSet<uint>> ControlledIds = new(BuildControlled);
    private static JArray Read(string name) => JArray.Parse(File.ReadAllText(Path.Combine("Resources","ExcelOutputAsset",name + ".json")));
    private static SqlTable<Balance> Balances => Global.Table<Balance>("CompetitiveMedals",("key","TEXT",x => x.Key,true));
    private static SqlTable<Receipt> Receipts => Global.Table<Receipt>("CompetitiveMedalReceipts",("key","TEXT",x => x.Key,true));
    public sealed class Balance : IRow
    {
        public long Id { get; set; }
        public long CreatedAt { get; set; }
        public string Key { get; set; }
        public uint Uid { get; set; }
        public uint MedalId { get; set; }
        public uint EndTime { get; set; }
        public bool AbyssDurationReconciled { get; set; }
    }
    public sealed class Receipt : IRow
    {
        public long Id { get; set; }
        public long CreatedAt { get; set; }
        public string Key { get; set; }
        public long EarnedAt { get; set; }
    }
    private static HashSet<uint> BuildControlled()
    {
        var rewardIds = new HashSet<int>();
        foreach (var (file,fields) in new[] {
            ("ExBossRankReward",new[]{"rewardId"}), ("UltraEndlessReward",new[]{"SettleReward"}),
            ("OWEndlessRewardConfig",new[]{"prototeRewardID","normalRewardID","demoteRewardID"}),
            ("GreedyEndlessRankReward",new[]{"RankReward"}) })
        {
            if (!File.Exists(Path.Combine("Resources","ExcelOutputAsset",file+".json"))) continue;
            foreach (var row in Read(file)) foreach(var field in fields) if ((int?)row[field] is int id && id > 0) rewardIds.Add(id);
        }
        // Weekly Elysian Realm medals are earned, time-limited rewards too.
        if(File.Exists(Path.Combine("Resources","ExcelOutputAsset","GodWarChallengeReward.json")))
        {
            var missions=Read("GodWarChallengeReward").Select(x=>(uint)x["Mission"]).ToHashSet();
            foreach(var m in BaseExcelReader<MissionData,MissionDataExcel>.GetInstance().All.Where(x=>missions.Contains((uint)x.Id)))if(m.RewardId>0)rewardIds.Add(m.RewardId);
        }
        var ids = new HashSet<uint>();
        foreach (var cfg in BaseExcelReader<Common.Utils.ExcelReader.RewardData,RewardDataExcel>.GetInstance().All.Where(x => rewardIds.Contains(x.RewardId)))
            for(int i=1;i<=6;i++)
            {
                uint id=(uint)(int)typeof(RewardDataExcel).GetProperty("RewardItem"+i+"Id").GetValue(cfg);
                if (Meta.Value.TryGetValue(id,out var medal) && (int)medal["timeType"] == 2) ids.Add(id);
            }
        return ids;
    }
    internal static bool Controlled(uint id) => ControlledIds.Value.Contains(id);
    internal static void Prepare() { _=Balances.Count(); _=Receipts.Count(); }
    internal static bool Owns(uint uid,uint id)
    {
        if (!Meta.Value.ContainsKey(id)) return false;
        if (!Controlled(id)) return true;
        var b=Balances.FirstOrDefault(x => x.Key == uid+":"+id);
        return b != null && b.EndTime > Clock().ToUnixTimeSeconds();
    }
    internal static Medal Get(uint uid,uint id)
    {
        if (id == 0 || !Owns(uid,id)) return null;
        return new Medal { Id=id, EndTime=Controlled(id) ? Balances.FirstOrDefault(x => x.Key==uid+":"+id).EndTime : 0, ExtraParam=0 };
    }
    internal static Medal Equipped(UserScheme user) => Get(user.Uid,user.OnMedalId);
    internal static uint[] VisibleSlots(UserScheme user) => PlayerCardService.Slots(user.ShowcaseMedalIds,2).Select(id => Owns(user.Uid,id) ? id : 0).ToArray();
    internal static Medal[] List(uint uid)
    {
        var earned=Balances.Where(x => x.Uid==uid).ToDictionary(x => x.MedalId);
        return Meta.Value.Keys.Where(id => !Controlled(id) || earned.ContainsKey(id)).Select(id => new Medal {
            Id=id, EndTime=Controlled(id) ? earned[id].EndTime : 0, ExtraParam=0 }).ToArray();
    }
    // Collection tracks historical acquisition, while wearing requires a currently valid balance.
    internal static uint[] Collection(uint uid,IEnumerable<uint> original)
    {
        var earned=Balances.Where(x => x.Uid==uid).Select(x => x.MedalId).ToHashSet();
        return original.Where(id => !Controlled(id) || earned.Contains(id)).ToArray();
    }
    internal static void ReconcileAbyssDurations(uint uid)
    {
        Global.Store.InTransaction(() =>
        {
            var pending = Balances.Where(x => x.Uid == uid && !x.AbyssDurationReconciled).ToArray();
            if (pending.Length == 0) return;
            string prefix = uid + ":";
            var history = Receipts.All().Where(x => x.Key.StartsWith(prefix, StringComparison.Ordinal))
                .ToLookup(x => x.Key.Split(':')[1]);
            foreach (var balance in pending)
            {
                string abyssPrefix = balance.Key + ":ultra:";
                var awards = history[balance.MedalId.ToString()]
                    .Where(x => x.Key.StartsWith(abyssPrefix, StringComparison.Ordinal))
                    .OrderBy(x => x.EarnedAt).ToArray();
                if (awards.Length == 0) continue;
                long duration = (long)(int)Meta.Value[balance.MedalId]["during"] * 60;
                long end = 0;
                foreach (var award in awards)
                    end = Math.Max(end, award.EarnedAt) + duration;
                balance.EndTime = Math.Max(balance.EndTime, checked((uint)end));
                balance.AbyssDurationReconciled = true;
                Balances.ReplaceOne(balance);
            }
        });
    }

    internal static bool Grant(uint uid,uint id,string source,long earnedAt)
    {
        if (!Controlled(id)) return false;
        int minutes=(int)Meta.Value[id]["during"];
        if(minutes<=0) throw new InvalidDataException("Invalid competitive medal duration "+id);
        bool added=false;
        Global.Store.InTransaction(() => {
            string receiptKey=uid+":"+id+":"+source;
            if(Receipts.FirstOrDefault(x=>x.Key==receiptKey)!=null) return;
            string key=uid+":"+id;
            var balance=Balances.FirstOrDefault(x=>x.Key==key) ?? new Balance {Key=key,Uid=uid,MedalId=id};
            // Renew from the later of the award time and the existing expiry.
            balance.EndTime = checked((uint)(Math.Max(balance.EndTime, earnedAt) + (long)minutes * 60));
            if (source.StartsWith("ultra:", StringComparison.Ordinal))
                balance.AbyssDurationReconciled = true;
            Balances.ReplaceOne(balance);
            Receipts.InsertOne(new Receipt {Key=receiptKey,EarnedAt=earnedAt}); added=true;
        });
        return added;
    }
    internal static void GrantReward(uint uid,int rewardId,string source,long earnedAt)
    {
        var cfg=BaseExcelReader<Common.Utils.ExcelReader.RewardData,RewardDataExcel>.GetInstance().FromId(rewardId)
            ?? throw new InvalidDataException("Missing competitive medal reward "+rewardId);
        for(int i=1;i<=6;i++)
        {
            uint id=(uint)(int)typeof(RewardDataExcel).GetProperty("RewardItem"+i+"Id").GetValue(cfg);
            if(Controlled(id)) Grant(uid,id,source,earnedAt);
        }
    }
    internal static void GrantAbyss(uint uid,uint schedule,uint group,long period,long settlement,bool participated)
    {
        if(!participated) return;
        var row=AbyssSettlementRewards.RewardRow(group,schedule);
        if(row!=null) GrantReward(uid,(int)row["SettleReward"],"ultra:"+period,settlement);
    }
}
