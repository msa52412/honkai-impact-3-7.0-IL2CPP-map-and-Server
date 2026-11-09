using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.GetArmadaActivityListReq)] internal class GetArmadaActivityListReqHandler:IPacketHandler
{public void Handle(Session s,Packet p)=>s.Send(Packet.FromProto(ArmadaBossGameplay.Activities(s.Player),CmdId.GetArmadaActivityListRsp));}
