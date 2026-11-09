using System;
using System.IO;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static class StaminaStorageGameplay
{
 public sealed class State:IRow {public long Id{get;set;}public long CreatedAt{get;set;}public uint Uid{get;set;}public long Tick{get;set;}public uint Stored{get;set;}public long Fraction{get;set;}}
 private static SqlTable<State> Store=>Global.Table<State>("StaminaStorageStates",("owner_uid","INTEGER",x=>x.Uid,true));
 internal static int Limit(Player p){var r=PlayerLevelData.GetInstance();return r.FromLevel(r.CalculateLevel(p.User.Exp).Level)?.Stamina??r.All.Last().Stamina;}
 internal static State Settle(Player p,uint level){lock(p.Equipment){
  long now=DailyGameplay.Clock();var s=Store.All().FirstOrDefault(x=>x.Uid==p.User.Uid)??new State{Uid=p.User.Uid,Tick=now};
  var row=JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/AdventureWelfare.json")).Where(x=>(int)x["Type"]==5&&(uint)x["AdventureLevel"]<=level).OrderByDescending(x=>(uint)x["AdventureLevel"]).FirstOrDefault();
  if(row==null)throw new InvalidOperationException("Missing stamina storage welfare");
  uint cap=(uint)row["Par2"],rate=(uint)row["Par1"];if(rate>10000)throw new InvalidOperationException("Invalid overflow percentage");
  int before=p.User.Stamina;
  long ticks=Math.Max(0,(now-s.Tick)/360);long normal=Math.Min(ticks,Math.Max(0,Limit(p)-p.User.Stamina));
  long extra=0;
  if(GmLocks.StaminaLocked)
  {
   // 单机版：体力锁死 —— 既不自然恢复，也不往仓库里溢。
   // extra 保持 0，Tick 照常推进，等于「这段时间当作没过去」。
   p.User.Stamina=GmLocks.StaminaLock;
  }
  else
  {
   p.User.Stamina=checked(p.User.Stamina+(int)normal);extra=ticks-normal;
  }
  long credit=checked(extra*rate+s.Fraction);s.Stored=(uint)Math.Min(cap,(long)s.Stored+credit/10000);s.Fraction=s.Stored>=cap?0:credit%10000;
  s.Tick+=ticks*360;
  try{Global.Store.InTransaction(()=>{if(s.Id==0)Store.InsertOne(s);else Store.ReplaceOne(s);p.User.Save();});}
  catch{p.User.Stamina=before;throw;}
  return s;
 }}
 // Read-only gate: heartbeat queries must not write every three seconds.
 internal static bool Due(Player p){if(GmLocks.StaminaLocked)return false;var s=Store.All().FirstOrDefault(x=>x.Uid==p.User.Uid);return s==null||DailyGameplay.Clock()-s.Tick>=360;}
 internal static uint Balance(Player p)=>Store.All().FirstOrDefault(x=>x.Uid==p.User.Uid)?.Stored??0;
 internal static uint RecoverLeft(Player p){var s=Store.All().FirstOrDefault(x=>x.Uid==p.User.Uid);return s==null?360u:(uint)(360-Math.Clamp(DailyGameplay.Clock()-s.Tick,0,359));}
 internal static ClaimStaminaRsp Claim(Player p,uint level,uint amount){lock(p.Equipment){
  var rsp=new ClaimStaminaRsp{retcode=ClaimStaminaRsp.Retcode.Fail};
  try{
   var s=Settle(p,level);
   if(amount==0||amount>s.Stored){rsp.retcode=ClaimStaminaRsp.Retcode.StoreLack;return rsp;}
   if((long)p.User.Stamina+amount>Limit(p)){rsp.retcode=ClaimStaminaRsp.Retcode.StaminaExceed;return rsp;}
   int before=p.User.Stamina;uint stored=s.Stored;
   try{Global.Store.InTransaction(()=>{p.User.Stamina+=checked((int)amount);s.Stored-=amount;p.User.Save();Store.ReplaceOne(s);});}
   catch{p.User.Stamina=before;s.Stored=stored;throw;}
   rsp.retcode=ClaimStaminaRsp.Retcode.Succ;rsp.Stamina=amount;return rsp;
  }catch(Exception ex){Packet.c.Warn("[StaminaStorage] failed without claim: "+ex.Message);return rsp;}
 }}
}
