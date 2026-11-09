using System;
using System.IO;
using System.Linq;
using Common.Resources.Proto;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
// Local catalogue compatibility: bundled BP data ends at 6.6, not an authenticated 7.0 season.
internal static class BattlePassGameplay
{
 internal static JObject Schedule => JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/BattlePassSchedule.json")).OfType<JObject>().OrderBy(x=>(int)x["SeasonScheduleID"]).Last();
 internal static JObject[] Levels(JObject schedule)=>JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/BattlePassSeasonLevelConfig.json")).OfType<JObject>().Where(x=>(int)x["SeasonLevelComfigID"]==(int)schedule["SeasonLevelConfigID"]).OrderBy(x=>(int)x["SeasonLevel"]).ToArray();
 internal static GetBattlePassRsp Build(Player p)
 {
  lock(p.Equipment)
  {
   if(!File.Exists("Resources/ExcelOutputAsset/BattlePassSchedule.json"))return new(){retcode=GetBattlePassRsp.Retcode.NotOpen};
   var row=Schedule;var levels=Levels(row);if(levels.Length==0)throw new InvalidOperationException("No BP levels");
   var s=DailyGameplay.State(p);ulong exp=s.TotalBattlePassExp;uint level=(uint)levels[0]["SeasonLevel"];
   foreach(var x in levels){uint cost=(uint)x["NextLevelExp"];if(cost==0 || exp<cost || level>=(uint)levels[^1]["SeasonLevel"])break;exp-=cost;level++;}
   uint end=checked((uint)(((DailyGameplay.Week(DailyGameplay.Clock())+1)*7-3)*86400-14400));
   var response=new GetBattlePassRsp{retcode=GetBattlePassRsp.Retcode.Succ,ScheduleId=(uint)row["SeasonScheduleID"],Level=level,Exp=checked((uint)Math.Min(exp,uint.MaxValue)),HasTakeRewardLevel=s.BattlePassRewardLevel,HasGotTicketLists=new[]{(uint)row["BasicTypeID"]},PhaseMaxExp=10000,PhaseExp=s.WeeklyExp,IsTakePhaseFreeExp=true,PhaseEndTime=end};
   if(File.Exists("Resources/ExcelOutputAsset/BattlePassChallengeData.json"))
    foreach(var m in JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/BattlePassChallengeData.json")).OfType<JObject>())
     response.BattlePassMissionLists.Add(new BattlePassMission{MissionId=(uint)m["MissionID"],MissionFinishTimes=0});
   return response;
  }
 }
 internal static void Push(Session s){var rsp=Build(s.Player);if(rsp.retcode==0)s.Send(Packet.FromProto(rsp,CmdId.GetBattlePassRsp));}
 internal static TakeBattlePassLevelRewardRsp Take(Player p)
 {
  var rsp=new TakeBattlePassLevelRewardRsp{retcode=TakeBattlePassLevelRewardRsp.Retcode.Fail};
  lock(p.Equipment)
  {
   var bp=Build(p);if(bp.retcode!=0){rsp.retcode=TakeBattlePassLevelRewardRsp.Retcode.NotOpen;return rsp;}
   var state=DailyGameplay.State(p);if(state.BattlePassRewardLevel>=bp.Level){rsp.retcode=TakeBattlePassLevelRewardRsp.Retcode.HasTake;return rsp;}
   try{
    var rewards=Levels(Schedule).Where(x=>(uint)x["SeasonLevel"]>state.BattlePassRewardLevel&&(uint)x["SeasonLevel"]<=bp.Level&&(int)x["BasicRewardID"]>0).Select(x=>DailyGameplay.Reward((int)x["BasicRewardID"])).ToArray();
    DailyGameplay.Validate(p,DailyGameplay.Combine(rewards));
    if(!DailyGameplay.Atomic(p,()=>{foreach(var plan in rewards)rsp.BasicRewardLists.Add(DailyGameplay.Grant(p,plan));DailyGameplay.State(p).BattlePassRewardLevel=bp.Level;})){rsp.BasicRewardLists.Clear();return rsp;}
    rsp.retcode=TakeBattlePassLevelRewardRsp.Retcode.Succ;return rsp;
   }catch(Exception ex){rsp.BasicRewardLists.Clear();Packet.c.Warn("[BattlePass] reward rejected: "+ex.Message);return rsp;}
  }
 }
}
