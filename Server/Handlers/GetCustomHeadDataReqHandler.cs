using System.Collections.Generic;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetCustomHeadDataReq)]
internal class GetCustomHeadDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetCustomHeadDataRsp getCustomHeadDataRsp = new GetCustomHeadDataRsp
		{
			retcode = GetCustomHeadDataRsp.Retcode.Succ,
			IsAll = true
		};
		List<CustomHead> list = new List<CustomHead>();
		CustomHeadDataExcel[] all = BaseExcelReader<CustomHeadData, CustomHeadDataExcel>.GetInstance().All;
		foreach (CustomHeadDataExcel customHeadDataExcel in all)
		{
			CustomHead item = new CustomHead
			{
				Id = (uint)customHeadDataExcel.HeadId
			};
			list.Add(item);
		}
		getCustomHeadDataRsp.CustomHeadLists.AddRange(list);
		session.Send(Packet.FromProto(getCustomHeadDataRsp, CmdId.GetCustomHeadDataRsp));
	}
}
