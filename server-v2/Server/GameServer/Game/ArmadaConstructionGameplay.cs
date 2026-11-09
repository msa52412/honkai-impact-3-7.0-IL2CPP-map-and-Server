using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Newtonsoft.Json.Linq;
using PemukulPaku.GameServer.MPModule;
namespace PemukulPaku.GameServer.Game;
// Construction costs/durations/limits use local tables. Lease300s and the
// single-grid cabin layout are explicit compatibility behavior, not a full MP replica.
internal static class ArmadaConstructionGameplay
{
 public sealed class Cabin{public uint Id{get;set;}public ArmadaCabinType Type{get;set;}public uint Level{get;set;}public uint Grid{get;set;}}
 public sealed class State:IRow{public long Id{get;set;}public long CreatedAt{get;set;}public uint Armada{get;set;}public uint Owner{get;set;}public uint OwnerUntil{get;set;}public List<Cabin> Cabins{get;set;}=new();public Cabin Pending{get;set;}public uint End{get;set;}public uint? PaidFund{get;set;}}
 private static SqlTable<State> Store=>Global.Table<State>("ArmadaConstructionStates",("armada_id","INTEGER",x=>x.Armada,true));
 private static ArmadaModule Module=>ArmadaModule.GetInstance();
 private static uint Now=>checked((uint)DailyGameplay.Clock());
 private static JArray Table(string name)=>JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/"+name+".json"));
 private static JObject ArmadaRow(uint level)=>Table("ArmadaData").OfType<JObject>().FirstOrDefault(x=>(uint)x["level"]==level);
 private static void Save(State s){if(s.Id==0)Store.InsertOne(s);else Store.ReplaceOne(s);}
 private static State Load(Armada a){
  var s=Store.All().FirstOrDefault(x=>x.Armada==a.Id)??new State{Armada=a.Id,Cabins=new(){new Cabin{Id=1,Type=ArmadaCabinType.ArmadaCabinMain,Level=a.Level,Grid=0}}};
  if(s.OwnerUntil<=Now){s.Owner=s.OwnerUntil=0;}
  if(s.Pending!=null&&s.End<=Now){Global.Store.InTransaction(()=>{
   var old=s.Cabins.FirstOrDefault(x=>x.Id==s.Pending.Id&&x.Type==s.Pending.Type);
   if(old==null)s.Cabins.Add(s.Pending);else{old.Level=s.Pending.Level;old.Grid=s.Pending.Grid;}
   if(s.Pending.Type==ArmadaCabinType.ArmadaCabinMain){uint level=s.Pending.Level;if(Module.Update(a.LeaderUid,x=>x.Level=level)!=0)throw new InvalidOperationException("Armada disappeared");a.Level=level;}
   s.Pending=null;s.End=0;s.PaidFund=null;Save(s);
  });}
  return s;
 }
 internal static GetArmadaBuildOwnershipRsp Own(Player p){lock(ArmadaModule.SyncGate){
  var rsp=new GetArmadaBuildOwnershipRsp{retcode=GetArmadaBuildOwnershipRsp.Retcode.Fail};var a=Module.ByUid(p.User.Uid);
  if(a==null){rsp.retcode=GetArmadaBuildOwnershipRsp.Retcode.NotInArmada;return rsp;}
  if(a.LeaderUid!=p.User.Uid){rsp.retcode=GetArmadaBuildOwnershipRsp.Retcode.NotGranted;return rsp;}
  try{var s=Load(a);s.Owner=p.User.Uid;s.OwnerUntil=checked(Now+300);Save(s);rsp.retcode=0;rsp.BuildOwner=s.Owner;rsp.BuildOwnershipTime=s.OwnerUntil;}catch(Exception ex){Packet.c.Warn("[ArmadaConstruction] ownership: "+ex.Message);}
  return rsp;
 }}
 internal static ReleaseArmadaBuildOwnershipRsp Release(Player p){lock(ArmadaModule.SyncGate){
  var rsp=new ReleaseArmadaBuildOwnershipRsp{retcode=ReleaseArmadaBuildOwnershipRsp.Retcode.Fail};var a=Module.ByUid(p.User.Uid);if(a==null)return rsp;
  try{var s=Load(a);if(s.Owner!=0&&s.Owner!=p.User.Uid)return rsp;s.Owner=s.OwnerUntil=0;Save(s);rsp.retcode=0;}catch(Exception ex){Packet.c.Warn("[ArmadaConstruction] release: "+ex.Message);}return rsp;
 }}
 internal static GetArmadaManageDataRsp Manage(Player p){lock(ArmadaModule.SyncGate){
  var a=Module.ByUid(p.User.Uid);var rsp=new GetArmadaManageDataRsp{retcode=GetArmadaManageDataRsp.Retcode.NotInArmada};if(a==null)return rsp;
  if(a.LeaderUid!=p.User.Uid){rsp.retcode=GetArmadaManageDataRsp.Retcode.NotGranted;return rsp;}
  var s=Load(a);rsp.retcode=0;rsp.ApplyLevel=a.ApplyLevel;rsp.AutoPass=a.AutoPass;rsp.AutoPassLevel=a.AutoPassLevel;rsp.BuildOwner=s.Owner;rsp.BuildOwnershipTime=s.OwnerUntil;return rsp;
 }}
 private static (string file,string cap) Names(ArmadaCabinType t)=>t switch{
  ArmadaCabinType.ArmadaCabinContact=>("ArmadaContactData","ContactNum"),
  ArmadaCabinType.ArmadaCabinHangar=>("ArmadaHangarData","HangarNum"),
  ArmadaCabinType.ArmadaCabinWorkshop=>("ArmadaWorkshopData","workshopNum"),
  ArmadaCabinType.ArmadaCabinExchequer=>("ArmadaExchequerData","exchequerNum"),
  ArmadaCabinType.ArmadaCabinBoss=>("ArmadaBossData","BossNum"),_=>throw new InvalidOperationException("Unknown cabin type")};
 private static bool Grid(Armada a,uint grid)=>Table("ArmadaGridData").Take((int)ArmadaRow(a.Level)["grids"]).Any(x=>(uint)x["gridID"]==grid);
 internal static LevelUpArmadaCabinRsp Start(Player p,LevelUpArmadaCabinReq req){lock(ArmadaModule.SyncGate){
  var rsp=new LevelUpArmadaCabinRsp{retcode=LevelUpArmadaCabinRsp.Retcode.Fail,Type=req.Type,CabinId=req.CabinId,GridId=req.GridId};var a=Module.ByUid(p.User.Uid);
  if(a==null){rsp.retcode=LevelUpArmadaCabinRsp.Retcode.NotInArmada;return rsp;}
  if(a.LeaderUid!=p.User.Uid){rsp.retcode=LevelUpArmadaCabinRsp.Retcode.NotGranted;return rsp;}
  try{var s=Load(a);if(s.Owner!=p.User.Uid){rsp.retcode=LevelUpArmadaCabinRsp.Retcode.NoOwnership;return rsp;}
   if(s.Pending!=null){rsp.retcode=LevelUpArmadaCabinRsp.Retcode.Busy;return rsp;}
   var old=s.Cabins.FirstOrDefault(x=>x.Type==req.Type&&x.Id==req.CabinId);JObject row;uint id=req.CabinId,grid=req.GridId,target=(old?.Level??0)+1;
   if(req.Type==ArmadaCabinType.ArmadaCabinMain){
    old=s.Cabins.First(x=>x.Type==req.Type);if(req.CabinId!=old.Id){rsp.retcode=LevelUpArmadaCabinRsp.Retcode.CabinNotExist;return rsp;}id=old.Id;grid=old.Grid;target=a.Level+1;row=ArmadaRow(target);
    if(row==null){rsp.retcode=LevelUpArmadaCabinRsp.Retcode.LevelLack;return rsp;}
    if(ArmadaContributionGameplay.Population(a)<(uint)row["requirePopl"]){rsp.retcode=LevelUpArmadaCabinRsp.Retcode.PoplLack;return rsp;}
   }else{
    var names=Names(req.Type);row=Table(names.file).OfType<JObject>().FirstOrDefault(x=>(uint)x["level"]==target);
    if(row==null){rsp.retcode=LevelUpArmadaCabinRsp.Retcode.LevelLack;return rsp;}
    if((uint)row["armadaLevel"]>a.Level){rsp.retcode=LevelUpArmadaCabinRsp.Retcode.ArmadaLevelLack;return rsp;}
    if(req.CabinId!=0&&old==null){rsp.retcode=LevelUpArmadaCabinRsp.Retcode.CabinNotExist;return rsp;}
    if(old!=null)grid=old.Grid;
    else{
     if(s.Cabins.Count(x=>x.Type==req.Type)>=(uint)ArmadaRow(a.Level)[names.cap]){rsp.retcode=LevelUpArmadaCabinRsp.Retcode.CabinFull;return rsp;}
     if(!Grid(a,grid)||s.Cabins.Any(x=>x.Grid==grid)){rsp.retcode=LevelUpArmadaCabinRsp.Retcode.GridError;return rsp;}
     id=s.Cabins.Select(x=>x.Id).DefaultIfEmpty(0u).Max()+1;
    }
   }
   uint cost=(uint)row["costFund"];if(a.Fund<cost){rsp.retcode=LevelUpArmadaCabinRsp.Retcode.FundLack;return rsp;}
   s.PaidFund=cost;s.Pending=new Cabin{Type=req.Type,Id=id,Level=target,Grid=grid};s.End=checked(Now+(uint)row["costTime"]);
   Global.Store.InTransaction(()=>{Module.AddFund(a.Id,-(long)cost);Save(s);});
   rsp.retcode=0;rsp.CabinId=id;rsp.GridId=grid;rsp.LevelUpEndTime=s.End;return rsp;
  }catch(Exception ex){Packet.c.Warn("[ArmadaConstruction] start rolled back: "+ex.Message);return rsp;}
 }}
 internal static MoveArmadaCabinRsp Move(Player p,MoveArmadaCabinReq req){lock(ArmadaModule.SyncGate){
  var r=new MoveArmadaCabinRsp{retcode=MoveArmadaCabinRsp.Retcode.Fail};var a=Module.ByUid(p.User.Uid);if(a==null){r.retcode=MoveArmadaCabinRsp.Retcode.NotInArmada;return r;}if(a.LeaderUid!=p.User.Uid){r.retcode=MoveArmadaCabinRsp.Retcode.NotGranted;return r;}
  try{var s=Load(a);if(s.Owner!=p.User.Uid){r.retcode=MoveArmadaCabinRsp.Retcode.NoOwnership;return r;}
   var grids=req.GridLists.ToArray();r.retcode=MoveArmadaCabinRsp.Retcode.DataError;
   if(grids.Length==0||grids.Select(x=>x.Id).Distinct().Count()!=grids.Length||grids.Any(x=>!Grid(a,x.Id)))return r;
   var occupied=grids.Where(x=>x.Type!=ArmadaCabinType.ArmadaCabinNone).ToArray();
   if(grids.Any(x=>x.Type==ArmadaCabinType.ArmadaCabinNone&&x.CabinId!=0)||occupied.Length!=s.Cabins.Count||occupied.Select(x=>(x.Type,x.CabinId)).Distinct().Count()!=occupied.Length)return r;
   if(occupied.Any(x=>!s.Cabins.Any(c=>c.Type==x.Type&&c.Id==x.CabinId)))return r;
   if(s.Pending!=null&&occupied.Any(x=>x.Type==s.Pending.Type&&x.CabinId==s.Pending.Id&&x.Id!=s.Pending.Grid))return r;
   if(s.Pending!=null&&!s.Cabins.Any(c=>c.Type==s.Pending.Type&&c.Id==s.Pending.Id)&&occupied.Any(x=>x.Id==s.Pending.Grid))return r;
   foreach(var c in s.Cabins)c.Grid=occupied.Single(x=>x.Type==c.Type&&x.CabinId==c.Id).Id;
   Save(s);r.retcode=0;return r;
  }catch(Exception e){r.retcode=MoveArmadaCabinRsp.Retcode.Fail;Packet.c.Warn("[ArmadaConstruction] move: "+e.Message);return r;}
 }}
 internal static CancelLevelUpArmadaCabinRsp Cancel(Player p,CancelLevelUpArmadaCabinReq req){lock(ArmadaModule.SyncGate){
  var r=new CancelLevelUpArmadaCabinRsp{retcode=CancelLevelUpArmadaCabinRsp.Retcode.Fail};var a=Module.ByUid(p.User.Uid);if(a==null){r.retcode=CancelLevelUpArmadaCabinRsp.Retcode.NotInArmada;return r;}if(a.LeaderUid!=p.User.Uid){r.retcode=CancelLevelUpArmadaCabinRsp.Retcode.NotGranted;return r;}
  try{var s=Load(a);if(s.Owner!=p.User.Uid){r.retcode=CancelLevelUpArmadaCabinRsp.Retcode.NoOwnership;return r;}
   if(s.Pending==null||s.Pending.Type!=req.Type||s.Pending.Id!=req.CabinId){r.retcode=CancelLevelUpArmadaCabinRsp.Retcode.CabinNotInLevelup;return r;}
   // Refund only the actual debit retained with this pending build. A legacy
   // pending build has no recorded debit; look up the unchanged local cost.
   uint paid=s.PaidFund??(uint)(s.Pending.Type==ArmadaCabinType.ArmadaCabinMain?ArmadaRow(s.Pending.Level):Table(Names(s.Pending.Type).file).First(x=>(uint)x["level"]==s.Pending.Level))["costFund"];
   if((long)a.Fund+paid>uint.MaxValue)return r;
   Global.Store.InTransaction(()=>{if(paid>0)Module.AddFund(a.Id,paid);s.Pending=null;s.End=0;s.PaidFund=null;Save(s);});r.retcode=0;return r;
  }catch(Exception e){Packet.c.Warn("[ArmadaConstruction] cancel: "+e.Message);return r;}
 }}

