using System;
using System.IO;
using Common;
using Newtonsoft.Json;

namespace PemukulPaku.GameServer;

/// <summary>
/// 后崩坏书爬塔（无限塔）的调参，**改文件即生效，不用重启服务端**。
///
/// 为什么天气要走这里
/// ------------------
/// 爬塔的天气**不是服务端下发的** —— GetDLCTowerRsp / GetScDLCTowerRsp 里根本没有天气字段。
/// 客户端拿到 rsp.schedule_id 之后自己去查
///   DLCTowerSchedule.Get(schedule_id).Weather   （第一章）
///   ScDLCTowerSchedule.Get(schedule_id).Weather （第二章）
/// 所以「换天气」本质上就是「换一个 Weather 对得上的赛季」，服务端只要把
/// schedule_id 换成那一期就行，不用碰客户端表。
///
/// 副作用（要提前知道）
/// --------------------
/// 赛季行是「天气 + ConfigType + MaxFloor」绑在一起的，换天气必然连带换
/// ConfigType（决定 DLCTowerFloor 查哪一组楼层）和总层数。
/// 而且 schedule_id 一变，DlcTower.GetOrCreate 会按新赛季重置进度 ——
/// 这正是「切换配置」该有的行为，但会让当前进度清零。
///
/// 文件：run\tower.json
/// {
///   "dlc_weather": 0,        // 第一章爬塔天气 ID（1001/1002/1003/1005）；0 = 不接管，按时间选赛季
///   "scdlc_weather": 0,      // 第二章爬塔天气 ID（1001~1008）；0 = 不接管
///   "dlc_schedule_id": 0,    // 直接指定赛季 ID，非 0 时优先级高于天气
///   "scdlc_schedule_id": 0
/// }
///
/// 指定天气时选「离现在最近的那一期」：已经命中就原样用，否则取 |开始时间-现在| 最小的。
/// </summary>
internal static class TowerTuning
{
	private const string FILE_NAME = "tower.json";

	private class Dto
	{
		[JsonProperty("dlc_weather")]
		public uint DlcWeather { get; set; }

		[JsonProperty("scdlc_weather")]
		public uint ScDlcWeather { get; set; }

		[JsonProperty("dlc_schedule_id")]
		public uint DlcScheduleId { get; set; }

		[JsonProperty("scdlc_schedule_id")]
		public uint ScDlcScheduleId { get; set; }
	}

	private static DateTime _stamp = DateTime.MinValue;
	private static Dto _dto = new Dto();

	/// <summary>第一章爬塔天气 ID。0 = 不接管。</summary>
	public static uint DlcWeather => Load().DlcWeather;

	/// <summary>第二章爬塔天气 ID。0 = 不接管。</summary>
	public static uint ScDlcWeather => Load().ScDlcWeather;

	/// <summary>第一章爬塔强制赛季 ID。0 = 不指定。</summary>
	public static uint DlcScheduleId => Load().DlcScheduleId;

	/// <summary>第二章爬塔强制赛季 ID。0 = 不指定。</summary>
	public static uint ScDlcScheduleId => Load().ScDlcScheduleId;

	/// <summary>当前文件里的值，给 GM 面板回显用。</summary>
	public static (uint weather, uint scheduleId) Dlc => (Load().DlcWeather, Load().DlcScheduleId);

	public static (uint weather, uint scheduleId) ScDlc => (Load().ScDlcWeather, Load().ScDlcScheduleId);

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
			Packet.c.Log("[Tower] 读取 " + FILE_NAME + " -> "
				+ $"dlcWeather={_dto.DlcWeather} dlcSchedule={_dto.DlcScheduleId} "
				+ $"scdlcWeather={_dto.ScDlcWeather} scdlcSchedule={_dto.ScDlcScheduleId}");
		}
		catch (Exception ex)
		{
			// 调参文件写坏了就当没配，绝不因为配置让服务端抛异常。
			_dto = new Dto();
			_stamp = DateTime.MinValue;
			Packet.c.Warn("[Tower] 读取 " + FILE_NAME + " 失败，忽略：" + ex.Message);
		}
		return _dto;
	}
}
