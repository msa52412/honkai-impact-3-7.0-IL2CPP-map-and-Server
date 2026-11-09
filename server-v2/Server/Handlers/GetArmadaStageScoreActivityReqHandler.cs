using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.GetArmadaStageScoreActivityReq)] internal class GetArmadaStageScoreActivityReqHandler:IPacketHandler
{public void Handle(Session s,Packet p)=>s.Send(Packet.FromProto(ArmadaBossGameplay.Build(s.Player),CmdId.GetArmadaStageScoreActivityRsp));}
