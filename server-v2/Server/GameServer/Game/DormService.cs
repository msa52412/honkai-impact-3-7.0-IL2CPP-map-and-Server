using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;
using ProtoRewardData = Common.Resources.Proto.RewardData;
using Material = Common.Resources.Proto.Material;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 家园（宿舍）业务层。
///
/// 协议族（cmd 601~642 / 680 / 1008~1011 / 1134~1139）：
///   拉数据        GetDormData / GetDormHouse / GetDepotFurniture / GetHasGotFurnitureIdList
///   装修          EditDormRoom / SetDormAvatar / SetDormName / UnlockDormHouse / LevelUpDorm
///   家具          BuyFurniture / UnlockFurniture / DisjoinFurniture
///   剧情/事件      FinishDormTalk / FinishDormEvent
///   社交          SetDormSnsInfo / GetDormSnsData / GetOtherDormData / LikeDorm
///   每日点赞掉落    CheckDormLikesDrop / TakeDormLikesDrop
///
/// 配置对应：DormitoryHouseData / DormitoryRoomData / DormitoryFurnitureData /
///           DormitoryDecoration / DormitoryTalkEvent / DormitoryInteractEvent /
///           DormitoryEventWeight / DormitoryAvatarConfig / RewardData。
/// </summary>
internal static class DormService
{
	/// <summary>每日最多给多少人点赞 / 最多收到多少赞（客户端 is_send_like_limit / is_receive_like_limit）。</summary>
	private const int DailySendLikeLimit = 10;

	private const int DailyReceiveLikeLimit = 10;

	/// <summary>宿舍访问记录最多保留条数。</summary>
	private const int MaxVisitRecords = 20;

	/// <summary>点赞成功固定给的好友点（只用于回包展示）。</summary>
	private const uint LikeFriendsPoint = 5u;

	private static DormitoryFurnitureData FurnitureReader => BaseExcelReader<DormitoryFurnitureData, DormitoryFurnitureDataExcel>.GetInstance();

	private static DormitoryHouseData HouseReader => BaseExcelReader<DormitoryHouseData, DormitoryHouseDataExcel>.GetInstance();

	private static DormitoryRoomData RoomReader => BaseExcelReader<DormitoryRoomData, DormitoryRoomDataExcel>.GetInstance();

	private static DormitoryDecoration DecorationReader => BaseExcelReader<DormitoryDecoration, DormitoryDecorationExcel>.GetInstance();

	private static DormitoryTalkEvent TalkReader => BaseExcelReader<DormitoryTalkEvent, DormitoryTalkEventExcel>.GetInstance();

	private static DormitoryInteractEvent InteractReader => BaseExcelReader<DormitoryInteractEvent, DormitoryInteractEventExcel>.GetInstance();

	private static DormitoryEventWeight EventWeightReader => BaseExcelReader<DormitoryEventWeight, DormitoryEventWeightExcel>.GetInstance();

	private static DormitoryAvatarConfig AvatarConfigReader => BaseExcelReader<DormitoryAvatarConfig, DormitoryAvatarConfigExcel>.GetInstance();

	/// <summary>拉取/修改家园数据前的统一整理：结算升级、跨天重置、补随机事件。</summary>
	internal static void Prepare(DormsScheme dorms)
	{
		long now = Global.GetUnixInSeconds();
		dorms.EnsureDefaults();
		dorms.FinalizeLevelUp(now);
		dorms.RefreshDaily(now);
		EnsureEvents(dorms, now);
	}

	#region 协议结构转换

	internal static DormHouse ToProto(DormsScheme.DormHouse house)
	{
		DormHouse proto = new DormHouse
		{
			Id = house.Id,
			Name = house.Name ?? "",
			Level = house.Level
		};
		foreach (DormsScheme.DormRoom room in house.RoomLists ?? new List<DormsScheme.DormRoom>())
		{
			proto.RoomLists.Add(ToProto(room));
		}
		return proto;
	}

