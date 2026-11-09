using Common.Resources.Proto;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.TakeAdventureCompensationReq)] internal class TakeAdventureCompensationReqHandler:IPacketHandler
{public void Handle(Session s,Packet p)=>s.Send(Packet.FromProto(new TakeAdventureCompensationRsp{retcode=TakeAdventureCompensationRsp.Retcode.HasTake},CmdId.TakeAdventureCompensationRsp));}
