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

// The 7.0 wire contract is verified. Growth weights/tail curve and local job
// selection are an explicitly documented approximation, not extracted V2 data.
internal static class AdventureService
{
    internal static Func<DateTimeOffset> Clock = () => DateTimeOffset.UtcNow;
    private static readonly object Gate = new();
    private static readonly Dictionary<string,JArray> Tables = new();
    private static SqlTable<State> Store => Global.Table<State>("AdventureStates",("owner_uid","INTEGER",s=>s.Uid,true));
    public sealed class State : IRow
    {
        public long Id {get;set;}
        public long CreatedAt {get;set;}
        public uint Uid {get;set;}
        public Dictionary<uint,Rank> Ranks {get;set;} = new();
        public uint Level {get;set;} = 1;
        public uint Total {get;set;}
        public uint Badge {get;set;}
        public uint Taken {get;set;}
        public uint Grain {get;set;}
        public uint Scoin {get;set;}
        public long Tick {get;set;}
        public long Day {get;set;} = -1;
        public uint Resets {get;set;}
        public Dictionary<uint,Job> Jobs {get;set;} = new();
    }
    public sealed class Rank {public uint Star {get;set;} public uint Sub {get;set;} }
    public sealed class Job
    {
        public uint Pool {get;set;}
        public uint Quest {get;set;}
        public long Start {get;set;}
        public uint[] Avatars {get;set;} = Array.Empty<uint>();
        public uint Elf {get;set;}
        public bool Taken {get;set;}
    }
    public sealed class GrowthConfig
    {
        public string Mode {get;set;} = "approximate-7.0";
        public uint[] Need {get;set;}
        public uint[] NormalA {get;set;} = new uint[]{0,0,1,2,3,5};
        public uint[] SpA {get;set;} = new uint[]{0,0,3,6,10,15};
        public uint[] InitialS {get;set;} = new uint[]{0,0,0,6,10,16};
        public uint SubStarExp {get;set;} = 1;
        public uint ACap {get;set;} = 180;
    }
    private static GrowthConfig _growth;
    private static JArray Table(string name)
    {
        if(!Tables.TryGetValue(name,out var data))
            Tables[name]=data=JArray.Parse(File.ReadAllText(Path.Combine("Resources","ExcelOutputAsset",name+".json")));
        return data;
    }
    private static GrowthConfig Growth
    {
        get
        {
            if(_growth!=null)return _growth;
            var path="adventure-growth.json";
            var cfg=File.Exists(path)?JsonConvert.DeserializeObject<GrowthConfig>(File.ReadAllText(path)):new GrowthConfig();
            cfg ??= new GrowthConfig();
            cfg.Need ??= Table("AdventureData").OrderBy(x=>(int)x["AdventureLevel"]).Take(38).Select(x=>(uint)x["BadgeNeed"]).Concat(new uint[]{16,20,20,20,20,20}).ToArray();
            if(cfg.Need.Length!=44 || cfg.Need.Any(n=>n==0) || cfg.NormalA?.Length!=6 || cfg.SpA?.Length!=6 || cfg.InitialS?.Length!=6 || cfg.ACap!=180)
                throw new InvalidDataException("Invalid adventure-growth.json: need 44 positive thresholds and 6-entry rank weights; A cap must be 180.");
            _growth=cfg;
            Packet.c.Warn("[Adventure] approximate-7.0 growth enabled; documented weights/tail thresholds are reconstructed, not official V2 tables.");
            return cfg;
        }
    }
    internal static void Prepare() { _=Store; _=Growth; }
    private static long Now => Clock().ToUnixTimeSeconds();
    private static long Day => (Now+4*3600)/86400;
    private static JToken AvatarConfig(uint id)=>Table("AvatarData").FirstOrDefault(x=>(uint)x["avatarID"]==id);
    private static bool Eligible(JToken c)=>c!=null && (uint)c["unlockStar"]>=2 && !(bool)c["IsCollaboration"];
    internal static uint Contribution(uint id,uint star,uint sub)
    {
        var c=AvatarConfig(id);
        if(!Eligible(c) || star<(uint)c["unlockStar"] || star>5)return 0;
        var g=Growth;
        if((uint)c["unlockStar"]>=3)
            return Math.Min(g.InitialS[5],g.InitialS[star]+(star<5?Math.Min(sub,3)*g.SubStarExp:0));
        return ((int)c["avatarType"]==2?g.SpA:g.NormalA)[star];
    }
    internal static (uint Level,uint Badge) LevelFor(uint total)
    {
        uint level=1,left=total;
        foreach(uint need in Growth.Need){if(left<need)break;left-=need;level++;}
        return (level,level>=45?0:left);
    }
    private static State Load(Player p)
    {
        var s=Store.All().FirstOrDefault(x=>x.Uid==p.User.Uid);
        if(s==null){s=new State{Uid=p.User.Uid,Tick=Now,Grain=50000};Store.InsertOne(s);}
        foreach(var a in p.AvatarList.Where(a=>a.Star>0))
        {
            if(!Eligible(AvatarConfig(a.AvatarId)))continue;
            if(!s.Ranks.TryGetValue(a.AvatarId,out var old) || a.Star>old.Star || (a.Star==old.Star && a.SubStar>old.Sub))
                s.Ranks[a.AvatarId]=new Rank{Star=a.Star,Sub=a.SubStar};
        }
        ulong av=0,sv=0;
        foreach(var pair in s.Ranks)
        {
            var c=AvatarConfig(pair.Key);uint xp=Contribution(pair.Key,pair.Value.Star,pair.Value.Sub);
            if(c!=null && (uint)c["unlockStar"]>=3)sv+=xp;else av+=xp;
        }
        s.Total=(uint)Math.Min(uint.MaxValue,Math.Min((ulong)Growth.ACap,av)+sv);
        (s.Level,s.Badge)=LevelFor(s.Total);
        var work=Work(s.Level);
        uint cap=(uint)work["AdventureMaxGrain"];
        long hours=Math.Max(0,(Now-s.Tick)/3600);
        if(hours>0)
        {
            s.Grain=(uint)Math.Min(cap,(ulong)s.Grain+(ulong)hours*(uint)work["AdventureGrainRecover"]);
            var coin=Welfare(s.Level,4);
            s.Scoin=(uint)Math.Min((uint)coin["Par2"],(ulong)s.Scoin+(ulong)hours*(uint)coin["Par1"]);
            s.Tick+=hours*3600;
        }
        s.Grain=Math.Min(cap,s.Grain);
        if(s.Day!=Day)
        {
            // Overnight work, including completed but unclaimed work, survives refresh.
            s.Jobs=s.Jobs.Where(x=>x.Value.Start>0&&!x.Value.Taken).ToDictionary(x=>x.Key,x=>x.Value);
            s.Resets=0;s.Day=Day;
            Generate(s,work);
        }
        Store.ReplaceOne(s);
        return s;
    }
    private static JToken Welfare(uint level,int type)=>Table("AdventureWelfare").Where(x=>(int)x["Type"]==type && (uint)x["AdventureLevel"]<=level).OrderByDescending(x=>(uint)x["AdventureLevel"]).First();
    private static JToken Work(uint level)
    {
        uint tier=(uint)Welfare(level,3)["Par1"];
        return Table("AdventureQuestConfig").Single(x=>(uint)x["QuestConfigId"]==tier);
    }
    private static void Generate(State s,JToken work)
    {
        uint tier=(uint)work["QuestConfigId"];int count=(int)work["DailyTimes"];
        if(s.Jobs.Count>=count)return;
        var random=new Random(unchecked((int)(s.Uid*397+s.Day*17)));
        // Shipped tables: PoolId=position+20*(tier-1),
        // QuestId=(position+9)*1000+(tier-1)*10+rarity. All rows are checked in tests.
        var pools=Table("AdventureQuestPool").Where(x=>((uint)x["QuestPoolId"]-(uint)x["MapPosition"])/20+1==tier).OrderBy(_=>random.Next()).ToArray();
        foreach(var pool in pools)
        {
            if(s.Jobs.Values.Any(j=>Position(j.Pool)==(uint)pool["MapPosition"]))continue;
            uint id=(uint)pool["QuestPoolId"];
            if(ChooseQuest(id,random.Next()) is uint quest && quest!=0)s.Jobs[id]=new Job{Pool=id,Quest=quest};
            if(s.Jobs.Count>=count)break;
        }
    }
    private static uint Position(uint pool)=>(uint)Table("AdventureQuestPool").Single(x=>(uint)x["QuestPoolId"]==pool)["MapPosition"];
    private static uint ChooseQuest(uint pool,int seed)
    {
        uint pos=Position(pool);
        uint prefix=(pos+9)*1000+((pool-pos)/20)*10;
        var rows=Table("AdventureQuestData").Where(x=>(uint)x["QuestId"]>prefix && (uint)x["QuestId"]<prefix+10 && (uint)x["Rarity"]<=5).ToArray();
        return rows.Length==0?0:(uint)rows[(int)((uint)seed%(uint)rows.Length)]["QuestId"];
    }
    private static JToken Quest(uint id)=>Table("AdventureQuestData").Single(x=>(uint)x["QuestId"]==id);
    private static bool Done(Job j)=>j.Start>0 && Now>=j.Start+(long)Quest(j.Quest)["TimeCost"];
    internal static GetAdventureGroupRsp Main(Player p)
    {
        lock(p.Equipment)
        lock(Gate)
        {
            Prepare();var s=Load(p);var stamina=StaminaStorageGameplay.Settle(p,s.Level);
            var rsp=new GetAdventureGroupRsp{retcode=0,Level=s.Level,Badge=s.Badge,Grain=s.Grain,HasTakeRewardLevel=s.Taken,NextScoinAddTime=(uint)(s.Tick+3600),StorageScoin=s.Scoin,StorageStamina=stamina.Stored,TodayResetQuestNum=s.Resets};
            foreach(var a in p.AvatarList.Where(a=>a.Star>0 && AvatarConfig(a.AvatarId)!=null && !(bool)AvatarConfig(a.AvatarId)["IsCollaboration"]))
                rsp.AvatarLists.Add(new AdventureGroupAvatar{AvatarId=a.AvatarId,FinishedRecordLists=Array.Empty<uint>()});
            foreach(var j in s.Jobs.Values)
            {
                if(j.Start==0 && !j.Taken)rsp.AvailableQuestLists.Add(new AdventureGroupAvailableQuest{PoolId=j.Pool,QuestId=j.Quest});
                // Native 1825B4280 only sends claim for status1; 1829E31F0 checks
                // its deadline locally. Status2 disables the claim button.
                if(j.Start>0 && !j.Taken)rsp.QuestLists.Add(new AdventureGroupQuest{PoolId=j.Pool,QuestId=j.Quest,AcceptTime=(uint)j.Start,AvatarLists=j.Avatars,ElfId=j.Elf,Status=1u});
            }
            // This local server has no legitimate pre-7.0 island migration data.
            rsp.HasTakeCompensationLists=new uint[]{1,2};
            return rsp;
        }
    }
    internal static void Push(Session s)=>s.Send(Packet.FromProto(Main(s.Player),CmdId.GetAdventureGroupRsp));
    private static void Atomic(Player p,Action action)
    {
        string user=JsonConvert.SerializeObject(p.User),equipment=JsonConvert.SerializeObject(p.Equipment),avatars=JsonConvert.SerializeObject(p.AvatarList);
        try{Global.Store.InTransaction(action);}
        catch{p.User=JsonConvert.DeserializeObject<UserScheme>(user);JsonConvert.PopulateObject(equipment,p.Equipment,new JsonSerializerSettings{ObjectCreationHandling=ObjectCreationHandling.Replace});p.AvatarList=JsonConvert.DeserializeObject<AvatarScheme[]>(avatars);throw;}
    }
    private static Dictionary<uint,uint> Traits(uint id,uint star,bool elf)
    {
        var map=new Dictionary<uint,uint>();
        var rows=Table(elf?"AdventureElfUnlockData":"AdventureAvatarUnlockData")
            .Where(x=>(uint)x[elf?"ElfID":"AvatarID"]==id).ToArray();
        // 7.0: initial B/A battlesuits and A-rank ELFs use their maximum
        // job traits, regardless of their present promotion tier. S ranks
        // still use the owned tier; never grant another ELF's skill rows.
        var config=elf?Table("ElfData").FirstOrDefault(x=>(uint)x["ElfID"]==id):AvatarConfig(id);
        bool maximum=config!=null && (elf?(uint)config["UnlockStar"]==1:(uint)config["unlockStar"]<3);
        if(maximum && rows.Length>0)star=rows.Max(x=>(uint)x["Unlock"]);
        foreach(var row in rows.Where(x=>(uint)x["Unlock"]<=star))
        {
            var skill=Table("AdventureAvatarSkillLevel").FirstOrDefault(x=>(uint)x["ID"]==(uint)row["Skill"]);
            if(skill==null)continue;uint type=(uint)skill["AvatarSkillID"],value=(uint)skill["level"];
            if(type<201||type>206)continue;
            map[type]=Math.Max(map.GetValueOrDefault(type),value);
        }
        return map;
    }
    internal static SelectAdventureQuestRsp Select(Player p,SelectAdventureQuestReq req)
    {
        lock(p.Equipment)
        lock(Gate)
        {
            Prepare();var s=Load(p);var rsp=new SelectAdventureQuestRsp();
            SelectAdventureQuestRsp Fail(SelectAdventureQuestRsp.Retcode c){rsp.retcode=c;return rsp;}
            if(!s.Jobs.TryGetValue(req.PoolId,out var j))return Fail(SelectAdventureQuestRsp.Retcode.Fail);
            if(j.Taken)return Fail(SelectAdventureQuestRsp.Retcode.HasTake);
            if(j.Start>0)return Fail(SelectAdventureQuestRsp.Retcode.NotFinished);
            var ids=req.AvatarLists??Array.Empty<uint>();var cfg=Quest(j.Quest);
            if(ids.Distinct().Count()!=ids.Length || ids.Length<(int)cfg["MinMember"] || ids.Length>(int)cfg["MaxMember"])return Fail(SelectAdventureQuestRsp.Retcode.AvatarNumError);
            var owned=p.AvatarList.Where(a=>a.Star>0).ToDictionary(a=>a.AvatarId);
            if(ids.Any(id=>!owned.ContainsKey(id)))return Fail(SelectAdventureQuestRsp.Retcode.AvatarLock);
            if(s.Jobs.Values.Any(x=>!x.Taken&&x.Start>0&&x.Avatars.Intersect(ids).Any()))return Fail(SelectAdventureQuestRsp.Retcode.AvatarInQuest);
            var elf=req.ElfId==0?null:p.Elfs.Elves.FirstOrDefault(e=>e.ElfId==req.ElfId);
            if(req.ElfId!=0 && elf==null)return Fail(SelectAdventureQuestRsp.Retcode.ElfLock);
            if(elf!=null && s.Jobs.Values.Any(x=>!x.Taken&&x.Start>0&&x.Elf==elf.ElfId))return Fail(SelectAdventureQuestRsp.Retcode.ElfInQuest);
            var traits=new Dictionary<uint,uint>();
            foreach(var id in ids)foreach(var kv in Traits(id,owned[id].Star,false))traits[kv.Key]=traits.GetValueOrDefault(kv.Key)+kv.Value;
            if(elf!=null)foreach(var kv in Traits(elf.ElfId,elf.Star,true))traits[kv.Key]=traits.GetValueOrDefault(kv.Key)+kv.Value;
            if(cfg["NeedTraitList"].Any(t=>traits.GetValueOrDefault((uint)t["SkillID"])<(uint)t["SkillStrength"]))return Fail(SelectAdventureQuestRsp.Retcode.SkillLack);
            uint cost=(uint)cfg["GrainCost"];
            if(s.Grain<cost)return Fail(SelectAdventureQuestRsp.Retcode.LackGrain);
            s.Grain-=cost;j.Start=Now;j.Avatars=ids.ToArray();j.Elf=req.ElfId;
            Store.ReplaceOne(s);return rsp;
        }
    }
    private static bool RewardSupported(RewardDataExcel cfg)
    {
        for(int i=1;i<=6;i++)
        {
            int id=(int)typeof(RewardDataExcel).GetProperty("RewardItem"+i+"Id").GetValue(cfg);
            if(id==0)continue;
            // Every shipped base job/level award currently uses ordinary materials.
            // Do not silently consume an award if a future table introduces an unsupported item.
            if(BaseExcelReader<MaterialData,MaterialDataExcel>.GetInstance().FromId((uint)id)==null ||
                BaseExcelReader<AvatarData,AvatarDataExcel>.GetInstance().All.Any(x=>x.AvatarFragmentId==id))return false;
        }
        return true;
    }
    internal static void NotifyInventory(Session s)
    {
        var p=s.Player;
        s.Send(Packet.FromProto(new GetMainDataRsp{retcode=0,Hcoin=(uint)p.User.Hcoin,Scoin=p.Equipment.MaterialList.FirstOrDefault(x=>x.Id==100)?.Num??0,Stamina=(uint)p.User.Stamina},CmdId.GetMainDataRsp));
        s.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(),CmdId.GetEquipmentDataReq));
        if(File.Exists("Resources/ExcelOutputAsset/MissionData.json"))
        {
            s.Send(Packet.FromProto(MissionDataBuilder.Build(p),CmdId.GetMissionDataRsp));
            DailyGameplay.PushDuty(s);BattlePassGameplay.Push(s);
        }
    }
    internal static TakeAdventureQuestRewardRsp TakeQuest(Player p,uint pool)
    {
        lock(p.Equipment)
        lock(Gate)
        {
            Prepare();var s=Load(p);var rsp=new TakeAdventureQuestRewardRsp{PoolId=pool};
            if(!s.Jobs.TryGetValue(pool,out var j)||j.Start==0){rsp.retcode=TakeAdventureQuestRewardRsp.Retcode.NoQuest;return rsp;}
            rsp.QuestId=j.Quest;
            if(j.Taken){rsp.retcode=TakeAdventureQuestRewardRsp.Retcode.HasTake;return rsp;}
            if(!Done(j)){rsp.retcode=TakeAdventureQuestRewardRsp.Retcode.NotEnd;return rsp;}
            var cfg=BaseExcelReader<Common.Utils.ExcelReader.RewardData,RewardDataExcel>.GetInstance().FromId((int)Quest(j.Quest)["QuestDropShow"]);
            if(cfg==null || !RewardSupported(cfg)){rsp.retcode=TakeAdventureQuestRewardRsp.Retcode.Fail;return rsp;}
            DailyGameplay.Plan plan;
            try{plan=DailyGameplay.Reward((int)Quest(j.Quest)["QuestDropShow"]);DailyGameplay.Validate(p,plan);}
            catch(Exception ex){Packet.c.Warn("[Adventure] claim validation: "+ex.Message);rsp.retcode=TakeAdventureQuestRewardRsp.Retcode.Fail;return rsp;}
            Atomic(p,()=>
            {
                var reward=DailyGameplay.Grant(p,plan);
                foreach(var item in reward.ItemLists)rsp.DropItemLists.Add(new DropItem{ItemId=item.Id,Num=item.Num});
                if(reward.Hcoin>0)rsp.DropItemLists.Add(new DropItem{ItemId=101,Num=reward.Hcoin});
                if(reward.Scoin>0)rsp.DropItemLists.Add(new DropItem{ItemId=100,Num=reward.Scoin});
                DailyGameplay.Add(p,10494,0);j.Taken=true;Store.ReplaceOne(s);p.User.Save();p.Equipment.Save();Common.Database.Avatar.SaveBulk(p.AvatarList);
            });
            return rsp;
        }
    }
    internal static TakeAdventureLevelRewardRsp TakeLevel(Player p)
    {
        lock(p.Equipment)
        lock(Gate)
        {
            Prepare();var s=Load(p);var rsp=new TakeAdventureLevelRewardRsp{HasTakeRewardLevel=s.Taken};
            if(s.Taken>=s.Level){rsp.retcode=TakeAdventureLevelRewardRsp.Retcode.HasTake;return rsp;}
            var ids=Table("AdventureData").Where(x=>(uint)x["AdventureLevel"]>s.Taken && (uint)x["AdventureLevel"]<=s.Level && (uint)x["Reward"]>0).Select(x=>(int)x["Reward"]).ToArray();
            var reader=BaseExcelReader<Common.Utils.ExcelReader.RewardData,RewardDataExcel>.GetInstance();
            if(ids.Any(id=>reader.FromId(id)==null || !RewardSupported(reader.FromId(id)))){rsp.retcode=TakeAdventureLevelRewardRsp.Retcode.Fail;return rsp;}
            DailyGameplay.Plan[] plans;
            try{plans=ids.Select(DailyGameplay.Reward).ToArray();DailyGameplay.Validate(p,DailyGameplay.Combine(plans));}
            catch(Exception ex){Packet.c.Warn("[Adventure] level claim validation: "+ex.Message);rsp.retcode=TakeAdventureLevelRewardRsp.Retcode.Fail;return rsp;}
            Atomic(p,()=>
            {
                foreach(var plan in plans)rsp.RewardLists.Add(DailyGameplay.Grant(p,plan));
                s.Taken=s.Level;Store.ReplaceOne(s);p.User.Save();p.Equipment.Save();Common.Database.Avatar.SaveBulk(p.AvatarList);
            });
            rsp.HasTakeRewardLevel=s.Taken;return rsp;
        }
    }
    internal static CancelAdventureQuestRsp Cancel(Player p,uint pool)
    {
        lock(p.Equipment)
        lock(Gate)
        {
            Prepare();var s=Load(p);var rsp=new CancelAdventureQuestRsp{PoolId=pool};
            if(!s.Jobs.TryGetValue(pool,out var j)||j.Start==0||j.Taken){rsp.retcode=CancelAdventureQuestRsp.Retcode.NotSelected;return rsp;}
            if(Done(j)){rsp.retcode=CancelAdventureQuestRsp.Retcode.OverTime;return rsp;}
            // Conservative: cancelled work does not refund consumed rice balls.
            j.Start=0;j.Avatars=Array.Empty<uint>();j.Elf=0;Store.ReplaceOne(s);return rsp;
        }
    }
    internal static ResetAdventureQuestRsp Reset(Player p,uint pool)
    {
        lock(p.Equipment)
        lock(Gate)
        {
            Prepare();var s=Load(p);var rsp=new ResetAdventureQuestRsp{PoolId=pool};
            if(!s.Jobs.TryGetValue(pool,out var j)){rsp.retcode=ResetAdventureQuestRsp.Retcode.NoQuest;return rsp;}
            if(j.Taken){rsp.retcode=ResetAdventureQuestRsp.Retcode.HasFinished;return rsp;}
            if(j.Start>0){rsp.retcode=ResetAdventureQuestRsp.Retcode.HasSelected;return rsp;}
            if(s.Resets>=(uint)Work(s.Level)["QuestRefreshTimes"]){rsp.retcode=ResetAdventureQuestRsp.Retcode.NoTimes;return rsp;}
            uint next=ChooseQuest(pool,unchecked((int)(s.Uid+s.Day+s.Resets+1)));
            if(next==0){rsp.retcode=ResetAdventureQuestRsp.Retcode.Fail;return rsp;}
            if(next==j.Quest)next=ChooseQuest(pool,unchecked((int)(s.Uid+s.Day+s.Resets+2)));
            j.Quest=next;s.Resets++;Store.ReplaceOne(s);return rsp;
        }
    }

    // Called before processing every authenticated packet, including heartbeat.
    // This settles the old balance before stage costs/rewards change it, and
    // updates the native UI while staying online without reopening a panel.
    internal static (GetMainDataRsp Main,GetAdventureGroupRsp Adventure) StaminaUpdate(Player p)
    {
        lock(p.Equipment)lock(Gate)
        {
            if(!StaminaStorageGameplay.Due(p))return (null,null);
            int before=p.User.Stamina;uint stored=StaminaStorageGameplay.Balance(p);
            var group=Main(p);
            GetMainDataRsp update=before==p.User.Stamina?null:new GetMainDataRsp{retcode=0,Stamina=(uint)p.User.Stamina,StaminaRecoverConfigTime=360,StaminaRecoverLeftTime=StaminaStorageGameplay.RecoverLeft(p)};
            return(update,stored==group.StorageStamina?null:group);
        }
    }
    internal static void TickStamina(Session session)
    {
        var update=StaminaUpdate(session.Player);
        if(update.Main!=null)session.Send(Packet.FromProto(update.Main,CmdId.GetMainDataRsp));
        if(update.Adventure!=null)session.Send(Packet.FromProto(update.Adventure,CmdId.GetAdventureGroupRsp));
    }
    internal static void SettleStamina(Player p){lock(p.Equipment)lock(Gate){Prepare();var state=Load(p);StaminaStorageGameplay.Settle(p,state.Level);}}
    internal static ClaimStaminaRsp ClaimStamina(Player p,uint amount){lock(p.Equipment)lock(Gate){Prepare();var state=Load(p);return StaminaStorageGameplay.Claim(p,state.Level,amount);}}
    internal static ClaimScoinRsp ClaimCoins(Player p)
    {
        lock(p.Equipment)
        lock(Gate)
        {
            Prepare();var s=Load(p);var rsp=new ClaimScoinRsp();
            uint balance=p.Equipment.MaterialList.FirstOrDefault(m=>m.Id==100)?.Num??0;
            if(balance>int.MaxValue-s.Scoin){rsp.retcode=ClaimScoinRsp.Retcode.ScoinFull;return rsp;}
            rsp.AddScoin=s.Scoin;
            if(s.Scoin==0)return rsp;
            Atomic(p,()=>{p.Equipment.AddMaterial(100,(int)s.Scoin);DailyGameplay.Add(p,495,0);s.Scoin=0;Store.ReplaceOne(s);p.Equipment.Save();});
            return rsp;
        }
    }
}
