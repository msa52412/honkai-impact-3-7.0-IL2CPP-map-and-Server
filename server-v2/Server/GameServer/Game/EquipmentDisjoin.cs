using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static class EquipmentDisjoin
{
 private sealed class Reject:Exception{internal IslandDisjoinEquipmentRsp.Retcode Code;internal Reject(IslandDisjoinEquipmentRsp.Retcode c){Code=c;}}
 private static void Check(bool valid,IslandDisjoinEquipmentRsp.Retcode code){if(!valid)throw new Reject(code);}
 internal static IslandDisjoinEquipmentRsp Execute(Player p,IslandDisjoinEquipmentReq req)
 {
  var rsp=new IslandDisjoinEquipmentRsp{retcode=IslandDisjoinEquipmentRsp.Retcode.Fail};
  lock(p.Equipment)
  {
   try
   {
    var ids=req.UniqueIdLists??Array.Empty<uint>();
    Check(ids.Length>0&&ids.Length<=50,IslandDisjoinEquipmentRsp.Retcode.BatchNum);
    Check(ids.All(x=>x>0)&&ids.Distinct().Count()==ids.Length,IslandDisjoinEquipmentRsp.Retcode.ConsumeItemRepeat);
    Check(req.Type==EquipmentType.EquipmentWeapon||req.Type==EquipmentType.EquipmentStigmata,IslandDisjoinEquipmentRsp.Retcode.Fail);
    Check(BaseExcelReader<PlayerLevelData,PlayerLevelDataExcel>.GetInstance().CalculateLevel(p.User.Exp).Level>=25,IslandDisjoinEquipmentRsp.Retcode.LevelLack);
    Check(req.DisjoinType==DisjoinType.DisjoinTotal,IslandDisjoinEquipmentRsp.Retcode.TitleLevelError);
    var rewards=new Dictionary<int,long>();long cost=0;
    void Add(int id,long n){Check(id>0&&n>0,IslandDisjoinEquipmentRsp.Retcode.Fail);rewards[id]=checked(rewards.GetValueOrDefault(id)+n);}
    foreach(uint uid in ids)
    {
     bool weapon=req.Type==EquipmentType.EquipmentWeapon;
     if(weapon)
     {
      var item=p.Equipment.WeaponList.SingleOrDefault(x=>x.UniqueId==uid);Check(item!=null,IslandDisjoinEquipmentRsp.Retcode.ConsumeItemNotExist);
      Check(!item.IsProtected,IslandDisjoinEquipmentRsp.Retcode.ConsumeItemProtected);
      Check(!(p.AvatarList??Array.Empty<AvatarScheme>()).Any(x=>x.WeaponUniqueId==uid),IslandDisjoinEquipmentRsp.Retcode.ConsumeItemInUse);
      var row=BaseExcelReader<WeaponData,WeaponDataExcel>.GetInstance().FromId(checked((int)item.Id));Check(row!=null,IslandDisjoinEquipmentRsp.Retcode.Fail);
      var keys=JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/GrandKeyMainWeapon.json"));
      Check(!item.IsExtracted&&item.SubWeaponId==0&&!keys.Any(x=>(int)x["mainWeaponID"]==row.WeaponMainId),IslandDisjoinEquipmentRsp.Retcode.ConsumeItemGrandKey);
      EquipmentDisjoinReturns.Weapon(row,item,rewards);
      Check(row.DisjoinScoinCost>=0&&row.DisjoinAddMaterial?.Length>0,IslandDisjoinEquipmentRsp.Retcode.Fail);
      cost=checked(cost+row.DisjoinScoinCost);foreach(var x in row.DisjoinAddMaterial)Add(x.Id,x.Num);
     }
     else
     {
      var item=p.Equipment.StigmataList.SingleOrDefault(x=>x.UniqueId==uid);Check(item!=null,IslandDisjoinEquipmentRsp.Retcode.ConsumeItemNotExist);
      Check(!item.IsProtected,IslandDisjoinEquipmentRsp.Retcode.ConsumeItemProtected);
      Check(!(p.AvatarList??Array.Empty<AvatarScheme>()).Any(x=>x.StigmataUniqueId1==uid||x.StigmataUniqueId2==uid||x.StigmataUniqueId3==uid),IslandDisjoinEquipmentRsp.Retcode.ConsumeItemInUse);
      Check(item.PromoteTimes==0,IslandDisjoinEquipmentRsp.Retcode.TitleLevelError);
      Check(item.WaitSelectRuneLists.Count==0&&item.WaitSelectRuneGroupLists.Count==0,IslandDisjoinEquipmentRsp.Retcode.TitleLevelError);
      var row=BaseExcelReader<StigmataData,StigmataDataExcel>.GetInstance().FromId(checked((int)item.Id));Check(row!=null,IslandDisjoinEquipmentRsp.Retcode.Fail);
      EquipmentDisjoinReturns.Stigmata(row,item,rewards);
      Check(row.DisjoinScoinCost>=0&&row.DisjoinAddMaterial?.Length>0,IslandDisjoinEquipmentRsp.Retcode.Fail);
      cost=checked(cost+row.DisjoinScoinCost);foreach(var x in row.DisjoinAddMaterial)Add(x.Id,x.Num);
     }
    }
    Post62Materials.Normalize(rewards);
    Check(cost<=int.MaxValue,IslandDisjoinEquipmentRsp.Retcode.Fail);
    long coins=p.Equipment.MaterialList.Where(x=>x.Id==100).Sum(x=>(long)x.Num);Check(coins>=cost,IslandDisjoinEquipmentRsp.Retcode.ScoinLack);
    foreach(var x in rewards){var row=BaseExcelReader<MaterialData,MaterialDataExcel>.GetInstance().FromId(checked((uint)x.Key));Check(row!=null,IslandDisjoinEquipmentRsp.Retcode.Fail);long n=p.Equipment.MaterialList.Where(y=>y.Id==x.Key).Sum(y=>(long)y.Num);long cap=row.QuantityLimit>0?Math.Min(row.QuantityLimit,int.MaxValue):int.MaxValue;Check(x.Value<=int.MaxValue&&n+x.Value-(x.Key==100?cost:0)<=cap,IslandDisjoinEquipmentRsp.Retcode.EquipmentFull);}
    _=Common.Database.Equipment.collection;
    string snapshot=JsonConvert.SerializeObject(p.Equipment);
    try
    {
     Global.Store.InTransaction(()=>{
      long debit=cost;foreach(var item in p.Equipment.MaterialList.Where(x=>x.Id==100)){uint n=(uint)Math.Min(debit,item.Num);item.Num-=n;debit-=n;}
      foreach(var x in rewards)p.Equipment.AddMaterial(x.Key,(int)x.Value);
      if(req.Type==EquipmentType.EquipmentWeapon)p.Equipment.WeaponList=p.Equipment.WeaponList.Where(x=>!ids.Contains(x.UniqueId)).ToArray();
      else p.Equipment.StigmataList=p.Equipment.StigmataList.Where(x=>!ids.Contains(x.UniqueId)).ToArray();
      DailyGameplay.Add(p,118,0,(uint)ids.Length);p.Equipment.Save();
     });
    }
    catch{p.Equipment=JsonConvert.DeserializeObject<EquipmentScheme>(snapshot);throw;}
    rsp.retcode=IslandDisjoinEquipmentRsp.Retcode.Succ;rsp.DisjoinUniqueIdLists=ids.ToArray();return rsp;
   }
   catch(Reject ex){rsp.retcode=ex.Code;return rsp;}
   catch(Exception ex){Packet.c.Warn("[EquipmentDisjoin] rejected without committed inventory loss: "+ex.Message);return rsp;}
  }
 }
}

