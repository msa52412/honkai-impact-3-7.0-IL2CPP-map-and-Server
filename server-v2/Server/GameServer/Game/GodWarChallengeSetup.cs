using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Common.Resources.Proto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PemukulPaku.GameServer.Game;

// The persisted pre-battle phase only. Event generation, entering a site,
// battle settlement and completion rewards are intentionally separate.
internal static partial class GodWarProgression
{
    public sealed class ChallengeRun
    {
        public uint AvatarScheduleId {get;set;}
        public long StartedAt {get;set;}
        // Original story 665030 permanently expands ordinary portal choices.
        // Snapshot at setup; legacy in-flight runs keep their issued layout.
        public bool AdditionalStoryPortal {get;set;}
        public DeepState Deep {get;set;}
        // Getter-only protobuf collections are not populated by the database's
        // JSON reader. Persist the exact protobuf wire payload instead.
        public byte[] ChallengeProto
        {
            get {using var stream=new MemoryStream();ProtoBuf.Serializer.Serialize(stream,Challenge);return stream.ToArray();}
            set {using var stream=new MemoryStream(value??Array.Empty<byte>());Challenge=NormalizeChallenge(ProtoBuf.Serializer.Deserialize<GodWarChallenge>(stream));}
        }
        [Newtonsoft.Json.JsonIgnore,System.Text.Json.Serialization.JsonIgnore]
        public GodWarChallenge Challenge {get;set;} = new();
    }
    private static GodWarChallenge NormalizeChallenge(GodWarChallenge value)
    {
        value.SupportAvatarIdLists??=Array.Empty<uint>();value.PunishBuffIdLists??=Array.Empty<uint>();value.BoonBuffIdLists??=Array.Empty<uint>();return value;
    }
    private static GodWarChallenge CopyChallenge(GodWarChallenge value)=>NormalizeChallenge(ProtoBuf.Serializer.DeepClone(value));
    private static JObject CurrentCombatSchedule(uint tale)=>GodWarCalendar.AvatarSchedule(tale,Table("GodWarAvatarTaleSchedule"),Clock().ToUnixTimeSeconds());
    private static uint[] Ids(uint[] ids)=>(ids??Array.Empty<uint>()).Where(x=>x!=0).ToArray();
    private static bool ValidSupport(uint[] ids,JToken schedule,JToken step,uint main)
    {
        if(ids.Length>2||ids.Distinct().Count()!=ids.Length||ids.Contains(main))return false;
        var allowed=schedule["SupportAvatarList"].Values<uint>().ToHashSet();
        if(ids.Any(x=>!allowed.Contains(x)))return false;
        // Authored story slot restrictions include signed -1 sentinels and
        // ordered slots. They are not an unordered avatar whitelist. Current
        // supported trial/weekly rows have no such restriction; story setup
        // remains outside this implementation.
        return step["SupportAvatarLimit"]==null||!step["SupportAvatarLimit"].Any();
    }
    private static bool ValidExtra(Player p,uint id,uint type)=>id==0||
        (p.Equipment.MaterialList.Any(x=>x.Id==id&&x.Num>0)&&Table("GodWarExtraItem").Any(x=>(uint)x["GodWarID"]==1&&(uint)x["ExtraItemID"]==id&&(uint)x["ExtraItemType"]==type));
    private static GodWarChallengeStigmata SnapshotStigmata(Player p,uint unique)
    {
        var item=p.Equipment.StigmataList.FirstOrDefault(x=>unique!=0&&x.UniqueId==unique);
        var result=new GodWarChallengeStigmata{Id=item?.Id??0,Level=item?.Level??0};
        if(item!=null)result.RuneLists.AddRange(item.RuneLists.Select(x=>JsonConvert.DeserializeObject<StigmataRune>(JsonConvert.SerializeObject(x))));
        return result;
    }
    private static bool SameChallengeSetup(ChallengeRun run,SelectGodWarChallengeAvatarReq req)
    {
        var c=run.Challenge;
        return c!=null&&c.AvatarLists.Count==1&&c.StepLevel==req.StepLevel&&c.IsUseVirtualAvatar==req.IsUseVirtualAvatar&&
            c.AvatarLists.Select(x=>x.AvatarId).SequenceEqual(Ids(req.AvatarIdLists))&&
            SupportIds(c.SupportAvatarIdLists).SequenceEqual(SupportIds(req.SupportAvatarIdLists))&&
            Ids(c.BoonBuffIdLists).OrderBy(x=>x).SequenceEqual(Ids(req.BoonBuffIdLists).OrderBy(x=>x))&&
            c.ElfId==req.ElfId&&c.ExtraItemId==req.ExtraItemId&&c.AssistantExtraItemId==req.AssistantExtraItemId;
    }
    internal static SelectGodWarChallengeAvatarRsp SelectChallenge(Player p,SelectGodWarChallengeAvatarReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new SelectGodWarChallengeAvatarRsp{GodWarId=req.GodWarId,TaleId=req.TaleId};
            if(req.GodWarId!=1){rsp.retcode=SelectGodWarChallengeAvatarRsp.Retcode.FeatureClose;return rsp;}
            // Story trials and virtual equipment need their own authored setup.
            if(req.TaleId is not (20 or 30)){rsp.retcode=SelectGodWarChallengeAvatarRsp.Retcode.NotOpen;return rsp;}
            if(req.IsUseVirtualAvatar){rsp.retcode=SelectGodWarChallengeAvatarRsp.Retcode.FeatureClose;return rsp;}
            if(req.TaleId==30&&!WeeklyEntryOpen(Clock().ToUnixTimeSeconds())){rsp.retcode=SelectGodWarChallengeAvatarRsp.Retcode.NotOpen;return rsp;}
            var schedule=CurrentCombatSchedule(req.TaleId);
            if(schedule==null){rsp.retcode=SelectGodWarChallengeAvatarRsp.Retcode.AvatarScheduleError;return rsp;}
            var s=Load(p.User);
            if(s.Runs.TryGetValue(req.TaleId,out var existing))
            {
                // Native TryRequestSelectGodWarChallengeAvatarWhileInChallenge
                // resends its saved lineup when returning from the lobby.
                // A matching request resumes; it never rebuilds or resets a run.
                rsp.retcode=SameChallengeSetup(existing,req)?SelectGodWarChallengeAvatarRsp.Retcode.Succ:SelectGodWarChallengeAvatarRsp.Retcode.AlreadySelected;
                return rsp;
            }
            var mainIds=Ids(req.AvatarIdLists);var supports=SupportIds(req.SupportAvatarIdLists);var boons=Ids(req.BoonBuffIdLists);
            if(mainIds.Length!=1||!schedule["MainAvatarList"].Values<uint>().Contains(mainIds[0])){rsp.retcode=SelectGodWarChallengeAvatarRsp.Retcode.AvatarError;return rsp;}
            uint main=mainIds[0];var avatar=p.AvatarList.FirstOrDefault(x=>x.AvatarId==main&&x.Star>0);
            var relation=Table("GodWarRelationData").FirstOrDefault(x=>(uint)x["AvatarID"]==main&&(uint)x["RoleType"]==2);
            if(avatar==null||relation==null||!s.Roles.TryGetValue((uint)relation["RoleID"],out var role)||role.Level==0){rsp.retcode=SelectGodWarChallengeAvatarRsp.Retcode.AvatarError;return rsp;}
            var step=Table("GodWarPunishStep").SingleOrDefault(x=>(uint)x["PunishStepConfigID"]==(uint)schedule["PunishStepID"]&&(uint)x["PunishStep"]==req.StepLevel);
            if(step==null){rsp.retcode=SelectGodWarChallengeAvatarRsp.Retcode.PunishBuffError;return rsp;}
            if(req.TaleId==20)
            {
                var mainMeta=Table("GodWarMainAvatar").Single(x=>(uint)x["MainAvatarID"]==main);
                var unlock=mainMeta["StepUnlockMissionMap"].Where(x=>(uint)x["StepID"]==req.StepLevel).Select(x=>(uint)x["MissionID"]);
                if(!MissionsClosed(p,unlock)){rsp.retcode=SelectGodWarChallengeAvatarRsp.Retcode.AvatarMissionNotFinish;return rsp;}
            }
            if(!ValidSupport(supports,schedule,step,main)){rsp.retcode=SelectGodWarChallengeAvatarRsp.Retcode.SupportAvatarError;return rsp;}
            if(req.ElfId!=0&&(p.Elfs==null||!p.Elfs.Elves.Any(x=>x.ElfId==req.ElfId))){rsp.retcode=SelectGodWarChallengeAvatarRsp.Retcode.ElfError;return rsp;}
            if(!ValidExtra(p,req.ExtraItemId,1)||!ValidExtra(p,req.AssistantExtraItemId,2)){rsp.retcode=SelectGodWarChallengeAvatarRsp.Retcode.ExtraItemError;return rsp;}
            uint challengeId=(uint)step["ChallengeID"];var challenge=Table("GodWarChallenge").SingleOrDefault(x=>(uint)x["ChallengeID"]==challengeId);
            var allowed=challenge?["BuffList"]?.Values<uint>().ToHashSet()??new HashSet<uint>();
            long boonCost=0;
            foreach(uint id in boons)
            {
                var buff=Table("GodWarChallengeBuff").SingleOrDefault(b=>(uint)b["BuffID"]==id);
                if(buff==null){rsp.retcode=SelectGodWarChallengeAvatarRsp.Retcode.BoonBuffError;return rsp;}
                boonCost+=(long)buff["BuffCost"];
            }
            if(boons.Distinct().Count()!=boons.Length||boons.Any(x=>!allowed.Contains(x))||boonCost>((long?)challenge?["BuffCostLimit"]??0))
            {rsp.retcode=SelectGodWarChallengeAvatarRsp.Retcode.BoonBuffError;return rsp;}
            var initial=step["PunishStepInitialBuffList"].Values<uint>().ToArray();
            if(initial.Any(x=>!Table("GodWarPunishBuff").Any(b=>(uint)b["PunishBuffID"]==x))){rsp.retcode=SelectGodWarChallengeAvatarRsp.Retcode.PunishBuffError;return rsp;}
            var weapon=p.Equipment.WeaponList.FirstOrDefault(x=>avatar.WeaponUniqueId!=0&&x.UniqueId==avatar.WeaponUniqueId);
            var setup=new GodWarChallenge{StepLevel=req.StepLevel,SupportAvatarIdLists=supports,ElfId=req.ElfId,ExtraItemId=req.ExtraItemId,AssistantExtraItemId=req.AssistantExtraItemId,
                PunishBuffIdLists=initial,BoonBuffIdLists=boons,RandomSeed=(uint)RandomNumberGenerator.GetInt32(1,int.MaxValue),IsUseVirtualAvatar=false};
            // Client declares MAX_AVATAR_HPPERCENT=100, not 10000.
            setup.AvatarLists.Add(new GodWarChallengeAvatar{AvatarId=main,HpPercent=100,IsVirtual=false,WeaponId=weapon?.Id??0,WeaponLevel=weapon?.Level??0,
                Stigmata1=SnapshotStigmata(p,avatar.StigmataUniqueId1),Stigmata2=SnapshotStigmata(p,avatar.StigmataUniqueId2),Stigmata3=SnapshotStigmata(p,avatar.StigmataUniqueId3)});
            var run=new ChallengeRun{AvatarScheduleId=(uint)schedule["AvatarScheduleID"],StartedAt=Clock().ToUnixTimeSeconds(),AdditionalStoryPortal=HasAdditionalStoryPortal(p,s),Challenge=setup};
            if(!Commit(p,s,()=>s.Runs[req.TaleId]=run))rsp.retcode=SelectGodWarChallengeAvatarRsp.Retcode.Fail;
            return rsp;
        }
    }
    internal sealed class ChallengeEntryResult
    {
        internal SelectGodWarChallengeAvatarRsp Reply;
        internal ResetGodWarChallengeRsp Settlement;
    }
    internal static ChallengeEntryResult SelectChallengeForClient(Player p,SelectGodWarChallengeAvatarReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var result=new ChallengeEntryResult{Reply=SelectChallenge(p,req)};
            if(result.Reply.retcode!=SelectGodWarChallengeAvatarRsp.Retcode.Succ)return result;
            var state=Load(p.User);
            if(!state.Runs.TryGetValue(req.TaleId,out var run)||run.Deep is not {} deep||
                deep.PendingStage!=0||deep.Phase is not ("failed" or "complete"))return result;
            // Re-entering a finished/abandoned challenge must settle it, not
            // acknowledge a resume and leave the scene loader waiting forever.
            // Reuse the normal atomic settlement and its real response payload.
            // Active runs remain read-only resumes; no new run is created here.
            var settled=SettleDeep(p,state,req.TaleId,run);
            if(settled.retcode!=ResetGodWarChallengeRsp.Retcode.Succ)
            {result.Reply.retcode=SelectGodWarChallengeAvatarRsp.Retcode.Fail;return result;}
            result.Settlement=settled;
            result.Reply.retcode=SelectGodWarChallengeAvatarRsp.Retcode.ForceSettle;
            return result;
        }
    }
    internal static ChangeGodWarChallengeSupportAvatarRsp ChangeSupport(Player p,ChangeGodWarChallengeSupportAvatarReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new ChangeGodWarChallengeSupportAvatarRsp{GodWarId=req.GodWarId,TaleId=req.TaleId};
            if(req.GodWarId!=1){rsp.retcode=ChangeGodWarChallengeSupportAvatarRsp.Retcode.FeatureClose;return rsp;}
            var s=Load(p.User);
            if(!s.Runs.TryGetValue(req.TaleId,out var run)){rsp.retcode=ChangeGodWarChallengeSupportAvatarRsp.Retcode.NotOpen;return rsp;}
            var schedule=Table("GodWarAvatarTaleSchedule").SingleOrDefault(x=>(uint)x["AvatarScheduleID"]==run.AvatarScheduleId);
            var step=schedule==null?null:Table("GodWarPunishStep").SingleOrDefault(x=>(uint)x["PunishStepConfigID"]==(uint)schedule["PunishStepID"]&&(uint)x["PunishStep"]==run.Challenge.StepLevel);
            if(step==null||run.Challenge.AvatarLists.Count!=1){rsp.retcode=ChangeGodWarChallengeSupportAvatarRsp.Retcode.NotOpen;return rsp;}
            var ids=SupportIds(req.SupportAvatarIdLists);
            if(ids.SequenceEqual(SupportIds(run.Challenge.SupportAvatarIdLists))&&
                req.ExtraItemId==run.Challenge.ExtraItemId&&req.AssistantExtraItemId==run.Challenge.AssistantExtraItemId)
            {
                rsp.SupportAvatarIdLists=ids;rsp.ExtraItemId=req.ExtraItemId;rsp.AssistantExtraItemId=req.AssistantExtraItemId;
                return rsp;
            }
            if(!DeepCanChangeLineup(run)){rsp.retcode=ChangeGodWarChallengeSupportAvatarRsp.Retcode.NotOpen;return rsp;}
            if(!ValidSupport(ids,schedule,step,run.Challenge.AvatarLists[0].AvatarId)){rsp.retcode=ChangeGodWarChallengeSupportAvatarRsp.Retcode.SupportAvatarError;return rsp;}
            if(!ValidExtra(p,req.ExtraItemId,1)||!ValidExtra(p,req.AssistantExtraItemId,2)){rsp.retcode=ChangeGodWarChallengeSupportAvatarRsp.Retcode.ExtraItemError;return rsp;}
            if(!Commit(p,s,()=>{run.Challenge.SupportAvatarIdLists=ids;run.Challenge.ExtraItemId=req.ExtraItemId;run.Challenge.AssistantExtraItemId=req.AssistantExtraItemId;CapturePortalSigilRefresh(run);} ))
            {rsp.retcode=ChangeGodWarChallengeSupportAvatarRsp.Retcode.Fail;return rsp;}
            rsp.SupportAvatarIdLists=ids;rsp.ExtraItemId=req.ExtraItemId;rsp.AssistantExtraItemId=req.AssistantExtraItemId;return rsp;
        }
    }
    internal static ResetGodWarChallengeRsp ResetChallenge(Player p,ResetGodWarChallengeReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new ResetGodWarChallengeRsp{GodWarId=req.GodWarId,TaleId=req.TaleId};
            if(req.GodWarId!=1){rsp.retcode=ResetGodWarChallengeRsp.Retcode.FeatureClose;return rsp;}
            var s=Load(p.User);
            if(!s.Runs.TryGetValue(req.TaleId,out var run)){rsp.retcode=ResetGodWarChallengeRsp.Retcode.NotOpen;return rsp;}
            if(req.Channel is not (ResetGodWarChallengeReq._ResetChannel.UserOperation or ResetGodWarChallengeReq._ResetChannel.ForceSettle)){rsp.retcode=ResetGodWarChallengeRsp.Retcode.Fail;return rsp;}
            if(run.Deep!=null)return SettleDeep(p,s,req.TaleId,run);
            // An unplayed setup is an abandonment, never a victory/reward claim.
            // The event engine must add a real settlement path before scored
            // or completed runs may use this branch.
            if(run.Challenge.ChallengeScore!=0){rsp.retcode=ResetGodWarChallengeRsp.Retcode.Fail;return rsp;}
            if(!Commit(p,s,()=>s.Runs.Remove(req.TaleId))){rsp.retcode=ResetGodWarChallengeRsp.Retcode.Fail;return rsp;}
            rsp.AvatarLists.AddRange(CopyChallenge(run.Challenge).AvatarLists);rsp.IsChallengeSucc=false;rsp.IsUseVirtualAvatar=run.Challenge.IsUseVirtualAvatar;
            return rsp;
        }
    }
    private static void ApplyChallengeSetups(State s,GodWar realm)
    {
        foreach(var tale in realm.TaleLists)if(s.Runs.TryGetValue(tale.TaleId,out var run)){tale.AvatarScheduleId=run.AvatarScheduleId;tale.Challenge=CopyChallenge(run.Challenge);ApplyDeep(run,tale);}
    }
    internal static void PushChallengeSetup(Session session,uint taleId,bool syncCurrency=true)
    {
        var tale=GodWarService.Build(session.Player).GodWarLists.Single().TaleLists.Single(x=>x.TaleId==taleId);
        // This path changes the virtual realm wallet, not physical equipment.
        // Explicit GetEquipmentDataReq and real inventory grants retain full sync.
        if(syncCurrency)session.Send(Packet.FromProto(DeepCurrencySnapshot(session.Player),CmdId.GetEquipmentDataRsp));
        session.Send(Packet.FromProto(new SyncGodWarTaleNotify{Tale=tale},CmdId.SyncGodWarTaleNotify));
    }
}
