using System;
using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;
using Common.Database;
using Newtonsoft.Json.Linq;

namespace PemukulPaku.GameServer.Game;

internal static partial class GodWarProgression
{
    private sealed class LobbyExtraEffects
    {
        internal DailyGameplay.Plan Plan=new();
        internal HashSet<uint> Receipts=new();
    }
    private static Dictionary<uint,JToken> ExtraEventRows;
    private static readonly Dictionary<uint,uint[]> ExtraLeafCache=new();
    // Bounded compatibility interpretation: only pure authored99 groups with
    // dialogue/save/sync and exact [extraItem,1,1] leaves are supported. No100
    // conditional/random branches, flags, currencies or inferred quantities.
    private static uint[] PureExtraLeaves(uint root)
    {
        if(ExtraLeafCache.TryGetValue(root,out var cached))return cached;
        ExtraEventRows??=Table("GodWarEvent").ToDictionary(x=>(uint)x["EventID"],x=>x);var todo=new Queue<uint>();todo.Enqueue(root);var seen=new HashSet<uint>();var leaves=new List<uint>();bool valid=true;
        while(todo.Count>0)
        {
            uint id=todo.Dequeue();if(!seen.Add(id))continue;
            if(seen.Count>512||!ExtraEventRows.TryGetValue(id,out var row)){valid=false;break;}uint type=(uint)row["EventType"];
            if(type==99){foreach(uint child in row["ParamsVar"].Values<uint>())todo.Enqueue(child);continue;}
            if(type is 8 or 10 or 20)continue;
            if(type!=16){valid=false;break;}var args=row["ParamsVar"].Values<uint>().ToArray();
            if(args.Length!=3||args[1]!=1||args[2]!=1){valid=false;break;}uint item=args[0];
            var meta=Table("GodWarMaterial").SingleOrDefault(x=>(uint)x["GodWarID"]==1&&(uint)x["MaterialID"]==item);
            if(meta==null||(uint)meta["MaterialType"]!=7||(uint)meta["QuantityLimit"]!=1||!Table("GodWarExtraItem").Any(x=>(uint)x["GodWarID"]==1&&(uint)x["ExtraItemID"]==item)){valid=false;break;}
            leaves.Add(id);
        }
        return ExtraLeafCache[root]=valid?leaves.ToArray():Array.Empty<uint>();
    }
    private static LobbyExtraEffects PlanLobbyExtras(Player p,State state,uint root)
    {
        var effects=new LobbyExtraEffects();
        foreach(uint id in PureExtraLeaves(root))
        {
            if(state.LobbyEffectReceipts.Contains(id))continue;
            uint item=(uint)ExtraEventRows[id]["ParamsVar"][0];effects.Receipts.Add(id);
            if(!p.Equipment.MaterialList.Any(x=>x.Id==item&&x.Num>0))effects.Plan.Items[(int)item]=1;
        }
        return effects;
    }
    internal static bool RestoreLobbyExtras(Player p,out bool inventoryChanged)
    {
        inventoryChanged=false;
        lock(p.Equipment)lock(Gate)
        {
            var state=Load(p.User);var merged=new LobbyExtraEffects();var completed=state.StoryDone.Concat(state.StoryClaimed).Concat(MissionTake.TakenIds(p.User.Uid)).ToHashSet();
            var schedule = Table("GodWarSchedule").Single(row => (uint)row["GodWarID"] == 1);
            var slotItems = schedule["SupportAvatarSlotMaterialList"].Values<uint>().Where(id => id != 0).ToArray();
            var slotUnlocks = Table("GodWarSupportLevel").Where(row =>
                (uint)row["GodWarID"] == 1 && (uint)row["SupportType"] == 2)
                .OrderBy(row => (uint)row["Level"]);
            uint points = Points(state);
            foreach (var unlock in slotItems.Zip(slotUnlocks, (id, level) => (Id: id, Points: (uint)level["TalentPointExp"])))
                if (points >= unlock.Points && Held(p, unlock.Id) == 0)
                    merged.Plan.Items[(int)unlock.Id] = 1;

            if (state.ClearedStages.Contains(170203))
            {
                var starter = PlanLobbyExtras(p, state, 99999992);
                merged.Receipts.UnionWith(starter.Receipts);
                foreach (var item in starter.Plan.Items)
                    merged.Plan.Items[item.Key] = item.Value;
            }
            if (state.Chapter >= 3)
            {
                // The chapter transition grants both of these in event 5930672.
                // Restore the two starter types for imported saves that skipped it.
                foreach (uint item in new uint[] { 119301, 119331 })
                    if (Held(p, item) == 0)
                        merged.Plan.Items[(int)item] = 1;
            }

            foreach(var action in Table("GodWarLobbyInteractAction").Where(x=>(uint)x["ActionType"]==1&&x["ActionParam"].Any()))
            {
                uint mission=(uint)action["DisplayMission"];if(mission==0||!completed.Contains(mission))continue;
                var effects=PlanLobbyExtras(p,state,(uint)action["ActionParam"][0]);merged.Receipts.UnionWith(effects.Receipts);
                foreach(var item in effects.Plan.Items)merged.Plan.Items[item.Key]=Math.Max(merged.Plan.Items.GetValueOrDefault(item.Key),item.Value);
            }
            // Persisted real progress/claim receipts are evidence. The generic
            // legacy "all missions closed" display policy is never evidence.
            if(merged.Receipts.Count==0&&merged.Plan.Items.Count==0)return true;
            try{DailyGameplay.Validate(p,merged.Plan);}catch{return false;}
            if(!Commit(p,state,()=>{DailyGameplay.Grant(p,merged.Plan);state.LobbyEffectReceipts.UnionWith(merged.Receipts);} ))return false;
            inventoryChanged=merged.Plan.Items.Count>0;return true;
        }
    }
}



