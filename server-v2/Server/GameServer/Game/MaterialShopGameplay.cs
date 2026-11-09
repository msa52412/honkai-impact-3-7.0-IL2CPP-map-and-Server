using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
// The item/price/limit are verified local rows; shop stock cycles are a documented compatibility catalogue.
internal static class MaterialShopGameplay
{
 public sealed class Stock {public string Cycle{get;set;}="";public uint Count{get;set;}}
 public sealed class State:IRow {public long Id{get;set;}public long CreatedAt{get;set;}public uint Uid{get;set;}public Dictionary<string,Stock> Purchases{get;set;}=new();}
 private static SqlTable<State> Store=>Global.Table<State>("MaterialShopStates",("owner_uid","INTEGER",x=>x.Uid,true));
 private static JObject Config=>File.Exists("material-shops.json")?JObject.Parse(File.ReadAllText("material-shops.json")):new JObject{["shops"]=new JArray()};
 private static JObject[] Goods=>JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/ShopGoodsData.json")).OfType<JObject>().ToArray();
 private static State Load(Player p)=>Store.All().FirstOrDefault(x=>x.Uid==p.User.Uid)??new State{Uid=p.User.Uid};
 private static string Month()=>DateTimeOffset.FromUnixTimeSeconds(DailyGameplay.Clock()+14400).ToString("yyyy-MM");
 private static string Cycle(JObject shop,uint id=0)=>ShopStockPeriods.Cycle(shop,id);
 private static uint MonthEnd(){var now=DateTimeOffset.FromUnixTimeSeconds(DailyGameplay.Clock()+14400);return checked((uint)(new DateTimeOffset(now.Year,now.Month,1,0,0,0,TimeSpan.Zero).AddMonths(1).ToUnixTimeSeconds()-14400));}
 private static uint End(JObject shop)=>ShopStockPeriods.Next(shop);
 private static bool Unlimited(JObject shop,uint id)=>((JArray)shop["unlimited_goods"]??new JArray()).Any(x=>(uint)x==id);
 private static string Batch(Player p,JObject shop,uint id)=>((JArray)shop["fixed_goods"]??new JArray()).Any(x=>(uint)x==id)?"":HistoricalShopRotation.Batch(p,shop);
 private static uint Count(Player p,State state,JObject shop,uint id)=>state.Purchases.TryGetValue(shop["id"]+":"+id,out var stock)&&ShopStockPeriods.Matches(shop,id,stock.Cycle,Batch(p,shop,id))?stock.Count:0;
 // Pure-orb stock requires an owned SSS avatar whose surplus fragments yield that currency.
 private static bool CanAccess(Player p,JObject shop)
 {
  if((uint)shop["id"]==14)return WitchShopGameplay.Open(p);
  int currency=(int?)shop["required_fragment_currency"]??0;
  if(currency==0)return true;
  var avatars=BaseExcelReader<AvatarData,AvatarDataExcel>.GetInstance().All;
  return p.AvatarList.Any(a=>a.Star>=5&&avatars.Any(meta=>meta.AvatarId==a.AvatarId&&BaseExcelReader<AvatarFragmentData,AvatarFragmentDataExcel>.GetInstance().FromId(meta.AvatarFragmentId)?.ReturnMaterialId==currency));
 }
 internal static bool AffectsAvatar(uint item)=>BaseExcelReader<AvatarData,AvatarDataExcel>.GetInstance().All.Any(a=>a.AvatarCardId==item||a.AvatarFragmentId==item);
 internal static GetShopListRsp Build(Player p)
 {
  lock(p.Equipment)lock(HistoricalShopRotation.Gate){var rsp=new GetShopListRsp{retcode=0,IsAll=true};var state=Load(p);var goods=Goods.ToDictionary(x=>(uint)x["ID"]);
   foreach(var c in ((JArray)Config["shops"]).OfType<JObject>()){
    if((uint)c["id"]==5&&PemukulPaku.GameServer.MPModule.ArmadaModule.GetInstance().ByUid(p.User.Uid)==null)continue;
    if(!CanAccess(p,c))continue;
    var rotation=HistoricalShopRotation.Rotates(c)?HistoricalShopRotation.Load(p,c):null;
    var shop=new Shop{ShopId=(uint)c["id"],ShopType=(uint)c["id"],ShopName=(string)c["name"],ShopIconPath=(string)c["icon_path"]??((uint)c["id"]==1?"SpriteOutput/ShopTabIcons/ParentTab/IconDaily":"SpriteOutput/ShopTabIcons/ParentTab/IconCurrency"),TextMapName=(string)c["text_key"]??"ShopName"+(uint)c["id"],CurrencyLists=((JArray)c["currencies"]??new JArray((uint)c["id"]==8?100u:5u)).Select(x=>(uint)x).ToArray(),IsOpen=true,IsShow=true,UnlockLevel=1,BeginTime=1577836800,EndTime=2082758399,NextAutoRefreshTime=End(c),ManualRefreshTimes=rotation?.Refreshes??0,MaxManualRefreshTimes=rotation==null?0:(uint)c["rotation"]["manual_limit"],NextRefreshCost=rotation==null?0:(uint)c["rotation"]["manual_hcoin_cost"],RefreshItem=0u};
    foreach(uint id in HistoricalShopRotation.Goods(p,c))if(goods.ContainsKey(id)&&((uint)c["id"]!=14||WitchShopGameplay.Visible(p,goods[id]))&&!Post62Materials.Retired((int)goods[id]["ItemID"]))shop.GoodsLists.Add(new Common.Resources.Proto.Goods{GoodsId=id,BuyTimes=Count(p,state,c,id),CanBeRefresh=!Unlimited(c,id) && ShopStockPeriods.Refresh(c,id)!=Common.Resources.Proto.Goods.RefreshTimeType.ShopType && ShopStockPeriods.Refresh(c,id)!=Common.Resources.Proto.Goods.RefreshTimeType.VersionType,SortId=rotation==null?0u:(uint)shop.GoodsLists.Count+1,refresh_time_type=ShopStockPeriods.Refresh(c,id),show_type=Common.Resources.Proto.Goods.ShowType.NormalShow,BeginTime=1577836800,EndTime=shop.EndTime,MaxBuyType=Unlimited(c,id)?2u:0u,MinLevel=1,MaxLevel=100});
    if(shop.GoodsLists.Count>0)rsp.ShopLists.Add(shop);
   }return rsp;
  }
 }
 internal static BuyGoodsRsp Buy(Player p,BuyGoodsReq req)
 {
  var rsp=new BuyGoodsRsp{ShopId=req.ShopId,GoodsId=req.GoodsId,retcode=BuyGoodsRsp.Retcode.Fail};lock(p.Equipment)lock(HistoricalShopRotation.Gate){
   var shop=((JArray)Config["shops"]).OfType<JObject>().FirstOrDefault(x=>(uint)x["id"]==req.ShopId);
   if(shop==null||!CanAccess(p,shop)||(req.ShopId==5&&PemukulPaku.GameServer.MPModule.ArmadaModule.GetInstance().ByUid(p.User.Uid)==null)){rsp.retcode=BuyGoodsRsp.Retcode.ShopClose;return rsp;}
   if(!HistoricalShopRotation.Goods(p,shop).Contains(req.GoodsId)){rsp.retcode=BuyGoodsRsp.Retcode.GoodsNotExist;return rsp;}
   var g=Goods.FirstOrDefault(x=>(uint)x["ID"]==req.GoodsId);if(g==null||req.ShopId==14&&!WitchShopGameplay.Visible(p,g)||Post62Materials.Retired((int)g["ItemID"]))return rsp;
   if(req.CouponMaterialId!=0||req.CouponNum!=0||req.McoinCouponMaterialId!=0){rsp.retcode=BuyGoodsRsp.Retcode.CouponError;return rsp;}
   if(req.GiftPackSelectRewardId!=0||req.DiscountSaveNum!=0||(int)g["MCoinCost"]!=0||((int)g["PriceRateID"]<1||(int)g["PriceRateID"]>10)||((int)g["Discount"]<=0||(int)g["Discount"]>10000)||(bool)g["IsAutoOpen"])return rsp;
   uint amount=req.ShouldSerializeGoodsNum()?req.GoodsNum:1;
   if(amount==0||amount>1000||amount>1&&(!(bool)g["IsBuyMultiple"]||amount>(uint)g["BuyMultipleMax"])){rsp.retcode=BuyGoodsRsp.Retcode.BuyTimesLack;return rsp;}
   var state=Load(p);uint before=Count(p,state,shop,req.GoodsId);bool unlimited=Unlimited(shop,req.GoodsId);if(!unlimited&&(ulong)before+amount>(uint)g["MaxBuyTimes"]){rsp.retcode=BuyGoodsRsp.Retcode.BuyTimesLack;return rsp;}
   Dictionary<int,long> costs;long hcoin;try{(costs,hcoin)=ShopPricing.Quote(g,before,amount);}catch{return rsp;}
   if(hcoin<0||hcoin>p.User.Hcoin||costs.Any(x=>x.Value>int.MaxValue||x.Value>p.Equipment.MaterialList.Where(m=>m.Id==x.Key).Sum(m=>(long)m.Num))){rsp.retcode=BuyGoodsRsp.Retcode.MoneyLack;return rsp;}
   int item=(int)g["ItemID"];long num=(long)(int)g["ItemNum"]*amount;var row=BaseExcelReader<MaterialData,MaterialDataExcel>.GetInstance().FromId((uint)item);
   var weapon=BaseExcelReader<WeaponData,WeaponDataExcel>.GetInstance().FromId(item);var stigma=BaseExcelReader<StigmataData,StigmataDataExcel>.GetInstance().FromId(item);
   var card=BaseExcelReader<AvatarData,AvatarDataExcel>.GetInstance().All.FirstOrDefault(x=>x.AvatarCardId==item);if(card!=null&&(num!=1||p.AvatarList.Any(a=>a.AvatarId==card.AvatarId&&a.Star>0)))return rsp;
   bool gear=weapon!=null||stigma!=null;if(gear&&(num!=1||(int)g["ItemLevel"]!=1||weapon!=null&&!GachaEligibility.IsAllowedWeapon(item)))return rsp;
   var elf=WitchShopGameplay.ElfFragment(item);
   var fragment=BaseExcelReader<AvatarFragmentData,AvatarFragmentDataExcel>.GetInstance().FromId(item);var meta=fragment==null?null:BaseExcelReader<AvatarData,AvatarDataExcel>.GetInstance().All.FirstOrDefault(a=>a.AvatarFragmentId==item);var owned=meta==null?null:p.AvatarList.FirstOrDefault(a=>a.AvatarId==meta.AvatarId);
   if((row==null&&!gear&&card==null&&elf==null&&(fragment==null||meta==null))||num<=0||num>int.MaxValue)return rsp;
   long held=elf!=null?(p.Elfs.ElfFragments??Array.Empty<ElfFragment>()).Where(x=>x.ElfId==elf.ElfId).Sum(x=>(long)x.FragmentNum):row!=null?p.Equipment.MaterialList.Where(m=>m.Id==item).Sum(m=>(long)m.Num)-costs.GetValueOrDefault(item):(owned?.Fragment??0);long limit=elf!=null?int.MaxValue:row!=null?row.QuantityLimit:gear||card!=null?1:fragment.QuantityLimit;long cap=limit>0?Math.Min(limit,int.MaxValue):int.MaxValue;
   if(held+num>cap){rsp.retcode=BuyGoodsRsp.Retcode.EquipmentFull;return rsp;}
   if(elf!=null){_=Common.Database.Elfs.collection;}var elfBefore=elf==null?null:Newtonsoft.Json.JsonConvert.SerializeObject(p.Elfs.ElfFragments);
   var cardRoster=card==null?null:Newtonsoft.Json.JsonConvert.SerializeObject(p.AvatarList);var weapons=p.Equipment.WeaponList;var stigmas=p.Equipment.StigmataList;var roster=p.AvatarList;var fragments=roster.Select(a=>(item:a,before:a.Fragment)).ToArray();if(!DailyGameplay.Atomic(p,()=>{foreach(var c in costs)p.Equipment.AddMaterial(c.Key,-checked((int)c.Value));p.User.Hcoin-=checked((int)hcoin);if(elf!=null)WitchShopGameplay.GrantElfFragment(p,item,(int)num);else if(row!=null)p.Equipment.AddMaterial(item,(int)num);else if(weapon!=null)p.Equipment.AddWeapon(item);else if(stigma!=null)p.Equipment.AddStigmata(item);else if(card!=null)GachaService.GrantShopCard(p,(uint)item);else{if(owned==null){owned=new AvatarScheme{OwnerUid=p.User.Uid,AvatarId=(uint)meta.AvatarId,Star=0,Level=1,DressId=(uint)meta.DefaultDressId,DressLists=new uint[]{(uint)meta.DefaultDressId}};Common.Database.Avatar.collection.InsertOne(owned);p.AvatarList=p.AvatarList.Append(owned).ToArray();}owned.Fragment=checked(owned.Fragment+(uint)num);owned.Save();}state.Purchases[req.ShopId+":"+req.GoodsId]=new Stock{Cycle=Cycle(shop,req.GoodsId)+Batch(p,shop,req.GoodsId),Count=before+amount};if(state.Id==0)Store.InsertOne(state);else Store.ReplaceOne(state);} )){if(elf!=null)p.Elfs.ElfFragments=Newtonsoft.Json.JsonConvert.DeserializeObject<ElfFragment[]>(elfBefore);p.Equipment.WeaponList=weapons;p.Equipment.StigmataList=stigmas;p.AvatarList=cardRoster==null?roster:Newtonsoft.Json.JsonConvert.DeserializeObject<AvatarScheme[]>(cardRoster);foreach(var a in fragments)a.item.Fragment=a.before;return rsp;}
   rsp.retcode=BuyGoodsRsp.Retcode.Succ;rsp.GoodsBuyTimes=before+amount;rsp.ItemId=(uint)item;rsp.Level=(uint)Math.Max(1,(int)g["ItemLevel"]);rsp.Num=(uint)num;return rsp;
  }
 }
}
