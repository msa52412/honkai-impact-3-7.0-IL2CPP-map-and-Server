using System;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.TriggerGodWarEventReq)]
internal class TriggerGodWarEventReqHandler:IPacketHandler
{
 public void Handle(Session session,Packet packet)
 {
  var req=packet.GetDecodedBody<TriggerGodWarEventReq>();
  if((req.EventIdLists??Array.Empty<uint>()).Length==0){session.Send(Packet.FromProto(new TriggerGodWarEventRsp{GodWarId=req.GodWarId,TaleId=req.TaleId,retcode=req.GodWarId==1?0:TriggerGodWarEventRsp.Retcode.FeatureClose,EventIdLists=Array.Empty<uint>()},CmdId.TriggerGodWarEventRsp));return;}
  bool lobby=GodWarProgression.IsLobbyTale(req.TaleId);
  var result=lobby?GodWarProgression.TriggerLobby(session.Player,req):GodWarProgression.TriggerDeep(session.Player,req);
  if(lobby&&result.Reply.retcode==0){if(result.InventoryChanged)session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(),CmdId.GetEquipmentDataReq));if(result.Changed){session.Send(Packet.FromProto(MissionDataBuilder.Build(session.Player),CmdId.GetMissionDataRsp));GodWarProgression.PushCollections(session);}GodWarProgression.PushLobby(session,GodWarService.Build(session.Player).GodWarLists[0].LobbyId);}
  else if(result.Changed)GodWarProgression.PushChallengeSetup(session,req.TaleId);
  session.Send(Packet.FromProto(result.Reply,CmdId.TriggerGodWarEventRsp));
  if(result.Reply.retcode==0)GodWarProgression.PushDeepEvents(session,req.TaleId,result.Events,req.TriggerType,req.TriggerParamLists);
 }
}
