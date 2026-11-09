using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.RefreshGodWarTicketReq)]
internal class RefreshGodWarTicketReqHandler : IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var rsp=GodWarProgression.Tickets(s.Player,p.GetDecodedBody<RefreshGodWarTicketReq>());
  if(rsp.retcode==0&&rsp.TicketRefreshLists.Count>0)DailyGameplay.PushInventory(s);
  s.Send(Packet.FromProto(rsp,CmdId.RefreshGodWarTicketRsp));
 }
}
