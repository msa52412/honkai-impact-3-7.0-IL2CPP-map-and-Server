using System.Collections.Generic;
using Common.Resources.Proto;

namespace Common.Database;

public class DLCsScheme : IRow
{
	public class DLC
	{
		public string Name { get; set; }
	}

	public class ScDLCFever : Common.Resources.Proto.ScDLCFever
	{
		public new List<FeverSkillSuite> SkillSuiteLists { get; set; } = new List<FeverSkillSuite>();

	}

	public class ScDLCAvatar : Common.Resources.Proto.ScDLCAvatar
	{
		public new List<ScDLCTalent> TalentLists { get; set; } = new List<ScDLCTalent>();

	}

	public class ScDLCDailyQuestData : Common.Resources.Proto.ScDLCDailyQuestData
	{
		public new List<OpenworldQuest> QuestLists { get; set; } = new List<OpenworldQuest>();

	}

	public class ScDLC
	{
		public ScDLCFever Fever { get; set; }

		public List<ScDLCSupportNPC> NpcLists { get; set; }

		public List<ScDLCAvatar> AvatarLists { get; set; }

		public ScDLCDailyQuestData DailyQuestData { get; set; }

		public string Name { get; set; }

		public List<ScDLCAchievement> AchievementLists { get; set; }

		public uint MaxChallengeRewardTakeProgress { get; set; }

		public uint[] ActiveFeverAbilityGroupIdLists { get; set; }

		public uint MaxActiveFeverAbilityGroupNum { get; set; }
	}

	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	public DLC Dlc { get; set; }

	public ScDLC ScDlc { get; set; }

	public void Save()
	{
		DLCs.collection.ReplaceOne(this);
	}

	public void setDlcName(string Name)
	{
		Dlc.Name = Name;
	}

	public void setScDlcName(string Name)
	{
		ScDlc.Name = Name;
	}
}
