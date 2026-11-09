using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.WeekDayActivitySweepReq)]
internal class WeekDayActivitySweepReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var rsp=WeekdayGameplay.Sweep(s.Player,p.GetDecodedBody<WeekDayActivitySweepReq>().StageIdLists);
  s.Send(Packet.FromProto(rsp,CmdId.WeekDayActivitySweepRsp));
  if(rsp.retcode==0){DailyGameplay.PushInventory(s);WeekdayGameplay.Push(s);}
 }
}
[PacketCmdId(CmdId.OpenWeekDayActivityReq)]
internal class OpenWeekDayActivityReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p)
 {
  var id=p.GetDecodedBody<OpenWeekDayActivityReq>().ActivityId;
  var rsp=WeekdayCalendar.Open(s.Player,id);s.Send(Packet.FromProto(rsp,CmdId.OpenWeekDayActivityRsp));
  if(rsp.retcode==OpenWeekDayActivityRsp.Retcode.Succ||rsp.retcode==OpenWeekDayActivityRsp.Retcode.IsOpen){if(rsp.retcode==0)DailyGameplay.PushInventory(s);s.Send(Packet.FromProto(WeekdayGameplay.Build(s.Player),CmdId.GetWeekDayActivityDataRsp));}
 }
}
[PacketCmdId(CmdId.ExchangeWeekDayActivityTimesReq)]
internal class ExchangeWeekDayActivityTimesReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p){var rsp=WeekdayPurchases.Buy(s.Player,p.GetDecodedBody<ExchangeWeekDayActivityTimesReq>());s.Send(Packet.FromProto(rsp,CmdId.ExchangeWeekDayActivityTimesRsp));if(rsp.retcode==0){DailyGameplay.PushInventory(s);WeekdayGameplay.Push(s);}}
}
[PacketCmdId(CmdId.GetWeekDayActivityCompensationInfoReq)]
internal class GetWeekDayActivityCompensationInfoReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p)=>s.Send(Packet.FromProto(new GetWeekDayActivityCompensationInfoRsp{retcode=GetWeekDayActivityCompensationInfoRsp.Retcode.Succ,HasTakenCompensation=false},CmdId.GetWeekDayActivityCompensationInfoRsp));
}
[PacketCmdId(CmdId.TakeWeekDayActivityCompensationReq)]
internal class TakeWeekDayActivityCompensationReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p)=>s.Send(Packet.FromProto(new TakeWeekDayActivityCompensationRsp{retcode=TakeWeekDayActivityCompensationRsp.Retcode.Fail},CmdId.TakeWeekDayActivityCompensationRsp));
}
[PacketCmdId(CmdId.TakeWeekDayActivityReturnCoinReq)]
internal class TakeWeekDayActivityReturnCoinReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p){var rsp=WeekdayRefund.Take(s.Player);s.Send(Packet.FromProto(rsp,CmdId.TakeWeekDayActivityReturnCoinRsp));if(rsp.retcode==0)DailyGameplay.PushInventory(s);}
}
