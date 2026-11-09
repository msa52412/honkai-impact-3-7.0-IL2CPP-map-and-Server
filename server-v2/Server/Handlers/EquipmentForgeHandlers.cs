using System;
using System.Linq;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.GetEquipmentForgeDataReq)]
internal class GetEquipmentForgeDataReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p)=>s.Send(Packet.FromProto(EquipmentForgeGameplay.Build(s.Player),CmdId.GetEquipmentForgeDataRsp));
}
[PacketCmdId(CmdId.ForgeEquipmentReq)]
internal class ForgeEquipmentReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p){var req=p.GetDecodedBody<ForgeEquipmentReq>();var rsp=EquipmentForgeGameplay.Forge(s.Player,req);s.Send(Packet.FromProto(rsp,CmdId.ForgeEquipmentRsp));if(rsp.retcode==0){var used=req.ConsumeItemList?.ItemLists;var ws=used?.Where(x=>x.Type==EquipmentType.EquipmentWeapon).Select(x=>x.IdOrUniqueId).ToArray()??Array.Empty<uint>();var ss=used?.Where(x=>x.Type==EquipmentType.EquipmentStigmata).Select(x=>x.IdOrUniqueId).ToArray()??Array.Empty<uint>();if(ws.Length+ss.Length>0)s.Send(Packet.FromProto(new DelEquipmentNotify{WeaponUniqueIdLists=ws,StigmataUniqueIdLists=ss},CmdId.DelEquipmentNotify));DailyGameplay.PushInventory(s);s.Send(Packet.FromProto(EquipmentForgeGameplay.Build(s.Player),CmdId.GetEquipmentForgeDataRsp));}}
}
