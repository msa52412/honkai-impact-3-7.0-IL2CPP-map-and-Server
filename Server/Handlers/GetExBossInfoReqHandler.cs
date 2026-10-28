using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetExBossInfoReq)]
internal class GetExBossInfoReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetExBossInfoRsp Rsp = new GetExBossInfoRsp
		{
			retcode = GetExBossInfoRsp.Retcode.Succ,
			BossInfo = new ExBossInfo
			{
				EnterTimes = 0u,
				ScheduleId = 1u,
				MaxSweepLevel = 4u
			}
		};
		Rsp.BossInfo.RankId = BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance().ExBossRankFromExp(session.Player.User.Exp);
		foreach (ExBossMonsterDataExcel item in BaseExcelReader<ExBossMonsterData, ExBossMonsterDataExcel>.GetInstance().All.Where((ExBossMonsterDataExcel monster) => monster.ConfigId == Rsp.BossInfo.RankId))
		{
			Rsp.BossInfo.BossIdLists.Add(new ExBossIdInfo
			{
				BossId = (uint)item.BossId
			});
		}
		session.Send(Packet.FromProto(Rsp, CmdId.GetExBossInfoRsp));
	}
}