	internal static DormRoom ToProto(DormsScheme.DormRoom room)
	{
		DormRoom proto = new DormRoom
		{
			Id = room.Id,
			Name = room.Name ?? "",
			AvatarLists = room.AvatarLists ?? new uint[0]
		};
		foreach (Furniture furniture in room.FurnitureLists ?? new List<Furniture>())
		{
			proto.FurnitureLists.Add(new Furniture
			{
				Id = furniture.Id,
				PosX = furniture.PosX,
				PosY = furniture.PosY,
				Direction = furniture.Direction,
				Location = furniture.Location
			});
		}
		return proto;
	}

	internal static void FillDormData(GetDormDataRsp rsp, DormsScheme dorms, DormDataType type)
	{
		bool all = type == DormDataType.DormDataAll;
		if (all || type == DormDataType.DormDataDecoration)
		{
			foreach (DormsScheme.DormHouse house in dorms.HouseLists)
			{
				rsp.HouseLists.Add(ToProto(house));
			}
			foreach (DepotFurniture furniture in dorms.DepotFurnitureLists)
			{
				rsp.DepotFurnitureLists.Add(new DepotFurniture
				{
					Id = furniture.Id,
					Num = furniture.Num
				});
			}
			rsp.FacilityData = ToProto(dorms.FacilityData);
			rsp.LevelUpHouseId = dorms.LevelUpHouseId;
			rsp.LevelUpEndTime = dorms.LevelUpEndTime;
			rsp.LevelUpEndHouseId = dorms.LevelUpEndHouseId;
			rsp.UnlockAvatarLists = dorms.UnlockAvatarLists ?? new uint[0];
		}
		if (all || type == DormDataType.DormDataEvent)
		{
			rsp.FinishTalkPlotIdLists = dorms.FinishTalkPlotIdLists ?? new uint[0];
			foreach (DormEvent dormEvent in dorms.EventLists)
			{
				rsp.EventLists.Add(new DormEvent
				{
					AvatarId = dormEvent.AvatarId,
					EventIdLists = dormEvent.EventIdLists ?? new uint[0]
				});
			}
		}
		if (all || type == DormDataType.DormDataSns)
		{
			rsp.ShowHouse = dorms.ShowHouse;
			rsp.ShowRoom = dorms.ShowRoom;
			rsp.VisitAvatar = dorms.VisitAvatar;
			rsp.IsAllowVisit = dorms.IsAllowVisit;
		}
	}

	internal static DormFacilityData ToProto(DormsScheme.DormFacilityData data)
	{
		if (data == null)
		{
			return new DormFacilityData();
		}
		DormFacilityData proto = new DormFacilityData
		{
			LevelUpFacilityType = data.LevelUpFacilityType,
			LevelUpEndTime = data.LevelUpEndTime,
			StoreStamina = data.StoreStamina,
			IsCanStoreStamina = data.IsCanStoreStamina,
			Power = data.Power,
			PowerRecoverLeftTime = data.PowerRecoverLeftTime,
			PowerRecoverConfigTime = data.PowerRecoverConfigTime,
			PowerLimit = data.PowerLimit,
			BasePowerLimit = data.BasePowerLimit,
			PowerRecovery = data.PowerRecovery,
			BasePowerRecovery = data.BasePowerRecovery,
			BonusPowerRecovery = data.BonusPowerRecovery
		};
		foreach (DormFacility facility in data.FacilityLists ?? new List<DormFacility>())
		{
			proto.FacilityLists.Add(new DormFacility
			{
				Type = facility.Type,
				Level = facility.Level,
				Id = facility.Id,
				BeginTime = facility.BeginTime
			});
		}
		foreach (DormPowerBonus bonus in data.LimitBonusLists ?? new List<DormPowerBonus>())
		{
			proto.LimitBonusLists.Add(new DormPowerBonus
			{
				Bonus = bonus.Bonus,
				HouseId = bonus.HouseId
			});
		}
		return proto;
	}

