using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
// Historical reference rules; uniform per-group stock is a local reconstruction.
internal static class HistoricalShopRotation
{
 internal static readonly object Gate=new();
 public sealed class State:IRow{public long Id{get;set;}public long CreatedAt{get;set;}public uint Uid{get;set;}public uint Shop{get;set;}public long Day{get;set;}public uint Refreshes{get;set;}public uint[] Goods{get;set;}=Array.Empty<uint>();}
 private static SqlTable<State> Store=>Global.Table<State>("HistoricalShopRotations",("owner_shop","TEXT",s=>s.Uid+":"+s.Shop,true));
 internal static bool Rotates(JObject shop)=>shop["rotation"] is JObject;
 private static uint[] Draw(JObject config,uint[] previous)
 {
  for(int tries=0;tries<8;tries++){
   var selected=new List<uint>();foreach(var group in ((JArray)config["groups"]).OfType<JObject>()){
    int count=(int)group["count"];var pool=((JArray)group["goods"]).Select(x=>(uint)x).Distinct().Where(x=>!selected.Contains(x)).ToArray();if(count<1||pool.Length<count)throw new InvalidDataException("Invalid historical stock group");Random.Shared.Shuffle(pool.AsSpan());selected.AddRange(pool.Take(count));}
   if(!selected.SequenceEqual(previous))return selected.ToArray();
  }throw new InvalidOperationException("Stock did not change; refresh cancelled");
 }
 internal static State Load(Player p,JObject shop)
 {
  lock(Gate){uint id=(uint)shop["id"];var s=Store.All().FirstOrDefault(x=>x.Uid==p.User.Uid&&x.Shop==id)??new State{Uid=p.User.Uid,Shop=id,Day=-1};long day=(string)shop["period"]=="week"?DailyGameplay.Week(DailyGameplay.Clock()):DailyGameplay.Day(DailyGameplay.Clock());
   if(s.Day!=day){s.Day=day;s.Refreshes=0;s.Goods=Draw((JObject)shop["rotation"],s.Goods);Global.Store.InTransaction(()=>Save(s));}return s;
  }
 }
 private static void Save(State s){if(s.Id==0)Store.InsertOne(s);else Store.ReplaceOne(s);}
 internal static uint[] Goods(Player p,JObject shop)=>Rotates(shop)?Load(p,shop).Goods.Concat(((JArray)shop["fixed_goods"]??new JArray()).Select(x=>(uint)x)).Distinct().ToArray():((JArray)shop["goods"]).Select(x=>(uint)x).ToArray();
 internal static string Batch(Player p,JObject shop)=>Rotates(shop)?":"+Load(p,shop).Refreshes:"";
 internal static ManualRefreshShopRsp Refresh(Player p,ManualRefreshShopReq req)
 {
  var rsp=new ManualRefreshShopRsp{retcode=ManualRefreshShopRsp.Retcode.Fail};lock(p.Equipment)lock(Gate){
   var path="material-shops.json";var shop=File.Exists(path)?((JArray)JObject.Parse(File.ReadAllText(path))["shops"]).OfType<JObject>().FirstOrDefault(x=>(uint)x["id"]==req.ShopId):null;
   if(shop==null||((uint)shop["id"]==14&&!WitchShopGameplay.Open(p))){rsp.retcode=ManualRefreshShopRsp.Retcode.ShopNotExist;return rsp;}if(!Rotates(shop)){rsp.retcode=ManualRefreshShopRsp.Retcode.NoNeedRefresh;return rsp;}
   var config=(JObject)shop["rotation"];var s=Load(p,shop);if(s.Refreshes>=(uint)config["manual_limit"]){rsp.retcode=ManualRefreshShopRsp.Retcode.RefreshTimesLack;return rsp;}int cost=(int)config["manual_hcoin_cost"];if(cost<0)return rsp;if(p.User.Hcoin<cost){rsp.retcode=ManualRefreshShopRsp.Retcode.HcoinLack;return rsp;}
   uint[] next;try{next=Draw(config,s.Goods);}catch(Exception ex){Packet.c.Warn("[HistoricalShop] "+ex.Message);return rsp;}
   if(!DailyGameplay.Atomic(p,()=>{p.User.Hcoin-=cost;s.Refreshes++;s.Goods=next;Save(s);} ))return rsp;rsp.retcode=0;return rsp;
  }
 }
}
