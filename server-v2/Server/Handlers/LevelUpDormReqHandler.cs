using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 宿舍装修升级（LevelUpDormReq 613）：一次只能升一个宿舍（BUSY），
/// 需要舒适度达到 NeedComfort、材料满足 MaterialList；NeedTime 秒后由 FinalizeLevelUp 结算。
/// </summary>
[PacketCmdId(CmdId.LevelUpDormReq)]
internal class LevelUpDormReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		LevelUpDormReq req = packet.GetDecodedBody<LevelUpDormReq>();
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		LevelUpDormRsp rsp = new LevelUpDormRsp
		{
			retcode = LevelUpDormRsp.Retcode.Succ
		};

		DormsScheme.DormHouse house = dorms.GetHouse(req.HouseId);
		DormitoryDecorationExcel cfg = DormService.DecorationConfig(req.HouseId, req.TargetLevel);
		if (house == null)
		{
			rsp.retcode = LevelUpDormRsp.Retcode.HouseNotExist;
		}
		else if (dorms.IsLevelUpBusy())
		{
			rsp.retcode = LevelUpDormRsp.Retcode.Busy;
		}
		else if (req.TargetLevel <= house.Level || cfg == null)
		{
			rsp.retcode = LevelUpDormRsp.Retcode.LevelError;
		}
		else if (dorms.ComfortValue(req.HouseId) < cfg.NeedComfort)
		{
			rsp.retcode = LevelUpDormRsp.Retcode.ComfortLimit;
		}
		else
		{
			Dictionary<uint, int> materials = DormService.SumMaterials(cfg.MaterialList);
			if (!DormService.HasMaterials(session.Player, materials))
			{
				rsp.retcode = LevelUpDormRsp.Retcode.MaterialLimit;
			}
			else
			{
				DormService.ConsumeMaterials(session.Player, materials);
				long now = Global.GetUnixInSeconds();
				if (cfg.NeedTime <= 0)
				{
					house.Level = req.TargetLevel;
					dorms.LevelUpEndHouseId = req.HouseId;
					dorms.Save();
					rsp.LevelUpHouseId = req.HouseId;
					rsp.LevelUpEndTime = (uint)now;
				}
				else
				{
					dorms.LevelUpHouseId = req.HouseId;
					dorms.LevelUpTargetLevel = req.TargetLevel;
					dorms.LevelUpEndTime = (uint)(now + cfg.NeedTime);
					dorms.Save();
					rsp.LevelUpHouseId = req.HouseId;
					rsp.LevelUpEndTime = dorms.LevelUpEndTime;
				}
			}
		}

		session.Send(Packet.FromProto(rsp, CmdId.LevelUpDormRsp));
	}
}
