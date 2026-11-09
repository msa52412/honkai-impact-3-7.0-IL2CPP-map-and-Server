using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers.Openworld;

/// <summary>
/// 后崩坏书 2（ScDLC / 客户端内部叫 DLC2）主数据，6300 / 6301。
///
/// 为什么必须有这个 handler
/// -----------------------
/// 开放世界列表里有一张图是后崩坏书 2，点「进入」时客户端会先发 GetScDLCReq，
/// 原来服务端没接（日志里 `GetScDLCReq not handled!`），客户端拿不到数据就一直转圈。
///
/// 客户端 DLC2 模块的 OnGetScDLCRsp（0x180BCF4C0）**不检查 retcode**，而是：
///     setAvatarList(proto[+0x20])   setFever(proto[+0x18])
///     setFeverAbility(proto[+0x50], proto[+0x58])
///     setDailyQuest(proto[+0x30])   setNpcList(proto[+0x28])
///     name = proto[+0x38]           setAchievement(proto[+0x40])
/// 也就是把每个子对象直接塞进 setter。**子对象为 null 会在 setter 里抛异常，
/// 整包处理失败 → 一直转圈**。所以下面每个字段都必须给非 null 值。
///
/// 角色等级 / 天赋 / 技能为什么会被「夹回去」
/// ------------------------------------------
/// `ScDLCAvatarLevel.FeverLevelLimit` 和 `ScDLCTalentLevel.FeverLevelLimit` 是**狂热等级门槛**：
///   角色 30 级要狂热 10、90 级要狂热 30；天赋最高门槛 25。
/// 只下发 level 而不抬 `fever.level`，客户端会把等级夹回门槛允许的最大值
/// （狂热 1 时角色只能显示 1 级、天赋全锁）—— 表现就是「等级、天赋、技能全没点」。
/// 所以 fever.level 默认取表里的最大门槛（30）。
///
/// 角色表（ScDLCAvatar.json，6 行）：
///   9011 Adam / 9021 Mei / 9031 Carol / 9041 Bronya / 9051 Timido / 9061 Lyle(IsFake，纯支援)
/// 天赋表（ScDLCTalentData.json，87 行）：每个真实角色 16 个节点，
///   TalentType 1 Passive / 2 Main / 3 Branch / 4 Combine / 5 Support。
/// `equip_talent_list` = 装备中的技能槽，每个类型（Main/Branch/Combine）各挑一个。
///
/// 存档在 `ScDlcAvatars`（ScDlcAvatarScheme）：等级 / 天赋等级 / 已装备技能槽。
/// 首次见到某个角色时按「满级 + 满天赋 + 默认技能槽」初始化并落库。
/// </summary>
[PacketCmdId(CmdId.GetScDLCReq)]
internal class GetScDLCReqHandler : IPacketHandler
{
	private static readonly int[] SkillTypes = { 2, 3, 4 };

	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		uint level = ResolveAvatarLevel();
		uint feverLevel = ResolveFeverLevel();

		GetScDLCRsp rsp = new GetScDLCRsp
		{
			retcode = GetScDLCRsp.Retcode.Succ,

			// 狂热系统：非 null 是硬要求，否则 setFever 里会炸
			Fever = new ScDLCFever
			{
				Level = feverLevel,
				Exp = 0u,
				FeverSlotNum = 0u,
				HasTakeRewardLevel = feverLevel,
				ActiveSuiteId = 0u
			},

			// 每日委托：同样必须非 null
			DailyQuestData = new ScDLCDailyQuestData(),

			Name = session.Player.User.Nick ?? string.Empty,
			MaxChallengeRewardTakeProgress = 0u,
			MaxActiveFeverAbilityGroupNum = 0u,
			ActiveFeverAbilityGroupIdLists = Array.Empty<uint>()
		};

		ScDLCTalentData talentData = ScDLCTalentData.GetInstance();
		ScDLCTalentLevelData talentLevelData = ScDLCTalentLevelData.GetInstance();

