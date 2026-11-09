using System;
using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Game;

internal static partial class GodWarProgression
{
    public sealed class PlotBattle
    {
        public uint Stage {get;set;}
        public uint Chapter {get;set;}
        public uint Plot {get;set;}
        public long Since {get;set;}
    }
    internal static bool HandlesPlotBattle(uint stage)=>Table("GodWarPlotStage").Any(x=>(uint)x["StageID"]==stage);
    private static bool IsPlotMission(uint id)=>Table("GodWarPlotStage").Any(x=>(uint)x["MissionID"]==id);
    private static bool IssuedPlot(State state,uint plot,long now)=>state.LobbyDialogue is {} dialogue&&dialogue.Plot==plot&&dialogue.Lobby==CurrentLobby(state)&&now-dialogue.Since is >=0 and <=21600;
    private static void ApplyPlotMissions(Player p,State state,GetMissionDataRsp response,bool legacyClosed)
    {
        long now=Clock().ToUnixTimeSeconds();
        foreach(var row in Table("GodWarPlotStage"))
        {
            uint id=(uint)row["MissionID"],stage=(uint)row["StageID"],plot=(uint)row["PlotID"];
            bool taken=state.StoryClaimed.Contains(id)||MissionTake.Has(p.User.Uid,id),done=taken||state.StoryDone.Contains(id)||state.PlotClears.Contains(stage);
            bool active=IssuedPlot(state,plot,now)||(state.PendingPlotBattle?.Stage==stage&&state.PendingPlotBattle.Chapter==state.Chapter);
            // Do not reset the legacy all-closed account globally. Once a real
            // plot is issued, its unplayed auxiliary guard must no longer be
            // a synthetic FINISH/CLOSE: the native client tests statuses3/5
            // before it sends StageBegin.
            if(legacyClosed&&!active&&!done)continue;
            var mission=response.MissionLists.FirstOrDefault(x=>x.MissionId==id);
            if(mission==null){mission=new Mission{MissionId=id};response.MissionLists.Add(mission);}
            mission.Status=taken?MissionStatus.MissionClose:done?MissionStatus.MissionFinish:active?MissionStatus.MissionDoing:MissionStatus.MissionNotBegin;
            mission.Progress=done?1u:0u;
        }
    }
    internal static StageBeginRsp BeginPlotBattle(Player p,StageBeginReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new StageBeginRsp{StageId=req.StageId,retcode=StageBeginRsp.Retcode.Fail};
            var row=Table("GodWarPlotStage").SingleOrDefault(x=>(uint)x["StageID"]==req.StageId);
            var stage=Table("StageData_Main").SingleOrDefault(x=>(uint)x["levelId"]==req.StageId);
            if(row==null||stage==null){rsp.retcode=StageBeginRsp.Retcode.StageNotExist;return rsp;}
            // These authored plot stages have no entrance charge or fixed
            // account payout. DropList contains999 in-run currencies, not a
            // guaranteed inventory reward. Other definitions need real rules.
            if((uint)stage["staminaCost"]!=0||(uint)stage["costMaterialNum"]!=0||(uint)stage["firstCostMaterialNum"]!=0||(uint)stage["scoinReward"]!=0||(uint)stage["avatarExpReward"]!=0)return rsp;
            if(BaseExcelReader<PlayerLevelData,PlayerLevelDataExcel>.GetInstance().CalculateLevel(p.User.Exp).Level<(uint)stage["unlockPlayerLevel"])
            {rsp.retcode=StageBeginRsp.Retcode.LevelLack;return rsp;}
            var state=Load(p.User);long now=Clock().ToUnixTimeSeconds();uint plot=(uint)row["PlotID"],mission=(uint)row["MissionID"];
            if(state.PlotClears.Contains(req.StageId)||state.StoryDone.Contains(mission)||state.StoryClaimed.Contains(mission)||MissionTake.Has(p.User.Uid,mission))return rsp;
            if(state.PendingPlotBattle is {} pending)
            {
                if(pending.Stage!=req.StageId||pending.Chapter!=state.Chapter||now<pending.Since)return rsp;
            }
            else if(!IssuedPlot(state,plot,now))return rsp;
            var avatars=Ids(req.AvatarIdLists);var elves=Ids(req.ElfIdLists);
            // Native plot entry sends an empty lineup; the stage Lua owns the
            // fixed actors. A supplied owned lineup is allowed, never invented.
            if(avatars.Length>3||avatars.Distinct().Count()!=avatars.Length||avatars.Any(id=>!p.AvatarList.Any(x=>x.AvatarId==id&&x.Star>0))||elves.Length>1||elves.Any(id=>p.Elfs==null||!p.Elfs.Elves.Any(x=>x.ElfId==id)))
            {rsp.retcode=StageBeginRsp.Retcode.AvatarError;return rsp;}
            if(state.PendingPlotBattle!=null&&now-state.PendingPlotBattle.Since<=21600){rsp.retcode=0;return rsp;}
            if(Commit(p,state,()=>state.PendingPlotBattle=new PlotBattle{Stage=req.StageId,Chapter=state.Chapter,Plot=plot,Since=now}))rsp.retcode=0;
            return rsp;
        }
    }
    internal static StageEndRsp EndPlotBattle(Player p,StageEndReqBody req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new StageEndRsp{StageId=req.StageId,EndStatus=req.EndStatus,retcode=StageEndRsp.Retcode.Fail};
            var row=Table("GodWarPlotStage").SingleOrDefault(x=>(uint)x["StageID"]==req.StageId);var state=Load(p.User);var pending=state.PendingPlotBattle;
            if(row==null||pending==null||pending.Stage!=req.StageId||pending.Chapter!=state.Chapter)return rsp;
            long elapsed=Clock().ToUnixTimeSeconds()-pending.Since;
            if(elapsed<0||elapsed>21600||req.StagePassTime>21600){rsp.retcode=StageEndRsp.Retcode.StageTimeError;return rsp;}
            if(req.EndStatus is not (StageEndStatus.StageWin or StageEndStatus.StageExit or StageEndStatus.STAGEAllDEAD))return rsp;
            var status=StoryStatuses(p);bool win=req.EndStatus==StageEndStatus.StageWin;
            if(!Commit(p,state,()=>{
                if(win)
                {
                    state.PlotClears.Add(req.StageId);state.StoryDone.Add((uint)row["MissionID"]);
                    foreach(var mission in BaseExcelReader<MissionData,MissionDataExcel>.GetInstance().All.Where(x=>x.FinishParaInt==req.StageId&&x.FinishWay is 5 or 10051))
                        if(IsStoryMission((uint)mission.Id)&&status.GetValueOrDefault((uint)mission.Id)==MissionStatus.MissionDoing)state.StoryDone.Add((uint)mission.Id);
                }
                state.PendingPlotBattle=null;
            }))return rsp;
            rsp.retcode=0;rsp.IsFirstWin=win;return rsp;
        }
    }
}
