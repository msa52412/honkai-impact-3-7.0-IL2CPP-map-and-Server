using System;
using System.IO;
using Common;
using Newtonsoft.Json;

namespace PemukulPaku.GameServer;

/// <summary>
/// 开放世界（Open World）地图状态调参，**改文件即生效，不用重启服务端**。
///
/// 为什么需要
/// ----------
/// 2026-10-28：开放世界选择页 4 张卡（樱色轮回 / 天命总部 / 后崩坏书 / 后崩坏书第二章）
/// 全部进不去 —— 天命总部显示锁图标，另外 3 张显示「未激活」，
/// 底部「权限激活」「进入」两个按钮都是灰的。
///
/// 原因是服务端两个响应把 status 写死成 3：
///   GetNewOpenworldRsp.map_list[].status = 3   （OpenworldMapStatusLock）
///   GetOpenworldMapRsp.status            = 3
/// 而客户端枚举顺序是
///   0 NONE / 1 ACTIVATE / 2 AUTO_ACTIVATE / 3 LOCK
/// 写死 3 就是「永久锁定」，所以怎么点都进不去。
///
/// 另外 4504 ActivateOpenworldMapReq（页面底部「权限激活」按钮）服务端原来**没有 handler**，
/// 点了也不会回包，客户端只能干等 → 按钮一直不可用。
///
/// 文件：run\openworld.json
/// {
///   "map_status": 1,             // 0 = 沿用旧行为（写死 3=LOCK）；1=已激活 2=可权限激活 3=锁定
///   "extra_map_ids": [],         // 追加进 map_list 的 mapId（客户端表里有、但服务端没给的图）
///   "next_refresh_seconds": 0,   // 0 = 默认 259200（3 天）
///   "close_seconds": 0           // 0 = 默认 259200（3 天）
/// }
/// </summary>
internal static class OwMapTuning
{
	private const string FILE_NAME = "openworld.json";

	private class Dto
	{
		/// <summary>
		/// 下发给客户端的 OpenworldMapStatus。
		/// 0 = 不接管，保持旧行为（两个 handler 里原来的 3u）。
		/// </summary>
		[JsonProperty("map_status")]
		public uint MapStatus { get; set; }

		[JsonProperty("extra_map_ids")]
		public uint[]? ExtraMapIds { get; set; }

		[JsonProperty("next_refresh_seconds")]
		public uint NextRefreshSeconds { get; set; }

		[JsonProperty("close_seconds")]
		public uint CloseSeconds { get; set; }

		/// <summary>
		/// true = 开放世界「周期完成奖励」视为已领取。
		///
		/// 客户端 GetOpenworldMapRsp.has_take_finish_reward_cycle 和 Cycle 比较，
		/// Cycle 更大就弹「奖励领取」弹窗。存档里这个字段初始是 0，
		/// 而 Cycle 从 OpenWorldCycleData.GetInitCycle 起算（≥1），
		/// 所以每次打开地图都会弹一次。
		/// 打开时把它拉平到当前 Cycle 即可，默认 true。
		/// </summary>
		[JsonProperty("auto_take_cycle_reward")]
		public bool? AutoTakeCycleReward { get; set; }

		/// <summary>
		/// 开放世界剧情是否全部视为「已通过」。null / 缺省 = true。
		/// 天命总部（map 2）的 OpenWorldMap.UnlockStoryId = 308（樱色轮回 3-8），
		/// 客户端 IsMapUnlock 要求 IsStoryFinish(308) 为真，不发就会一直锁。
		/// </summary>
		[JsonProperty("all_story_done")]
		public bool? AllStoryDone { get; set; }

		/// <summary>
		/// 开放世界「冒险等级」（OpenworldQuestData.level / GetNewOpenworldRsp.quest_level）。
		/// 0 / 缺省 = 30（表里 OpenWorldQuestLevelData 最高就是 30）。
		/// </summary>
		[JsonProperty("quest_level")]
		public uint QuestLevel { get; set; }

