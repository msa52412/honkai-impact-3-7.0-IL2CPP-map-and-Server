using Common.Resources.Proto;using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.ElfFragmentTransformReq)] internal class ElfFragmentTransformReqHandler:IPacketHandler
{public void Handle(Session s,Packet p){var rsp=ElfFragmentGameplay.Transform(s.Player,p.GetDecodedBody<ElfFragmentTransformReq>());s.Send(Packet.FromProto(rsp,CmdId.ElfFragmentTransformRsp));if(rsp.retcode==0){s.Send(Packet.FromProto(WitchShopGameplay.Snapshot(s.Player),CmdId.SyncElfFragmentNotify));DailyGameplay.PushInventory(s);}}}
