using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.ExBossSweepReq)]
internal class ExBossSweepReqHandler : IPacketHandler { public void Handle(Session session, Packet packet) { session.Send(Packet.FromProto(ExBossService.Sweep(session, packet.GetDecodedBody<ExBossSweepReq>()), CmdId.ExBossSweepRsp)); ExBossService.Flush(session); session.Send(Packet.FromProto(ExBossService.GetInfo(session), CmdId.GetExBossInfoRsp)); } }