using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.ExBossStageBeginReq)]
internal class ExBossStageBeginReqHandler : IPacketHandler { public void Handle(Session session, Packet packet) { session.Send(Packet.FromProto(ExBossService.Begin(session, packet.GetDecodedBody<ExBossStageBeginReq>()), CmdId.ExBossStageBeginRsp)); } }