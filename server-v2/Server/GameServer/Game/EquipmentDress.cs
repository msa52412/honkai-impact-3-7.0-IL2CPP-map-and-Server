using System;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json;
namespace PemukulPaku.GameServer.Game;
internal static class EquipmentDress
{
 internal static uint Slot(AvatarScheme a,EquipmentSlot slot)=>slot switch {
  EquipmentSlot.EquipmentSlotWeapon1=>a.WeaponUniqueId,
  EquipmentSlot.EquipmentSlotStigmata1=>a.StigmataUniqueId1,
  EquipmentSlot.EquipmentSlotStigmata2=>a.StigmataUniqueId2,
  EquipmentSlot.EquipmentSlotStigmata3=>a.StigmataUniqueId3,_=>0};
 private static void Set(AvatarScheme a,EquipmentSlot slot,uint id){switch(slot){
  case EquipmentSlot.EquipmentSlotWeapon1:a.WeaponUniqueId=id;break;
  case EquipmentSlot.EquipmentSlotStigmata1:a.StigmataUniqueId1=id;break;
  case EquipmentSlot.EquipmentSlotStigmata2:a.StigmataUniqueId2=id;break;
  case EquipmentSlot.EquipmentSlotStigmata3:a.StigmataUniqueId3=id;break;}}
 // Validate against the recovered native table, including new-version rows.
 internal static int[] Types(AvatarScheme a)=>BaseExcelReader<AvatarData,AvatarDataExcel>.GetInstance().FromId((int)a.AvatarId)?.WeaponBaseTypeList??Array.Empty<int>();
 internal static int InitialWeapon(AvatarScheme a){
  var types=Types(a);if(types.Length==0)throw new InvalidOperationException("Missing avatar weapon type");
  var meta=BaseExcelReader<AvatarData,AvatarDataExcel>.GetInstance().FromId((int)a.AvatarId);
  var initial=meta==null?null:BaseExcelReader<WeaponData,WeaponDataExcel>.GetInstance().FromId(meta.InitialWeapon);
  if(initial!=null&&types.Contains(initial.BaseType))return initial.Id;
  return BaseExcelReader<WeaponData,WeaponDataExcel>.GetInstance().All.Where(x=>types.Contains(x.BaseType)&&x.WeaponQuality!=47).OrderBy(x=>x.Rarity).ThenBy(x=>x.Id).First().Id;
 }
 internal static bool Accepts(AvatarScheme a,Weapon w){
  var row=BaseExcelReader<WeaponData,WeaponDataExcel>.GetInstance().FromId((int)w.Id);
  if(row==null||row.WeaponQuality==47)return false;
  return Types(a).Contains(row.BaseType);
 }
 internal static DressEquipmentRsp Execute(Player p,DressEquipmentReq req,out uint[] changed)
 {
  changed=Array.Empty<uint>();var rsp=new DressEquipmentRsp{retcode=DressEquipmentRsp.Retcode.Fail};
  lock(p.Equipment){
   var target=p.AvatarList.FirstOrDefault(a=>a.AvatarId==req.AvatarId&&a.Star>0);
   if(target==null){rsp.retcode=DressEquipmentRsp.Retcode.AvatarNotExist;return rsp;}
   if(req.Slot<EquipmentSlot.EquipmentSlotWeapon1||req.Slot>EquipmentSlot.EquipmentSlotStigmata3){rsp.retcode=DressEquipmentRsp.Retcode.EquipmentSlotError;return rsp;}
   bool weapon=req.Slot==EquipmentSlot.EquipmentSlotWeapon1;
   if(weapon){var w=p.Equipment.WeaponList.SingleOrDefault(x=>x.UniqueId==req.UniqueId);
    if(w==null){rsp.retcode=DressEquipmentRsp.Retcode.EquipmentNotExist;return rsp;}
    if(!Accepts(target,w)){rsp.retcode=DressEquipmentRsp.Retcode.EquipmentSlotError;return rsp;}
   }else if(req.UniqueId!=0){var st=p.Equipment.StigmataList.SingleOrDefault(x=>x.UniqueId==req.UniqueId);
    if(st==null){rsp.retcode=DressEquipmentRsp.Retcode.EquipmentNotExist;return rsp;}
    var row=BaseExcelReader<StigmataData,StigmataDataExcel>.GetInstance().FromId((int)st.Id);
    if(row==null||row.BaseType!=(int)req.Slot-1){rsp.retcode=DressEquipmentRsp.Retcode.EquipmentSlotError;return rsp;}
   }
   var affected=p.AvatarList.Where(a=>a==target||req.UniqueId!=0&&Slot(a,req.Slot)==req.UniqueId).ToArray();
   var before=affected.Select(a=>(avatar:a,id:Slot(a,req.Slot))).ToArray();var oldWeapons=p.Equipment.WeaponList;
   try{
    uint previous=Slot(target,req.Slot);Set(target,req.Slot,req.UniqueId);
    foreach(var donor in affected.Where(a=>a!=target)){
     uint replacement=0;
     if(weapon){
      bool Free(Weapon w)=>w.UniqueId!=req.UniqueId&&!p.AvatarList.Any(a=>a!=donor&&a.WeaponUniqueId==w.UniqueId)&&Accepts(donor,w);
      var old=p.Equipment.WeaponList.FirstOrDefault(w=>w.UniqueId==previous&&Free(w));
      var fallback=old??p.Equipment.WeaponList.FirstOrDefault(Free);
      if(fallback==null){fallback=p.Equipment.AddWeapon(InitialWeapon(donor));
       if(!Accepts(donor,fallback))throw new InvalidOperationException("Invalid donor initial weapon");
      }
      replacement=fallback.UniqueId;
     }
     Set(donor,req.Slot,replacement);
    }
    _=Common.Database.Avatar.collection;_=Common.Database.Equipment.collection;
    Global.Store.InTransaction(()=>{foreach(var a in affected)a.Save();p.Equipment.Save();});
    changed=affected.Select(a=>a.AvatarId).ToArray();rsp.retcode=DressEquipmentRsp.Retcode.Succ;
   }catch(Exception ex){foreach(var x in before)Set(x.avatar,req.Slot,x.id);p.Equipment.WeaponList=oldWeapons;Packet.c.Warn("[EquipmentDress] rolled back: "+ex.Message);}
   return rsp;
  }
 }
}
