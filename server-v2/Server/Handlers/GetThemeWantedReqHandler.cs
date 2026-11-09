using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.GetThemeWantedReq)] internal class GetThemeWantedReqHandler:IPacketHandler {public void Handle(Session s,Packet p){s.Send(Packet.FromProto(ThemeWantedGameplay.Build(s.Player),CmdId.GetThemeWantedRsp));DailyGameplay.PushInventory(s);}}
[PacketCmdId(CmdId.ThemeWantedRefreshTicketReq)] internal class ThemeWantedRefreshTicketReqHandler:IPacketHandler {public void Handle(Session s,Packet p){var r=ThemeWantedGameplay.Refresh(s.Player);s.Send(Packet.FromProto(r,CmdId.ThemeWantedRefreshTicketRsp));if(r.retcode==0)DailyGameplay.PushInventory(s);}}
[PacketCmdId(CmdId.ThemeWantedBuyExtraDropReq)] internal class ThemeWantedBuyExtraDropReqHandler:IPacketHandler {public void Handle(Session s,Packet p){var r=ThemeWantedGameplay.BuyExtra(s.Player);s.Send(Packet.FromProto(r,CmdId.ThemeWantedBuyExtraDropRsp));if(r.retcode==0)DailyGameplay.PushInventory(s);}}
