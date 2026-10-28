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

	public Player(UserScheme user)
	{
		User = user;
		Equipment = Common.Database.Equipment.FromUid(user.Uid);
		AvatarList = Common.Database.Avatar.AvatarsFromUid(user.Uid);
		OpenWorlds = OpenWorld.FromUid(user.Uid);
		Elfs = Common.Database.Elfs.FromUid(user.Uid);
		Dorms = Common.Database.Dorms.FromUid(user.Uid);
	}

	public void SaveAll()
	{
		User.Save();
		Equipment.Save();
		Common.Database.Avatar.SaveBulk(AvatarList);
		OpenWorld.SaveBulk(OpenWorlds);
		Elfs.Save();
		Dorms.Save();
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
		return new PlayerCardData
		{
			Uid = User.Uid,
			MsgData = new PlayerCardMsgData
			{
				MsgIndex = 0u,
				MsgConfig = 1u
			},
			OnPhonePendantId = 350005u
		};
	}

	public PlayerDetailData GetDetailData()
	{
		return new PlayerDetailData
		{
			Uid = User.Uid,
			Nickname = User.Nick,
			Level = (uint)BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance().CalculateLevel(User.Exp).Level,
			SelfDesc = User.SelfDesc,
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
