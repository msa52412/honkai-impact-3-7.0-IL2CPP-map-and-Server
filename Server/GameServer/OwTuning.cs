using System;
using System.IO;
using Common;
using Newtonsoft.Json;

namespace PemukulPaku.GameServer;

/// <summary>
/// 旧深渊（迪拉克之海 / 量子奇点）的运行期调参，**改文件即生效，不用重启服务端**。
///
/// 为什么需要这个
/// --------------
/// 2026-10-25 17:48：把 GroupLevel 从 21 改成 35，客户端黑屏转圈，
/// 服务端一秒内收到 100 次 GetOpenworldEndlessDataReq ——
/// RefreshPlayerGroup+0x404 拿到 GetOWEndlessGroupMetaByKey(35) == null 抛 NRE。
///
/// 2026-10-25 17:53：改成 24 后没有风暴，但客户端**静默拒绝**了整份数据 ——
/// 表现是 OWEndlessModule.get_Group() 返回的对象字段全 0
/// （GroupId=0 / GroupLevel=0 / CurTop=0），而 17:32 用 21 时是 1/21/22。
/// 分组为 0 时 GoToOWEndless 会打开一个永远等不到回调的页面（转圈无法操作）。
///
/// 说明「整份响应里某个字段让客户端 apply 中途抛异常」，但不一定只是 GroupLevel。
/// 所以这里把每个可变量都做成可调，逐个排除，不用每次改代码重编译。
///
/// 文件：run\abyss_ow.json
/// {
///   "group_level": 21,            // 分组等级；0/省略 = 用 AbyssConfig.OwGroupLevel
///   "cur_top_group_level": 22,    // 0/省略 = 按阶梯最高档自动算
///   "hard_level": 1,              // 扰动幅度；0/省略 = 查表 groupHardLevel
///   "top_group_player_num": 20,   // 0/省略 = 查表 maxCapacity
///   "promote_unlock_num": 7,      // 0/省略 = 查表 promoteRank
///   "send_brief": true            // false = 不下发 brief_data_list
/// }
///
/// 之所以要 send_brief 开关：brief_data_list 是 17:53 那次相比 17:32 新增的字段，
/// 必须能单独关掉验证它是不是元凶。
/// </summary>
internal static class OwTuning
{
	private const string FILE_NAME = "abyss_ow.json";

	private class Dto
	{
		[JsonProperty("group_level")]
		public uint GroupLevel { get; set; }

		[JsonProperty("cur_top_group_level")]
		public uint CurTopGroupLevel { get; set; }

		[JsonProperty("hard_level")]
		public uint HardLevel { get; set; }

		[JsonProperty("top_group_player_num")]
		public uint TopGroupPlayerNum { get; set; }

		[JsonProperty("promote_unlock_num")]
		public uint PromoteUnlockNum { get; set; }

		[JsonProperty("send_brief")]
		public bool? SendBrief { get; set; }

		[JsonProperty("roster_size")]
		public uint RosterSize { get; set; }

		[JsonProperty("brief_mask")]
		public uint BriefMask { get; set; }

		[JsonProperty("brief_self_only")]
		public bool BriefSelfOnly { get; set; }

		/// <summary>
		/// rsp.battle_config_id 覆盖。0 = 自动按 (模式, 扰动等级) 从
		/// OWEndlessBattleConfig 里挑「4 区域 + BOSS + 全局天气」那一行。
		/// </summary>
		[JsonProperty("battle_config_id")]
		public uint BattleConfigId { get; set; }

		/// <summary>
		/// 量子奇点专用覆盖（type=3）。0 = 自动挑 AreaScore 2001..2008 里对应扰动的那档。
		/// 单独开一个键是因为迪拉克和量子的 BattleConfigID 完全不相交，
		/// 共用一个键必然有一边被另一个模式的 ID 顶掉（这正是之前的 bug）。
		/// </summary>
		[JsonProperty("qt_battle_config_id")]
		public uint QtBattleConfigId { get; set; }

