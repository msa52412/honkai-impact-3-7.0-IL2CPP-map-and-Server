using System;
using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;
using Newtonsoft.Json.Linq;

namespace PemukulPaku.GameServer.Game;

internal static partial class GodWarProgression
{
    internal static bool HandlesShallow(uint stage)=>Table("GodWarAvatarChallenge").Any(x=>(uint)x["StageID"]==stage);
    private static JToken ShallowOwner(uint stage)=>Table("GodWarMainAvatar").SingleOrDefault(x=>x["AvatarChallengeStageIDList"].Values<uint>().Contains(stage));
    internal static StageBeginRsp BeginShallow(Player p,StageBeginReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new StageBeginRsp{StageId=req.StageId};
            var cfg=Table("GodWarAvatarChallenge").SingleOrDefault(x=>(uint)x["StageID"]==req.StageId);var main=ShallowOwner(req.StageId);
            if(cfg==null||main==null){rsp.retcode=StageBeginRsp.Retcode.StageNotExist;return rsp;}
            uint avatar=(uint)main["MainAvatarID"];var ids=(req.AvatarIdLists??Array.Empty<uint>()).Where(x=>x!=0).ToArray();
            // Do not mistake the preset/recommended supports on a stage row
            // for the realm's full list of selectable support battlesuits.
            var allowed=Table("GodWarSupportAvatar").Select(x=>(uint)x["SupportAvatarID"]).Append(avatar).ToHashSet();
            if(ids.Length==0||ids.Length>3||!ids.Contains(avatar)||ids.Distinct().Count()!=ids.Length||ids.Any(x=>!allowed.Contains(x))||!p.AvatarList.Any(x=>x.AvatarId==avatar&&x.Star>0))
            {rsp.retcode=StageBeginRsp.Retcode.AvatarError;return rsp;}
            var elves=(req.ElfIdLists??Array.Empty<uint>()).Where(x=>x!=0).ToArray();
            if(elves.Length>1||elves.Any(x=>p.Elfs==null||!p.Elfs.Elves.Any(e=>e.ElfId==x)))
            {rsp.retcode=StageBeginRsp.Retcode.AvatarError;return rsp;}
            var s=Load(p.User);var roleMeta=Table("GodWarRelationData").FirstOrDefault(x=>(uint)x["AvatarID"]==avatar&&(uint)x["RoleType"]==2);
            if(roleMeta==null||!s.Roles.TryGetValue((uint)roleMeta["RoleID"],out var role)||role.Level==0)
            {rsp.retcode=StageBeginRsp.Retcode.AvatarError;return rsp;}
            var sequence=main["AvatarChallengeStageIDList"].Values<uint>().ToArray();int index=Array.IndexOf(sequence,req.StageId);
            if(index>0&&!s.ClearedStages.Contains(sequence[index-1])){rsp.retcode=StageBeginRsp.Retcode.PreStageNotFinish;return rsp;}
            if(!Commit(p,s,()=>{s.PendingStage=req.StageId;s.PendingAvatar=avatar;s.PendingLightningSupport=avatar!=204&&ids.Contains(204u);s.PendingSince=Clock().ToUnixTimeSeconds();}))rsp.retcode=StageBeginRsp.Retcode.Fail;
            return rsp;
        }
    }
    private static void AwardAvatarExp(State s,uint roleId,uint amount)
    {
        var role=s.Roles[roleId];var rows=Rows(roleId);ulong exp=(ulong)role.Exp+amount;
        // Battle EXP levels an already-unlocked battlesuit automatically. NPC
        // memory progression retains its separate manual upgrade protocol.
        while(true)
        {
            var current=rows.First(x=>(uint)x["Level"]==role.Level);
            var next=rows.FirstOrDefault(x=>(uint)x["Level"]==role.Level+1);
            if(next==null){role.Exp=0;return;}
            uint need=(uint)current["Exp"];
            if(need==0||exp<need){role.Exp=checked((uint)exp);return;}
            if((uint)next["LvUpMaterial"]["Num"]!=0)throw new InvalidOperationException("Unexpected avatar promotion cost");
            exp-=need;role.Level++;
        }
    }
    internal static StageEndRsp EndShallow(Player p,StageEndReqBody req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new StageEndRsp{retcode=StageEndRsp.Retcode.Fail,StageId=req.StageId,EndStatus=req.EndStatus};
            var cfg=Table("GodWarAvatarChallenge").SingleOrDefault(x=>(uint)x["StageID"]==req.StageId);
            if(cfg==null){rsp.retcode=StageEndRsp.Retcode.StageError;return rsp;}
            var s=Load(p.User);long now=Clock().ToUnixTimeSeconds();
            if(s.PendingStage!=req.StageId||now<s.PendingSince||now-s.PendingSince>21600)return rsp;
            if(req.EndStatus!=StageEndStatus.StageWin)
            {
                if(Commit(p,s,()=>{s.PendingStage=0;s.PendingAvatar=0;}))rsp.retcode=StageEndRsp.Retcode.Succ;
                return rsp;
            }
            if(req.StagePassTime==0||req.StagePassTime>21600){rsp.retcode=StageEndRsp.Retcode.StageTimeError;return rsp;}
            if(s.RewardWeek>Week)return rsp;
            bool first=!s.ClearedStages.Contains(req.StageId);uint avatar=s.PendingAvatar;
            var roleMeta=Table("GodWarRelationData").First(x=>(uint)x["AvatarID"]==avatar&&(uint)x["RoleType"]==2);
            uint roleId=(uint)roleMeta["RoleID"],expItem=(uint)roleMeta["ExpMaterialID"];
            var earned=s.RewardWeek==Week?new Dictionary<uint,uint>(s.WeeklyEarned):new Dictionary<uint,uint>();
            var plan=new DailyGameplay.Plan();var normal=new List<DropItem>();var extra=new List<DropItem>();
            foreach(var item in cfg["RewardMaterialMap"])
            {
                uint id=(uint)item["Material"],num=(uint)item["Num"];
                var meta=Table("GodWarMaterial").First(x=>(uint)x["MaterialID"]==id);
                if((uint)meta["MaterialType"] is 3 or 4)
                {
                    uint cap=(uint)meta["QuantityLimit"],used=earned.GetValueOrDefault(id);
                    num=Math.Min(num,cap-Math.Min(cap,used));earned[id]=checked(used+num);
                }
                else if((uint)meta["QuantityLimit"]>0)
                {
                    uint cap=(uint)meta["QuantityLimit"],held=Held(p,id);
                    num=Math.Min(num,cap-Math.Min(cap,held));
                }
                if(num==0)continue;plan.Items[(int)id]=plan.Items.GetValueOrDefault((int)id)+num;normal.Add(new DropItem{ItemId=id,Num=num});
            }
            uint normalExp=(uint)cfg["AvatarExpMaterialAddNum"],firstExp=first?(uint)cfg["FirstWinExpMaterialAddNum"]:0;
            if(normalExp>0)normal.Add(new DropItem{ItemId=expItem,Num=normalExp});
            if(firstExp>0)extra.Add(new DropItem{ItemId=expItem,Num=firstExp});
            var stage=Table("StageData_Main").Single(x=>(uint)x["levelId"]==req.StageId);
            var challenges=s.StageChallenges.GetValueOrDefault(req.StageId)??new HashSet<uint>();
            var awards=new List<(uint Index,DailyGameplay.Plan Plan)>();uint index=0;
            foreach(var rule in stage["challengeList"])
            {
                var condition=Table("StageChallengeData").Single(x=>(uint)x["challengeId"]==(uint)rule["challengeId"]);
                if((uint)condition["conditionId"]!=5)throw new InvalidOperationException("Unverified shallow challenge condition");
                if(!challenges.Contains(index)&&req.StagePassTime<=(uint)condition["paramList"][0])awards.Add((index,DailyGameplay.Reward((int)rule["rewardId"])));
                index++;
            }
            try{DailyGameplay.Validate(p,DailyGameplay.Combine(awards.Select(x=>x.Plan).Append(plan)));}
            catch(Exception ex){Packet.c.Warn("[GodWar] shallow settlement validation: "+ex.Message);return rsp;}
            if(!Commit(p,s,()=>{
                DailyGameplay.Grant(p,plan);AwardAvatarExp(s,roleId,checked(normalExp+firstExp));
                foreach(var award in awards){rsp.ChallengeLists.Add(new StageChallengeData{ChallengeIndex=award.Index,Reward=DailyGameplay.Grant(p,award.Plan)});challenges.Add(award.Index);}
                s.StageChallenges[req.StageId]=challenges;s.ClearedStages.Add(req.StageId);s.RewardWeek=Week;s.WeeklyEarned=earned;s.PendingStage=0;s.PendingAvatar=0;
                if(s.PendingLightningSupport) CountLightningSupportWin(s);
                s.PendingLightningSupport=false;
                DailyGameplay.Add(p,10051,(int)req.StageId);
            })){rsp.ChallengeLists.Clear();return rsp;}
            // Native result dialog reads BonusDropLists (+0x90) as ordinary
            // rewards and ExtraDropLists (+0xA8) as first-clear rewards.
            rsp.BonusDropLists.AddRange(normal);rsp.ExtraDropLists.AddRange(extra);rsp.Progress=1;rsp.IsFirstWin=first;rsp.retcode=StageEndRsp.Retcode.Succ;return rsp;
        }
    }
    internal static void ApplyShallowStages(Player p,GetStageDataRsp response)
    {
        lock(p.Equipment)lock(Gate)
        {
            var s=Load(p.User);var ids=Table("GodWarAvatarChallenge").Select(x=>(uint)x["StageID"]).ToHashSet();
            foreach(var stage in response.StageLists.Where(x=>ids.Contains(x.Id)))
            {
                stage.IsDone=s.ClearedStages.Contains(stage.Id);stage.Progress=stage.IsDone?1u:0u;
                stage.ChallengeIndexLists=s.StageChallenges.TryGetValue(stage.Id,out var done)?done.OrderBy(x=>x).ToArray():Array.Empty<uint>();
                stage.EnterTimes=stage.IsDone?1u:0u;
            }
        }
    }
}
