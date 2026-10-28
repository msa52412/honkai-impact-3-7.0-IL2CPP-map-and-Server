using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetMissionDataReq)]
internal class GetMissionDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		Session session2 = session;
		GetMissionDataRsp getMissionDataRsp = new GetMissionDataRsp
		{
			retcode = GetMissionDataRsp.Retcode.Succ,
			IsAll = true
		};
		MissionDataExcel[] all = BaseExcelReader<MissionData, MissionDataExcel>.GetInstance().All;
		getMissionDataRsp.MissionLists.AddRange(all.Select((MissionDataExcel mission) => new Mission
		{
			MissionId = (uint)mission.Id,
			Status = MissionStatus.MissionFinish,
			Priority = (uint)mission.Priority,
			Progress = (uint)mission.TotalProgress,
			BeginTime = session2.Player.User.GetCreationTime(),
			EndTime = 2073239999u,
			CycleId = 1u
		}));
		session2.Send(Packet.FromProto(getMissionDataRsp, CmdId.GetMissionDataRsp));
	}
}
