using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PemukulPaku.GameServer.Game;

// Chapter/sequence schedules remain distinct. Once the original 7.0 archive
// expires, cycle its six authored weeks for offline play; this repeating
// extension is not an assertion that official servers repeat old versions.
internal static class GodWarCalendar
{
    private static long Time(JToken row,string field) => (long?)row?[field]?["TimeStampForBakedReader"] ?? 0;
    internal static JObject AvatarSchedule(uint tale,JArray schedules,long now)
    {
        uint fixedId=tale switch {10=>10001,11=>11001,12=>12001,20=>20000,_=>0};
        if(fixedId!=0)return schedules.OfType<JObject>().SingleOrDefault(x=>(uint)x["AvatarScheduleID"]==fixedId);
        if(tale!=30)return null; // Lobby tales have no combat avatar schedule.
        // Original 7.0 archive: first three weeks use1431, next three1432.
        // Keep historical timestamps intact for evidence and preserve every
        // old schedule ID so an in-flight run keeps its issued setup.
        var cycle=Enumerable.Range(30083,6).Select(id=>schedules.OfType<JObject>()
            .SingleOrDefault(x=>(uint)x["AvatarScheduleID"]==(uint)id)).ToArray();
        if(cycle.All(x=>x!=null)&&now>cycle.Max(x=>Time(x,"EndTime")))
        {
            // CN weekly rollover is Monday04:00; entry remains closed until
            //10:00 through the existing EntryWindow, independently of the
            // archived overseas timestamps. Phase starts Sep18,2023 CN04:00.
            const long anchor=1694980800,weekSeconds=604800;
            long week=(now-anchor)/weekSeconds;
            if(now<anchor&&(now-anchor)%weekSeconds!=0)week--;
            int index=(int)((week%cycle.Length+cycle.Length)%cycle.Length);
            return cycle[index];
        }
        var candidates=schedules.OfType<JObject>().Where(x=>(uint)x["AvatarScheduleID"]>=30000 && Time(x,"BeginTime")<=now).ToArray();
        return candidates.Where(x=>Time(x,"EndTime")>=now)
            .OrderByDescending(x=>Time(x,"BeginTime")).ThenByDescending(x=>(uint)x["AvatarScheduleID"]).FirstOrDefault()
            ?? candidates.OrderByDescending(x=>Time(x,"BeginTime")).ThenByDescending(x=>(uint)x["AvatarScheduleID"]).FirstOrDefault();
    }
    internal static JObject TaleSchedule(uint tale,JArray schedules,long now)
    {
        // Unscheduled/debug tales and retired pre-chapter tale 1 are not
        // advertised as playable current modes. Permanent chapter/lobby tales
        // remain available after their baked end dates in this offline server.
        if(tale==1 || tale==99)return null;
        return schedules.OfType<JObject>().Where(x=>x["TaleIDList"].Values<uint>().Contains(tale) && Time(x,"BeginTime")<=now)
            .OrderByDescending(x=>Time(x,"BeginTime")).ThenByDescending(x=>(uint)x["TaleScheduleID"]).FirstOrDefault();
    }
}
