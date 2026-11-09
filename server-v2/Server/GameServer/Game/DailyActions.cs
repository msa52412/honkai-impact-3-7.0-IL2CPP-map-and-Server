using System;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
namespace PemukulPaku.GameServer.Game;
internal static class DailyActions
{
 internal static EquipmentPowerUpRsp PowerUp(Player p,EquipmentPowerUpReq req)
 {
  var rsp=new EquipmentPowerUpRsp{retcode=EquipmentPowerUpRsp.Retcode.Fail,MainItem=req.MainItem,BoostRate=100};lock(p.Equipment)
  {
   if(req.MainItem==null||req.ConsumeItemList?.ItemLists==null||req.ConsumeItemList.ItemLists.Count==0||req.ConsumeItemList.ItemLists.Count>100)return rsp;
   var w=p.Equipment.WeaponList.FirstOrDefault(x=>x.UniqueId==req.MainItem.IdOrUniqueId);var st=p.Equipment.StigmataList.FirstOrDefault(x=>x.UniqueId==req.MainItem.IdOrUniqueId);
   int max;
   if(req.MainItem.Type==EquipmentType.EquipmentWeapon&&w!=null)max=BaseExcelReader<WeaponData,WeaponDataExcel>.GetInstance().FromId((int)w.Id)?.MaxLv??0;
   else if(req.MainItem.Type==EquipmentType.EquipmentStigmata&&st!=null)max=BaseExcelReader<StigmataData,StigmataDataExcel>.GetInstance().FromId((int)st.Id)?.MaxLv??0;
   else{rsp.retcode=EquipmentPowerUpRsp.Retcode.MainItemNotExist;return rsp;}
   if((req.MainItem.Type==EquipmentType.EquipmentWeapon?w.Level:st.Level)>=max){rsp.retcode=EquipmentPowerUpRsp.Retcode.MainItemLevelFull;return rsp;}
   long exp=0;
   foreach(var g in req.ConsumeItemList.ItemLists.GroupBy(x=>x.IdOrUniqueId))
   {
    var c=BaseExcelReader<MaterialData,MaterialDataExcel>.GetInstance().FromId(g.Key);long amount=g.Sum(x=>(long)x.Num);
    long held=p.Equipment.MaterialList.Where(x=>x.Id==g.Key).Sum(x=>(long)x.Num);
    if(g.Any(x=>x.Type!=EquipmentType.EquipmentMaterial||x.Num==0)||amount>held||c==null||c.GearExpProvideBase<=0){rsp.retcode=EquipmentPowerUpRsp.Retcode.ConsumeItemNotExist;return rsp;}
    exp+=amount*c.GearExpProvideBase;
   }
   if(exp<=0||exp>int.MaxValue/2)return rsp;
   uint beforeLevel=req.MainItem.Type==EquipmentType.EquipmentWeapon?w.Level:st.Level;uint beforeExp=req.MainItem.Type==EquipmentType.EquipmentWeapon?w.Exp:st.Exp;
   bool ok=DailyGameplay.Atomic(p,()=>{
    if(req.MainItem.Type==EquipmentType.EquipmentWeapon)p.Equipment.AddWeaponExp(req.MainItem.IdOrUniqueId,(int)exp);else p.Equipment.AddStigmataExp(req.MainItem.IdOrUniqueId,(int)exp);
    uint afterLevel=req.MainItem.Type==EquipmentType.EquipmentWeapon?w.Level:st.Level;uint afterExp=req.MainItem.Type==EquipmentType.EquipmentWeapon?w.Exp:st.Exp;
    if(afterLevel==beforeLevel&&afterExp==beforeExp)throw new InvalidOperationException("No equipment growth");
    foreach(var g in req.ConsumeItemList.ItemLists.GroupBy(x=>x.IdOrUniqueId))p.Equipment.AddMaterial((int)g.Key,-checked((int)g.Sum(x=>(long)x.Num)));
    DailyGameplay.Add(p,req.MainItem.Type==EquipmentType.EquipmentWeapon?49:50,0);
   });if(ok)rsp.retcode=EquipmentPowerUpRsp.Retcode.Succ;return rsp;
  }
 }
 internal static AddAvatarExpByMaterialRsp Feed(Player p,AddAvatarExpByMaterialReq req)
 {
  var rsp=new AddAvatarExpByMaterialRsp{retcode=AddAvatarExpByMaterialRsp.Retcode.Fail};lock(p.Equipment)
  {
   var a=p.AvatarList.FirstOrDefault(x=>x.AvatarId==req.AvatarId&&x.Star>0);
   if(a==null){rsp.retcode=AddAvatarExpByMaterialRsp.Retcode.AvatarNotExist;return rsp;}
   var c=BaseExcelReader<MaterialData,MaterialDataExcel>.GetInstance().FromId(req.MaterialId);
   if(c==null||c.CharacterExpProvide<=0||req.MaterialNum==0){rsp.retcode=AddAvatarExpByMaterialRsp.Retcode.MaterailCanNotAddExp;return rsp;}
   long held=p.Equipment.MaterialList.Where(x=>x.Id==req.MaterialId).Sum(x=>(long)x.Num);long exp=(long)c.CharacterExpProvide*req.MaterialNum;
   if(held<req.MaterialNum||exp>int.MaxValue/2){rsp.retcode=AddAvatarExpByMaterialRsp.Retcode.MaterailNotEnough;return rsp;}
   uint lv=a.Level,ae=a.Exp;
   if(DailyGameplay.Atomic(p,()=>{a.AddExp((uint)exp);if(a.Level==lv&&a.Exp==ae)throw new InvalidOperationException("No avatar growth");p.Equipment.AddMaterial((int)req.MaterialId,-checked((int)req.MaterialNum));DailyGameplay.Add(p,12,0);a.Save();}))rsp.retcode=AddAvatarExpByMaterialRsp.Retcode.Succ;
   return rsp;
  }
 }
 internal static AvatarSubSkillLevelUpRsp Skill(Player p,AvatarSubSkillLevelUpReq req)
 {
  var rsp=new AvatarSubSkillLevelUpRsp{retcode=AvatarSubSkillLevelUpRsp.Retcode.Fail};lock(p.Equipment)
  {
   var a=p.AvatarList.FirstOrDefault(x=>x.AvatarId==req.AvatarId&&x.Star>0);
   if(a==null){rsp.retcode=AvatarSubSkillLevelUpRsp.Retcode.AvatarNotExist;return rsp;}
   var cfg=BaseExcelReader<AvatarSubSkillData,AvatarSubSkillDataExcel>.GetInstance().FromId((int)req.SubSkillId);
   var ac=BaseExcelReader<AvatarData,AvatarDataExcel>.GetInstance().FromId((int)a.AvatarId);
   if(cfg==null||!(ac?.SkillList??Array.Empty<int>()).Contains(cfg.SkillId)){rsp.retcode=AvatarSubSkillLevelUpRsp.Retcode.SubSkillNotExist;return rsp;}
   if(a.Level<cfg.UnlockLv){rsp.retcode=AvatarSubSkillLevelUpRsp.Retcode.AvatarLevelLack;return rsp;}
   if(a.Star<cfg.UnlockStar||(a.Star==cfg.UnlockStar&&a.SubStar<cfg.UnlockSubStar)){rsp.retcode=AvatarSubSkillLevelUpRsp.Retcode.AvatarStarLack;return rsp;}
   ulong before=a.SkillLists.SelectMany(x=>x.SubSkillLists).Aggregate(0UL,(v,x)=>v+x.Level);
   if(DailyGameplay.Atomic(p,()=>{a.LevelUpSkill(req.SubSkillId,req.IsLevelUpAll);ulong after=a.SkillLists.SelectMany(x=>x.SubSkillLists).Aggregate(0UL,(v,x)=>v+x.Level);if(after<=before)throw new InvalidOperationException("No skill growth");DailyGameplay.Add(p,13,0);a.Save();}))rsp.retcode=AvatarSubSkillLevelUpRsp.Retcode.Succ;
   return rsp;
  }
 }
}