 internal static void Fill(Armada a,ArmadaData data){lock(ArmadaModule.SyncGate){
  var s=Load(a);data.Brief.Level=a.Level;
  data.Brief.FundCap=checked((uint)s.Cabins.Where(c=>c.Type==ArmadaCabinType.ArmadaCabinExchequer).Sum(c=>(long)Table("ArmadaExchequerData").First(x=>(uint)x["level"]==c.Level)["fundCap"]));
  data.Brief.PoplCap=checked((uint)s.Cabins.Where(c=>c.Type==ArmadaCabinType.ArmadaCabinContact).Sum(c=>(long)Table("ArmadaContactData").First(x=>(uint)x["level"]==c.Level)["poplCap"]));
  foreach(var row in Table("ArmadaGridData").Take((int)ArmadaRow(a.Level)["grids"])){
   uint grid=(uint)row["gridID"];var c=s.Cabins.FirstOrDefault(x=>x.Grid==grid);data.GridLists.Add(new ArmadaGrid{Id=grid,Type=c?.Type??ArmadaCabinType.ArmadaCabinNone,CabinId=c?.Id??0});
  }
  foreach(var c in s.Cabins)switch(c.Type){
   case ArmadaCabinType.ArmadaCabinContact:data.ContactLists.Add(new ArmadaContact{Id=c.Id,Level=c.Level,MemberLists=a.MemberUids.ToArray()});break;
   case ArmadaCabinType.ArmadaCabinHangar:data.HangarLists.Add(new ArmadaHangar{Id=c.Id,Level=c.Level});break;
   case ArmadaCabinType.ArmadaCabinWorkshop:data.WorkshopLists.Add(new ArmadaWorkshop{Id=c.Id,Level=c.Level});break;
   case ArmadaCabinType.ArmadaCabinExchequer:data.ExchequerLists.Add(new ArmadaExchequer{Id=c.Id,Level=c.Level});break;
   case ArmadaCabinType.ArmadaCabinBoss:data.BossroomLists.Add(new ArmadaBossroom{Id=c.Id,Level=c.Level});break;
  }
  if(s.Pending!=null){data.LevelUpCabinType=s.Pending.Type;data.LevelUpCabinId=s.Pending.Id;data.LevelUpEndTime=s.End;}
 }}
}
