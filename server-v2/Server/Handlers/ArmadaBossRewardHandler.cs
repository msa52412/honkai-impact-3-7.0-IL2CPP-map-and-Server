using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.TakeArmadaStageScoreActivityRewardReq)] internal class TakeArmadaStageScoreActivityRewardReqHandler:IPacketHandler
{public void Handle(Session s,Packet p){var rsp=ArmadaBossGameplay.Take(s.Player,p.GetDecodedBody<TakeArmadaStageScoreActivityRewardReq>());s.Send(Packet.FromProto(rsp,CmdId.TakeArmadaStageScoreActivityRewardRsp));if(rsp.retcode==0){DailyGameplay.PushInventory(s);s.ProcessPacket(Packet.FromProto(new GetArmadaDataReq(),CmdId.GetArmadaDataReq));s.Send(Packet.FromProto(ArmadaBossGameplay.Build(s.Player),CmdId.GetArmadaStageScoreActivityRsp));}}}
