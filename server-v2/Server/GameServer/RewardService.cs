using System;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;
using ProtoRewardData = Common.Resources.Proto.RewardData;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 通用发奖：把一条 RewardData 配置真正写进存档，并返回给客户端展示用的结构。
///
/// 原本这段逻辑埋在 <see cref="LoginActivityService"/> 里（授勋领奖用），
/// 收藏评分领奖（TakeWikiRatingReward）也要用同一套，所以抽出来。
///
/// 物品 id 不带类型，只能靠各配置表反查 —— 判断顺序按「越具体越先」：
/// 武器 → 圣痕 → 材料 → 角色卡(avatarCardID) → 服装(dressID)。
/// </summary>
internal static class RewardService
{
	internal static ProtoRewardData Grant(Player player, RewardDataExcel cfg)
	{
		ProtoRewardData proto = new ProtoRewardData
		{
			Exp = (uint)Math.Max(0, cfg.RewardExp),
			Hcoin = (uint)Math.Max(0, cfg.RewardHCoin),
			Stamina = (uint)Math.Max(0, cfg.RewardStamina),
			FriendsPoint = (uint)Math.Max(0, cfg.RewardFriendPoint)
		};

		if (cfg.RewardExp > 0)
		{
			player.User.AddExp(cfg.RewardExp);
		}

		if (cfg.RewardHCoin > 0)
		{
			player.User.Hcoin += cfg.RewardHCoin;
		}

		if (cfg.RewardStamina > 0)
		{
			player.User.Stamina += cfg.RewardStamina;
		}

		int[] itemIds =
		{
			cfg.RewardItem1Id, cfg.RewardItem2Id, cfg.RewardItem3Id,
			cfg.RewardItem4Id, cfg.RewardItem5Id, cfg.RewardItem6Id
		};
		int[] itemNums =
		{
			cfg.RewardItem1Num, cfg.RewardItem2Num, cfg.RewardItem3Num,
			cfg.RewardItem4Num, cfg.RewardItem5Num, cfg.RewardItem6Num
		};

		for (int i = 0; i < itemIds.Length; i++)
		{
			int itemId = itemIds[i];
			if (itemId <= 0)
			{
				continue;
			}

			uint num = (uint)Math.Max(1, itemNums[i]);
            var converted=new System.Collections.Generic.Dictionary<int,long>{{itemId,num}};
            Post62Materials.Normalize(converted);var actual=converted.Single();itemId=actual.Key;num=checked((uint)actual.Value);
			GrantItem(player, itemId, num);

			proto.ItemLists.Add(new RewardItemData
			{
				Id = (uint)itemId,
				Num = num
			});
		}

		return proto;
	}

	private static void GrantItem(Player player, int itemId, uint num)
	{
		uint uid = player.User.Uid;

		if (BaseExcelReader<WeaponData, WeaponDataExcel>.GetInstance().FromId(itemId) != null)
		{
			for (uint i = 0; i < num; i++)
			{
				player.Equipment.AddWeapon(itemId);
			}

			return;
		}

		if (BaseExcelReader<StigmataData, StigmataDataExcel>.GetInstance().FromId(itemId) != null)
		{
			for (uint i = 0; i < num; i++)
			{
				player.Equipment.AddStigmata(itemId);
			}

			return;
		}

		if (BaseExcelReader<MaterialData, MaterialDataExcel>.GetInstance().FromId((uint)itemId) != null)
		{
			player.Equipment.AddMaterial(itemId, (int)num);
			return;
		}

		AvatarDataExcel? card = BaseExcelReader<AvatarData, AvatarDataExcel>.GetInstance().All
			.FirstOrDefault(x => x.AvatarCardId == itemId);
		if (card != null)
		{
			if (player.AvatarList.All(x => x.AvatarId != (uint)card.AvatarId))
			{
				AvatarScheme avatar = Common.Database.Avatar.Create(card.AvatarId, uid, player.Equipment, false);
				player.AvatarList = player.AvatarList.Append(avatar).ToArray();
			}

			return;
		}

		DressDataExcel? dress = BaseExcelReader<DressData, DressDataExcel>.GetInstance().All
			.FirstOrDefault(x => x.DressId == itemId);
		if (dress != null)
		{
			foreach (int avatarId in dress.AvatarIdList ?? Array.Empty<int>())
			{
				AvatarScheme? avatar = player.AvatarList.FirstOrDefault(x => x.AvatarId == (uint)avatarId);
				if (avatar == null)
				{
					continue;
				}

				if (avatar.DressLists == null || !avatar.DressLists.Contains((uint)dress.DressId))
				{
					avatar.DressLists = (avatar.DressLists ?? Array.Empty<uint>())
						.Append((uint)dress.DressId).ToArray();
					avatar.Save();
				}
			}

			return;
		}

		Packet.c.Warn($"[Reward] 物品 {itemId} 在武器/圣痕/材料/角色卡/服装里都查不到，只回给客户端不落库");
	}
}
