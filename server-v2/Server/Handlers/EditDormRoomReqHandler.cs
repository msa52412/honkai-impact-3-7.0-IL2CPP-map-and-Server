using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 保存房间摆放（EditDormRoomReq 605）。客户端每次上报完整布局，直接整体替换；
/// 家具 id 在配置里查不到就当家具不足。
/// </summary>
[PacketCmdId(CmdId.EditDormRoomReq)]
internal class EditDormRoomReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		EditDormRoomReq req = packet.GetDecodedBody<EditDormRoomReq>();
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		EditDormRoomRsp rsp = new EditDormRoomRsp
		{
			retcode = EditDormRoomRsp.Retcode.Succ
		};

		if (req.FurnitureLists.Any((Furniture x) => DormService.FurnitureConfig(x.Id) == null))
		{
			rsp.retcode = EditDormRoomRsp.Retcode.FurnitureLack;
		}
		else if (dorms.EditDorm(req.HouseId, req.RoomId, req.FurnitureLists) != 2u)
		{
			rsp.retcode = EditDormRoomRsp.Retcode.RoomNotExist;
		}

		session.Send(Packet.FromProto(rsp, CmdId.EditDormRoomRsp));
	}
}
