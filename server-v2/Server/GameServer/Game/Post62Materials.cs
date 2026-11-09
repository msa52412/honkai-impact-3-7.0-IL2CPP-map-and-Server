using System;
using System.Linq;
using System.Collections.Generic;
namespace PemukulPaku.GameServer.Game;
// 6.2 material consolidation. Exported historical rows still contain retired IDs.
internal static class Post62Materials
{
 internal static (int Id,long Num,long Den) Rule(int id)=>id switch {
  4001=>(1301,1,5),4002=>(1301,1,1),4003 or 4007=>(1301,3,1),
  5005 or 5009=>(1301,1,5),5006 or 5010=>(1301,1,1),5007 or 5011=>(1301,4,1),
  5008 or 5012=>(1302,1,1),921 or 4015=>(1302,3,10),_ =>(id,1,1)};
 internal static bool Retired(int id)=>Rule(id).Id!=id;
 internal static void Normalize(Dictionary<int,long> items)
 {
  foreach(var x in items.ToArray()) {var r=Rule(x.Key);if(r.Id==x.Key)continue;if(x.Value<0)throw new InvalidOperationException("Negative material conversion");items.Remove(x.Key);long amount=checked((checked(x.Value*r.Num)+r.Den-1)/r.Den);if(amount>0)items[r.Id]=checked(items.GetValueOrDefault(r.Id)+amount);}
 }
}
