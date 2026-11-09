using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.GetRecommendMissionPanelListReq)]
internal class GetRecommendMissionPanelListReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  s.Send(Packet.FromProto(MissionDataBuilder.Build(s.Player),CmdId.GetMissionDataRsp));
  BattlePassGameplay.Push(s);
  s.Send(Packet.FromProto(DailyGameplay.Panels(s.Player),CmdId.GetRecommendMissionPanelListRsp));
  DailyGameplay.PushDuty(s);
 }
}
