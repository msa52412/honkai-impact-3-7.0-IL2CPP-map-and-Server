using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static partial class GodWarProgression
{
    private sealed record WeeklyShop(uint Site,uint Floor,uint Stage,uint StageEvent,uint Period);
    private static readonly WeeklyShop[] WeeklyShops={
        new(6005,5,175640,560050151,0),new(6006,6,175641,560050161,0),
        new(6009,9,175642,560050191,1),new(6010,10,175643,560051101,1),
        new(6013,13,175644,560051131,2),new(6014,14,175645,560051141,2),
        new(6018,16,175650,560051241,3)
    };
    private static bool IsWeeklyShopBinding(WeeklyShop shop)
    {
        var site=Table("GodWarSite").Single(x=>(uint)x["SiteID"]==shop.Site);
        var stage=Table("StageData_Main").Single(x=>(uint)x["levelId"]==shop.Stage);
        var eventStage=Table("GodWarEventStage").Single(x=>(uint)x["StageID"]==shop.Stage);
        if((uint)site["SiteIndex"]!=shop.Floor||(uint)eventStage["StartCoinEventID"]!=0||!((string)stage["luaFile"]).EndsWith("/GodWar_WeeklyChallenge.lua",StringComparison.Ordinal))return false;
        var seen=new HashSet<uint>();var q=new Queue<uint>();q.Enqueue((uint)site["SiteEvent"]);
        while(q.Count>0&&seen.Count<100){uint id=q.Dequeue();if(!seen.Add(id))continue;var e=Table("GodWarEvent").Single(x=>(uint)x["EventID"]==id);if(id==shop.StageEvent)return (uint)e["EventType"]==1;if((uint)e["EventType"] is 99 or 100)foreach(uint child in e["ParamsVar"].Values<uint>())q.Enqueue(child);}
        return false;
    }
    private static WeeklyShop CurrentWeeklyShop(DeepState d)
    {
        if(d.RoomRulesVersion<4)return null;
        var shop=WeeklyShops.SingleOrDefault(x=>x.Site==d.Sites[d.Floor]&&x.Stage==d.Stages[d.Floor]);
        return shop!=null&&IsWeeklyShopBinding(shop)?shop:null;
    }
    private static WeeklyShop NextWeeklyShop(DeepState d)
    {
        if(d.RoomRulesVersion<4||d.Floor+1>=d.Sites.Length)return null;
        var shop=WeeklyShops.SingleOrDefault(x=>x.Site==d.Sites[d.Floor+1]&&
            (x.Period==3||!d.WeeklyShopVisits.Contains(x.Period)));
        // Exact source branch weights are not exported. Compatibility choice:
        // expose one authored shop door in either unused slot of each pair.
        return shop!=null&&IsWeeklyShopBinding(shop)?shop:null;
    }

    private static bool LeadsToFinalWeeklyShop(DeepState d)=>NextWeeklyShop(d)?.Period==3;
}
