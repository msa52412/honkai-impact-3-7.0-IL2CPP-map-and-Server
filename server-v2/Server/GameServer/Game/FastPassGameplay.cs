using System;
using System.Linq;
using System.IO;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static class FastPassGameplay
{
 public sealed class State:IRow {public long Id{get;set;}public long CreatedAt{get;set;}public uint Uid{get;set;}public long Day{get;set;}public uint Used{get;set;}}
 private static SqlTable<State> Store=>Global.Table<State>("FastPassStates",("owner_uid","INTEGER",s=>s.Uid,true));
 private static JToken Config(uint type)=>JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/FastPass.json")).FirstOrDefault(x=>(uint)x["ID"]==type);
 private static State Load(Player p){var s=Store.All().FirstOrDefault(x=>x.Uid==p.User.Uid)??new State{Uid=p.User.Uid};long day=DailyGameplay.Day(DailyGameplay.Clock());if(s.Day!=day){s.Day=day;s.Used=0;}return s;}
 private static bool Open(JToken c){var now=DateTimeOffset.FromUnixTimeSeconds(DailyGameplay.Clock()).ToOffset(TimeSpan.FromHours(8)).DateTime;return now>=DateTime.Parse((string)c["BeginTime"])&&now<DateTime.Parse((string)c["EndTime"]);}
 internal static GetFastPassDataRsp Build(Player p,uint type)
 {
  lock(p.Equipment){var c=Config(type);if(c==null||type!=2)return new(){FastPassType=type,retcode=GetFastPassDataRsp.Retcode.NotExist};return new(){retcode=0,FastPassType=type,UseTimes=Load(p).Used};}
 }
 internal static FinishFastPassRsp Finish(Player p,FinishFastPassReq req)
 {
  lock(p.Equipment){uint id=req.FastPassParaWeekday?.StageId??0;var rsp=new FinishFastPassRsp{FastPassType=req.FastPassType,FastPassParaWeekday=req.FastPassParaWeekday,retcode=FinishFastPassRsp.Retcode.Fail};
   if(req.FastPassType!=2||req.FastPassParaOw!=null||id==0)return rsp;
   var cfg=Config(2);if(cfg==null||!Open(cfg)){rsp.retcode=FinishFastPassRsp.Retcode.NotInSchedule;return rsp;}
   var state=Load(p);if(state.Used>=(uint)cfg["LimitNum"]){rsp.retcode=FinishFastPassRsp.Retcode.NoUseTimes;return rsp;}
   bool allowed=JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/FastPassLevel.json")).Any(x=>(uint)x["ID"]==2&&(uint)x["levelId"]==id);
   var stage=WeekdayGameplay.Stage(id);var activity=WeekdayGameplay.Activity(id);if(stage==null){rsp.retcode=FinishFastPassRsp.Retcode.StageNotExist;return rsp;}if(!allowed||activity==null){rsp.retcode=FinishFastPassRsp.Retcode.StageCantFastPass;return rsp;}
   var valid=WeekdayGameplay.Validate(p,stage,activity);if(valid!=0){rsp.retcode=valid==StageBeginRsp.Retcode.StaminaLack?FinishFastPassRsp.Retcode.StaminaLack:valid==StageBeginRsp.Retcode.ActivityEnterTimesLack?FinishFastPassRsp.Retcode.NoEnterTimes:FinishFastPassRsp.Retcode.StageCantFastPass;return rsp;}
   uint ticket=(uint)cfg["MaterialID"],cost=(uint)cfg["MaterialCost"];if(cost==0||p.Equipment.MaterialList.Where(x=>x.Id==ticket).Sum(x=>(long)x.Num)<cost){rsp.retcode=FinishFastPassRsp.Retcode.MaterialLack;return rsp;}
   var reward=WeekdayGameplay.Rewards(stage);try{DailyGameplay.Validate(p,reward);}catch{rsp.retcode=FinishFastPassRsp.Retcode.EquipmentFull;return rsp;}
   if(!DailyGameplay.Atomic(p,()=>{p.Equipment.AddMaterial((int)ticket,-checked((int)cost));WeekdayGameplay.Credit(p,stage,activity);state.Used++;if(state.Id==0)Store.InsertOne(state);else Store.ReplaceOne(state);} ))return rsp;
   foreach(var x in reward.Items)if(x.Key==100)rsp.ScoinReward=checked((uint)x.Value);else rsp.DropItemLists.Add(new DropItem{ItemId=(uint)x.Key,Num=checked((uint)x.Value)});rsp.PlayerExpReward=checked((uint)reward.Exp);rsp.retcode=0;return rsp;
  }
 }
}
