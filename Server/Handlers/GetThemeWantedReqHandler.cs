using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetThemeWantedReq)]
internal class GetThemeWantedReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetThemeWantedRsp getThemeWantedRsp = new GetThemeWantedRsp();
		getThemeWantedRsp.retcode = GetThemeWantedRsp.Retcode.Succ;
		getThemeWantedRsp.ThemeWantedActivity = new ThemeWantedActivity
		{
			ScheduleId = 1u,
			ActivityId = 11104u,
			OpenStageGroupIdLists = new uint[4] { 13u, 14u, 15u, 16u }
		};
		GetThemeWantedRsp getThemeWantedRsp2 = getThemeWantedRsp;
		uint[] openStageGroupIdLists = getThemeWantedRsp2.ThemeWantedActivity.OpenStageGroupIdLists;
		foreach (uint num in openStageGroupIdLists)
		{
			SingleWantedStageGroupExcel singleWantedStageGroupExcel = BaseExcelReader<SingleWantedStageGroup, SingleWantedStageGroupExcel>.GetInstance().FromGroupId((int)num);
			if (singleWantedStageGroupExcel != null)
			{
				getThemeWantedRsp2.ThemeWantedActivity.StageGroupInfoLists.Add(new ThemeWantedStageGroupInfo
				{
					StageGroupId = num,
					Progress = (uint)singleWantedStageGroupExcel.StageIdList.Length,
					UnlockMpProgressLists = singleWantedStageGroupExcel.MpStageIdList.Select((int mpStage, int index) => (uint)(index + 1)).ToArray()
				});
			}
		}
		session.Send(Packet.FromProto(getThemeWantedRsp2, CmdId.GetThemeWantedRsp));
	}
}
