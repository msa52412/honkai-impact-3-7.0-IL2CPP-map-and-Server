using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 设置宿舍角色（SetDormAvatarReq 611）：角色必须在玩家名下、房间必须存在、
/// 每个房间人数不能超过 DormitoryRoomData.AvatarLimit；成功后重新摇当天互动事件。
/// </summary>
[PacketCmdId(CmdId.SetDormAvatarReq)]
internal class SetDormAvatarReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SetDormAvatarReq req = packet.GetDecodedBody<SetDormAvatarReq>();
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		SetDormAvatarRsp rsp = new SetDormAvatarRsp
		{
			retcode = SetDormAvatarRsp.Retcode.Succ
		};

		List<DormRoomAvatar> assigns = req.RoomAvatarLists
			.Select((DormRoomAvatar x) => new DormRoomAvatar
			{
				RoomId = x.RoomId,
				AvatarLists = (x.AvatarLists ?? new uint[0]).Where((uint id) => id != 0u).ToArray()
			})
			.ToList();

		if (dorms.GetHouse(req.HouseId) == null)
		{
			rsp.retcode = SetDormAvatarRsp.Retcode.HouseNotExist;
		}
		else if (assigns.Any((DormRoomAvatar x) => dorms.GetRoom(req.HouseId, x.RoomId) == null))
		{
			rsp.retcode = SetDormAvatarRsp.Retcode.RoomNotExist;
		}
		else if (assigns.SelectMany((DormRoomAvatar x) => x.AvatarLists).Any(
			(uint id) => !session.Player.AvatarList.Any((AvatarScheme a) => a.AvatarId == id)))
		{
			rsp.retcode = SetDormAvatarRsp.Retcode.AvatarNotExist;
		}
		else if (assigns.Any((DormRoomAvatar x) => ExceedLimit(dorms, req.HouseId, x)))
		{
			rsp.retcode = SetDormAvatarRsp.Retcode.AvatarLimit;
		}
		else
		{
			dorms.SetDormAvatar(req.HouseId, assigns);
			DormService.ResetEvents(dorms);
			session.Send(Packet.FromProto(rsp, CmdId.SetDormAvatarRsp));
			session.ProcessPacket(Packet.FromProto(new GetDormDataReq
			{
				DataType = DormDataType.DormDataAll
			}, CmdId.GetDormDataReq));
			return;
		}

		session.Send(Packet.FromProto(rsp, CmdId.SetDormAvatarRsp));
	}

	private static bool ExceedLimit(DormsScheme dorms, uint houseId, DormRoomAvatar assign)
	{
		DormitoryRoomDataExcel cfg = DormService.RoomConfig(assign.RoomId);
		if (cfg == null || cfg.AvatarLimit <= 0)
		{
			return false;
		}
		DormsScheme.DormRoom room = dorms.GetRoom(houseId, assign.RoomId);
		int count = (room.AvatarLists ?? new uint[0]).Union(assign.AvatarLists).Distinct().Count();
		return count > cfg.AvatarLimit;
	}
}
