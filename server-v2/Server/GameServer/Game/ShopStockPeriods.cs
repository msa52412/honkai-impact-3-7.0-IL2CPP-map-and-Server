using System;using System.Linq;using Common.Resources.Proto;using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
// Fixed historical version length is an explicit approximation, configured separately from stock.
internal static class ShopStockPeriods
{
 internal static string Period(JObject shop,uint id)=>(string)shop["goods_periods"]?[id.ToString()]??(string)shop["period"]??"permanent";
 private static long Version(JObject s)=>Math.Max(0,(DailyGameplay.Clock()-((long?)s["version_anchor"]??1695844800L))/(((long?)s["version_days"]??42)*86400));
 internal static string Cycle(JObject s,uint id=0)=>Period(s,id) switch{"day"=>"d"+DailyGameplay.Day(DailyGameplay.Clock()),"week"=>"w"+DailyGameplay.Week(DailyGameplay.Clock()),"month"=>"m"+DateTimeOffset.FromUnixTimeSeconds(DailyGameplay.Clock()+14400).ToString("yyyy-MM"),"version"=>"v"+Version(s),_=>"permanent"};
 internal static bool Matches(JObject s,uint id,string saved,string batch){string cycle=Cycle(s,id);return saved==cycle+batch||batch==""&&((JArray)s["cycle_aliases"]?[cycle]??new JArray()).Any(x=>(string)x==saved);}
 internal static uint Next(JObject s)
 {
  string p=Period(s,0);if(p=="permanent")return 0;
  if(p=="version")return checked((uint)(((long?)s["version_anchor"]??1695844800L)+(Version(s)+1)*(((long?)s["version_days"]??42)*86400)));
  if(p=="week")return checked((uint)(((DailyGameplay.Week(DailyGameplay.Clock())+1)*7-3)*86400-14400));
  if(p=="month"){var n=DateTimeOffset.FromUnixTimeSeconds(DailyGameplay.Clock()+14400);return checked((uint)(new DateTimeOffset(n.Year,n.Month,1,0,0,0,TimeSpan.Zero).AddMonths(1).ToUnixTimeSeconds()-14400));}
  return DailyGameplay.NextReset;
 }
 internal static Goods.RefreshTimeType Refresh(JObject s,uint id)=>Period(s,id) switch{"day"=>Goods.RefreshTimeType.DayType,"week"=>Goods.RefreshTimeType.WeekType,"month"=>Goods.RefreshTimeType.MonthType,"version"=>Goods.RefreshTimeType.VersionType,_=>Goods.RefreshTimeType.ShopType};
}
