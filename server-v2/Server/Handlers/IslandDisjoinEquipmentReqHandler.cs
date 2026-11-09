using System;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.IslandDisjoinEquipmentReq)]
internal class IslandDisjoinEquipmentReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var req=p.GetDecodedBody<IslandDisjoinEquipmentReq>();var rsp=EquipmentDisjoin.Execute(s.Player,req);
  s.Send(Packet.FromProto(rsp,CmdId.IslandDisjoinEquipmentRsp));
  if(rsp.retcode==0){s.Send(Packet.FromProto(new DelEquipmentNotify{WeaponUniqueIdLists=req.Type==EquipmentType.EquipmentWeapon?rsp.DisjoinUniqueIdLists:Array.Empty<uint>(),StigmataUniqueIdLists=req.Type==EquipmentType.EquipmentStigmata?rsp.DisjoinUniqueIdLists:Array.Empty<uint>()},CmdId.DelEquipmentNotify));DailyGameplay.PushInventory(s);DailyGameplay.PushDuty(s);}
 }
}
