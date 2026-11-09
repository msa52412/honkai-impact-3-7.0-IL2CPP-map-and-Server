using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.RefineStigmataRuneReq)]
internal class RefineStigmataRuneReqHandler : IPacketHandler
{
    public void Handle(Session session, Packet packet)
    {
        var req = packet.GetDecodedBody<RefineStigmataRuneReq>();
        var rsp = StigmataRefining.Refine(session.Player.Equipment, req);
        if (rsp.retcode == RefineStigmataRuneRsp.Retcode.Succ)
            session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(), CmdId.GetEquipmentDataReq));
        session.Send(Packet.FromProto(rsp, CmdId.RefineStigmataRuneRsp));
    }
}
