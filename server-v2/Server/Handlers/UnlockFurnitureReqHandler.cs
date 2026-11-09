using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 解锁家具图纸（UnlockFurnitureReq 680）：已有图纸 → HAS_UNLOCK，
/// 按 HcoinUnlock + UnlockItemList 扣费。
/// </summary>
[PacketCmdId(CmdId.UnlockFurnitureReq)]
internal class UnlockFurnitureReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		UnlockFurnitureReq req = packet.GetDecodedBody<UnlockFurnitureReq>();
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		UnlockFurnitureRsp rsp = new UnlockFurnitureRsp
		{
			retcode = UnlockFurnitureRsp.Retcode.Succ
		};

		DormitoryFurnitureDataExcel cfg = DormService.FurnitureConfig(req.FurnitureId);
		if (cfg == null)
		{
			rsp.retcode = UnlockFurnitureRsp.Retcode.Fail;
		}
		else if (dorms.IsFurnitureUnlocked(req.FurnitureId))
		{
			rsp.retcode = UnlockFurnitureRsp.Retcode.HasUnlock;
		}
		else if (cfg.HcoinUnlock > session.Player.User.Hcoin)
		{
			rsp.retcode = UnlockFurnitureRsp.Retcode.HcoinLack;
		}
		else if (!DormService.HasMaterials(session.Player, DormService.SumMaterials(cfg.UnlockItemList)))
		{
			rsp.retcode = UnlockFurnitureRsp.Retcode.MaterialLack;
		}
		else
		{
			session.Player.User.Hcoin -= cfg.HcoinUnlock;
			DormService.ConsumeMaterials(session.Player, DormService.SumMaterials(cfg.UnlockItemList));
			dorms.UnlockFurniture(req.FurnitureId);
			dorms.Save();
		}

		session.Send(Packet.FromProto(rsp, CmdId.UnlockFurnitureRsp));
	}
}
