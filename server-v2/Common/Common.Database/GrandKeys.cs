using System;
using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace Common.Database;

/// <summary>
/// 神之键（GrandKey）存档。
///
/// 表 GrandKeys(id, payload, owner_uid)，payload 里存 <see cref="GrandKeysScheme.GrandKeyList"/>。
/// **列表里存在的 id 才算「已解锁」** —— 客户端 FPLBEFMIALC 的 4306 处理是
///     Dictionary.ContainsKey(id) ? 更新 : 跳过
/// 所以未解锁的神之键只要不出现在回包里就行，不需要发 level=0 的占位项。
/// </summary>
public class GrandKeys
{
	public class GrandKeysScheme : IRow
	{
		public long Id { get; set; }

		public long CreatedAt { get; set; }

		public uint OwnerUid { get; set; }

		public List<GrandKey> GrandKeyList { get; set; }

		public void Save()
		{
			collection.ReplaceOne(this);
		}

		/// <summary>
		/// 按 GrandKey.json 新建一把已解锁的神之键。
		/// 注意 GrandKey.Skill 默认是 null，**必须显式 new**，
		/// 老代码用 `Skill = { SkillId = 1 }` 这种嵌套初始化器会直接 NRE。
		/// </summary>
		public GrandKey AddGrandKey(int grandKeyId, uint level = 1u)
		{
			GrandKeyDataExcel grandKeyDataExcel = BaseExcelReader<GrandKeyData, GrandKeyDataExcel>.GetInstance().FromId(grandKeyId);
			if (grandKeyDataExcel == null)
			{
				throw new ArgumentException("Invalid grandKeyId");
			}
			if (GrandKeyList == null)
			{
				GrandKeyList = new List<GrandKey>();
			}
			GrandKey grandKey = new GrandKey
			{
				Id = (uint)grandKeyDataExcel.GrandKeyId,
				Level = level == 0u ? 1u : level,
				EndTime = 0u,
				UnlockLevel = UnlockWeaponLevel(grandKeyId, (int)(level == 0u ? 1u : level)),
				ActivateLevel = 0u,
				BreachLevel = 0u,
				ScoinNum = 0u,
				Skill = new GrandKeySkill
				{
					KeyId = (uint)grandKeyDataExcel.GrandKeyId,
					// 必须是 GrandKeyBuff.json 里的真实 grandKeySkill（20101/20601 …），
					// 发 1 这种假 ID 客户端查不到技能，技能列表会全空。
					SkillId = DefaultSkillId(grandKeyId),
					LastTime = 0u
				}
			};
			GrandKeyList.Add(grandKey);
			return grandKey;
		}

		public GrandKey? Find(uint id)
		{
			return GrandKeyList?.FirstOrDefault((GrandKey k) => k.Id == id);
		}

		/// <summary>升级，返回升级后的等级；已经满级返回 0。</summary>
		public uint LevelUpGrandKey(uint id)
		{
			GrandKey? grandKey = Find(id);
			if (grandKey == null)
			{
				return 0u;
			}
			uint max = MaxLevel((int)id);
			if (grandKey.Level >= max)
			{
				return 0u;
			}
			grandKey.Level += 1u;
			grandKey.UnlockLevel = UnlockWeaponLevel((int)id, (int)grandKey.Level);
			return grandKey.Level;
		}

		public uint BreachGrandKey(uint id)
		{
			GrandKey? grandKey = Find(id);
			if (grandKey == null)
			{
				return 0u;
			}
			uint max = MaxBreachLevel((int)id);
			if (grandKey.BreachLevel >= max)
			{
				return 0u;
			}
			grandKey.BreachLevel += 1u;
			return grandKey.BreachLevel;
		}

                public void ActivateGrandKeySkill(uint keyId, uint skillId, uint lastTime)
                {
                        GrandKey? grandKey = Find(keyId);
                        if (grandKey != null)
                        {
                                grandKey.Skill ??= new GrandKeySkill { KeyId = keyId };
                                grandKey.Skill.SkillId = skillId;

                                // 客户端「开启」时选的是时长（3 小时 / 1 天 / 7 天…），
                                // 发过来的 last_time 是**秒数**（实测 10800 = 3 小时），不是绝对时间戳。
                                // 换算成绝对到期时间；0 表示不限时，原样存 0（不要自己编一个十年）。
                                uint now = (uint)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                                uint end = lastTime;
                                if (end != 0u && end < now)
                                {
                                        end = now + end;
                                }
                                grandKey.Skill.LastTime = end;

                                // 关键：客户端 KDILACPGKEN 只更新
                                //   unlock / end_time / breach_level / level / unlock_level
                                // **不读 skill**，所以「开启」是否生效看的是 GrandKey.EndTime。
                                // 手动开启就是把它设成这次选的到期时间。
                                grandKey.EndTime = end;
                        }
                }

                public void SetGrandKeySkill(uint keyId, uint skillId)
                {
                        GrandKey? grandKey = Find(keyId);
                        if (grandKey != null)
                        {
                                grandKey.Skill ??= new GrandKeySkill { KeyId = keyId };
                                grandKey.Skill.SkillId = skillId;
                                // 技能是挂在钥匙上的：钥匙开着多久，技能就到什么时候。
                                // 只改 SkillId 不改 LastTime 的话，换技能后界面会显示成「未生效」。
                                grandKey.Skill.LastTime = grandKey.EndTime;
                        }
                }

