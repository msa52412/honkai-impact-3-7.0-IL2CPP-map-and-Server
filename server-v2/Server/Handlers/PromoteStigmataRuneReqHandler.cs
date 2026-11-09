using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.PromoteStigmataRuneReq)]
internal class PromoteStigmataRuneReqHandler : IPacketHandler
{
    public void Handle(Session session,Packet packet)
    {
        var req=packet.GetDecodedBody<PromoteStigmataRuneReq>();
        var rsp=StigmataRefining.Promote(session.Player.Equipment,req.UniqueId);
        Packet.c.Warn("[Affix][Promote] stg="+req.UniqueId+" rc="+rsp.retcode);
        if(rsp.retcode==PromoteStigmataRuneRsp.Retcode.Succ)
            session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(),CmdId.GetEquipmentDataReq));
        session.Send(Packet.FromProto(rsp,CmdId.PromoteStigmataRuneRsp));
    }
}
