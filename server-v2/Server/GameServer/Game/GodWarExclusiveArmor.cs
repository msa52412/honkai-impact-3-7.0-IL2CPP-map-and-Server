using System;
using System.Collections.Generic;
using System.Linq;

namespace PemukulPaku.GameServer.Game;

internal static partial class GodWarProgression
{
    // Matched the 7.0 armor skill descriptions to the original signet names.
    // The four late suits with missing skill descriptions only upgrade at max.
    private static readonly Dictionary<uint,uint> ExclusiveArmorThresholds = new()
    {
            // Armor level 3
            [10002] = 3, [10101] = 3, [10204] = 3, [10301] = 3, [10401] = 3, [10501] = 3, [10601] = 3,
            [10701] = 3, [10801] = 3, [10901] = 3, [11003] = 3, [11101] = 3, [11201] = 3, [11301] = 3,
            [11401] = 3, [11501] = 3, [11601] = 3, [11701] = 3, [11801] = 3, [11901] = 3, [12003] = 3,
            [12101] = 3, [12201] = 3, [12301] = 3, [12401] = 3, [12501] = 3, [12601] = 3, [12701] = 3,
            [12801] = 3, [12901] = 3, [13001] = 3, [13101] = 3, [13201] = 3, [13301] = 3, [13401] = 3,
            [13501] = 3, [13601] = 3, [14004] = 3, [15004] = 3, [16003] = 3, [17001] = 3, [18001] = 3,
            [19004] = 3,
            // Armor level 6
            [10001] = 6, [10102] = 6, [10201] = 6, [10302] = 6, [10402] = 6, [10502] = 6, [10602] = 6,
            [10702] = 6, [10802] = 6, [10902] = 6, [11002] = 6, [11102] = 6, [11202] = 6, [11302] = 6,
            [11402] = 6, [11502] = 6, [11602] = 6, [11702] = 6, [11802] = 6, [11902] = 6, [12004] = 6,
            [12102] = 6, [12202] = 6, [12302] = 6, [12402] = 6, [12502] = 6, [12602] = 6, [12702] = 6,
            [12802] = 6, [12902] = 6, [13003] = 6, [13102] = 6, [13202] = 6, [13302] = 6, [13402] = 6,
            [13502] = 6, [13602] = 6, [14002] = 6, [15003] = 6, [16001] = 6, [17002] = 6, [18002] = 6,
            [19005] = 6,
            // Armor level 9
            [10003] = 9, [10105] = 9, [10202] = 9, [10303] = 9, [10403] = 9, [10503] = 9, [10603] = 9,
            [10703] = 9, [10803] = 9, [10903] = 9, [11004] = 9, [11103] = 9, [11203] = 9, [11303] = 9,
            [11403] = 9, [11503] = 9, [11603] = 9, [11703] = 9, [11803] = 9, [11903] = 9, [12002] = 9,
            [12103] = 9, [12203] = 9, [12303] = 9, [12403] = 9, [12503] = 9, [12603] = 9, [12703] = 9,
            [12803] = 9, [12903] = 9, [13004] = 9, [13103] = 9, [13203] = 9, [13303] = 9, [13403] = 9,
            [13503] = 9, [13603] = 9, [14001] = 9, [15002] = 9, [16005] = 9, [17005] = 9, [18004] = 9,
            [19001] = 9,
            // Armor level 12
            [10004] = 12, [10103] = 12, [10203] = 12, [10304] = 12, [10404] = 12, [10504] = 12, [10604] = 12,
            [10704] = 12, [10804] = 12, [10904] = 12, [11005] = 12, [11104] = 12, [11204] = 12, [11304] = 12,
            [11404] = 12, [11504] = 12, [11604] = 12, [11704] = 12, [11804] = 12, [11904] = 12, [12001] = 12,
            [12104] = 12, [12204] = 12, [12304] = 12, [12404] = 12, [12504] = 12, [12604] = 12, [12704] = 12,
            [12804] = 12, [12904] = 12, [13002] = 12, [13104] = 12, [13204] = 12, [13304] = 12, [13404] = 12,
            [13504] = 12, [13604] = 12, [14005] = 12, [15005] = 12, [16002] = 12, [17003] = 12, [18005] = 12,
            [19002] = 12,
            // Armor level 15
            [10005] = 15, [10104] = 15, [10205] = 15, [10305] = 15, [10405] = 15, [10505] = 15, [10605] = 15,
            [10705] = 15, [10805] = 15, [10905] = 15, [11001] = 15, [11105] = 15, [11205] = 15, [11305] = 15,
            [11405] = 15, [11505] = 15, [11605] = 15, [11705] = 15, [11805] = 15, [11905] = 15, [12005] = 15,
            [12105] = 15, [12205] = 15, [12305] = 15, [12405] = 15, [12505] = 15, [12605] = 15, [12705] = 15,
            [12805] = 15, [12905] = 15, [13005] = 15, [13105] = 15, [13205] = 15, [13305] = 15, [13405] = 15,
            [13505] = 15, [13605] = 15, [13701] = 15, [13702] = 15, [13703] = 15, [13704] = 15, [13705] = 15,
            [13801] = 15, [13802] = 15, [13803] = 15, [13804] = 15, [13805] = 15, [13901] = 15, [13902] = 15,
            [13903] = 15, [13904] = 15, [13905] = 15, [14003] = 15, [14101] = 15, [14102] = 15, [14103] = 15,
            [14104] = 15, [14105] = 15, [15001] = 15, [16004] = 15, [17004] = 15, [18003] = 15, [19003] = 15,
    };

    private static uint ExclusiveVariant(ChallengeRun run,uint baseId)
    {
        if (ExclusiveArmorThresholds.TryGetValue(baseId,out uint level) &&
            run.Deep.ExclusiveArmorLevel>=level &&
            PlayableBuffRows().Any(row=>(uint)row["BuffID"]==baseId+10000 && (uint)row["BuffQuality"]==4))
            return baseId+10000;
        return baseId;
    }

    private static void ApplyExclusiveArmor(State state,ChallengeRun run)
    {
        var d=run.Deep;
        var avatar=run.Challenge.AvatarLists.SingleOrDefault();
        if (avatar==null) return;
        var role=Table("GodWarRelationData").FirstOrDefault(row=>(uint)row["GodWarID"]==1 &&
            (uint)row["RoleType"]==2 && (uint)row["AvatarID"]==avatar.AvatarId);
        if (role!=null && state.Roles.TryGetValue((uint)role["RoleID"],out var progress))
            d.ExclusiveArmorLevel=progress.Level;
        var baseIds=PlayableBuffRows().Where(row=>(uint)row["BuffQuality"]==4 &&
            (uint)row["BuffGroup"]==avatar.AvatarId && (uint)row["BuffID"]<20000)
            .Select(row=>(uint)row["BuffID"]).ToHashSet();
        foreach (uint id in d.Buffs.Keys.Where(baseIds.Contains).ToArray())
        {
            uint variant=ExclusiveVariant(run,id);
            if (variant==id) continue;
            d.Buffs[variant]=Math.Max(d.Buffs[id],d.Buffs.GetValueOrDefault(variant));
            d.Buffs.Remove(id);
        }
        d.Candidates=d.Candidates.Select(id=>baseIds.Contains(id)?ExclusiveVariant(run,id):id).Distinct().ToArray();
    }
}
