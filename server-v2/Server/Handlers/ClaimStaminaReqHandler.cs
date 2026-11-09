using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.ClaimStaminaReq)] internal class ClaimStaminaReqHandler:IPacketHandler {
 public void Handle(Session s,Packet p){var rsp=AdventureService.ClaimStamina(s.Player,p.GetDecodedBody<ClaimStaminaReq>().Stamina);
  s.Send(Packet.FromProto(rsp,CmdId.ClaimStaminaRsp));AdventureService.Push(s);if(rsp.retcode==0)AdventureService.NotifyInventory(s);
 }
}
