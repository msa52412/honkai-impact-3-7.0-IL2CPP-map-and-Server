using System;
using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer;

/// <summary>
/// 旧开放世界深渊（迪拉克之海 Type=2 / 量子奇点 Type=3）的<b>按模式</b>取数规则。
///
/// 2026-10-26 为什么单独抽出来
/// --------------------------
/// 之前 GetOpenworldEndlessDataReqHandler 里只有一张
/// ClientSafeLevels = {21,22,23,24,31..34}，那是迪拉克的实测白名单；
/// battle_config_id 的自激覆盖（62136）也是 Type=2 的行。
/// 结果量子请求（type=3）拿到的是 Type=2 的战斗配置：
///     QTInLevelData.Refresh 只跑 rsp.type==3 分支，
///     再用 NAJIOHCPGNI(battle_config_id, 3) 查表 —— Type=2 的行查不到，
///     BattleConfig / areaScoreConfig 全为 null，准备页与进关链路直接断。
///
/// 所以这里的原则是：**凡是从「模式」派生的数据，一律带 type 参数**。
/// 两张白名单来自 7.0 客户端实测（GetOWEndlessGroupMetaByKey 逐档扫描）：
///     迪拉克 Type=2 : 21~24, 31~34（35 图标缺失，不放）
///     量子   Type=3 : 11~14, 21~24, 31~35
///
/// 2026-10-26 修正：量子原来只放到 24，是早期扫表漏了 31~35。
/// 在运行中的客户端上用 GetOWEndlessGroupMetaByKey(level,3) 逐档探测，
/// mode=3 实际有 11,12,13,14,21,22,23,24,31,32,33,34,35 十三行，
/// 段位分值 GroupHardLevel 依次 1,1,2,3,4,4,5,6,7,8,9,10,11，
/// 与服务端 OWEndlessGroup.json 完全一致。所以量子也能进高级区（playerGroup 3）。
/// 现在的 abyss_ow.json 写的是 group_level=34 —— 量子会落到 34（高级区），
/// 不再被夹回 24（中级区）。
/// </summary>
internal static class OwEndlessConfig
{
	private static readonly uint[] DiracSafeLevels = { 21u, 22u, 23u, 24u, 31u, 32u, 33u, 34u };
	private static readonly uint[] QuantumSafeLevels =
	{
		11u, 12u, 13u, 14u, 21u, 22u, 23u, 24u, 31u, 32u, 33u, 34u, 35u
	};

	public static bool IsOldAbyss(uint type)
	{
		return type == (uint)EndlessType.EndlessTypeOpenworld || type == (uint)EndlessType.EndlessTypeQuantum;
	}

	public static uint[] SafeLevels(int type)
	{
		return type == (int)EndlessType.EndlessTypeQuantum ? QuantumSafeLevels : DiracSafeLevels;
	}

	/// <summary>
	/// 取 (GroupLevel, Type) 对应的分组行。
	/// 先夹进该模式的实测白名单（防 RefreshPlayerGroup+0x404 NRE 黑屏），再查服务端表。
	/// </summary>
	public static OWEndlessGroupExcel ResolveGroup(int type, int wantLevel)
	{
		uint[] levels = SafeLevels(type);
		uint safe = levels
			.Where((uint lv) => lv >= wantLevel)
			.DefaultIfEmpty(levels.Max())
			.Min();
		if (safe != (uint)wantLevel)
		{
			Packet.c.Log($"[Abyss] GroupLevel {wantLevel} 不在 type={type} 的客户端安全白名单 "
				+ $"[{string.Join(",", levels)}] 内，夹紧到 {safe}");
		}

		OWEndlessGroup reader = OWEndlessGroup.GetInstance();
		OWEndlessGroupExcel? row = reader.FromKey((int)safe, type);
		if (row != null)
		{
			return row;
		}

		// 白名单是客户端视角，服务端表可能缺某档；按同一 Type 的阶梯就近归一。
		row = reader.Normalize((int)safe, type);
		if (row != null)
		{
			return row;
		}

		Packet.c.Warn($"[Abyss] OWEndlessGroup 表里没有 (GroupLevel={safe}, Type={type})，使用合成行");
		return new OWEndlessGroupExcel
		{
			GroupLevel = (int)safe,
			Type = type,
			PlayerGroup = type == (int)EndlessType.EndlessTypeQuantum ? 1 : 2,
			GroupHardLevel = 1,
			MaxCapacity = 20,
			PromoteRank = 7,
			ExtraScore = 1000
		};
	}

