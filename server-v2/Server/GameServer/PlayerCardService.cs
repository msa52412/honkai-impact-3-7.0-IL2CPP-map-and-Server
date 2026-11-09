using System;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Game;

internal static class PlayerCardService
{
	internal static uint[] Slots(uint[] saved, int count)
	{
		var slots = new uint[count];
		if (saved != null) Array.Copy(saved, slots, Math.Min(saved.Length, count));
		return slots;
	}

	internal static GetPlayerCardRsp Build(UserScheme user)
	{
		var rsp = new GetPlayerCardRsp
		{
			retcode = GetPlayerCardRsp.Retcode.Succ, Type = PlayerCardType.CardAll,
			AvatarIdLists = Slots(user.ShowcaseAvatarIds, 3), ElfIdLists = Slots(user.ShowcaseElfIds, 1),
			MedalIdLists = CompetitiveMedalService.VisibleSlots(user), MsgConfig = user.ShowcaseMsgConfig,
			MsgData = new PlayerCardMsgData { MsgIndex = 0, MsgConfig = user.ShowcaseMsgConfig }
		};
		rsp.MedalLists.AddRange(rsp.MedalIdLists.Where(id => id != 0).Select(id => CompetitiveMedalService.Get(user.Uid,id)));
		return rsp;
	}

	internal static bool Change(Player player, ChangePlayerCardReq req)
	{
		// The 7.0 avatar picker sends type 0 (omitted/default CardAll), while
		// medal and ELF pickers send their explicit types. Preserve the wire type
		// in the acknowledgement, but apply type 0 slots to the avatar showcase.
		var type = req.Type == PlayerCardType.CardAll ? PlayerCardType.CardAvatar : req.Type;
		if (req.Type == PlayerCardType.CardMsgConfig)
		{
			if (!Enum.IsDefined(typeof(PlayerCardMsgConfig), req.MsgConfig)) return false;
			player.User.ShowcaseMsgConfig = (uint)req.MsgConfig;
			player.User.Save();
			return true;
		}
		uint[] slots;
		switch (type)
		{
		case PlayerCardType.CardAvatar: slots = Slots(player.User.ShowcaseAvatarIds, 3); break;
		case PlayerCardType.CardMedal: slots = Slots(player.User.ShowcaseMedalIds, 2); break;
		case PlayerCardType.CardElf: slots = Slots(player.User.ShowcaseElfIds, 1); break;
		default: return false;
		}
		if (req.SlotLists.Count == 0 || req.SlotLists.Select(x => x.Index).Distinct().Count() != req.SlotLists.Count) return false;
		foreach (var slot in req.SlotLists)
		{
			if (slot.Index >= slots.Length) return false;
			bool owned = slot.TargetId == 0 || (type switch
			{
				PlayerCardType.CardAvatar => player.AvatarList.Any(x => x.AvatarId == slot.TargetId),
				PlayerCardType.CardElf => player.Elfs.Elves.Any(x => x.ElfId == slot.TargetId),
				PlayerCardType.CardMedal => CompetitiveMedalService.Owns(player.User.Uid,slot.TargetId),
				_ => false
			});
			if (!owned) return false;
			slots[slot.Index] = slot.TargetId;
		}
		switch (type)
		{
		case PlayerCardType.CardAvatar: player.User.ShowcaseAvatarIds = slots; break;
		case PlayerCardType.CardMedal: player.User.ShowcaseMedalIds = slots; break;
		case PlayerCardType.CardElf: player.User.ShowcaseElfIds = slots; break;
		}
		player.User.Save();
		return true;
	}

	internal static PlayerCardData PublicCard(Player player)
	{
		var user = player.User;
		var card = new PlayerCardData
		{
			Uid = user.Uid, MsgData = new PlayerCardMsgData { MsgConfig = user.ShowcaseMsgConfig, MsgIndex = 0 },
			OnPhonePendantId = user.OnPhonePendantId > 0 ? (uint)user.OnPhonePendantId : 350005u
		};
		var avatarIds = Slots(user.ShowcaseAvatarIds, 3);
		for (int i = 0; i < avatarIds.Length; i++)
		{
			var avatar = player.AvatarList.FirstOrDefault(x => x.AvatarId == avatarIds[i]);
			if (avatar != null) card.AvatarLists.Add(new PlayerCardAvatarData { Index = (uint)i, Avatar = avatar.ToDetailData(player.Equipment) });
		}
		var medalIds = CompetitiveMedalService.VisibleSlots(user);
		for (int i = 0; i < medalIds.Length; i++)
			if (medalIds[i] != 0) card.MedalLists.Add(new PlayerCardMedalData { Index = (uint)i, MedalId = medalIds[i], ExtraParam = 0 });
		var elfIds = Slots(user.ShowcaseElfIds, 1);
		for (int i = 0; i < elfIds.Length; i++)
		{
			var elf = player.Elfs.Elves.FirstOrDefault(x => x.ElfId == elfIds[i]);
			if (elf == null) continue;
			var detail = new ElfDetailData { ElfId = elf.ElfId, Level = elf.Level, Star = elf.Star };
			detail.SkillLists.AddRange(elf.SkillLists.Select(x => new ElfSkillDetailData { ElfSkillId = x.SkillId, SkillLevel = x.SkillLevel }));
			card.ElfLists.Add(new PlayerCardElfData { Index = (uint)i, Elf = detail });
		}
		return card;
	}
}
