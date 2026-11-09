using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.AvatarSubSkillLevelUpReq)]
internal class AvatarSubSkillLevelUpReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p){var req=p.GetDecodedBody<AvatarSubSkillLevelUpReq>();var rsp=DailyActions.Skill(s.Player,req);s.Send(Packet.FromProto(rsp,CmdId.AvatarSubSkillLevelUpRsp));if(rsp.retcode==0){s.ProcessPacket(Packet.FromProto(new GetAvatarDataReq{AvatarIdLists=new[]{req.AvatarId}},CmdId.GetAvatarDataReq));s.Send(Packet.FromProto(MissionDataBuilder.Build(s.Player),CmdId.GetMissionDataRsp));}}
}