	/// <summary>
	/// 实际下发的 HardLevel —— 客户端「能级 / 扰动幅度」那行文字就取这个值：
	/// MonoOWEndlessUIManager.RefreshInfo → OWEndlessDataMode.GetHardLevelByDataMode()
	/// → _CurDataMode==0 时直接返回 GroupInfo._HardLevel，而 GroupInfo 是
	/// EndlessBaseData.Refresh(rsp) 从 rsp.hard_level 灌进去的。
	/// 所以想让界面显示多少，就填多少，不受 OWEndlessGroup 表的 groupHardLevel 限制。
	///
	/// 2026-10-26：量子也走覆盖了。之前这里对 type=3 做了 &gt;11 就回退表值的特判，
	/// 理由是「能级会参与天气/Buff 查表」；查过客户端后确认：
	///   - 天气是 QTInLevelData.GetCurrentEnv()，按 environmentID 查，与能级无关；
	///   - 关卡里 QTEndlessGetHardLevel() 读的是表行，不是这个字段。
	/// 界面显示这条链是纯展示，所以 199 直接下发。
	///
	/// 选战斗配置**不要**用这个值，用 BattleHardLevel()。
	/// </summary>
	public static uint EffectiveHardLevel(int type, uint tableHardLevel)
	{
		return OwTuning.HardLevel != 0u ? OwTuning.HardLevel : tableHardLevel;
	}

	/// <summary>
	/// 选战斗配置用的等级。量子的 AreaScoreConfigID = 2000 + 能级，
	/// 客户端表里只有 2001..2008（实测 IODGPLEPPAF(2009) == null），
	/// 所以能级超过 8 时改用该档位的表值（再夹到 8），避免拿 199 去查空行。
	///
	/// 3419 与 3424/3448 必须调用同一个函数 —— 否则两边算出的 battle_config 不同，
	/// Align 会把进度当成「换了配置」反复清零。
	/// </summary>
	public static uint BattleHardLevel(int type, uint tableHardLevel)
	{
		uint hard = EffectiveHardLevel(type, tableHardLevel);
		if (type == (int)EndlessType.EndlessTypeQuantum && hard > 8u)
		{
			return Math.Min(tableHardLevel, 8u);
		}
		return hard;
	}

	/// <summary>
	/// 按当前请求模式 + 调参文件决定 battle_config_id。
	/// 3419（主数据）和 3424/3448（兜底 Ensure）必须走同一套选择，
	/// 否则两边配置不同 → Align 每次判定「配置变了」→ 进度被反复清零。
	/// </summary>
	public static uint ResolveRequestedBattleConfigId(int type, uint hardLevel)
	{
		uint overrideId = type == (int)EndlessType.EndlessTypeQuantum && OwTuning.QtBattleConfigId != 0u
			? OwTuning.QtBattleConfigId
			: OwTuning.BattleConfigId;
		return ResolveBattleConfigId(type, hardLevel, overrideId);
	}

	/// <summary>
	/// 决定下发给客户端的 battle_config_id。
	///
	/// overrideId 只在该行确实属于本次请求的 type 时才采用 ——
	/// 这正是之前「量子拿到 62136（迪拉克行）」的根因。
	/// </summary>
	public static uint ResolveBattleConfigId(int type, uint hardLevel, uint overrideId)
	{
		OWEndlessBattleConfig reader = OWEndlessBattleConfig.GetInstance();

		if (overrideId != 0u)
		{
			OWEndlessBattleConfigExcel? manual = reader.FromKey((int)overrideId);
			if (manual == null)
			{
				Packet.c.Warn($"[Abyss] battle_config_id={overrideId} 不在 OWEndlessBattleConfig 里，改回自动挑选");
			}
			else if (manual.EndlessConfigType != type)
			{
				Packet.c.Warn($"[Abyss] battle_config_id={overrideId} 属于 type={manual.EndlessConfigType}，"
					+ $"本次请求 type={type}，忽略覆盖并自动挑选");
			}
			else
			{
				return overrideId;
			}
		}

		return type == (int)EndlessType.EndlessTypeQuantum
			? PickQuantum(type, hardLevel)
			: PickGeneric(type, hardLevel);
	}

