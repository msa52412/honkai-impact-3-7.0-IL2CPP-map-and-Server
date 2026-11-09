using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.SelectAdventureQuestReq)]
internal class SelectAdventureQuestReqHandler : IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var rsp=AdventureService.Select(s.Player,p.GetDecodedBody<SelectAdventureQuestReq>());
  s.Send(Packet.FromProto(rsp,CmdId.SelectAdventureQuestRsp));
  AdventureService.Push(s);
 }
}
[PacketCmdId(CmdId.TakeAdventureQuestRewardReq)]
internal class TakeAdventureQuestRewardReqHandler : IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var rsp=AdventureService.TakeQuest(s.Player,p.GetDecodedBody<TakeAdventureQuestRewardReq>().PoolId);
  s.Send(Packet.FromProto(rsp,CmdId.TakeAdventureQuestRewardRsp));
  AdventureService.Push(s);
  if(rsp.retcode==0)AdventureService.NotifyInventory(s);
 }
}
[PacketCmdId(CmdId.TakeAdventureLevelRewardReq)]
internal class TakeAdventureLevelRewardReqHandler : IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var rsp=AdventureService.TakeLevel(s.Player);
  s.Send(Packet.FromProto(rsp,CmdId.TakeAdventureLevelRewardRsp));
  AdventureService.Push(s);
  if(rsp.retcode==0)AdventureService.NotifyInventory(s);
 }
}
[PacketCmdId(CmdId.CancelAdventureQuestReq)]
internal class CancelAdventureQuestReqHandler : IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var rsp=AdventureService.Cancel(s.Player,p.GetDecodedBody<CancelAdventureQuestReq>().PoolId);
  s.Send(Packet.FromProto(rsp,CmdId.CancelAdventureQuestRsp));
  AdventureService.Push(s);
 }
}
[PacketCmdId(CmdId.ResetAdventureQuestReq)]
internal class ResetAdventureQuestReqHandler : IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var rsp=AdventureService.Reset(s.Player,p.GetDecodedBody<ResetAdventureQuestReq>().PoolId);
  s.Send(Packet.FromProto(rsp,CmdId.ResetAdventureQuestRsp));
  AdventureService.Push(s);
 }
}
[PacketCmdId(CmdId.ClaimScoinReq)]
internal class ClaimScoinReqHandler : IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var rsp=AdventureService.ClaimCoins(s.Player);
  s.Send(Packet.FromProto(rsp,CmdId.ClaimScoinRsp));
  AdventureService.Push(s);
  if(rsp.retcode==0)AdventureService.NotifyInventory(s);
 }
}
