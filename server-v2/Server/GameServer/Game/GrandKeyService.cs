using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 神之键（GrandKey）业务逻辑。
///
/// 客户端实证（UserAssembly.dll 反汇编）
/// ------------------------------------
/// 4306/507 GetGrandKeyRsp 的处理在 FPLBEFMIALC.BEOLLNHHMAD (0x1816D2160)：
///     if (rsp.retcode != 0) return;
///     foreach (var k in rsp.key_list) {
///         if (!this[+0x58].ContainsKey(k.id)) continue;   // 不在字典里就跳过
///         this[+0x58][k.id].KDILACPGKEN(k);               // 0x1810585A0 更新
///     }
/// 而 KDILACPGKEN 的语义是：
///     k == null  -> +0x18 = false（未解锁）
///     k != null  -> +0x18 = true, +0x20 = breach_level, +0x24 = max(level,1),
///                   +0x28 = unlock_level, +0x1C = end_time,
///                   然后 Notify(656 GrandKeyUpdate, k.id) 让界面刷新
/// 所以：**回包里出现即已解锁**，未解锁的直接不放进 key_list 就行。
///
/// GrandKeyLevelUpRsp 的处理是空函数（只判空），升级后界面靠
/// 重新收一份 GetGrandKeyRsp 触发 Notify(656)，所以升级完要再推一遍完整列表。
/// </summary>
internal static class GrandKeyService
{
	private static List<GrandKeyDataExcel> Configs()
	{
		return BaseExcelReader<GrandKeyData, GrandKeyDataExcel>.GetInstance().All.ToList();
	}

	internal static GrandKeys.GrandKeysScheme Scheme(Player player)
	{
		GrandKeys.GrandKeysScheme scheme = GrandKeys.FromUid(player.User.Uid);
		if (scheme.GrandKeyList == null)
		{
			scheme.GrandKeyList = new List<GrandKey>();
		}
		return scheme;
	}

	private static HashSet<uint> OwnedWeaponIds(Player player)
	{
		HashSet<uint> set = new HashSet<uint>();
		Weapon[] weaponList = player.Equipment?.WeaponList;
		if (weaponList == null)
		{
			return set;
		}
		foreach (Weapon weapon in weaponList)
		{
			if (weapon != null && weapon.Id != 0u)
			{
				set.Add(weapon.Id);
			}
		}
		return set;
	}

	private static bool WeaponUnlocks(GrandKeyDataExcel cfg, HashSet<uint> owned)
	{
		if (cfg.MainWeaponIdList != null)
		{
			foreach (long id in cfg.MainWeaponIdList)
			{
				if (id > 0 && owned.Contains((uint)id))
				{
					return true;
				}
			}
		}
		if (cfg.SubWeaponmainIDlist != null)
		{
			foreach (long id in cfg.SubWeaponmainIDlist)
			{
				if (id > 0 && owned.Contains((uint)id))
				{
					return true;
				}
			}
		}
		return false;
	}

