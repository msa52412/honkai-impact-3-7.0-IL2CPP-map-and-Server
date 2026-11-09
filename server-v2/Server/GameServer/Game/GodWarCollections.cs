using System;
using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Newtonsoft.Json.Linq;

namespace PemukulPaku.GameServer.Game;

internal static partial class GodWarProgression
{
    private static HashSet<uint> CollectionPhotoCache;
    private static Dictionary<uint,JToken> CollectionEventCache;
    private static readonly Dictionary<uint,uint[]> CollectionRootCache=new();
    private static HashSet<uint> CollectionPhotos=>CollectionPhotoCache??=Table("CityEventPhoto").Where(x=>(uint)x["photoType"]==12).Select(x=>(uint)x["PhotoID"]).ToHashSet();
    private static uint[] PureCollectionPhotos(uint root)
    {
        if(CollectionRootCache.TryGetValue(root,out var cached))return cached;
        CollectionEventCache??=Table("GodWarEvent").ToDictionary(x=>(uint)x["EventID"],x=>x);
        var queue=new Queue<uint>();queue.Enqueue(root);var visited=new HashSet<uint>();var photos=new HashSet<uint>();bool valid=true;
        while(queue.Count>0)
        {
            uint id=queue.Dequeue();if(!visited.Add(id))continue;
            if(visited.Count>512||!CollectionEventCache.TryGetValue(id,out var row)){valid=false;break;}
            uint kind=(uint)row["EventType"];
            if(kind==99){foreach(uint child in row["ParamsVar"].Values<uint>())queue.Enqueue(child);continue;}
            if(kind is 8 or 10 or 20)continue;
            if(kind!=17){valid=false;break;}
            foreach(uint photo in row["ParamsVar"].Values<uint>()){if(!CollectionPhotos.Contains(photo)){valid=false;break;}photos.Add(photo);}
            if(!valid)break;
        }
        return CollectionRootCache[root]=valid?photos.OrderBy(x=>x).ToArray():Array.Empty<uint>();
    }
    // Called inside the same inventory/progression transaction as acquisition.
    // Catalogue membership alone and synthetic all-closed missions are not
    // ownership. Once earned, an archive entry survives finishing a run.
    private static bool RememberCollections(Player p,State state,bool includeReceipts=true)
    {
        int before=state.Collections.Count,seenBefore=state.SeenBuffs.Count;
        foreach(var run in state.Runs.Values)if(run.Deep!=null)state.SeenBuffs.UnionWith(run.Deep.Buffs.Keys);
        foreach(var row in Table("CityEventPhoto").Where(x=>(uint)x["photoType"]==12&&(bool)x["isDefault"]))state.Collections.Add((uint)row["PhotoID"]);
        var meta=Table("GodWarCollectionData");
        foreach(var row in meta.Where(x=>(uint)x["GodWarCollectionType"]==3&&state.SeenBuffs.Contains((uint)x["BuffID"])))state.Collections.Add((uint)row["CollectionID"]);
        var held=p.Equipment.MaterialList.Where(x=>x.Num>0).Select(x=>x.Id).ToHashSet();
        foreach(var item in Table("GodWarExtraItem").Where(x=>(uint)x["GodWarID"]==1&&held.Contains((uint)x["ExtraItemID"])))
        foreach(var row in meta.Where(x=>(uint)x["GodWarCollectionType"]==5&&(long)x["LockName"]["hash"]==(long)item["ExtraItemName"]["hash"]&&(uint)x["CollectionStarNum"]==(uint)item["ExtraItemStar"]))state.Collections.Add((uint)row["CollectionID"]);
        var completed=state.StoryDone.Concat(state.StoryClaimed).Concat(includeReceipts?MissionTake.TakenIds(p.User.Uid):Array.Empty<uint>()).ToHashSet();
        foreach(var action in Table("GodWarLobbyInteractAction").Where(x=>(uint)x["ActionType"]==1&&x["ActionParam"].Any()&&completed.Contains((uint)x["DisplayMission"])))state.Collections.UnionWith(PureCollectionPhotos((uint)action["ActionParam"][0]));
        return before!=state.Collections.Count||seenBefore!=state.SeenBuffs.Count;
    }
    internal static GetPhotoDataRsp CollectionsReply(Player p)
    {
        lock(p.Equipment)lock(Gate)
        {
            var response=new GetPhotoDataRsp{Type=12,PhotoIdLists=Array.Empty<uint>()};var state=Load(p.User);
            if(RememberCollections(p,state)&&!Commit(p,state,()=>{})){response.retcode=GetPhotoDataRsp.Retcode.Fail;return response;}
            response.PhotoIdLists=state.Collections.Where(CollectionPhotos.Contains).OrderBy(x=>x).ToArray();return response;
        }
    }
    internal static uint[] FilterCollectionPhotos(uint[] ids,GetPhotoDataRsp earned)
    {
        lock(Gate){return (ids??Array.Empty<uint>()).Where(x=>!CollectionPhotos.Contains(x)).Concat(earned.retcode==0?earned.PhotoIdLists??Array.Empty<uint>():Array.Empty<uint>()).Distinct().ToArray();}
    }
    internal static void PushCollections(Session s)=>s.Send(Packet.FromProto(CollectionsReply(s.Player),CmdId.GetPhotoDataRsp));
}
