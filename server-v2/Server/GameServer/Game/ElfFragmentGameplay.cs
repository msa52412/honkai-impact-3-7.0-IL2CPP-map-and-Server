using System;using System.IO;using System.Linq;using System.Collections.Generic;using Common;using Common.Database;using Common.Resources.Proto;using Common.Utils.ExcelReader;using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static class ElfFragmentGameplay
{
 private static readonly Lazy<JObject[]> Data=new(()=>JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/ElfFragmentData.json")).OfType<JObject>().ToArray());
 internal static JObject[] Rows()=>Data.Value;
 internal static bool CurrencyUnlocked(Player p,int currency)=>Rows().Any(r=>(int)r["ReturnMaterialID"]==currency&&p.Elfs?.Elves?.Any(e=>e.ElfId==(uint)r["ElfID"]&&e.Star>=(uint)r["ReturnMinStar"])==true);
 internal static ElfFragmentTransformRsp Transform(Player p,ElfFragmentTransformReq req)
 {
  var rsp=new ElfFragmentTransformRsp{retcode=ElfFragmentTransformRsp.Retcode.Fail};if(p.Elfs==null)return rsp;lock(p.Equipment){
   bool batch=req.FragmentLists.Count>0;if(batch&&(req.ShouldSerializeElfFragmentId()||req.ShouldSerializeElfFragmentNum()))return rsp;
   var input=batch?req.FragmentLists:new List<GenericItemNum>{new(){Id=req.ElfFragmentId,Num=req.ElfFragmentNum}};
   if(input.Count==0||input.Count>256||input.Any(x=>x==null||x.Id==0||x.Num==0||x.Level>1))return rsp;
   var rows=Rows();var before=p.Elfs.ElfFragments;var values=(before??Array.Empty<ElfFragment>()).Select(x=>new ElfFragment{ElfId=x.ElfId,FragmentNum=x.FragmentNum}).ToArray();var credits=new Dictionary<int,long>();
   foreach(var group in input.GroupBy(x=>x.Id)){
    var row=rows.SingleOrDefault(x=>(uint)x["FragmentID"]==group.Key);if(row==null)return rsp;uint id=(uint)row["ElfID"];
    var owner=p.Elfs.Elves.SingleOrDefault(x=>x.ElfId==id);if(owner==null||owner.Star<(uint)row["ReturnMinStar"]){rsp.retcode=ElfFragmentTransformRsp.Retcode.StarLack;return rsp;}
    ulong num=group.Aggregate(0UL,(a,x)=>a+x.Num);var balances=values.Where(x=>x.ElfId==id).ToArray();if(balances.Length!=1||num>balances[0].FragmentNum){rsp.retcode=ElfFragmentTransformRsp.Retcode.FragmentLack;return rsp;}
    int currency=(int)row["ReturnMaterialID"];long per=(long)row["ReturnNum"];if(currency<=0||per<=0)return rsp;credits[currency]=credits.GetValueOrDefault(currency)+checked((long)num*per);balances[0].FragmentNum-=checked((uint)num);
   }
   foreach(var pair in credits){var meta=BaseExcelReader<MaterialData,MaterialDataExcel>.GetInstance().FromId((uint)pair.Key);if(meta==null)return rsp;long cap=meta.QuantityLimit>0?Math.Min(meta.QuantityLimit,int.MaxValue):int.MaxValue;long held=p.Equipment.MaterialList.Where(x=>x.Id==pair.Key).Sum(x=>(long)x.Num);if(pair.Value>int.MaxValue||held+pair.Value>cap){rsp.retcode=ElfFragmentTransformRsp.Retcode.MaterialFull;return rsp;}}
   _=Common.Database.Elfs.collection;if(!DailyGameplay.Atomic(p,()=>{p.Elfs.ElfFragments=values;p.Elfs.Save();foreach(var pair in credits)p.Equipment.AddMaterial(pair.Key,(int)pair.Value);})){p.Elfs.ElfFragments=before;return rsp;}
   foreach(var pair in credits)rsp.ReturnLists.Add(new GenericItemNum{Id=(uint)pair.Key,Num=(uint)pair.Value});if(!batch&&rsp.ReturnLists.Count==1){rsp.AddMaterialId=rsp.ReturnLists[0].Id;rsp.AddMaterialNum=rsp.ReturnLists[0].Num;rsp.ReturnLists.Clear();}rsp.retcode=ElfFragmentTransformRsp.Retcode.Succ;return rsp;
  }
 }
}
