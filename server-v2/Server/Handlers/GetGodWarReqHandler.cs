using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.GetGodWarReq)]
internal class GetGodWarReqHandler : IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var req=p.GetDecodedBody<GetGodWarReq>();
  if(req.GodWarId is 0 or 1){if(!GodWarProgression.RestoreLobbyExtras(s.Player,out bool inventoryChanged)){s.Send(Packet.FromProto(new GetGodWarRsp{retcode=GetGodWarRsp.Retcode.Fail},CmdId.GetGodWarRsp));return;}if(inventoryChanged)s.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(),CmdId.GetEquipmentDataReq));}
  if(req.GodWarId is 0 or 1){var photos=GodWarProgression.CollectionsReply(s.Player);if(photos.retcode!=0){s.Send(Packet.FromProto(new GetGodWarRsp{retcode=GetGodWarRsp.Retcode.Fail},CmdId.GetGodWarRsp));return;}s.Send(Packet.FromProto(photos,CmdId.GetPhotoDataRsp));}
  var rsp=req.GodWarId is 0 or 1?GodWarService.Build(s.Player):new GetGodWarRsp{retcode=GetGodWarRsp.Retcode.NotOpen};
  s.Send(Packet.FromProto(rsp,CmdId.GetGodWarRsp));
 }
}
