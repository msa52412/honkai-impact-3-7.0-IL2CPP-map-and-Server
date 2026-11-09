using System;using System.IO;using System.Linq;using System.Collections.Generic;using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static class ShopPricing
{
 internal static (Dictionary<int,long> Costs,long Crystals) Quote(JObject goods,uint before,uint amount)
 {
  int kind=(int)goods["PriceRateID"],discount=(int)goods["Discount"];if(kind<1||kind>10||discount<=0||discount>10000||amount==0||amount>1000||before>uint.MaxValue-amount)throw new InvalidDataException("Invalid shop price parameters");
  var rows=kind==10?null:JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/ShopGoodsPriceRate.json")).OfType<JObject>().ToDictionary(x=>(uint)x["BuyTimes"]);
  var costs=new Dictionary<int,long>();long crystals=0;
  for(uint offset=1;offset<=amount;offset++){
   int rate=100;if(kind!=10){if(!rows.TryGetValue(before+offset,out var row)||row["PriceType"+kind]==null)throw new InvalidDataException("Missing stepped shop price");rate=(int)row["PriceType"+kind];}if(rate<=0)throw new InvalidDataException("Invalid stepped shop rate");
   long Cost(int n){if(n<0)throw new InvalidDataException("Negative base price");return checked(checked((long)n*discount*rate)/1000000);}
   crystals=checked(crystals+Cost((int)goods["HCoinCost"]));for(int i=1;i<=5;i++){int id=(int)goods["CostItemId"+i],n=(int)goods["CostItemNum"+i];if(id==0)continue;if(id<0||n<=0)throw new InvalidDataException("Invalid shop currency");costs[id]=checked(costs.GetValueOrDefault(id)+Cost(n));}
  }
  return(costs,crystals);
 }
}
