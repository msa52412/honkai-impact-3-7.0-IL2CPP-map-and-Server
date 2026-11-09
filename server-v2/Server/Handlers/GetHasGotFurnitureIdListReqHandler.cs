using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 家具图鉴 / 图纸解锁列表（GetHasGotFurnitureIdListReq 619）。
/// furniture_id_list = 配置里全部家具，has_unlock_furniture_id_list = 已解锁图纸。
/// </summary>
[PacketCmdId(CmdId.GetHasGotFurnitureIdListReq)]
internal class GetHasGotFurnitureIdListReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		GetHasGotFurnitureIdListRsp rsp = new GetHasGotFurnitureIdListRsp
		{
			retcode = GetHasGotFurnitureIdListRsp.Retcode.Succ,
			FurnitureIdLists = BaseExcelReader<DormitoryFurnitureData, DormitoryFurnitureDataExcel>.GetInstance().All
				.Select((DormitoryFurnitureDataExcel x) => x.FurnitureId)
				.ToArray(),
			HasUnlockFurnitureIdLists = dorms.UnlockFurnitureIdLists ?? new uint[0]
		};

		session.Send(Packet.FromProto(rsp, CmdId.GetHasGotFurnitureIdListRsp));
	}
}
