using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;

namespace Common.Database;

public class DormsScheme : IRow
{
	public class DormRoom : Common.Resources.Proto.DormRoom
	{
		public new List<Furniture> FurnitureLists { get; set; } = new List<Furniture>();

	}

	public class DormHouse : Common.Resources.Proto.DormHouse
	{
		public new List<DormRoom> RoomLists { get; set; } = new List<DormRoom>();

	}

	public class DormFacilityData : Common.Resources.Proto.DormFacilityData
	{
		public new List<DormFacility> FacilityLists { get; set; } = new List<DormFacility>();


		public new List<DormPowerBonus> LimitBonusLists { get; set; } = new List<DormPowerBonus>();

	}

	public class DormSnsData
	{
		public uint BeLikedNum { get; set; }

		public List<DormVisitRecord> RecordLists { get; set; } = new List<DormVisitRecord>();


		public uint BeVisitNum { get; set; }
	}

	public class CheckDormLikesDrop
	{
		public bool HasLikesDrop { get; set; }

		public uint YesterdayBeLikedNum { get; set; }
	}

	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	public List<DormHouse> HouseLists { get; set; }

	public uint[] FinishTalkPlotIdLists { get; set; }

	public List<DepotFurniture> DepotFurnitureLists { get; set; } = new List<DepotFurniture>();


	public uint LevelUpHouseId { get; set; }

	public uint LevelUpEndTime { get; set; }

	public uint LevelUpEndHouseId { get; set; }

	public DormFacilityData FacilityData { get; set; }

	public List<DormEvent> EventLists { get; set; } = new List<DormEvent>();


	public uint ShowHouse { get; set; }

	public uint ShowRoom { get; set; }

	public uint VisitAvatar { get; set; }

	public bool IsAllowVisit { get; set; }

	public uint LastTakeDormDailyStaminaTime { get; set; }

	public uint[] UnlockAvatarLists { get; set; }

	public DormSnsData SnsData { get; set; }

	public CheckDormLikesDrop CheckDormLikes { get; set; }

	public void Save()
	{
		Dorms.collection.ReplaceOne(this);
	}

	public void SetDormName(uint HouseId, string name)
	{
		DormHouse dormHouse = HouseLists.Where((DormHouse house) => house.Id == HouseId).FirstOrDefault();
		if (dormHouse != null)
		{
			dormHouse.Name = name;
		}
	}

	public uint EditDorm(uint HouseId, uint RoomId, List<Furniture> FurnitureLists)
	{
		DormHouse dormHouse = HouseLists.Where((DormHouse house) => house.Id == HouseId).FirstOrDefault();
		if (dormHouse != null)
		{
			DormRoom dormRoom = dormHouse.RoomLists.Where((DormRoom room) => room.Id == RoomId).FirstOrDefault();
			if (dormRoom != null)
			{
				IEnumerable<Furniture> collection = FurnitureLists.Select((Furniture Furniture) => new Furniture
				{
					Id = Furniture.Id,
					PosX = Furniture.PosX,
					PosY = Furniture.PosY,
					Direction = Furniture.Direction,
					Location = Furniture.Location
				});
				dormRoom.FurnitureLists.AddRange(collection);
				Save();
				return 1u;
			}
		}
		return 0u;
	}

	public uint SetDormAvatar(uint HouseId, List<DormRoomAvatar> RoomAvatarLists)
	{
		DormHouse dormHouse = HouseLists.Where((DormHouse house) => house.Id == HouseId).FirstOrDefault();
		uint num = 1u;
		if (dormHouse != null && RoomAvatarLists != null)
		{
			foreach (DormRoomAvatar dra in RoomAvatarLists)
			{
				DormRoom dormRoom = dormHouse.RoomLists.Where((DormRoom room) => room.Id == dra.RoomId).FirstOrDefault();
				if (dormRoom != null)
				{
					dormRoom.AvatarLists = dra.AvatarLists;
					num++;
				}
			}
			Save();
			return num;
		}
		return 0u;
	}
}
