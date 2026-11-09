using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json.Linq;
using PemukulPaku.GameServer.MPModule;
namespace PemukulPaku.GameServer.Game;
// Material costs/credits use OrderItemData. The available export lacks the
// server's weighted reward distribution. We use the local 910001 basic reward
// per submission, reserved to its contributor and claimable after 20:00.
// This is an explicit local compatibility policy, not the original random pool.
internal static class ArmadaConsignedGameplay
{
 public sealed class Chain{public uint Unique{get;set;}public uint Uid{get;set;}public uint Progress{get;set;}public uint[] Items{get;set;}=Array.Empty<uint>();}
 public sealed class Board:IRow{public long Id{get;set;}public long CreatedAt{get;set;}public uint Armada{get;set;}public long Period{get;set;}=-1;public uint NextId{get;set;}=1;public List<Chain> Chains{get;set;}=new();}
 public sealed class Receipt{public long Period{get;set;}public uint Count{get;set;}public uint Credit{get;set;}public bool Taken{get;set;}public uint[] Optional{get;set;}=Array.Empty<uint>();}
 public sealed class Member:IRow{public long Id{get;set;}public long CreatedAt{get;set;}public string Key{get;set;}public uint Armada{get;set;}public uint Uid{get;set;}public long Period{get;set;}=-1;public uint Finish{get;set;}public uint Apply{get;set;}public List<Receipt> Receipts{get;set;}=new();}
 private static SqlTable<Board> Boards=>Global.Table<Board>("ArmadaConsignedBoards",("armada_id","INTEGER",x=>x.Armada,true));
 private static SqlTable<Member> Members=>Global.Table<Member>("ArmadaConsignedMembers",("member_key","TEXT",x=>x.Key,true));
 // The daily consigned batch ends at 20:00 UTC+8, independently of 04:00 tasks.
 private static long Period=>(DailyGameplay.Clock()-43200)/86400;
 private static JObject[] Items=>JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/OrderItemData.json")).OfType<JObject>().Where(x=>new[]{3000,3005,3006,3007,3008}.Contains((int)x["itemId"])&&(int)x["itemNum"]>0).ToArray();
 private static Board LoadBoard(uint id){var b=Boards.All().FirstOrDefault(x=>x.Armada==id)??new Board{Armada=id};if(b.Period!=Period){b.Period=Period;b.Chains.Clear();}return b;}
 private static Member LoadMember(uint id,uint uid){var s=Members.All().FirstOrDefault(x=>x.Key==id+":"+uid)??new Member{Key=id+":"+uid,Armada=id,Uid=uid};if(s.Period!=Period){s.Period=Period;s.Finish=s.Apply=0;}return s;}
 private static void Save(Board b){if(b.Id==0)Boards.InsertOne(b);else Boards.ReplaceOne(b);}
 private static void Save(Member b){if(b.Id==0)Members.InsertOne(b);else Members.ReplaceOne(b);}
 private static uint Limit(Armada a,uint uid)=>a.LeaderUid==uid?20u:1u;
 private static JObject Row(uint id)=>Items.FirstOrDefault(x=>(uint)x["orderId"]==id)??throw new InvalidOperationException("Unsupported order");
 private static DailyGameplay.Plan Basic()=>DailyGameplay.Reward(910001);
 private static Common.Resources.Proto.RewardData Display(){var plan=Basic();var r=plan.Proto();foreach(var x in plan.Items)r.ItemLists.Add(new RewardItemData{Id=(uint)x.Key,Num=(uint)x.Value});return r;}
 private static ConsignedOrder Proto(Chain c){var row=Row(c.Items[c.Progress]);var o=new ConsignedOrder{Id=(uint)row["orderId"],UniqueId=c.Unique,ConsignerUid=c.Uid,Progress=c.Progress+1,OrderLevel=c.Progress+1,RewardId=910001,OrderCredit=(uint)row["orderCredits"]};o.RewardLists.Add(Display());return o;}
 private static uint[] Candidates=>Items.GroupBy(x=>(int)x["itemId"]).Select(g=>(uint)g.OrderBy(x=>(int)x["itemNum"]).First()["orderId"]).Take(3).ToArray();
 internal static GetConsignedOrderDataRsp Build(Player p){lock(ArmadaModule.SyncGate){
  var a=ArmadaModule.GetInstance().ByUid(p.User.Uid);var r=new GetConsignedOrderDataRsp{retcode=GetConsignedOrderDataRsp.Retcode.NotInArmada};if(a==null)return r;var b=LoadBoard(a.Id);var s=LoadMember(a.Id,p.User.Uid);r.retcode=0;r.FinishOrderTimes=s.Finish;r.TotalOrderTimes=8;r.ApplyOrderTimes=s.Apply;r.TotalApplyTimes=Limit(a,p.User.Uid);r.ArmadaFinishTimes=(uint)Members.All().Where(x=>x.Armada==a.Id&&x.Period==Period).Sum(x=>(long)x.Finish);r.MyCredit=s.Receipts.Where(x=>x.Period==Period).Select(x=>x.Credit).FirstOrDefault();r.IsRewardTaken=!s.Receipts.Any(x=>x.Period<Period&&!x.Taken);r.OrderLists.AddRange(b.Chains.Where(x=>x.Progress<8).Select(Proto));r.Progress=r.OrderLists.FirstOrDefault(x=>x.ConsignerUid==p.User.Uid)?.Progress??0;return r;
 }}
 internal static GetNewConsignedOrderDataRsp New(Player p){lock(ArmadaModule.SyncGate){
  var r=new GetNewConsignedOrderDataRsp{retcode=GetNewConsignedOrderDataRsp.Retcode.NotInArmada};var a=ArmadaModule.GetInstance().ByUid(p.User.Uid);if(a==null)return r;var s=LoadMember(a.Id,p.User.Uid);r.retcode=0;r.TotalOrderTimes=8;r.FinishOrderTimes=s.Finish;
  if(s.Apply<Limit(a,p.User.Uid))foreach(uint id in Candidates)r.OrderLists.Add(new ConsignedOrder{Id=id,Progress=1,OrderLevel=1,ConsignerUid=p.User.Uid,RewardId=910001,OrderCredit=(uint)Row(id)["orderCredits"]});return r;
 }}
 internal static ChooseConsignedOrderRsp Choose(Player p,ChooseConsignedOrderReq req){lock(ArmadaModule.SyncGate){
  var r=new ChooseConsignedOrderRsp{retcode=ChooseConsignedOrderRsp.Retcode.NotInArmada};var a=ArmadaModule.GetInstance().ByUid(p.User.Uid);if(a==null)return r;var b=LoadBoard(a.Id);var s=LoadMember(a.Id,p.User.Uid);
  if(s.Apply>=Limit(a,p.User.Uid)){r.retcode=ChooseConsignedOrderRsp.Retcode.TimesLimit;return r;}
  if(b.Chains.Any(x=>x.Uid==s.Uid&&x.Progress<8)){r.retcode=ChooseConsignedOrderRsp.Retcode.StatusError;return r;}
  var candidates=Candidates;if(req.Index>=candidates.Length){r.retcode=ChooseConsignedOrderRsp.Retcode.IndexError;return r;}
  uint choice=candidates[req.Index];if(req.ChoosenOrder!=null&&req.ChoosenOrder.Id!=choice){r.retcode=ChooseConsignedOrderRsp.Retcode.IndexError;return r;}
  var c=new Chain{Unique=b.NextId++,Uid=s.Uid,Items=Enumerable.Repeat(choice,8).ToArray()};b.Chains.Add(c);s.Apply++;
  try{Global.Store.InTransaction(()=>{Save(b);Save(s);});r.ChoosenOrder=Proto(c);r.retcode=0;}catch(Exception ex){r.retcode=ChooseConsignedOrderRsp.Retcode.Fail;Packet.c.Warn("[ArmadaConsigned] choose: "+ex.Message);}return r;
 }}
 internal static FinishConsignedOrderRsp Finish(Player p,FinishConsignedOrderReq req){lock(p.Equipment)lock(ArmadaModule.SyncGate){
  var r=new FinishConsignedOrderRsp{retcode=FinishConsignedOrderRsp.Retcode.Fail,ReliableId=req.ReliableId};var a=ArmadaModule.GetInstance().ByUid(p.User.Uid);if(a==null){r.retcode=FinishConsignedOrderRsp.Retcode.NotInArmada;return r;}
  var b=LoadBoard(a.Id);var s=LoadMember(a.Id,p.User.Uid);var c=b.Chains.FirstOrDefault(x=>x.Unique==req.UniqueId);if(c==null){r.retcode=FinishConsignedOrderRsp.Retcode.StatusError;return r;}if(c.Progress>=8){r.retcode=FinishConsignedOrderRsp.Retcode.OrderFinished;return r;}
  var o=Proto(c);if(req.ConsignId!=o.Id||req.Progress!=o.Progress||req.OrderLevel!=o.OrderLevel){r.retcode=FinishConsignedOrderRsp.Retcode.OrderProgressChange;return r;}
  if(s.Finish>=8){r.retcode=FinishConsignedOrderRsp.Retcode.TimesLack;return r;}var row=Row(o.Id);int id=(int)row["itemId"],num=(int)row["itemNum"];
  if(req.Item==null||req.Item.Type!=EquipmentType.EquipmentMaterial||req.Item.IdOrUniqueId!=id||req.Item.Num!=(uint)num){r.retcode=FinishConsignedOrderRsp.Retcode.ConsumeItemNotExist;return r;}
  if(p.Equipment.MaterialList.Where(x=>x.Id==id).Sum(x=>(long)x.Num)<num){r.retcode=FinishConsignedOrderRsp.Retcode.ItemLack;return r;}
  try{var plan=Basic();long fund=plan.Items.GetValueOrDefault(80001);if(fund<0||fund+a.Fund>uint.MaxValue)return r;
   if(!DailyGameplay.Atomic(p,()=>{p.Equipment.AddMaterial(id,-num);c.Progress++;s.Finish++;var receipt=s.Receipts.FirstOrDefault(x=>x.Period==Period);if(receipt==null){receipt=new Receipt{Period=Period};s.Receipts.Add(receipt);}receipt.Count++;receipt.Credit=checked(receipt.Credit+o.OrderCredit);if(fund>0){ArmadaModule.GetInstance().AddFund(a.Id,fund);ArmadaContributionGameplay.Credit(p,fund:checked((uint)fund));}DailyGameplay.Add(p,86,0);Save(b);Save(s);})){return r;}
   r.retcode=0;r.Item=new EquipmentItem{Type=EquipmentType.EquipmentMaterial,IdOrUniqueId=(uint)id,Num=(uint)num};r.ConsignId=o.Id;r.Progress=c.Progress;r.FinishOrderTimes=s.Finish;return r;
  }catch(Exception ex){Packet.c.Warn("[ArmadaConsigned] finish rejected: "+ex.Message);return r;}
 }}
 private static DailyGameplay.Plan Pending(Member s){var plan=Basic();plan.Items.Remove(80001);var count=s.Receipts.Where(x=>x.Period<Period&&!x.Taken).Sum(x=>(long)x.Count);plan.Exp*=count;plan.Hcoin*=count;plan.Stamina*=count;plan.Bp*=count;plan.Contribution*=count;foreach(var id in plan.Items.Keys.ToArray())plan.Items[id]*=count;return plan;}
 internal static GetConsignedRewardRsp Rewards(Player p){lock(ArmadaModule.SyncGate){var r=new GetConsignedRewardRsp{retcode=GetConsignedRewardRsp.Retcode.NotInArmada};var a=ArmadaModule.GetInstance().ByUid(p.User.Uid);if(a==null)return r;var s=LoadMember(a.Id,p.User.Uid);r.retcode=0;r.MyCredit=checked((uint)s.Receipts.Where(x=>x.Period<Period&&!x.Taken).Sum(x=>(long)x.Credit));r.TotalCredit=checked((uint)Members.All().Where(x=>x.Armada==a.Id).SelectMany(x=>x.Receipts).Where(x=>x.Period<Period&&!x.Taken).Sum(x=>(long)x.Credit));if(r.MyCredit>0){var plan=Pending(s);var display=plan.Proto();foreach(var x in plan.Items)display.ItemLists.Add(new RewardItemData{Id=(uint)x.Key,Num=(uint)x.Value});r.RequiredRewardLists.Add(display);}r.MyOptionalRewardLists=Array.Empty<uint>();return r;}}
 internal static TakeConsignedRewardRsp Take(Player p,TakeConsignedRewardReq req){lock(p.Equipment)lock(ArmadaModule.SyncGate){var r=new TakeConsignedRewardRsp{retcode=TakeConsignedRewardRsp.Retcode.Fail,ReliableId=req.ReliableId};var a=ArmadaModule.GetInstance().ByUid(p.User.Uid);if(a==null){r.retcode=TakeConsignedRewardRsp.Retcode.NotInArmada;return r;}var s=LoadMember(a.Id,p.User.Uid);if(!s.Receipts.Any(x=>x.Period<Period&&!x.Taken)){r.retcode=TakeConsignedRewardRsp.Retcode.RewardTaken;return r;}try{var plan=Pending(s);DailyGameplay.Validate(p,plan);if(!DailyGameplay.Atomic(p,()=>{r.RewardLists.Add(DailyGameplay.Grant(p,plan));foreach(var x in s.Receipts.Where(x=>x.Period<Period))x.Taken=true;Save(s);})){r.RewardLists.Clear();return r;}r.retcode=0;return r;}catch(Exception e){r.RewardLists.Clear();Packet.c.Warn("[ArmadaConsigned] reward: "+e.Message);return r;}}}
 internal static GetConsignedRewardLogRsp Log(Player p){lock(ArmadaModule.SyncGate){var r=new GetConsignedRewardLogRsp{retcode=GetConsignedRewardLogRsp.Retcode.NotInArmada};var a=ArmadaModule.GetInstance().ByUid(p.User.Uid);if(a==null)return r;r.retcode=0;foreach(var s in Members.All().Where(x=>x.Armada==a.Id))foreach(var x in s.Receipts.Where(x=>x.Taken).OrderByDescending(x=>x.Period).Take(7)){var line=new ConsignedRewardLog{Uid=s.Uid,Credit=x.Credit};var plan=Basic();plan.Items.Remove(80001);var display=plan.Proto();foreach(var item in plan.Items)display.ItemLists.Add(new RewardItemData{Id=(uint)item.Key,Num=checked((uint)(item.Value*x.Count))});line.RewardLists.Add(display);r.RewardLogLists.Add(line);}return r;}}
 internal static ChooseConsignedRewardRsp ChooseReward(Player p,ChooseConsignedRewardReq req){var a=ArmadaModule.GetInstance().ByUid(p.User.Uid);return new(){retcode=a==null?ChooseConsignedRewardRsp.Retcode.NotInArmada:(req.OptionalRewardLists??Array.Empty<uint>()).Length==0?0:ChooseConsignedRewardRsp.Retcode.ItemNotExist,OptionalRewardLists=Array.Empty<uint>()};}
 internal static ResetConsignedOrderRsp Reset(Player p){lock(ArmadaModule.SyncGate){var r=new ResetConsignedOrderRsp{retcode=ResetConsignedOrderRsp.Retcode.NotInArmada};var a=ArmadaModule.GetInstance().ByUid(p.User.Uid);if(a==null)return r;try{var b=LoadBoard(a.Id);b.Chains.RemoveAll(x=>x.Uid==p.User.Uid&&x.Progress<8);Save(b);r.retcode=0;}catch(Exception ex){r.retcode=ResetConsignedOrderRsp.Retcode.Fail;Packet.c.Warn("[ArmadaConsigned] reset: "+ex.Message);}return r;}}
}