		/// <summary>等级上限：优先 GrandKeyLevel.json，其次 GrandKey.json 的 maxGrandKeyLv。</summary>
		public static uint MaxLevel(int grandKeyId)
		{
			int fromLevelTable = BaseExcelReader<GrandKeyLevelData, GrandKeyLevelDataExcel>.GetInstance().MaxLevel(grandKeyId);
			if (fromLevelTable > 1)
			{
				return (uint)fromLevelTable;
			}
			GrandKeyDataExcel grandKeyDataExcel = BaseExcelReader<GrandKeyData, GrandKeyDataExcel>.GetInstance().FromId(grandKeyId);
			if (grandKeyDataExcel == null || grandKeyDataExcel.MaxGrandKeyLv <= 0)
			{
				return 1u;
			}
			return (uint)grandKeyDataExcel.MaxGrandKeyLv;
		}

		/// <summary>超限上限 = breachIconList 长度 - 1（0 表示不能超限）。</summary>
		public static uint MaxBreachLevel(int grandKeyId)
		{
			GrandKeyDataExcel grandKeyDataExcel = BaseExcelReader<GrandKeyData, GrandKeyDataExcel>.GetInstance().FromId(grandKeyId);
			int n = grandKeyDataExcel?.BreachIconList?.Length ?? 0;
			return n <= 1 ? 0u : (uint)(n - 1);
		}

		public static uint UnlockWeaponLevel(int grandKeyId, int level)
		{
			GrandKeyLevelDataExcel grandKeyLevelDataExcel = BaseExcelReader<GrandKeyLevelData, GrandKeyLevelDataExcel>.GetInstance().FromId(grandKeyId, level);
			return (uint)(grandKeyLevelDataExcel?.UnlockWeaponLevel ?? 0);
		}

		// ------------------------------------------------------------------
		// 技能 ID
		//
		// GrandKeyBuff.json 里每把神之键的技能有**真实 ID**（20101..20110、20601、20602 …），
		// 分两类：
		//   IsSynchron = 0 普通技能，unlockgrandKeyLevel = 1..10（随神之键等级解锁）
		//   IsSynchron = 1 同步技能，breachLevel = 1..2（随超限档位解锁）
		// 之前一律发 SkillId = 1，客户端按 ID 查不到任何技能 → 技能同步永远是空的。
		// ------------------------------------------------------------------

		private static List<GrandKeyBuffDataExcel> Buffs(int grandKeyId)
		{
			return BaseExcelReader<GrandKeyBuffData, GrandKeyBuffDataExcel>.GetInstance()
				.All.Where((GrandKeyBuffDataExcel x) => x.GrandKeyId == grandKeyId).ToList();
		}

		/// <summary>该技能 ID 是不是这把神之键的真实技能。</summary>
		public static bool IsValidSkillId(int grandKeyId, uint skillId)
		{
			if (skillId == 0u)
			{
				return false;
			}
			return Buffs(grandKeyId).Any((GrandKeyBuffDataExcel x) => x.GrandKeySkill == skillId);
		}

		/// <summary>默认技能：等级 1 就能用的普通技能里 ID 最小的那个。</summary>
		public static uint DefaultSkillId(int grandKeyId)
		{
			List<GrandKeyBuffDataExcel> list = Buffs(grandKeyId);
			GrandKeyBuffDataExcel? pick = list
				.Where((GrandKeyBuffDataExcel x) => x.IsSynchron == 0 && x.UnlockgrandKeyLevel <= 1)
				.OrderBy((GrandKeyBuffDataExcel x) => x.GrandKeySkill)
				.FirstOrDefault();
			pick ??= list.Where((GrandKeyBuffDataExcel x) => x.IsSynchron == 0)
				.OrderBy((GrandKeyBuffDataExcel x) => x.UnlockgrandKeyLevel)
				.ThenBy((GrandKeyBuffDataExcel x) => x.GrandKeySkill)
				.FirstOrDefault();
			return (uint)(pick?.GrandKeySkill ?? 0);
		}

		/// <summary>当前超限档位下能用的同步技能（breachLevel &lt;= 当前值里最大的那个）。</summary>
		public static uint BestSynchronSkillId(int grandKeyId, uint breachLevel)
		{
			GrandKeyBuffDataExcel? pick = Buffs(grandKeyId)
				.Where((GrandKeyBuffDataExcel x) => x.IsSynchron != 0 && x.BreachLevel <= breachLevel)
				.OrderByDescending((GrandKeyBuffDataExcel x) => x.BreachLevel)
				.ThenByDescending((GrandKeyBuffDataExcel x) => x.GrandKeySkill)
				.FirstOrDefault();
			return (uint)(pick?.GrandKeySkill ?? 0);
		}
	}

	public static readonly SqlTable<GrandKeysScheme> collection = Global.Table<GrandKeysScheme>("GrandKeys", ("owner_uid", "INTEGER", (GrandKeysScheme x) => x.OwnerUid, true));

	public static GrandKeysScheme FromUid(uint uid)
	{
		return (from c in collection.All()
			where c.OwnerUid == uid
			select c).FirstOrDefault() ?? Create(uid);
	}

	public static GrandKeysScheme Create(uint uid)
	{
		GrandKeysScheme? exist = (from c in collection.All()
			where c.OwnerUid == uid
			select c).FirstOrDefault();
		if (exist != null)
		{
			return exist;
		}
		// 老代码这里没写 OwnerUid，插入的行 owner_uid 是 0 ——
		// 于是 FromUid 永远查不到，每次都新建一行。必须补上。
		GrandKeysScheme scheme = new GrandKeysScheme
		{
			OwnerUid = uid,
			GrandKeyList = new List<GrandKey>()
		};
		collection.InsertOne(scheme);
		return scheme;
	}
}
