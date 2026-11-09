using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.StageBeginReq)]
internal class StageBeginReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		StageBeginReq decodedBody = packet.GetDecodedBody<StageBeginReq>();
        if(GodWarProgression.HandlesPlotBattle(decodedBody.StageId)){session.Send(Packet.FromProto(GodWarProgression.BeginPlotBattle(session.Player,decodedBody),CmdId.StageBeginRsp));return;}
        if(GodWarProgression.HandlesShallow(decodedBody.StageId)){session.Send(Packet.FromProto(GodWarProgression.BeginShallow(session.Player,decodedBody),CmdId.StageBeginRsp));return;}
        if(GodWarProgression.HandlesDeep(decodedBody.StageId)){var rsp=GodWarProgression.BeginDeep(session.Player,decodedBody);session.Send(Packet.FromProto(rsp,CmdId.StageBeginRsp));if(rsp.retcode==0)GodWarProgression.PushDeepAfter(session,GodWarProgression.ActiveDeepTale(session.Player,decodedBody.StageId));return;}
        if(ThemeWantedGameplay.Handles(decodedBody.StageId)){var wanted=ThemeWantedGameplay.Begin(session.Player,decodedBody.StageId);Packet.c.Log($"[StageBegin] uid={session.Player.User.Uid} stage={decodedBody.StageId} route=ThemeWanted result={wanted.retcode}");session.Send(Packet.FromProto(wanted,CmdId.StageBeginRsp));return;}
        if(ArmadaBossGameplay.Handles(decodedBody.StageId)){session.Send(Packet.FromProto(ArmadaBossGameplay.Begin(session.Player,decodedBody),CmdId.StageBeginRsp));return;}
		if (WeekdayGameplay.Activity(decodedBody.StageId) != null)
        {
            session.Send(Packet.FromProto(WeekdayGameplay.Begin(session.Player,decodedBody.StageId),CmdId.StageBeginRsp));return;
        }
        StageBeginRsp proto = new StageBeginRsp
		{
			retcode = StageBeginRsp.Retcode.Succ,
			StageId = decodedBody.StageId,
			Progress = 0u,
			IsCollectCheatData = false
		};
        if(WeekdayGameplay.Stage(decodedBody.StageId)==null)proto.retcode=StageBeginRsp.Retcode.StageNotExist;
        else if(!DailyGameplay.Atomic(session.Player,()=>{var state=DailyGameplay.State(session.Player);state.GeneralPendingStage=decodedBody.StageId;state.GeneralPendingSince=DailyGameplay.Clock();}))proto.retcode=StageBeginRsp.Retcode.Fail;
		Packet.c.Log($"[StageBegin] uid={session.Player.User.Uid} stage={decodedBody.StageId} route=General result={proto.retcode}");
		session.Send(Packet.FromProto(proto, CmdId.StageBeginRsp));
	}
}
