using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetWarshipItemDataReq)]
internal class GetWarshipItemDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetWarshipItemDataRsp proto = new GetWarshipItemDataRsp
		{
			retcode = GetWarshipItemDataRsp.Retcode.Succ,
			IsAll = true,
			WarshipItemIdLists = BaseExcelReader<EntryThemeItemData, EntryThemeItemDataExcel>.GetInstance().All.Select((EntryThemeItemDataExcel theme) => (uint)theme.ThemeItemId).ToArray()
		};
		session.Send(Packet.FromProto(proto, CmdId.GetWarshipItemDataRsp));
	}
}
