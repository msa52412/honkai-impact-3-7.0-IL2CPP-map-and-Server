using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Database;

/// <summary>后崩坏书 2 单个天赋的存档状态。</summary>
public class ScDlcTalentState
{
	public uint TalentId { get; set; }

	public uint Level { get; set; }
}

/// <summary>
/// 一个玩家在后崩坏书 2（DLC2）里的角色状态。
///
/// `GetScDLCRsp.avatar_list[]` 每项 = avatar_id + level + talent_list + equip_talent_list，
/// 而 talent_list 是天赋/技能界面**唯一**的数据来源（客户端不查表建列表），
/// 所以等级、天赋等级、装备的技能槽都必须落库，否则每次刷新都会被「按表重建」冲掉。
/// </summary>
public class ScDlcAvatarScheme : IRow
{
	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	public uint AvatarId { get; set; }

	public uint Level { get; set; }

	/// <summary>装备中的技能槽（EquipScDLCAvatarTalentReq 写入）。</summary>
	public uint[] Equipped { get; set; } = Array.Empty<uint>();

	public List<ScDlcTalentState> Talents { get; set; } = new List<ScDlcTalentState>();

	public void Save()
	{
		ScDlcAvatar.collection.ReplaceOne(this);
	}

	public ScDlcTalentState Talent(uint talentId)
	{
		if (Talents == null)
		{
			return null;
		}
		foreach (ScDlcTalentState state in Talents)
		{
			if (state.TalentId == talentId)
			{
				return state;
			}
		}
		return null;
	}
}

public class ScDlcAvatar
{
	public static readonly SqlTable<ScDlcAvatarScheme> collection =
		Global.Table<ScDlcAvatarScheme>("ScDlcAvatars",
			("owner_uid", "INTEGER", x => x.OwnerUid, false),
			("avatar_id", "INTEGER", x => x.AvatarId, false));

	public static ScDlcAvatarScheme Get(uint uid, uint avatarId)
	{
		return (from x in collection.All()
			where x.OwnerUid == uid && x.AvatarId == avatarId
			select x).FirstOrDefault();
	}

	public static ScDlcAvatarScheme GetOrCreate(uint uid, uint avatarId)
	{
		ScDlcAvatarScheme scheme = Get(uid, avatarId);
		if (scheme != null)
		{
			scheme.Talents ??= new List<ScDlcTalentState>();
			scheme.Equipped ??= Array.Empty<uint>();
			return scheme;
		}

		scheme = new ScDlcAvatarScheme
		{
			OwnerUid = uid,
			AvatarId = avatarId,
			Level = 1u,
			Equipped = Array.Empty<uint>(),
			Talents = new List<ScDlcTalentState>()
		};
		collection.InsertOne(scheme);
		return scheme;
	}
}
