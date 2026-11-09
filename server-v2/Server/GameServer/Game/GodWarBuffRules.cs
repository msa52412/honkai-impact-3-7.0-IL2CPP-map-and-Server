using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Game;

internal static partial class GodWarProgression
{
    private static JToken[] PlayableBuffCache;
    private static JToken[] PlayableBuffRows()=>PlayableBuffCache??=Table("GodWarBuff").Where(x=>(uint)x["BuffLevel"]==1&&(uint)x["BuffSuit"]!=0&&!string.IsNullOrWhiteSpace((string)x["AbilityName"])&&!string.IsNullOrWhiteSpace((string)x["BuffIcon"])&&Table("GodWarBuffSuit").Any(s=>(uint)s["SuitID"]==(uint)x["BuffSuit"])).ToArray();
    private static uint CoreBranch(uint group)=>group is 11 or 12?group-10:group;
    // Variant weights/weekly replacement rules are not available in this
    // export. Prefer an already-owned version, otherwise the lowest authored
    // ID, so one ability never counts as several different basic signets.
    private static IEnumerable<uint> EligibleSignets(ChallengeRun run,uint suit,bool upgrades)
    {
        var d=run.Deep;uint main=run.Challenge.AvatarLists.Single().AvatarId;var rows=PlayableBuffRows();
        var owned=rows.Where(x=>d.Buffs.ContainsKey((uint)x["BuffID"])).ToArray();
        if(suit is 0 or 8)
            foreach(var meta in rows.Where(x=>(uint)x["BuffQuality"]==4 && (uint)x["BuffGroup"]==main && (uint)x["BuffID"]<20000))
            {
                uint baseId=(uint)meta["BuffID"], variant=ExclusiveVariant(run,baseId);
                if(!d.Buffs.ContainsKey(baseId) && !d.Buffs.ContainsKey(baseId+10000))yield return variant;
            }
        foreach(var meta in rows.Where(x=>(uint)x["BuffQuality"]!=4&&((uint)x["BuffGroup"]<100||(uint)x["BuffGroup"]==main)).GroupBy(x=>(string)x["AbilityName"]).Select(g=>g.OrderByDescending(x=>d.Buffs.ContainsKey((uint)x["BuffID"])).ThenBy(x=>(uint)x["BuffID"]).First()))
        {
            uint id=(uint)meta["BuffID"],family=(uint)meta["BuffSuit"],quality=(uint)meta["BuffQuality"],branch=(uint)meta["BuffGroup"];
            if(suit!=0&&family!=suit)continue;
            if(quality!=4&&branch>=100&&branch!=main)continue;
            if(d.Buffs.TryGetValue(id,out uint level))
            {if(upgrades&&Table("GodWarBuff").Any(x=>(uint)x["BuffID"]==id&&(uint)x["BuffLevel"]==level+1))yield return id;continue;}
            if(family==8)continue;
            if(quality==1){yield return id;continue;}
            var same=owned.Where(x=>(uint)x["BuffSuit"]==family).ToArray();var cores=same.Where(x=>(uint)x["BuffQuality"]==3).ToArray();
            if(quality==3&&cores.Length==0&&same.Where(x=>(uint)x["BuffQuality"]==1).Select(x=>(string)x["AbilityName"]).Distinct().Count()>=3)yield return id;
            if(quality==2&&cores.Any(x=>CoreBranch((uint)x["BuffGroup"])==CoreBranch(branch)))yield return id;
        }
    }
    private static IEnumerable<uint> EligibleShopSignets(ChallengeRun run)
    {
        var excluded = PlayableBuffRows()
            .Where(row => (uint)row["BuffSuit"] == 8 || (uint)row["BuffQuality"] == 4)
            .Select(row => (uint)row["BuffID"])
            .ToHashSet();
        return EligibleSignets(run, 0, true).Where(id => !excluded.Contains(id));
    }

