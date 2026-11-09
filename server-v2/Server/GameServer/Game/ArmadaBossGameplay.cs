using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.MPModule;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
// Activity IDs, stages, thresholds and reward IDs come from local client tables.
// Open continuously inside each local schedule; official daily open hours remain unverified.
internal static class ArmadaBossGameplay
{
 public sealed class State:IRow {public long Id{get;set;}public long CreatedAt{get;set;}public uint Uid{get;set;}public uint Armada{get;set;}public uint Activity{get;set;}public Dictionary<uint,uint> Scores{get;set;}=new();public uint SelfTaken{get;set;}public uint ArmadaTaken{get;set;}public uint Pending{get;set;}public long Since{get;set;}public uint[] Avatars{get;set;}=Array.Empty<uint>();}
 private static readonly object Gate=new();
 private static SqlTable<State> Store=>Global.Table<State>("ArmadaBossStates",("owner_uid","INTEGER",s=>s.Uid,true));
 private static JArray Table(string name)=>JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/"+name+".json"));
 private static JObject[] Schedules=>File.Exists("Resources/ExcelOutputAsset/ArmadaBossActivityData.json")?Table("ArmadaBossActivityData").OfType<JObject>().OrderBy(x=>(long)x["EndTime"]["TimeStampForBakedReader"]).ToArray():Array.Empty<JObject>();
 private static long End(JObject s)=>(long)s["EndTime"]["TimeStampForBakedReader"];
 private static JObject Current=>Schedules.FirstOrDefault(x=>End(x)>DailyGameplay.Clock());
 private static uint[] Stages(JObject s)=>((string)s["StageIDList"]).Split(',').Select(uint.Parse).ToArray();
 internal static bool Handles(uint stage)=>Schedules.Any(x=>Stages(x).Contains(stage));
 private static State Load(Player p,uint armada,uint activity){var s=Store.All().FirstOrDefault(x=>x.Uid==p.User.Uid)??new State{Uid=p.User.Uid};if(s.Armada!=armada||s.Activity!=activity){s.Armada=armada;s.Activity=activity;s.Scores.Clear();s.SelfTaken=s.ArmadaTaken=s.Pending=0;s.Since=0;s.Avatars=Array.Empty<uint>();}return s;}
 private static void Save(State s){if(s.Id==0)Store.InsertOne(s);else Store.ReplaceOne(s);}
 private static uint Total(State s)=>(uint)Math.Min(uint.MaxValue,s.Scores.Values.Sum(x=>(long)x));
 private static ArmadaStageScorePlayerScore[] Members(Armada a,uint activity)=>Store.All().Where(s=>s.Activity==activity&&s.Armada==a.Id&&a.MemberUids.Contains(s.Uid)).Select(s=>new ArmadaStageScorePlayerScore{Uid=s.Uid,Score=Total(s),AvatarLists=s.Avatars}).ToArray();
 internal static GetArmadaActivityListRsp Activities(Player p)
 {
  var rsp=new GetArmadaActivityListRsp{retcode=0};if(ArmadaModule.GetInstance().ByUid(p.User.Uid)==null)return rsp;var c=Current;if(c==null)return rsp;
  long begin=Schedules.Where(x=>End(x)<End(c)).Select(End).DefaultIfEmpty(End(c)-345600).Max();
  rsp.ActivityLists.Add(new ArmadaActivity{Type=ArmadaActivityType.ArmadaActivityArmadaStageScoreActivity,ScheduleId=(uint)c["ActivityID"],BeginTime=checked((uint)begin),EndTime=checked((uint)End(c)),OpenDayTime=0,CloseDayTime=86400});return rsp;
 }
 internal static GetArmadaStageScoreActivityRsp Build(Player p)
 {
  lock(p.Equipment)lock(Gate){var rsp=new GetArmadaStageScoreActivityRsp{retcode=0};var a=ArmadaModule.GetInstance().ByUid(p.User.Uid);var c=Current;if(a==null||c==null)return rsp;uint activity=(uint)c["ActivityID"];var s=Load(p,a.Id,activity);rsp.ActivityId=activity;rsp.HasTakeSelfScore=s.SelfTaken;rsp.HasTakeArmadaScore=s.ArmadaTaken;foreach(uint stage in Stages(c))rsp.StageScoreLists.Add(new ArmadaStageScore{StageId=stage,Score=s.Scores.GetValueOrDefault(stage)});rsp.ArmadaScoreLists.AddRange(Members(a,activity));return rsp;}
 }
 internal static StageBeginRsp Begin(Player p,StageBeginReq req)
 {
  var rsp=new StageBeginRsp{StageId=req.StageId,retcode=StageBeginRsp.Retcode.StageNotExist};lock(p.Equipment)lock(Gate){var c=Current;var a=ArmadaModule.GetInstance().ByUid(p.User.Uid);if(c==null||a==null||!Stages(c).Contains(req.StageId))return rsp;var stage=WeekdayGameplay.Stage(req.StageId);if(stage==null)return rsp;
   if(Common.Utils.ExcelReader.PlayerLevelData.GetInstance().CalculateLevel(p.User.Exp).Level<stage.UnlockPlayerLevel){rsp.retcode=StageBeginRsp.Retcode.Fail;return rsp;}
   var ids=req.AvatarIdLists??Array.Empty<uint>();
   if(ids.Length<1||ids.Length>3||ids.Distinct().Count()!=ids.Length||ids.Any(id=>!p.AvatarList.Any(v=>v.AvatarId==id&&v.Star>0))){rsp.retcode=StageBeginRsp.Retcode.Fail;return rsp;}
   var s=Load(p,a.Id,(uint)c["ActivityID"]);s.Pending=req.StageId;s.Since=DailyGameplay.Clock();s.Avatars=(req.AvatarIdLists??Array.Empty<uint>()).ToArray();rsp.retcode=DailyGameplay.Atomic(p,()=>Save(s))?StageBeginRsp.Retcode.Succ:StageBeginRsp.Retcode.Fail;return rsp;}
 }
 internal static StageEndRsp Finish(Player p,StageEndReqBody req)
 {
  var rsp=new StageEndRsp{StageId=req.StageId,EndStatus=req.EndStatus,retcode=StageEndRsp.Retcode.StageError};lock(p.Equipment)lock(Gate){var c=Current;var a=ArmadaModule.GetInstance().ByUid(p.User.Uid);if(c==null||a==null||!Stages(c).Contains(req.StageId))return rsp;var s=Load(p,a.Id,(uint)c["ActivityID"]);long elapsed=DailyGameplay.Clock()-s.Since;if(!Enum.IsDefined(typeof(StageEndStatus),req.EndStatus)||s.Pending!=req.StageId||elapsed<0||elapsed>21600)return rsp;
   if(!DailyGameplay.Atomic(p,()=>{s.Pending=0;s.Since=0;if(req.EndStatus==StageEndStatus.StageWin)s.Scores[req.StageId]=Math.Max(s.Scores.GetValueOrDefault(req.StageId),req.Score);Save(s);})){rsp.retcode=StageEndRsp.Retcode.Fail;return rsp;}rsp.retcode=0;return rsp;}
 }
 internal static TakeArmadaStageScoreActivityRewardRsp Take(Player p,TakeArmadaStageScoreActivityRewardReq req)
 {
  var rsp=new TakeArmadaStageScoreActivityRewardRsp{Type=req.Type,retcode=TakeArmadaStageScoreActivityRewardRsp.Retcode.Fail};lock(p.Equipment)lock(Gate)lock(ArmadaModule.SyncGate){var c=Current;var a=ArmadaModule.GetInstance().ByUid(p.User.Uid);if(c==null||a==null){rsp.retcode=TakeArmadaStageScoreActivityRewardRsp.Retcode.NotOpen;return rsp;}uint type=(uint)req.Type;if(type!=1&&type!=2)return rsp;uint activity=(uint)c["ActivityID"];var s=Load(p,a.Id,activity);var members=Members(a,activity);long score=type==1?Total(s):members.Sum(x=>(long)x.Score);uint taken=type==1?s.SelfTaken:s.ArmadaTaken;
   var rows=Table("ArmadaBossActivityScore").OfType<JObject>().Where(x=>(uint)x["ActivityID"]==activity&&(uint)x["ScoreType"]==type&&(uint)x["ActivityScore"]>taken&&(uint)x["ActivityScore"]<=score).OrderBy(x=>(uint)x["ActivityScore"]).ToArray();if(rows.Length==0){rsp.retcode=taken>0?TakeArmadaStageScoreActivityRewardRsp.Retcode.HasTake:TakeArmadaStageScoreActivityRewardRsp.Retcode.NotFinishStage;return rsp;}
   try{var plans=rows.Select(x=>DailyGameplay.Reward((int)x["Reward"])).ToArray();var funds=plans.Select(x=>x.Items.GetValueOrDefault(80001)).ToArray();foreach(var plan in plans)plan.Items.Remove(80001);long fund=funds.Sum();if(fund<0||fund+a.Fund>uint.MaxValue)throw new InvalidOperationException("Armada fund limit");DailyGameplay.Validate(p,DailyGameplay.Combine(plans));
    if(!DailyGameplay.Atomic(p,()=>{for(int i=0;i<plans.Length;i++){var reward=DailyGameplay.Grant(p,plans[i]);if(funds[i]>0)reward.ItemLists.Add(new RewardItemData{Id=80001,Num=checked((uint)funds[i])});rsp.RewardLists.Add(reward);}if(fund>0)ArmadaModule.GetInstance().AddFund(a.Id,fund);if(type==1)s.SelfTaken=(uint)rows.Last()["ActivityScore"];else s.ArmadaTaken=(uint)rows.Last()["ActivityScore"];Save(s);})){rsp.RewardLists.Clear();return rsp;}
    rsp.retcode=0;rsp.ScoreLists.AddRange(members);return rsp;
   }catch(Exception e){rsp.RewardLists.Clear();Packet.c.Warn("[ArmadaBoss] reward rejected: "+e.Message);return rsp;}
  }
 }
}
