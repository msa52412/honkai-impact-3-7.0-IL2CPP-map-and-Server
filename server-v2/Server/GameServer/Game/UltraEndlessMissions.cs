using System;using System.Linq;using System.Collections.Generic;using Common;using Common.Database;using Common.Resources.Proto;using Common.Utils.ExcelReader;
namespace PemukulPaku.GameServer.Game;
internal static class UltraEndlessMissions
{
 public sealed class State:IRow{public long Id{get;set;}public long CreatedAt{get;set;}public uint Uid{get;set;}public long Period{get;set;}public HashSet<uint> Claimed{get;set;}=new();}
 private static SqlTable<State> Store=>Global.Table<State>("UltraEndlessMissionClaims",("owner_uid","INTEGER",x=>x.Uid,true));
 private static long Period=>UltraEndlessCycle.Enabled?UltraEndlessCycle.Current.Key:UltraEndlessState.EffectiveScheduleId;
 private static State Load(Player p){var s=Store.All().FirstOrDefault(x=>x.Uid==p.User.Uid);return s!=null&&s.Period==Period?s:new State{Id=s?.Id??0,CreatedAt=s?.CreatedAt??0,Uid=p.User.Uid,Period=Period};}
 internal static bool Handles(uint id)=>id is 86002 or 86003 or 86004;
 private static uint Progress(Player p,uint id)
 {
  var scores=UltraEndlessState.Get(p.User.Uid);
  if(id!=86002)return UltraEndlessRank.TotalScore(scores);
  var sites=UltraEndlessSites.Active().Where(x=>x.SiteNodeName=="Area1").ToArray();
  return sites.Any(s=>{var floors=BaseExcelReader<Common.Utils.ExcelReader.UltraEndlessFloor,UltraEndlessFloorExcel>.GetInstance().GetFloorDatasFromStageId(s.StageId);return floors.Count>0&&floors.All(f=>scores.FloorScore((uint)s.SiteId,(uint)f.FloorId)>0);})?1u:0u;
 }
 internal static void Apply(Player p,GetMissionDataRsp rsp)
 {
  lock(p.Equipment){var state=Load(p);foreach(uint id in new uint[]{86002,86003,86004}){var meta=BaseExcelReader<MissionData,MissionDataExcel>.GetInstance().FromId((int)id);uint progress=Math.Min(Progress(p,id),(uint)meta.TotalProgress);rsp.MissionLists.RemoveAll(x=>x.MissionId==id);rsp.MissionLists.Add(new Mission{MissionId=id,Progress=progress,Status=state.Claimed.Contains(id)?MissionStatus.MissionClose:progress>=(uint)meta.TotalProgress?MissionStatus.MissionFinish:MissionStatus.MissionDoing,CycleId=checked((uint)Period),BeginTime=UltraEndlessCycle.Enabled?checked((uint)UltraEndlessCycle.Current.Key):0,EndTime=UltraEndlessCycle.Enabled?checked((uint)UltraEndlessCycle.Current.Close):uint.MaxValue});}}
 }
 internal static GetMissionRewardRsp.Retcode Take(Player p,uint[] ids,out Common.Resources.Proto.RewardData reward,out uint[] granted)
 {
  reward=new();granted=Array.Empty<uint>();lock(p.Equipment){if(ids==null||ids.Length==0||ids.Distinct().Count()!=ids.Length||ids.Any(x=>!Handles(x)))return GetMissionRewardRsp.Retcode.MissionIdError;
   var state=Load(p);var reader=BaseExcelReader<MissionData,MissionDataExcel>.GetInstance();if(ids.Any(id=>state.Claimed.Contains(id)||Progress(p,id)<reader.FromId((int)id).TotalProgress))return GetMissionRewardRsp.Retcode.MissionStatusError;
   DailyGameplay.Plan plan;try{plan=DailyGameplay.Combine(ids.Select(id=>DailyGameplay.Reward(reader.FromId((int)id).RewardId)));DailyGameplay.Validate(p,plan);}catch{return GetMissionRewardRsp.Retcode.EquipmentMaterialLimit;}
   Common.Resources.Proto.RewardData paid=null;var before=state.Claimed.ToHashSet();if(!DailyGameplay.Atomic(p,()=>{paid=DailyGameplay.Grant(p,plan);state.Claimed.UnionWith(ids);if(state.Id==0)Store.InsertOne(state);else Store.ReplaceOne(state);})){state.Claimed=before;return GetMissionRewardRsp.Retcode.Fail;}
   reward=paid;granted=ids;return GetMissionRewardRsp.Retcode.Succ;
  }
 }
}
