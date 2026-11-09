using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
// Native: GetReturnMaterialWeapon / 183545710 (includes previous evolutions),
// return-exp percent GetConfigRsp fields 15/61 = 25; default return material 3000.
internal static class EquipmentDisjoinReturns
{
 private static long Accumulate(int level,int type,uint exp)
 {
  if(level<1||level>99||type<0||type>9)throw new InvalidOperationException("Invalid equipment level/type");
  long total=exp;foreach(var r in BaseExcelReader<EquipmentLevelData,EquipmentLevelDataExcel>.GetInstance().All.Where(x=>x.Level<level))total=checked(total+r.Type1[type]);return total;
 }
 private static void Credit(long exp,Dictionary<int,long> rewards)
 {
  // Single-precision multiply then truncation matches the client. Remaining sub-material exp is discarded.
  long refund=(long)((float)exp*(25f*0.01f));
  // Native GetReturnMaterialWeapon sorts configured experience materials descending,
  // then performs quotient/remainder at 181b2a46d..181b2a47e. The local return list
  // includes the fallback 3000 plus 3005..3008 (ConstValuesClient hash 143446459).
  var config=JArray.Parse(System.IO.File.ReadAllText("Resources/ExcelOutputAsset/ConstValuesClient.json"));
  var entry=config.FirstOrDefault(x=>(int)x["name"]["hash"]==143446459);
  if(entry==null)throw new InvalidOperationException("Missing disjoin experience material list");
  var ids=((string)entry["value"]).Split(',').Select(int.Parse).Distinct().ToArray();
  if(!ids.Contains(3000))throw new InvalidOperationException("Invalid disjoin fallback material");
  var rows=ids.Select(id=>BaseExcelReader<MaterialData,MaterialDataExcel>.GetInstance().FromId((uint)id)).ToArray();
  if(rows.Any(x=>x==null||x.GearExpProvideBase<=0))throw new InvalidOperationException("Invalid experience material");
  foreach(var row in rows.OrderByDescending(x=>x.GearExpProvideBase)){
   long count=refund/row.GearExpProvideBase;refund%=row.GearExpProvideBase;
   if(count>0)rewards[row.Id]=checked(rewards.GetValueOrDefault(row.Id)+count);
  }
 }
 internal static void Weapon(WeaponDataExcel row,Weapon item,Dictionary<int,long> rewards)
 {
  if(item.Level>row.MaxLv)throw new InvalidOperationException("Weapon above configured level");
  long exp=Accumulate((int)item.Level,row.ExpType,item.Exp);var seen=new HashSet<int>{row.Id};
  for(;;){var prev=BaseExcelReader<WeaponData,WeaponDataExcel>.GetInstance().All.SingleOrDefault(x=>x.EvoId==row.Id);if(prev==null)break;if(!seen.Add(prev.Id))throw new InvalidOperationException("Evolution cycle");exp=checked(exp+Accumulate(prev.MaxLv,prev.ExpType,0));row=prev;}
  Credit(exp,rewards);
 }
 internal static void Stigmata(StigmataDataExcel row,Common.Resources.Proto.Stigmata item,Dictionary<int,long> rewards)
 {
  if(item.Level>row.MaxLv)throw new InvalidOperationException("Stigmata above configured level");
  long exp=Accumulate((int)item.Level,row.ExpType,item.Exp);var seen=new HashSet<int>{row.Id};
  for(;;){var prev=BaseExcelReader<StigmataData,StigmataDataExcel>.GetInstance().All.SingleOrDefault(x=>x.EvoId==row.Id);if(prev==null)break;if(!seen.Add(prev.Id))throw new InvalidOperationException("Evolution cycle");exp=checked(exp+Accumulate(prev.MaxLv,prev.ExpType,0));row=prev;}
  Credit(exp,rewards);
 }
}
