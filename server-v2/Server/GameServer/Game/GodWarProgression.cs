using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PemukulPaku.GameServer.Game;

// Persistent meta progression only. This does not substitute empty successes
// for the still-missing GodWar event and in-run combat engine.
internal static partial class GodWarProgression
{
    internal static Func<DateTimeOffset> Clock = () => DateTimeOffset.UtcNow;
    private static readonly object Gate = new();
    private static readonly Dictionary<string,JArray> Tables = new();
    private static SqlTable<State> Store => Global.Table<State>("GodWarProgressStates",("owner_uid","INTEGER",s=>s.Uid,true));
    public sealed class State : IRow
    {
        public long Id {get;set;}
        public long CreatedAt {get;set;}
        public uint Uid {get;set;}
        public Dictionary<uint,Role> Roles {get;set;} = new();
        public Dictionary<uint,uint> Talents {get;set;} = new();
        public Dictionary<uint,ChallengeRun> Runs {get;set;} = new();
        public HashSet<string> DeepFirstWins {get;set;} = new();
        public Dictionary<string,DeepHistory> DeepHistory {get;set;} = new();
        public long DeepWeek {get;set;} = -1;
        public uint DeepBest {get;set;}
        public HashSet<uint> DeepClaimed {get;set;} = new();
        public HashSet<uint> StoryDone {get;set;} = new();
        public HashSet<uint> StoryClaimed {get;set;} = new();
        public HashSet<uint> SkippedStoryNodes {get;set;} = new();
        public HashSet<string> LobbyActions {get;set;} = new();
        public HashSet<string> LobbyFollowEvents {get;set;} = new();
        public HashSet<uint> LobbyEffectReceipts {get;set;} = new();
        public HashSet<uint> Collections {get;set;} = new();
        public HashSet<uint> SeenBuffs {get;set;} = new();
        public LobbyDialogue LobbyDialogue {get;set;}
        public PlotBattle PendingPlotBattle {get;set;}
        public HashSet<uint> PlotClears {get;set;} = new();
        public uint Chapter {get;set;} = 1;
        public long TicketWeek {get;set;} = -1;
        public HashSet<uint> ClearedStages {get;set;} = new();
        public Dictionary<uint,HashSet<uint>> StageChallenges {get;set;} = new();
        public uint PendingStage {get;set;}
        public uint PendingAvatar {get;set;}
        public bool PendingLightningSupport {get;set;}
        public uint LightningSupportWins {get;set;}
        public long PendingSince {get;set;}
        public long RewardWeek {get;set;} = -1;
        public Dictionary<uint,uint> WeeklyEarned {get;set;} = new();
    }
    public sealed class Role
    {
        public uint Level {get;set;}
        public uint Exp {get;set;}
        public uint RewardLevel {get;set;}
        public HashSet<uint> Stories {get;set;} = new();
    }
    private static JArray Table(string name)
    {
        lock(Gate)
        {
            if(!Tables.TryGetValue(name,out var a))
            {
                a=JArray.Parse(File.ReadAllText(Path.Combine("Resources","ExcelOutputAsset",name+".json")));
                CompleteEntryTable(name,a);
                if(name=="GodWarRelationData") CompleteVesselLevels(a);
                Tables[name]=a;
            }
            return a;
        }
    }
    private static long Week => (DailyGameplay.Day(Clock().ToUnixTimeSeconds())+3)/7;
    private static JToken[] Rows(uint role)=>Table("GodWarRelationData").Where(x=>(uint)x["GodWarID"]==1&&(uint)x["RoleID"]==role).OrderBy(x=>(uint)x["Level"]).ToArray();
    private static State Load(UserScheme user)
    {
        var s=Store.FirstOrDefault(x=>x.Uid==user.Uid);
        if(s!=null){RestoreMissingEntryRoles(s);foreach(var pair in s.Runs){var run=pair.Value;
            // Old pause exits were marked failed despite positive saved living HP.
            // True all-dead exits are recorded with zero HP and remain terminal.
            if(run.Deep is {} d&&d.Phase=="failed"&&run.Challenge.AvatarLists.Count==1&&run.Challenge.AvatarLists[0].HpPercent>0){d.Phase="ready";run.Challenge.IsChallengeFail=false;}
            PrepareDeepEffects(s,run,pair.Key,false);}return s;}
        s=new State{Uid=user.Uid};
        // Preserve the levels previously advertised by this local server;
        // do not erase existing saved levels, EXP, or claimed rewards.
        foreach(var group in Table("GodWarRelationData").Where(x=>(uint)x["GodWarID"]==1).GroupBy(x=>(uint)x["RoleID"]))
        {
            var old=user.GodWarRoleRelations?.FirstOrDefault(x=>x.RoleId==group.Key);
            s.Roles[group.Key]=new Role{Level=old?.Level??(EntryRoles.Any(x=>x.Role==group.Key)?0u:group.Min(x=>(uint)x["Level"])),Exp=old?.Exp??0,RewardLevel=old?.RewardHasTakeLevel??0,Stories=(old?.RewardHasTakeStoryLists??Array.Empty<uint>()).ToHashSet()};
        }
        Store.InsertOne(s);return s;
    }
    private static bool Commit(Player p,State state,Action action)
    {
        // Schema initialization must precede the outer inventory transaction.
        _=Store;
        return DailyGameplay.Atomic(p,()=>{action();Store.ReplaceOne(state);});
    }
    private static uint Held(Player p,uint id)=>p.Equipment.MaterialList.FirstOrDefault(x=>x.Id==id)?.Num??0;
    private static void Debit(Player p,uint id,uint n)
    {
        if(n==0)return;
        var m=p.Equipment.MaterialList.FirstOrDefault(x=>x.Id==id);
        if(m==null||m.Num<n)throw new InvalidOperationException("GodWar material balance changed");
        m.Num-=n;
    }
    internal static GodWarRoleInfo RoleInfo(Player player)
    {
        lock(Gate)
        {
            if(player==null)return new GodWarRoleInfo();
            var state=Load(player.User);
            var info=new GodWarRoleInfo{
                MainAvatarIdLists=Table("GodWarMainAvatar").Select(x=>(uint)x["MainAvatarID"]).Distinct().ToArray(),
                SupportAvatarIdLists=Table("GodWarSupportAvatar").Select(x=>(uint)x["SupportAvatarID"]).Distinct().ToArray(),
                AvatarExpUp=new GodWarAvatarExpUp{BeginTime=0,EndTime=uint.MaxValue,AvatarIdLists=Array.Empty<uint>(),UpRatio=0}};
            foreach(var pair in state.Roles.OrderBy(x=>x.Key))info.RoleRelationLists.Add(new GodWarRoleRelation{RoleId=pair.Key,Level=pair.Value.Level,Exp=pair.Value.Exp,RewardHasTakeLevel=VesselClaimLevel(player,pair.Key,pair.Value),RewardHasTakeStoryLists=pair.Value.Stories.OrderBy(x=>x).ToArray()});
            return info;
        }
    }
    internal static void Apply(Player p,GodWar realm)
    {
        lock(p.Equipment)lock(Gate)
        {
            var s=Load(p.User);realm.CurChapterId=s.Chapter;
            realm.LobbyId=(uint)Table("GodWarChapter").Single(x=>(uint)x["ChapterID"]==s.Chapter)["LobbyID"];
            realm.MaxSupportPoint=Points(s);
            ApplyChallengeSetups(s,realm);
            ApplyDeepHistory(s,realm);
            var weeklyTale = realm.TaleLists.FirstOrDefault(tale => tale.TaleId == 30);
            if (weeklyTale != null)
            {
                uint best = s.DeepWeek == Week ? s.DeepBest : 0u;
                ProtoBuf.Extensible.AppendValue(weeklyTale, 13, best);
                ProtoBuf.Extensible.AppendValue(weeklyTale, 14, new WeeklyScheduleScore { MaxChallengeScore = best });
            }
            realm.TalentLists.Clear();
            foreach(var x in s.Talents.OrderBy(x=>x.Key))realm.TalentLists.Add(new GodWarTalent{TalentId=x.Key,TalentLevel=x.Value});
        }
    }
    [ProtoBuf.ProtoContract]
    private sealed class WeeklyScheduleScore
    {
        [ProtoBuf.ProtoMember(1)]
        public uint MaxChallengeScore { get; set; }
    }

