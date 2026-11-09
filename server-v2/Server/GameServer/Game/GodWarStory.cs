using System;
using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json.Linq;
using RewardData=Common.Resources.Proto.RewardData;

namespace PemukulPaku.GameServer.Game;

internal static partial class GodWarProgression
{
    private static uint[] NodeMissions(JToken node)=>node["AssociatedMissionList"].Values<uint>().Where(x=>x!=0).Distinct().ToArray();
    internal static bool IsStoryMission(uint id)=>IsPlotMission(id)||Table("GodWarMainMissionNode").Any(x=>NodeMissions(x).Contains(id));
    internal static void ApplyStoryMissions(Player p,GetMissionDataRsp response,bool preserveLegacyAllClosed)
    {
        lock(Gate)
        {
            var s=Load(p.User);var closed=response.MissionLists.Where(x=>x.Status==MissionStatus.MissionClose).Select(x=>x.MissionId).ToHashSet();
            closed.UnionWith(s.StoryDone);closed.UnionWith(s.StoryClaimed);
            var nodes=Table("GodWarMainMissionNode");
            foreach(var node in nodes)
            {
                uint parent=(uint)node["PreNodeID"];var previous=nodes.SingleOrDefault(x=>(uint)x["NodeID"]==parent);
                bool active=(parent==0||(previous!=null&&NodeMissions(previous).All(closed.Contains)))&&((long?)node["UnlockTime"]?["TimeStampForBakedReader"]??0)<=Clock().ToUnixTimeSeconds();
                foreach(uint id in NodeMissions(node))
                {
                    if(preserveLegacyAllClosed&&!s.StoryDone.Contains(id)&&!s.StoryClaimed.Contains(id))continue;
                    bool taken=s.StoryClaimed.Contains(id)||MissionTake.Has(p.User.Uid,id);bool done=s.StoryDone.Contains(id)||taken;
                    var row=response.MissionLists.FirstOrDefault(x=>x.MissionId==id);if(row==null){row=new Mission{MissionId=id};response.MissionLists.Add(row);}
                    row.Status=taken?MissionStatus.MissionClose:done?MissionStatus.MissionFinish:active?MissionStatus.MissionDoing:MissionStatus.MissionNotBegin;
                    row.Progress=done?(uint)(BaseExcelReader<MissionData,MissionDataExcel>.GetInstance().FromId((int)id)?.TotalProgress??1):0;
                }
            }
            ApplyPlotMissions(p,s,response,preserveLegacyAllClosed);
        }
    }
    internal static SkipGodWarStoryNodeRsp SkipStoryNode(Player p,SkipGodWarStoryNodeReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new SkipGodWarStoryNodeRsp{GodWarId=req.GodWarId,NodeId=req.NodeId};
            if(req.GodWarId!=1){rsp.retcode=SkipGodWarStoryNodeRsp.Retcode.FeatureClose;return rsp;}
            var row=Table("GodWarMainMissionNode").SingleOrDefault(x=>(uint)x["NodeID"]==req.NodeId);
            if(row==null){rsp.retcode=SkipGodWarStoryNodeRsp.Retcode.NodeNotExist;return rsp;}
            var s=Load(p.User);
            if((uint)row["ChapterID"]!=s.Chapter){rsp.retcode=SkipGodWarStoryNodeRsp.Retcode.ChapterMismatch;return rsp;}
            uint material=(uint)row["SkipCostMaterialID"],amount=(uint)row["SkipCostMaterialNum"];var missions=NodeMissions(row);
            if(material==0||amount==0||missions.Length==0){rsp.retcode=SkipGodWarStoryNodeRsp.Retcode.NodeSkipInvalid;return rsp;}
            var status=StoryStatuses(p);
            if(s.SkippedStoryNodes.Contains(req.NodeId)||missions.Any(id=>Complete(id,s,status))){rsp.retcode=SkipGodWarStoryNodeRsp.Retcode.MissionStatusError;return rsp;}
            uint parent=(uint)row["PreNodeID"];var before=Table("GodWarMainMissionNode").SingleOrDefault(x=>(uint)x["NodeID"]==parent);
            if((parent!=0&&(before==null||NodeMissions(before).Any(id=>!Complete(id,s,status))))||missions.Any(id=>status.GetValueOrDefault(id)!=MissionStatus.MissionDoing)||((long?)row["UnlockTime"]?["TimeStampForBakedReader"]??0)>Clock().ToUnixTimeSeconds())
            {rsp.retcode=SkipGodWarStoryNodeRsp.Retcode.MissionStatusError;return rsp;}
            if(Held(p,material)<amount){rsp.retcode=SkipGodWarStoryNodeRsp.Retcode.MaterialLack;return rsp;}
            if(!Commit(p,s,()=>{Debit(p,material,amount);s.StoryDone.UnionWith(missions);s.SkippedStoryNodes.Add(req.NodeId);} ))rsp.retcode=SkipGodWarStoryNodeRsp.Retcode.Fail;
            return rsp;
        }
    }
    private static DailyGameplay.Plan StoryReward(uint mission)
    {
        var row=BaseExcelReader<MissionData,MissionDataExcel>.GetInstance().FromId((int)mission)??throw new InvalidOperationException("Missing story mission "+mission);
        if(row.RewardId==0)return new DailyGameplay.Plan();
        var reward=BaseExcelReader<Common.Utils.ExcelReader.RewardData,RewardDataExcel>.GetInstance().FromId(row.RewardId)??throw new InvalidOperationException("Missing story reward "+row.RewardId);
        bool noItems=new[]{reward.RewardItem1Id,reward.RewardItem2Id,reward.RewardItem3Id,reward.RewardItem4Id,reward.RewardItem5Id,reward.RewardItem6Id}.All(x=>x==0);
        if(noItems&&reward.RewardExp==0&&reward.RewardHCoin==0&&reward.RewardStamina==0&&reward.RewardFriendPoint==0&&reward.RewardDutyPoint==0)return new DailyGameplay.Plan();
        return DailyGameplay.Reward(row.RewardId);
    }
    internal static GetMissionRewardRsp.Retcode TakeStoryMissions(Player p,uint[] ids,out RewardData reward,out uint[] granted)
    {
        reward=new RewardData();granted=Array.Empty<uint>();
        lock(p.Equipment)lock(Gate)
        {
            if(ids==null||ids.Length==0||ids.Length>100||ids.Distinct().Count()!=ids.Length||ids.Any(x=>!IsStoryMission(x)))return GetMissionRewardRsp.Retcode.MissionIdError;
            var s=Load(p.User);_=MissionTake.TakenIds(p.User.Uid);
            if(ids.Any(x=>!s.StoryDone.Contains(x)||s.StoryClaimed.Contains(x)||MissionTake.Has(p.User.Uid,x)))return GetMissionRewardRsp.Retcode.MissionStatusError;
            DailyGameplay.Plan plan;try{plan=DailyGameplay.Combine(ids.Select(StoryReward));DailyGameplay.Validate(p,plan);}catch(Exception ex){Packet.c.Warn("[GodWar] story mission reward validation: "+ex.Message);return GetMissionRewardRsp.Retcode.EquipmentMaterialLimit;}
            RewardData paid=null;if(!Commit(p,s,()=>{paid=DailyGameplay.Grant(p,plan);s.StoryClaimed.UnionWith(ids);} ))return GetMissionRewardRsp.Retcode.Fail;
            reward=paid;granted=ids;return 0;
        }
    }
}
