using System;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer;

/// <summary>
/// 后崩坏书（DLC）的「调查等级」「角色解锁」「天赋/技能等级」「词条」。
///
/// 客户端这两块全靠两条协议，服务端原来一条都没实现（日志里全是 not handled）：
///   GetDLCReq        3550 → GetDLCRsp       3551  level/exp/has_take_reward_level
///   GetDLCAvatarReq  3559 → GetDLCAvatarRsp 3560  avatar_list（含 talent_list）
///
/// 客户端侧（反汇编）：
///   0x1827B44B0  OnGetDLCRsp:        this->KOLICAPONBH(当前等级) = rsp.level
///   0x1827B3AE0  OnGetDLCAvatarRsp:  遍历 rsp.avatar_list，按 avatar_id 建
///                                    DLCAvatarDataItem(avatarID, dlcAvatar, level)
///   0x1827B40B0  IsAvatarUnlock(id): 只判断 id 是否在上面的字典里
///   0x1806276A0  GetTalentListByType: 遍历 DLCAvatar.talent_list，按 DLCTalentData.TalentType 过滤
///   0x1806273A0  GetTalentDataItem:   在 talent_list 里按 talent_id 找，找不到传 null
///   0x181EA5E80  OHHANBBFFIA 构造:    level = protoTalent != null ? protoTalent.level : 1
///                                    上限取自 DLCTalentLevelData（GetTalentMaxLevel）
///   0x181EA3B10  （词条套装 id）     = protoTalent.affix_set_id
///   0x180626CC0  GetApplicableAffixSetList: 遍历**类型 4** 的天赋，按 affix_set_id 分组计数，
///                                    数量 → 套装档位（2/3 件 = 1 档，4 件以上 = 2 档）
///
/// 结论：天赋列表、等级、词条**全部只认服务端下发的 proto**，表只提供图标/名字/等级上限。
/// 不下发 = 界面上没有这个天赋；level 给 0 = 显示 0 级。
/// </summary>
public static class DlcService
{
	/// <summary>DLCTalentAffixSet.json 里合法的套装 id 范围（1..5）。</summary>
	public const uint AffixSetMin = 1u;
	public const uint AffixSetMax = 5u;

	/// <summary>DLCLevelData 表里的最高调查等级（30）。</summary>
	public static uint MaxLevel => DLCLevelData.GetInstance().MaxLevel;

	/// <summary>
	/// 下发用的调查等级：run\openworld.json 的 dlc_level，缺省取表内最高（30）。
	/// </summary>
	public static uint TargetLevel => Math.Min(OwMapTuning.DlcLevel, MaxLevel);

	/// <summary>从 level 升到下一级需要的经验；满级返回 0。</summary>
	public static uint ExpNeed(uint level)
	{
		if (level >= MaxLevel)
		{
			return 0u;
		}
		foreach (DLCLevelDataExcel row in DLCLevelData.GetInstance().All)
		{
			if ((uint)row.Level == level)
			{
				return (uint)Math.Max(0, row.Exp);
			}
		}
		return 0u;
	}

	/// <summary>某天赋属于哪个角色（LevelUp 请求里没有 avatar_id，只能反查表）。</summary>
	public static uint AvatarOfTalent(uint talentId)
	{
		DLCTalentDataExcel[] all = DLCTalentData.GetInstance().All;
		if (all == null)
		{
			return 0u;
		}
		foreach (DLCTalentDataExcel row in all)
		{
			if ((uint)row.DLCTalentID == talentId)
			{
				return (uint)row.AvatarID;
			}
		}
		return 0u;
	}

	/// <summary>某天赋的类型（1 属性 / 2 必杀 / 3 分支 / 4 武器特技）。</summary>
	public static int TalentTypeOf(int talentId)
	{
		DLCTalentDataExcel[] all = DLCTalentData.GetInstance().All;
		if (all == null)
		{
			return 0;
		}
		foreach (DLCTalentDataExcel row in all)
		{
			if (row.DLCTalentID == talentId)
			{
				return row.TalentType;
			}
		}
		return 0;
	}

