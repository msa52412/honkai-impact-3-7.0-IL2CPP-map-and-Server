using System;
using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static partial class GodWarProgression
{
    private static bool TryDeepStartTalentCoins(ChallengeRun run,uint tale,uint root,out List<GodWarEventInfo> events)
    {
        var result=new List<GodWarEventInfo>();events=result;var d=run.Deep;
        if(d==null||d.CoinTalentRulesVersion!=1||d.RoomTalentCoinHandled)return true;
        // Only these actual start groups contain the fixed talent gifts.
        // Never run all conditional children, initial111 twice, or debug500.
        if(root is not (3000201 or 3000208 or 3000217))return true;
        var children=Table("GodWarEvent").Single(x=>(uint)x["EventID"]==root)["ParamsVar"].Values<uint>().ToHashSet();
        uint currency=(uint)Table("GodWarTale").Single(x=>(uint)x["TaleID"]==tale)["RogueCoin"];
        bool Add(uint amount,uint first,uint last)
        {
            if(amount==0)return true;
            var row=Table("GodWarEvent").SingleOrDefault(x=>(uint)x["EventID"]>=first&&(uint)x["EventID"]<=last&&children.Contains((uint)x["EventID"])&&(uint)x["EventType"]==44&&x["ParamsVar"].Values<uint>().SequenceEqual(new[]{amount,amount}));
            if(row==null)return false;
            result.Add(Event((uint)row["EventID"],currency,amount));return true;
        }
        return Add(d.NormalStartCoinBonus,3000205,3000207)
            &&(root!=3000217||Add(d.EliteStartCoinBonus,3000210,3000212))
            &&(root!=3000208||Add(d.BossStartCoinBonus,3000213,3000215));
    }
}
