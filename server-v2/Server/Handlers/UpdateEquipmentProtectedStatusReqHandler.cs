using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.UpdateEquipmentProtectedStatusReq)]
internal class UpdateEquipmentProtectedStatusReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var rsp=EquipmentProtection.Update(s.Player,p.GetDecodedBody<UpdateEquipmentProtectedStatusReq>());
  s.Send(Packet.FromProto(rsp,CmdId.UpdateEquipmentProtectedStatusRsp));
  s.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(),CmdId.GetEquipmentDataReq));
 }
}