	/// <summary>
	/// 某天赋在「当前调查等级 + 调参」下应该下发的等级。
	/// 返回 0 表示这个天赋还没解锁（LevelUpDLCLv 高于当前调查等级）。
	/// </summary>
	public static uint TalentLevel(int talentId, uint dlcLevel)
	{
		DLCTalentLevelData levels = DLCTalentLevelData.GetInstance();

		int tableMax = levels.MaxLevelOf(talentId);
		int cap = levels.MaxLevelOf(talentId, dlcLevel);

		uint force = OwMapTuning.DlcTalentLevel;
		uint want = force != 0u ? force : (uint)cap;

		// 不能超过表上限，否则客户端 KINNKFHBBHL 会小于 level，界面显示成 "8/3"。
		if (tableMax > 0 && want > (uint)tableMax)
		{
			want = (uint)tableMax;
		}
		if (want > (uint)cap)
		{
			want = (uint)cap;
		}
		return want;
	}

	/// <summary>
	/// 新建天赋时的默认词条套装 id。只有类型 4（武器/特技）带词条，
	/// 客户端 GetApplicableAffixSetList 也只统计类型 4。
	/// 后台调参 dlc_affix_set：0 / 缺省 = 1（四个武器天赋同套装 → 4 件满档）。
	/// </summary>
	public static uint DefaultAffixSet(int talentId)
	{
		if (TalentTypeOf(talentId) != 4)
		{
			return 0u;
		}
		uint configured = OwMapTuning.DlcAffixSet;
		if (configured < AffixSetMin || configured > AffixSetMax)
		{
			configured = AffixSetMin;
		}
		return configured;
	}

	/// <summary>
	/// 取（必要时创建）某角色的天赋存档，并按调参把「该有的等级」补齐。
	/// 只升不降，避免玩家自己升过的等级被调参抹掉。
	/// </summary>
	public static DlcAvatarScheme EnsureAvatar(uint uid, uint avatarId, uint dlcLevel)
	{
		DlcAvatarScheme scheme = DlcAvatar.GetOrCreate(uid, avatarId);

		foreach (DLCTalentDataExcel row in DLCTalentData.GetInstance().ListByAvatar((int)avatarId))
		{
			uint want = TalentLevel(row.DLCTalentID, dlcLevel);
			if (want == 0u)
			{
				// 调查等级不够，这个天赋还没解锁。存档里已有的保持原样（升过级的等级不受影响）。
				continue;
			}

			DlcTalentState state = scheme.Talent((uint)row.DLCTalentID);
			if (state == null)
			{
				state = new DlcTalentState
				{
					TalentId = (uint)row.DLCTalentID,
					Level = want,
					AffixSetId = DefaultAffixSet(row.DLCTalentID),
					WaitAffixSetId = 0u
				};
				scheme.Talents.Add(state);
			}
			else if (state.Level < want)
			{
				state.Level = want;
			}
		}

		// 默认装备：IsNeedSwitch（专精二选一）的那几个，每组给第一个。
		if (scheme.Equipped == null || scheme.Equipped.Length == 0)
		{
			scheme.Equipped = DefaultEquipTalents((int)avatarId);
		}

		scheme.Save();
		return scheme;
	}

	/// <summary>
	/// 构造某个角色的完整天赋列表（DLCTalentData 里该角色的全部 33 条），
	/// 等级/词条取自存档（存档由 <see cref="EnsureAvatar"/> 建立）。
	/// </summary>
	public static List<DLCAvatarTalent> BuildTalentList(uint uid, int avatarId, uint dlcLevel)
	{
		DlcAvatarScheme scheme = EnsureAvatar(uid, (uint)avatarId, dlcLevel);

		List<DLCAvatarTalent> list = new List<DLCAvatarTalent>();
		foreach (DLCTalentDataExcel row in DLCTalentData.GetInstance().ListByAvatar(avatarId))
		{
			DlcTalentState state = scheme.Talent((uint)row.DLCTalentID);
			if (state == null || state.Level == 0u)
			{
				continue;
			}
			list.Add(new DLCAvatarTalent
			{
				TalentId = state.TalentId,
				Level = state.Level,
				AffixSetId = state.AffixSetId,
				WaitSelectAffixSetId = state.WaitAffixSetId,
				IsMask = false
			});
		}
		return list;
	}

	/// <summary>取已装配的天赋（GetDLCAvatarRsp.avatar_list[].equip_talent_list）。</summary>
	public static uint[] EquippedOf(uint uid, int avatarId, uint dlcLevel)
	{
		return EnsureAvatar(uid, (uint)avatarId, dlcLevel).Equipped ?? Array.Empty<uint>();
	}