	/// <summary>按配置补齐应该解锁的神之键，并按 force_* 开关拉等级。返回存档是否有改动。</summary>
	internal static bool Sync(Player player, GrandKeys.GrandKeysScheme scheme)
	{
		HashSet<uint> owned = OwnedWeaponIds(player);
		bool dirty = false;
		foreach (GrandKeyDataExcel cfg in Configs())
		{
			int keyId = (int)cfg.GrandKeyId;
			uint max = GrandKeyTuning.MaxLevelOverride != 0u
				? GrandKeyTuning.MaxLevelOverride
				: GrandKeys.GrandKeysScheme.MaxLevel(keyId);
			if (max == 0u)
			{
				max = 1u;
			}

			// 目标等级：force_max_level 优先（拉到该把的上限），其次 force_level（拉到指定等级）。
			// 两个都关就是 0 = 不动已有等级。
			uint target = 0u;
			if (GrandKeyTuning.ForceMaxLevel)
			{
				target = max;
			}
			else if (GrandKeyTuning.ForceLevel > 0u)
			{
				target = System.Math.Min(GrandKeyTuning.ForceLevel, max);
			}

			GrandKey? exist = scheme.Find((uint)keyId);
			if (exist == null)
			{
				bool unlocked = GrandKeyTuning.UnlockAll
					|| (GrandKeyTuning.UnlockByWeapon && WeaponUnlocks(cfg, owned));
				if (!unlocked)
				{
					continue;
				}
				uint start = target > 0u ? target : System.Math.Min(GrandKeyTuning.DefaultLevel, max);
				if (start == 0u)
				{
					start = 1u;
				}
				scheme.AddGrandKey(keyId, start);
				dirty = true;
				continue;
			}

			if (exist.Level == 0u)
			{
				exist.Level = 1u;
				dirty = true;
			}
			if (exist.Level > max)
			{
				exist.Level = max;
				dirty = true;
			}
			if (target > 0u && exist.Level != target)
			{
				exist.Level = target;
				exist.UnlockLevel = GrandKeys.GrandKeysScheme.UnlockWeaponLevel(keyId, (int)target);
				dirty = true;
			}
			// ---- 技能 ----
			// SkillId 必须是 GrandKeyBuff.json 里的真实 grandKeySkill（20101 / 20602 …）。
			// 老代码发的是 1，客户端按 ID 查不到任何技能 → 技能列表全空、「技能同步」永远不满。
			if (exist.Skill == null)
			{
				exist.Skill = new GrandKeySkill { KeyId = (uint)keyId };
				dirty = true;
			}
			if (exist.Skill.KeyId != (uint)keyId)
			{
				exist.Skill.KeyId = (uint)keyId;
				dirty = true;
			}

			uint curSkill = exist.Skill.SkillId;
			bool skillValid = GrandKeys.GrandKeysScheme.IsValidSkillId(keyId, curSkill);
			// 同步技能随超限档位解锁：206 的 20602 要 breach>=1，202 的 20212 要 breach>=2
			uint syncSkill = GrandKeys.GrandKeysScheme.BestSynchronSkillId(keyId, exist.BreachLevel);

			if (!skillValid)
			{
				uint pick = GrandKeyTuning.AutoSyncSkill && syncSkill != 0u
					? syncSkill
					: GrandKeys.GrandKeysScheme.DefaultSkillId(keyId);
				if (pick != 0u)
				{
					exist.Skill.SkillId = pick;
					// 这里只补「默认技能是哪个」，不碰到期时间。
					// 「开启」是手动动作，由 GrandKeyActivateSkillReq 写入 EndTime，
					// 自动补时间会让玩家点了开启也看不出差别。
					exist.Skill.LastTime = 0u;
					dirty = true;
				}
			}
			// 注意：这里**不能**再无条件把技能改成「同步技能」。
			// 一把神之键的 Skill 只有一个槽位，玩家在「开启」弹窗里选的就是它。
			// 老代码只要 AutoSyncSkill 开着就强制覆盖成 BestSynchronSkillId，
			// 于是玩家点开启 → 存进去 → BuildRsp 里的 Sync 立刻改回去 → 界面毫无变化，
			// 这就是「开启神之键不生效」的真正原因。同步技能只在技能为空/非法时兜底（上面那支）。

			// 「开启」状态看的是 GrandKey.EndTime（客户端 KDILACPGKEN 只搬这个字段，不读 skill）。
			// **不要**在这里自动补时间 —— 开启是手动动作，补了就看不出玩家点了什么。
		}
		if (dirty)
		{
			scheme.Save();
		}
		return dirty;
	}

	internal static GetGrandKeyRsp BuildRsp(Player player)
	{
		GrandKeys.GrandKeysScheme scheme = Scheme(player);
		Sync(player, scheme);
		GetGrandKeyRsp rsp = new GetGrandKeyRsp
		{
			retcode = GetGrandKeyRsp.Retcode.Succ,
			IsAll = true
		};
		foreach (GrandKey key in scheme.GrandKeyList)
		{
			rsp.KeyLists.Add(key);
		}
		return rsp;
	}

	/// <summary>把整份神之键列表推给客户端（升级/超限/改技能后用，触发客户端 Notify(656)）。</summary>
	internal static void PushMainInfo(Session session)
	{
		GetGrandKeyRsp rsp = BuildRsp(session.Player);
		Packet.c.Log("[GrandKey] 推送 " + rsp.KeyLists.Count + " 把：" + Describe(rsp));
		session.Send(Packet.FromProto(rsp, CmdId.GetGrandKeyRsp));
	}

	internal static string Describe(GetGrandKeyRsp rsp)
	{
		return string.Join(",", rsp.KeyLists.Select((GrandKey k)
			=> $"{k.Id}:Lv{k.Level}/Br{k.BreachLevel}"));
	}

	/// <summary>带技能信息的存档描述（排查「开启不生效」用）。</summary>
	internal static string DescribeScheme(GrandKeys.GrandKeysScheme scheme)
	{
		return string.Join(",", (scheme.GrandKeyList ?? new System.Collections.Generic.List<GrandKey>())
			.Select((GrandKey k) => $"{k.Id}:Lv{k.Level}/Br{k.BreachLevel}/End{k.EndTime}/Skill{k.Skill?.SkillId ?? 0}/Last{k.Skill?.LastTime ?? 0}"));
	}
}
