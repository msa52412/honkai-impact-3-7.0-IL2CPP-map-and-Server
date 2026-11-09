using Common.Database;
using Common.Resources.Proto;
namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetGachaLogReq)]
internal sealed class GetGachaLogReqHandler:IPacketHandler
{
    public void Handle(Session session,Packet packet)
    {
        GetGachaLogRsp rsp;
        try {rsp=GachaRecords.Read(session.Player.User.Uid);}
        catch(System.Exception error){Packet.c.Warn("[GachaLog] Read failed: "+error.Message);rsp=new GetGachaLogRsp{retcode=GetGachaLogRsp.Retcode.Fail};}
        session.Send(Packet.FromProto(rsp,CmdId.GetGachaLogRsp));
    }
}
