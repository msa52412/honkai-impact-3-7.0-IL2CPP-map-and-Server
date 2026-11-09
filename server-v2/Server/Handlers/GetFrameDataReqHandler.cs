using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetFrameDataReq)]
internal class GetFrameDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetFrameDataRsp getFrameDataRsp = new GetFrameDataRsp
		{
			retcode = GetFrameDataRsp.Retcode.Succ,
			IsAll = true
		};
		getFrameDataRsp.FrameLists.AddRange(BaseExcelReader<Common.Utils.ExcelReader.FrameData, FrameDataExcel>.GetInstance().All.Select((FrameDataExcel x) => new Common.Resources.Proto.FrameData
		{
			Id = (uint)x.Id
		}));
		session.Send(Packet.FromProto(getFrameDataRsp, CmdId.GetFrameDataRsp));
	}
}
