using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;

internal static class StigmataRefining
{
    private static readonly Dictionary<string,JArray> Tables = new();
    private static readonly object TableGate = new();
    private static JArray Table(string name)
    {
        lock(TableGate)
        {
            if (!Tables.TryGetValue(name,out var rows))
                Tables[name] = rows = JArray.Parse(File.ReadAllText(Path.Combine("Resources","ExcelOutputAsset",name+".json")));
            return rows;
        }
    }
    private static StigmataRune Copy(StigmataRune rune) => new() { RuneId=rune.RuneId, StrengthPercent=rune.StrengthPercent };
    private static uint SequenceBase(EquipmentScheme equipment)
    {
        uint pending=(equipment.StigmataList ?? Array.Empty<Stigmata>()).Where(x=>x!=null)
            .SelectMany(x=>x.WaitSelectRuneGroupLists).Where(x=>x!=null).Select(x=>x.UniqueId).DefaultIfEmpty(0u).Max();
        return Math.Max(65535u,Math.Max(equipment.RuneCandidateSequence,pending));
    }
    // Client tables define valid affixes, but do not contain server-side drop weights.
    // Equal weighting is a compatibility fallback, not a claim about official odds.
    private static StigmataRune RandomRune(uint tree, uint? exclude = null)
    {
        var pool=Table("AffixList").Where(x => tree==3 ? (uint)x["affixID"]>=30000 : (uint)x["affixID"]/10000==tree)
            .Where(x=>!exclude.HasValue || (uint)x["affixID"]/10!=exclude.Value/10).ToArray();
        if(pool.Length==0)throw new InvalidOperationException("No configured affix pool");
        return new(){ RuneId=(uint)pool[Random.Shared.Next(pool.Length)]["affixID"], StrengthPercent=(uint)Random.Shared.Next(1,101) };
    }
    private static bool IsOffensive(JToken row)
    {
        int type=(int)row["AffixSkillType"],prop=(int)row["propID"];
        return type==1 || type==4 || type==6 || prop==931 || prop==932 || prop==933;
    }
    private static bool IsSupport(JToken row)
    {
        int type=(int)row["AffixSkillType"],prop=(int)row["propID"];
        return type==2 || type==3 || prop==951 || prop==952 || prop==953;
    }
    private static StigmataRune Special(uint id,int slot)
    {
        if(id==3 || id==14)
        {
            // Local preview text guarantees two 3-star offensive/support affixes.
            // AffixSkillType 5 contains both elemental damage and resistance;
            // distinguish their prop IDs instead of treating the whole category alike.
            var pool=Table("AffixList").Where(x=>(uint)x["affixID"]>=30000 && (int)x["level"]==3)
                .Where(x => id==3 ? IsOffensive(x) : IsSupport(x)).ToArray();
            if(pool.Length==0)throw new InvalidOperationException("Missing oriented affixes");
            return new(){RuneId=(uint)pool[Random.Shared.Next(pool.Length)]["affixID"],StrengthPercent=(uint)Random.Shared.Next(1,101)};
        }
        uint rune=id switch {14=>30013,4 or 8=>30063,5 or 9=>30073,6 or 10=>30083,7 or 11=>30193,12 or 13=>30213,15=>slot==0?50033u:50043u,_=>50043};
        uint strength=id switch {3 or 14=>(uint)Random.Shared.Next(1,101),8 or 9 or 10 or 11 or 13=>(uint)Random.Shared.Next(7,66),15=>slot==0?45u:59u,_=>100};
        return new(){RuneId=rune,StrengthPercent=strength};
    }
    internal static PromoteStigmataRuneRsp Promote(EquipmentScheme equipment,uint uniqueId)
    {
        lock(equipment)
        {
            var rsp=new PromoteStigmataRuneRsp();
            PromoteStigmataRuneRsp Fail(PromoteStigmataRuneRsp.Retcode code){rsp.retcode=code;return rsp;}
            // Explicit opt-in only. This distribution is not recovered official behavior.
            try
            {
                if(!File.Exists("stigmata-promotion.json"))return Fail(PromoteStigmataRuneRsp.Retcode.Fail);
                var policy=JObject.Parse(File.ReadAllText("stigmata-promotion.json"));
                if((bool?)policy["enabled"]!=true || (string)policy["mode"]!="approximate")return Fail(PromoteStigmataRuneRsp.Retcode.Fail);
            }
            catch{return Fail(PromoteStigmataRuneRsp.Retcode.Fail);}
            var item=equipment.StigmataList?.FirstOrDefault(x=>x.UniqueId==uniqueId);
            if(item==null)return Fail(PromoteStigmataRuneRsp.Retcode.StigmataNotExist);
            if(item.PromoteTimes>=10)return Fail(PromoteStigmataRuneRsp.Retcode.TimesLimit);
            if(item.WaitSelectRuneGroupLists.Count>0)
            {
                // 客户端在 TooManyWaitRune 分支里会拿这个列表去弹「还有词缀没选」的界面，
                // 空数组会让它弹一个空列表，所以把待选圣痕都带上。
                rsp.WaitSelectStgimataLists=(equipment.StigmataList??Array.Empty<Stigmata>())
                    .Where(x=>x!=null&&x.WaitSelectRuneGroupLists.Count>0).Select(x=>x.UniqueId).ToArray();
                return Fail(PromoteStigmataRuneRsp.Retcode.TooManyWaitRune);
            }
            if(item.SlotNum==0 || item.SlotNum>2 || item.RuneLists.Count!=item.SlotNum)return Fail(PromoteStigmataRuneRsp.Retcode.StigmataNotIdentify);
            var meta=Table("StigmataData").FirstOrDefault(x=>(uint)x["ID"]==item.Id);
            if(meta==null || !(bool)meta["canRefine"])return Fail(PromoteStigmataRuneRsp.Retcode.CanNotPromote);
            var definitions=Table("AffixList").ToDictionary(x=>(uint)x["affixID"]);
            if(item.RuneLists.Any(x=>x==null || x.StrengthPercent>100 || !definitions.ContainsKey(x.RuneId)))return Fail(PromoteStigmataRuneRsp.Retcode.CanNotPromote);
            var eligible=item.RuneLists.Select((x,i)=>(x,i)).Where(v=>v.x.StrengthPercent<100).ToArray();
            if(eligible.Length==0)return Fail(PromoteStigmataRuneRsp.Retcode.CanNotPromote);
            var node=Table("AffixTreeNode").FirstOrDefault(x=>(uint)x["id"]==(uint)meta["affixTreeId"]);
            if(node?["refineCost_core"] is not JArray costs || costs.Count==0)return Fail(PromoteStigmataRuneRsp.Retcode.Fail);
            var required=costs.GroupBy(x=>(uint)x["materialID"]).ToDictionary(g=>g.Key,g=>g.Sum(x=>(long)x["materialNum"]));
            if(required.Any(x=>x.Key==0 || x.Value<=0 || x.Value>uint.MaxValue))return Fail(PromoteStigmataRuneRsp.Retcode.Fail);
            if(required.Any(x=>equipment.MaterialList.Where(m=>m.Id==x.Key).Sum(m=>(long)m.Num)<x.Value))return Fail(PromoteStigmataRuneRsp.Retcode.MaterialLack);
            uint sequence=SequenceBase(equipment);
            if(sequence==uint.MaxValue)return Fail(PromoteStigmataRuneRsp.Retcode.Fail);
            var group=new StigmataRuneGroup{UniqueId=sequence+1};
            group.RuneLists.AddRange(item.RuneLists.Select(Copy));
            int selected=eligible[Random.Shared.Next(eligible.Length)].i;
            group.RuneLists[selected].StrengthPercent=Math.Min(100u,group.RuneLists[selected].StrengthPercent+(uint)Random.Shared.Next(1,6));
            bool previousIdentified=item.IsAffixIdentify;
            var counts=equipment.MaterialList.Select(x=>x.Num).ToArray();uint previousTimes=item.PromoteTimes,previousSequence=equipment.RuneCandidateSequence;
            try
            {
                foreach(var cost in required){long remaining=cost.Value;foreach(var material in equipment.MaterialList.Where(x=>x.Id==cost.Key)){uint debit=(uint)Math.Min(material.Num,remaining);material.Num-=debit;remaining-=debit;}}
                item.IsAffixIdentify=item.RuneLists.Count>0;item.PromoteTimes++;equipment.RuneCandidateSequence=group.UniqueId;item.WaitSelectRuneGroupLists.Add(group);equipment.Save();
            }
            catch
            {
                for(int i=0;i<counts.Length;i++)equipment.MaterialList[i].Num=counts[i];
                item.IsAffixIdentify=previousIdentified;item.PromoteTimes=previousTimes;equipment.RuneCandidateSequence=previousSequence;item.WaitSelectRuneGroupLists.Clear();return Fail(PromoteStigmataRuneRsp.Retcode.Fail);
            }
            // 单机版：词缀消耗品锁定 9999。扣减是照常扣的，扣完立刻顶回去，
            // 客户端这边看到的永远是满的（放在 try 外面，避免影响回滚逻辑）。
            GmLocks.Materials(equipment);
            rsp.WaitSelectStgimataLists=new[]{uniqueId};
            rsp.retcode=PromoteStigmataRuneRsp.Retcode.Succ;return rsp;
        }
    }
    internal static RefineStigmataRuneRsp Refine(EquipmentScheme equipment,RefineStigmataRuneReq req)
    {
        lock(equipment)
        {
            var rsp=new RefineStigmataRuneRsp{TimesType=req.TimesType};
            RefineStigmataRuneRsp Fail(RefineStigmataRuneRsp.Retcode code){rsp.retcode=code;return rsp;}
            var item=equipment.StigmataList?.FirstOrDefault(x=>x.UniqueId==req.UniqueId);
            if(item==null)return Fail(RefineStigmataRuneRsp.Retcode.StigmataNotExist);
            var meta=Table("StigmataData").FirstOrDefault(x=>(uint)x["ID"]==item.Id);
            if(meta==null || !(bool)meta["canRefine"])return Fail(RefineStigmataRuneRsp.Retcode.StigmataCanNotRefine);
            uint tree=(uint)meta["affixTreeId"];
            var node=Table("AffixTreeNode").FirstOrDefault(x=>(uint)x["id"]==tree);
            if(node==null || item.SlotNum>2)return Fail(RefineStigmataRuneRsp.Retcode.Fail);
            int times=(int)req.TimesType;
            if(times!=1 && times!=10)return Fail(RefineStigmataRuneRsp.Retcode.Fail);
            if(times==10 && req.Type!=StigmataRefineType.StigmataRefineNormal)return Fail(RefineStigmataRuneRsp.Retcode.Fail);
            if(times==10 && item.SlotNum!=2)return Fail(RefineStigmataRuneRsp.Retcode.SlotLack);
            if(item.WaitSelectRuneGroupLists.Count>0 && !req.IsRetry)return Fail(RefineStigmataRuneRsp.Retcode.StigmataWaitRuneExist);
            JToken costs;uint slots=item.SlotNum;
            switch(req.Type)
            {
                case StigmataRefineType.StigmataRefineNormal:
                    if(slots==0)return Fail(RefineStigmataRuneRsp.Retcode.StigmataNotIdentify);
                    costs=node[slots==1?"refineCost_1":"refineCost_2"];break;
                case StigmataRefineType.StigmataRefineAddSlot:
                    if(slots>=2)return Fail(RefineStigmataRuneRsp.Retcode.Fail);
                    if(slots==1 && (item.RuneLists.Count!=1 || item.RuneLists.Any(x=>x==null || x.RuneId==0 || x.StrengthPercent>100)))return Fail(RefineStigmataRuneRsp.Retcode.StigmataNotIdentify);
                    costs=node[slots==0?"refineCost_0to1":"refineCost_1to2"];slots++;break;
                case StigmataRefineType.StigmataRefineLock:
                    if(slots!=2 || item.RuneLists.Count!=2)return Fail(RefineStigmataRuneRsp.Retcode.SlotLack);
                    if(item.RuneLists.Any(x=>x==null || x.RuneId==0 || x.StrengthPercent>100))return Fail(RefineStigmataRuneRsp.Retcode.StigmataNotIdentify);
                    if(req.LockRuneIndex>1)return Fail(RefineStigmataRuneRsp.Retcode.LockIndexError);
                    costs=node["refineCost_lock1"];break;
                case StigmataRefineType.StigmataRefineSpecial:
                    var special=Table("SpecialAffixData").FirstOrDefault(x=>(uint)x["id"]==req.SpecialId);
                    if(special==null || (uint)special["affixTreeNodeID"]!=tree || (uint)special["nodeNumRequire"]!=slots)
                        return Fail(RefineStigmataRuneRsp.Retcode.Fail);
                    costs=special["refineCost"];break;
                default:return Fail(RefineStigmataRuneRsp.Retcode.Fail);
            }
            if(costs is not JArray costRows || costRows.Count==0)return Fail(RefineStigmataRuneRsp.Retcode.Fail);
            var required=costRows.GroupBy(x=>(uint)x["materialID"]).ToDictionary(g=>g.Key,g=>g.Sum(x=>(long)x["materialNum"])*times);
            if(req.Type==StigmataRefineType.StigmataRefineSpecial)
                Packet.c.Warn("[Refine][Special] specialId="+req.SpecialId+" slots="+item.SlotNum
                    +"times="+times+" cost="+string.Join("+",required.Select(x=>x.Key+"x"+x.Value))
                    +" owned="+string.Join("+",required.Select(x=>x.Key+":"+equipment.MaterialList.Where(m=>m.Id==x.Key).Sum(m=>(long)m.Num))));
            if(required.Any(x=>x.Key==0 || x.Value<=0 || x.Value>uint.MaxValue))return Fail(RefineStigmataRuneRsp.Retcode.Fail);
            if(required.Any(x=>equipment.MaterialList.Where(m=>m.Id==x.Key).Sum(m=>(long)m.Num)<x.Value))
                return Fail(RefineStigmataRuneRsp.Retcode.ConsumeItemLack);
            // Equipment sacrifice is not supported by this material-based 7.0 path.
            if(req.ConsumeItemList?.ItemLists.Any(x=>x.Type!=EquipmentType.EquipmentMaterial || !required.ContainsKey(x.IdOrUniqueId))==true)
                return Fail(RefineStigmataRuneRsp.Retcode.ConsumeItemUnNecessary);
            // Ordinary single-slot reconstruction accumulates unlock progress, including
            // paid retries; failed/insufficient-material requests never advance it.
            uint progress=item.RefineValue;
            if(req.Type==StigmataRefineType.StigmataRefineNormal && item.SlotNum==1)
            {progress=Math.Min(10u,checked(progress+1));if(progress>=10)slots=2;}
            var groups=new List<StigmataRuneGroup>();
            uint sequence=SequenceBase(equipment);
            if((ulong)sequence+(uint)times>uint.MaxValue)return Fail(RefineStigmataRuneRsp.Retcode.Fail);
            for(int i=0;i<times;i++)
            {
                var group=new StigmataRuneGroup{UniqueId=++sequence};
                for(int slot=0;slot<slots;slot++)
                {
                    StigmataRune rune;
                    if(req.Type==StigmataRefineType.StigmataRefineLock && slot==req.LockRuneIndex)rune=Copy(item.RuneLists[slot]);
                    else if(req.Type==StigmataRefineType.StigmataRefineAddSlot && item.SlotNum==1 && slot==0)rune=Copy(item.RuneLists[0]);
                    else if(req.Type==StigmataRefineType.StigmataRefineSpecial)rune=Special(req.SpecialId,slot);
                    else rune=RandomRune(tree,slot>0?group.RuneLists[0].RuneId:(uint?)null);
                    group.RuneLists.Add(rune);
                }
                groups.Add(group);
            }
            if(req.Type==StigmataRefineType.StigmataRefineSpecial)
                Packet.c.Warn("[Refine][Special] runes="+string.Join(",",groups.SelectMany(g=>g.RuneLists).Select(r=>r.RuneId+"@"+r.StrengthPercent)));
            var oldCounts=equipment.MaterialList.Select(x=>x.Num).ToArray();
            bool oldIdentified=item.IsAffixIdentify;
            var oldGroups=item.WaitSelectRuneGroupLists.ToArray();uint oldSlots=item.SlotNum,oldSequence=equipment.RuneCandidateSequence,oldProgress=item.RefineValue;
            try
            {
                foreach(var pair in required)
                {
                    long remaining=pair.Value;
                    foreach(var material in equipment.MaterialList.Where(m=>m.Id==pair.Key))
                    {uint debit=(uint)Math.Min(remaining,material.Num);material.Num-=debit;remaining-=debit;}
                }
                item.IsAffixIdentify=item.RuneLists.Count>0;item.SlotNum=slots;item.RefineValue=progress;equipment.RuneCandidateSequence=sequence;
                item.WaitSelectRuneGroupLists.Clear();item.WaitSelectRuneGroupLists.AddRange(groups);
                equipment.Save();
            }
            catch
            {
                for(int i=0;i<oldCounts.Length;i++)equipment.MaterialList[i].Num=oldCounts[i];
                item.IsAffixIdentify=oldIdentified;item.SlotNum=oldSlots;item.RefineValue=oldProgress;equipment.RuneCandidateSequence=oldSequence;
                item.WaitSelectRuneGroupLists.Clear();item.WaitSelectRuneGroupLists.AddRange(oldGroups);
                return Fail(RefineStigmataRuneRsp.Retcode.Fail);
            }
            GmLocks.Materials(equipment);
            rsp.retcode=RefineStigmataRuneRsp.Retcode.Succ;rsp.RuneGroupLists.AddRange(groups);return rsp;
        }
    }
}

