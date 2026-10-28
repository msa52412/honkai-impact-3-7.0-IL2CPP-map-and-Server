using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.One;

[PacketCmdId(CmdId.GetWeekDayActivityDataReq)]
internal class GetWeekDayActivityDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetWeekDayActivityDataRsp getWeekDayActivityDataRsp = new GetWeekDayActivityDataRsp
		{
			retcode = GetWeekDayActivityDataRsp.Retcode.Succ
		};
		getWeekDayActivityDataRsp.ActivityLists.Add(new WeekDayActivity
		{
			ActivityId = 1003u,
			StageIdLists = new uint[4] { 101302u, 101303u, 101304u, 101305u },
			EnterTimes = 1u,
			BeginTime = 0u,
			EndTime = (uint)((int)Global.GetUnixInSeconds() + 604800),
			ActivityEndTime = (uint)Global.GetUnixInSeconds(),
			ForceOpenTime = 0u
		});
		session.Send(Packet.FromProto(getWeekDayActivityDataRsp, CmdId.GetWeekDayActivityDataRsp));
	}
}
