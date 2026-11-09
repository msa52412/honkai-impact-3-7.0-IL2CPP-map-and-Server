using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers.One;
[PacketCmdId(CmdId.GetWeekDayActivityDataReq)]
internal class GetWeekDayActivityDataReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p)=>s.Send(Packet.FromProto(WeekdayGameplay.Build(s.Player),CmdId.GetWeekDayActivityDataRsp));
}
