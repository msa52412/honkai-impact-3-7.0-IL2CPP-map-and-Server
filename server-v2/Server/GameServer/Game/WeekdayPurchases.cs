using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static class WeekdayPurchases
{
 public sealed class State:IRow{public long Id{get;set;}public long CreatedAt{get;set;}public uint Uid{get;set;}public long Day{get;set;}public Dictionary<uint,long> Opened{get;set;}=new();public Dictionary<int,uint> Bought{get;set;}=new();}
 private static SqlTable<State> Store=>Global.Table<State>("WeekdayPurchases",("owner_uid","INTEGER",s=>s.Uid,true));
 private static State Load(Player p){var s=Store.All().FirstOrDefault(x=>x.Uid==p.User.Uid)??new State{Uid=p.User.Uid};long day=DailyGameplay.Day(DailyGameplay.Clock());if(s.Day!=day){s.Day=day;s.Bought.Clear();s.Opened.Clear();}return s;}
 internal static uint OpenStamp(Player p,uint id)=>checked((uint)Load(p).Opened.GetValueOrDefault(id));
 internal static void MarkOpen(Player p,uint id){var s=Load(p);s.Opened[id]=DailyGameplay.Clock();if(s.Id==0)Store.InsertOne(s);else Store.ReplaceOne(s);}
 internal static uint Bonus(Player p,int series)=>Load(p).Bought.GetValueOrDefault(series);
 internal static ExchangeWeekDayActivityTimesRsp Buy(Player p,ExchangeWeekDayActivityTimesReq req)
 {
  var rsp=new ExchangeWeekDayActivityTimesRsp{retcode=ExchangeWeekDayActivityTimesRsp.Retcode.Fail};lock(p.Equipment){
   var rows=JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/WeekDayActivity.json")).OfType<JObject>();var a=rows.FirstOrDefault(x=>(uint)x["weekDayActivityID"]==req.ActivityId);
   if(a==null||!WeekDayTuning.ActivityIds.Contains(req.ActivityId)||!WeekdayGameplay.Open()||!WeekdayCalendar.Active(p,req.ActivityId)){rsp.retcode=ExchangeWeekDayActivityTimesRsp.Retcode.ActivityNotOpen;return rsp;}
   if(!(bool)a["activityCoinEnable"]||req.ActivityTimes>1)return rsp;int key=(int)a["seriesID"];if(key==0)key=(int)req.ActivityId;var s=Load(p);uint bought=s.Bought.GetValueOrDefault(key);uint used=DailyGameplay.State(p).ActivityUsed.GetValueOrDefault(key);
   if((ulong)used<(ulong)(uint)a["maxEnterTimes"]+bought){rsp.retcode=ExchangeWeekDayActivityTimesRsp.Retcode.TimesNotZero;return rsp;}
   int id=(int)a["activityCoinID"],cost=(int)a["activityCoinCost"];if(id<=0||cost<=0||bought==uint.MaxValue)return rsp;
   if(p.Equipment.MaterialList.Where(x=>x.Id==id).Sum(x=>(long)x.Num)<cost){rsp.retcode=ExchangeWeekDayActivityTimesRsp.Retcode.CoinLack;return rsp;}
   if(!DailyGameplay.Atomic(p,()=>{p.Equipment.AddMaterial(id,-cost);s.Bought[key]=bought+1;if(s.Id==0)Store.InsertOne(s);else Store.ReplaceOne(s);}))return rsp;
   rsp.retcode=0;rsp.CoinCost=(uint)cost;return rsp;
  }
 }
}
