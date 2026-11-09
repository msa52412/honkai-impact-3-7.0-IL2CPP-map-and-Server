using System;
using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json.Linq;
using RewardData=Common.Resources.Proto.RewardData;

namespace PemukulPaku.GameServer.Game;

// Lobby layouts use authored refresh-event combinations. Missing branch flags
// are reconstructed from the current action/mission graph; not official logic.
internal static partial class GodWarProgression
{
    public sealed class LobbyDialogue
    {
        public uint Lobby {get;set;}
        public uint Tale {get;set;}
        public uint Root {get;set;}
        public uint Action {get;set;}
        public uint Leaf {get;set;}
        public uint Plot {get;set;}
        public uint[] Follow {get;set;}=Array.Empty<uint>();
        public long Since {get;set;}
    }
    private static uint CurrentLobby(State s)=>(uint)Table("GodWarChapter").Single(x=>(uint)x["ChapterID"]==s.Chapter)["LobbyID"];
    private static uint LobbyTale(uint lobby)=>(uint)Table("GodWarLobbyData").Single(x=>(uint)x["LobbyID"]==lobby)["TaleID"];
    internal static bool IsLobbyTale(uint tale)=>Table("GodWarLobbyData").Any(x=>(uint)x["TaleID"]==tale);
    private static uint LobbyStart(uint lobby)
    {
        uint scene=Table("GodWarLobbyData").Single(x=>(uint)x["LobbyID"]==lobby)["SceneConfigIDList"].Values<uint>().Single();
        return (uint)Table("GodWarSceneConfig").Single(x=>(uint)x["SceneConfigID"]==scene)["StartEventGroup"];
    }
    private static readonly Dictionary<uint,JToken[]> LobbyLayouts=new();
    private static Dictionary<uint,JToken> LobbyEvents;
    private static IEnumerable<JToken> EventLeaves(uint root)
    {
        LobbyEvents??=Table("GodWarEvent").ToDictionary(x=>(uint)x["EventID"],x=>x);
        var q=new Queue<uint>();q.Enqueue(root);var seen=new HashSet<uint>();
        while(q.Count>0&&seen.Count<16384){uint id=q.Dequeue();if(!seen.Add(id))continue;var row=LobbyEvents.GetValueOrDefault(id);if(row==null)continue;if((uint)row["EventType"] is 99 or 100){foreach(uint child in row["ParamsVar"].Values<uint>())q.Enqueue(child);}else yield return row;}
    }
    private static Dictionary<uint,MissionStatus> StoryStatuses(Player p)=>MissionDataBuilder.Build(p).MissionLists.GroupBy(x=>x.MissionId).ToDictionary(x=>x.Key,x=>x.First().Status);
    private static bool Complete(uint id,State s,Dictionary<uint,MissionStatus> statuses)=>id==0||s.StoryDone.Contains(id)||s.StoryClaimed.Contains(id)||statuses.GetValueOrDefault(id) is MissionStatus.MissionFinish or MissionStatus.MissionClose;
    private static bool ActionEnabled(JToken action,State s,Dictionary<uint,MissionStatus> statuses)
    {
        if(action==null||action["UnlockMission"].Values<uint>().Any(x=>!Complete(x,s,statuses)))return false;
        uint deleted=(uint)action["DeleteMission"];if(deleted!=0&&Complete(deleted,s,statuses))return false;
        uint display=(uint)action["DisplayMission"];
        // Completed navigation remains usable; story dialogue actions retire
        // when their display mission finishes.
        return (uint)action["ActionType"]==2||display==0||(!Complete(display,s,statuses)&&statuses.GetValueOrDefault(display)==MissionStatus.MissionDoing);
    }
    private static uint[] NpcActions(uint lobby,uint npc,State s,Dictionary<uint,MissionStatus> statuses)=>Table("GodWarLobbyInteractProp")
        .Where(x=>(uint)x["LobbyID"]==lobby&&(uint)x["Type"]==2&&(uint)x["PropID"]==npc).SelectMany(x=>x["ActionIDList"].Values<uint>())
        .Where(id=>ActionEnabled(Table("GodWarLobbyInteractAction").SingleOrDefault(x=>(uint)x["ActionID"]==id),s,statuses)).Distinct().ToArray();
    private static JToken Layout(uint lobby,State s,Dictionary<uint,MissionStatus> statuses)
    {
        if(!LobbyLayouts.TryGetValue(lobby,out var candidates))LobbyLayouts[lobby]=candidates=EventLeaves(LobbyStart(lobby)).Where(x=>(uint)x["EventType"]==21&&x["ParamsVar"].Any()).Where(x=>{
            var actors=x["ParamsVar"].Values<uint>().ToArray();
            var rows=actors.Select(id=>Table("GodWarLobbyAvatarData").SingleOrDefault(n=>(uint)n["LobbyCharID"]==id)).ToArray();
            return rows.All(n=>n!=null)&&rows.Select(n=>((uint)n["PositionID"],(uint)n["SubPositionID"])).Distinct().Count()==rows.Length&&actors.All(id=>Table("GodWarLobbyInteractProp").Any(p=>(uint)p["LobbyID"]==lobby&&(uint)p["PropID"]==id&&(uint)p["Type"]==2));
        }).ToArray();
        // Keep one complete authored combination. Never merge alternate story
        // branches into simultaneous actors at the same spawn point.
        return candidates.OrderBy(x=>{
            var active=x["ParamsVar"].Values<uint>().SelectMany(id=>NpcActions(lobby,id,s,statuses)).Select(id=>Table("GodWarLobbyInteractAction").Single(a=>(uint)a["ActionID"]==id))
                .Select(a=>(uint)a["DisplayMission"]).Where(id=>id!=0&&!Complete(id,s,statuses)).ToArray();
            return active.Length==0?uint.MaxValue:active.Min();
        }).FirstOrDefault();
    }
    internal static GetGodWarLobbyRsp Lobby(Player p,GetGodWarLobbyReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new GetGodWarLobbyRsp{GodWarId=req.GodWarId,LobbyId=req.LobbyId};
            if(req.GodWarId!=1){rsp.retcode=GetGodWarLobbyRsp.Retcode.FeatureClose;return rsp;}var s=Load(p.User);
            if(req.LobbyId!=CurrentLobby(s)){rsp.retcode=GetGodWarLobbyRsp.Retcode.NotOpen;return rsp;}
            var statuses=StoryStatuses(p);var layout=Layout(req.LobbyId,s,statuses);
            if(layout==null){rsp.retcode=GetGodWarLobbyRsp.Retcode.Fail;return rsp;}
            foreach(uint id in layout["ParamsVar"].Values<uint>())rsp.NpcLists.Add(new GodWarNpc{NpcId=id,ActionLists=NpcActions(req.LobbyId,id,s,statuses)});
            return rsp;
        }
    }
    private static bool ScopedAction(uint lobby,uint id,JToken layout)=>Table("GodWarLobbyInteractProp").Any(x=>(uint)x["LobbyID"]==lobby&&x["ActionIDList"].Values<uint>().Contains(id)&&
        ((uint)x["Type"]==1||(layout!=null&&layout["ParamsVar"].Values<uint>().Contains((uint)x["PropID"]))));
    private static IEnumerable<uint> MissionsForEvent(uint id)=>BaseExcelReader<MissionData,MissionDataExcel>.GetInstance().All.Where(x=>x.FinishWay==10673&&x.FinishParaInt==1&&x.FinishParaStr==id.ToString()).Select(x=>(uint)x.Id);
    internal static FinishGodWarLobbyActionRsp FinishLobbyAction(Player p,FinishGodWarLobbyActionReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new FinishGodWarLobbyActionRsp{GodWarId=req.GodWarId,LobbyId=req.LobbyId,ActionId=req.ActionId};
            if(req.GodWarId!=1){rsp.retcode=FinishGodWarLobbyActionRsp.Retcode.FeatureClose;return rsp;}var s=Load(p.User);
            if(req.LobbyId!=CurrentLobby(s)){rsp.retcode=FinishGodWarLobbyActionRsp.Retcode.NotOpen;return rsp;}
            var statuses=StoryStatuses(p);var layout=Layout(req.LobbyId,s,statuses);var action=Table("GodWarLobbyInteractAction").SingleOrDefault(x=>(uint)x["ActionID"]==req.ActionId);
            bool previouslyIssued=s.LobbyDialogue?.Lobby==req.LobbyId&&s.LobbyDialogue.Action==req.ActionId&&Clock().ToUnixTimeSeconds()-s.LobbyDialogue.Since is >=0 and <=21600;
            if(action==null||(!ScopedAction(req.LobbyId,req.ActionId,layout)&&!previouslyIssued)||(!ActionEnabled(action,s,statuses)&&!previouslyIssued)){rsp.retcode=FinishGodWarLobbyActionRsp.Retcode.NotOpen;return rsp;}
            if(!Commit(p,s,()=>{
                s.LobbyActions.Add(req.LobbyId+"/"+req.ActionId);
                // Native b__101_1 reports the interaction separately from the
                // type-2 trigger sent for dialogue. Only way674 is completed by
                // this request; a dialogue click cannot complete way10673.
                if((uint)action["ActionType"]==2)foreach(var m in BaseExcelReader<MissionData,MissionDataExcel>.GetInstance().All.Where(x=>x.FinishWay==674&&x.FinishParaInt==req.LobbyId&&x.FinishParaStr==req.ActionId.ToString()))if(IsStoryMission((uint)m.Id)&&statuses.GetValueOrDefault((uint)m.Id)==MissionStatus.MissionDoing)s.StoryDone.Add((uint)m.Id);
            }))rsp.retcode=FinishGodWarLobbyActionRsp.Retcode.Fail;
            return rsp;
        }
    }
    internal static DeepEvents TriggerLobby(Player p,TriggerGodWarEventReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var result=new DeepEvents{Reply=new TriggerGodWarEventRsp{GodWarId=req.GodWarId,TaleId=req.TaleId,EventIdLists=Array.Empty<uint>(),retcode=TriggerGodWarEventRsp.Retcode.Fail}};
            if(req.GodWarId!=1){result.Reply.retcode=TriggerGodWarEventRsp.Retcode.FeatureClose;return result;}var ids=req.EventIdLists??Array.Empty<uint>();if(ids.Length!=1)return result;
            var s=Load(p.User);uint lobby=CurrentLobby(s);if(req.TaleId!=LobbyTale(lobby))return result;
            uint root=ids[0];var statuses=StoryStatuses(p);var layout=Layout(lobby,s,statuses);if(layout==null)return result;
            if(req.TriggerType==0&&root==LobbyStart(lobby))result.Events.Add(Event((uint)layout["EventID"],layout["ParamsVar"].Values<uint>().ToArray()));
            else if(req.TriggerType==2)
            {
                var action=Table("GodWarLobbyInteractAction").FirstOrDefault(x=>(uint)x["ActionType"]==1&&x["ActionParam"].Values<uint>().FirstOrDefault()==root&&ScopedAction(lobby,(uint)x["ActionID"],layout)&&ActionEnabled(x,s,statuses));
                if(action==null&&s.LobbyDialogue?.Lobby==lobby&&s.LobbyDialogue.Root==root&&Clock().ToUnixTimeSeconds()-s.LobbyDialogue.Since is >=0 and <=21600)
                {
                    var effects=PlanLobbyExtras(p,s,root);
                    try{DailyGameplay.Validate(p,effects.Plan);}catch{return result;}
                    if(effects.Receipts.Count>0){if(!Commit(p,s,()=>{DailyGameplay.Grant(p,effects.Plan);s.LobbyEffectReceipts.UnionWith(effects.Receipts);RememberCollections(p,s,false);} ))return result;result.Changed=true;result.InventoryChanged=effects.Plan.Items.Count>0;}
                    result.Events.Add(Event(s.LobbyDialogue.Leaf,s.LobbyDialogue.Plot));
                }
                else
                {
                    if(action==null)return result;
                    var leaf=EventLeaves(root).FirstOrDefault(x=>(uint)x["EventType"]==8&&x["ParamsVar"].Any());if(leaf==null){result.Reply.retcode=TriggerGodWarEventRsp.Retcode.FeatureClose;return result;}
                    uint plotId=(uint)leaf["ParamsVar"][0];var plot=Table("GodWarEventPlot").SingleOrDefault(x=>(uint)x["PlotID"]==plotId);if(plot==null)return result;
                    var pending=new LobbyDialogue{Lobby=lobby,Tale=req.TaleId,Root=root,Action=(uint)action["ActionID"],Leaf=(uint)leaf["EventID"],Plot=plotId,Since=Clock().ToUnixTimeSeconds(),
                        Follow=plot["EventParams"].Select(x=>(uint)x["EventID"]).Append((uint)plot["DefaultFollowEventID"]).Where(x=>x!=0).Distinct().ToArray()};
                    var effects=PlanLobbyExtras(p,s,root);
                    try{DailyGameplay.Validate(p,effects.Plan);}catch{return result;}
                    if(!Commit(p,s,()=>{s.LobbyDialogue=pending;s.StoryDone.UnionWith(MissionsForEvent(root).Concat(MissionsForEvent(pending.Leaf)).Where(id=>IsStoryMission(id)&&statuses.GetValueOrDefault(id)==MissionStatus.MissionDoing));DailyGameplay.Grant(p,effects.Plan);s.LobbyEffectReceipts.UnionWith(effects.Receipts);RememberCollections(p,s,false);} ))return result;
                    result.Changed=true;result.InventoryChanged=effects.Plan.Items.Count>0;result.Events.Add(Event(pending.Leaf,pending.Plot));
                }
            }
            else if(req.TriggerType==0&&s.LobbyDialogue is {} pending&&pending.Tale==req.TaleId&&pending.Follow.Contains(root)&&Clock().ToUnixTimeSeconds()-pending.Since is >=0 and <=21600)
            {
                // Acknowledges this issued dialogue's explicit continuation.
                // Missing branch flags do not authorize arbitrary reward leaves.
                if(!Commit(p,s,()=>{s.StoryDone.UnionWith(MissionsForEvent(root).Where(id=>IsStoryMission(id)&&statuses.GetValueOrDefault(id)==MissionStatus.MissionDoing));s.LobbyFollowEvents.Add(req.TaleId+"/"+root);RememberCollections(p,s,false);} ))return result;
                result.Changed=true;result.Events.Add(Event(3000016));
            }
            else return result;
            result.Reply.retcode=0;result.Reply.EventIdLists=ids;return result;
        }
    }
    internal static void PushLobby(Session session,uint lobby)
    {
        session.Send(Packet.FromProto(Lobby(session.Player,new GetGodWarLobbyReq{GodWarId=1,LobbyId=lobby}),CmdId.GetGodWarLobbyRsp));
    }
}





