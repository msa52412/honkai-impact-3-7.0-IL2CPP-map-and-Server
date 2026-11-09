using System;
using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using RewardData=Common.Resources.Proto.RewardData;
using GodWarBuff=Common.Resources.Proto.GodWarBuff;
using Material=Common.Resources.Proto.Material;

namespace PemukulPaku.GameServer.Game;

internal static partial class GodWarProgression
{
    public sealed class DeepHistory
    {
        public uint Avatar {get;set;}
        public uint Best {get;set;}
        public uint Punish {get;set;}
        public HashSet<uint> Steps {get;set;}=new();
    }
    private static void CapDeepPlan(Player p,DailyGameplay.Plan plan,Dictionary<uint,uint> earned,Dictionary<int,long> already)
    {
        foreach(var item in plan.Items.ToArray())
        {
            var meta=Table("GodWarMaterial").SingleOrDefault(x=>(uint)x["MaterialID"]==item.Key);if(meta==null)continue;
            uint id=(uint)item.Key,cap=(uint)meta["QuantityLimit"];if(cap==0)continue;
            long value=item.Value;
            if((uint)meta["MaterialType"] is 3 or 4){uint used=earned.GetValueOrDefault(id);value=Math.Min(value,cap-Math.Min(cap,used));earned[id]=checked(used+(uint)value);}
            else value=Math.Min(value,Math.Max(0,(long)cap-Held(p,id)-already.GetValueOrDefault(item.Key)));
            if(value==0)plan.Items.Remove(item.Key);else plan.Items[item.Key]=value;
            already[item.Key]=already.GetValueOrDefault(item.Key)+value;
        }
    }
    private static ResetGodWarChallengeRsp SettleDeep(Player p,State s,uint tale,ChallengeRun run)
    {
        var rsp=new ResetGodWarChallengeRsp{GodWarId=1,TaleId=tale,retcode=ResetGodWarChallengeRsp.Retcode.Fail};var d=run.Deep;
        if(Week<s.RewardWeek||Week<s.DeepWeek||d.Phase is not ("site" or "ready" or "fighting" or "buff" or "portal" or "complete" or "failed"))return rsp;
        // Reset can be a deliberate abandonment from the lobby. Only a
        // confirmed stage end permits success; provisional in-level score
        // still awaiting StageEnd must not become a clear by resetting.
        bool complete=(d.Phase=="complete"&&d.PendingStage==0)||HasConfirmedDeepMainClear(run);
        uint score=run.Challenge.ChallengeScore;if(d.PendingStage!=0)score-=Math.Min(score,d.LastScore);
        uint punish=Punish(run),main=run.Challenge.AvatarLists.Single().AvatarId;
        string firstKey=tale+"/"+main+"/"+run.Challenge.StepLevel;uint firstId=complete&&!s.DeepFirstWins.Contains(firstKey)?(uint)DeepStep(run)["PunishStepFirstReward"]:0;
        var row=Table("GodWarPunishReward").Where(x=>(uint)x["PunishLevel"]==punish&&(uint)x["PunishScore"]<=score)
            .Where(x=>MissionsClosed(p,new[]{(uint)x["ShowRewardMission"]})).OrderByDescending(x=>(uint)x["PunishScore"]).FirstOrDefault();
        var normal=new DailyGameplay.Plan();
        if(row!=null)foreach(var item in row["PunishMaterial"]){int id=(int)item["Material"];long n=(long)item["Num"];if(n>0)normal.Items[id]=normal.Items.GetValueOrDefault(id)+n;}
        // Apply the authored normal-settlement material bonus before the shared weekly cap.
        // Fixed first-clear rewards are not modified by this drop-yield rule.
        if(d.YieldRulesVersion==1&&d.StoneYieldBonus>0&&normal.Items.TryGetValue(119703,out long stones))normal.Items[119703]=checked(stones+checked(stones*d.StoneYieldBonus)/100);
        var earned=s.RewardWeek==Week?new Dictionary<uint,uint>(s.WeeklyEarned):new();var already=new Dictionary<int,long>();DailyGameplay.Plan first=new();
        try
        {
            if(firstId!=0)first=DailyGameplay.Reward((int)firstId);CapDeepPlan(p,normal,earned,already);CapDeepPlan(p,first,earned,already);DailyGameplay.Validate(p,DailyGameplay.Combine(new[]{normal,first}));
        }
        catch(Exception ex){Packet.c.Warn("[GodWar] deep settlement validation: "+ex.Message);return rsp;}
        uint exp=row==null?0:(uint)row["AvatarExpMaterialAddNum"];var role=Table("GodWarRelationData").First(x=>(uint)x["RoleType"]==2&&(uint)x["AvatarID"]==main);
        if(!Commit(p,s,()=>{
            DailyGameplay.Grant(p,normal);if(firstId!=0)rsp.FirstWinRewardLists.Add(DailyGameplay.Grant(p,first));
            if(exp>0)AwardAvatarExp(s,(uint)role["RoleID"],exp);
            s.RewardWeek=Week;s.WeeklyEarned=earned;
            if(complete)s.DeepFirstWins.Add(firstKey);
            string historyKey=tale+"/"+main;if(!s.DeepHistory.TryGetValue(historyKey,out var history))s.DeepHistory[historyKey]=history=new DeepHistory{Avatar=main};
            history.Best=Math.Max(history.Best,score);history.Punish=Math.Max(history.Punish,punish);if(complete)history.Steps.Add(run.Challenge.StepLevel);
            if(tale==30&&(DailyGameplay.Day(run.StartedAt)+3)/7==Week)
            {
                if(s.DeepWeek<Week){s.DeepWeek=Week;s.DeepBest=0;s.DeepClaimed.Clear();}
                s.DeepBest=Math.Max(s.DeepBest,score);
            }
            RememberCollections(p,s,false);run.Challenge.ChallengeScore=score;run.Challenge.IsChallengeFail=!complete;
            s.Runs.Remove(tale);
        })){rsp.FirstWinRewardLists.Clear();return rsp;}
        rsp.AvatarLists.AddRange(CopyChallenge(run.Challenge).AvatarLists);rsp.BuffLists.AddRange(d.Buffs.Select(x=>new GodWarBuff{BuffId=x.Key,BuffLevel=x.Value}));
        rsp.IsChallengeSucc=complete;rsp.ChallengeScore=score;rsp.TotalScore=score;rsp.PunishLevel=punish;rsp.SiteIdx=checked((uint)d.Floor+1);rsp.ChallengeCostTime=d.TotalTime;
        foreach(var item in normal.Items)rsp.RewardMaterialLists.Add(new Material{Id=(uint)item.Key,Num=(uint)item.Value});
        rsp.retcode=0;return rsp;
    }
    private static void ApplyDeepHistory(State s,GodWar realm)
    {
        foreach(var tale in realm.TaleLists)foreach(var h in s.DeepHistory.Where(x=>x.Key.StartsWith(tale.TaleId+"/",StringComparison.Ordinal)).Select(x=>x.Value))
            tale.ChallengeHistoryLists.Add(new GodWarChallengeHistory{AvatarId=h.Avatar,IsChallengeSucc=h.Steps.Count>0,MaxChallengeScore=h.Best,MaxPunishLevel=h.Punish,PassStepLevelLists=h.Steps.OrderBy(x=>x).ToArray()});
    }
    // The 7.0 weekly score label reads this tracking mission, separate from reward milestones.
    private const uint DeepWeeklyScoreMission = 662031;
    internal static bool IsDeepMission(uint id)=>Table("GodWarChallengeReward").Any(x=>(uint)x["Mission"]==id);
    internal static void ApplyDeepMissions(Player p,GetMissionDataRsp response)
    {
        lock(Gate)
        {
            var s=Load(p.User);uint best=s.DeepWeek==Week?s.DeepBest:0;
            var tracker = response.MissionLists.FirstOrDefault(mission => mission.MissionId == DeepWeeklyScoreMission);
            if (tracker == null)
            {
                tracker = new Mission { MissionId = DeepWeeklyScoreMission };
                response.MissionLists.Add(tracker);
            }
            tracker.Progress = best;
            tracker.Status = MissionStatus.MissionDoing;
            tracker.BeginTime = checked((uint)(Week * 7 * 86400 - 3 * 86400 - 14400));
            tracker.EndTime = checked((uint)((Week + 1) * 7 * 86400 - 3 * 86400 - 14400));
            tracker.CycleId = checked((uint)Week);
            response.CloseMissionLists = response.CloseMissionLists?.Where(id => id != DeepWeeklyScoreMission).ToArray();
            foreach(var row in Table("GodWarChallengeReward"))
            {
                uint id=(uint)row["Mission"],need=(uint)row["Score"];response.MissionLists.RemoveAll(x=>x.MissionId==id);
                response.MissionLists.Add(new Mission{MissionId=id,Progress=Math.Min(best,need),Status=s.DeepWeek==Week&&s.DeepClaimed.Contains(id)?MissionStatus.MissionClose:best>=need?MissionStatus.MissionFinish:MissionStatus.MissionDoing,
                    BeginTime=checked((uint)(Week*7*86400-3*86400-14400)),EndTime=checked((uint)((Week+1)*7*86400-3*86400-14400)),CycleId=checked((uint)Week)});
            }
        }
    }
    internal static GetMissionRewardRsp.Retcode TakeDeepMissions(Player p,uint[] ids,out RewardData reward,out uint[] granted)
    {
        reward=new RewardData();granted=Array.Empty<uint>();
        lock(p.Equipment)lock(Gate)
        {
            if(ids==null||ids.Length==0||ids.Distinct().Count()!=ids.Length||ids.Any(x=>!IsDeepMission(x)))return GetMissionRewardRsp.Retcode.MissionIdError;
            var s=Load(p.User);if(s.DeepWeek!=Week||ids.Any(x=>s.DeepClaimed.Contains(x)||s.DeepBest<(uint)Table("GodWarChallengeReward").Single(r=>(uint)r["Mission"]==x)["Score"]))return GetMissionRewardRsp.Retcode.MissionStatusError;
            DailyGameplay.Plan plan;KeyValuePair<int,long>[] medals;
            try
            {
                plan=DailyGameplay.Combine(ids.Select(x=>DailyGameplay.Reward(BaseExcelReader<MissionData,MissionDataExcel>.GetInstance().FromId((int)x).RewardId)));
                medals=plan.Items.Where(x=>CompetitiveMedalService.Controlled((uint)x.Key)).ToArray();foreach(var medal in medals)plan.Items.Remove(medal.Key);
                DailyGameplay.Validate(p,plan);CompetitiveMedalService.Prepare();
            }
            catch(Exception ex){Packet.c.Warn("[GodWar] weekly reward validation: "+ex.Message);return GetMissionRewardRsp.Retcode.EquipmentMaterialLimit;}
            RewardData paid=null;if(!Commit(p,s,()=>{
                paid=DailyGameplay.Grant(p,plan);
                foreach(var medal in medals){CompetitiveMedalService.Grant(p.User.Uid,(uint)medal.Key,"godwar:"+Week,Clock().ToUnixTimeSeconds());paid.ItemLists.Add(new RewardItemData{Id=(uint)medal.Key,Num=(uint)medal.Value});}
                s.DeepClaimed.UnionWith(ids);
            } ))return GetMissionRewardRsp.Retcode.Fail;
            reward=paid;granted=ids;return 0;
        }
    }
}
