using System;
using System.Linq;
using Common.Resources.Proto;
namespace PemukulPaku.GameServer.Game;
internal static partial class GodWarProgression
{
    // Native GetGodWarCoin = equipment material baseline + actor-local delta.
    // A full equipment sync must not add the already animated room delta again.
    private static uint DeepCurrencyBase(ChallengeRun run)=>run.Deep.PendingStage!=0&&run.Deep.RoomCurrencyVersion==1?run.Deep.RoomCoinBase:run.Deep.Coins;
    private static bool RebaseDeepCurrency(Player p,State s,ChallengeRun run)
    {
        var d=run.Deep;if(d.PendingStage==0)return true;
        return Commit(p,s,()=>{d.RoomCoinBase=d.Coins;d.RoomCurrencyVersion=1;if(d.Phase=="fighting")d.StartCoinHandled=false;});
    }
    private static GodWarEventInfo DeepCoinEvent(uint tale,uint eventId,uint amount)
    {
        uint currency=(uint)Table("GodWarTale").Single(x=>(uint)x["TaleID"]==tale)["RogueCoin"];
        return Event(eventId,currency,amount);
    }
    private static GodWarEventInfo[] DeepShopRebaseEvents(ChallengeRun run,uint tale)
    {
        if(run?.Deep is not {} d||d.PendingStage==0||d.RoomCurrencyVersion!=1||d.RoomCoinBase==d.Coins)
            return Array.Empty<GodWarEventInfo>();
        // The merchant UI reads storage directly. Clear the pre-existing room
        // delta before pushing the new absolute balance so combat won't count it twice.
        var correction=d.Coins>d.RoomCoinBase
            ?DeepCoinEvent(tale,5600106,d.Coins-d.RoomCoinBase)
            :DeepCoinEvent(tale,551002,d.RoomCoinBase-d.Coins);
        correction.SourceEventId=correction.EventId;
        correction.SourceType=1;
        return new[]{correction};
    }
    internal static (BuyGodWarShopGoodsRsp Reply,GodWarEventInfo[] Events) BuyDeepGoodsForClient(Player p,BuyGodWarShopGoodsReq req)
    {
        lock(p.Equipment)lock(Gate){var state=Load(p.User);var events=DeepShopRebaseEvents(state.Runs.GetValueOrDefault(req.TaleId),req.TaleId);
            var rsp=BuyDeepGoods(p,req);return(rsp,rsp.retcode==0?events:Array.Empty<GodWarEventInfo>());}
    }
    internal static (RefreshGodWarShopGoodsRsp Reply,GodWarEventInfo[] Events) RefreshDeepShopForClient(Player p,RefreshGodWarShopGoodsReq req)
    {
        lock(p.Equipment)lock(Gate){var state=Load(p.User);var events=DeepShopRebaseEvents(state.Runs.GetValueOrDefault(req.TaleId),req.TaleId);
            var rsp=RefreshDeepShop(p,req);return(rsp,rsp.retcode==0?events:Array.Empty<GodWarEventInfo>());}
    }
}
