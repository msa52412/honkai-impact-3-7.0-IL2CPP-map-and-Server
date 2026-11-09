using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 拉仓库家具（GetDepotFurnitureReq 607）。不传 furniture_id_list 就返回全部。
/// </summary>
[PacketCmdId(CmdId.GetDepotFurnitureReq)]
internal class GetDepotFurnitureReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetDepotFurnitureReq req = packet.GetDecodedBody<GetDepotFurnitureReq>();
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		GetDepotFurnitureRsp rsp = new GetDepotFurnitureRsp
		{
			retcode = GetDepotFurnitureRsp.Retcode.Succ
		};

		uint[] filter = req.FurnitureIdLists ?? new uint[0];
		foreach (DepotFurniture furniture in dorms.DepotFurnitureLists)
		{
			if (filter.Length != 0 && !filter.Contains(furniture.Id))
			{
				continue;
			}
			rsp.DepotFurnitureLists.Add(new DepotFurniture
			{
				Id = furniture.Id,
				Num = furniture.Num
			});
		}

		session.Send(Packet.FromProto(rsp, CmdId.GetDepotFurnitureRsp));
	}
}
