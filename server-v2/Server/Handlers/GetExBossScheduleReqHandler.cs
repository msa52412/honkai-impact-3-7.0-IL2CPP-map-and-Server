using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.GetExBossScheduleReq)]
internal class GetExBossScheduleReqHandler : IPacketHandler { public void Handle(Session session, Packet packet) { session.Send(Packet.FromProto(ExBossService.GetSchedule(session), CmdId.GetExBossScheduleRsp)); } }