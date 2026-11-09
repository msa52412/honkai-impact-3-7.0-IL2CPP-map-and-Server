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
internal static class ArmadaContributionGameplay
{
 public sealed class State:IRow{
  public long Id{get;set;}public long CreatedAt{get;set;}public string Key{get;set;}public uint Armada{get;set;}public uint Uid{get;set;}public long Week{get;set;}=-1;
  public uint WeekContribution{get;set;}public uint TotalContribution{get;set;}public uint WeekPopl{get;set;}public uint TotalPopl{get;set;}public uint WeekFund{get;set;}public uint TotalFund{get;set;}public HashSet<uint> Taken{get;set;}=new();
 }
 private static SqlTable<State> Store=>Global.Table<State>("ArmadaContributionStates",("member_key","TEXT",x=>x.Key,true));
 private static long Week=>DailyGameplay.Week(DailyGameplay.Clock());
 private static State Current(State s){if(s.Week!=Week){s.Week=Week;s.WeekContribution=s.WeekPopl=s.WeekFund=0;s.Taken.Clear();}return s;}
 private static State Load(uint armada,uint uid)=>Current(Store.All().FirstOrDefault(x=>x.Key==armada+":"+uid)??new State{Key=armada+":"+uid,Armada=armada,Uid=uid});
 private static void Save(State s){if(s.Id==0)Store.InsertOne(s);else Store.ReplaceOne(s);}
 internal static uint Total(Armada a)=>checked((uint)Store.All().Where(s=>s.Armada==a.Id&&a.MemberUids.Contains(s.Uid)).Sum(s=>(long)Current(s).WeekContribution));
 internal static uint Population(Armada a)=>checked((uint)Math.Min(uint.MaxValue,Store.All().Where(s=>s.Armada==a.Id&&a.MemberUids.Contains(s.Uid)).Sum(s=>(long)s.TotalPopl)));
 // Called inside the same reward/stamina transaction as the source action.
 internal static void Credit(Player p,uint contribution=0,uint population=0,uint fund=0){lock(ArmadaModule.SyncGate){
  var a=ArmadaModule.GetInstance().ByUid(p.User.Uid);if(a==null||contribution==0&&population==0&&fund==0)return;
  var s=Load(a.Id,p.User.Uid);s.WeekContribution=checked(s.WeekContribution+contribution);s.TotalContribution=checked(s.TotalContribution+contribution);s.WeekPopl=checked(s.WeekPopl+population);s.TotalPopl=checked(s.TotalPopl+population);s.WeekFund=checked(s.WeekFund+fund);s.TotalFund=checked(s.TotalFund+fund);Save(s);
 }}
 internal static ArmadaMember Member(Armada a,uint uid,ArmadaPosition position){lock(ArmadaModule.SyncGate){
  var s=Load(a.Id,uid);var u=User.FromUid(uid);var avatar=u==null?null:Common.Database.Avatar.AvatarsFromUid(uid).FirstOrDefault(x=>x.AvatarId==(u.WarshipAvatar?.WarshipFirstAvatarId??0))??Common.Database.Avatar.AvatarsFromUid(uid).FirstOrDefault(x=>x.Star>0);
  var brief=new PlayerFriendBriefData{Uid=uid,Nickname=u?.Nick??("UID"+uid),Level=u==null?1u:(uint)PlayerLevelData.GetInstance().CalculateLevel(u.Exp).Level,ArmadaId=a.Id,ArmadaName=a.Name,FrameId=(uint)Math.Max(0,u?.FrameId??0),CustomHeadId=(uint)Math.Max(0,u?.CustomHeadId??0),AvatarId=avatar?.AvatarId??101,AvatarLevel=avatar?.Level??1,AvatarStar=avatar?.Star??1,DressId=avatar?.DressId??0};
  return new ArmadaMember{Uid=uid,Position=position,BriefData=brief,WeekContribution=s.WeekContribution,TotalContribution=s.TotalContribution,WeekPopl=s.WeekPopl,TotalPopl=s.TotalPopl,WeekFund=s.WeekFund,TotalFund=s.TotalFund};
 }}
 internal static void Fill(Armada a,ArmadaData data,uint uid){lock(ArmadaModule.SyncGate){
  var s=Load(a.Id,uid);data.ArmadaPlayerData.TakeAchievementIdLists=s.Taken.Order().ToArray();data.Brief.Contribution=Total(a);data.Brief.RecentContribution=data.Brief.Contribution;data.Brief.Popl=Population(a);
  data.Brief.LeaderBriefData=Member(a,a.LeaderUid,ArmadaPosition.ArmadaPositionLeader).BriefData;
 }}
 private static JObject[] Rewards=>JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/ArmadaLinerReward.json")).OfType<JObject>().OrderBy(x=>(uint)x["ID"]).ToArray();
 internal static TakeArmadaAchievementRsp Take(Player p,TakeArmadaAchievementReq req){lock(p.Equipment)lock(ArmadaModule.SyncGate){
  var rsp=new TakeArmadaAchievementRsp{retcode=TakeArmadaAchievementRsp.Retcode.Fail,Id=req.Id};var a=ArmadaModule.GetInstance().ByUid(p.User.Uid);
  if(a==null){rsp.retcode=TakeArmadaAchievementRsp.Retcode.NotInArmada;return rsp;}
  var s=Load(a.Id,p.User.Uid);uint score=Total(a);var rows=Rewards.Where(x=>req.IsTakeAll||(uint)x["ID"]==req.Id).ToArray();
  if(rows.Length==0)return rsp;
  rows=rows.Where(x=>!s.Taken.Contains((uint)x["ID"])).ToArray();if(rows.Length==0){rsp.retcode=TakeArmadaAchievementRsp.Retcode.HasTake;return rsp;}
  rows=rows.Where(x=>(uint)x["NeedScore"]<=score).ToArray();if(rows.Length==0){rsp.retcode=TakeArmadaAchievementRsp.Retcode.NotReach;return rsp;}
  try{var plans=rows.Select(x=>DailyGameplay.Reward((int)x["RewardID"])).ToArray();DailyGameplay.Validate(p,DailyGameplay.Combine(plans));
   if(!DailyGameplay.Atomic(p,()=>{foreach(var plan in plans)rsp.RewardLists.Add(DailyGameplay.Grant(p,plan));foreach(var row in rows)s.Taken.Add((uint)row["ID"]);Save(s);})){rsp.RewardLists.Clear();return rsp;}
   rsp.IdLists=rows.Select(x=>(uint)x["ID"]).ToArray();rsp.TakeAchievementIdLists=s.Taken.Order().ToArray();rsp.RewardData=rsp.RewardLists.FirstOrDefault();rsp.retcode=0;return rsp;
  }catch(Exception e){rsp.RewardLists.Clear();Packet.c.Warn("[ArmadaContribution] reward rejected: "+e.Message);return rsp;}
 }}
 internal static void Push(Session s){var a=ArmadaModule.GetInstance().ByUid(s.Player.User.Uid);if(a==null)return;var m=Member(a,s.Player.User.Uid,a.LeaderUid==s.Player.User.Uid?ArmadaPosition.ArmadaPositionLeader:ArmadaPosition.ArmadaPositionMember);s.Send(Packet.FromProto(new ArmadaPlayerContributionSyncNotify{Uid=s.Player.User.Uid,ArmadaContribution=Total(a),WeekContribution=m.WeekContribution,TotalContribution=m.TotalContribution},CmdId.ArmadaPlayerContributionSyncNotify));s.ProcessPacket(Packet.FromProto(new GetArmadaDataReq(),CmdId.GetArmadaDataReq));}
}
