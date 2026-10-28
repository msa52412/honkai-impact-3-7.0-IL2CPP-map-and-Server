using System;
using System.IO;
using Common;
using Newtonsoft.Json;

namespace PemukulPaku.GameServer;

/// <summary>
/// 超弦空间（UltraEndless）的运行期调参，**改文件即生效，不用重启服务端**。
///
/// 为什么需要
/// ----------
/// 「换一套超弦环境」实际只有三个变量：
///   ① 组号（= SiteID / 10）      决定 5203 下发哪 4 个站点 → 地图 + 天气 + BOSS
///   ② 段位 GroupLevel（1..9）    决定段位名 / 杯数区间
///   ③ 扰动 DynamicHardLevel      决定关卡难度系数
/// 原来 ① 写死在 UltraEndlessGetMainDataReqHand.SiteGroups，
/// 每换一次就要改代码 + 重新编译 + 重启，调试体验很差。
/// 这里照抄 OwTuning 的做法：读 run\ultra_endless.json，按文件 mtime 重载。
///
/// 文件：run\ultra_endless.json
/// {
///   "site_groups": [110],
///   "schedule_id": 0,
///   "group_level": 0,
///   "dynamic_hard_level": null
/// }
///
/// 各键语义
/// --------
/// site_groups        组号数组（SiteID/10 的那个商）。空/省略 = 用代码里的默认值。
///                    多个组号时：地图只画排最前那一组，滚动列表全部显示（见下方说明）。
/// schedule_id        0/省略 = 用 UltraEndlessState.DefaultScheduleId（改期才需要动）。
///                    非 0 时 5203 下发、5206 记分、换期清分三处统一以文件为准，
///                    改文件就等于换期（玩家下次进界面/上报时分数自动清零）。
///                    注意客户端内部用 (ushort) 截断，合法区间 0..65535。
/// group_level        0/省略 = 用存档里的 User.AbyssGroupLevel（非法时兜底 8 红莲）。
/// dynamic_hard_level null/省略 = 用存档里的 User.AbyssDynamicHard。
///                    写成数字（哪怕 0）就强制覆盖下发的扰动值，不写存档。
///
/// 两条渲染路径（实证，决定了 site_groups 该怎么填）
/// ------------------------------------------------
///   1) MonoUltraEndlessZoneMapCmpt.SetupSites (0x181F53A50)
///      遍历 zone 下 Area1..Area4 子节点，每个各调一次
///      UltraEndlessModule.GetSiteIDByNodeName (0x1838640B0) 反查 site_list。
///      GetSiteIDByNodeName 是严格 first-match：按 site_list 顺序遍历，
///      System::String::Equals 命中立即 return，查不到返回 0 → 该槽 SetActive(false)。
///      → 所以地图 4 个槽永远来自 site_list 里排最前的那一组，
///        后面几组只能出现在滚动列表里。
///
///   2) MonoUltraEndlessZoneMapCmpt.SetupScrollerSites (0x181F53770)
///      拿 List.Count 当滚动项数量，按下标逐个渲染，**全部**站点都出现。
///
/// 结论：想让地图真的换成目标环境，site_groups 只写一个组号最省事。
/// </summary>
internal static class UltraTuning
{
	private const string FILE_NAME = "ultra_endless.json";

	private class Dto
	{
		/// <summary>组号（SiteID / 10）。空 = 用代码默认值。</summary>
		[JsonProperty("site_groups")]
		public int[] SiteGroups { get; set; }

		/// <summary>排期号。0 = 用默认。</summary>
		[JsonProperty("schedule_id")]
		public uint ScheduleId { get; set; }

		/// <summary>段位。0 = 用存档。</summary>
		[JsonProperty("group_level")]
		public uint GroupLevel { get; set; }

		/// <summary>扰动。null = 用存档；写数字则强制覆盖。</summary>
		[JsonProperty("dynamic_hard_level")]
		public uint? DynamicHardLevel { get; set; }
	}

	private static DateTime _stamp = DateTime.MinValue;
	private static Dto _dto = new Dto();

	/// <summary>组号数组；文件没配就返回 null，调用方回退到代码默认值。</summary>
	public static int[] SiteGroups
	{
		get
		{
			Dto d = Load();
			return (d.SiteGroups != null && d.SiteGroups.Length > 0) ? d.SiteGroups : null;
		}
	}

	/// <summary>排期号；0 表示未设置，用默认。</summary>
	public static uint ScheduleId => Load().ScheduleId;

	/// <summary>段位覆盖；0 表示未设置，用存档。</summary>
	public static uint GroupLevel => Load().GroupLevel;

	/// <summary>扰动覆盖；null 表示未设置，用存档。</summary>
	public static uint? DynamicHardLevel => Load().DynamicHardLevel;

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

			string groups = (_dto.SiteGroups == null || _dto.SiteGroups.Length == 0)
				? "(默认)"
				: string.Join(",", _dto.SiteGroups);
			Packet.c.Log("[Ultra] 读取 " + FILE_NAME + " -> "
				+ $"site_groups={groups} schedule_id={_dto.ScheduleId} "
				+ $"group_level={_dto.GroupLevel} "
				+ $"dynamic_hard_level={(_dto.DynamicHardLevel.HasValue ? _dto.DynamicHardLevel.Value.ToString() : "(存档)")}");
		}
		catch (Exception ex)
		{
			// 文件写坏了就当没配，绝不让调参文件把服务端带崩。
			_dto = new Dto();
			_stamp = DateTime.MinValue;
			Packet.c.Warn("[Ultra] 读取 " + FILE_NAME + " 失败，忽略：" + ex.Message);
		}
		return _dto;
	}
}
