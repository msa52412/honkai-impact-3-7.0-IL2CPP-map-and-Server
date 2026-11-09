using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.GetStageDataReq)]
internal class GetStageDataReqHandler : IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var rsp=WeekdayGameplay.StageList(s.Player);
  GodWarProgression.ApplyShallowStages(s.Player,rsp);
  s.Send(Packet.FromProto(rsp,CmdId.GetStageDataRsp));
 }
}
