using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static class ThemeWantedGameplay
{
 public sealed class State:IRow {public long Id{get;set;}public long CreatedAt{get;set;}public uint Uid{get;set;}public long Day{get;set;}=-1;public HashSet<uint> Cleared{get;set;}=new();public uint Pending{get;set;}public long Since{get;set;}public uint ExtraStage{get;set;}}
 private static SqlTable<State> Store=>Global.Table<State>("ThemeWantedStates",("owner_uid","INTEGER",s=>s.Uid,true));
 private static JArray Table(string n)=>JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/"+n+".json"));
 private static JObject[] Groups=>Table("SingleWantedStageGroup").OfType<JObject>().Where(x=>(int)x["StageGroupID"]>=13&&(int)x["StageGroupID"]<=16).ToArray();
 private static State Load(Player p)=>Store.All().FirstOrDefault(s=>s.Uid==p.User.Uid)??new State{Uid=p.User.Uid};
 private static void Save(State s){if(s.Id==0)Store.InsertOne(s);else Store.ReplaceOne(s);}
 private static bool Level(Player p)=>PlayerLevelData.GetInstance().CalculateLevel(p.User.Exp).Level>=28;
 internal static bool Handles(uint id)=>Groups.Any(g=>g["StageIDList"].Any(x=>(uint)x==id)||g["MPStageIDList"].Any(x=>(uint)x==id));
 private static (JObject Group,int Index,JToken Rule) Row(uint id){foreach(var g in Groups){var ids=g["StageIDList"].Values<uint>().ToArray();int i=Array.IndexOf(ids,id);if(i>=0)return(g,i,Table("SingleWantedProgress").Single(x=>(uint)x["ThemeID"]==(uint)g["StageGroupThemeID"]&&(int)x["Progress"]==i+1));}return(null,-1,null);}
 private static uint Held(Player p,uint id)=>checked((uint)p.Equipment.MaterialList.Where(x=>x.Id==id).Sum(x=>(long)x.Num));
 internal static ThemeWantedRefreshTicketRsp Refresh(Player p)
 {
  lock(p.Equipment){var fail=new ThemeWantedRefreshTicketRsp{retcode=ThemeWantedRefreshTicketRsp.Retcode.Fail};if(!Level(p))return fail;var s=Load(p);long day=DailyGameplay.Day(DailyGameplay.Clock());if(s.Day>day)return fail;if(s.Day==day)return new(){retcode=0};long start=s.Day<0?day:Math.Max(s.Day+1,day-6);uint n=0;
   for(long d=start;d<=day;d++){var weekday=DateTimeOffset.FromUnixTimeSeconds(d*86400-14400).ToOffset(TimeSpan.FromHours(8)).DayOfWeek;n+=(weekday==DayOfWeek.Friday||weekday==DayOfWeek.Saturday||weekday==DayOfWeek.Sunday)?2u:1u;}
   uint held=Held(p,119107),credit=Math.Min(n,6u-Math.Min(held,6u));if(!DailyGameplay.Atomic(p,()=>{if(credit>0)p.Equipment.AddMaterial(119107,(int)credit);s.Day=day;Save(s);} ))return fail;return new(){retcode=0};
  }
 }
 // Native OKDEGDKKICE.DBALFLHIFGE subtracts1 from Progress before comparing zero-based floors.
 // Zero underflows and makes every floor look done. Include the next playable floor in NotPassProgressLists.
 internal static GetThemeWantedRsp Build(Player p)
 {
  lock(p.Equipment){if(Refresh(p).retcode!=0)return new(){retcode=GetThemeWantedRsp.Retcode.Fail};var s=Load(p);var a=new ThemeWantedActivity{ScheduleId=4,ActivityId=11104,OpenStageGroupIdLists=Groups.Select(g=>(uint)g["StageGroupID"]).ToArray()};foreach(var g in Groups){var ids=g["StageIDList"].Values<uint>().ToArray();uint cleared=0;for(int i=0;i<ids.Length;i++)if(s.Cleared.Contains(ids[i]))cleared=(uint)i+1;uint progress=Math.Min((uint)ids.Length,cleared+1);a.StageGroupInfoLists.Add(new(){StageGroupId=(uint)g["StageGroupID"],Progress=progress,NotPassProgressLists=Enumerable.Range(1,(int)progress).Where(i=>!s.Cleared.Contains(ids[i-1])).Select(i=>(uint)i).ToArray(),UnlockMpProgressLists=Array.Empty<uint>()});}return new(){retcode=0,ThemeWantedActivity=a};}
 }
 internal static StageBeginRsp Begin(Player p,uint id)
 {
  lock(p.Equipment){var rsp=new StageBeginRsp{StageId=id,retcode=StageBeginRsp.Retcode.Fail,IsCollectCheatData=false};if(Refresh(p).retcode!=0)return rsp;var row=Row(id);if(row.Group==null){rsp.retcode=StageBeginRsp.Retcode.StageNotExist;return rsp;}var s=Load(p);var ids=row.Group["StageIDList"].Values<uint>().ToArray();if(row.Index>0&&!s.Cleared.Contains(ids[row.Index-1])){rsp.retcode=StageBeginRsp.Retcode.PreStageNotFinish;return rsp;}uint cost=(uint)row.Rule[s.Cleared.Contains(id)?"CostMaterialNum":"FirstCostMaterialNum"];if(Held(p,119107)<cost){rsp.retcode=StageBeginRsp.Retcode.CostMaterialLack;return rsp;}
   if(DailyGameplay.Atomic(p,()=>{s.Pending=id;s.Since=DailyGameplay.Clock();s.ExtraStage=0;Save(s);} ))rsp.retcode=0;return rsp;}
 }
 private static DailyGameplay.Plan Reward(JToken list){var plan=new DailyGameplay.Plan();foreach(var x in list){int id=(int)x["MaterialID"];long n=(long)x["Num"];if(id<=0||n<=0)throw new InvalidOperationException("Invalid wanted reward");plan.Items[id]=checked(plan.Items.GetValueOrDefault(id)+n);}return plan;}
 private static void Drops(ICollection<DropItem> drops,DailyGameplay.Plan plan){foreach(var x in plan.Items)drops.Add(new(){ItemId=(uint)x.Key,Num=checked((uint)x.Value)});}
 internal static StageEndRsp End(Player p,StageEndReqBody req)
 {
  lock(p.Equipment){var rsp=new StageEndRsp{StageId=req.StageId,EndStatus=req.EndStatus,retcode=StageEndRsp.Retcode.StageError};var s=Load(p);long elapsed=DailyGameplay.Clock()-s.Since;if(s.Pending!=req.StageId||elapsed<0||elapsed>21600||!Enum.IsDefined(typeof(StageEndStatus),req.EndStatus))return rsp;var row=Row(req.StageId);if(row.Group==null)return rsp;
   if(req.EndStatus!=StageEndStatus.StageWin){if(DailyGameplay.Atomic(p,()=>{s.Pending=0;Save(s);} ))rsp.retcode=0;return rsp;}
   bool first=!s.Cleared.Contains(req.StageId);uint cost=(uint)row.Rule[first?"FirstCostMaterialNum":"CostMaterialNum"];if(Held(p,119107)<cost)return rsp;var normal=Reward(row.Rule["DropDisplayList"]);var extra=Reward(row.Rule[first?"FirstDropDisplayList":"ExtraRewardList"]);var award=first?DailyGameplay.Combine(new[]{normal,extra}):normal;
   try{DailyGameplay.Validate(p,award);}catch{return rsp;}
   if(!DailyGameplay.Atomic(p,()=>{if(cost>0)p.Equipment.AddMaterial(119107,-checked((int)cost));DailyGameplay.Grant(p,award);s.Pending=0;s.Cleared.Add(req.StageId);s.ExtraStage=first?0:req.StageId;Save(s);} ))return rsp;
   rsp.retcode=0;rsp.Progress=1;rsp.IsFirstWin=first;rsp.IsThemeWantedExtraDropTakenByFirstPass=first;Drops(rsp.ThemeWantedDropItemLists,normal);Drops(rsp.ThemeWantedShowExtraDropItemLists,extra);return rsp;
  }
 }
 internal static ThemeWantedBuyExtraDropRsp BuyExtra(Player p)
 {
  lock(p.Equipment){var rsp=new ThemeWantedBuyExtraDropRsp{retcode=ThemeWantedBuyExtraDropRsp.Retcode.Fail};var s=Load(p);if(!Level(p)||s.ExtraStage==0)return rsp;var row=Row(s.ExtraStage);if(row.Group==null)return rsp;var costs=Reward(row.Rule["ExtraDropCostList"]);if(costs.Items.Any(x=>Held(p,(uint)x.Key)<x.Value)){rsp.retcode=ThemeWantedBuyExtraDropRsp.Retcode.MaterialLack;return rsp;}var award=Reward(row.Rule["ExtraRewardList"]);try{DailyGameplay.Validate(p,award);}catch{return rsp;}
   if(!DailyGameplay.Atomic(p,()=>{foreach(var x in costs.Items)p.Equipment.AddMaterial(x.Key,-checked((int)x.Value));DailyGameplay.Grant(p,award);s.ExtraStage=0;Save(s);} ))return rsp;Drops(rsp.DropItemLists,award);rsp.retcode=0;return rsp;
  }
 }
}
