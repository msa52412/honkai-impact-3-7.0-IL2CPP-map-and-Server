using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.GetGachaProbReq)]
internal sealed class GetGachaProbReqHandler:IPacketHandler
{
    public void Handle(Session session,Packet packet)
    {
        var req=packet.GetDecodedBody<GetGachaProbReq>();GetGachaProbRsp rsp;
        try {rsp=GachaProbabilities.Build(session.Player.Equipment,req.GachaType);}
        catch(System.Exception error){Packet.c.Warn("[GachaProb] Build failed: "+error.Message);rsp=new GetGachaProbRsp{retcode=GetGachaProbRsp.Retcode.Fail,GachaType=req.GachaType};}
        session.Send(Packet.FromProto(rsp,CmdId.GetGachaProbRsp));
    }
}
