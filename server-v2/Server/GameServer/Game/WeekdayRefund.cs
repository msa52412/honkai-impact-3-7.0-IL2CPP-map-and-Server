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
// Forward-only tracking with a reconstructed old-version unused-attempt conversion.
internal static class WeekdayRefund
{
 public sealed class State:IRow{public long Id{get;set;}public long CreatedAt{get;set;}public uint Uid{get;set;}public long Day{get;set;}public HashSet<int> Used{get;set;}=new();public uint Pending{get;set;}}
 private static JObject Config=>File.Exists("weekday-refund.json")?JObject.Parse(File.ReadAllText("weekday-refund.json")):new JObject();
 internal static bool Enabled=>(bool?)Config["enabled"]==true;
 private static SqlTable<State> Store=>Global.Table<State>("WeekdayRefunds",("owner_uid","INTEGER",s=>s.Uid,true));
 private static void Save(State s){if(s.Id==0)Store.InsertOne(s);else Store.ReplaceOne(s);}
 private static State Load(Player p)
 {
  long day=DailyGameplay.Day(DailyGameplay.Clock());var s=Store.All().FirstOrDefault(x=>x.Uid==p.User.Uid);if(s==null){s=new State{Uid=p.User.Uid,Day=day};Save(s);return s;}
  if(day>s.Day){long gap=day-s.Day;s.Pending=checked((uint)((JObject)Config["free_group_costs"]).Properties().Where(x=>gap>1||!s.Used.Contains(int.Parse(x.Name))).Sum(x=>(long)x.Value));s.Day=day;s.Used.Clear();Save(s);}return s;
 }
 internal static void Observe(Player p){if(Enabled)lock(p.Equipment)Global.Store.InTransaction(()=>Load(p));}
 internal static void MarkUsed(Player p,int group){if(!Enabled)return;var s=Load(p);if(s.Used.Add(group))Save(s);}
 internal static TakeWeekDayActivityReturnCoinRsp Take(Player p)
 {
  var rsp=new TakeWeekDayActivityReturnCoinRsp{retcode=TakeWeekDayActivityReturnCoinRsp.Retcode.NoReturn};if(!Enabled)return rsp;lock(p.Equipment){
   var s=Load(p);int id=(int)Config["material"];var meta=BaseExcelReader<MaterialData,MaterialDataExcel>.GetInstance().FromId((uint)id);if(meta==null||meta.QuantityLimit<=0){rsp.retcode=TakeWeekDayActivityReturnCoinRsp.Retcode.Fail;return rsp;}
   long held=p.Equipment.MaterialList.Where(x=>x.Id==id).Sum(x=>(long)x.Num);rsp.IsCoinFullAfterTaken=held>=meta.QuantityLimit;if(s.Pending==0)return rsp;if(held>=meta.QuantityLimit){rsp.retcode=TakeWeekDayActivityReturnCoinRsp.Retcode.CoinLimit;return rsp;}
   uint n=(uint)Math.Min(s.Pending,meta.QuantityLimit-held);if(!DailyGameplay.Atomic(p,()=>{p.Equipment.AddMaterial(id,checked((int)n));s.Pending-=n;Save(s);})){rsp.retcode=TakeWeekDayActivityReturnCoinRsp.Retcode.Fail;return rsp;}
   rsp.retcode=0;rsp.Num=n;rsp.IsCoinFullAfterTaken=held+n>=meta.QuantityLimit;return rsp;
  }
 }
}
