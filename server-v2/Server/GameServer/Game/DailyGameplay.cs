using System;
using System.Linq;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using ProtoReward = Common.Resources.Proto.RewardData;
namespace PemukulPaku.GameServer.Game;
internal static class DailyGameplay
{
 internal static Func<long> Clock = () => DateTimeOffset.UtcNow.ToUnixTimeSeconds();
 internal static long Day(long now) => (now + 14400) / 86400;
 internal static long Week(long now) => (Day(now) + 3) / 7;
 internal static uint NextReset => checked((uint)((Day(Clock())+1)*86400-14400));
 // A compatibility catalogue, selected from local data; retired event/meal variants excluded.
 internal static readonly HashSet<int> Catalog = new(){38148,38149,38150,38160,38161,38165,38166,38167,38168,38169,38170,38171,38244,38245,38246,38247,38262,38263,38278};
 internal static MissionDataExcel[] Missions => BaseExcelReader<MissionData,MissionDataExcel>.GetInstance().All.Where(x=>x.Type==3&&Catalog.Contains(x.Id)).ToArray();
 internal static DailyGameplayState State(Player p)
 {
  var s=p.Equipment.DailyGameplay??=new();var day=Day(Clock());var week=Week(Clock());
  if(day>s.Day){s.Day=day;s.Progress.Clear();s.Claimed.Clear();s.DutyClaimed.Clear();s.DailyExp=0;s.ActivityUsed.Clear();s.StageUsed.Clear();s.PendingStage=0;s.PendingSince=0;s.GeneralPendingStage=0;s.GeneralPendingSince=0;}
  if(week>s.Week){s.Week=week;s.WeeklyExp=0;}
  return s;
 }
 internal static bool Atomic(Player p,Action action)
 {
  lock(p.Equipment)
  {
   var e=p.Equipment;var state=JsonConvert.SerializeObject(e.DailyGameplay);var list=e.MaterialList;var counts=list.Select(x=>x.Num).ToArray();
   var weapons=e.WeaponList.Select(x=>(item:x,level:x.Level,exp:x.Exp)).ToArray();
   var stigmata=e.StigmataList.Select(x=>(item:x,level:x.Level,exp:x.Exp)).ToArray();
   var avatars=(p.AvatarList??Array.Empty<AvatarScheme>()).Select(x=>(item:x,level:x.Level,exp:x.Exp,skills:JsonConvert.SerializeObject(x.SkillLists))).ToArray();
   int exp=p.User.Exp,h=p.User.Hcoin,st=p.User.Stamina;
   try{Global.Store.InTransaction(()=>{action();if(st>p.User.Stamina)ArmadaContributionGameplay.Credit(p,population:checked((uint)(st-p.User.Stamina)));e.Save();p.User.Save();});return true;}
   catch(Exception ex){e.DailyGameplay=JsonConvert.DeserializeObject<DailyGameplayState>(state)??new();e.MaterialList=list;for(int i=0;i<list.Length;i++)list[i].Num=counts[i];foreach(var x in weapons){x.item.Level=x.level;x.item.Exp=x.exp;}foreach(var x in stigmata){x.item.Level=x.level;x.item.Exp=x.exp;}
    foreach(var x in avatars){x.item.Level=x.level;x.item.Exp=x.exp;x.item.SkillLists=JsonConvert.DeserializeObject<List<AvatarScheme.AvatarSkill>>(x.skills);}
    p.User.Exp=exp;p.User.Hcoin=h;p.User.Stamina=st;Packet.c.Warn("[DailyGameplay] transaction rolled back: "+ex.Message);return false;}
  }
 }
 internal static bool Ready(Player p,MissionDataExcel m)
 {
  var s=State(p);if(s.Day!=Day(Clock()))return false;
  int hour=DateTimeOffset.FromUnixTimeSeconds(Clock()).ToOffset(TimeSpan.FromHours(8)).Hour;
  if(m.Id==38170)return hour>=11&&hour<14;
  if(m.Id==38171)return hour>=18&&hour<21;
  if(m.Id==38169)return true;
  return s.Progress.GetValueOrDefault(m.Id)>=(uint)Math.Max(1,m.TotalProgress);
 }
 internal static IEnumerable<Mission> Build(Player p)
 {
  lock(p.Equipment)
  {
   var s=State(p);return Missions.Select(m=>new Mission{MissionId=(uint)m.Id,
    Status=s.Claimed.Contains((uint)m.Id)?MissionStatus.MissionClose:Ready(p,m)?MissionStatus.MissionFinish:MissionStatus.MissionDoing,
    Progress=(m.Id==38169||m.Id==38170||m.Id==38171)?(Ready(p,m)?1u:0u):s.Progress.GetValueOrDefault(m.Id),
    Priority=(uint)m.Priority,BeginTime=checked((uint)(s.Day*86400-14400)),EndTime=checked((uint)((s.Day+1)*86400-14400)),CycleId=(uint)s.Day}).ToArray();
  }
 }
 internal static void Add(Player p,int way,int para,uint amount=1)
 {
  if(!File.Exists("Resources/ExcelOutputAsset/MissionData.json"))return;
  var s=State(p);if(s.Day!=Day(Clock())||amount==0)return;
  foreach(var m in Missions.Where(x=>x.FinishWay==way&&(x.FinishParaInt==para||x.FinishParaInt==0)))
   s.Progress[m.Id]=(uint)Math.Min((ulong)Math.Max(1,m.TotalProgress),(ulong)s.Progress.GetValueOrDefault(m.Id)+amount);
 }
 internal static bool Record(Player p,int way,int para,uint amount=1)=>Atomic(p,()=>Add(p,way,para,amount));
 internal sealed class Plan
 {
  internal long Exp,Hcoin,Stamina,Bp,Contribution;
  internal Dictionary<int,long> Items=new();
  internal ProtoReward Proto()=>new(){Exp=checked((uint)Exp),Hcoin=checked((uint)Hcoin),Stamina=checked((uint)Stamina)};
 }
 internal static Plan Reward(int id)
 {
  var c=BaseExcelReader<Common.Utils.ExcelReader.RewardData,RewardDataExcel>.GetInstance().FromId(id)??throw new InvalidOperationException("Missing reward "+id);
  if(c.RewardExp<0||c.RewardHCoin<0||c.RewardStamina<0||c.RewardFriendPoint!=0)throw new InvalidOperationException("Unsupported reward");
  var p=new Plan{Exp=c.RewardExp,Hcoin=c.RewardHCoin,Stamina=c.RewardStamina,Contribution=c.RewardDutyPoint};
  int[] ids={c.RewardItem1Id,c.RewardItem2Id,c.RewardItem3Id,c.RewardItem4Id,c.RewardItem5Id,c.RewardItem6Id};
  int[] ns={c.RewardItem1Num,c.RewardItem2Num,c.RewardItem3Num,c.RewardItem4Num,c.RewardItem5Num,c.RewardItem6Num};
  for(int i=0;i<ids.Length;i++){if(ids[i]==0)continue;if(ns[i]<=0)throw new InvalidOperationException("Invalid reward quantity");if(ids[i]==80011)p.Contribution+=ns[i];else if(ids[i]==80016)p.Bp+=ns[i];else p.Items[ids[i]]=p.Items.GetValueOrDefault(ids[i])+ns[i];}
  if(p.Exp+p.Hcoin+p.Stamina+p.Bp+p.Contribution+p.Items.Values.Sum()==0)throw new InvalidOperationException("Empty reward "+id);
  return p;
 }
 internal static Plan Combine(IEnumerable<Plan> plans)
 {
  var p=new Plan();foreach(var x in plans){p.Exp+=x.Exp;p.Hcoin+=x.Hcoin;p.Stamina+=x.Stamina;p.Bp+=x.Bp;p.Contribution+=x.Contribution;foreach(var i in x.Items)p.Items[i.Key]=p.Items.GetValueOrDefault(i.Key)+i.Value;}return p;
 }
 internal static void Validate(Player p,Plan plan)
 {
  Post62Materials.Normalize(plan.Items);
  if(plan.Contribution<0||plan.Contribution>uint.MaxValue||plan.Exp<0||plan.Hcoin<0||plan.Stamina<0||plan.Bp<0||plan.Exp+p.User.Exp>int.MaxValue||plan.Hcoin+p.User.Hcoin>int.MaxValue||plan.Stamina+p.User.Stamina>int.MaxValue)throw new InvalidOperationException("Currency limit");
  foreach(var x in plan.Items)
  {
   var c=BaseExcelReader<MaterialData,MaterialDataExcel>.GetInstance().FromId((uint)x.Key)??throw new InvalidOperationException("Unsupported material "+x.Key);
   long held=p.Equipment.MaterialList.Where(y=>y.Id==x.Key).Sum(y=>(long)y.Num);
   long cap=c.QuantityLimit>0?Math.Min((long)c.QuantityLimit,int.MaxValue):int.MaxValue;
   if(x.Value<=0||held+x.Value>cap)throw new InvalidOperationException("Material limit "+x.Key);
  }
 }
 internal static ProtoReward Grant(Player p,Plan plan)
 {
  Validate(p,plan);var r=plan.Proto();p.User.AddExp((int)plan.Exp);p.User.Hcoin+=(int)plan.Hcoin;p.User.Stamina+=(int)plan.Stamina;
  foreach(var x in plan.Items){p.Equipment.AddMaterial(x.Key,(int)x.Value);if(x.Key==100)r.Scoin=checked((uint)x.Value);else r.ItemLists.Add(new RewardItemData{Id=(uint)x.Key,Num=(uint)x.Value});}
  if(plan.Contribution>0){ArmadaContributionGameplay.Credit(p,contribution:checked((uint)plan.Contribution));}
  if(plan.Bp>0){var s=State(p);uint amount=checked((uint)plan.Bp);s.DailyExp=checked(s.DailyExp+amount);uint credited=Math.Min(amount,10000-Math.Min(10000,s.WeeklyExp));s.WeeklyExp+=credited;s.TotalBattlePassExp+=credited;if(credited>0)r.ItemLists.Add(new RewardItemData{Id=80016,Num=credited});}
  return r;
 }
 internal static GetMissionRewardRsp.Retcode Take(Player p,uint[] ids,out ProtoReward reward,out uint[] granted)
 {
  reward=new();granted=Array.Empty<uint>();lock(p.Equipment)
  {
   if(ids==null||ids.Length==0||ids.Length>100||ids.Distinct().Count()!=ids.Length)return GetMissionRewardRsp.Retcode.MissionIdError;
   var ms=Missions.ToDictionary(x=>(uint)x.Id);var s=State(p);
   if(ids.Any(x=>!ms.ContainsKey(x)))return GetMissionRewardRsp.Retcode.MissionIdError;
   if(ids.Any(x=>s.Claimed.Contains(x)||!Ready(p,ms[x])))return GetMissionRewardRsp.Retcode.MissionStatusError;
   Plan plan;try{plan=Combine(ids.Select(x=>Reward(ms[x].RewardId)));Validate(p,plan);}catch(Exception ex){Packet.c.Warn("[Daily] reward validation: "+ex.Message);return GetMissionRewardRsp.Retcode.EquipmentMaterialLimit;}
   ProtoReward result=null;
   if(!Atomic(p,()=>{result=Grant(p,plan);foreach(var x in ids)State(p).Claimed.Add(x);}))return GetMissionRewardRsp.Retcode.Fail;
   reward=result;granted=ids.ToArray();return GetMissionRewardRsp.Retcode.Succ;
  }
 }
 internal static TakeDutyRewardRsp TakeDuty(Player p,TakeDutyRewardReq req)
 {
  var rsp=new TakeDutyRewardRsp{DutyType=req.DutyType,retcode=TakeDutyRewardRsp.Retcode.Fail};lock(p.Equipment)
  {
   if(State(p).Day!=Day(Clock())||req.DutyType!=DutyType.DutyDaily||req.DutyIdLists==null||req.DutyIdLists.Length==0||req.DutyIdLists.Length>5||req.DutyIdLists.Distinct().Count()!=req.DutyIdLists.Length)return rsp;
   var s=State(p);var rows=JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/DutyDailyData.json")).ToDictionary(x=>(uint)x["dutyId"]);
   if(req.DutyIdLists.Any(x=>!rows.ContainsKey(x)))return rsp;
   // The client can submit the cumulative list of eligible boxes, including earlier receipts.
   var requested=req.DutyIdLists.Where(x=>!s.DutyClaimed.Contains(x)).ToArray();
   if(requested.Length==0){rsp.retcode=TakeDutyRewardRsp.Retcode.HasTake;return rsp;}
   req.DutyIdLists=requested;
   var level=BaseExcelReader<PlayerLevelData,PlayerLevelDataExcel>.GetInstance().CalculateLevel(p.User.Exp).Level;
   if(req.DutyIdLists.Any(x=>level<(int)rows[x]["unlockLv"])){rsp.retcode=TakeDutyRewardRsp.Retcode.LevelLack;return rsp;}
   if(req.DutyIdLists.Any(x=>s.DailyExp<(uint)rows[x]["needDuty"])){rsp.retcode=TakeDutyRewardRsp.Retcode.DutyPointLack;return rsp;}
   try
   {
    var plans=req.DutyIdLists.Select(x=>Reward((int)rows[x]["rewardId"])).ToArray();Validate(p,Combine(plans));
    if(!Atomic(p,()=>{foreach(var plan in plans)rsp.RewardLists.Add(Grant(p,plan));foreach(var id in req.DutyIdLists)State(p).DutyClaimed.Add(id);})){rsp.RewardLists.Clear();return rsp;}
    rsp.retcode=TakeDutyRewardRsp.Retcode.Succ;rsp.DutyIdLists=req.DutyIdLists.ToArray();return rsp;
   }catch(Exception ex){rsp.RewardLists.Clear();Packet.c.Warn("[Daily] duty claim: "+ex.Message);return rsp;}
  }
 }
 internal static GetRecommendMissionPanelListRsp Panels(Player p)
 {
  var rsp=new GetRecommendMissionPanelListRsp{retcode=GetRecommendMissionPanelListRsp.Retcode.Succ};
  if(!File.Exists("Resources/ExcelOutputAsset/RecommendPanel.json"))return rsp;
  var supported=Missions.Select(x=>x.Id).ToHashSet();
  uint begin=checked((uint)(State(p).Day*86400-14400));
  foreach(var row in JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/RecommendPanel.json")).OfType<JObject>().OrderBy(x=>(int)x["SortID"]))
  {
   var ids=((JArray)row["HangMissionList"]).Select(x=>(int)x).ToArray();
   // Do not advertise retired/event tasks absent from the supported ledger.
   if(ids.Length==0 || ids.Any(id=>!supported.Contains(id)))continue;
   rsp.RecommendMissionPanelLists.Add(new RecommendMissionPanel{PanelId=(uint)row["PanelID"],IsPanelShow=true,MissionBeginTime=begin});
  }
  return rsp;
 }
 internal static void PushDuty(Session s)
 {
  lock(s.Player.Equipment){var d=State(s.Player);s.Send(Packet.FromProto(new SyncDutyNotify{DailyDutyPoint=d.DailyExp,WeeklyDutyPoint=d.WeeklyExp,HasTakeDailyDutyIdLists=d.DutyClaimed.ToArray(),HasTakeWeeklyDutyIdLists=Array.Empty<uint>()},CmdId.SyncDutyNotify));}
 }
 internal static void PushInventory(Session s)
 {
  var p=s.Player;var l=BaseExcelReader<PlayerLevelData,PlayerLevelDataExcel>.GetInstance().CalculateLevel(p.User.Exp);
  s.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(),CmdId.GetEquipmentDataReq));
  s.Send(Packet.FromProto(new GetMainDataRsp{retcode=GetMainDataRsp.Retcode.Succ,Level=(uint)l.Level,Exp=(uint)l.Exp,Hcoin=(uint)p.User.Hcoin,FreeHcoin=(uint)p.User.Hcoin,Stamina=(uint)p.User.Stamina,Scoin=(uint)p.Equipment.MaterialList.Where(x=>x.Id==100).Sum(x=>(long)x.Num)},CmdId.GetMainDataRsp));
 }
}

