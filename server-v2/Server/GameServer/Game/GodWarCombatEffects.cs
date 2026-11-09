using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PemukulPaku.GameServer.Game;

internal static partial class GodWarProgression
{
    // These are server-side run rules. Ability-type talents and signets are
    // already read by the client's GodWar plugin; do not inject them twice.
    private static uint TalentRule(State state,uint tale,uint id,uint effect,uint fallback,int parameter=0)
    {
        uint level=state.Talents.GetValueOrDefault(id);
        if(level==0)return fallback;
        var meta=Table("GodWarTalentData").SingleOrDefault(x=>(uint)x["TalentID"]==id&&(uint)x["TalentEffectType"]==effect&&x["TaleIDList"].Values<uint>().Contains(tale));
        var row=meta==null?null:Table("GodWarTalentLevelData").SingleOrDefault(x=>(uint)x["TalentID"]==id&&(uint)x["TalentLevel"]==level);
        return row!=null&&row["EffectParamList"].Count()>parameter&&uint.TryParse((string)row["EffectParamList"][parameter],out uint value)?value:fallback;
    }
    private static uint DeepPrice(DeepState d,uint amount,bool upgrade)=>checked((uint)((ulong)amount*(upgrade?d.UpgradePercent:d.PurchasePercent)*DeepSpecialDiscount(d)/10000));
    private static void PrepareDeepEffects(State state,ChallengeRun run,uint tale,bool initial)
    {
        var d=run.Deep;if(d==null)return;
        ApplyExclusiveArmor(state,run);
        // Capture reward-yield talent for new runs only. Existing runs keep their issued reward rules.
        if(initial&&d.ShopRulesVersion>=2&&run.Challenge.AvatarLists.Single().AvatarId==2601){d.AvatarShopPercent=80;d.Coins=checked(d.Coins+500);run.Challenge.GainCoinNum=checked(run.Challenge.GainCoinNum+500);}
        if(initial){d.CoinTalentRulesVersion=1;d.NormalStartCoinBonus=TalentRule(state,tale,113,8,0,1);d.EliteStartCoinBonus=TalentRule(state,tale,131,8,0,1);d.BossStartCoinBonus=TalentRule(state,tale,133,8,0,1);d.YieldRulesVersion=1;d.StoneYieldBonus=TalentRule(state,tale,132,4,0)==119703?TalentRule(state,tale,132,4,0,1):0;}
        if(d.Goods.Count>0)d.MerchantOpen=true;
        if(d.StockVersion==0){d.PurchaseSlotsBonus=checked(2*TalentRule(state,tale,221,11,0));d.UpgradeSlotsBonus=TalentRule(state,tale,222,12,0);d.StockVersion=1;}
        if(d.EffectsVersion!=0){CapturePortalSigilRefresh(run);return;}
        d.PurchasePercent=Math.Min(100,TalentRule(state,tale,122,6,100));
        d.UpgradePercent=Math.Min(100,TalentRule(state,tale,123,7,100));
        uint baseLimit=(uint)Table("GodWarTale").Single(x=>(uint)x["TaleID"]==tale)["TeleportResetNum"];
        d.PortalRefreshLimit=Math.Max(run.Challenge.RefreshTeleportTimes,checked(baseLimit+TalentRule(state,tale,211,3,0)));
        run.Challenge.RefreshTeleportTimesLimit=d.PortalRefreshLimit;
        // Existing runs keep their earned/spent balance; a snapshot must not
        // grant the starting bonus again on re-entry or migration.
        if(initial){uint bonus=TalentRule(state,tale,111,5,0,1);d.Coins=checked(d.Coins+bonus);run.Challenge.GainCoinNum=checked(run.Challenge.GainCoinNum+bonus);}
        foreach(var good in d.Goods.Where(x=>!x.Sold)){good.BaseCost=good.Cost;good.Cost=DeepPrice(d,good.BaseCost,good.Upgrade);}
        d.EffectsVersion=1;CapturePortalSigilRefresh(run);
    }
}
