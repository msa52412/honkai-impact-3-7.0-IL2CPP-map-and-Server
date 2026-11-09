using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.ExBossStageEndReq)]
internal class ExBossStageEndReqHandler : IPacketHandler { public void Handle(Session session, Packet packet) { session.Send(Packet.FromProto(ExBossService.End(session, packet.GetDecodedBody<ExBossStageEndReq>()), CmdId.ExBossStageEndRsp)); ExBossService.Flush(session); session.Send(Packet.FromProto(ExBossService.GetInfo(session), CmdId.GetExBossInfoRsp)); } }