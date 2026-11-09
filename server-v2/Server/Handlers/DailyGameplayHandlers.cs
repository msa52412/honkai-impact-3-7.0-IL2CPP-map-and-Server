using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.TakeDutyRewardReq)]
internal class TakeDutyRewardReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var req=p.GetDecodedBody<TakeDutyRewardReq>();
  var ids=string.Join(",",req.DutyIdLists??System.Array.Empty<uint>());
  var rsp=DailyGameplay.TakeDuty(s.Player,req);
  if(rsp.retcode!=0)Packet.c.Warn("[DailyDuty] result="+rsp.retcode+" type="+rsp.DutyType+" ids="+ids+" points="+DailyGameplay.State(s.Player).DailyExp);
  s.Send(Packet.FromProto(rsp,CmdId.TakeDutyRewardRsp));
  if(rsp.retcode==0){DailyGameplay.PushInventory(s);DailyGameplay.PushDuty(s);}
 }
}
[PacketCmdId(CmdId.UpdateMissionProgressReq)]
internal class UpdateMissionProgressReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  // Progress is recorded by authoritative success paths, never arbitrary client increments.
  s.Send(Packet.FromProto(new UpdateMissionProgressRsp{retcode=UpdateMissionProgressRsp.Retcode.FinishWayError},CmdId.UpdateMissionProgressRsp));
  s.Send(Packet.FromProto(MissionDataBuilder.Build(s.Player),CmdId.GetMissionDataRsp));
  DailyGameplay.PushDuty(s);
 }
}
