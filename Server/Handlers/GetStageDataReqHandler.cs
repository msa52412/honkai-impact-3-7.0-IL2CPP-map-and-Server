using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetStageDataReq)]
internal class GetStageDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetStageDataRsp getStageDataRsp = new GetStageDataRsp
		{
			retcode = GetStageDataRsp.Retcode.Succ,
			IsAll = true
		};
		getStageDataRsp.StageLists.AddRange(BaseExcelReader<StageData, StageDataExcel>.GetInstance().All.Select((StageDataExcel stage) => new Stage
		{
			Id = (uint)stage.LevelId,
			IsDone = true,
			Progress = (uint)stage.MaxProgress,
			EnterTimes = 1u
		}));
		session.Send(Packet.FromProto(getStageDataRsp, CmdId.GetStageDataRsp));
	}
}
