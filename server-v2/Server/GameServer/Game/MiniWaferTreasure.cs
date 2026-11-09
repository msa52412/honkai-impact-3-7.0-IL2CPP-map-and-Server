using System;using System.IO;using System.Linq;using System.Security.Cryptography;using System.Collections.Generic;
using Common.Resources.Proto;using Common.Utils.ExcelReader;using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
// User-authorized approximation of the server-only TREASURE1008 pool; never treat other treasure IDs as this box.
internal static class MiniWaferTreasure
{
 internal sealed record Outcome(int Id,int Num,int Weight);
 internal static Outcome[] Pool()
 {
  var root=JObject.Parse(File.ReadAllText("mini-wafer-treasure.json"));
  if((int?)root["schema"]!=1||(int?)root["material_id"]!=8402||(int?)root["treasure_id"]!=1008||(string)root["mode"]!="approximate")throw new InvalidOperationException("Invalid mini wafer pool identity");
  var rows=((JArray)root["outcomes"]).OfType<JObject>().Select(x=>new Outcome((int)x["item_id"],(int)x["num"],(int)x["weight"])).ToArray();
  if(rows.Length!=3||!rows.Select(x=>x.Id).OrderBy(x=>x).SequenceEqual(new[]{8001,8003,8101})||rows.Any(x=>x.Num<=0||x.Num>10000||x.Weight<=0)||rows.Sum(x=>(long)x.Weight)!=10000)throw new InvalidOperationException("Invalid mini wafer outcomes");
  return rows;
 }
 internal static Outcome Pick(Outcome[] rows,int roll)
 {
  if(roll<0||roll>=10000)throw new ArgumentOutOfRangeException(nameof(roll));
  foreach(var row in rows){if(roll<row.Weight)return row;roll-=row.Weight;}throw new InvalidOperationException("Empty mini wafer outcome");
 }
 internal static UseMaterialRsp Use(Player p,UseMaterialReq req,MaterialUseDataExcel config,Func<int,int> draw=null)
 {
  lock(p.Equipment){var rsp=new UseMaterialRsp{MaterialId=req.MaterialId,Num=req.Num,retcode=UseMaterialRsp.Retcode.Fail};
   UseMaterialRsp Fail(UseMaterialRsp.Retcode code){rsp.retcode=code;rsp.GiftRewardLists.Clear();return rsp;}
   if(p.User==null||p.User.Uid!=p.Equipment.OwnerUid||req.MaterialId!=8402||config==null||config.UseId!=8402||config.UseType!=2||config.EventId!=0||config.UseMin!=0||config.UseMax!=0||config.ParaStr==null||config.ParaStr.Length!=1||config.ParaStr[0]!="1008")return Fail(UseMaterialRsp.Retcode.FeatureClosed);
   if(req.Num==0||req.Num>99||config.MultiUse<=0||req.Num>config.MultiUse)return Fail(UseMaterialRsp.Retcode.InvalidNum);
   if((req.Parameter!=0&&req.Parameter!=1008)||req.SimulateMagic!=0||req.ConsumeItemList?.ItemLists.Count>0)return Fail(UseMaterialRsp.Retcode.ConsumeItemNotExist);
   if(p.Equipment.MaterialList.Where(x=>x.Id==8402).Sum(x=>(long)x.Num)<req.Num)return Fail(UseMaterialRsp.Retcode.MaterialLack);
   Outcome[] pool;try{pool=Pool();}catch(Exception ex){Packet.c.Warn("[MiniWafer] Pool unavailable: "+ex.Message);return Fail(UseMaterialRsp.Retcode.FeatureClosed);}
   // Check all outcomes before rolling so inventory caps cannot be used to reroll a rejected outcome for free.
   try{foreach(var row in pool){var worst=new DailyGameplay.Plan();worst.Items[row.Id]=checked((long)row.Num*req.Num);DailyGameplay.Validate(p,worst);}}catch{return Fail(UseMaterialRsp.Retcode.EquipmentMaterialLimit);}
   draw??=RandomNumberGenerator.GetInt32;var award=new DailyGameplay.Plan();
   try{for(uint i=0;i<req.Num;i++){var row=Pick(pool,draw(10000));award.Items[row.Id]=checked(award.Items.GetValueOrDefault(row.Id)+row.Num);}DailyGameplay.Validate(p,award);}catch{return Fail(UseMaterialRsp.Retcode.Fail);}
   Common.Resources.Proto.RewardData reward=null;
   if(!DailyGameplay.Atomic(p,()=>{p.Equipment.AddMaterial(8402,-checked((int)req.Num));reward=DailyGameplay.Grant(p,award);} ))return Fail(UseMaterialRsp.Retcode.Fail);
   rsp.GiftRewardLists.Add(reward);rsp.retcode=UseMaterialRsp.Retcode.Succ;return rsp;
  }
 }
}
