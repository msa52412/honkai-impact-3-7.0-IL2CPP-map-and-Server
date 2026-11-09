using Common.Resources.Proto;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PemukulPaku.GameServer;

/// <summary>
/// 深渊运行期开关。
///
/// 为什么是「进程内静态字段」而不是存档字段：
/// 这几个值只影响客户端怎么摆 UI，不属于玩家资产，
/// 落库反而要动 SQLite schema、还容易和 v1 存档迁移打架。
/// 重启服务端就回到默认值，够实验用，也随时能回退。
///
/// 默认值 = 迪拉克之海（Selected=2 / CurStatus=Openworld / OwGroupLevel=21）。
///
/// 历史
/// ----
/// 原版 v2 是 Selected=5（贪婪）+ CurStatus=Ultra，两者自相矛盾，
/// 客户端于是跑去请求 GreedyEndlessTakeRankRewardReq（实测 44 次全落空）。
///
/// 2026-10-22 11:40 曾把默认值还原成「17:26 那次」的基线（Selected=5 / CurStatus=Ultra），
/// 当时是为了排查「点开放世界入口跳转失败」的问题；那条线后来查明是
/// OpenWorldMainStubPageContext 自身的跳转条件，与这几个字段无关。
///
/// 2026-10-25 17:26 之后确认：真正卡住 GoToOWEndless 的是两个条件
///   ① CurStatus.EndlessType ∈ {2,3}   —— 否则 GetCurrentEndlessData() 为 null，SetupView 判空炸
///   ② rsp.GroupLevel ∈ 客户端 OWEndlessGroup 表有效集 —— 否则 RefreshPlayerGroup+0x404 炸
/// 这两条现在都已满足（CurStatus=Openworld + OwGroupLevel=21），GoToOWEndless 实测成功。
///
/// 所以默认值改成 2 / Openworld —— 重启服务端不再丢设置，省掉每次手动 `abyss legacy 2`。
/// 要换别的深渊：游戏内聊天 `abyss legacy <n>`，热生效。
/// </summary>
internal static class AbyssConfig
{
	/// <summary>
	/// GetEndlessStatusRsp.SelectedEndlessType —— 客户端据此决定「挑战」面板里
	/// 主推哪一套深渊，也会拿它去拉对应的排行/奖励。
	///
	///   0 不选  1 普通  2 迪拉克之海  3 量子奇点  4 超弦空间  5 贪婪深渊  6 前哨无尽
	///
	/// 2026-11-08：改成「启动时从 abyss_ow.json 的 selected_type 读，读不到用 2」。
	/// 原来纯内存，重启服务端必回默认 2 —— 面板上切到量子、服务端一重启又变回迪拉克，
	/// 现象和「切换不生效」一模一样（实测踩过一次，误判成接口坏了）。
	/// 现在客户端切类型（3462）、GM 开关、聊天命令都会写回文件，重启后保留。
	/// </summary>
	public static uint SelectedEndlessType = LoadSelected();

	/// <summary>GetEndlessStatusRsp.CurStatus.EndlessType</summary>
	public static EndlessType CurStatusType = MapStatus(SelectedEndlessType);

	/// <summary>
	/// 是否在 GetEndlessStatusRsp.NextStatusLists 里挂上其它深渊。
	///
	/// v2 原来这个字段是空的，客户端「挑战」面板就只画得出 CurStatus 那一张卡。
	/// 实测：把 CurStatus 切成 Openworld(2) 后，点 Task_3 反而不打开任何页面了
	/// ——说明面板卡片是跟着这个列表走的，不是跟着 CurStatus 单值走的。
	/// 这里把它填上，才有机会让迪拉克之海 / 量子奇点出卡片。
	/// </summary>
	public static bool ShowOtherEndless = true;