	#endregion

	#region 材料 / 家具

	internal static int PlayerLevel(UserScheme user)
	{
		return BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance().CalculateLevel(user.Exp).Level;
	}

	/// <summary>把若干家具配置里的材料需求按数量倍率合并。</summary>
	internal static Dictionary<uint, int> SumMaterials(IEnumerable<LevelUpMaterialList> materials, int multiplier = 1)
	{
		Dictionary<uint, int> result = new Dictionary<uint, int>();
		foreach (LevelUpMaterialList material in materials ?? Enumerable.Empty<LevelUpMaterialList>())
		{
			if (material == null || material.MaterialId == 0u)
			{
				continue;
			}
			int num = (int)material.Count * multiplier;
			result.TryGetValue(material.MaterialId, out int current);
			result[material.MaterialId] = current + num;
		}
		return result;
	}

	internal static bool HasMaterials(Player player, Dictionary<uint, int> materials)
	{
		foreach (KeyValuePair<uint, int> material in materials)
		{
			Material owned = player.Equipment.MaterialList.FirstOrDefault((Material x) => x.Id == material.Key);
			if (owned == null || owned.Num < material.Value)
			{
				return false;
			}
		}
		return true;
	}

	internal static void ConsumeMaterials(Player player, Dictionary<uint, int> materials)
	{
		foreach (KeyValuePair<uint, int> material in materials)
		{
			player.Equipment.AddMaterial((int)material.Key, -material.Value);
		}
	}

	internal static void GrantMaterials(Player player, Dictionary<uint, int> materials)
	{
		foreach (KeyValuePair<uint, int> material in materials)
		{
			player.Equipment.AddMaterial((int)material.Key, material.Value);
		}
	}

	/// <summary>玩家当前拥有的某件家具总数（仓库 + 已摆到房间里的），KeepLimit 按这个算。</summary>
	internal static int OwnedFurnitureCount(DormsScheme dorms, uint furnitureId)
	{
		int count = dorms.GetDepotCount(furnitureId);
		foreach (DormsScheme.DormHouse house in dorms.HouseLists ?? new List<DormsScheme.DormHouse>())
		{
			foreach (DormsScheme.DormRoom room in house.RoomLists ?? new List<DormsScheme.DormRoom>())
			{
				foreach (Furniture furniture in room.FurnitureLists ?? new List<Furniture>())
				{
					if (furniture.Id == furnitureId)
					{
						count++;
					}
				}
			}
		}
		return count;
	}

	#endregion

	#region 好友 / 社交

	/// <summary>玩家自己当前展示的宿舍 id（show_house 失效时退回列表里的第一个）。</summary>
	internal static uint CurrentShowHouse(DormsScheme dorms)
	{
		if (dorms.GetHouse(dorms.ShowHouse) != null)
		{
			return dorms.ShowHouse;
		}
		return dorms.HouseLists?.FirstOrDefault()?.Id ?? 0u;
	}

	internal static void AddVisitRecord(Player visitor, DormsScheme target, uint visitAvatar, uint level, string name)
	{
		target.EnsureDefaults();
		if (visitor.User.Uid == target.OwnerUid)
		{
			return;
		}
		DormVisitRecord record = new DormVisitRecord
		{
			VisitTime = (uint)Global.GetUnixInSeconds(),
			VisitUid = visitor.User.Uid,
			VisitAvatar = visitAvatar != 0u ? visitAvatar : (uint)visitor.User.AssistantAvatarId,
			Level = level != 0u ? level : (uint)PlayerLevel(visitor.User),
			Name = string.IsNullOrEmpty(name) ? (visitor.User.Nick ?? "") : name
		};
		target.SnsData.RecordLists.Insert(0, record);
		if (target.SnsData.RecordLists.Count > MaxVisitRecords)
		{
			target.SnsData.RecordLists.RemoveRange(MaxVisitRecords, target.SnsData.RecordLists.Count - MaxVisitRecords);
		}
		target.SnsData.BeVisitNum++;
		target.Save();
	}

