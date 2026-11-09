using System.Collections.Generic;
using System.Linq;
using Common.Utils.ExcelReader;

namespace Common.Database;

public class OpenWorld
{
	public static readonly SqlTable<OpenWorldScheme> collection = Global.Table<OpenWorldScheme>("OpenWorlds", ("owner_uid", "INTEGER", x => x.OwnerUid, false), ("map_id", "INTEGER", x => x.MapId, false));

	public static readonly uint[] ShowMapList = new uint[8] { 1u, 2u, 301u, 401u, 601u, 701u, 801u, 1001u };

	public static void InitData(uint uid)
	{
		collection.InsertMany(from x in BaseExcelReader<OpenWorldMap, OpenWorldMapExcel>.GetInstance().All
			select (uint)x.MapId into mapId
			select new OpenWorldScheme
			{
				MapId = mapId,
				Cycle = BaseExcelReader<OpenWorldCycleData, OpenWorldCycleDataExcel>.GetInstance().GetInitCycle(mapId),
				OwnerUid = uid,
				QuestLevel = 1u,
				HasTakeFinishRewardCycle = 0u,
				SpawnPoint = ""
			});
	}

	public static List<OpenWorldScheme> FromUid(uint uid)
	{
		List<OpenWorldScheme> list = (from x in collection.All()
			where x.OwnerUid == uid && ShowMapList.Contains(x.MapId)
			select x).ToList();
		if (list.Count > 0)
		{
			return list;
		}
		InitData(uid);
		return (from x in collection.All()
			where x.OwnerUid == uid && ShowMapList.Contains(x.MapId)
			select x).ToList();
	}

	public static void SaveBulk(IEnumerable<OpenWorldScheme> openWorlds)
	{
		collection.BulkWrite(openWorlds);
	}
}