	/// <summary>
	/// GetOpenworldEndlessDataRsp.GroupLevel —— 旧版深渊（迪拉克之海 / 量子奇点）
	/// 下发给客户端的「分组等级」。
	///
	/// 为什么必须单独开一个字段，而不是沿用 User.AbyssGroupLevel
	/// --------------------------------------------------------
	/// 客户端的 OWEndlessGroup 配置表是按 (GroupLevel, EndlessType) 二元组查行的，
	/// 实测 7.0 客户端里这张表的有效行非常少（用 GetOWEndlessGroupMetaByKey 逐级扫出来的）：
	///     mode=2 迪拉克之海 : 21, 22, 23, 24
	///     mode=3 量子奇点   : 11, 12, 13, 14, 21, 22, 23, 24
	///     mode=4 超弦空间   : 这张表里没有行（超弦走 UltraEndless* 另一套表）
	///
	/// 只要下发的 GroupLevel 不在这张表里，客户端就会在这里炸：
	///     MonoOWEndlessUIManager.RefreshPlayerGroup + 0x404
	///         ebx  = GroupInfo._GroupLevel              (GroupInfo+0x24)
	///         meta = GetOWEndlessGroupMetaByKey(ebx)    (内部 = (ebx, GetActiveMode()))
	///         if (meta == null) -> raise_null_reference_exception
	/// 视图装配失败 → BaseContext.OnViewSet 每帧重试 → 每帧重发 3419
	/// → 实测 120 次/秒的请求风暴 + 界面黑屏（仍可点击）。
	///
	/// 而 User.AbyssGroupLevel 建号时默认是 8（见 User.cs），8 不在上表里；
	/// 它同时被超弦空间的 UltraEndlessGetMainDataReqHand 使用，直接改会波及超弦。
	/// 所以旧深渊单独走这个字段。
	///
	/// 热切换（不用重启服务端，下次请求即生效）：
	///     abyss owlevel 24
	/// 或者直接写 run\abyss_ow.json（连聊天都不用，见 OwTuning）：
	///     { "group_level": 24, "cur_top_group_level": 24 }
	///
	/// 2026-10-25 17:48 事故：35 会把客户端打成黑屏
	/// ---------------------------------------------
	/// 曾把默认值改成 35（以为 31~35 是「原罪→无限」五档），结果客户端黑屏转圈，
	/// 服务端一秒内收到 100 次 GetOpenworldEndlessDataReq。
	/// 这是 MonoOWEndlessUIManager.RefreshPlayerGroup+0x404 拿到
	/// GetOWEndlessGroupMetaByKey(35) == null 后抛 NRE、视图每帧重试的表现。
	///
	/// 结论：**服务端 Resources 的 OWEndlessGroup.json ≠ 客户端那张加密表。**
	/// 服务端表里 Type=2 确实有 21~24 和 31~35 两条阶梯，
	/// 但客户端 mode=2 只认 21~24（早期逆向扫表的结论才是对的）。
	/// 所以默认值回到 21，想换档位用上面的调参文件试，出问题能立刻改回来。
	///
	/// 已知安全值：21 22 23 24
	/// 已知会崩：35
	/// </summary>
	public static uint OwGroupLevel = 21u;

	/// <summary>要额外挂到 NextStatusLists 的深渊类型。</summary>
	public static readonly EndlessType[] OtherEndless =
	{
		EndlessType.EndlessTypeOpenworld,   // 迪拉克之海
		EndlessType.EndlessTypeQuantum,     // 量子奇点
		EndlessType.EndlessTypeGreedy       // 贪婪深渊
	};

	/// <summary>按 SelectedEndlessType 推导 CurStatus 该是什么，保持两者自洽。</summary>
	public static void SetSelected(uint selected)
	{
		SelectedEndlessType = selected;
		CurStatusType = MapStatus(selected);
		PersistSelected(selected);
	}

	private static EndlessType MapStatus(uint selected)
	{
		return selected switch
		{
			1u => EndlessType.EndlessTypeNormal,
			2u => EndlessType.EndlessTypeOpenworld,
			3u => EndlessType.EndlessTypeQuantum,
			4u => EndlessType.EndlessTypeUltra,
			5u => EndlessType.EndlessTypeGreedy,
			6u => EndlessType.EndlessTypeFront,
			_ => EndlessType.EndlessTypeNone
		};
	}

	// ---------------------------------------------------------------- 持久化

	private static readonly JsonSerializerOptions Pretty = new()
	{
		WriteIndented = true,
		TypeInfoResolver = new System.Text.Json.Serialization.Metadata.DefaultJsonTypeInfoResolver()
	};

	private static string ConfigPath =>
		Path.Combine(Directory.GetCurrentDirectory(), "abyss_ow.json");

	/// <summary>只认 2/3/4（迪拉克/量子/超弦），其它值一律回默认 2。</summary>
	private static uint LoadSelected()
	{
		try
		{
			if (!File.Exists(ConfigPath))
			{
				return 2u;
			}
			JsonNode node = JsonNode.Parse(File.ReadAllText(ConfigPath));
			JsonNode? raw = node?["selected_type"];
			if (raw == null)
			{
				return 2u;
			}
			uint v = raw.GetValue<uint>();
			return v >= 2u && v <= 4u ? v : 2u;
		}
		catch
		{
			// 文件坏了不影响起服，用默认值；下次切换会覆盖回正常内容。
			return 2u;
		}
	}

	/// <summary>
	/// 把当前选择写回 abyss_ow.json（保留文件里其它键）。
	/// 客户端每切一次深渊类型都会走到这里，频率很低，直接整文件重写即可。
	/// </summary>
	private static void PersistSelected(uint selected)
	{
		try
		{
			JsonObject obj = File.Exists(ConfigPath)
				? JsonNode.Parse(File.ReadAllText(ConfigPath)) as JsonObject ?? new JsonObject()
				: new JsonObject();
			obj["selected_type"] = selected;
			File.WriteAllText(ConfigPath, obj.ToJsonString(Pretty), new UTF8Encoding(false));
		}
		catch
		{
			// 写盘失败只影响「重启后记不住」，不该打断协议应答。
		}
	}

	public static string Describe()
	{
		return $"SelectedEndlessType={SelectedEndlessType} CurStatus.EndlessType={(int)CurStatusType} OwGroupLevel={OwGroupLevel}";
	}
}
