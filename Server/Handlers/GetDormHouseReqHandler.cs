using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetDormHouseReq)]
internal class GetDormHouseReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetDormHouseRsp getDormHouseRsp = new GetDormHouseRsp
		{
			retcode = GetDormHouseRsp.Retcode.Succ
		};
		IEnumerable<DormHouse> collection = session.Player.Dorms.HouseLists.Select(delegate(DormsScheme.DormHouse house)
		{
			DormHouse dormHouse = new DormHouse
			{
				Id = house.Id,
				Name = house.Name,
				Level = house.Level
			};
			IEnumerable<DormRoom> collection2 = house.RoomLists.Select(delegate(DormsScheme.DormRoom room)
			{
				DormRoom dormRoom = new DormRoom
				{
					Id = room.Id,
					Name = room.Name,
					AvatarLists = room.AvatarLists
				};
				IEnumerable<Furniture> enumerable = room.FurnitureLists.Select((Furniture Furniture) => new Furniture
				{
					Id = Furniture.Id,
					PosX = Furniture.PosX,
					PosY = Furniture.PosY,
					Direction = Furniture.Direction,
					Location = Furniture.Location
				});
				dormRoom.FurnitureLists.AddRange(room.FurnitureLists);
				return dormRoom;
			});
			dormHouse.RoomLists.AddRange(collection2);
			return dormHouse;
		});
		getDormHouseRsp.HouseLists.AddRange(collection);
		session.Send(Packet.FromProto(getDormHouseRsp, CmdId.GetDormHouseRsp));
	}
}
