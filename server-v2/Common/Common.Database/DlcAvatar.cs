using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Database;

/// <summary>单个天赋的存档状态（对应 DLCAvatarTalent 里除 talent_id 以外的字段）。</summary>
public class DlcTalentState
{
	public uint TalentId { get; set; }

	public uint Level { get; set; }

	/// <summary>已生效的词条套装 id（DLCTalentAffixSet.AffixSetID，1..5）。0 = 没有词条。</summary>
	public uint AffixSetId { get; set; }

	/// <summary>刷新出来、还没确认的词条套装 id。客户端「刷新后选择」用它。</summary>
	public uint WaitAffixSetId { get; set; }
}

/// <summary>
/// 一个玩家在后崩坏书里的角色状态。
///
/// GetDLCAvatarRsp.avatar_list[] 每一项 = avatar_id + equip_talent_list + talent_list，
/// 而 talent_list 是天赋/技能界面**唯一**的数据来源（客户端不查表建列表），
/// 所以等级、词条这些必须落库，否则一刷新就被「按表重建」冲掉。
/// </summary>
public class DlcAvatarScheme : IRow
{
	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	public uint AvatarId { get; set; }

	public List<DlcTalentState> Talents { get; set; } = new List<DlcTalentState>();

	/// <summary>已装配的天赋（EquipDLCAvatarTalent / SwitchDLCAvatarTalent 写入）。</summary>
	public uint[] Equipped { get; set; } = Array.Empty<uint>();

	public void Save()
	{
		DlcAvatar.collection.ReplaceOne(this);
	}

	public DlcTalentState Talent(uint talentId)
	{
		if (Talents == null)
		{
			return null;
		}
		foreach (DlcTalentState state in Talents)
		{
			if (state.TalentId == talentId)
			{
				return state;
			}
		}
		return null;
	}
}

public class DlcAvatar
{
	public static readonly SqlTable<DlcAvatarScheme> collection = Global.Table<DlcAvatarScheme>("DlcAvatars", ("owner_uid", "INTEGER", x => x.OwnerUid, false), ("avatar_id", "INTEGER", x => x.AvatarId, false));

	public static DlcAvatarScheme Get(uint uid, uint avatarId)
	{
		return (from x in collection.All()
			where x.OwnerUid == uid && x.AvatarId == avatarId
			select x).FirstOrDefault();
	}

	public static DlcAvatarScheme GetOrCreate(uint uid, uint avatarId)
	{
		DlcAvatarScheme scheme = Get(uid, avatarId);
		if (scheme != null)
		{
			if (scheme.Talents == null)
			{
				scheme.Talents = new List<DlcTalentState>();
			}
			if (scheme.Equipped == null)
			{
				scheme.Equipped = Array.Empty<uint>();
			}
			return scheme;
		}

		scheme = new DlcAvatarScheme
		{
			OwnerUid = uid,
			AvatarId = avatarId,
			Talents = new List<DlcTalentState>(),
			Equipped = Array.Empty<uint>()
		};
		collection.InsertOne(scheme);
		return scheme;
	}
}
