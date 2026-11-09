using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.GetExBossRankReq)]
internal class GetExBossRankReqHandler : IPacketHandler { public void Handle(Session session, Packet packet) { session.Send(Packet.FromProto(ExBossService.GetRank(session, packet.GetDecodedBody<GetExBossRankReq>()), CmdId.GetExBossRankRsp)); } }