		/// <summary>
		/// rsp.invasion_id 覆盖（迪拉克「干涉」界面的入侵配置号）。0 = 用 1。
		///
		/// 为什么需要
		/// ----------
		/// 客户端 OWEndLessBattleLevelInfo.Refresh 把 rsp+0x7c 直接写进
		/// _invadeID（类内 +0x58）：
		///     mov eax,[r15+0x7c] ; mov [rsi+0x58],eax
		/// 「干涉 -> 召唤干涉怪物」界面（OWEndlessUseInvadeItemDialogContext）
		/// SetupBossInfo / SetupBuffCondition 都拿 _invadeID 去查 OWEndlessInvade：
		///     OWEndlessInvade[invadeID].BossConfigIDList
		///       -> OWEndlessBossData[ID].BOSSDisplayIDList[0]
		///         -> StageDetail_Monster[displayID].IconPath / DisplayName
		/// 服务端原来下发 0，查不到行 -> boss 图标和名字全空，界面只剩
		/// prefab 默认文本，看起来就是「一堆数字 / ID」。
		/// </summary>
		[JsonProperty("invasion_id")]
		public uint InvasionId { get; set; }

		/// <summary>
		/// 本期深渊开始时间（Unix 秒）。0 = 自动（按 period_days 对齐到 UTC+8 04:00 的换期点）。
		/// 客户端「深渊排期」界面显示的「1970-…」就是它 —— 服务端原来写死 0。
		/// </summary>
		[JsonProperty("period_begin_time")]
		public uint PeriodBeginTime { get; set; }

		/// <summary>一期多少天，默认 3；0 = 用默认；上限夹到 28。</summary>
		[JsonProperty("period_days")]
		public uint PeriodDays { get; set; }

		/// <summary>结束到关闭之间留多少秒，默认 1200；0 = 用默认。</summary>
		[JsonProperty("period_close_delay")]
		public uint PeriodCloseDelay { get; set; }

		/// <summary>
		/// true = 固定区间模式：period_begin_time / period_end_time 原样下发，
		/// 不做 04:00 对齐、不做过期顺延，此时 period_begin_time 允许为 0（= 1970-01-01 08:00）。
		/// 用于把深渊周期钉死在一个永不轮换的区间。
		/// </summary>
		[JsonProperty("period_fixed")]
		public bool? PeriodFixed { get; set; }

		/// <summary>
		/// 本期结束时间（Unix 秒）。0 = begin + period_days。
		/// 2099-12-31 23:59:59 (UTC+8) = 4102415999。
		/// </summary>
		[JsonProperty("period_end_time")]
		public uint PeriodEndTime { get; set; }

		/// <summary>
		/// true/省略 = 打完一组后额外推 3445 OpenworldEndlessPlayerUpdateNotify。
		/// 这是客户端刷新「自己那行分数/名次」的通道；万一它反而让客户端报错，
		/// 把这里设成 false 就能单独关掉做二分。
		/// </summary>
		[JsonProperty("push_score_notify")]
		public bool? PushScoreNotify { get; set; }
	}

	private static DateTime _stamp = DateTime.MinValue;
	private static Dto _dto = new Dto();

	/// <summary>覆盖文件里的 group_level；0 表示未设置。</summary>
	public static uint GroupLevel => Load().GroupLevel != 0u ? _dto.GroupLevel : AbyssConfig.OwGroupLevel;

	/// <summary>覆盖文件里的 cur_top_group_level；0 表示按阶梯最高档自动算。</summary>
	public static uint CurTopGroupLevel => Load().CurTopGroupLevel;

	/// <summary>覆盖文件里的 hard_level；0 表示查表 groupHardLevel。</summary>
	public static uint HardLevel => Load().HardLevel;

	/// <summary>覆盖文件里的 top_group_player_num；0 表示查表 maxCapacity。</summary>
	public static uint TopGroupPlayerNum => Load().TopGroupPlayerNum;

	/// <summary>覆盖文件里的 promote_unlock_num；0 表示查表 promoteRank。</summary>
	public static uint PromoteUnlockNum => Load().PromoteUnlockNum;

	/// <summary>是否下发 brief_data_list，默认 true。</summary>
	public static bool SendBrief => Load().SendBrief ?? true;

