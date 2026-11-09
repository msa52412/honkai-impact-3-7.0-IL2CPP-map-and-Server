using System.Linq;

namespace Common.Database;

/// <summary>
/// 任务 / 成就领奖记录（GetMissionRewardReq 114）。
/// </summary>
public class MissionTake
{
	public static readonly SqlTable<MissionTakeScheme> collection =
		Global.Table<MissionTakeScheme>("MissionTakes",
			("owner_uid", "INTEGER", x => x.OwnerUid, false),
			("mission_id", "INTEGER", x => x.MissionId, false));

	public static uint[] TakenIds(uint uid)
	{
		return collection.Where(x => x.OwnerUid == uid)
			.Select(x => x.MissionId)
			.Distinct()
			.OrderBy(x => x)
			.ToArray();
	}

	public static bool Has(uint uid, uint missionId)
	{
		return collection.Where(x => x.OwnerUid == uid && x.MissionId == missionId).ToList().Count > 0;
	}

	public static void Mark(uint uid, uint missionId)
	{
		if (Has(uid, missionId))
		{
			return;
		}

		collection.InsertOne(new MissionTakeScheme
		{
			OwnerUid = uid,
			MissionId = missionId
		});
	}
}
