using System;
using Common.Database;
using System.Collections.Generic;
using System.Linq;
namespace PemukulPaku.GameServer.Game;
internal static partial class GodWarProgression
{
    // TextMap CN 278286695 explicitly requires the story stage and excludes floor16.
    // Do not use legacy MissionDataBuilder's all-closed compatibility projection
    // as proof of a real clear; never unlock story or grant rewards here.
    private static bool HasAdditionalStoryPortal(Player p,State s)=>
        s.StoryDone.Contains(665030)||s.StoryClaimed.Contains(665030)||
        MissionTake.Has(p.User.Uid,665030)||s.PlotClears.Contains(175279)||s.ClearedStages.Contains(175279);
    private static int DeepPortalChoiceCount(ChallengeRun run)=>
        run.AdditionalStoryPortal&&run.Deep.Floor+1!=16?4:3;
    private static IEnumerable<uint> DeepPortalPool(ChallengeRun run)
    {
        int egoCount = PlayableBuffRows().Count(row =>
            (uint)row["BuffSuit"] == 8 && run.Deep.Buffs.ContainsKey((uint)row["BuffID"]));
        return Table("GodWarTeleport")
            .Where(row => (uint)row["TeleportType"] == 1 && (uint)row["TeleportID"] is >= 101 and <= 113)
            .Select(row => (uint)row["TeleportID"])
            .Where(id => (id != 108 || egoCount < 4) && EligibleBuffs(run, PortalSuit(id)).Any());
    }

    private static void Portals(ChallengeRun run)
    {
        var deep = run.Deep;
        deep.Revision++;
        if (IsDeepBossChoice(run))
        {
            deep.Portals = DeepBossDoors();
        }
        else if (LeadsToRegularBoss(deep))
        {
            deep.Portals = new[] { RegularBossDoor };
        }
        else if (LeadsToFinalWeeklyShop(deep))
        {
            deep.Portals = new[] { 100u };
        }
        else
        {
            bool shop = NextWeeklyShop(deep) != null;
            int count = DeepPortalChoiceCount(run) - (shop ? 1 : 0);
            var ordinary = DrawUniform(DeepPortalPool(run), count);
            deep.Portals = shop ? ordinary.Prepend(100u).ToArray() : ordinary;
        }
        deep.Phase = "portal";
    }
}
