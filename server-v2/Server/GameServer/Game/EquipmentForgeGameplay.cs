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
internal static class EquipmentForgeGameplay
{
 public sealed class State:IRow{public long Id{get;set;}public long CreatedAt{get;set;}public uint Uid{get;set;}public Dictionary<uint,Receipt> Counts{get;set;}=new();}
 public sealed class Receipt{public long Day{get;set;}public uint Count{get;set;}}
 private static SqlTable<State> Store=>Global.Table<State>("EquipmentForgeStates",("owner_uid","INTEGER",s=>s.Uid,true));
 private static JArray Rows()=>JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/EquipForgeData.json"));
 private static State Load(Player p)=>Store.All().FirstOrDefault(x=>x.Uid==p.User.Uid)??new State{Uid=p.User.Uid};
 private static uint Used(State s,JToken row){uint id=(uint)row["ForgeId"];return s.Counts.TryGetValue(id,out var r)&&((int)row["RefreshTimeType"]!=2||r.Day==DailyGameplay.Day(DailyGameplay.Clock()))?r.Count:0;}
 private static bool Supported(JToken row)
 {
  // Verified predecessor-weapon recipes (PRI-ARMs) and weapon spirits are supported; affix inheritance stays separate.
  if(row["ForgeParaStr"].Any(x=>BaseExcelReader<MaterialData,MaterialDataExcel>.GetInstance().FromId((uint)x["itemMetaID"])==null && BaseExcelReader<WeaponData,WeaponDataExcel>.GetInstance().FromId((int)x["itemMetaID"])?.WeaponQuality!=47))return false;
  int target=(int)row["TargetId"],type=(int)row["EquipmentType"];
  if(row["PreEquipmentList"].Any() && (type!=1 || row["PreEquipmentList"].Any(x=>BaseExcelReader<WeaponData,WeaponDataExcel>.GetInstance().FromId((int)x["WeaponID"])==null)))return false;
  return type==1?GachaEligibility.IsAllowedWeapon(target)&&BaseExcelReader<WeaponData,WeaponDataExcel>.GetInstance().FromId(target)!=null:
   type==2&&BaseExcelReader<StigmataData,StigmataDataExcel>.GetInstance().FromId(target)!=null;
 }
 internal static GetEquipmentForgeDataRsp Build(Player p)
 {
  lock(p.Equipment){var s=Load(p);var rows=Rows().Where(Supported).ToArray();var rsp=new GetEquipmentForgeDataRsp{retcode=0,ScheduleId=1,ForgeLists=rows.Select(x=>(uint)x["ForgeId"]).ToArray()};
   foreach(var r in rows){uint n=Used(s,r);if(n>0)rsp.HasForgeLists.Add(new EquipmentForge{ForgeId=(uint)r["ForgeId"],Times=n});}return rsp;}
 }
 private sealed class Reject:Exception{internal ForgeEquipmentRsp.Retcode Code;internal Reject(ForgeEquipmentRsp.Retcode c){Code=c;}}
 private static void Check(bool b,ForgeEquipmentRsp.Retcode c){if(!b)throw new Reject(c);}
 internal static ForgeEquipmentRsp Forge(Player p,ForgeEquipmentReq req)
 {
  var rsp=new ForgeEquipmentRsp{ForgeId=req.ForgeId,retcode=ForgeEquipmentRsp.Retcode.Fail};lock(p.Equipment){try{
   var row=Rows().SingleOrDefault(x=>(uint)x["ForgeId"]==req.ForgeId);Check(row!=null&&Supported(row),ForgeEquipmentRsp.Retcode.FeatureClosed);
   int lv=BaseExcelReader<PlayerLevelData,PlayerLevelDataExcel>.GetInstance().CalculateLevel(p.User.Exp).Level;
   Check(lv>=(int)row["MinLevel"]&&lv<=(int)row["MaxLevel"],ForgeEquipmentRsp.Retcode.LevelLimit);
   var state=Load(p);int target=(int)row["TargetId"],type=(int)row["EquipmentType"];
uint used=Used(state,row);
   int owned=type==1?p.Equipment.WeaponList.Count(x=>x.Id==target):p.Equipment.StigmataList.Count(x=>x.Id==target);
   Check(((int)row["MaxForgeType"]==1?used:owned)<(int)row["MaxBuyTimes"],ForgeEquipmentRsp.Retcode.NoTimes);
   // Retaining selected affixes requires the separate, explicit native cost list.
   Check(req.KeepRuneEquipUniqueId==0&&(req.KeepRuneCostItemList?.ItemLists.Count??0)==0,ForgeEquipmentRsp.Retcode.FeatureClosed);
   var costs=new Dictionary<uint,long>();void Cost(uint id,long n){Check(id>0&&n>0,ForgeEquipmentRsp.Retcode.Fail);costs[id]=checked(costs.GetValueOrDefault(id)+n);}
   foreach(var c in row["CurrencyList"])Cost((uint)c["ItemID"],(long)c["Number"]);
   var expectedGear=new List<(int id,int level,int count)>();
   foreach(var c in row["ForgeParaStr"]){int id=(int)c["itemMetaID"],level=(int)c["itemLevel"],n=(int)c["itemNum"];if(BaseExcelReader<MaterialData,MaterialDataExcel>.GetInstance().FromId((uint)id)!=null)Cost((uint)id,n);else expectedGear.Add((id,level,n));}
   var selected=req.ConsumeItemList?.ItemLists??new List<EquipmentItem>();
   Check(selected.Count<=100&&selected.All(x=>x.Num>0),ForgeEquipmentRsp.Retcode.ConsumeItemMatchingLack);
   var gear=selected.Where(x=>x.Type==EquipmentType.EquipmentWeapon||x.Type==EquipmentType.EquipmentStigmata).ToArray();
   Check(gear.Select(x=>(x.Type,x.IdOrUniqueId)).Distinct().Count()==gear.Length&&gear.All(x=>x.Num==1),ForgeEquipmentRsp.Retcode.ConsumeItemRepeat);
   var alternatives=row["PreEquipmentList"].ToArray();
   if(alternatives.Length>0){Check(gear.Length==1&&gear[0].Type==EquipmentType.EquipmentWeapon,ForgeEquipmentRsp.Retcode.ConsumeItemMatchingLack);var w=p.Equipment.WeaponList.SingleOrDefault(x=>x.UniqueId==gear[0].IdOrUniqueId);Check(w!=null&&alternatives.Any(x=>(uint)x["WeaponID"]==w.Id&&w.Level>=(uint)x["Level"]),ForgeEquipmentRsp.Retcode.ConsumeItemMatchingLack);}
   else{Check(gear.Length==expectedGear.Sum(x=>x.count),ForgeEquipmentRsp.Retcode.ConsumeItemMatchingLack);foreach(var e in expectedGear)Check(gear.Count(g=>g.Type==EquipmentType.EquipmentWeapon?p.Equipment.WeaponList.Any(w=>w.UniqueId==g.IdOrUniqueId&&w.Id==e.id&&w.Level>=e.level):p.Equipment.StigmataList.Any(w=>w.UniqueId==g.IdOrUniqueId&&w.Id==e.id&&w.Level>=e.level))==e.count,ForgeEquipmentRsp.Retcode.ConsumeItemMatchingLack);}
   var returns=new Dictionary<int,long>();
   foreach(var g in gear){
    if(g.Type==EquipmentType.EquipmentWeapon){var w=p.Equipment.WeaponList.SingleOrDefault(x=>x.UniqueId==g.IdOrUniqueId);Check(w!=null,ForgeEquipmentRsp.Retcode.ConsumeItemNotExist);Check(!w.IsProtected,ForgeEquipmentRsp.Retcode.ConsumeItemProtected);Check(!p.AvatarList.Any(x=>x.WeaponUniqueId==w.UniqueId),ForgeEquipmentRsp.Retcode.ConsumeItemInUse);Check(!w.IsExtracted&&w.SubWeaponId==0,ForgeEquipmentRsp.Retcode.ConsumeItemGrandKey);var meta=BaseExcelReader<WeaponData,WeaponDataExcel>.GetInstance().FromId((int)w.Id);var keys=JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/GrandKeyMainWeapon.json"));Check(alternatives.Length>0 || !keys.Any(x=>(int)x["mainWeaponID"]==meta.WeaponMainId),ForgeEquipmentRsp.Retcode.ConsumeItemGrandKey);// Spirit recipes create their input atLv50; this synthetic level is not spent training experience.
    if(meta.WeaponQuality!=47)EquipmentDisjoinReturns.Weapon(meta,w,returns);}
    else{var st=p.Equipment.StigmataList.SingleOrDefault(x=>x.UniqueId==g.IdOrUniqueId);Check(st!=null,ForgeEquipmentRsp.Retcode.ConsumeItemNotExist);Check(!st.IsProtected,ForgeEquipmentRsp.Retcode.ConsumeItemProtected);Check(!p.AvatarList.Any(x=>x.StigmataUniqueId1==st.UniqueId||x.StigmataUniqueId2==st.UniqueId||x.StigmataUniqueId3==st.UniqueId),ForgeEquipmentRsp.Retcode.ConsumeItemInUse);Check(st.RefineValue==0&&st.PromoteTimes==0&&st.WaitSelectRuneLists.Count==0&&st.WaitSelectRuneGroupLists.Count==0,ForgeEquipmentRsp.Retcode.WaitSelectedRune);EquipmentDisjoinReturns.Stigmata(BaseExcelReader<StigmataData,StigmataDataExcel>.GetInstance().FromId((int)st.Id),st,returns);}
   }
   foreach(var c in costs)Check(c.Value<=int.MaxValue&&p.Equipment.MaterialList.Where(x=>x.Id==c.Key).Sum(x=>(long)x.Num)>=c.Value,ForgeEquipmentRsp.Retcode.CurrencyLack);
   var extra=selected.Where(x=>x.Type!=EquipmentType.EquipmentWeapon&&x.Type!=EquipmentType.EquipmentStigmata).ToArray();
   Check(extra.All(x=>x.Type==EquipmentType.EquipmentMaterial&&costs.ContainsKey(x.IdOrUniqueId))&&extra.GroupBy(x=>x.IdOrUniqueId).All(g=>g.Sum(x=>(long)x.Num)<=costs[g.Key]),ForgeEquipmentRsp.Retcode.ConsumeItemMatchingRedundant);
   var plan=new DailyGameplay.Plan{Items=returns};DailyGameplay.Validate(p,plan);
   _=Store;string backup=JsonConvert.SerializeObject(p.Equipment);
   try{bool ok=DailyGameplay.Atomic(p,()=>{foreach(var c in costs)p.Equipment.AddMaterial((int)c.Key,-(int)c.Value);p.Equipment.WeaponList=p.Equipment.WeaponList.Where(w=>!gear.Any(g=>g.Type==EquipmentType.EquipmentWeapon&&g.IdOrUniqueId==w.UniqueId)).ToArray();p.Equipment.StigmataList=p.Equipment.StigmataList.Where(w=>!gear.Any(g=>g.Type==EquipmentType.EquipmentStigmata&&g.IdOrUniqueId==w.UniqueId)).ToArray();if(type==1)p.Equipment.AddWeapon(target).Level=(uint)row["TargetInitLevel"];else p.Equipment.AddStigmata(target).Level=(uint)row["TargetInitLevel"];DailyGameplay.Grant(p,plan);state.Counts[req.ForgeId]=new Receipt{Day=DailyGameplay.Day(DailyGameplay.Clock()),Count=used+1};if(state.Id==0)Store.InsertOne(state);else Store.ReplaceOne(state);});if(!ok){p.Equipment=JsonConvert.DeserializeObject<EquipmentScheme>(backup);return rsp;}}
   catch{p.Equipment=JsonConvert.DeserializeObject<EquipmentScheme>(backup);throw;}
   foreach(var r in returns)rsp.ReturnMaterialLists.Add(new Common.Resources.Proto.Material{Id=(uint)r.Key,Num=(uint)r.Value});rsp.retcode=0;return rsp;
  }catch(Reject e){rsp.retcode=e.Code;return rsp;}catch(Exception e){Packet.c.Warn("[EquipmentForge] "+e.Message);return rsp;}}
 }
}
