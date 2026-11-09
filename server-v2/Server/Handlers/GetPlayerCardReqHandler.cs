using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetPlayerCardReq)]
internal class GetPlayerCardReqHandler : IPacketHandler
{
    public void Handle(Session session, Packet packet)
    {
        session.Send(Packet.FromProto(PlayerCardService.Build(session.Player.User), CmdId.GetPlayerCardRsp));
    }
}