    internal static void Push(Session session)
    {
        DailyGameplay.PushInventory(session);
        session.Send(Packet.FromProto(new GetGodWarRoleRsp{retcode=0,GodWarId=1,RoleInfo=RoleInfo(session.Player)},CmdId.GetGodWarRoleRsp));
        session.Send(Packet.FromProto(GodWarService.Build(session.Player),CmdId.GetGodWarRsp));
    }
    private static uint Points(State s)=>checked((uint)s.Talents.Sum(pair=>(long)(uint)Table("GodWarTalentLevelData").Single(x=>(uint)x["TalentID"]==pair.Key&&(uint)x["TalentLevel"]==pair.Value)["TalentPoint"]));
    internal static AddGodWarRoleRelationExpRsp AddExp(Player p,AddGodWarRoleRelationExpReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new AddGodWarRoleRelationExpRsp{GodWarId=req.GodWarId};
            if(req.GodWarId!=1){rsp.retcode=AddGodWarRoleRelationExpRsp.Retcode.FeatureClose;return rsp;}
            var s=Load(p.User);var rows=Rows(req.RoleId);
            if(rows.Length==0||!s.Roles.TryGetValue(req.RoleId,out var role)){rsp.retcode=AddGodWarRoleRelationExpRsp.Retcode.RoleError;return rsp;}
            var row=rows.FirstOrDefault(x=>(uint)x["Level"]==role.Level);
            if(row==null||!rows.Any(x=>(uint)x["Level"]==role.Level+1)||role.Exp>=(uint)row["Exp"]){rsp.retcode=AddGodWarRoleRelationExpRsp.Retcode.ExpFull;return rsp;}
            uint count=req.ExpMaterialNum,id=(uint)row["ExpMaterialID"];
            if(count==0){rsp.retcode=AddGodWarRoleRelationExpRsp.Retcode.Fail;return rsp;}
            // One dedicated EXP material represents one relation EXP. Never
            // consume the excess requested beyond the current upgrade bar.
            count=Math.Min(count,(uint)row["Exp"]-role.Exp);
            if(Held(p,id)<count){rsp.retcode=AddGodWarRoleRelationExpRsp.Retcode.MaterialLack;return rsp;}
            if(!Commit(p,s,()=>{Debit(p,id,count);role.Exp+=count;}))rsp.retcode=AddGodWarRoleRelationExpRsp.Retcode.Fail;
            return rsp;
        }
    }
    internal static LevelUpGodWarRoleRelationRsp.Retcode LevelUp(Player p,uint roleId)
    {
        lock(p.Equipment)lock(Gate)
        {
            var s=Load(p.User);var rows=Rows(roleId);
            if(!s.Roles.TryGetValue(roleId,out var role))return LevelUpGodWarRoleRelationRsp.Retcode.RoleError;
            var current=rows.FirstOrDefault(x=>(uint)x["Level"]==role.Level);
            var next=rows.FirstOrDefault(x=>(uint)x["Level"]==role.Level+1);
            if(next==null)return LevelUpGodWarRoleRelationRsp.Retcode.RoleError;
            if(role.Level==0&&(uint)next["RoleType"]==2&&!p.AvatarList.Any(x=>x.AvatarId==(uint)next["AvatarID"]&&x.Star>0))return LevelUpGodWarRoleRelationRsp.Retcode.RoleError;
            uint need=current==null?0u:(uint)current["Exp"];
            if(role.Exp<need)return LevelUpGodWarRoleRelationRsp.Retcode.ExpLack;
            // Native 180B5B450/180B5BF20 selects Level+1; the unlock dialog
            // 181321E90 reads LvUpMaterial from that next-level row.
            uint id=(uint)next["LvUpMaterial"]["MaterialID"],count=(uint)next["LvUpMaterial"]["Num"];
            if(Held(p,id)<count)return LevelUpGodWarRoleRelationRsp.Retcode.MaterialLack;
            return Commit(p,s,()=>{Debit(p,id,count);role.Exp-=need;role.Level++;})?LevelUpGodWarRoleRelationRsp.Retcode.Succ:LevelUpGodWarRoleRelationRsp.Retcode.Fail;
        }
    }
    internal static TakeGodWarRoleRelationLevelRewardRsp TakeLevels(Player p,TakeGodWarRoleRelationLevelRewardReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new TakeGodWarRoleRelationLevelRewardRsp{GodWarId=req.GodWarId};
            if(req.GodWarId!=1){rsp.retcode=TakeGodWarRoleRelationLevelRewardRsp.Retcode.FeatureClose;return rsp;}
            var s=Load(p.User);
            if(!s.Roles.TryGetValue(req.RoleId,out var role)){rsp.retcode=TakeGodWarRoleRelationLevelRewardRsp.Retcode.RoleError;return rsp;}
            var rewards=Rows(req.RoleId).Where(x=>(uint)x["Level"]>role.RewardLevel&&(uint)x["Level"]<=role.Level&&(int)x["RewardID"]!=0).Select(x=>(int)x["RewardID"]).ToArray();
            var missing = MissingVesselSigils(p,req.RoleId,role).ToArray();
            if(rewards.Length==0 && missing.Length==0){rsp.retcode=TakeGodWarRoleRelationLevelRewardRsp.Retcode.NoReward;return rsp;}
            DailyGameplay.Plan[] plans;
            try
            {
                var grants = rewards.Select(DailyGameplay.Reward).ToList();
                var eligibleSigils = VesselSigils(req.RoleId);
                foreach(var plan in grants)
                    foreach(var item in eligibleSigils.Where(x=>Held(p,x.Id)>0))
                        plan.Items.Remove((int)item.Id);
                var supplement = new DailyGameplay.Plan();
                foreach(var item in missing)
                    if(!grants.Any(plan=>plan.Items.ContainsKey((int)item.Id)))
                        supplement.Items.Add((int)item.Id,1);
                if(supplement.Items.Count>0) grants.Add(supplement);
                plans=grants.ToArray();
                DailyGameplay.Validate(p,DailyGameplay.Combine(plans));
            }
            catch(Exception ex){Packet.c.Warn("[GodWar] role reward validation: "+ex.Message);rsp.retcode=TakeGodWarRoleRelationLevelRewardRsp.Retcode.Fail;return rsp;}
            if(!Commit(p,s,()=>{foreach(var plan in plans)rsp.RewardLists.Add(DailyGameplay.Grant(p,plan));role.RewardLevel=role.Level;}))
            {rsp.RewardLists.Clear();rsp.retcode=TakeGodWarRoleRelationLevelRewardRsp.Retcode.Fail;}
            return rsp;
        }
    }
    private static bool MissionsClosed(Player p,IEnumerable<uint> ids)
    {
        var needed=ids.Where(x=>x!=0).ToHashSet();if(needed.Count==0)return true;
        var response=MissionDataBuilder.Build(p);
        needed.ExceptWith(response.MissionLists.Where(x=>x.Status==MissionStatus.MissionClose).Select(x=>x.MissionId));
        needed.ExceptWith(response.CloseMissionLists??Array.Empty<uint>());return needed.Count==0;
    }
    internal static TakeGodWarRoleStoryRewardRsp TakeStory(Player p,TakeGodWarRoleStoryRewardReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new TakeGodWarRoleStoryRewardRsp{GodWarId=req.GodWarId,StoryId=req.StoryId};
            if(req.GodWarId!=1){rsp.retcode=TakeGodWarRoleStoryRewardRsp.Retcode.FeatureClose;return rsp;}
            var row=Table("GodWarRoleStoryData").FirstOrDefault(x=>(uint)x["StoryID"]==req.StoryId);
            if(row==null){rsp.retcode=TakeGodWarRoleStoryRewardRsp.Retcode.Fail;return rsp;}
            var s=Load(p.User);
            if(!s.Roles.TryGetValue((uint)row["RoleID"],out var role)||role.Level<(uint)row["NeedRelationLevel"]){rsp.retcode=TakeGodWarRoleStoryRewardRsp.Retcode.RelationLevelLack;return rsp;}
            if(role.Stories.Contains(req.StoryId)){rsp.retcode=TakeGodWarRoleStoryRewardRsp.Retcode.HasTake;return rsp;}
            if(!MissionsClosed(p,row["UnlockMissionList"].Values<uint>())){rsp.retcode=TakeGodWarRoleStoryRewardRsp.Retcode.MissionUnfinish;return rsp;}
            DailyGameplay.Plan plan;
            try{plan=DailyGameplay.Reward((int)row["RewardID"]);DailyGameplay.Validate(p,plan);}
            catch(Exception ex){Packet.c.Warn("[GodWar] story reward validation: "+ex.Message);rsp.retcode=TakeGodWarRoleStoryRewardRsp.Retcode.Fail;return rsp;}
            if(!Commit(p,s,()=>{rsp.Reward=DailyGameplay.Grant(p,plan);role.Stories.Add(req.StoryId);})){rsp.Reward=null;rsp.retcode=TakeGodWarRoleStoryRewardRsp.Retcode.Fail;}
            return rsp;
        }
    }
    internal static LevelUpGodWarTalentRsp Talent(Player p,LevelUpGodWarTalentReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new LevelUpGodWarTalentRsp{GodWarId=req.GodWarId};
            if(req.GodWarId!=1){rsp.retcode=LevelUpGodWarTalentRsp.Retcode.FeatureClose;return rsp;}
            var meta=Table("GodWarTalentData").FirstOrDefault(x=>(uint)x["GodWarID"]==1&&(uint)x["TalentID"]==req.TalentId);
            if(meta==null){rsp.retcode=LevelUpGodWarTalentRsp.Retcode.TalentError;return rsp;}
            var s=Load(p.User);uint old=s.Talents.GetValueOrDefault(req.TalentId),next=old;
            if(old>=(uint)meta["MaxLevel"]){rsp.retcode=LevelUpGodWarTalentRsp.Retcode.LevelFull;return rsp;}
            var costs=new Dictionary<uint,uint>();
            do
            {
                var row=Table("GodWarTalentLevelData").SingleOrDefault(x=>(uint)x["TalentID"]==req.TalentId&&(uint)x["TalentLevel"]==next+1);
                if(row==null){rsp.retcode=LevelUpGodWarTalentRsp.Retcode.TargetLevelError;return rsp;}
                // Never bypass an unimplemented prerequisite in future data.
                if(row["LvUpFlagList"].Any()){if(next==old){rsp.retcode=LevelUpGodWarTalentRsp.Retcode.FlagInvalid;return rsp;}break;}
                var trial=new Dictionary<uint,uint>(costs);
                foreach(var c in row["UpGradeMaterialList"]){uint id=(uint)c["MaterialID"];trial[id]=checked(trial.GetValueOrDefault(id)+(uint)c["Cost"]);}
                if(trial.Any(c=>Held(p,c.Key)<c.Value)){if(next==old){rsp.retcode=LevelUpGodWarTalentRsp.Retcode.MaterialLack;return rsp;}break;}
                costs=trial;next++;
            }while(req.IsLevelUpAll&&next<(uint)meta["MaxLevel"]);
            if(!Commit(p,s,()=>{foreach(var c in costs)Debit(p,c.Key,c.Value);s.Talents[req.TalentId]=next;})){rsp.retcode=LevelUpGodWarTalentRsp.Retcode.Fail;return rsp;}
            rsp.Talent=new GodWarTalent{TalentId=req.TalentId,TalentLevel=next};rsp.MaxSupportPoint=Points(s);return rsp;
        }
    }
    internal static RefreshGodWarTicketRsp Tickets(Player p,RefreshGodWarTicketReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new RefreshGodWarTicketRsp{GodWarId=req.GodWarId};
            if(req.GodWarId!=1){rsp.retcode=RefreshGodWarTicketRsp.Retcode.FeatureClose;return rsp;}
            var s=Load(p.User);long week=Week;
            if(s.TicketWeek>=week)return rsp;
            var rows=Table("GodWarTicket").ToArray();
            var adds=rows.Select(x=>new RefreshGodWarTicketInfo{TicketId=(uint)x["TicketID"],MaterialId=(uint)x["MaterialID"],AddNum=Held(p,(uint)x["MaterialID"])<(uint)x["MaxNum"]?1u:0u}).Where(x=>x.AddNum>0).ToArray();
            var plan=new DailyGameplay.Plan();foreach(var x in adds)plan.Items[(int)x.MaterialId]=x.AddNum;
            try{if(adds.Length>0)DailyGameplay.Validate(p,plan);}
            catch{rsp.retcode=RefreshGodWarTicketRsp.Retcode.Fail;return rsp;}
            if(!Commit(p,s,()=>{if(adds.Length>0)DailyGameplay.Grant(p,plan);s.TicketWeek=week;})){rsp.retcode=RefreshGodWarTicketRsp.Retcode.Fail;return rsp;}
            rsp.TicketRefreshLists.AddRange(adds);return rsp;
        }
    }
    internal static SwitchGodWarChapterRsp SwitchChapter(Player p,SwitchGodWarChapterReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new SwitchGodWarChapterRsp{GodWarId=req.GodWarId,ChapterId=req.ChapterId};
            if(req.GodWarId!=1){rsp.retcode=SwitchGodWarChapterRsp.Retcode.FeatureClose;return rsp;}
            var row=Table("GodWarChapter").FirstOrDefault(x=>(uint)x["GodWarID"]==1&&(uint)x["ChapterID"]==req.ChapterId);
            if(row==null){rsp.retcode=SwitchGodWarChapterRsp.Retcode.NotOpen;return rsp;}
            var s=Load(p.User);
            if(!Commit(p,s,()=>s.Chapter=req.ChapterId))rsp.retcode=SwitchGodWarChapterRsp.Retcode.Fail;
            return rsp;
        }
    }
}

