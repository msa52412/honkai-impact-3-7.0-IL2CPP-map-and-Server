using System;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 家园小队（Sanctuary / 圣域）业务层。
///
/// 协议族（cmd 4305~4314）：
///   4305/4306  SanctuaryGetMainInfo      主界面（等级 / 经验 / 分组 / 关卡）
///   4307/4308  SanctuaryTakeLevelReward 领取等级奖励
///   4309/4310  SanctuaryTakeProduct     领取打工产物
///   4311       SanctuaryStartNotify     活动开始推送
///   4314       SanctuaryStageEndNotify  远征结算（total_add_exp / bonus_add_exp / old_level / old_exp）
///
/// 客户端链路（反汇编实证）
/// ------------------------
///   GHIABGLOBMK.OnSanctuaryGetMainInfoRsp (0x182454610)
///     ├─ [rsp+0x10] = retcode，**非 0 直接 return，什么都不更新**
///     ├─ [rsp+0x18] = activity_data，**为 null 直接 throw（0x1804fe400）**
///     ├─ this.SanctuaryID = activity_data.activity_id
///     ├─ this.BeginTime   = DateTime(rsp.begin_time)
///     ├─ this.EndTime     = DateTime(rsp.end_time)
///     └─ PNJNBJEAFAJ(activity_data) (0x182454820)
///          ├─ activity_data.activity_id 必须 == this.SanctuaryID，否则丢弃
///          └─ +0x60 为 null → BEJCNOPOFHA(0x180E3CF80) 建包装；否则 CPOPAAGJKEC(0x180E3D210) 更新
///
///   CPOPAAGJKEC 里三个查表（都要能查到，否则包装里的配置引用为 null）：
///     +0x20 = 按 activity_id 查活动配置
///     +0x28 = 按 (player_level_group_id, activity_id) 查玩家分组配置
///     +0x30 = 按 (level, activity_id) 查等级配置
///   本版资源里没有「等级」配置表（ExcelMeta 里搜不到任何 Sanctuary 类，
///   SanctuaryBuildingLevel.json / SanctuaryBuildingActivity.json 都是 2 字节空数组），
///   所以客户端拿不到经验上限，会退化成 int.MaxValue —— 界面显示「经验值 0/2147483647」。
///   等级本身仍然由 activity_data.level 直接显示，所以服务端下发什么就显示什么。
/// </summary>
internal static class SanctuaryService
{
	/// <summary>家园小队活动的固定 id（SanctuaryPlayerGroup.json 三行都是 1001）。</summary>
	private const uint DefaultActivityId = 1001u;

	/// <summary>活动开始时间（2020-01-01 00:00:00 UTC）。固定值，保证每次下发一致。</summary>
	private const uint BeginTime = 1577836800u;

	/// <summary>活动结束时间（2035-12-31 23:59:59 UTC）。uint 上限是 2106 年，留足余量。</summary>
	private const uint EndTime = 2082758399u;

	/// <summary>
	/// 每级所需经验（索引 0 = 1 级升 2 级），共 19 档 → 最高 20 级。
	/// 上限 20 的依据：TextMap 里家园小队只出现了 Lv.1 / Lv.5 / Lv.10 / Lv.15 / Lv.20 五档文案。
	/// 曲线是服务端自定的（客户端没有这张表），要改就改 run\sanctuary.json 的 exp_scale，
	/// 不要直接改这个数组 —— 数组只作为「1.0 倍」的基准。
	/// 累计到 20 级 = 71650 经验。
	/// </summary>
	private static readonly uint[] LevelUpExp =
	{
		100u, 200u, 350u, 500u, 700u,
		950u, 1250u, 1600u, 2000u, 2500u,
		3100u, 3800u, 4600u, 5500u, 6500u,
		7600u, 8800u, 10100u, 11500u
	};

	private static SanctuaryPlayerGroup PlayerGroupReader =>
		BaseExcelReader<SanctuaryPlayerGroup, SanctuaryPlayerGroupExcel>.GetInstance();

