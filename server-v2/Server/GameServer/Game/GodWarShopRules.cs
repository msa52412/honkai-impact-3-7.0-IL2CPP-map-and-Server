using System.Linq;
namespace PemukulPaku.GameServer.Game;
internal static partial class GodWarProgression
{
    // BV18C4y1J7n9, 7.0 formal-server recording, 23:18-24:00:
    // ordinary purchase 206 at 90%; normal upgrades 68/91/114 at 90%.
    // BV1t94y1h72m P13 24:02: augmentation purchase 306, ordinary 229.
    // First augmentation upgrade 102 is corroborated by 91 at 90% in the former video;
    // later augmentation tiers 127/153 remain a reconstruction of the observed tier grid.
    private static uint DeepGoodsBaseCost(DeepState d,uint id,bool upgrade)
    {
        if(d.ShopRulesVersion>=1&&(uint)Table("GodWarBuff").First(x=>(uint)x["BuffID"]==id)["BuffQuality"]==1)
            return upgrade?d.Buffs[id] switch{1=>76u,2=>102u,3=>127u,_=>40u}:229u;
        if(d.ShopRulesVersion>=2&&(uint)Table("GodWarBuff").First(x=>(uint)x["BuffID"]==id)["BuffQuality"]==2)
            return upgrade?d.Buffs[id] switch{1=>102u,2=>127u,3=>153u,_=>40u}:306u;
        return upgrade?40u:80u;
    }
    // P13 23:54/24:02/24:13 directly shows 0/80/160 in 7.0.
    // The original 581107 description grants the first TWO refreshes free.
    private static uint DeepRefreshCost(DeepState d)
    {
        if(d.ShopRulesVersion==0)return 20;
        uint free=d.ShopRulesVersion>=2&&d.Buffs.ContainsKey(581107)?2u:1u;
        return d.ShopRefreshCount<free?0:checked((d.ShopRefreshCount-free+1)*80u);
    }
    // Both native buy/upgrade labels read netShop+0x24 = special_discount.
    // Discounts beyond the authored individual effects stack multiplicatively
    // as an explicit compatibility reconstruction, not a measured probability.
    private static uint DeepSpecialDiscount(DeepState d)=>d.ShopRulesVersion>=2?
        d.AvatarShopPercent*(d.Buffs.ContainsKey(581108)?90u:100u)/100:100u;
    private static void RepriceDeepShop(DeepState d)
    {
        if(d.ShopRulesVersion<2)return;
        foreach(var good in d.Goods.Where(g=>!g.Sold))
        {good.BaseCost=DeepGoodsBaseCost(d,good.Buff,good.Upgrade);good.Cost=DeepPrice(d,good.BaseCost,good.Upgrade);}
    }
    private static void RenewDeepUpgrade(DeepState d,DeepGoods g)
    {
        if(d.ShopRulesVersion==0||!g.Upgrade)return;
        if(!Table("GodWarBuff").Any(x=>(uint)x["BuffID"]==g.Buff&&(uint)x["BuffLevel"]==d.Buffs[g.Buff]+1))return;
        // Retire the authorized index: replay of the previous request cannot buy
        // the next level. The new offer is issued only after this commit succeeds.
        g.Index=checked(++d.Revision*64+1);g.Sold=false;
        g.BaseCost=DeepGoodsBaseCost(d,g.Buff,true);g.Cost=DeepPrice(d,g.BaseCost,true);
    }
}
