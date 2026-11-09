using System;
using System.Linq;
namespace PemukulPaku.GameServer.Game;
internal static partial class GodWarProgression
{
    // Both actual GodWar_WeeklyChallenge scripts classify precisely these four
    // floors as elites. Their false follow result means missing the timed bonus,
    // not losing the battle: GenerateTeleportPorts still sets the win flag.
    private static bool IsDeepWeeklyElite(ChallengeRun run)
    {
        var d=run.Deep;if(d==null||(uint)Site(d)["SiteIndex"] is not (4 or 7 or 11 or 15))return false;
        var stage=Table("StageData_Main").SingleOrDefault(x=>(uint)x["levelId"]==d.Stages[d.Floor]);
        return ((string)stage?["luaFile"])?.EndsWith("/GodWar_WeeklyChallenge.lua",StringComparison.Ordinal)==true;
    }
    // These exact four shallow templates require the original 80-second
    // GenerateEliteMonsters config, then still GenerateTeleportPorts on timeout.
    private static bool IsDeepTimedElite(ChallengeRun run)
    {
        if(IsDeepWeeklyElite(run))return true;
        var d=run.Deep;if(d==null)return false;
        var stage=Table("StageData_Main").SingleOrDefault(x=>(uint)x["levelId"]==d.Stages[d.Floor]);
        return (string)stage?["luaFile"] is
            "Lua/Levels/GodWar/GodWar_Battle_City_Elite.lua" or
            "Lua/Levels/GodWar/GodWar_Battle_Forest_Elite.lua" or
            "Lua/Levels/GodWar/GodWar_Battle_GreenSpace_Elite_Hard.lua" or
            "Lua/Levels/GodWar/GodWar_Battle_PinkStone_Elite_Hard.lua";
    }
    private static bool IsDeepEliteTimeout(ChallengeRun run,uint eventId)=>IsDeepTimedElite(run)&&eventId==(uint)Stage(run.Deep)["FailEventID"]&&eventId!=(uint)Stage(run.Deep)["FollowEventID"];
}
