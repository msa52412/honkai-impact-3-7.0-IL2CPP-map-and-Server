using System;using System.IO;using System.Linq;using System.Collections.Generic;using Newtonsoft.Json;using Newtonsoft.Json.Linq;using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer;
internal static class GachaRotation
{
 internal static long Slot(long now,long anchor,long duration){if(duration<=0)throw new InvalidDataException("Invalid rotation duration");long delta=checked(now-anchor);long n=delta/duration;return delta<0&&delta%duration!=0?n-1:n;}
 private static JObject Config=>File.Exists("gacha-rotation.json")?JObject.Parse(File.ReadAllText("gacha-rotation.json")):new JObject();
 internal static List<GachaPool> Select(List<GachaPool> pools)
 {
  var output=pools.Where(x=>string.IsNullOrEmpty(x.RotationGroup)).ToList();var cfg=Config;
  foreach(var group in pools.Where(x=>!string.IsNullOrEmpty(x.RotationGroup)).GroupBy(x=>x.RotationGroup)){
   var rule=cfg["groups"]?[group.Key];if(rule==null)continue;long duration=(long)rule["duration_seconds"],anchor=(long)rule["anchor"];int count=(int)rule["slots"];if(count<=0||count>100)throw new InvalidDataException("Invalid rotation slots");
   long slot=Slot(DailyGameplay.Clock(),anchor,duration);int phase=(int)((slot%count+count)%count);var selected=group.Where(x=>x.RotationSlot==phase).ToArray();if(selected.Length!=1)throw new InvalidDataException("Missing/duplicate rotation pool");
   var copy=JsonConvert.DeserializeObject<GachaPool>(JsonConvert.SerializeObject(selected[0]));copy.PeriodKey="rotation:"+group.Key+":"+slot;output.Add(copy);
  }
  return output;
 }
 private sealed class Seen {internal Dictionary<uint,(uint Id,uint Begin)> Pools=new();}
 private static readonly System.Runtime.CompilerServices.ConditionalWeakTable<Session,Seen> Views=new();
 internal static void Remember(Session session,Common.Resources.Proto.GetGachaDisplayRsp display)
 {var seen=Views.GetOrCreateValue(session);lock(seen)seen.Pools=display.GachaDisplayInfoLists.ToDictionary(x=>(uint)x.GachaType,x=>(x.CommonData.GachaId,x.CommonData.DataBeginTime));}
 internal static bool CurrentDisplay(Session session,GachaPool pool)
 {if(string.IsNullOrEmpty(pool.RotationGroup)||pool.GachaType==20||!Views.TryGetValue(session,out var seen))return true;lock(seen)return seen.Pools.TryGetValue(pool.GachaType,out var shown)&&shown.Id==pool.GachaId&&shown.Begin==Window(pool).Begin;}
 internal static (uint Begin,uint End) Window(GachaPool pool)
 {
  if(pool.GachaType==20||string.IsNullOrEmpty(pool.RotationGroup))return(1,uint.MaxValue);var rule=Config["groups"]?[pool.RotationGroup];if(rule==null)return(1,uint.MaxValue);
  long duration=(long)rule["duration_seconds"],anchor=(long)rule["anchor"],slot=Slot(DailyGameplay.Clock(),anchor,duration);long begin=checked(anchor+slot*duration),end=checked(begin+duration);return((uint)Math.Clamp(begin,1,uint.MaxValue),(uint)Math.Clamp(end,1,uint.MaxValue));
 }
}
