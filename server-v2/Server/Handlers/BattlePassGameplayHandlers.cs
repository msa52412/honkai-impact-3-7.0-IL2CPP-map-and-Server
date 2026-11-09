using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.GetBattlePassReq)]
internal class GetBattlePassReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p){s.Send(Packet.FromProto(BattlePassGameplay.Build(s.Player),CmdId.GetBattlePassRsp));s.Send(Packet.FromProto(MissionDataBuilder.Build(s.Player),CmdId.GetMissionDataRsp));DailyGameplay.PushDuty(s);}
}
[PacketCmdId(CmdId.TakeBattlePassLevelRewardReq)]
internal class TakeBattlePassLevelRewardReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p){var rsp=BattlePassGameplay.Take(s.Player);s.Send(Packet.FromProto(rsp,CmdId.TakeBattlePassLevelRewardRsp));if(rsp.retcode==0){DailyGameplay.PushInventory(s);BattlePassGameplay.Push(s);}}
}
[PacketCmdId(CmdId.GetBattlePassThemeReq)]
internal class GetBattlePassThemeReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p){s.Send(Packet.FromProto(new GetBattlePassThemeRsp{retcode=GetBattlePassThemeRsp.Retcode.NotOpen},CmdId.GetBattlePassThemeRsp));}
}
[PacketCmdId(CmdId.TakeBattlePassPhaseExpReq)]
internal class TakeBattlePassPhaseExpReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p){s.Send(Packet.FromProto(new TakeBattlePassPhaseExpRsp{retcode=TakeBattlePassPhaseExpRsp.Retcode.Fail},CmdId.TakeBattlePassPhaseExpRsp));}
}