		/// <summary>
		/// 冒险等级对应的累计星数。0 / 缺省 = 按表算满级所需（141 星）。
		/// </summary>
		[JsonProperty("quest_star")]
		public uint QuestStar { get; set; }

		/// <summary>
		/// 后崩坏书「调查等级」（GetDLCRsp.level）。0 / 缺省 = DLCLevelData 表内最高（30）。
		/// </summary>
		[JsonProperty("dlc_level")]
		public uint DlcLevel { get; set; }

		/// <summary>
		/// 后崩坏书角色天赋等级的强制值。0 / 缺省 = 每个天赋都取自己的满级
		/// （DLCTalentLevelData 里的 max(TalentLevel)：属性 1~4、必杀 1、分支 3、武器 8）。
		/// 填 1 可以退回「所有天赋 1 级」看升级过程。
		/// </summary>
		[JsonProperty("dlc_talent_level")]
		public uint DlcTalentLevel { get; set; }

		/// <summary>
		/// 后崩坏书「词条套装」id。DLCTalentAffixSet 只有 1~5（HP/SP/破盾/抗性/护甲）。
		/// 0 / 缺省 = 1。客户端按「类型 4 天赋里同 affix_set_id 的件数」算套装档位，
		/// 4 件同套装即满档，所以默认全给 1 就能看到套装效果。
		/// </summary>
		[JsonProperty("dlc_affix_set")]
		public uint DlcAffixSet { get; set; }

		/// <summary>
		/// 后崩坏书 2（ScDLC / DLC2）的调查等级。0 / 缺省 = 30（ScDLCLevel.json 最高就是 30）。
		/// 对应 GetScDLCRsp.fever.level 和每个角色的 level。
		/// </summary>
		[JsonProperty("scdlc_level")]
		public uint ScDlcLevel { get; set; }

		/// <summary>
		/// 后崩坏书 2 的「狂热」等级（GetScDLCRsp.fever.level）。0 / 缺省 = 1。
		/// </summary>
		[JsonProperty("scdlc_fever_level")]
		public uint ScDlcFeverLevel { get; set; }

		/// <summary>
		/// 开放世界区域实体状态（传送点 / 宝箱 / 门）的强制值。0 = 原样回写客户端上报的值。
		///
		/// 客户端 ReportOpenworldAreaEntityStateReq 上报的 entity_state 是它自己的状态机，
		/// 后崩坏书 2 地图页的传送点用 DLC2PointState（Locked = 1 / Unlocked = 2 / Hide = 3）。
		/// 实测客户端报上来的全是 1，回写 1 时地图上锚点全是灰的；写 2 才能点。
		/// </summary>
		[JsonProperty("force_entity_state")]
		public uint ForceEntityState { get; set; }

		/// <summary>
		/// true（缺省）= 按 OpenWorldArea.switchPoint 把地图的切换点补进 spawn_point，
		/// 否则没去过的区域永远进不去（地图页上「部分区域没开放」）。
		/// </summary>
		[JsonProperty("fill_spawn_points")]
		public bool? FillSpawnPoints { get; set; }
	}

	private static DateTime _stamp = DateTime.MinValue;
	private static Dto _dto = new Dto();

	/// <summary>
	/// 真正的下发值。0 表示「没配」→ 回落到 1（ACTIVATE）：
	/// 默认就要让地图能进，否则这个功能等于没修。
	/// 想复现问题就把文件里的 map_status 写成 3。
	/// </summary>
	public static uint Status
	{
		get
		{
			uint v = Load().MapStatus;
			return v == 0u ? 1u : v;
		}
	}

	public static uint[] ExtraMapIds => Load().ExtraMapIds ?? Array.Empty<uint>();

	/// <summary>
	/// true（缺省）= GetOpenworldStoryRsp 直接把整张 OpenWorldStoryData 的 StoryID 全量下发成
	/// finish_story_id_list，客户端会为每条建 KFOBMMKEFFA(storyId, 2)（state = Finished），
	/// IsStoryFinish 才会返回 true，地图解锁 / 剧情相关门禁才会全通过。
	/// </summary>
	public static bool AllStoryDone => Load().AllStoryDone ?? true;

