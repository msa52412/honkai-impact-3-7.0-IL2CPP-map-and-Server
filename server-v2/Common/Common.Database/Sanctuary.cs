using System;
using System.Linq;

namespace Common.Database;

/// <summary>
/// SanctuaryScheme 的集合入口，写法照抄 <see cref="Dorms"/>。
/// 表由 SqlTable 自动 CREATE TABLE IF NOT EXISTS，不需要手写迁移。
/// </summary>
public class Sanctuary
{
	public static readonly SqlTable<SanctuaryScheme> collection =
		Global.Table<SanctuaryScheme>("Sanctuaries", ("owner_uid", "INTEGER", (SanctuaryScheme x) => x.OwnerUid, true));

	/// <summary>取存档，没有就建一份。</summary>
	public static SanctuaryScheme FromUid(uint uid)
	{
		return (from s in collection.All()
			where s.OwnerUid == uid
			select s).FirstOrDefault() ?? Create(uid);
	}

	public static SanctuaryScheme Create(uint uid)
	{
		SanctuaryScheme? exist = (from s in collection.All()
			where s.OwnerUid == uid
			select s).FirstOrDefault();
		if (exist != null)
		{
			return exist;
		}

		SanctuaryScheme scheme = new SanctuaryScheme
		{
			OwnerUid = uid,
			ActivityId = 0u,
			Level = 1u,
			Exp = 0u,
			HasTakeRewardLevel = 0u,
			ProductCounterTime = 0u,
			ProductStorageNum = 0u,
			IsInit = false,
			PlayerLevelGroupId = 0u,
			StageIdLists = Array.Empty<uint>(),
			IsNewStage = false,
			TotalExp = 0u,
			BonusExp = 0u,
			LastSyncTime = 0u
		};
		collection.InsertOne(scheme);
		return scheme;
	}
}
