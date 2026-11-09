using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetGodWarLobbyReq)]
internal class GetGodWarLobbyReqHandler:IPacketHandler
{
    public void Handle(Session s,Packet p){s.Send(Packet.FromProto(GodWarProgression.Lobby(s.Player,p.GetDecodedBody<GetGodWarLobbyReq>()),CmdId.GetGodWarLobbyRsp));}
}
[PacketCmdId(CmdId.FinishGodWarLobbyActionReq)]
internal class FinishGodWarLobbyActionReqHandler:IPacketHandler
{
    public void Handle(Session s,Packet p){var req=p.GetDecodedBody<FinishGodWarLobbyActionReq>();var rsp=GodWarProgression.FinishLobbyAction(s.Player,req);s.Send(Packet.FromProto(rsp,CmdId.FinishGodWarLobbyActionRsp));if(rsp.retcode==0){s.Send(Packet.FromProto(MissionDataBuilder.Build(s.Player),CmdId.GetMissionDataRsp));GodWarProgression.PushLobby(s,req.LobbyId);}}
}
[PacketCmdId(CmdId.SkipGodWarStoryNodeReq)]
internal class SkipGodWarStoryNodeReqHandler:IPacketHandler
{
    public void Handle(Session s,Packet p){var req=p.GetDecodedBody<SkipGodWarStoryNodeReq>();var rsp=GodWarProgression.SkipStoryNode(s.Player,req);s.Send(Packet.FromProto(rsp,CmdId.SkipGodWarStoryNodeRsp));if(rsp.retcode==0){GodWarProgression.Push(s);s.Send(Packet.FromProto(MissionDataBuilder.Build(s.Player),CmdId.GetMissionDataRsp));GodWarProgression.PushLobby(s,GodWarService.Build(s.Player).GodWarLists[0].LobbyId);}}
}