	/// <summary>true = 周期完成奖励视为已领取，打开地图不再弹「奖励领取」。</summary>
	public static bool AutoTakeCycleReward => Load().AutoTakeCycleReward ?? true;

	/// <summary>后崩坏书 2 角色等级。0 / 缺省 = 用 ScDLCAvatarLevel 表里的最大等级（90）。</summary>
	public static uint ScDlcLevel => Load().ScDlcLevel;

	/// <summary>区域实体状态的强制值。0 = 原样回写客户端上报的值。</summary>
	public static uint ForceEntityState => Load().ForceEntityState;

	/// <summary>true（缺省）= 按配置把切换点补进 spawn_point。</summary>
	public static bool FillSpawnPoints => Load().FillSpawnPoints ?? true;

	/// <summary>后崩坏书 2 狂热等级。0 / 缺省 = 用表里的最大门槛（30）。</summary>
	public static uint ScDlcFeverLevel => Load().ScDlcFeverLevel;

	/// <summary>
	/// 冒险等级目标值。0 / 缺省 = 30（委托等级表满级）。
	/// 客户端 NewOpenWorldModule.GetCurrentQuestLevel（0x183655E10）直接读 rsp 缓存里的
	/// quest_level，不会自己按星数反算，所以这个值就是界面上显示的数字。
	/// </summary>
	public static uint QuestLevel
	{
		get
		{
			uint v = Load().QuestLevel;
			return v == 0u ? 30u : v;
		}
	}

	/// <summary>冒险等级对应的累计星数。0 / 缺省 = 由 OwQuestService.StarForLevel 按表算。</summary>
	public static uint QuestStar => Load().QuestStar;

	/// <summary>后崩坏书调查等级。0 / 缺省 = 30（DLCLevelData 满级）。</summary>
	public static uint DlcLevel
	{
		get
		{
			uint v = Load().DlcLevel;
			return v == 0u ? 30u : v;
		}
	}

	/// <summary>后崩坏书天赋的强制等级。0 / 缺省 = 由 DlcService 按表取每个天赋的满级。</summary>
	public static uint DlcTalentLevel => Load().DlcTalentLevel;

	/// <summary>后崩坏书词条套装 id。0 / 缺省 = 1。</summary>
	public static uint DlcAffixSet
	{
		get
		{
			uint v = Load().DlcAffixSet;
			return v == 0u ? 1u : v;
		}
	}

	public static uint NextRefreshSeconds
	{
		get
		{
			uint v = Load().NextRefreshSeconds;
			return v == 0u ? 259200u : v;
		}
	}

	public static uint CloseSeconds
	{
		get
		{
			uint v = Load().CloseSeconds;
			return v == 0u ? 259200u : v;
		}
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
			_dto = JsonConvert.DeserializeObject<Dto>(File.ReadAllText(path)) ?? new Dto();
			_stamp = stamp;
			Packet.c.Log("[OW] 读取 " + FILE_NAME + " -> "
				+ $"map_status={_dto.MapStatus}(生效 {Status}) "
				+ $"extra=[{string.Join(",", ExtraMapIds)}] "
				+ $"nextRefresh={NextRefreshSeconds} close={CloseSeconds} "
				+ $"allStoryDone={AllStoryDone} questLevel={QuestLevel} questStar={QuestStar} "
				+ $"dlcLevel={DlcLevel} dlcTalentLevel={DlcTalentLevel} dlcAffixSet={DlcAffixSet} "
				+ $"autoTakeCycleReward={AutoTakeCycleReward}");
		}
		catch (Exception ex)
		{
			// 调参文件写坏了就当没配，绝不因为配置让服务端抛异常。
			_dto = new Dto();
			_stamp = DateTime.MinValue;
			Packet.c.Warn("[OW] 读取 " + FILE_NAME + " 失败，忽略：" + ex.Message);
		}
		return _dto;
	}
}