	internal static bool IsSendLikeLimit(DormsScheme dorms)
	{
		return (dorms.SnsData.LikedTargetLists?.Length ?? 0) >= DailySendLikeLimit;
	}

	internal static bool IsReceiveLikeLimit(DormsScheme dorms)
	{
		return dorms.SnsData.BeLikedToday >= DailyReceiveLikeLimit;
	}

	internal static bool HasLiked(DormsScheme dorms, uint targetUid)
	{
		return dorms.SnsData.LikedTargetLists != null && dorms.SnsData.LikedTargetLists.Contains(targetUid);
	}

	internal static bool HasLikedMe(DormsScheme dorms, uint visitorUid)
	{
		return dorms.SnsData.LikedByLists != null && dorms.SnsData.LikedByLists.Contains(visitorUid);
	}

	internal static void AddLike(Player player, DormsScheme target, uint targetUid)
	{
		player.Dorms.SnsData.LikedTargetLists = (player.Dorms.SnsData.LikedTargetLists ?? new uint[0]).Append(targetUid).ToArray();
		player.Dorms.Save();

		target.SnsData.LikedByLists = (target.SnsData.LikedByLists ?? new uint[0]).Append(player.User.Uid).ToArray();
		target.SnsData.BeLikedNum++;
		target.SnsData.BeLikedToday++;
		target.Save();
	}

	internal static uint LikeFriendsPointValue => LikeFriendsPoint;

	#endregion

	#region 点赞掉落

	/// <summary>
	/// 昨天的获赞数对应的奖励档位（DormitoryCollectReward 里 finishWay=10189 的 4 档：3/12/30/50 赞）。
	/// 返回 0 表示不到第一档。
	/// </summary>
	internal static int LikesDropRewardId(uint yesterdayBeLikedNum)
	{
		if (yesterdayBeLikedNum >= 50)
		{
			return 121104;
		}
		if (yesterdayBeLikedNum >= 30)
		{
			return 121103;
		}
		if (yesterdayBeLikedNum >= 12)
		{
			return 121102;
		}
		if (yesterdayBeLikedNum >= 3)
		{
			return 121101;
		}
		return 0;
	}

	internal static ProtoRewardData GrantReward(Player player, int rewardId)
	{
		if (rewardId <= 0)
		{
			return null;
		}
		RewardDataExcel cfg = BaseExcelReader<Common.Utils.ExcelReader.RewardData, RewardDataExcel>.GetInstance().FromId(rewardId);
		if (cfg == null)
		{
			Packet.c.Warn($"[Dorm] RewardData 里找不到 rewardId={rewardId}");
			return null;
		}
		return RewardService.Grant(player, cfg);
	}

	/// <summary>把发奖结果再摊平成客户端 Drop 弹窗用的 DropItem 列表。</summary>
	internal static void AppendDropItems(List<DropItem> target, ProtoRewardData reward)
	{
		if (reward == null)
		{
			return;
		}
		foreach (RewardItemData item in reward.ItemLists)
		{
			target.Add(new DropItem
			{
				ItemId = item.Id,
				Num = item.Num,
				Level = item.Level
			});
		}
	}

	#endregion

	#region 剧情 / 事件

	internal static void AddGoodfeel(Player player, uint avatarId, int exp)
	{
		AvatarScheme avatar = player.AvatarList.FirstOrDefault((AvatarScheme x) => x.AvatarId == avatarId);
		if (avatar == null || exp <= 0)
		{
			return;
		}
		avatar.TodayHasAddGoodfeel += (uint)exp;
		avatar.TouchGoodfeel += (uint)exp;
		avatar.Save();
	}

