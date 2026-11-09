using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PemukulPaku.GameServer.Game;

internal static partial class GodWarProgression
{
    private sealed record DeepBoss(uint Door,uint Selector,uint Stage,uint StageEvent,uint DoorEvent,uint ChoiceEvent,string Script);
    private static readonly DeepBoss[] DeepBosses={
        new(30002,1,175636,560050170,300114,5600066,"GodWar_Battle_PinkElysiaBoss_Story4.lua"),
        new(30003,2,175637,560050171,300115,5600067,"GodWar_Battle_GreenSpacePlus_Boss6.lua"),
        new(30004,3,175638,560050172,300116,5600068,"GodWar_Battle_Temple_Boss_Kalpas.lua"),
        new(30005,4,175639,560050173,300117,5600069,"GodWar_Battle_Temple_Boss_Aponia.lua"),
        new(30010,5,175654,560050174,300120,5600128,"GodWar_Battle_Temple_Boss_VillV.lua"),
        new(30011,6,175655,560050175,300121,5600137,"GodWar_Battle_Temple_Boss_Kosmo.lua"),
        new(30012,7,175656,560050176,300122,5600146,"GodWar_Battle_Temple_Boss_Kevin.lua")
    };
    private static bool IsDeepBossChoice(ChallengeRun run)=>run.Deep is {} d&&d.RoomRulesVersion>=2&&d.Sites[d.Floor]==6016&&d.Stages[d.Floor]==175635&&d.Floor+1<d.Sites.Length&&d.Sites[d.Floor+1]==6017;
    private static bool HasConfirmedDeepMainClear(ChallengeRun run)
    {
        var d=run.Deep;if(d==null||d.RoomRulesVersion<2)return false;
        if(d.RegularBossCleared)return true;
        // Saved v2/v3 challenges already in an issued optional boss room prove
        // that the preceding regular boss's StageEnd was confirmed previously.
        return d.Floor==17&&d.Sites.Length==18&&d.Stages.Length==18&&d.Sites[16]==6016&&d.Stages[16]==175635&&d.Sites[17]==6017&&DeepBosses.Any(x=>x.Selector==d.SelectedBoss&&x.Stage==d.Stages[17]);
    }
    private const uint RegularBossDoor = 301;

    private static bool LeadsToRegularBoss(DeepState deep) =>
        deep.Floor + 1 < deep.Sites.Length &&
        deep.Sites[deep.Floor + 1] == 6016 && deep.Stages[deep.Floor + 1] == 175635;

    private static uint[] DeepCurrentPortals(ChallengeRun run) =>
        LeadsToRegularBoss(run.Deep) ? new[] { RegularBossDoor } :
        LeadsToFinalWeeklyShop(run.Deep) ? new[] { 100u } :
        IsDeepBossChoice(run) ? DeepBossDoors() : run.Deep.Portals;
    private static uint[] DeepBossDoors()
    {
        // Validate exact authored door -> choice variable -> scene bindings.
        // Do not execute unrelated group children (story, rewards, or flags).
        var site=Table("GodWarSite").Single(x=>(uint)x["SiteID"]==6017);
        var group=Table("GodWarEvent").Single(x=>(uint)x["EventID"]==(uint)site["SiteEvent"]);
        if((uint)group["EventType"]!=99)throw new InvalidOperationException("GodWar final room group changed");
        foreach(var boss in DeepBosses)
        {
            var door=Table("GodWarTeleport").Single(x=>(uint)x["TeleportID"]==boss.Door);
            var root=Table("GodWarEvent").Single(x=>(uint)x["EventID"]==boss.DoorEvent);
            var choice=Table("GodWarEvent").Single(x=>(uint)x["EventID"]==boss.ChoiceEvent);
            var stage=Table("StageData_Main").Single(x=>(uint)x["levelId"]==boss.Stage);
            var enter=Table("GodWarEvent").Single(x=>(uint)x["EventID"]==boss.StageEvent);
            if((uint)door["TeleportType"]!=3||(uint)door["TeleportEvent"]!=boss.DoorEvent||(uint)root["EventType"]!=99||!root["ParamsVar"].Values<uint>().Contains(boss.ChoiceEvent)||(uint)choice["EventType"]!=30||!choice["ParamsVar"].Values<uint>().SequenceEqual(new uint[]{611,boss.Selector})||!group["ParamsVar"].Values<uint>().Contains(boss.StageEvent)||(uint)enter["EventType"]!=1||!((string)stage["luaFile"]).EndsWith("/"+boss.Script,StringComparison.Ordinal))
                throw new InvalidOperationException("GodWar boss binding changed: "+boss.Door);
        }
        return DeepBosses.Select(x=>x.Door).ToArray();
    }
}
