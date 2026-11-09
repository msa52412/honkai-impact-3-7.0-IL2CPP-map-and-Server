using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SelectGodWarChallengeAvatarReq)]
internal class SelectGodWarChallengeAvatarReqHandler : IPacketHandler
{
    public void Handle(Session session,Packet packet)
    {
        var req=packet.GetDecodedBody<SelectGodWarChallengeAvatarReq>();var result=GodWarProgression.SelectChallengeForClient(session.Player,req);var rsp=result.Reply;
        if(rsp.retcode==0)GodWarProgression.PushChallengeSetup(session,req.TaleId);
        session.Send(Packet.FromProto(rsp,CmdId.SelectGodWarChallengeAvatarRsp));
        if(result.Settlement!=null)
        {
            GodWarProgression.Push(session);GodWarProgression.PushChallengeSetup(session,req.TaleId);
            session.Send(Packet.FromProto(MissionDataBuilder.Build(session.Player),CmdId.GetMissionDataRsp));
            session.Send(Packet.FromProto(result.Settlement,CmdId.ResetGodWarChallengeRsp));
        }
    }
}
[PacketCmdId(CmdId.ChangeGodWarChallengeSupportAvatarReq)]
internal class ChangeGodWarChallengeSupportAvatarReqHandler : IPacketHandler
{
    public void Handle(Session session,Packet packet)
    {
        var req=packet.GetDecodedBody<ChangeGodWarChallengeSupportAvatarReq>();var rsp=GodWarProgression.ChangeSupport(session.Player,req);
        if(rsp.retcode==0)GodWarProgression.PushChallengeSetup(session,req.TaleId);
        session.Send(Packet.FromProto(rsp,CmdId.ChangeGodWarChallengeSupportAvatarRsp));
    }
}
[PacketCmdId(CmdId.ResetGodWarChallengeReq)]
internal class ResetGodWarChallengeReqHandler : IPacketHandler
{
    public void Handle(Session session,Packet packet)
    {
        var req=packet.GetDecodedBody<ResetGodWarChallengeReq>();var rsp=GodWarProgression.ResetChallenge(session.Player,req);
        if(rsp.retcode==0){GodWarProgression.Push(session);GodWarProgression.PushChallengeSetup(session,req.TaleId);session.Send(Packet.FromProto(MissionDataBuilder.Build(session.Player),CmdId.GetMissionDataRsp));}
        session.Send(Packet.FromProto(rsp,CmdId.ResetGodWarChallengeRsp));
    }
}
