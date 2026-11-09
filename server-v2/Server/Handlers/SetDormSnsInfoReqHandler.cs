using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 设置宿舍对外展示信息（SetDormSnsInfoReq 635）：展示的宿舍 / 房间 / 拜访角色。
/// </summary>
[PacketCmdId(CmdId.SetDormSnsInfoReq)]
internal class SetDormSnsInfoReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SetDormSnsInfoReq req = packet.GetDecodedBody<SetDormSnsInfoReq>();
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		SetDormSnsInfoRsp rsp = new SetDormSnsInfoRsp
		{
			retcode = SetDormSnsInfoRsp.Retcode.Succ
		};

		uint houseId = req.ShowHouse != 0u ? req.ShowHouse : dorms.ShowHouse;
		if (req.ShowHouse != 0u && dorms.GetHouse(req.ShowHouse) == null)
		{
			rsp.retcode = SetDormSnsInfoRsp.Retcode.HouseNotExist;
		}
		else if (req.ShowRoom != 0u && dorms.GetRoom(houseId, req.ShowRoom) == null)
		{
			rsp.retcode = SetDormSnsInfoRsp.Retcode.RoomNotExist;
		}
		else if (req.VisitAvatar != 0u && !session.Player.AvatarList.Any((AvatarScheme x) => x.AvatarId == req.VisitAvatar))
		{
			rsp.retcode = SetDormSnsInfoRsp.Retcode.AvatarNotExist;
		}
		else
		{
			if (req.ShowHouse != 0u)
			{
				dorms.ShowHouse = req.ShowHouse;
			}
			if (req.ShowRoom != 0u)
			{
				dorms.ShowRoom = req.ShowRoom;
			}
			dorms.VisitAvatar = req.VisitAvatar;
			dorms.IsAllowVisit = req.IsAllowVisit;
			dorms.Save();
		}

		session.Send(Packet.FromProto(rsp, CmdId.SetDormSnsInfoRsp));
	}
}