	/// <summary>装备/切换：整表替换，回 Succ 后客户端会 RefreshActivationView。</summary>
	public static void Equip(uint uid, uint avatarId, uint[] list, uint dlcLevel)
	{
		DlcAvatarScheme scheme = EnsureAvatar(uid, avatarId, dlcLevel);
		scheme.Equipped = list ?? Array.Empty<uint>();
		scheme.Save();
	}

	/// <summary>天赋升级：只升不降，且不超过该天赋在表里的上限。</summary>
	public static uint LevelUp(uint uid, uint talentId, uint targetLevel, uint dlcLevel)
	{
		uint avatarId = AvatarOfTalent(talentId);
		if (avatarId == 0u)
		{
			return 0u;
		}
		DlcAvatarScheme scheme = EnsureAvatar(uid, avatarId, dlcLevel);
		DlcTalentState state = scheme.Talent(talentId);
		if (state == null)
		{
			return 0u;
		}
		int cap = DLCTalentLevelData.GetInstance().MaxLevelOf((int)talentId);
		if (cap > 0 && targetLevel > (uint)cap)
		{
			targetLevel = (uint)cap;
		}
		if (targetLevel > state.Level)
		{
			state.Level = targetLevel;
			scheme.Save();
		}
		return state.Level;
	}

	/// <summary>
	/// 刷新词条：随机一个新的 affix_set_id 放到 wait_select_affix_set_id，
	/// 返回给客户端（RefreshDLCTalentAffixRsp.new_affix_set_id）。
	/// 客户端点「替换」才发 SelectDLCTalentAffix(is_select=true) 把它转正。
	/// </summary>
	public static uint RefreshAffix(uint uid, uint avatarId, uint talentId, uint dlcLevel)
	{
		DlcAvatarScheme scheme = EnsureAvatar(uid, avatarId, dlcLevel);
		DlcTalentState state = scheme.Talent(talentId);
		if (state == null || TalentTypeOf((int)talentId) != 4)
		{
			return 0u;
		}

		uint current = state.AffixSetId;
		uint next = current;
		// 最多试 8 次，避免「随机到和自己一样」白白消耗；
		// 极端情况下（只有 1 个合法套装）就允许相同。
		for (int i = 0; i < 8 && next == current; i++)
		{
			next = (uint)Random.Shared.Next((int)AffixSetMin, (int)AffixSetMax + 1);
		}

		state.WaitAffixSetId = next;
		scheme.Save();
		return next;
	}

	/// <summary>确认/放弃刷新出来的词条。</summary>
	public static void SelectAffix(uint uid, uint avatarId, uint talentId, bool isSelect, uint dlcLevel)
	{
		DlcAvatarScheme scheme = EnsureAvatar(uid, avatarId, dlcLevel);
		DlcTalentState state = scheme.Talent(talentId);
		if (state == null)
		{
			return;
		}
		if (isSelect && state.WaitAffixSetId != 0u)
		{
			state.AffixSetId = state.WaitAffixSetId;
		}
		state.WaitAffixSetId = 0u;
		scheme.Save();
	}

	/// <summary>
	/// 默认装备/切换的天赋：客户端 talent 界面里 IsNeedSwitch 的那几个（专精二选一），
	/// 给每组的第一个即可，避免界面显示「未选择」。
	/// </summary>
	public static uint[] DefaultEquipTalents(int avatarId)
	{
		List<uint> picked = new List<uint>();
		HashSet<int> seenTag = new HashSet<int>();
		foreach (DLCTalentDataExcel row in DLCTalentData.GetInstance().ListByAvatar(avatarId))
		{
			if (!row.IsNeedSwitch)
			{
				continue;
			}
			if (seenTag.Add(row.UniqueTag))
			{
				picked.Add((uint)row.DLCTalentID);
			}
		}
		return picked.ToArray();
	}

	/// <summary>下发日志用：把 talent_list 压成 "id:lv,id:lv"。</summary>
	public static string Describe(List<DLCAvatarTalent> talents)
	{
		List<string> parts = new List<string>();
		foreach (DLCAvatarTalent t in talents)
		{
			parts.Add(t.TalentId + ":" + t.Level + (t.AffixSetId != 0u ? "/affix" + t.AffixSetId : ""));
		}
		return string.Join(",", parts);
	}
}
