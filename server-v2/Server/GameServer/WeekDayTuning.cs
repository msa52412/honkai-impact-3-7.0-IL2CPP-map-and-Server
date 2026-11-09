using System;
using System.IO;
using Common;
using Newtonsoft.Json;

namespace PemukulPaku.GameServer;

/// <summary>
/// 「材料远征 / 曜日」（WeekDayActivity）调参。
///
/// 文件：run\weekday.json（不存在就用默认值）
/// {
///   "activity_ids": [1001,...,1301],   // 要下发的活动（1001~1005 周一到周五、1006 周末、1301 另一组）
///   "begin_date": "2020-01-01",
///   "end_date": "2099-12-31",
///   "force_max_enter_times": true      // true = 次数给满（否则读表里的 maxEnterTimes）
/// }
/// </summary>
internal static class WeekDayTuning
{
	private const string FILE_NAME = "weekday.json";

	private static readonly uint[] DefaultIds = { 1001u, 1002u, 1003u, 1004u, 1005u, 1006u, 1301u };

	private class Dto
	{
		[JsonProperty("activity_ids")]
		public uint[] ActivityIds { get; set; }

		[JsonProperty("begin_date")]
		public string BeginDate { get; set; }

		[JsonProperty("end_date")]
		public string EndDate { get; set; }

		[JsonProperty("force_max_enter_times")]
		public bool? ForceMaxEnterTimes { get; set; }
	}

	private static DateTime _stamp = DateTime.MinValue;
	private static Dto _dto = new Dto();

	public static uint[] ActivityIds
	{
		get
		{
			uint[] ids = Load().ActivityIds;
			return ids != null && ids.Length > 0 ? ids : DefaultIds;
		}
	}

	public static bool ForceMaxEnterTimes => Load().ForceMaxEnterTimes ?? true;

	public static uint BeginTime => ToUnix(Load().BeginDate, new DateTime(2020, 1, 1));

	public static uint EndTime => ToUnix(Load().EndDate, new DateTime(2099, 12, 31));

	private static uint ToUnix(string text, DateTime fallback)
	{
		DateTime value = fallback;
		if (!string.IsNullOrEmpty(text) && DateTime.TryParse(text, out DateTime parsed))
		{
			value = parsed;
		}
		long seconds = (long)(value - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local)).TotalSeconds;
		return seconds < 0 ? 0u : (uint)seconds;
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
			Packet.c.Log("[WeekDay] 读取 " + FILE_NAME + " -> "
				+ $"ids=[{string.Join(",", ActivityIds)}] 窗口 {BeginTime} ~ {EndTime} "
				+ $"forceMaxEnter={ForceMaxEnterTimes}");
		}
		catch (Exception ex)
		{
			Packet.c.Log("[WeekDay] 读取配置失败，用默认值：" + ex.Message);
			_dto = new Dto();
		}
		return _dto;
	}
}
