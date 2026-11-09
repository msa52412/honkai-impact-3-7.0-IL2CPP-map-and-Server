using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.BuyGachaTicketReq)]
internal sealed class BuyGachaTicketReqHandler:IPacketHandler
{
    public void Handle(Session session,Packet packet)
    {
        var rsp=GachaTransactions.BuyTicket(session.Player,packet.GetDecodedBody<BuyGachaTicketReq>());
        if(rsp.retcode==BuyGachaTicketRsp.Retcode.Succ)
        {
            session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(),CmdId.GetEquipmentDataReq));
            session.Send(Packet.FromProto(new GetMainDataRsp{retcode=GetMainDataRsp.Retcode.Succ,Hcoin=(uint)session.Player.User.Hcoin},CmdId.GetMainDataRsp));
        }
        session.Send(Packet.FromProto(rsp,CmdId.BuyGachaTicketRsp));
    }
}

[PacketCmdId(CmdId.WishWellReq)]
internal sealed class WishWellReqHandler:IPacketHandler
{
    public void Handle(Session session,Packet packet)
    {
        var rsp=GachaTransactions.Wish(session.Player,packet.GetDecodedBody<WishWellReq>());
        if(rsp.retcode==WishWellRsp.Retcode.Succ)
        {
            session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(),CmdId.GetEquipmentDataReq));
            session.Send(Packet.FromProto(GetGachaDisplayReqHandler.Build(session.Player.Equipment),CmdId.GetGachaDisplayRsp));
        }
        session.Send(Packet.FromProto(rsp,CmdId.WishWellRsp));
    }
}
