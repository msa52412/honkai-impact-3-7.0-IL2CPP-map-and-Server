using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SelectGodWarBuffReq)]
internal class SelectGodWarBuffReqHandler:IPacketHandler
{
    public void Handle(Session s,Packet p){var req=p.GetDecodedBody<SelectGodWarBuffReq>();var rsp=GodWarProgression.SelectDeepBuff(s.Player,req);if(rsp.retcode==0){GodWarProgression.PushChallengeSetup(s,req.TaleId);GodWarProgression.PushCollections(s);}s.Send(Packet.FromProto(rsp,CmdId.SelectGodWarBuffRsp));if(rsp.retcode==0)GodWarProgression.PushDeepAfter(s,req.TaleId,true);else if(rsp.retcode==SelectGodWarBuffRsp.Retcode.NotCandidate)GodWarProgression.PushDeepAfter(s,req.TaleId);}
}
[PacketCmdId(CmdId.GetGodWarTeleportEventReq)]
internal class GetGodWarTeleportEventReqHandler:IPacketHandler
{
    public void Handle(Session s,Packet p){var req=p.GetDecodedBody<GetGodWarTeleportEventReq>();var rsp=GodWarProgression.DeepTeleports(s.Player,req);s.Send(Packet.FromProto(rsp,CmdId.GetGodWarTeleportEventRsp));}
}
[PacketCmdId(CmdId.RefreshGodWarBuffEventReq)]
internal class RefreshGodWarBuffEventReqHandler:IPacketHandler
{
    public void Handle(Session s,Packet p){var req=p.GetDecodedBody<RefreshGodWarBuffEventReq>();var rsp=GodWarProgression.RefreshDeepBuff(s.Player,req);if(rsp.retcode==0)GodWarProgression.PushChallengeSetup(s,req.TaleId);s.Send(Packet.FromProto(rsp,CmdId.RefreshGodWarBuffEventRsp));if(rsp.retcode==0)GodWarProgression.PushDeepAfter(s,req.TaleId);}
}
[PacketCmdId(CmdId.RefreshGodWarTeleportEventReq)]
internal class RefreshGodWarTeleportEventReqHandler:IPacketHandler
{
    public void Handle(Session s,Packet p){var req=p.GetDecodedBody<RefreshGodWarTeleportEventReq>();var rsp=GodWarProgression.RefreshDeepTeleport(s.Player,req);if(rsp.retcode==0)GodWarProgression.PushChallengeSetup(s,req.TaleId);s.Send(Packet.FromProto(rsp,CmdId.RefreshGodWarTeleportEventRsp));if(rsp.retcode==0)GodWarProgression.PushDeepAfter(s,req.TaleId);}
}
[PacketCmdId(CmdId.GetGodWarTaleShopReq)]
internal class GetGodWarTaleShopReqHandler:IPacketHandler
{
    public void Handle(Session s,Packet p){s.Send(Packet.FromProto(GodWarProgression.DeepShop(s.Player,p.GetDecodedBody<GetGodWarTaleShopReq>()),CmdId.GetGodWarTaleShopRsp));}
}
[PacketCmdId(CmdId.BuyGodWarShopGoodsReq)]
internal class BuyGodWarShopGoodsReqHandler:IPacketHandler
{
    public void Handle(Session s,Packet p){var req=p.GetDecodedBody<BuyGodWarShopGoodsReq>();var transaction=GodWarProgression.BuyDeepGoodsForClient(s.Player,req);var rsp=transaction.Reply;GodWarProgression.PushDeepEvents(s,req.TaleId,transaction.Events);if(rsp.retcode==0){GodWarProgression.PushChallengeSetup(s,req.TaleId);GodWarProgression.PushCollections(s);if(!GodWarProgression.DeepChoosingCore(s.Player,req.TaleId))s.Send(Packet.FromProto(GodWarProgression.DeepShop(s.Player,new GetGodWarTaleShopReq{GodWarId=1,TaleId=req.TaleId}),CmdId.GetGodWarTaleShopRsp));}s.Send(Packet.FromProto(rsp,CmdId.BuyGodWarShopGoodsRsp));if(rsp.retcode==0&&GodWarProgression.DeepChoosingCore(s.Player,req.TaleId))GodWarProgression.PushDeepAfter(s,req.TaleId);}
}
[PacketCmdId(CmdId.RefreshGodWarShopGoodsReq)]
internal class RefreshGodWarShopGoodsReqHandler:IPacketHandler
{
    public void Handle(Session s,Packet p){var req=p.GetDecodedBody<RefreshGodWarShopGoodsReq>();var transaction=GodWarProgression.RefreshDeepShopForClient(s.Player,req);var rsp=transaction.Reply;GodWarProgression.PushDeepEvents(s,req.TaleId,transaction.Events);if(rsp.retcode==0)GodWarProgression.PushChallengeSetup(s,req.TaleId);s.Send(Packet.FromProto(rsp,CmdId.RefreshGodWarShopGoodsRsp));if(rsp.retcode==0){if(GodWarProgression.DeepChoosingCore(s.Player,req.TaleId))GodWarProgression.PushDeepAfter(s,req.TaleId);else s.Send(Packet.FromProto(GodWarProgression.DeepShop(s.Player,new GetGodWarTaleShopReq{GodWarId=1,TaleId=req.TaleId}),CmdId.GetGodWarTaleShopRsp));}}
}
[PacketCmdId(CmdId.PredictGodWarEventReq)]
internal class PredictGodWarEventReqHandler:IPacketHandler
{
    public void Handle(Session s,Packet p){s.Send(Packet.FromProto(GodWarProgression.PredictDeep(s.Player,p.GetDecodedBody<PredictGodWarEventReq>()),CmdId.PredictGodWarEventRsp));}
}

