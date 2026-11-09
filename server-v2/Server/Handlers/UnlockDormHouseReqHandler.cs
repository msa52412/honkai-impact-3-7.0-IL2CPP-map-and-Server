using System.Collections.Generic;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 解锁新宿舍（UnlockDormHouseReq 609）：等级达到 UnlockLv，再交 1 个 UnlockMaterial。
/// </summary>
[PacketCmdId(CmdId.UnlockDormHouseReq)]
internal class UnlockDormHouseReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		UnlockDormHouseReq req = packet.GetDecodedBody<UnlockDormHouseReq>();
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		UnlockDormHouseRsp rsp = new UnlockDormHouseRsp
		{
			retcode = UnlockDormHouseRsp.Retcode.Succ
		};

		DormitoryHouseDataExcel cfg = DormService.HouseConfig(req.HouseId);
		if (cfg == null)
		{
			rsp.retcode = UnlockDormHouseRsp.Retcode.Fail;
		}
		else if (dorms.GetHouse(req.HouseId) != null)
		{
			rsp.retcode = UnlockDormHouseRsp.Retcode.RepeatUnlock;
		}
		else if (DormService.PlayerLevel(session.Player.User) < cfg.UnlockLv)
		{
			rsp.retcode = UnlockDormHouseRsp.Retcode.LevelLimit;
		}
		else
		{
			Dictionary<uint, int> materials = new Dictionary<uint, int>();
			if (cfg.UnlockMaterial > 0)
			{
				materials[(uint)cfg.UnlockMaterial] = 1;
			}
			if (!DormService.HasMaterials(session.Player, materials))
			{
				rsp.retcode = UnlockDormHouseRsp.Retcode.MaterialLimit;
			}
			else
			{
				DormService.ConsumeMaterials(session.Player, materials);
				DormsScheme.DormHouse house = DormService.CreateHouse(cfg);
				dorms.HouseLists.Add(house);
				dorms.Save();
				rsp.House = DormService.ToProto(house);
			}
		}

		session.Send(Packet.FromProto(rsp, CmdId.UnlockDormHouseRsp));
	}
}
