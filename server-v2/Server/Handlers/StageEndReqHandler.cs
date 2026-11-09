using System;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using ProtoBuf;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.StageEndReq)]
internal class StageEndReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		StageEndReq decodedBody = packet.GetDecodedBody<StageEndReq>();
		StageEndReqBody stageEndReqBody = Serializer.Deserialize<StageEndReqBody>(decodedBody.Body.AsSpan());
        if(GodWarProgression.HandlesPlotBattle(stageEndReqBody.StageId)){var story=GodWarProgression.EndPlotBattle(session.Player,stageEndReqBody);if(story.retcode==0){GodWarProgression.Push(session);session.Send(Packet.FromProto(MissionDataBuilder.Build(session.Player),CmdId.GetMissionDataRsp));GodWarProgression.PushLobby(session,GodWarService.Build(session.Player).GodWarLists[0].LobbyId);}session.Send(Packet.FromProto(story,CmdId.StageEndRsp));return;}
        if(GodWarProgression.HandlesShallow(stageEndReqBody.StageId)){var realm=GodWarProgression.EndShallow(session.Player,stageEndReqBody);if(realm.retcode==0)GodWarProgression.Push(session);session.Send(Packet.FromProto(realm,CmdId.StageEndRsp));if(realm.retcode==0)session.ProcessPacket(Packet.FromProto(new GetStageDataReq(),CmdId.GetStageDataReq));return;}
        if (GodWarProgression.HandlesDeep(stageEndReqBody.StageId))
        {
            uint tale = GodWarProgression.ActiveDeepTale(session.Player, stageEndReqBody.StageId);
            var rsp = GodWarProgression.EndDeep(session.Player, stageEndReqBody, out var settlement);
            if(settlement!=null)
            {
                session.Send(Packet.FromProto(rsp,CmdId.StageEndRsp));
                session.Send(Packet.FromProto(settlement,CmdId.ResetGodWarChallengeRsp));
                GodWarProgression.Push(session);
                GodWarProgression.PushChallengeSetup(session,settlement.TaleId);
                session.Send(Packet.FromProto(MissionDataBuilder.Build(session.Player),CmdId.GetMissionDataRsp));
                return;
            }

            bool accepted = rsp.retcode == 0;
            var transition = accepted && stageEndReqBody.EndStatus == StageEndStatus.StageWin
                ? GodWarProgression.DeepPortalTransitionEvents(session.Player, tale, stageEndReqBody.GodWarStatus?.TeleportId ?? 0)
                : Array.Empty<GodWarEventInfo>();
            if (accepted)
            {
                GodWarProgression.PushChallengeSetup(session, tale);
                GodWarProgression.PushDeepEvents(session, tale, transition);
            }
            session.Send(Packet.FromProto(rsp, CmdId.StageEndRsp));
            if (accepted && transition.Length == 0)
                GodWarProgression.PushDeepResume(session, tale);
            return;
        }
        if(ThemeWantedGameplay.Handles(stageEndReqBody.StageId)){var wanted=ThemeWantedGameplay.End(session.Player,stageEndReqBody);session.Send(Packet.FromProto(wanted,CmdId.StageEndRsp));if(wanted.retcode==0){DailyGameplay.PushInventory(session);session.Send(Packet.FromProto(ThemeWantedGameplay.Build(session.Player),CmdId.GetThemeWantedRsp));}return;}
        if(ArmadaBossGameplay.Handles(stageEndReqBody.StageId)){var boss=ArmadaBossGameplay.Finish(session.Player,stageEndReqBody);session.Send(Packet.FromProto(boss,CmdId.StageEndRsp));if(boss.retcode==0)session.Send(Packet.FromProto(ArmadaBossGameplay.Build(session.Player),CmdId.GetArmadaStageScoreActivityRsp));return;}
		StageDataExcel stageDataExcel = BaseExcelReader<StageData, StageDataExcel>.GetInstance().FromId((int)stageEndReqBody.StageId);
		if (stageDataExcel == null)
		{
			session.c.Error("StageData Excel Is Bad Please Fix");
			session.Send(Packet.FromProto(new StageEndRsp
			{
				retcode = StageEndRsp.Retcode.StageError
			}, CmdId.StageEndRsp));
			return;
		}
        if (WeekdayGameplay.Activity(stageEndReqBody.StageId) != null)
        {
            var weekday=WeekdayGameplay.End(session.Player,stageEndReqBody);
            session.Send(Packet.FromProto(weekday,CmdId.StageEndRsp));
            if(weekday.retcode==0){DailyGameplay.PushInventory(session);WeekdayGameplay.Push(session);}
            return;
        }
		if (stageDataExcel.LevelId == 10101)
		{
			session.Player.User.IsFirstLogin = false;
		}
		StageEndRsp stageEndRsp = new StageEndRsp
		{
			retcode = StageEndRsp.Retcode.Succ,
			StageId = stageEndReqBody.StageId,
			EndStatus = stageEndReqBody.EndStatus
		};
		if (stageEndReqBody.EndStatus == StageEndStatus.StageWin)
		{
            DailyGameplay.Atomic(session.Player,()=>{
                var state=DailyGameplay.State(session.Player);
                if(state.GeneralPendingStage==stageEndReqBody.StageId&&DailyGameplay.Clock()>=state.GeneralPendingSince&&DailyGameplay.Clock()-state.GeneralPendingSince<=21600){
                    foreach(int tag in (stageDataExcel.Tag??Array.Empty<int>()).Distinct())DailyGameplay.Add(session.Player,10051,tag);
                    if(stageDataExcel.Type==2)DailyGameplay.Add(session.Player,22,0);
                    state.GeneralPendingStage=0;
                }
            });
			EquipmentScheme equipment = session.Player.Equipment;
			foreach (DropItem dropItemList in stageEndReqBody.DropItemLists)
			{
				equipment.AddMaterial((int)dropItemList.ItemId, (int)dropItemList.Num);
			}
			if (stageEndReqBody.ChallengeIndexLists != null)
			{
				session.Player.User.Hcoin += stageEndReqBody.ChallengeIndexLists.Length * 5;
				stageEndRsp.ChallengeLists.AddRange(stageEndReqBody.ChallengeIndexLists.Select((uint challengeIndex) => new StageChallengeData
				{
					ChallengeIndex = challengeIndex,
					Reward = new Common.Resources.Proto.RewardData
					{
						Hcoin = 5u
					}
				}));
			}
			session.Player.User.AddExp(100);
			session.ProcessPacket(Packet.FromProto(new GetMainDataReq(), CmdId.GetMainDataReq));
			session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(), CmdId.GetEquipmentDataReq));
			session.ProcessPacket(Packet.FromProto(new GetWorldMapDataReq(), CmdId.GetWorldMapDataReq));
			session.ProcessPacket(Packet.FromProto(new GetStageDataReq(), CmdId.GetStageDataReq));
			stageEndRsp.PlayerExpReward = 100u;
			stageEndRsp.AvatarExpReward = stageEndReqBody.AvatarExpReward;
			stageEndRsp.ScoinReward = stageEndReqBody.ScoinReward;
		}
		session.Send(Packet.FromProto(stageEndRsp, CmdId.StageEndRsp));
	}
}

