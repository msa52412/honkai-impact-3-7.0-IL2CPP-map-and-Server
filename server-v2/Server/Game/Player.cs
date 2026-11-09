using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Game;

public class Player
{
	public UserScheme User;

	public AvatarScheme[] AvatarList;

	public EquipmentScheme Equipment;

	public List<OpenWorldScheme> OpenWorlds;

	public ElfsScheme Elfs;

	public DormsScheme Dorms;

	/// <summary>家园小队（Sanctuary，cmd 4305~4314）。和 Dorms 是两套独立系统。</summary>
	public SanctuaryScheme Sanctuary;

	public Player(UserScheme user)
	{
		User = user;
		Equipment = Common.Database.Equipment.FromUid(user.Uid);
		// 读档体检：圣痕词缀是 List 属性，曾经被 ObjectCreationHandling.Replace 静默丢掉，
		// 重登就「词缀消失」。这条日志能一眼看出读档有没有把词缀带出来。
		Packet.c.Warn("[Equip] load uid=" + user.Uid
			+ " stigmata=" + (Equipment.StigmataList?.Length ?? 0)
			+ " withRunes=" + (Equipment.StigmataList ?? System.Array.Empty<Stigmata>()).Count(x => x.RuneLists.Count > 0)
			+ " [" + string.Join(",", (Equipment.StigmataList ?? System.Array.Empty<Stigmata>())
				.Where(x => x.RuneLists.Count > 0)
				.Select(x => x.UniqueId + ":" + string.Join("/", x.RuneLists.Select(r => r.RuneId + "@" + r.StrengthPercent)))) + "]");
		AvatarList = Common.Database.Avatar.AvatarsFromUid(user.Uid);
		OpenWorlds = OpenWorld.FromUid(user.Uid);
		Elfs = Common.Database.Elfs.FromUid(user.Uid);
		Dorms = Common.Database.Dorms.FromUid(user.Uid);
		Sanctuary = Common.Database.Sanctuary.FromUid(user.Uid);
	}

	public void SaveAll()
	{
		User.Save();
		Equipment.Save();
		Common.Database.Avatar.SaveBulk(AvatarList);
		OpenWorld.SaveBulk(OpenWorlds);
		Elfs.Save();
		Dorms.Save();
		Sanctuary.Save();
	}

	public void ResetAvatarsTodayGoodfeel()
	{
		AvatarScheme[] avatarList = AvatarList;
		foreach (AvatarScheme avatarScheme in avatarList)
		{
			avatarScheme.TodayHasAddGoodfeel = 0u;
		}
	}

	public PlayerCardData GetCardData()
	{
		return PlayerCardService.PublicCard(this);
	}

	public PlayerDetailData GetDetailData()
	{
		return new PlayerDetailData
		{
			Uid = User.Uid,
			Nickname = User.Nick,
			Level = (uint)BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance().CalculateLevel(User.Exp).Level,
			SelfDesc = User.SelfDesc,
			OnMedalId = CompetitiveMedalService.Equipped(User)?.Id ?? 0,
			OnMedal = CompetitiveMedalService.Equipped(User),
			CustomHeadId = (uint)User.CustomHeadId,
			FrameId = ((User.FrameId < 200001) ? 200001u : ((uint)User.FrameId)),
			LeaderAvatar = (AvatarList.FirstOrDefault(delegate(AvatarScheme x)
			{
				uint avatarId = x.AvatarId;
				AvatarTeam? avatarTeam = User.AvatarTeamList.FirstOrDefault();
				return avatarId == ((avatarTeam != null) ? new uint?(avatarTeam!.AvatarIdLists[0]) : null);
			})?.ToDetailData(Equipment) ?? new AvatarDetailData
			{
				AvatarId = 101u
			})
		};
	}
}