	/// <summary>
	/// 量子奇点：挑 AreaScoreConfigID = 2000 + 扰动 的配置。
	///
	/// 实测 type=3 有 299 行，AreaScore 2001..2008 是主玩法（20/15 场一条链），
	/// 2100.. / 4095.. 是单层教学/挑战档（5 场）。
	/// 每档里取最小 BattleConfigID（100001 / 200001 / ...），即该档的基础盘。
	/// </summary>
	private static uint PickQuantum(int type, uint hardLevel)
	{
		OWEndlessBattleConfig reader = OWEndlessBattleConfig.GetInstance();
		List<OWEndlessBattleConfigExcel> all = reader.ByType(type)
			.Where((OWEndlessBattleConfigExcel row) => row.AreaIDList != null && row.AreaIDList.Length > 0)
			.ToList();
		if (all.Count == 0)
		{
			Packet.c.Warn($"[Abyss] OWEndlessBattleConfig 里没有 type={type} 且带区域的配置");
			return 0u;
		}

		List<OWEndlessBattleConfigExcel> pool = all
			.Where((OWEndlessBattleConfigExcel row) => row.AreaScoreConfigID >= 2001 && row.AreaScoreConfigID <= 2008)
			.ToList();
		if (pool.Count == 0)
		{
			pool = all;
		}

		int wantScore = 2000 + (int)Math.Clamp(hardLevel, 1u, 8u);
		OWEndlessBattleConfigExcel? hit = pool
			.Where((OWEndlessBattleConfigExcel row) => row.AreaScoreConfigID == wantScore)
			.OrderBy((OWEndlessBattleConfigExcel row) => row.BattleConfigID)
			.FirstOrDefault();
		hit ??= pool
			.OrderBy((OWEndlessBattleConfigExcel row) => Math.Abs(row.AreaScoreConfigID - wantScore))
			.ThenBy((OWEndlessBattleConfigExcel row) => row.BattleConfigID)
			.FirstOrDefault();

		return hit == null ? 0u : (uint)hit.BattleConfigID;
	}

	/// <summary>其它模式沿用原来的「4 区域 + BOSS + 全局天气」挑选。</summary>
	private static uint PickGeneric(int type, uint hardLevel)
	{
		OWEndlessBattleConfig reader = OWEndlessBattleConfig.GetInstance();
		OWEndlessBattleConfigExcel? cfg = reader.PickFourAreaGlobalWeather(type, (int)hardLevel);
		if (cfg == null)
		{
			cfg = reader.ByType(type)
				.Where((OWEndlessBattleConfigExcel row) => row.AreaIDList != null && row.AreaIDList.Length > 0)
				.OrderBy((OWEndlessBattleConfigExcel row) => row.BattleConfigID)
				.FirstOrDefault();
		}
		return cfg == null ? 0u : (uint)cfg.BattleConfigID;
	}

	public static OWEndlessBattleConfigExcel? BattleConfig(uint battleConfigId)
	{
		return battleConfigId == 0u
			? null
			: OWEndlessBattleConfig.GetInstance().FromKey((int)battleConfigId);
	}

	public static OWEndlessAreaScoreConfigExcel? AreaScoreConfig(int type, uint battleConfigId)
	{
		OWEndlessBattleConfigExcel? cfg = BattleConfig(battleConfigId);
		if (cfg == null || cfg.EndlessConfigType != type)
		{
			return null;
		}
		return OWEndlessAreaScoreConfig.GetInstance().FromKey(cfg.AreaScoreConfigID, type);
	}

	/// <summary>本场战斗顺序（groupScoreId 序列）。</summary>
	public static OWEndlessSplitBattle[] SplitBattles(int type, uint battleConfigId)
	{
		return AreaScoreConfig(type, battleConfigId)?.SplitBattles ?? Array.Empty<OWEndlessSplitBattle>();
	}

	/// <summary>按 group_id 找它在 SplitBattles 里的位置，找不到返回 -1（与客户端 MonsterGroupToIndex 同义）。</summary>
	public static int IndexOf(OWEndlessSplitBattle[] splits, uint groupId)
	{
		for (int i = 0; i < splits.Length; i++)
		{
			if ((uint)splits[i].GroupScoreId == groupId)
			{
				return i;
			}
		}
		return -1;
	}

	public static uint GroupScore(uint groupId)
	{
		return OWEndlessMonsterGroupScore.GetInstance().ScoreOf(groupId);
	}

