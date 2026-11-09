using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.GetShopListReq)]
internal class GetShopListReqHandler:IPacketHandler
{public void Handle(Session s,Packet p)=>s.Send(Packet.FromProto(MaterialShopGameplay.Build(s.Player),CmdId.GetShopListRsp));}
