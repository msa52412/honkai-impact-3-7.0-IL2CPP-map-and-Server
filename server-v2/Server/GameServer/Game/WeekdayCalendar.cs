using System;
using System.IO;
using System.Linq;
using Common.Resources.Proto;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static class WeekdayCalendar
{
 private static JObject Config=>File.Exists("weekday-calendar.json")?JObject.Parse(File.ReadAllText("weekday-calendar.json")):new JObject();
 internal static bool Enabled=>(bool?)Config["enabled"]==true;
 private static int[] Days(uint id)=>((JArray)Config["days"]?[id.ToString()]??new JArray()).Select(x=>(int)x).ToArray();
 private static int Weekday(long day)=>(int)DateTimeOffset.FromUnixTimeSeconds(day*86400-14400).ToOffset(TimeSpan.FromHours(8)).DayOfWeek;
 internal static bool Natural(uint id)=>!Enabled||Days(id).Contains(Weekday(DailyGameplay.Day(DailyGameplay.Clock())));
 internal static bool Active(Player p,uint id)=>Natural(id)||WeekdayPurchases.OpenStamp(p,id)>0;
 internal static (uint Begin,uint End) Window(Player p,uint id)
 {
  long day=DailyGameplay.Day(DailyGameplay.Clock());if(Active(p,id))return(checked((uint)(day*86400-14400)),checked((uint)((day+1)*86400-14400)));
  for(int offset=1;offset<=7;offset++)if(Days(id).Contains(Weekday(day+offset)))return(checked((uint)((day+offset)*86400-14400)),checked((uint)((day+offset+1)*86400-14400)));
  return(uint.MaxValue,uint.MaxValue);
 }
 internal static OpenWeekDayActivityRsp Open(Player p,uint id)
 {
  var rsp=new OpenWeekDayActivityRsp{retcode=OpenWeekDayActivityRsp.Retcode.Fail};lock(p.Equipment){
   if(!WeekDayTuning.ActivityIds.Contains(id)||!WeekdayGameplay.Open())return rsp;
   if(Active(p,id)){rsp.retcode=OpenWeekDayActivityRsp.Retcode.IsOpen;return rsp;}
   var a=JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/WeekDayActivity.json")).FirstOrDefault(x=>(uint)x["weekDayActivityID"]==id);if(a==null)return rsp;
   if(Common.Utils.ExcelReader.PlayerLevelData.GetInstance().CalculateLevel(p.User.Exp).Level<(int)a["minPlayerLevel"])return rsp;
   int cost=(int)a["openScoinCost"];
   if(cost<=0||p.Equipment.MaterialList.Where(x=>x.Id==100).Sum(x=>(long)x.Num)<cost)return rsp;
   if(DailyGameplay.Atomic(p,()=>{p.Equipment.AddMaterial(100,-cost);WeekdayPurchases.MarkOpen(p,id);})){rsp.retcode=OpenWeekDayActivityRsp.Retcode.Succ;}return rsp;
  }
 }
}
