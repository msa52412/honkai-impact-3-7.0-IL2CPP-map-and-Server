using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.AddGodWarRoleRelationExpReq)]
internal class AddGodWarRoleRelationExpReqHandler : IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var rsp=GodWarProgression.AddExp(s.Player,p.GetDecodedBody<AddGodWarRoleRelationExpReq>());
  if(rsp.retcode==0)GodWarProgression.Push(s);
  s.Send(Packet.FromProto(rsp,CmdId.AddGodWarRoleRelationExpRsp));
 }
}
[PacketCmdId(CmdId.TakeGodWarRoleRelationLevelRewardReq)]
internal class TakeGodWarRoleRelationLevelRewardReqHandler : IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var req=p.GetDecodedBody<TakeGodWarRoleRelationLevelRewardReq>();
  var rsp=GodWarProgression.TakeLevels(s.Player,req);
  Packet.c.Log($"[GodWar] vessel claim role={req.RoleId} result={rsp.retcode}");
  s.Send(Packet.FromProto(rsp,CmdId.TakeGodWarRoleRelationLevelRewardRsp));
  if(rsp.retcode==0)GodWarProgression.Push(s);
 }
}
[PacketCmdId(CmdId.TakeGodWarRoleStoryRewardReq)]
internal class TakeGodWarRoleStoryRewardReqHandler : IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var rsp=GodWarProgression.TakeStory(s.Player,p.GetDecodedBody<TakeGodWarRoleStoryRewardReq>());
  if(rsp.retcode==0)GodWarProgression.Push(s);
  s.Send(Packet.FromProto(rsp,CmdId.TakeGodWarRoleStoryRewardRsp));
 }
}
[PacketCmdId(CmdId.LevelUpGodWarTalentReq)]
internal class LevelUpGodWarTalentReqHandler : IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var rsp=GodWarProgression.Talent(s.Player,p.GetDecodedBody<LevelUpGodWarTalentReq>());
  if(rsp.retcode==0)GodWarProgression.Push(s);
  s.Send(Packet.FromProto(rsp,CmdId.LevelUpGodWarTalentRsp));
 }
}
[PacketCmdId(CmdId.SwitchGodWarChapterReq)]
internal class SwitchGodWarChapterReqHandler : IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var rsp=GodWarProgression.SwitchChapter(s.Player,p.GetDecodedBody<SwitchGodWarChapterReq>());
  if(rsp.retcode==0)GodWarProgression.Push(s);
  s.Send(Packet.FromProto(rsp,CmdId.SwitchGodWarChapterRsp));
 }
}
