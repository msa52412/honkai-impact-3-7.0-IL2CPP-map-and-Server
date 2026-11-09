using System;
using System.Linq;
namespace PemukulPaku.GameServer.Game;
internal static partial class GodWarProgression
{
    private static uint DeepSelectedBuffLevel(ChallengeRun run,uint buff)
    {
        var row=PlayableBuffRows().Single(x=>(uint)x["BuffID"]==buff);
        uint suit=(uint)row["BuffSuit"],bonus=0;
        foreach(uint item in new[]{run.Challenge.ExtraItemId,run.Challenge.AssistantExtraItemId})
        {
            // These item descriptions explicitly grant +1 on acquisition.
            // Resolve119320 and Elysia119332 do not contain that rule.
            bool grantsLevel=item is >=119314 and <=119319 or >=119331 and <=119348 && item!=119332;
            if(grantsLevel&&Table("GodWarExtraItem").Any(x=>(uint)x["ExtraItemID"]==item&&(uint)x["ExtraItemSuitID"]==suit))bonus++;
        }
        // Original7.0 environment2433 grants +2 to normal Helix/Stars;
        //2449 grants +2 to normal Bodhi/Setsuna. These are acquisition
        // rules with no ability payload. Core/enhanced/exclusive are excluded.
        // Use the run's issued environment, so a rollover cannot change an
        // in-flight run. Existing owned levels are never re-granted here.
        if((uint)row["BuffQuality"]==1&&
            (((run.Challenge.PunishBuffIdLists?.Contains(2433u)??false)&&suit is 11 or 13)||
             ((run.Challenge.PunishBuffIdLists?.Contains(2449u)??false)&&suit is 4 or 5)))bonus+=2;
        // The server issues the authoritative level in the choice ParamMap,
        // shop BuffInitLevel and owned BuffLists. Do not re-add it on upgrades.
        // Core/exclusive rows with no higher authored level remain at level1.
        return Table("GodWarBuff").Where(x=>(uint)x["BuffID"]==buff&&(uint)x["BuffLevel"]<=1+bonus)
            .Max(x=>(uint)x["BuffLevel"]);
    }
    private static bool IsDeepSupportEvent(uint id)=>id is 5600073 or 5600106 or 5600099 or 5600100 or 5600102 or 5600110 or 5600103 or 5600104 or 5600107 or 5600108;
    private static bool TriggerDeepSupport(Player p,State s,ChallengeRun run,uint id,DeepEvents result)
    {
        var d=run.Deep;
        if(d==null||d.PendingStage==0||d.PendingStage!=d.Stages[d.Floor])return false;
        // Both scripts read the carried slot stacks, attach the corresponding
        // abilities, then request these resets once at room initialization.
        if(id is 5600107 or 5600108)
        {
            if(d.Phase is not ("fighting" or "portal")||d.SlotSpinsInRoom!=0)return false;
            uint mask=id==5600107?1u:2u,overall=id==5600107?614u:615u;
            if((d.SlotResetMask&mask)!=0)return true;
            if(!Commit(p,s,()=>{d.CombatOverall[overall]=0;d.SlotResetMask|=mask;}))return false;
            result.Changed=true;return true;
        }
        if(d.Phase!="portal")return false;
        if(id==5600073)
        {
            if(run.Challenge.AssistantExtraItemId!=119332||d.ElysiaPortalUsed||IsDeepBossChoice(run)||d.Portals.Length==0)return false;
            // Actual charge lives in the client's ability actor between room
            // checkpoints. Keep that native charge gate; this server check
            // enforces equipment, phase and once-per-room replay protection.
            var ports=d.Portals.Length;
            if(!Commit(p,s,()=>{
                d.ElysiaPortalUsed=true;d.CombatOverall[612]=0;Portals(run);
                int ego=d.Buffs.Keys.Count(buff=>(uint)PlayableBuffRows().Single(x=>(uint)x["BuffID"]==buff)["BuffSuit"]==8);
                if(ego<4&&run.Challenge.ExtraItemId!=119321&&EligibleBuffs(run,8).Any()){
                    var ordinary=d.Portals.Where(x=>x!=108).ToList();ordinary.Insert(0,108);d.Portals=ordinary.Take(ports).ToArray();
                }
            }))return false;
            result.Changed=true;result.Events.Add(Event(10011,DeepCurrentPortals(run)));return true;
        }
        if(IsDeepRest(run)||run.Challenge.AssistantExtraItemId!=119343)return false;
        if(id==5600106)
        {
            long stamp=Clock().ToUnixTimeMilliseconds();
            if(d.SlotExploded||d.Coins<100||(d.SlotSpinsInRoom>0&&stamp-d.SlotLastCostAt<4500))return false;
            if(!Commit(p,s,()=>{d.Coins-=100;d.SlotPaid=true;d.SlotSpinsInRoom++;d.SlotLastCostAt=stamp;}))return false;
        }
        else if(id==5600110)
        {
            if(!d.SlotJackpotPending)return false;
            if(!Commit(p,s,()=>{d.SlotJackpotPending=false;d.Coins=checked(d.Coins+999);run.Challenge.GainCoinNum=checked(run.Challenge.GainCoinNum+999);} ))return false;
        }
        else
        {
            if(!d.SlotPaid||d.SlotExploded)return false;
            if(!Commit(p,s,()=>{
                d.SlotPaid=false;
                if(id is 5600099 or 5600100){uint overall=id==5600099?614u:615u;d.CombatOverall[overall]=Math.Min(5,d.CombatOverall.GetValueOrDefault(overall)+1);}
                else if(id==5600102){d.SlotExploded=true;d.SlotJackpotPending=true;d.CombatOverall[617]=1;}
                else {uint coins=id==5600103?10u:200u;d.Coins=checked(d.Coins+coins);run.Challenge.GainCoinNum=checked(run.Challenge.GainCoinNum+coins);}
            }))return false;
        }
        // The Lua slot variable and native general coin delta are distinct.
        // Keep the room base fixed and notify the real native delta exactly once.
        result.Changed=true;
        if(d.RoomCurrencyVersion==1){
            uint amount=id==5600106?100u:id==5600110?999u:id==5600103?10u:id==5600104?200u:0;
            if(amount>0)result.Events.Add(DeepCoinEvent(result.Reply.TaleId,id,amount));
        }
        return true;
    }
}
