using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;

namespace Common.Database;

public class DLCs
{
	public static readonly SqlTable<DLCsScheme> collection = Global.Table<DLCsScheme>("DLCs", ("owner_uid", "INTEGER", x => x.OwnerUid, true));

	public static DLCsScheme FromUid(uint uid)
	{
		return (from collection in collection.All()
			where collection.OwnerUid == uid
			select collection).FirstOrDefault() ?? Create(uid);
	}

	public static DLCsScheme Create(uint uid)
	{
		DLCsScheme dLCsScheme = (from collection in collection.All()
			where collection.OwnerUid == uid
			select collection).FirstOrDefault();
		if (dLCsScheme != null)
		{
			return dLCsScheme;
		}
		DLCsScheme dLCsScheme2 = new DLCsScheme();
		dLCsScheme2.OwnerUid = uid;
		dLCsScheme2.Dlc = new DLCsScheme.DLC();
		dLCsScheme2.ScDlc = new DLCsScheme.ScDLC
		{
			Fever = new DLCsScheme.ScDLCFever
			{
				Level = 1u,
				Exp = 0u,
				FeverSlotNum = 0u,
				HasTakeRewardLevel = 1u,
				SkillSuiteLists = new List<FeverSkillSuite>(),
				ActiveSuiteId = 0u
			},
			NpcLists = new List<ScDLCSupportNPC>(),
			DailyQuestData = new DLCsScheme.ScDLCDailyQuestData
			{
				QuestLists = new List<OpenworldQuest>(),
				LeftAcceptTimes = 0u,
				RefreshLeftTimes = 0u,
				RefreshCostMaterialId = 0u,
				RefreshCostMaterialNum = 0u
			},
			Name = "",
			AchievementLists = new List<ScDLCAchievement>(),
			MaxChallengeRewardTakeProgress = 0u,
			ActiveFeverAbilityGroupIdLists = new uint[0],
			MaxActiveFeverAbilityGroupNum = 0u
		};
		DLCsScheme dLCsScheme3 = dLCsScheme2;
		DLCsScheme.ScDLCAvatar item = new DLCsScheme.ScDLCAvatar
		{
			TalentLists = new List<ScDLCTalent>()
		};
		dLCsScheme3.ScDlc.AvatarLists = new List<DLCsScheme.ScDLCAvatar>();
		dLCsScheme3.ScDlc.AvatarLists.Add(item);
		collection.InsertOne(dLCsScheme3);
		return dLCsScheme3;
	}
}
