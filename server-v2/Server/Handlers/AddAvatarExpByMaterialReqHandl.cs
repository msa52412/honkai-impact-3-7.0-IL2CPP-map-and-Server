using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.AddAvatarExpByMaterialReq)]
internal class AddAvatarExpByMaterialReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p){var req=p.GetDecodedBody<AddAvatarExpByMaterialReq>();var rsp=DailyActions.Feed(s.Player,req);s.Send(Packet.FromProto(rsp,CmdId.AddAvatarExpByMaterialRsp));if(rsp.retcode==0){DailyGameplay.PushInventory(s);s.ProcessPacket(Packet.FromProto(new GetAvatarDataReq{AvatarIdLists=new[]{req.AvatarId}},CmdId.GetAvatarDataReq));s.Send(Packet.FromProto(MissionDataBuilder.Build(s.Player),CmdId.GetMissionDataRsp));}}
}