	/// <summary>
	/// 分组名单人数（endless_data_list / brief_data_list 各这么多条）。
	/// 0 = 默认 20。做成可调是为了二分「客户端到底要几条」。
	/// </summary>
	public static uint RosterSize => Load().RosterSize;

	/// <summary>
	/// brief 字段掩码：0 = 全字段；bit0=nickname bit1=level bit2=avatar_id
	/// bit3=custom_head_id bit4=frame_id bit5=mp_level bit6=online_status。
	/// 用于二分「brief 里哪个字段让客户端拒绝整份响应」。
	/// </summary>
	public static uint BriefMask => Load().BriefMask;

	/// <summary>true = 只给自己的那一行发 brief（其余行只有 uid）。</summary>
	public static bool BriefSelfOnly => Load().BriefSelfOnly;

	/// <summary>
	/// 覆盖 rsp.battle_config_id。0 = 自动挑「4 区域 + BOSS + 全局天气」的那一行。
	///
	/// 为什么这个字段比 GroupLevel 还关键：客户端
	/// OWEndLessBattleLevelInfo.Refresh 用 battle_config_id 反查
	/// OWEndlessBattleConfig 拿 _areaList / _bossAreaIDList /
	/// _environmentIDList / _mapID，查不到就整块地图数据为空
	/// （不是黑屏，是「有界面但没图」）。
	///
	/// 手动实验：
	///   "battle_config_id": 10001   4 区域，天气全 101（无天气）
	///   "battle_config_id": 62000   4 区域，天气全 118（全局天气，扰动 6 档）
	///   "battle_config_id": 0       自动（推荐）
	/// </summary>
	public static uint BattleConfigId => Load().BattleConfigId;

	/// <summary>量子奇点专用 battle_config_id 覆盖；0 = 自动。</summary>
	public static uint QtBattleConfigId => Load().QtBattleConfigId;

	/// <summary>
	/// 3420 下发的 invasion_id（迪拉克干涉配置号，OWEndlessInvade 的 InvadeID）。
	/// 0 = 用 1。客户端拿它查 OWEndlessInvade 建「召唤干涉怪物」界面的
	/// boss 图标 / 名字 / 条件 buff。
	/// </summary>
	public static uint InvasionId
	{
		get
		{
			uint v = Load().InvasionId;
			return v != 0u ? v : 1u;
		}
	}

	/// <summary>
	/// 是否在 3425 之后额外推 3445 OpenworldEndlessPlayerUpdateNotify，默认 true。
	/// 关掉就回到「只回 3425、等下次 3420 才刷新分数」的旧行为。
	/// </summary>
	public static bool PushScoreNotify => Load().PushScoreNotify ?? true;

