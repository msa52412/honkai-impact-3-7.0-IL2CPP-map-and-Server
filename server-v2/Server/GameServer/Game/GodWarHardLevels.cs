using System;
using System.Linq;
namespace PemukulPaku.GameServer.Game;
internal static partial class GodWarProgression
{
    private static uint DeepHardLevel(ChallengeRun run,uint tale)
    {
        var d=run.Deep;if(d==null)return 0;
        // Weekly explicit boss-entry events 550012..550018 set 4017..4617.
        // Intermediate floors reconstruct that family with authored +1 steps;
        // extra boss uses the actual 4018..4618 rows. Floor16 is a noncombat
        // shop with no 4016..4616 row; retain the previous combat row there.
        // Shallow 1001/1101/1201 families are compatibility reconstruction:
        // exported conditional initialization data is incomplete.
        uint step=run.Challenge.StepLevel;
        uint code=tale==30&&step is >=1 and <=7
            ?4000+(step-1)*100+(uint)(d.Floor==15?15:d.Floor+1)
            :tale==20&&step is >=1 and <=3?1000+(step-1)*100+(uint)(d.Floor+1):0;
        if(code>0&&Table("GodWarHardLevel").Any(x=>(uint)x["HardLevelID"]==code))return code;
        // A historical/custom route may lack a reconstructed code. Use only
        // its actual authored stage default if that native hard-level row exists.
        uint fallback=(uint?)Table("StageData_Main").SingleOrDefault(x=>(uint)x["levelId"]==d.Stages[d.Floor])?["hardLevel"]??0;
        return fallback>0&&Table("GodWarHardLevel").Any(x=>(uint)x["HardLevelID"]==fallback)?fallback:1;
    }
}
