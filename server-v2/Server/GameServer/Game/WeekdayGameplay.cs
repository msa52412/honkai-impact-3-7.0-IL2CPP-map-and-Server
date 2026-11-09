using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static class WeekdayGameplay
{
 internal static WeekDayActivityDataExcel Activity(uint stage) => WeekDayActivityData.GetInstance().All.FirstOrDefault(x=>WeekDayTuning.ActivityIds.Contains(x.ActivityId)&&(x.LevelIdList??Array.Empty<int>()).Contains((int)stage));
 internal static GetStageDataRsp StageList(Player p)
 {
  lock(p.Equipment)
  {
   var d=DailyGameplay.State(p);var ids=WeekDayTuning.ActivityIds.ToHashSet();
   var weekday=WeekDayActivityData.GetInstance().All.Where(x=>ids.Contains(x.ActivityId)).SelectMany(x=>x.LevelIdList??Array.Empty<int>()).ToHashSet();
   var rsp=new GetStageDataRsp{retcode=GetStageDataRsp.Retcode.Succ,IsAll=true};
   foreach(var stage in BaseExcelReader<StageData,StageDataExcel>.GetInstance().All)
   {
    uint id=(uint)stage.LevelId;bool special=weekday.Contains(stage.LevelId);bool done=!special||d.Cleared.Contains(id);
    rsp.StageLists.Add(new Stage{Id=id,IsDone=done,ChallengeIndexLists=special&&d.Challenges.TryGetValue(id,out var challenges)?challenges.OrderBy(x=>x).ToArray():Array.Empty<uint>(),Progress=done?(uint)stage.MaxProgress:0,EnterTimes=special?d.StageUsed.GetValueOrDefault(id):1});
   }
   return rsp;
  }
 }
 internal static StageDataExcel Stage(uint id)=>BaseExcelReader<StageData,StageDataExcel>.GetInstance().FromId((int)id);
 internal static int Key(WeekDayActivityDataExcel a)=>a.SeriesId>0?a.SeriesId:(int)a.ActivityId;
 internal static uint Used(Player p,WeekDayActivityDataExcel a)=>DailyGameplay.State(p).ActivityUsed.GetValueOrDefault(Key(a));
 internal static bool Open()=>DailyGameplay.Clock()>=WeekDayTuning.BeginTime&&DailyGameplay.Clock()<WeekDayTuning.EndTime;
 internal static GetWeekDayActivityDataRsp Build(Player p)
 {
  lock(p.Equipment)
  {
   WeekdayRefund.Observe(p);var s=DailyGameplay.State(p);var rsp=new GetWeekDayActivityDataRsp{retcode=GetWeekDayActivityDataRsp.Retcode.Succ,IsWholeData=true};
   foreach(var a in WeekDayActivityData.GetInstance().All.Where(x=>WeekDayTuning.ActivityIds.Contains(x.ActivityId))){var window=WeekdayCalendar.Enabled?WeekdayCalendar.Window(p,a.ActivityId):(WeekDayTuning.BeginTime,WeekDayTuning.EndTime);
    rsp.ActivityLists.Add(new WeekDayActivity{ActivityId=a.ActivityId,StageIdLists=(a.LevelIdList??Array.Empty<int>()).Select(x=>(uint)x).ToArray(),EnterTimes=Used(p,a),ExchangeTimes=WeekdayPurchases.Bonus(p,Key(a)),
     BeginTime=window.Item1,EndTime=window.Item2,ActivityBeginTime=window.Item1,ActivityEndTime=window.Item2,ForceOpenTime=WeekdayPurchases.OpenStamp(p,a.ActivityId),ScheduleId=Schedule(a.ActivityId)}); }
   foreach(var g in WeekDayActivityData.GetInstance().All.Where(x=>WeekDayTuning.ActivityIds.Contains(x.ActivityId)).GroupBy(Key))
   {
    uint max=(uint)Math.Max(0,g.Min(x=>x.MaxEnterTimes));uint used=Used(p,g.First());max=checked(max+WeekdayPurchases.Bonus(p,g.Key));
    rsp.SeriesLists.Add(new ActivitySeries{SeriesId=(uint)g.Key,AvailableSweepNum=max>used?max-used:0});
   }
   return rsp;
  }
 }
 private static uint Schedule(uint id)
 {
  var path="Resources/ExcelOutputAsset/WeekDayActivitySchedule.json";
  if(!File.Exists(path))return 0;
  return JArray.Parse(File.ReadAllText(path)).Where(x=>(uint)x["ActivityId"]==id).Select(x=>(uint)x["Id"]).FirstOrDefault();
 }
 internal static DailyGameplay.Plan Rewards(StageDataExcel stage)
 {
  var p=new DailyGameplay.Plan();if(stage.StaminaCost<0||stage.ScoinReward<0||stage.CostMaterialNum!=0||stage.FloatDrop?.Length>0)throw new InvalidOperationException("Unsupported stage cost/drop");
  var rows=JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/StageDropItemData.json")).ToDictionary(x=>(int)x["DropID"]);
  foreach(int drop in stage.DropList??Array.Empty<int>())
  {
   if(drop==0)continue;
   if(!rows.TryGetValue(drop,out var row))throw new InvalidOperationException("Unresolved stage drop "+drop);
   int id=(int)row["ItemID"],n=(int)row["ItemNum"];if(id<=0||n<=0)throw new InvalidOperationException("Invalid stage drop");
   p.Items[id]=p.Items.GetValueOrDefault(id)+n;
  }
  if(stage.ScoinReward>0)p.Items[100]=p.Items.GetValueOrDefault(100)+stage.ScoinReward;
  // Player EXP is consumed stamina, not the old hard-coded 100.
  p.Exp=stage.StaminaCost;return p;
 }
 internal static StageBeginRsp.Retcode Validate(Player p,StageDataExcel stage,WeekDayActivityDataExcel a)
 {
  if(a==null||stage==null)return StageBeginRsp.Retcode.StageNotExist;
  if(!Open()||!WeekdayCalendar.Active(p,a.ActivityId))return StageBeginRsp.Retcode.ActivityNotBegin;
  if(DailyGameplay.State(p).Day!=DailyGameplay.Day(DailyGameplay.Clock()))return StageBeginRsp.Retcode.Fail;
  var l=BaseExcelReader<PlayerLevelData,PlayerLevelDataExcel>.GetInstance().CalculateLevel(p.User.Exp).Level;
  if(l<Math.Max(a.MinPlayerLevel,stage.UnlockPlayerLevel))return StageBeginRsp.Retcode.LevelLack;
  if(a.MaxEnterTimes<=0||(ulong)Used(p,a)>=(ulong)a.MaxEnterTimes+WeekdayPurchases.Bonus(p,Key(a)))return StageBeginRsp.Retcode.ActivityEnterTimesLack;
  if(!GmLocks.HasStamina(p,stage.StaminaCost))return StageBeginRsp.Retcode.StaminaLack;
  try{DailyGameplay.Validate(p,Rewards(stage));}catch(Exception ex){Packet.c.Warn("[Weekday] preflight: "+ex.Message);return StageBeginRsp.Retcode.StageTypeError;}
  return StageBeginRsp.Retcode.Succ;
 }
 internal static StageBeginRsp Begin(Player p,uint id)
 {
  lock(p.Equipment)
  {
   var rsp=new StageBeginRsp{StageId=id,IsCollectCheatData=false};var st=Stage(id);var a=Activity(id);rsp.retcode=Validate(p,st,a);
   if(rsp.retcode!=0)return rsp;
   if(!DailyGameplay.Atomic(p,()=>{var s=DailyGameplay.State(p);s.PendingStage=id;s.PendingSince=DailyGameplay.Clock();}))rsp.retcode=StageBeginRsp.Retcode.Fail;
   return rsp;
  }
 }
 internal static void Credit(Player p,StageDataExcel stage,WeekDayActivityDataExcel a)
 {
  var s=DailyGameplay.State(p);WeekdayRefund.MarkUsed(p,Key(a));GmLocks.CostStamina(p,stage.StaminaCost);
  DailyGameplay.Grant(p,Rewards(stage));s.ActivityUsed[Key(a)]=checked(Used(p,a)+1);s.StageUsed[(uint)stage.LevelId]=checked(s.StageUsed.GetValueOrDefault((uint)stage.LevelId)+1);s.Cleared.Add((uint)stage.LevelId);
  DailyGameplay.Add(p,10017,0,(uint)stage.StaminaCost);
  foreach(int tag in (stage.Tag??Array.Empty<int>()).Distinct())DailyGameplay.Add(p,10051,tag);
  if(stage.Type==2)DailyGameplay.Add(p,22,0);
 }
 internal static StageEndRsp End(Player p,StageEndReqBody body)
 {
  lock(p.Equipment)
  {
   var rsp=new StageEndRsp{StageId=body.StageId,EndStatus=body.EndStatus,retcode=StageEndRsp.Retcode.StageError};var s=DailyGameplay.State(p);
   if(s.PendingStage!=body.StageId||DailyGameplay.Clock()<s.PendingSince||DailyGameplay.Clock()-s.PendingSince>21600)return rsp;
   if(body.EndStatus!=StageEndStatus.StageWin)
   {
    if(DailyGameplay.Atomic(p,()=>{DailyGameplay.State(p).PendingStage=0;}))rsp.retcode=StageEndRsp.Retcode.Succ;
    return rsp;
   }
   var st=Stage(body.StageId);var a=Activity(body.StageId);if(Validate(p,st,a)!=0)return rsp;
   var indices=(body.ChallengeIndexLists??Array.Empty<uint>()).Distinct().ToArray();
   if(indices.Any(i=>i>=(st.ChallengeList?.Length??0)))return rsp;
   var previous=s.Challenges.GetValueOrDefault(body.StageId)??new HashSet<uint>();
   var earned=indices.Where(i=>!previous.Contains(i)).ToArray();bool first=!s.Cleared.Contains(body.StageId);
   DailyGameplay.Plan[] challengePlans;
   try{challengePlans=earned.Select(i=>DailyGameplay.Reward(st.ChallengeList[i].RewardId)).ToArray();DailyGameplay.Validate(p,DailyGameplay.Combine(challengePlans.Append(Rewards(st))));}catch(Exception){return rsp;}
   if(!DailyGameplay.Atomic(p,()=>{
    Credit(p,st,a);var state=DailyGameplay.State(p);state.PendingStage=0;
    if(!state.Challenges.TryGetValue(body.StageId,out var done)){done=new();state.Challenges[body.StageId]=done;}
    for(int i=0;i<earned.Length;i++){var reward=DailyGameplay.Grant(p,challengePlans[i]);done.Add(earned[i]);rsp.ChallengeLists.Add(new StageChallengeData{ChallengeIndex=earned[i],Reward=reward});}
   })){rsp.ChallengeLists.Clear();return rsp;}
   rsp.retcode=StageEndRsp.Retcode.Succ;rsp.Progress=(uint)st.MaxProgress;rsp.PlayerExpReward=(uint)st.StaminaCost;rsp.AvatarExpReward=0;
   rsp.ScoinReward=(uint)Rewards(st).Items.GetValueOrDefault(100);rsp.IsFirstWin=first;
   return rsp;
  }
 }
 internal static WeekDayActivitySweepRsp Sweep(Player p,uint[] ids)
 {
  lock(p.Equipment)
  {
   var rsp=new WeekDayActivitySweepRsp{retcode=WeekDayActivitySweepRsp.Retcode.Fail};
   if(ids==null||ids.Length==0||ids.Length>20||ids.Distinct().Count()!=ids.Length)return rsp;
   var s=DailyGameplay.State(p);var stages=ids.Select(Stage).ToArray();var acts=ids.Select(Activity).ToArray();
   for(int i=0;i<ids.Length;i++)if(!s.Cleared.Contains(ids[i])||Validate(p,stages[i],acts[i])!=0)return rsp;
   if(!GmLocks.HasStamina(p,stages.Sum(x=>(long)x.StaminaCost)))return rsp;
   foreach(var g in acts.GroupBy(Key))if((long)Used(p,g.First())+g.Count()>(long)g.Min(x=>x.MaxEnterTimes)+WeekdayPurchases.Bonus(p,g.Key))return rsp;
   try
   {
    var plan=DailyGameplay.Combine(stages.Select(Rewards));DailyGameplay.Validate(p,plan);
    if(!DailyGameplay.Atomic(p,()=>{for(int i=0;i<ids.Length;i++)Credit(p,stages[i],acts[i]);}))return rsp;
    // The 7.0 settlement dialog receives materials through drop_item_list.
    var reward=plan.Proto();foreach(var x in plan.Items)if(x.Key==100)reward.Scoin=(uint)x.Value;else rsp.DropItemLists.Add(new DropItem{ItemId=(uint)x.Key,Num=(uint)x.Value});
    rsp.RewardData=reward;rsp.retcode=WeekDayActivitySweepRsp.Retcode.Succ;return rsp;
   }catch(Exception ex){Packet.c.Warn("[Weekday] sweep: "+ex.Message);return rsp;}
  }
 }
 internal static void Push(Session s)
 {
  s.Send(Packet.FromProto(Build(s.Player),CmdId.GetWeekDayActivityDataRsp));
  s.ProcessPacket(Packet.FromProto(new GetStageDataReq(),CmdId.GetStageDataReq));
  s.Send(Packet.FromProto(MissionDataBuilder.Build(s.Player),CmdId.GetMissionDataRsp));DailyGameplay.PushDuty(s);BattlePassGameplay.Push(s);
 }
}
