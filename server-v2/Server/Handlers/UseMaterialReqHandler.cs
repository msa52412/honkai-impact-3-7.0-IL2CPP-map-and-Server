using Common.Resources.Proto;
using System.Linq;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.UseMaterialReq)]
internal class UseMaterialReqHandler : IPacketHandler
{
    public void Handle(Session session,Packet packet)
    {
        var request=packet.GetDecodedBody<UseMaterialReq>();
        var rsp=MaterialConsumption.Use(session.Player,request);
        Packet.c.Log($"[UseMaterial] uid={session.Player.User.Uid} material={request.MaterialId} count={request.Num} result={rsp.retcode}");
        if(rsp.retcode==UseMaterialRsp.Retcode.Succ)
        {
            session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(),CmdId.GetEquipmentDataReq));
            session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq{AvatarIdLists=new uint[]{0}},CmdId.GetAvatarDataReq));
            if(session.Player.User!=null)
            {
                var user=session.Player.User;
                var level=BaseExcelReader<PlayerLevelData,PlayerLevelDataExcel>.GetInstance().CalculateLevel(user.Exp);
                session.Send(Packet.FromProto(new GetMainDataRsp{retcode=GetMainDataRsp.Retcode.Succ,
                    Hcoin=(uint)user.Hcoin,FreeHcoin=(uint)user.Hcoin,Stamina=(uint)user.Stamina,
                    Scoin=(uint)session.Player.Equipment.MaterialList.Where(x=>x.Id==100).Sum(x=>(long)x.Num),
                    Level=(uint)level.Level,Exp=(uint)level.Exp},CmdId.GetMainDataRsp));
            }
        }
        session.Send(Packet.FromProto(rsp,CmdId.UseMaterialRsp));
    }
}
