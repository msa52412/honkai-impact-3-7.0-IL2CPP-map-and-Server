using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.EquipmentPowerUpReq)]
internal class EquipmentPowerUpReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p){var rsp=DailyActions.PowerUp(s.Player,p.GetDecodedBody<EquipmentPowerUpReq>());s.Send(Packet.FromProto(rsp,CmdId.EquipmentPowerUpRsp));if(rsp.retcode==0){DailyGameplay.PushInventory(s);s.Send(Packet.FromProto(MissionDataBuilder.Build(s.Player),CmdId.GetMissionDataRsp));}}
}