		int avatarCount = 0;
		int talentCount = 0;

		foreach (ScDLCAvatarDataExcel cfg in BaseExcelReader<ScDLCAvatarData, ScDLCAvatarDataExcel>.GetInstance().All)
		{
			uint avatarId = (uint)cfg.AvatarId;
			ScDlcAvatarScheme scheme = ScDlcAvatar.GetOrCreate(uid, avatarId);
			List<ScDLCTalentDataExcel> talents = talentData.OfAvatar(cfg.AvatarId);
			bool dirty = false;

			if (scheme.Level < level)
			{
				scheme.Level = level;
				dirty = true;
			}

			ScDLCAvatar avatar = new ScDLCAvatar
			{
				AvatarId = avatarId,
				Level = scheme.Level
			};

			foreach (ScDLCTalentDataExcel t in talents)
			{
				if (t.ForceLock)
				{
					continue;
				}
				int maxLv = talentLevelData.MaxLevel(t.TalentId);
				if (maxLv <= 0)
				{
					continue;
				}

				ScDlcTalentState state = scheme.Talent((uint)t.TalentId);
				if (state == null)
				{
					// 首次见到：直接给满级（用户要的是全满，不是从 1 级爬）
					state = new ScDlcTalentState
					{
						TalentId = (uint)t.TalentId,
						Level = (uint)maxLv
					};
					scheme.Talents.Add(state);
					dirty = true;
				}

				avatar.TalentLists.Add(new ScDLCTalent
				{
					TalentId = state.TalentId,
					TalentLevel = state.Level
				});
				talentCount++;
			}

			// 技能槽：Main / Branch / Combine 各一个。存档里没有就填默认（各类型第一个节点）。
			if (scheme.Equipped == null || scheme.Equipped.Length == 0)
			{
				List<uint> equip = new List<uint>();
				foreach (int type in SkillTypes)
				{
					ScDLCTalentDataExcel pick = talents
						.Where(x => x.TalentType == type && !x.ForceLock)
						.OrderBy(x => x.TalentId)
						.FirstOrDefault();
					if (pick != null)
					{
						equip.Add((uint)pick.TalentId);
					}
				}
				scheme.Equipped = equip.ToArray();
				dirty = true;
			}
			avatar.EquipTalentLists = scheme.Equipped;

			if (dirty)
			{
				scheme.Save();
			}

			rsp.AvatarLists.Add(avatar);
			avatarCount++;
		}

		Packet.c.Log($"[ScDLC] GetScDLCReq -> Succ avatarLv={level} fever={feverLevel} "
			+ $"avatars={avatarCount} talents={talentCount} name='{rsp.Name}'");
		session.Send(Packet.FromProto(rsp, CmdId.GetScDLCRsp));
	}

	/// <summary>角色等级：tuning 优先，否则取 ScDLCAvatarLevel 的最大等级。</summary>
	internal static uint ResolveAvatarLevel()
	{
		uint configured = OwMapTuning.ScDlcLevel;
		if (configured != 0u)
		{
			return configured;
		}
		int max = ScDLCAvatarLevelData.GetInstance().MaxLevel();
		return max > 0 ? (uint)max : 90u;
	}

	/// <summary>
	/// 狂热等级：tuning 优先，否则取「角色等级门槛」和「天赋门槛」的最大值。
	/// 低于这个值，客户端会把角色等级夹回去、天赋全锁。
	/// </summary>
	internal static uint ResolveFeverLevel()
	{
		uint configured = OwMapTuning.ScDlcFeverLevel;
		if (configured != 0u)
		{
			return configured;
		}
		int limit = ScDLCAvatarLevelData.GetInstance().FeverLimit((int)ResolveAvatarLevel());
		int talentLimit = ScDLCTalentLevelData.GetInstance().MaxFeverLimit();
		if (talentLimit > limit)
		{
			limit = talentLimit;
		}
		return limit > 0 ? (uint)limit : 30u;
	}
}
