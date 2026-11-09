using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 拉单个/多个宿舍（GetDormHouseReq 603）。house_id_list 为空时返回全部。
/// </summary>
[PacketCmdId(CmdId.GetDormHouseReq)]
internal class GetDormHouseReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetDormHouseReq req = packet.GetDecodedBody<GetDormHouseReq>();
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		GetDormHouseRsp rsp = new GetDormHouseRsp
		{
			retcode = GetDormHouseRsp.Retcode.Succ
		};

		uint[] ids = req.HouseIdLists ?? new uint[0];
		if (ids.Length == 0)
		{
			foreach (DormsScheme.DormHouse house in dorms.HouseLists)
			{
				rsp.HouseLists.Add(DormService.ToProto(house));
			}
		}
		else
		{
			foreach (uint houseId in ids)
			{
				DormsScheme.DormHouse house = dorms.GetHouse(houseId);
				if (house == null)
				{
					rsp.retcode = GetDormHouseRsp.Retcode.HouseNotExist;
					continue;
				}
				rsp.HouseLists.Add(DormService.ToProto(house));
			}
		}

		session.Send(Packet.FromProto(rsp, CmdId.GetDormHouseRsp));
	}
}