	// ------------------------------------------------------------ BOSS 层
	//
	// 2026-10-26 关键发现（表数据 + 日志实证）
	// ---------------------------------------
	// 迪拉克 cfg 62136 / AreaScoreConfig 1006：
	//     MonsterScoreConfig = [ "101:102:2603:104:3605",
	//                            "106:107:3608:109:2610",
	//                            "111:112:2613:114:3615",
	//                            "116:2617:118:3619" ]
	//     BossScoreConfig    = [ 131, 132, 133, 134 ]
	//     SplitBattles 里 isBoss=true 的 4 项 = 3605 / 2610 / 3615 / 3619
	//
	// 但客户端打 BOSS（area 3015 AreaBoss）时，3424 上报的 group_id 是 **131**，
	// 不是 3605。日志：`FinishGroup group=131 boss=False gain=1700`。
	//
	// 也就是说同一个 BOSS 场有两个 id：
	//     SplitBattles 里那个（3605 等） —— 只表示「这一场是 BOSS 场」，管场次顺序；
	//     BossScoreConfig[layer]（131 等）—— BOSS 的**计分组 id**，客户端上报、也用它
	//                                       在 OWEndlessMonsterGroupScore 里取分。
	//
	// 所以服务端凡是要按「层」处理 BOSS 的地方，都必须换成 BossScoreConfig 的 id。

	/// <summary>某层的 BOSS 计分组 = BossScoreConfig[layerId]；没有则 0。</summary>
	public static uint BossGroupOf(int type, uint battleConfigId, int layerId)
	{
		OWEndlessAreaScoreConfigExcel? asc = AreaScoreConfig(type, battleConfigId);
		int[]? boss = asc?.BossScoreConfig;
		if (boss == null || layerId < 0 || layerId >= boss.Length)
		{
			return 0u;
		}
		return (uint)boss[layerId];
	}

	/// <summary>group_id 若是某层的 BOSS 计分组，返回层号；否则 -1。</summary>
	public static int LayerOfBoss(int type, uint battleConfigId, uint groupId)
	{
		OWEndlessAreaScoreConfigExcel? asc = AreaScoreConfig(type, battleConfigId);
		int[]? boss = asc?.BossScoreConfig;
		if (boss == null)
		{
			return -1;
		}
		for (int i = 0; i < boss.Length; i++)
		{
			if ((uint)boss[i] == groupId)
			{
				return i;
			}
		}
		return -1;
	}

	/// <summary>
	/// 0..layerId 所有层的「计分组」序列（小怪 + BOSS）。
	/// BOSS 项用 BossScoreConfig 的 id 替换掉 SplitBattles 里的占位 id。
	/// layerId 传 <see cref="int.MaxValue"/> 即全表。
	///
	/// 迪拉克 1006 全表 = 101,102,2603,104,131 | 106,107,3608,109,132 |
	///                    111,112,2613,114,133 | 116,2617,118,134  （共 19 个）
	/// 四层满分为 6300 + 8800 + 12500 + 18300 = 45900。
	/// </summary>
	public static uint[] GroupsUpToLayer(int type, uint battleConfigId, int layerId)
	{
		OWEndlessAreaScoreConfigExcel? asc = AreaScoreConfig(type, battleConfigId);
		OWEndlessSplitBattle[]? splits = asc?.SplitBattles;
		if (splits == null || splits.Length == 0)
		{
			return Array.Empty<uint>();
		}
		int[] boss = asc!.BossScoreConfig ?? Array.Empty<int>();

		List<uint> list = new List<uint>();
		foreach (OWEndlessSplitBattle split in splits)
		{
			if (split.LayerId > layerId)
			{
				continue;
			}
			uint gid = split.IsBoss && split.LayerId >= 0 && split.LayerId < boss.Length
				? (uint)boss[split.LayerId]
				: (uint)split.GroupScoreId;
			if (gid != 0u && !list.Contains(gid))
			{
				list.Add(gid);
			}
		}
		return list.ToArray();
	}

	/// <summary>0..layerId 的分数合计（含 BOSS）。用于日志与自检。</summary>
	public static uint ScoreUpToLayer(int type, uint battleConfigId, int layerId)
	{
		uint sum = 0u;
		foreach (uint gid in GroupsUpToLayer(type, battleConfigId, layerId))
		{
			sum += GroupScore(gid);
		}
		return sum;
	}

	/// <summary>把 SplitBattles 里的一项翻译成「客户端会用来上报的 group_id」。</summary>
	public static uint ReportedGroupId(int type, uint battleConfigId, OWEndlessSplitBattle split)
	{
		if (split.IsBoss)
		{
			uint boss = BossGroupOf(type, battleConfigId, split.LayerId);
			if (boss != 0u)
			{
				return boss;
			}
		}
		return (uint)split.GroupScoreId;
	}
}
