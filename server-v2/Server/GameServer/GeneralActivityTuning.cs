using System;
using System.IO;
using Common;
using Newtonsoft.Json;

namespace PemukulPaku.GameServer;

/// <summary>
/// 通用活动（GeneralActivity）调参。
///
/// 文件：run\generalactivity.json（不存在就用代码里的默认值，服务端照常启动）
/// {
///   "open_all": true,        // true = 全部活动都下发「已开启」的排期（默认）
///   "activity_ids": [],      // open_all=false 时，只给这些活动下发排期
///   "window_days": 30        // 排期窗口：now-1 天 ~ now+N 天
/// }
///
/// 为什么需要：客户端 `GeneralActivityGetScheduleRsp` 是活动开关的**唯一**依据，
/// 服务端原来没这个 handler（日志 `GeneralActivityGetScheduleReq not handled!`），
/// 客户端一个排期都拿不到 → 主界面的「小队联机」等入口全部显示「未开启」。
/// </summary>
internal static class GeneralActivityTuning
{
	private const string FILE_NAME = "generalactivity.json";

	private class Dto
	{
		[JsonProperty("open_all")]
		public bool OpenAll { get; set; } = true;

		[JsonProperty("activity_ids")]
		public uint[] ActivityIds { get; set; } = Array.Empty<uint>();

		/// <summary>排期起始日期（本地时间，yyyy-MM-dd）。缺省 2020-01-01。</summary>
		[JsonProperty("begin_date")]
		public string BeginDate { get; set; }

		/// <summary>排期结束日期（本地时间，yyyy-MM-dd）。缺省 2099-12-31。</summary>
		[JsonProperty("end_date")]
		public string EndDate { get; set; }

		/// <summary>
		/// 周期内开放窗口的结束偏移（秒）。缺省 604800 = 整周。
		/// 客户端按周循环算「准备中 / 进行中」，这个值是 0 就永远不开。
		/// </summary>
		[JsonProperty("day_window_seconds")]
		public uint DayWindowSeconds { get; set; }

		/// <summary>
		/// 只给「等级区间包含当前玩家」的活动下发排期。缺省 true。
		///
		/// GeneralActivity.json 里有 135 个活动的 MaxLevel 只有 55/69/70（旧版本内容），
		/// 满级号拿到它们的排期就会在活动页多出一堆「未开启 / 已结束」的卡。
		/// </summary>
		[JsonProperty("filter_by_level")]
		public bool FilterByLevel { get; set; } = true;
	}

	private static DateTime _stamp = DateTime.MinValue;
	private static Dto _dto = new Dto();

	public static bool OpenAll => Load().OpenAll;

	public static uint[] ActivityIds => Load().ActivityIds ?? Array.Empty<uint>();

	/// <summary>是否按等级区间过滤活动排期。默认 true。</summary>
	public static bool FilterByLevel => Load().FilterByLevel;

	/// <summary>排期起始 unix 秒。默认 2020-01-01 00:00:00。</summary>
	public static uint BeginTime => ToUnix(Load().BeginDate, new DateTime(2020, 1, 1));

	/// <summary>排期结束 unix 秒。默认 2099-12-31 00:00:00。</summary>
	public static uint EndTime => ToUnix(Load().EndDate, new DateTime(2099, 12, 31));

	/// <summary>周期内开放窗口的结束偏移（秒）。默认 604800 = 整周。</summary>
	public static uint DayWindowSeconds
	{
		get
		{
			uint v = Load().DayWindowSeconds;
			return v == 0u ? 604800u : v;
		}
	}

	private static uint ToUnix(string text, DateTime fallback)
	{
		DateTime value = fallback;
		if (!string.IsNullOrEmpty(text))
		{
			if (DateTime.TryParse(text, out DateTime parsed))
			{
				value = parsed;
			}
		}
		long seconds = (long)(value - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local)).TotalSeconds;
		if (seconds < 0)
		{
			seconds = 0;
		}
		return (uint)seconds;
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
			Packet.c.Log("[GeneralActivity] 读取 " + FILE_NAME + " -> "
				+ $"open_all={_dto.OpenAll} ids=[{string.Join(",", ActivityIds)}] "
				+ $"窗口 {BeginTime} ~ {EndTime}");
		}
		catch (Exception ex)
		{
			Packet.c.Log("[GeneralActivity] 读取配置失败，用默认值：" + ex.Message);
			_dto = new Dto();
		}
		return _dto;
	}
}
