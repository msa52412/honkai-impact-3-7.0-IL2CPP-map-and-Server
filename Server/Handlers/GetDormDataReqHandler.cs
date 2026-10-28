using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetDormDataReq)]
internal class GetDormDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetDormDataReq decodedBody = packet.GetDecodedBody<GetDormDataReq>();
		GetDormDataRsp getDormDataRsp = new GetDormDataRsp
		{
			retcode = GetDormDataRsp.Retcode.Succ
		};
		DormsScheme dorms = session.Player.Dorms;
		IEnumerable<DormHouse> collection = dorms.HouseLists.Select(delegate(DormsScheme.DormHouse house)
		{
			DormHouse dormHouse = new DormHouse
			{
				Id = house.Id,
				Name = house.Name,
				Level = house.Level
			};
			IEnumerable<DormRoom> collection6 = house.RoomLists.Select(delegate(DormsScheme.DormRoom room)
			{
				DormRoom dormRoom = new DormRoom
				{
					Id = room.Id,
					Name = room.Name,
					AvatarLists = room.AvatarLists
				};
				IEnumerable<Furniture> collection7 = room.FurnitureLists.Select((Furniture Furniture) => new Furniture
				{
					Id = Furniture.Id,
					PosX = Furniture.PosX,
					PosY = Furniture.PosY,
					Direction = Furniture.Direction,
					Location = Furniture.Location
				});
				dormRoom.FurnitureLists.AddRange(collection7);
				return dormRoom;
			});
			dormHouse.RoomLists.AddRange(collection6);
			return dormHouse;
		});
		getDormDataRsp.HouseLists.AddRange(collection);
		getDormDataRsp.FinishTalkPlotIdLists = dorms.FinishTalkPlotIdLists;
		IEnumerable<DepotFurniture> collection2 = dorms.DepotFurnitureLists.Select((DepotFurniture DepotFurniture) => new DepotFurniture
		{
			Id = DepotFurniture.Id,
			Num = DepotFurniture.Num
		});
		getDormDataRsp.DepotFurnitureLists.AddRange(collection2);
		getDormDataRsp.LevelUpHouseId = dorms.LevelUpHouseId;
		getDormDataRsp.LevelUpEndTime = dorms.LevelUpEndTime;
		getDormDataRsp.LevelUpEndHouseId = dorms.LevelUpEndHouseId;
		DormFacilityData dormFacilityData = new DormFacilityData
		{
			LevelUpFacilityType = dorms.FacilityData.LevelUpFacilityType,
			LevelUpEndTime = dorms.FacilityData.LevelUpEndTime,
			StoreStamina = dorms.FacilityData.StoreStamina,
			IsCanStoreStamina = dorms.FacilityData.IsCanStoreStamina,
			Power = dorms.FacilityData.Power,
			PowerRecoverLeftTime = dorms.FacilityData.PowerRecoverLeftTime,
			PowerRecoverConfigTime = dorms.FacilityData.PowerRecoverConfigTime,
			PowerLimit = dorms.FacilityData.PowerLimit,
			BasePowerLimit = dorms.FacilityData.BasePowerLimit,
			PowerRecovery = dorms.FacilityData.PowerRecovery,
			BasePowerRecovery = dorms.FacilityData.BasePowerRecovery,
			BonusPowerRecovery = dorms.FacilityData.BonusPowerRecovery
		};
		IEnumerable<DormFacility> collection3 = dorms.FacilityData.FacilityLists.Select((DormFacility DormFacility) => new DormFacility
		{
			Type = DormFacility.Type,
			Id = DormFacility.Id,
			Level = DormFacility.Level,
			BeginTime = DormFacility.BeginTime
		});
		dormFacilityData.FacilityLists.AddRange(collection3);
		IEnumerable<DormPowerBonus> collection4 = dorms.FacilityData.LimitBonusLists.Select((DormPowerBonus DormPowerBonus) => new DormPowerBonus
		{
			Bonus = DormPowerBonus.Bonus,
			HouseId = DormPowerBonus.HouseId
		});
		dormFacilityData.LimitBonusLists.AddRange(collection4);
		getDormDataRsp.FacilityData = dormFacilityData;
		IEnumerable<DormEvent> collection5 = dorms.EventLists.Select((DormEvent DormEvent) => new DormEvent
		{
			AvatarId = DormEvent.AvatarId,
			EventIdLists = DormEvent.EventIdLists
		});
		getDormDataRsp.EventLists.AddRange(collection5);
		getDormDataRsp.ShowHouse = dorms.ShowHouse;
		getDormDataRsp.ShowRoom = dorms.ShowRoom;
		getDormDataRsp.VisitAvatar = dorms.VisitAvatar;
		getDormDataRsp.IsAllowVisit = dorms.IsAllowVisit;
		getDormDataRsp.LastTakeDormDailyStaminaTime = dorms.LastTakeDormDailyStaminaTime;
		uint[] array = (getDormDataRsp.UnlockAvatarLists = dorms.UnlockAvatarLists);
		session.Send(Packet.FromProto(getDormDataRsp, CmdId.GetDormDataRsp));
	}
}
