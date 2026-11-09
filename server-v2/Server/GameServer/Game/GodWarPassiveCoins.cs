using System;
using System.Linq;
using System.Globalization;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static partial class GodWarProgression
{
    // The actual Lua listens for GodWar_TwoExtraCoin / GodWar_TenExtraCoin
    // queued by native abilities and requests precisely these fixed events.
    // Hit/cooldown simulation remains native; amounts are never client supplied.
    private static uint DeepMerchantPhilosophyBonus(DeepState deep, uint baseSilver)
    {
        if (!deep.Buffs.TryGetValue(580105, out uint level))
            return 0;

        var row = Table("GodWarBuff").SingleOrDefault(item =>
            (uint)item["BuffID"] == 580105 && (uint)item["BuffLevel"] == level);
        var parameters = row?["ParamList"];
        if (parameters == null || parameters.Count() != 2 ||
            !decimal.TryParse((string)parameters[0], NumberStyles.Number, CultureInfo.InvariantCulture, out decimal rate) ||
            !uint.TryParse((string)parameters[1], out uint cap))
            return 0;

        return (uint)Math.Min(cap, decimal.Floor(((decimal)deep.Coins + baseSilver) * rate));
    }

    private static bool TriggerDeepPassiveCoin(Player p,State s,ChallengeRun run,uint tale,uint id,DeepEvents result)
    {
        var d=run.Deep;
        if(d==null||d.PendingStage==0||d.PendingStage!=d.Stages[d.Floor]||IsDeepRest(run)||d.Phase is not ("fighting" or "buff" or "portal" or "complete"))return false;
        var row=Table("GodWarEvent").Single(x=>(uint)x["EventID"]==id);
        uint expected=id==551001?2u:10u;
        if((uint)row["EventType"]!=43||!row["ParamsVar"].Values<uint>().SequenceEqual(new[]{expected,expected}))return false;
        if(!Commit(p,s,()=>{d.Coins=checked(d.Coins+expected);run.Challenge.GainCoinNum=checked(run.Challenge.GainCoinNum+expected);} ))return false;
        result.Changed=true;
        if(d.RoomCurrencyVersion==1)result.Events.Add(DeepCoinEvent(tale,id,expected));
        return true;
    }
}

