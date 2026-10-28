using System;
using System.Collections.Generic;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace Common.Database;

public class UserScheme : IRow
{
	public class OpenWorldStoryScheme : OpenworldStory
	{
		public bool IsDone;

		public OpenworldStory ToProto()
		{
			return new OpenworldStory
			{
				StoryId = base.StoryId,
				StoryProgress = base.StoryProgress,
				AcceptTime = base.AcceptTime
			};
		}
	}

	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public string Name { get; set; }

	public uint Uid { get; set; }

	public string Nick { get; set; }

	public int Exp { get; set; }

	public int Hcoin { get; set; }

	public int Stamina { get; set; }

	public string SelfDesc { get; set; }

	public bool IsFirstLogin { get; set; }

	public string Token { get; set; }

	public int CustomHeadId { get; set; }

	public int FrameId { get; set; }

	public int WarshipId { get; set; }

	public WarshipAvatarData WarshipAvatar { get; set; }

	public int AssistantAvatarId { get; set; }

	public int BirthDate { get; set; }

	public uint AbyssDynamicHard { get; set; }

	public uint AbyssGroupLevel { get; set; }

	public List<AvatarTeam> AvatarTeamList { get; set; }

	public List<CustomAvatarTeam> CustomAvatarTeamList { get; set; }

	public List<OpenWorldStoryScheme> OpenWorldStory { get; set; } = new List<OpenWorldStoryScheme>();


	public void Save()
	{
		User.collection.ReplaceOne(this);
	}

	public uint GetCreationTime()
	{
		return (uint)CreatedAt;
	}

	public void AddExp(int exp)
	{
		Exp += exp;
		if (Exp > BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance().GetMaxPossibleExp())
		{
			Exp = BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance().GetMaxPossibleExp();
		}
	}

	public void AddOWStory(uint storyId)
	{
		OpenWorldStory.Add(new OpenWorldStoryScheme
		{
			StoryId = storyId,
			StoryProgress = 0u,
			AcceptTime = (uint)Global.GetUnixInSeconds(),
			IsDone = false
		});
	}
}
