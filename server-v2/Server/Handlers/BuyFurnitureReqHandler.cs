using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 购买家具（BuyFurnitureReq 621）：先要有图纸（LOCK），再按 HcoinCost + CostItemList 扣费，
/// 数量受 KeepLimit 限制（0 = 不限）。
/// </summary>
[PacketCmdId(CmdId.BuyFurnitureReq)]
internal class BuyFurnitureReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		BuyFurnitureReq req = packet.GetDecodedBody<BuyFurnitureReq>();
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		BuyFurnitureRsp rsp = new BuyFurnitureRsp
		{
			retcode = BuyFurnitureRsp.Retcode.Succ
		};

		List<DepotFurniture> items = req.FurnitureLists.Where((DepotFurniture x) => x.Id != 0u && x.Num != 0u).ToList();
		if (items.Count == 0)
		{
			rsp.retcode = BuyFurnitureRsp.Retcode.Fail;
			session.Send(Packet.FromProto(rsp, CmdId.BuyFurnitureRsp));
			return;
		}

		long hcoinCost = 0;
		Dictionary<uint, int> materials = new Dictionary<uint, int>();
		foreach (DepotFurniture item in items)
		{
			DormitoryFurnitureDataExcel cfg = DormService.FurnitureConfig(item.Id);
			if (cfg == null)
			{
				rsp.retcode = BuyFurnitureRsp.Retcode.Fail;
				session.Send(Packet.FromProto(rsp, CmdId.BuyFurnitureRsp));
				return;
			}
			if (!dorms.IsFurnitureUnlocked(item.Id))
			{
				rsp.retcode = BuyFurnitureRsp.Retcode.Lock;
				session.Send(Packet.FromProto(rsp, CmdId.BuyFurnitureRsp));
				return;
			}
			if (cfg.KeepLimit > 0 && DormService.OwnedFurnitureCount(dorms, item.Id) + item.Num > cfg.KeepLimit)
			{
				rsp.retcode = BuyFurnitureRsp.Retcode.KeepLimit;
				session.Send(Packet.FromProto(rsp, CmdId.BuyFurnitureRsp));
				return;
			}
			hcoinCost += (long)cfg.HcoinCost * item.Num;
			foreach (KeyValuePair<uint, int> material in DormService.SumMaterials(cfg.CostItemList, (int)item.Num))
			{
				materials.TryGetValue(material.Key, out int current);
				materials[material.Key] = current + material.Value;
			}
		}

		if (hcoinCost > session.Player.User.Hcoin)
		{
			rsp.retcode = BuyFurnitureRsp.Retcode.HcoinLack;
			session.Send(Packet.FromProto(rsp, CmdId.BuyFurnitureRsp));
			return;
		}
		if (!DormService.HasMaterials(session.Player, materials))
		{
			rsp.retcode = BuyFurnitureRsp.Retcode.MaterialLack;
			session.Send(Packet.FromProto(rsp, CmdId.BuyFurnitureRsp));
			return;
		}

		session.Player.User.Hcoin -= (int)hcoinCost;
		DormService.ConsumeMaterials(session.Player, materials);
		foreach (DepotFurniture item in items)
		{
			dorms.AddDepotFurniture(item.Id, (int)item.Num);
		}
		dorms.Save();

		session.Send(Packet.FromProto(rsp, CmdId.BuyFurnitureRsp));
	}
}
