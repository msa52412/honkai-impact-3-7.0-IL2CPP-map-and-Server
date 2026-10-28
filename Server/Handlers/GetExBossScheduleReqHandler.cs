using Common;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetExBossScheduleReq)]
internal class GetExBossScheduleReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetExBossScheduleRsp getExBossScheduleRsp = new GetExBossScheduleRsp
		{
			retcode = GetExBossScheduleRsp.Retcode.Succ,
			BeginTime = 0u,
			EndTime = (uint)((int)Global.GetUnixInSeconds() + 604800),
			MinLevel = 0u,
			ScheduleId = 1u
		};
		getExBossScheduleRsp.RankId = BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance().ExBossRankFromExp(session.Player.User.Exp);
		session.Send(Packet.FromProto(getExBossScheduleRsp, CmdId.GetExBossScheduleRsp));
	}
}
