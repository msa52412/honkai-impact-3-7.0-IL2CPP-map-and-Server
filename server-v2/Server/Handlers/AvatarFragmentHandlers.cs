using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.AvatarFragmentTransformReq)]
internal class AvatarFragmentTransformReqHandler : IPacketHandler
{
 public void Handle(Session s, Packet p)
 {
  var rsp = AvatarFragmentGameplay.Transform(s.Player, p.GetDecodedBody<AvatarFragmentTransformReq>());
  s.Send(Packet.FromProto(rsp, CmdId.AvatarFragmentTransformRsp));
  if (rsp.retcode == AvatarFragmentTransformRsp.Retcode.Succ) {
   DailyGameplay.PushInventory(s);
   s.ProcessPacket(Packet.FromProto(new GetAvatarDataReq(), CmdId.GetAvatarDataReq));
  }
 }
}