	/// <summary>
	/// 把存档补齐到可以下发的状态，返回 true 表示有变化、需要 Save。
	///
	/// 只在真的有变化时才写库：客户端每次进家园都会发 4305，
	/// 如果无条件 Save，会变成每次进界面写一次 SQLite。
	/// </summary>
	public static bool Prepare(SanctuaryScheme s, Player player)
	{
		bool changed = false;

		if (s.StageIdLists == null)
		{
			s.StageIdLists = Array.Empty<uint>();
			changed = true;
		}

		// ① 活动 id：以配置表为准，表缺失时退回 1001。
		uint activityId = PlayerGroupReader.ActivityId();
		if (activityId == 0u)
		{
			activityId = DefaultActivityId;
		}
		if (s.ActivityId != activityId)
		{
			s.ActivityId = activityId;
			changed = true;
		}

		// ② 玩家等级分组：永远按当前玩家等级重算（等级会涨，分组跟着走）。
		uint group = ResolveGroupId(PlayerLevelOf(player));
		if (s.PlayerLevelGroupId != group)
		{
			s.PlayerLevelGroupId = group;
			changed = true;
		}

		// ③ 等级 / 经验。
		uint level;
		uint exp;
		if (SanctuaryTuning.Level.HasValue)
		{
			// 调参文件强制等级：完全跳过折算，方便对照实验。
			level = Math.Clamp(SanctuaryTuning.Level.Value, 1u, SanctuaryTuning.MaxLevel);
			exp = SanctuaryTuning.Exp.Value;
		}
		else
		{
			// 阵容折算 + 玩法额外经验。
			//
			// 注意：这里**不能**对历史 TotalExp 取 max。
			// 早先版本用了 max(存档, 阵容)，结果 run\sanctuary.json 的 exp_scale 改了也不生效 ——
			// 存档里压着上次算出来的大数，永远赢。现在把两者拆开：
			//   阵容部分每次重算（跟着权重 / 缩放走）
			//   BonusExp 单独存（远征等玩法产出，不会被重算冲掉）
			uint roster = RosterExp(player);
			uint total = (roster > uint.MaxValue - s.BonusExp) ? uint.MaxValue : roster + s.BonusExp;
			if (s.TotalExp != total)
			{
				s.TotalExp = total;
				changed = true;
			}
			ComputeLevelExp(total, SanctuaryTuning.MaxLevel, out level, out exp);
			if (SanctuaryTuning.Exp.HasValue)
			{
				exp = SanctuaryTuning.Exp.Value;
			}
		}

		if (s.Level != level)
		{
			s.Level = level;
			changed = true;
		}
		if (s.Exp != exp)
		{
			s.Exp = exp;
			changed = true;
		}

		// ④ 等级奖励水位线。
		//    等级奖励表在本版资源里不存在（没有 Sanctuary 奖励表），
		//    所以默认让它跟随等级 —— 否则界面会挂一个永远点不掉的「有奖励可领」红点。
		uint take = SanctuaryTuning.HasTakeRewardLevel ?? s.Level;
		if (s.HasTakeRewardLevel != take)
		{
			s.HasTakeRewardLevel = take;
			changed = true;
		}

		if (!s.IsInit)
		{
			s.IsInit = true;
			changed = true;
		}

		if (changed)
		{
			s.LastSyncTime = (uint)Global.GetUnixInSeconds();
			Packet.c.Log($"[Sanctuary] uid={s.OwnerUid} 更新 -> activity={s.ActivityId} "
				+ $"group={s.PlayerLevelGroupId} level={s.Level} exp={s.Exp} "
				+ $"total_exp={s.TotalExp} take_reward={s.HasTakeRewardLevel}");
		}

		return changed;
	}

	/// <summary>填 4306 主信息回包。</summary>
	public static void FillMainInfo(SanctuaryGetMainInfoRsp rsp, SanctuaryScheme s)
	{
		rsp.retcode = SanctuaryGetMainInfoRsp.Retcode.Succ;
		rsp.ActivityData = ToProto(s);
		rsp.BeginTime = BeginTime;
		rsp.EndTime = EndTime;
	}