	/// <summary>
	/// 每天给房间里的角色随机挂 1 个互动事件（DormitoryEventWeight 按角色 RoleID 加权）。
	/// 已完成的不会在同一天再生成；跨天由 RefreshDaily 清空。
	/// </summary>
	internal static void EnsureEvents(DormsScheme dorms, long now)
	{
		uint day = (uint)(now / 86400);
		if (dorms.LastEventUpdateTime == day)
		{
			return;
		}
		dorms.EventLists.Clear();
		List<uint> avatars = new List<uint>();
		foreach (DormsScheme.DormHouse house in dorms.HouseLists ?? new List<DormsScheme.DormHouse>())
		{
			foreach (DormsScheme.DormRoom room in house.RoomLists ?? new List<DormsScheme.DormRoom>())
			{
				foreach (uint avatar in room.AvatarLists ?? new uint[0])
				{
					if (!avatars.Contains(avatar))
					{
						avatars.Add(avatar);
					}
				}
			}
		}
		Random random = new Random();
		foreach (uint avatarId in avatars)
		{
			DormitoryAvatarConfigExcel avatarCfg = AvatarConfigReader.FromAvatarId(avatarId);
			if (avatarCfg == null)
			{
				continue;
			}
			List<DormitoryEventWeightExcel> weights = EventWeightReader.FromRoleId(avatarCfg.RoleId);
			if (weights.Count == 0)
			{
				continue;
			}
			int total = weights.Sum((DormitoryEventWeightExcel x) => Math.Max(1, x.Weight));
			int roll = random.Next(total);
			uint eventId = weights[0].EventId;
			foreach (DormitoryEventWeightExcel weight in weights)
			{
				roll -= Math.Max(1, weight.Weight);
				if (roll < 0)
				{
					eventId = weight.EventId;
					break;
				}
			}
			dorms.EventLists.Add(new DormEvent
			{
				AvatarId = avatarId,
				EventIdLists = new uint[1] { eventId }
			});
		}
		dorms.LastEventUpdateTime = day;
		dorms.Save();
	}

	internal static void ResetEvents(DormsScheme dorms)
	{
		dorms.EventLists.Clear();
		dorms.LastEventUpdateTime = 0u;
		dorms.Save();
	}

	internal static DormitoryTalkEventExcel TalkConfig(uint plotId)
	{
		return TalkReader.FromPlotId(plotId);
	}

	internal static DormitoryInteractEventExcel InteractConfig(uint eventId)
	{
		return InteractReader.FromEventId(eventId);
	}

	#endregion

	#region 宿舍 / 房间配置

	internal static DormitoryHouseDataExcel HouseConfig(uint houseId)
	{
		return HouseReader.FromId((int)houseId);
	}

	internal static DormitoryRoomDataExcel RoomConfig(uint roomId)
	{
		return RoomReader.FromId((int)roomId);
	}

	internal static DormitoryFurnitureDataExcel FurnitureConfig(uint furnitureId)
	{
		return FurnitureReader.FromId((int)furnitureId);
	}

	internal static DormitoryDecorationExcel DecorationConfig(uint houseId, uint level)
	{
		return DecorationReader.FromLevel((int)houseId, (int)level);
	}

	internal static DormsScheme.DormHouse CreateHouse(DormitoryHouseDataExcel cfg)
	{
		DormsScheme.DormHouse house = new DormsScheme.DormHouse
		{
			Id = (uint)cfg.HouseId,
			Name = "",
			Level = 1u
		};
		for (int i = 0; i < (cfg.TotalRoomList?.Length ?? 0); i++)
		{
			house.RoomLists.Add(new DormsScheme.DormRoom
			{
				Id = (uint)cfg.TotalRoomList[i],
				Name = "",
				FurnitureLists = new List<Furniture>(),
				AvatarLists = new uint[0]
			});
		}
		return house;
	}

	#endregion
}
