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
internal static class AbyssSettlementRewards
{
 public sealed class Receipt:IRow
 {
  public long Id{get;set;} public long CreatedAt{get;set;}
  public string Key{get;set;} public uint Uid{get;set;} public long Period{get;set;}
  public long Due{get;set;} public int RewardId{get;set;} public bool Paid{get;set;}
  public string Info{get;set;}
  public string NoticeReward{get;set;} public bool NoticeSent{get;set;}
 }
 private static SqlTable<Receipt> Store=>Global.Table<Receipt>("AbyssSettlementReceipts",("key","TEXT",s=>s.Key,true));
 internal static UltraEndlessSettleInfo Frozen(uint uid,long period)
 {
  var r=Store.FirstOrDefault(x=>x.Key==uid+":"+period);
  return r==null?null:JsonConvert.DeserializeObject<UltraEndlessSettleInfo>(r.Info);
 }
 internal static void Queue(uint uid,long period,long due,UltraEndlessSettleInfo info,bool participated)
 {
  if(Store.FirstOrDefault(x=>x.Key==uid+":"+period)!=null)return;
  int reward=0;
  if(participated)
  {
   uint group=UltraEndlessCycle.GroupForCups(info.CupNum);
   var row=RewardRow(group,info.ScheduleId)
    ??throw new InvalidDataException("Missing abyss settlement reward for current schedule/group");
   reward=(int)row["SettleReward"];
  }
  Store.InsertOne(new Receipt{Key=uid+":"+period,Uid=uid,Period=period,Due=due,RewardId=reward,Paid=!participated,Info=JsonConvert.SerializeObject(info)});
 }
 // Reward economy follows the selected map schedule, including historical cycles.
 internal static JToken RewardRow(uint group,uint schedule)
 {
  var rows=JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/UltraEndlessReward.json"));
  return rows.SingleOrDefault(x=>(uint)x["GroupLevel"]==group&&(uint)x["BeginScheduleId"]<=schedule&&(uint)x["EndScheduleId"]>=schedule);
 }
 internal static List<UltraEndlessLastSettleRewardNotify> Pay(Player p)
 {
  var notices=new List<UltraEndlessLastSettleRewardNotify>();
  lock(p.Equipment)
  {
   CompetitiveMedalService.ReconcileAbyssDurations(p.User.Uid);
   foreach(var r in Store.All().Where(x=>x.Uid==p.User.Uid&&!x.Paid&&x.Due<=UltraEndlessCycle.Clock().ToUnixTimeSeconds()).OrderBy(x=>x.Period))
   {
    try
    {
     var cfg=BaseExcelReader<Common.Utils.ExcelReader.RewardData,RewardDataExcel>.GetInstance().FromId(r.RewardId)
      ??throw new InvalidDataException("Missing abyss reward "+r.RewardId);
     var plan=DailyGameplay.Reward(r.RewardId);
     var medals=plan.Items.Where(x=>CompetitiveMedalService.Controlled((uint)x.Key)).ToArray();
     foreach(var medal in medals)plan.Items.Remove(medal.Key);
     DailyGameplay.Validate(p,plan);CompetitiveMedalService.Prepare();
     Common.Resources.Proto.RewardData granted=null;
     if(!DailyGameplay.Atomic(p,()=>{
      granted=DailyGameplay.Grant(p,plan);
      CompetitiveMedalService.GrantReward(p.User.Uid,r.RewardId,"ultra:"+r.Period,r.Due);
      foreach(var m in medals)granted.ItemLists.Add(new RewardItemData{Id=(uint)m.Key,Num=checked((uint)m.Value)});
      r.NoticeReward=JsonConvert.SerializeObject(granted);
      r.Paid=true;Store.ReplaceOne(r);
     })) {r.Paid=false;continue;}
     notices.Add(new(){SettleInfo=JsonConvert.DeserializeObject<UltraEndlessSettleInfo>(r.Info),SettleRewardData=granted});
    }
    catch(Exception ex){Packet.c.Warn("[Ultra] reward remains pending: "+ex.Message);}
   }
  }
  return notices;
 }
 internal static bool Collect(Session s)
 {
  if(!UltraEndlessCycle.Enabled||PlayerLevelData.GetInstance().CalculateLevel(s.Player.User.Exp).Level<81)return false;
  _=UltraEndlessState.Get(s.Player.User.Uid);
  return Pay(s.Player).Count>0;
 }

 internal static void Deliver(Player p,Func<UltraEndlessLastSettleRewardNotify,bool> send)
 {
  lock(p.Equipment)
  {
   var receipts=Store.All().Where(x=>x.Uid==p.User.Uid&&x.Paid&&x.RewardId>0).OrderBy(x=>x.Period).ToArray();
   var latest=receipts.LastOrDefault();
   // Older saves recorded payment but did not track delivery. Recover the latest result only.
   foreach(var receipt in receipts.Where(x=>!x.NoticeSent&&(x.NoticeReward!=null||x==latest)))
   {
    var reward=receipt.NoticeReward==null?LegacyNoticeReward(receipt.RewardId):
      JsonConvert.DeserializeObject<Common.Resources.Proto.RewardData>(receipt.NoticeReward);
    var notice=new UltraEndlessLastSettleRewardNotify{
      SettleInfo=JsonConvert.DeserializeObject<UltraEndlessSettleInfo>(receipt.Info),SettleRewardData=reward};
    if(!send(notice))return;
    receipt.NoticeSent=true;
    Store.ReplaceOne(receipt);
   }
  }
 }

 private static Common.Resources.Proto.RewardData LegacyNoticeReward(int rewardId)
 {
  var plan=DailyGameplay.Reward(rewardId);
  var reward=plan.Proto();
  foreach(var item in plan.Items)
  {
   if(item.Key==100)reward.Scoin=checked((uint)item.Value);
   else reward.ItemLists.Add(new RewardItemData{Id=(uint)item.Key,Num=checked((uint)item.Value)});
  }
  return reward;
 }

 internal static void Notify(Session s)
 {
  if(!s.AbyssBridgeReady||!UltraEndlessCycle.Enabled||
     PlayerLevelData.GetInstance().CalculateLevel(s.Player.User.Exp).Level<81)return;
  if(Collect(s))AdventureService.NotifyInventory(s);
  Deliver(s.Player,notice=>s.TrySend(Packet.FromProto(notice,CmdId.UltraEndlessLastSettleRewardNotify)));
 }
}
