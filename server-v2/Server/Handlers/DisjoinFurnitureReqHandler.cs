using System.Collections.Generic;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;
using Material = Common.Resources.Proto.Material;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 分解家具（DisjoinFurnitureReq 1134）：从仓库扣掉整件家具，返还 DeleteMaterialList 里的材料。
/// </summary>
[PacketCmdId(CmdId.DisjoinFurnitureReq)]
internal class DisjoinFurnitureReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		DisjoinFurnitureReq req = packet.GetDecodedBody<DisjoinFurnitureReq>();
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		DisjoinFurnitureRsp rsp = new DisjoinFurnitureRsp
		{
			retcode = DisjoinFurnitureRsp.Retcode.Succ
		};

		Dictionary<uint, int> refund = new Dictionary<uint, int>();
		foreach (DepotFurniture item in req.FurnitureLists)
		{
			if (item.Id == 0u || item.Num == 0u)
			{
				continue;
			}
			DormitoryFurnitureDataExcel cfg = DormService.FurnitureConfig(item.Id);
			if (cfg == null || dorms.GetDepotCount(item.Id) < item.Num)
			{
				rsp.retcode = DisjoinFurnitureRsp.Retcode.FurnitureLack;
				session.Send(Packet.FromProto(rsp, CmdId.DisjoinFurnitureRsp));
				return;
			}
			foreach (KeyValuePair<uint, int> material in DormService.SumMaterials(cfg.DeleteMaterialList, (int)item.Num))
			{
				refund.TryGetValue(material.Key, out int current);
				refund[material.Key] = current + material.Value;
			}
		}

		foreach (DepotFurniture item in req.FurnitureLists)
		{
			if (item.Id != 0u && item.Num != 0u)
			{
				dorms.RemoveDepotFurniture(item.Id, (int)item.Num);
			}
		}
		dorms.Save();
		DormService.GrantMaterials(session.Player, refund);

		foreach (KeyValuePair<uint, int> material in refund)
		{
			rsp.MaterialLists.Add(new Material
			{
				Id = material.Key,
				Num = (uint)material.Value
			});
		}

		session.Send(Packet.FromProto(rsp, CmdId.DisjoinFurnitureRsp));
	}
}