    private static bool OfferCore(ChallengeRun run)
    {
        var cores=EligibleBuffs(run,0).Select(id=>PlayableBuffRows().Single(x=>(uint)x["BuffID"]==id)).Where(x=>(uint)x["BuffQuality"]==3).OrderBy(x=>(uint)x["BuffSuit"]).ThenBy(x=>(uint)x["BuffID"]).ToArray();
        if(cores.Length==0){run.Deep.CoreSuit=0;return false;}
        uint family=(uint)cores[0]["BuffSuit"];run.Deep.CoreSuit=family;run.Deep.Candidates=cores.Where(x=>(uint)x["BuffSuit"]==family).Select(x=>(uint)x["BuffID"]).ToArray();run.Deep.Phase="buff";PrepareSignetDialogue(run);return true;
    }
    internal static bool DeepChoosingCore(Player p,uint tale)
    {
        lock(Gate){return Load(p.User).Runs.TryGetValue(tale,out var run)&&run.Deep?.Phase=="buff"&&run.Deep.CoreSuit!=0;}
    }
    // Native ShowDialog @180EA1BB0 reads param[0] as pool, extra[0..1]
    // as refresh counters and ParamMap key/value as buff ID/level. Candidate
    // IDs in event_param_list do not populate the client's choice rows.
    private static GodWarEventInfo SelectionEvent(ChallengeRun run)
    {
        var d=run.Deep;var suits=d.Candidates.Select(id=>(uint)PlayableBuffRows().Single(x=>(uint)x["BuffID"]==id)["BuffSuit"]).Distinct().ToArray();
        uint suit=suits.Length==1?suits[0]:0;
        uint pool=(uint)Table("GodWarBuffPool").First(x=>(uint)x["BuffSuit"]==suit)["PoolID"];
        var info=Event(200001001,pool,(uint)d.Candidates.Length);
        info.ExtraParamLists=d.CoreSuit!=0?new uint[]{0,0}:new uint[]{d.BuffRefreshed?1u:0u,1};
        foreach(uint id in d.Candidates)info.EventParamMapLists.Add(new GodWarEventInfo.ParamMap{Key=id,Value=DeepSelectedBuffLevel(run,id)});
        return info;
    }
    private static bool RepairDeepOffers(Player p,State state,ChallengeRun run,out bool changed)
    {
        changed=false;var d=run.Deep;if(d==null)return true;
        bool candidates=false;var invalidGoods=new HashSet<uint>();
        if(d.Phase=="buff")
        {
            var allowed=EligibleBuffs(run,d.CoreSuit!=0?d.CoreSuit:d.NextSuit).Where(id=>d.CoreSuit==0||(uint)PlayableBuffRows().Single(x=>(uint)x["BuffID"]==id)["BuffQuality"]==3).ToHashSet();if(allowed.Count==0&&d.CoreSuit==0)allowed=EligibleBuffs(run,0).ToHashSet();
            bool needsCore=d.CoreSuit==0&&EligibleBuffs(run,0).Any(id=>(uint)PlayableBuffRows().Single(x=>(uint)x["BuffID"]==id)["BuffQuality"]==3);
            candidates=needsCore||d.Candidates.Length==0||d.Candidates.Any(x=>!allowed.Contains(x))||d.Candidates.Distinct().Count()!=d.Candidates.Length;
        }
        if(d.Phase=="portal"&&d.Goods.Count>0)
        {
            var allowed=EligibleShopSignets(run).ToHashSet();
            foreach(var good in d.Goods.Where(x=>!x.Sold))if(!allowed.Contains(good.Buff)||good.Upgrade!=d.Buffs.ContainsKey(good.Buff))invalidGoods.Add(good.Index);
        }
        if(!candidates&&invalidGoods.Count==0)return true;
        bool ok=Commit(p,state,()=>{if(candidates)Candidates(run);if(invalidGoods.Count>0)d.Goods.RemoveAll(x=>invalidGoods.Contains(x.Index));});
        changed=ok;return ok;
    }
}

