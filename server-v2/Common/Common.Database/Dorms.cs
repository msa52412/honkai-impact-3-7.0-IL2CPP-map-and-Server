using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace Common.Database;

public class Dorms
{
	public static readonly SqlTable<DormsScheme> collection = Global.Table<DormsScheme>("Dorms", ("owner_uid", "INTEGER", x => x.OwnerUid, true));

	public static DormsScheme FromUid(uint uid)
	{
		DormsScheme dorms = (from collection in collection.All()
			where collection.OwnerUid == uid
			select collection).FirstOrDefault() ?? Create(uid);
		if (dorms.EnsureDefaults())
		{
			dorms.Save();
		}
		return dorms;
	}

	/// <summary>只读查询（访问别人宿舍时用），不存在返回 null，不会顺手建存档。</summary>
	public static DormsScheme TryFromUid(uint uid)
	{
		DormsScheme dorms = (from collection in collection.All()
			where collection.OwnerUid == uid
			select collection).FirstOrDefault();
		if (dorms != null && dorms.EnsureDefaults())
		{
			dorms.Save();
		}
		return dorms;
	}

	public static DormsScheme Create(uint uid)
	{
		DormsScheme dormsScheme = (from collection in collection.All()
			where collection.OwnerUid == uid
			select collection).FirstOrDefault();
		if (dormsScheme != null)
		{
			return dormsScheme;
		}
		DormsScheme dormsScheme2 = new DormsScheme();
		dormsScheme2.OwnerUid = uid;
		dormsScheme2.FinishTalkPlotIdLists = new uint[0];
		dormsScheme2.LevelUpHouseId = 0u;
		dormsScheme2.LevelUpEndTime = 0u;
		dormsScheme2.LevelUpEndHouseId = 0u;
		dormsScheme2.EventLists = new List<DormEvent>();
		dormsScheme2.ShowHouse = 101u;
		dormsScheme2.ShowRoom = 1011u;
		dormsScheme2.VisitAvatar = 0u;
		dormsScheme2.IsAllowVisit = false;
		dormsScheme2.LastTakeDormDailyStaminaTime = 0u;
		dormsScheme2.UnlockAvatarLists = new uint[0];
		dormsScheme2.SnsData = new DormsScheme.DormSnsData
		{
			RecordLists = new List<DormVisitRecord>(),
			BeLikedNum = 0u,
			BeVisitNum = 0u
		};
		dormsScheme2.CheckDormLikes = new DormsScheme.CheckDormLikesDrop
		{
			HasLikesDrop = false,
			YesterdayBeLikedNum = 0u
		};
		DormsScheme dormsScheme3 = dormsScheme2;
		List<DormsScheme.DormHouse> list = new List<DormsScheme.DormHouse>();
		DormitoryHouseDataExcel[] all = BaseExcelReader<DormitoryHouseData, DormitoryHouseDataExcel>.GetInstance().All;
		foreach (DormitoryHouseDataExcel dormitoryHouseDataExcel in all)
		{
			DormsScheme.DormHouse dormHouse = new DormsScheme.DormHouse
			{
				Id = (uint)dormitoryHouseDataExcel.HouseId,
				Name = "免费项目",
				Level = 1u
			};
			List<DormsScheme.DormRoom> list2 = new List<DormsScheme.DormRoom>();
			for (uint num = 0u; num < dormitoryHouseDataExcel.TotalRoomList.Length; num++)
			{
				DormsScheme.DormRoom dormRoom = new DormsScheme.DormRoom();
				dormRoom.Id = (uint)dormitoryHouseDataExcel.TotalRoomList[num];
				dormRoom.Name = "房间" + (num + 1);
				dormRoom.FurnitureLists = new List<Furniture>();
				dormRoom.AvatarLists = new uint[0];
				DormsScheme.DormRoom item = dormRoom;
				list2.Add(item);
			}
			dormHouse.RoomLists.AddRange(list2);
			list.Add(dormHouse);
		}
		dormsScheme3.HouseLists = new List<DormsScheme.DormHouse>();
		dormsScheme3.HouseLists.AddRange(list);
		List<DepotFurniture> list3 = new List<DepotFurniture>();
		DormitoryFurnitureDataExcel[] all2 = BaseExcelReader<DormitoryFurnitureData, DormitoryFurnitureDataExcel>.GetInstance().All;
		foreach (DormitoryFurnitureDataExcel dormitoryFurnitureDataExcel in all2)
		{
			DepotFurniture item2 = new DepotFurniture
			{
				Id = dormitoryFurnitureDataExcel.FurnitureId,
				Num = 1u
			};
			list3.Add(item2);
		}
		dormsScheme3.DepotFurnitureLists = new List<DepotFurniture>();
		dormsScheme3.DepotFurnitureLists.AddRange(list3);
		// 私服默认全家具可用（和仓库一致），否则新号摆不了任何东西；解锁/购买协议仍然生效。
		dormsScheme3.UnlockFurnitureIdLists = dormsScheme3.DepotFurnitureLists.Select((DepotFurniture x) => x.Id).ToArray();
		dormsScheme3.LastResetTime = (uint)(Global.GetUnixInSeconds() / 86400);
		dormsScheme3.LevelUpTargetLevel = 0u;
		dormsScheme3.LastEventUpdateTime = 0u;
		dormsScheme3.FacilityData = new DormsScheme.DormFacilityData
		{
			LevelUpFacilityType = 0u,
			LevelUpEndTime = 0u,
			StoreStamina = 0u,
			IsCanStoreStamina = false,
			Power = 0u,
			PowerRecoverLeftTime = 0u,
			PowerRecoverConfigTime = 0u,
			PowerLimit = 0u,
			BasePowerLimit = 0u,
			PowerRecovery = 0u,
			BasePowerRecovery = 0u,
			BonusPowerRecovery = 0u
		};
		List<DormFacility> list4 = new List<DormFacility>();
		DormFacility item3 = new DormFacility
		{
			Type = 1u,
			Level = 1u,
			Id = 101u,
			BeginTime = 0u
		};
		list4.Add(item3);
		dormsScheme3.FacilityData.FacilityLists.AddRange(list4);
		List<DormPowerBonus> list5 = new List<DormPowerBonus>();
		DormPowerBonus item4 = new DormPowerBonus
		{
			HouseId = 101u,
			Bonus = 0u
		};
		list5.Add(item4);
		dormsScheme3.FacilityData.LimitBonusLists.AddRange(list5);
		List<uint> list6 = new List<uint>();
		AvatarMemoirDataExcel[] all3 = BaseExcelReader<AvatarMemoirData, AvatarMemoirDataExcel>.GetInstance().All;
		foreach (AvatarMemoirDataExcel avatarMemoirDataExcel in all3)
		{
			list6.Add(avatarMemoirDataExcel.AvatarId);
		}
		StigmataMemoirDataExcel[] all4 = BaseExcelReader<StigmataMemoirData, StigmataMemoirDataExcel>.GetInstance().All;
		foreach (StigmataMemoirDataExcel stigmataMemoirDataExcel in all4)
		{
			list6.Add(stigmataMemoirDataExcel.AvatarId);
		}
		dormsScheme3.UnlockAvatarLists = list6.ToArray();
		collection.InsertOne(dormsScheme3);
		return dormsScheme3;
	}
}
