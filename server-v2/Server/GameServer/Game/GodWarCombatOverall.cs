using Common;
using System;
using System.Collections.Generic;
using System.Linq;
namespace PemukulPaku.GameServer.Game;
internal static partial class GodWarProgression
{
    // Local LevelDesignGodWarCommon and GodWar_GodAbility_Config, both shipped
    // hotfix variants. IDs are isolated from server-owned coins, boss and progress.
    private static bool IsDeepCombatOverall(uint id,uint value)=>id switch
    {
        612=>value<=600, // Support extra energy uses a 600-unit scale.
        613=>value<=10,  // KalpasMaxStack in the support ability script.
        614 or 615=>value<=5, // Server-authorized slot prizes, not client checkpoints.
        617 or 717 or 721 or 722=>value<=1,
        619 or 620 or 621 or 623 or 624 or 719 or 720 or 723 or 724 or 725 or 726 or 729=>value<=int.MaxValue,
        _=>false
    };
    private static bool SaveDeepCombatOverall(Player p,State s,ChallengeRun run,uint eventId,uint[] args,out bool changed)
    {
        changed=false;var d=run.Deep;
        if(d==null||d.PendingStage==0||d.PendingStage!=d.Stages[d.Floor]||d.Phase is not ("fighting" or "buff" or "portal" or "complete"))return false;
        if(args==null||args.Length==0||args.Length%2!=0||args.Length>36||(eventId==551004&&args.Length!=2))return false;
        var incoming=new Dictionary<uint,uint>();
        for(int i=0;i<args.Length;i+=2)
        {
            uint id=args[i],value=args[i+1];
            bool permitted=eventId==551003
                ?id is 612 or 613 or 619 or 620 or 621 or 623 or 624 or 719 or 720 or 724 or 725 or 726 or 729
                :id is 612 or 717 or 721 or 722 or 723;
            if(!permitted||!IsDeepCombatOverall(id,value)||!incoming.TryAdd(id,value))return false;
        }
        if(incoming.All(x=>d.CombatOverall.TryGetValue(x.Key,out uint old)&&old==x.Value))return true;
        // These validated Lua counters touch only the run state. Saving the
        // full inventory/user here made every checkpoint serialize unrelated
        // weapons, stigmata and skills. Keep the same durable transaction and
        // response; restore the local dictionary as well if persistence fails.
        _=Store;
        var previous=incoming.Keys.ToDictionary(id=>id,id=>(Present:d.CombatOverall.ContainsKey(id),Value:d.CombatOverall.GetValueOrDefault(id)));
        try
        {
            Global.Store.InTransaction(()=>
            {
                foreach(var pair in incoming)d.CombatOverall[pair.Key]=pair.Value;
                Store.ReplaceOne(s);
            });
        }
        catch(Exception ex)
        {
            foreach(var pair in previous)
                if(pair.Value.Present)d.CombatOverall[pair.Key]=pair.Value.Value;
                else d.CombatOverall.Remove(pair.Key);
            Packet.c.Warn("[GodWar] combat checkpoint rolled back: "+ex.Message);
            return false;
        }
        changed=true;return true;
    }
}
