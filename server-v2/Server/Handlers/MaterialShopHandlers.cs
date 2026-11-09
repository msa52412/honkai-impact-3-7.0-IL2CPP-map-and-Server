using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.BuyGoodsReq)]
internal class BuyGoodsReqHandler : IPacketHandler
{
    public void Handle(Session session, Packet packet)
    {
        var rsp = MaterialShopGameplay.Buy(session.Player, packet.GetDecodedBody<BuyGoodsReq>());
        // The success popup reads the wallet while it handles BuyGoodsRsp.
        if (rsp.retcode == BuyGoodsRsp.Retcode.Succ)
            DailyGameplay.PushInventory(session);

        session.Send(Packet.FromProto(rsp, CmdId.BuyGoodsRsp));
        if (rsp.retcode != BuyGoodsRsp.Retcode.Succ)
            return;

        if (WitchShopGameplay.ElfFragment((int)rsp.ItemId) != null)
            session.Send(Packet.FromProto(WitchShopGameplay.Snapshot(session.Player), CmdId.SyncElfFragmentNotify));
        if (MaterialShopGameplay.AffectsAvatar(rsp.ItemId))
            session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq(), CmdId.GetAvatarDataReq));
        session.Send(Packet.FromProto(MaterialShopGameplay.Build(session.Player), CmdId.GetShopListRsp));
    }
}
[PacketCmdId(CmdId.ManualRefreshShopReq)] internal class ManualRefreshShopReqHandler:IPacketHandler
{public void Handle(Session s,Packet p){var rsp=HistoricalShopRotation.Refresh(s.Player,p.GetDecodedBody<ManualRefreshShopReq>());s.Send(Packet.FromProto(rsp,CmdId.ManualRefreshShopRsp));if(rsp.retcode==0){DailyGameplay.PushInventory(s);s.Send(Packet.FromProto(MaterialShopGameplay.Build(s.Player),CmdId.GetShopListRsp));}}}
[PacketCmdId(CmdId.GetShoppingMallListReq)] internal class GetShoppingMallListReqHandler:IPacketHandler
{public void Handle(Session s,Packet p)=>s.Send(Packet.FromProto(new GetShoppingMallListRsp{retcode=0},CmdId.GetShoppingMallListRsp));}
[PacketCmdId(CmdId.GetProductListReq)] internal class GetProductListReqHandler:IPacketHandler
{public void Handle(Session s,Packet p)=>s.Send(Packet.FromProto(new GetProductListRsp{retcode=0},CmdId.GetProductListRsp));}
[PacketCmdId(CmdId.GetRecommendGoodsReq)] internal class GetRecommendGoodsReqHandler:IPacketHandler
{public void Handle(Session s,Packet p)=>s.Send(Packet.FromProto(new GetRecommendGoodsRsp{retcode=0},CmdId.GetRecommendGoodsRsp));}
