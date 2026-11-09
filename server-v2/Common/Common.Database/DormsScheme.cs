using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

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

		/// <summary>今天收到的赞（跨天清零，用于 LikeDorm 的接收上限）。</summary>
		public uint BeLikedToday { get; set; }

		/// <summary>今天我点过赞的玩家 uid（用于 LikeDorm 的 HAS_LIKED）。</summary>
		public uint[] LikedTargetLists { get; set; } = new uint[0];

		/// <summary>今天给我点过赞的玩家 uid（用于 GetOtherDormData 的 is_has_liked）。</summary>
		public uint[] LikedByLists { get; set; } = new uint[0];
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

	/// <summary>已解锁图纸的家具 id（GetHasGotFurnitureIdList.has_unlock_furniture_id_list）。</summary>
	public uint[] UnlockFurnitureIdLists { get; set; }

	/// <summary>上一次跨天重置的 UTC 天数（now / 86400）。</summary>
	public uint LastResetTime { get; set; }

	/// <summary>正在升级的宿舍目标等级（proto 只回 level_up_house_id/end_time，目标等级单独存）。</summary>
	public uint LevelUpTargetLevel { get; set; }

	/// <summary>上一次生成宿舍随机事件的 UTC 天数，避免同一天反复刷新。</summary>
	public uint LastEventUpdateTime { get; set; }

	public void Save()
	{
		Dorms.collection.ReplaceOne(this);
	}

	/// <summary>
	/// 老存档（v1 迁移）没有后加的字段，统一在这里补齐。返回是否改过，改过才需要 Save。
	/// </summary>
	public bool EnsureDefaults()
	{
		bool changed = false;
		if (HouseLists == null)
		{
			HouseLists = new List<DormHouse>();
			changed = true;
		}
		foreach (DormHouse house in HouseLists)
		{
			if (house.RoomLists == null)
			{
				house.RoomLists = new List<DormRoom>();
				changed = true;
			}
			foreach (DormRoom room in house.RoomLists)
			{
				if (room.FurnitureLists == null)
				{
					room.FurnitureLists = new List<Furniture>();
					changed = true;
				}
				if (room.AvatarLists == null)
				{
					room.AvatarLists = new uint[0];
					changed = true;
				}
			}
		}
		if (DepotFurnitureLists == null)
		{
			DepotFurnitureLists = new List<DepotFurniture>();
			changed = true;
		}
		if (FinishTalkPlotIdLists == null)
		{
			FinishTalkPlotIdLists = new uint[0];
			changed = true;
		}
		if (UnlockAvatarLists == null)
		{
			UnlockAvatarLists = new uint[0];
			changed = true;
		}
		if (EventLists == null)
		{
			EventLists = new List<DormEvent>();
			changed = true;
		}
		if (FacilityData == null)
		{
			FacilityData = new DormFacilityData();
			changed = true;
		}
		if (FacilityData.FacilityLists == null)
		{
			FacilityData.FacilityLists = new List<DormFacility>();
			changed = true;
		}
		if (FacilityData.LimitBonusLists == null)
		{
			FacilityData.LimitBonusLists = new List<DormPowerBonus>();
			changed = true;
		}
		if (SnsData == null)
		{
			SnsData = new DormSnsData();
			changed = true;
		}
		if (SnsData.RecordLists == null)
		{
			SnsData.RecordLists = new List<DormVisitRecord>();
			changed = true;
		}
		if (SnsData.LikedTargetLists == null)
		{
			SnsData.LikedTargetLists = new uint[0];
			changed = true;
		}
		if (SnsData.LikedByLists == null)
		{
			SnsData.LikedByLists = new uint[0];
			changed = true;
		}
		if (CheckDormLikes == null)
		{
			CheckDormLikes = new CheckDormLikesDrop();
			changed = true;
		}
		if (UnlockFurnitureIdLists == null)
		{
			// 老存档：仓库里已有的家具一律视为图纸已解锁，再补上配置里的初始解锁。
			IEnumerable<uint> owned = DepotFurnitureLists.Select((DepotFurniture x) => x.Id);
			IEnumerable<uint> initial = BaseExcelReader<DormitoryFurnitureData, DormitoryFurnitureDataExcel>.GetInstance().All
				.Where((DormitoryFurnitureDataExcel x) => x.InitialUnLock != 0)
				.Select((DormitoryFurnitureDataExcel x) => x.FurnitureId);
			UnlockFurnitureIdLists = owned.Concat(initial).Distinct().ToArray();
			changed = true;
		}
		return changed;
	}

	public DormHouse GetHouse(uint houseId)
	{
		return HouseLists?.FirstOrDefault((DormHouse house) => house.Id == houseId);
	}

	public DormRoom GetRoom(uint houseId, uint roomId)
	{
		return GetHouse(houseId)?.RoomLists?.FirstOrDefault((DormRoom room) => room.Id == roomId);
	}

	/// <summary>改名：roomId 为 0 时改宿舍名，否则改房间名。返回 0=宿舍不存在 1=房间不存在 2=成功。</summary>
	public uint SetDormName(uint houseId, uint roomId, string name)
	{
		DormHouse house = GetHouse(houseId);
		if (house == null)
		{
			return 0u;
		}
		if (roomId == 0)
		{
			house.Name = name;
			Save();
			return 2u;
		}
		DormRoom room = GetRoom(houseId, roomId);
		if (room == null)
		{
			return 1u;
		}
		room.Name = name;
		Save();
		return 2u;
	}

	/// <summary>保存整个房间的摆放（客户端每次上报完整布局，直接替换）。返回 0=宿舍不存在 1=房间不存在 2=成功。</summary>
	public uint EditDorm(uint houseId, uint roomId, List<Furniture> furnitureLists)
	{
		DormHouse house = GetHouse(houseId);
		if (house == null)
		{
			return 0u;
		}
		DormRoom room = GetRoom(houseId, roomId);
		if (room == null)
		{
			return 1u;
		}
		room.FurnitureLists = (furnitureLists ?? new List<Furniture>()).Select((Furniture furniture) => new Furniture
		{
			Id = furniture.Id,
			PosX = furniture.PosX,
			PosY = furniture.PosY,
			Direction = furniture.Direction,
			Location = furniture.Location
		}).ToList();
		Save();
		return 2u;
	}

	/// <summary>
	/// 设置宿舍里的角色。先把这个角色从本宿舍其它房间摘掉，再放进目标房间。
	/// 返回 0=宿舍不存在 1=房间不存在 2=成功。
	/// </summary>
	public uint SetDormAvatar(uint houseId, List<DormRoomAvatar> roomAvatarLists)
	{
		DormHouse house = GetHouse(houseId);
		if (house == null)
		{
			return 0u;
		}
		foreach (DormRoomAvatar assign in roomAvatarLists ?? new List<DormRoomAvatar>())
		{
			if (GetRoom(houseId, assign.RoomId) == null)
			{
				return 1u;
			}
			uint[] avatars = assign.AvatarLists ?? new uint[0];
			foreach (DormRoom room in house.RoomLists)
			{
				room.AvatarLists = (room.AvatarLists ?? new uint[0]).Where((uint id) => !avatars.Contains(id)).ToArray();
			}
			DormRoom target = GetRoom(houseId, assign.RoomId);
			List<uint> merged = target.AvatarLists.ToList();
			foreach (uint avatar in avatars)
			{
				if (!merged.Contains(avatar))
				{
					merged.Add(avatar);
				}
			}
			target.AvatarLists = merged.ToArray();
		}
		Save();
		return 2u;
	}

	public bool IsFurnitureUnlocked(uint furnitureId)
	{
		return UnlockFurnitureIdLists != null && UnlockFurnitureIdLists.Contains(furnitureId);
	}

	public void UnlockFurniture(uint furnitureId)
	{
		if (!IsFurnitureUnlocked(furnitureId))
		{
			UnlockFurnitureIdLists = (UnlockFurnitureIdLists ?? new uint[0]).Append(furnitureId).ToArray();
		}
	}

	public int GetDepotCount(uint furnitureId)
	{
		return (int)(DepotFurnitureLists?.FirstOrDefault((DepotFurniture x) => x.Id == furnitureId)?.Num ?? 0);
	}

	public void AddDepotFurniture(uint furnitureId, int num)
	{
		if (num <= 0)
		{
			return;
		}
		DepotFurniture entry = DepotFurnitureLists.FirstOrDefault((DepotFurniture x) => x.Id == furnitureId);
		if (entry == null)
		{
			DepotFurnitureLists.Add(new DepotFurniture
			{
				Id = furnitureId,
				Num = (uint)num
			});
		}
		else
		{
			entry.Num += (uint)num;
		}
	}

	/// <summary>从仓库扣家具；数量不足返回 false，不做任何修改。</summary>
	public bool RemoveDepotFurniture(uint furnitureId, int num)
	{
		if (num <= 0)
		{
			return true;
		}
		DepotFurniture entry = DepotFurnitureLists.FirstOrDefault((DepotFurniture x) => x.Id == furnitureId);
		if (entry == null || entry.Num < num)
		{
			return false;
		}
		entry.Num -= (uint)num;
		if (entry.Num == 0)
		{
			DepotFurnitureLists.Remove(entry);
		}
		return true;
	}

	/// <summary>宿舍舒适度（GetOtherDormData 回显用，按家具配置直接累加）。</summary>
	public int ComfortValue(uint houseId)
	{
		DormHouse house = GetHouse(houseId);
		if (house == null)
		{
			return 0;
		}
		int total = 0;
		foreach (DormRoom room in house.RoomLists)
		{
			foreach (Furniture furniture in room.FurnitureLists)
			{
				DormitoryFurnitureDataExcel cfg = BaseExcelReader<DormitoryFurnitureData, DormitoryFurnitureDataExcel>.GetInstance().FromId((int)furniture.Id);
				if (cfg != null)
				{
					total += cfg.ComfortValue;
				}
			}
		}
		return total;
	}

	/// <summary>每日重置：昨天获赞结算成点赞掉落、当天点赞/事件清空。</summary>
	public bool RefreshDaily(long now)
	{
		if (SnsData == null || CheckDormLikes == null)
		{
			EnsureDefaults();
		}
		uint day = (uint)(now / 86400);
		if (LastResetTime == day)
		{
			return false;
		}
		CheckDormLikes.YesterdayBeLikedNum = SnsData.BeLikedNum;
		CheckDormLikes.HasLikesDrop = SnsData.BeLikedNum > 0;
		SnsData.BeLikedNum = 0u;
		SnsData.BeLikedToday = 0u;
		SnsData.LikedTargetLists = new uint[0];
		SnsData.LikedByLists = new uint[0];
		LastResetTime = day;
		EventLists.Clear();
		LastEventUpdateTime = 0u;
		Save();
		return true;
	}

	/// <summary>把到期的宿舍升级结算掉（登录/拉数据时调用）。</summary>
	public bool FinalizeLevelUp(long now)
	{
		if (LevelUpHouseId == 0u || LevelUpEndTime == 0u || LevelUpEndTime > now)
		{
			return false;
		}
		DormHouse house = GetHouse(LevelUpHouseId);
		if (house != null)
		{
			uint target = LevelUpTargetLevel > house.Level ? LevelUpTargetLevel : house.Level + 1;
			house.Level = target;
		}
		LevelUpEndHouseId = LevelUpHouseId;
		LevelUpHouseId = 0u;
		LevelUpEndTime = 0u;
		LevelUpTargetLevel = 0u;
		Save();
		return true;
	}

	public bool IsLevelUpBusy()
	{
		return LevelUpHouseId != 0u && LevelUpEndTime > Global.GetUnixInSeconds();
	}
}