	/// <summary>
	/// 主动下发一次 4306（幂等，重复调用无害）。
	///
	/// 为什么需要「主动推」而不是等客户端发 4305
	/// ----------------------------------------
	/// 客户端 MonoCASanctuaryUIManager.Init (0x18093BA70) 会把包装对象缓存下来：
	///     this._sanctuaryData = _module.KLGLBEEJGEC()      // +0x58
	/// 而 KLGLBEEJGEC (0x182454430) 只有一条 `mov rax,[rbx+0x60]` —— **纯字段读，不懒创建**；
	/// 那个 +0x60 的 KLDGODLLNFJ 只有第一次收到 4306 时才会 new（PNJNBJEAFAJ 0x182454820）。
	///
	/// 接着 SetupView (0x18093D180) 第一行就是
	///     cmp qword ptr [rbx+0x58], 0 / je ret
	/// —— 缓存为 null 时**整个页面一个 Setup 都不跑**，底栏停在预制体默认值
	/// 「家园小队 等级 1 / 经验值 0/2147483647」，之后再来 4306 也不会重画。
	///
	/// 所以必须在「家园页被创建之前」把 4306 送到，让 module+0x60 先存在。
	/// 实测 4305 是页面打开后才发的（日志：GetDormDataReq → SanctuaryGetMainInfoReq），
	/// 天生慢一步，只能由服务端在登录/世界加载阶段主动推。
	///
	/// 调用点（都在世界加载早期、早于玩家能点进家园）：
	///   PlayerLoginReq / GetMainDataReq / GetHasGotItemIdListReq
	/// 多打几枪是因为模块构造时机不确定，早到的会被丢掉（无害），晚到的才生效。
	/// </summary>
	public static void PushMainInfo(Session session)
	{
		SanctuaryScheme sanctuary = session.Player.Sanctuary;
		if (Prepare(sanctuary, session.Player))
		{
			sanctuary.Save();
		}

		SanctuaryGetMainInfoRsp rsp = new SanctuaryGetMainInfoRsp();
		FillMainInfo(rsp, sanctuary);
		session.Send(Packet.FromProto(rsp, CmdId.SanctuaryGetMainInfoRsp));
	}

	/// <summary>
	/// 推「活动开始」通知（4311，空消息）。
	///
	/// 为什么需要：家园页的 UI 只在 Init 那一刻缓存 module[+0x60]，
	/// 而那个对象只有模块处理过 4306 才会创建。官方客户端在登录阶段就能拿到数据，
	/// 说明模块在进家园之前就已经被活动系统初始化过 —— 4311 就是那个「开始」信号。
	///
	/// 4311 是 Notify（不是 Rsp），客户端对 Notify 不做「有没有请求过」的过滤，
	/// 所以这一枪能真正送到模块手里；4306 那种未请求的 Rsp 会被直接丢掉。
	/// </summary>
	public static void PushStartNotify(Session session)
	{
		session.Send(Packet.FromProto(new SanctuaryStartNotify(), CmdId.SanctuaryStartNotify));
	}

	/// <summary>延迟若干毫秒后再推一次 4306（模块构造时机不确定，用来兜底）。</summary>
	public static void PushMainInfoDelayed(Session session, int delayMs)
	{
		System.Threading.Tasks.Task.Run(async () =>
		{
			try
			{
				await System.Threading.Tasks.Task.Delay(delayMs);
				PushMainInfo(session);
			}
			catch (Exception ex)
			{
				Packet.c.Warn("[Sanctuary] 延迟推送失败：" + ex.Message);
			}
		});
	}

	/// <summary>存档 → proto。字段顺序 / 名字照 Common.Resources.Proto.SanctuaryActivityData。</summary>
	public static SanctuaryActivityData ToProto(SanctuaryScheme s)
	{
		return new SanctuaryActivityData
		{
			ActivityId = s.ActivityId,
			Level = s.Level,
			Exp = s.Exp,
			HasTakeRewardLevel = s.HasTakeRewardLevel,
			ProductCounterTime = s.ProductCounterTime,
			ProductStorageNum = s.ProductStorageNum,
			IsInit = s.IsInit,
			PlayerLevelGroupId = s.PlayerLevelGroupId,
			StageIdLists = s.StageIdLists ?? Array.Empty<uint>(),
			IsNewStage = s.IsNewStage
		};
	}

	/// <summary>玩家等级（按 User.Exp 查 PlayerLevelData 表）。</summary>
	public static int PlayerLevelOf(Player player)
	{
		try
		{
			return BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance()
				.CalculateLevel(player.User.Exp).Level;
		}
		catch (Exception ex)
		{
			Packet.c.Warn("[Sanctuary] 算玩家等级失败，按 1 级处理：" + ex.Message);
			return 1;
		}
	}

