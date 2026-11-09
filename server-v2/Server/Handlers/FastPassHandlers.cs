using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.GetFastPassDataReq)] internal class GetFastPassDataReqHandler:IPacketHandler
{public void Handle(Session s,Packet p)=>s.Send(Packet.FromProto(FastPassGameplay.Build(s.Player,p.GetDecodedBody<GetFastPassDataReq>().FastPassType),CmdId.GetFastPassDataRsp));}
[PacketCmdId(CmdId.FinishFastPassReq)] internal class FinishFastPassReqHandler:IPacketHandler
{public void Handle(Session s,Packet p){var rsp=FastPassGameplay.Finish(s.Player,p.GetDecodedBody<FinishFastPassReq>());s.Send(Packet.FromProto(rsp,CmdId.FinishFastPassRsp));if(rsp.retcode==0){DailyGameplay.PushInventory(s);WeekdayGameplay.Push(s);s.Send(Packet.FromProto(FastPassGameplay.Build(s.Player,rsp.FastPassType),CmdId.GetFastPassDataRsp));}}}