	/// <summary>
	/// 本期深渊的时间窗（begin / end / close），三处响应共用。
	///
	/// 为什么需要
	/// ----------
	/// 客户端「深渊排期」界面那行「1970/01/01 08:00 - 2026/11/02 04:00」，
	/// 来源是 EndlessStatus.BeginTime / EndTime，链路：
	///     MonoEndlessScheduleSelectionCmpt.SetNormalEndlessItemView(cmpt, EndlessStatus, bool)
	///       -> MonoEndlessScheduleEndlessItemCmpt.ViewParams
	///            +0x20 StartTime (RemoteTime)
	///            +0x24 EndTime   (RemoteTime)
	///       -> _openTimeTxt (+0x50)
	/// 服务端原来三处都写 BeginTime = 0u，0 就是 1970-01-01，于是显示成 1970-2026。
	///
	/// 客户端自己那张 OWEndlessSchedule / QTEndlessSchedule 表指望不上：
	/// 只有 RewardConfig + StartTime 两列，且最晚一行是
	///     迪拉克 2022-04-01 / 量子 2022-12-05
	/// now 早已越界，只能由协议字段给。
	///
	/// 对齐规则：begin 取「UTC+8 每天 04:00」的整数倍换期点，
	/// 这样同一次登录里反复刷新也不会看到开始时间跳变。
	///
	/// 固定区间模式（period_fixed = true）
	/// -----------------------------------
	/// begin / end 原样下发，不做对齐也不顺延，begin 允许是 0。
	/// 用来把周期钉死成 1970-01-01 08:00 ~ 2099-12-31 23:59:59：
	///     "period_fixed": true,
	///     "period_begin_time": 0,
	///     "period_end_time": 4102415999
	/// 此时 close 与 end 对齐，避免 close 溢到 2100 又被算成新一期。
	/// </summary>
	public static void Period(out uint begin, out uint end, out uint close)
	{
		Dto d = Load();
		long now = Global.GetUnixInSeconds();

		// ---- 固定区间模式 ----
		if (d.PeriodFixed == true)
		{
			long fb = d.PeriodBeginTime;
			long fe = d.PeriodEndTime != 0u ? d.PeriodEndTime : 4102415999L;
			if (fe <= fb)
			{
				fe = fb + 86400L;
			}
			begin = (uint)fb;
			end = (uint)fe;
			close = (uint)fe;
			return;
		}

		// ---- 自动模式：按 period_days 滚期 ----
		uint days = d.PeriodDays;
		if (days == 0u)
		{
			days = 3u;
		}
		if (days > 28u)
		{
			days = 28u;
		}
		long span = (long)days * 86400L;

		long b;
		if (d.PeriodBeginTime != 0u)
		{
			b = d.PeriodBeginTime;
		}
		else
		{
			// 28800 = UTC+8；14400 = 04:00。合起来让换期点落在本地 04:00。
			long local = now + 28800L - 14400L;
			b = now - (local % span);
		}

		long e = d.PeriodEndTime != 0u ? d.PeriodEndTime : b + span;

		if (e <= now || e <= b)
		{
			// begin/end 落在过去：整段顺延到下一期，
			// 否则客户端会按 end_time 判定「本期已结束」而关掉入口。
			b = now - (now % span);
			e = b + span;
		}

		uint delay = d.PeriodCloseDelay;
		if (delay == 0u)
		{
			delay = 1200u;
		}

		begin = (uint)b;
		end = (uint)e;
		close = (uint)(e + delay);
	}

	private static Dto Load()
	{
		try
		{
			string path = Path.Combine(Directory.GetCurrentDirectory(), FILE_NAME);
			if (!File.Exists(path))
			{
				_dto = new Dto();
				_stamp = DateTime.MinValue;
				return _dto;
			}
			DateTime stamp = File.GetLastWriteTimeUtc(path);
			if (stamp == _stamp)
			{
				return _dto;
			}
			Dto? dto = JsonConvert.DeserializeObject<Dto>(File.ReadAllText(path));
			_dto = dto ?? new Dto();
			_stamp = stamp;
			Packet.c.Log("[Abyss] 读取 " + FILE_NAME + " -> "
				+ $"group_level={_dto.GroupLevel} cur_top={_dto.CurTopGroupLevel} "
				+ $"hard={_dto.HardLevel} topNum={_dto.TopGroupPlayerNum} "
				+ $"promote={_dto.PromoteUnlockNum} brief={_dto.SendBrief} "
				+ $"mask={_dto.BriefMask} selfOnly={_dto.BriefSelfOnly} "
				+ $"battleConfig={_dto.BattleConfigId} qtBattleConfig={_dto.QtBattleConfigId} "
				+ $"periodBegin={_dto.PeriodBeginTime} periodDays={_dto.PeriodDays} "
				+ $"closeDelay={_dto.PeriodCloseDelay} "
				+ $"fixed={_dto.PeriodFixed} periodEnd={_dto.PeriodEndTime} "
				+ $"pushScoreNotify={_dto.PushScoreNotify} invasion={_dto.InvasionId}");
		}
		catch (Exception ex)
		{
			// 文件写坏了就当没配，绝不因为调参文件让服务端抛异常。
			_dto = new Dto();
			_stamp = DateTime.MinValue;
			Packet.c.Warn("[Abyss] 读取 " + FILE_NAME + " 失败，忽略：" + ex.Message);
		}
		return _dto;
	}
}