	/// <summary>
	/// 玩家等级 → Sanctuary 分组 id。
	/// 表：Lv81~85→101 / Lv61~80→102 / Lv30~60→103（ActivityID 全是 1001）。
	/// 表缺失或等级越界时按同样的分界硬编码兜底，绝不下发 0
	/// （0 会让客户端按 (0, activity_id) 查分组配置，查空后界面数值全塌）。
	/// </summary>
	public static uint ResolveGroupId(int playerLevel)
	{
		try
		{
			SanctuaryPlayerGroupExcel? row = PlayerGroupReader.ForPlayerLevel(playerLevel);
			if (row != null && row.PlayerGroupId > 0)
			{
				return (uint)row.PlayerGroupId;
			}
		}
		catch (Exception ex)
		{
			Packet.c.Warn("[Sanctuary] SanctuaryPlayerGroup 表读取失败：" + ex.Message);
		}

		if (playerLevel >= 81)
		{
			return 101u;
		}
		if (playerLevel >= 61)
		{
			return 102u;
		}
		return 103u;
	}

	/// <summary>
	/// 按女武神阵容折算家园小队经验。
	///
	/// 官方机制（TextMap 362360405 / 362360652）：
	///   「提升女武神等级和阶级可以提升 Squad Asrama 等级；
	///     提升女武神星级和阶级可以获得 Squad EXP」
	/// 所以这里对每个女武神按 level + star 各算一份贡献，再乘总缩放系数。
	/// 权重是 SanctuaryTuning 里的 exp_per_avatar_level / exp_per_avatar_star。
	/// </summary>
	public static uint RosterExp(Player player)
	{
		if (player.AvatarList == null || player.AvatarList.Length == 0)
		{
			return 0u;
		}

		double perLevel = SanctuaryTuning.ExpPerAvatarLevel;
		double perStar = SanctuaryTuning.ExpPerAvatarStar;
		double sum = 0.0;
		foreach (AvatarScheme avatar in player.AvatarList)
		{
			// 先转 double 再乘，避免 uint 溢出。
			sum += (double)avatar.Level * perLevel;
			sum += (double)avatar.Star * perStar;
		}

		sum *= SanctuaryTuning.ExpScale;
		if (sum <= 0.0)
		{
			return 0u;
		}
		if (sum >= uint.MaxValue)
		{
			return uint.MaxValue;
		}
		return (uint)sum;
	}

	/// <summary>
	/// 累计经验 → (等级, 当前等级内经验)。
	/// 满级时当前经验归 0（客户端满级的经验上限本来也显示不出来）。
	/// </summary>
	public static void ComputeLevelExp(uint totalExp, uint maxLevel, out uint level, out uint exp)
	{
		uint cap = Math.Clamp(maxLevel, 1u, (uint)LevelUpExp.Length + 1u);
		uint lv = 1u;
		uint left = totalExp;
		while (lv < cap)
		{
			uint need = LevelUpExp[lv - 1u];
			if (left < need)
			{
				break;
			}
			left -= need;
			lv++;
		}

		level = lv;
		exp = (lv >= cap) ? 0u : left;
	}

	/// <summary>某等级升下一级需要多少经验；满级返回 0。</summary>
	public static uint ExpToNextLevel(uint level)
	{
		if (level < 1u || level > (uint)LevelUpExp.Length)
		{
			return 0u;
		}
		return LevelUpExp[level - 1u];
	}

	/// <summary>本活动所有远征关卡 id（SanctuaryStageConfig.json，45 关 161001~161045）。</summary>
	public static uint[] AllStageIds()
	{
		try
		{
			SanctuaryStageConfigExcel[] all =
				BaseExcelReader<SanctuaryStageConfig, SanctuaryStageConfigExcel>.GetInstance().All;
			if (all == null || all.Length == 0)
			{
				return Array.Empty<uint>();
			}
			List<uint> ids = new List<uint>(all.Length);
			foreach (SanctuaryStageConfigExcel row in all)
			{
				if (row.StageID > 0)
				{
					ids.Add((uint)row.StageID);
				}
			}
			return ids.ToArray();
		}
		catch (Exception ex)
		{
			Packet.c.Warn("[Sanctuary] SanctuaryStageConfig 表读取失败：" + ex.Message);
			return Array.Empty<uint>();
		}
	}
}
