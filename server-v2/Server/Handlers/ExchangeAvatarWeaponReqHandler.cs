using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.ExchangeAvatarWeaponReq)]
internal class ExchangeAvatarWeaponReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p){var req=p.GetDecodedBody<ExchangeAvatarWeaponReq>();var r=EquipmentExchange.Execute(s.Player,req);s.Send(Packet.FromProto(r,CmdId.ExchangeAvatarWeaponRsp));if(r.retcode==0)s.ProcessPacket(Packet.FromProto(new GetAvatarDataReq{AvatarIdLists=new[]{req.AvatarId1,req.AvatarId2}},CmdId.GetAvatarDataReq));}
}
