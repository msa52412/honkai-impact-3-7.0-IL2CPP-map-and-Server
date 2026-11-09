using System;
using System.IO;
using Newtonsoft.Json;

namespace PemukulPaku.GameServer;

/// <summary>
/// 家园小队（Sanctuary）的运行期调参，**改文件即生效，不用重启服务端**。
/// 写法照抄 <see cref="UltraTuning"/>：读 run\sanctuary.json，按文件 mtime 重载。
///
/// 为什么需要
/// ----------
/// 家园小队的等级 / 经验在 7.0 客户端里是**服务端算好直接下发**的
/// （SanctuaryActivityData.level / .exp），客户端没有「经验 → 等级」的配置表
/// （ExcelMeta 里搜不到任何 Sanctuary 类，Resources 下也只有
///  SanctuaryPlayerGroup / SanctuaryStageConfig 两张有数据的表，
///  SanctuaryBuildingLevel / SanctuaryBuildingActivity 都是 2 字节空数组）。
/// 所以经验曲线只能由服务端自己定义，这里把曲线和权重全做成可调的。
///
/// 文件：run\sanctuary.json（不存在就用下面代码里的默认值，服务端照常启动）
/// {
///   "level": null,                  // 非 null = 强制等级，跳过一切计算
///   "exp": null,                    // 非 null = 强制当前等级内经验
///   "max_level": 20,                // 等级上限（TextMap 里 Asrama Lv. 只到 20）
///   "exp_per_avatar_level": 8,      // 每个女武神每级折算多少经验
///   "exp_per_avatar_star": 300,     // 每个女武神每星折算多少经验
///   "exp_scale": 0.08,              // 总经验缩放，想「压慢进度」就调小
///   "has_take_reward_level": null   // null = 跟随 level（等级奖励表本版不存在）
/// }
///
/// 关于 exp_scale 的默认值 0.08
/// ---------------------------
/// 一个「练满」的号（88 个女武神、等级合计 7040、星级合计 440）原始经验是 188320，
/// 按 1.0 倍直接顶到 20 级满级 —— 满级之后等级就再也不动了，没法验证升级链路。
/// 0.08 倍 = 15065 经验 = **12 级（1815 / 3800）**，留出成长空间；
/// 想让满编号直接满级就把 exp_scale 写成 1.0（改文件即生效，不用重启）。
///
/// 官方机制（TextMap 362360405 / 362360652 实证）：
///   「Squad Asrama 的等级通过**提升女武神的等级和阶级**、
///     或参加 Growth Trial 来提升；提升女武神星级和阶级可以获得 Squad EXP」
/// 所以默认实现就是按女武神阵容折算，阵容越强小队等级越高。
/// </summary>
internal static class SanctuaryTuning
{
	private const string FILE_NAME = "sanctuary.json";

	/// <summary>exp_scale 的默认值。见类注释「关于 exp_scale 的默认值」。</summary>
	private const double DefaultExpScale = 0.08;

	private class Dto
	{
		/// <summary>强制等级。null = 按女武神阵容折算。</summary>
		[JsonProperty("level")]
		public uint? Level { get; set; }

		/// <summary>强制当前等级内的经验。null = 按折算结果。</summary>
		[JsonProperty("exp")]
		public uint? Exp { get; set; }

		/// <summary>等级上限。0 = 用代码默认值 20。</summary>
		[JsonProperty("max_level")]
		public uint MaxLevel { get; set; }

		/// <summary>每个女武神每级折算的经验。0 = 用代码默认值。</summary>
		[JsonProperty("exp_per_avatar_level")]
		public uint ExpPerAvatarLevel { get; set; }

		/// <summary>每个女武神每星折算的经验。0 = 用代码默认值。</summary>
		[JsonProperty("exp_per_avatar_star")]
		public uint ExpPerAvatarStar { get; set; }

		/// <summary>总经验缩放系数。0 或负数 = 用默认值 1.0。</summary>
		[JsonProperty("exp_scale")]
		public double ExpScale { get; set; }

		/// <summary>已领取等级奖励水位线。null = 跟随 level。</summary>
		[JsonProperty("has_take_reward_level")]
		public uint? HasTakeRewardLevel { get; set; }
	}

	private static DateTime _stamp = DateTime.MinValue;
	private static Dto _dto = new Dto();

	public static uint? Level => Load().Level;

	public static uint? Exp => Load().Exp;

	public static uint MaxLevel
	{
		get
		{
			uint v = Load().MaxLevel;
			return v == 0u ? 20u : v;
		}
	}

	public static uint ExpPerAvatarLevel
	{
		get
		{
			uint v = Load().ExpPerAvatarLevel;
			return v == 0u ? 8u : v;
		}
	}

	public static uint ExpPerAvatarStar
	{
		get
		{
			uint v = Load().ExpPerAvatarStar;
			return v == 0u ? 300u : v;
		}
	}

	public static double ExpScale
	{
		get
		{
			double v = Load().ExpScale;
			return v <= 0.0 ? DefaultExpScale : v;
		}
	}

	public static uint? HasTakeRewardLevel => Load().HasTakeRewardLevel;

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

			Packet.c.Log("[Sanctuary] 读取 " + FILE_NAME + " -> "
				+ "level=" + (_dto.Level.HasValue ? _dto.Level.Value.ToString() : "(按阵容折算)")
				+ " exp=" + (_dto.Exp.HasValue ? _dto.Exp.Value.ToString() : "(按折算结果)")
				+ $" max_level={MaxLevel}"
				+ $" per_level={ExpPerAvatarLevel} per_star={ExpPerAvatarStar}"
				+ $" scale={ExpScale}"
				+ " has_take_reward_level=" + (_dto.HasTakeRewardLevel.HasValue ? _dto.HasTakeRewardLevel.Value.ToString() : "(跟随等级)"));
		}
		catch (Exception ex)
		{
			// 文件写坏了就当没配，绝不让调参文件把服务端带崩。
			_dto = new Dto();
			_stamp = DateTime.MinValue;
			Packet.c.Warn("[Sanctuary] 读取 " + FILE_NAME + " 失败，忽略：" + ex.Message);
		}
		return _dto;
	}
}
