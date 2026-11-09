using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 主界面天气。
///
/// 客户端从 ConstValuesClient 里拿到的地址是
///   https://sg-public-api.mihoyo.com/game_weather/weather/get_weather?auth_appid=game_weather...
/// 走代理后落到本机，由 ConfigController 处理。回包结构：
///   { retcode, message, data: { timezone, hourly: [ {condition, date, hour, temp} × N ] } }
/// 其中 condition 就是客户端的 WeatherType：
///   0=None 1=Sunny 2=Cloudy 3=HeavyCloudy 4=Rainy 5=Snowy 6=Windy 7=Lightning 8=Extreme
///
/// 两种模式（GM 面板切换，落 run\weather.json，按 mtime 热加载）：
///   lock = 固定天气，24 小时全用同一个 condition
///   auto = 按「地区」或「本机 IP」查真实天气，逐小时映射成 condition
///
/// 外部接口都免 key：
///   IP 定位   http://ip-api.com/json/?lang=zh-CN
///   地区→坐标 https://geocoding-api.open-meteo.com/v1/search?name=xxx
///   逐小时天气 https://api.open-meteo.com/v1/forecast?hourly=temperature_2m,weather_code
/// </summary>
public static class WeatherTuning
{
	public sealed class Settings
	{
		/// <summary>lock / auto</summary>
		public string Mode = "lock";
		/// <summary>lock 模式用的 WeatherType（0~8）</summary>
		public int Condition = 1;
		/// <summary>auto 模式的地区名（中文/英文都行，留空则按 IP）</summary>
		public string Region = "";
		/// <summary>auto 模式：是否自动识别本机 IP</summary>
		public bool AutoIp = true;
		/// <summary>lock 模式的温度</summary>
		public int Temp = 25;
		/// <summary>auto 模式的结果缓存秒数</summary>
		public int CacheSeconds = 600;
	}

	/// <summary>一次取数的结果，给 GM 面板显示用。</summary>
	public sealed class Probe
	{
		public bool Ok;
		public string Error = "";
		public string Place = "";
		public int Condition;
		public int Temp;
		public string Source = "";
		public DateTime At = DateTime.Now;
	}

	private static readonly object Lock = new object();
	private static Settings _settings = new Settings();
	private static DateTime _stamp = DateTime.MinValue;
	private static DateTime _lastFetch = DateTime.MinValue;
	private static List<(DateTime time, int condition, int temp)> _cache = new List<(DateTime, int, int)>();
	private static Probe _lastProbe = new Probe();

	private static readonly HttpClient Http = CreateClient();

	private static HttpClient CreateClient()
	{
		HttpClientHandler handler = new HttpClientHandler
		{
			// 机器上跑着 mitmproxy，别让服务端自己绕进去
			UseProxy = false,
			AllowAutoRedirect = true
		};
		HttpClient c = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(6) };
		c.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "BH3-GM/1.0");
		return c;
	}

	private static string ConfigPath => Path.Combine(Directory.GetCurrentDirectory(), "weather.json");

	// ------------------------------------------------------------------ 配置

	public static Settings Current()
	{
		lock (Lock)
		{
			try
			{
				string path = ConfigPath;
				if (File.Exists(path))
				{
					DateTime stamp = File.GetLastWriteTimeUtc(path);
					if (stamp != _stamp)
					{
						JsonNode node = JsonNode.Parse(File.ReadAllText(path));
						Settings s = new Settings();
						if (node is JsonObject o)
						{
							s.Mode = (o["mode"]?.GetValue<string>() ?? "lock").Trim().ToLowerInvariant();
							s.Condition = o["condition"]?.GetValue<int>() ?? 1;
							s.Region = (o["region"]?.GetValue<string>() ?? "").Trim();
							s.AutoIp = o["auto_ip"]?.GetValue<bool>() ?? true;
							s.Temp = o["temp"]?.GetValue<int>() ?? 25;
							s.CacheSeconds = o["cache_seconds"]?.GetValue<int>() ?? 600;
						}
						if (s.Mode != "auto")
						{
							s.Mode = "lock";
						}
						s.Condition = Math.Clamp(s.Condition, 0, 8);
						if (s.CacheSeconds < 0)
						{
							s.CacheSeconds = 0;
						}
						_settings = s;
						_stamp = stamp;
						_cache = new List<(DateTime, int, int)>();
					}
				}
			}
			catch (Exception ex)
			{
				Log("读取 weather.json 失败：" + ex.Message);
			}
			return _settings;
		}
	}

	public static void Save(Settings s)
	{
		lock (Lock)
		{
			JsonObject o = new JsonObject
			{
				["mode"] = s.Mode == "auto" ? "auto" : "lock",
				["condition"] = Math.Clamp(s.Condition, 0, 8),
				["region"] = s.Region ?? "",
				["auto_ip"] = s.AutoIp,
				["temp"] = s.Temp,
				["cache_seconds"] = s.CacheSeconds
			};
			File.WriteAllText(ConfigPath, o.ToJsonString(new JsonSerializerOptions
				{
					WriteIndented = true,
					// 不转义中文，文件里能直接看到「上海」而不是 \u4E0A\u6D77
					Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
				}),
				new System.Text.UTF8Encoding(false));
			_stamp = DateTime.MinValue;   // 下次 Current() 重读
			_cache = new List<(DateTime, int, int)>();
		}
	}

	public static Probe LastProbe()
	{
		lock (Lock)
		{
			return _lastProbe;
		}
	}

	// ------------------------------------------------------------------ 对外：生成 24 小时

	/// <summary>给客户端用的 24 条逐小时数据（从现在这一小时开始）。</summary>
	public static List<(int condition, string date, int hour, int temp)> BuildHourly()
	{
		Settings s = Current();
		DateTime start = DateTime.Now.Date.AddHours(DateTime.Now.Hour);

		List<(DateTime time, int condition, int temp)> rows = s.Mode == "auto"
			? AutoRows(s)
			: null;

		List<(int, string, int, int)> list = new List<(int, string, int, int)>();
		for (int i = 0; i < 24; i++)
		{
			DateTime t = start.AddHours(i);
			int condition = s.Condition;
			int temp = s.Temp;
			if (rows != null)
			{
				var hit = rows.FirstOrDefault(r => r.time == t);
				if (hit.time == t)
				{
					condition = hit.condition;
					temp = hit.temp;
				}
			}
			list.Add((condition, t.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), t.Hour, temp));
		}
		// 客户端只在登录 / weatherInfoPeriod 到期时才来拉，所以这条日志能对上是哪一次生效的
		Log(string.Format("get_weather -> mode={0} cond={1}{2} temp={3} 起点 {4} {5}时（共 {6} 条）",
			s.Mode,
			list[0].Item1,
			rows == null && s.Mode == "auto" ? "(取数失败,回退固定值)" : "",
			list[0].Item4,
			list[0].Item2,
			list[0].Item3,
			list.Count));
		return list;
	}

	/// <summary>GM 面板「测试」用：立刻查一次，返回取到的地点/天气/温度。</summary>
	public static Probe Refresh()
	{
		Settings s = Current();
		if (s.Mode != "auto")
		{
			lock (Lock)
			{
				_lastProbe = new Probe
				{
					Ok = true,
					Source = "lock",
					Place = "固定",
					Condition = s.Condition,
					Temp = s.Temp,
					At = DateTime.Now
				};
				return _lastProbe;
			}
		}
		lock (Lock)
		{
			_cache = new List<(DateTime, int, int)>();
			_lastFetch = DateTime.MinValue;
		}
		List<(DateTime time, int condition, int temp)> rows = AutoRows(s, force: true);
		return LastProbe();
	}

	// ------------------------------------------------------------------ auto 取数

	private static List<(DateTime time, int condition, int temp)> AutoRows(Settings s, bool force = false)
	{
		lock (Lock)
		{
			if (!force && _cache.Count > 0
				&& (DateTime.Now - _lastFetch).TotalSeconds < s.CacheSeconds)
			{
				return _cache;
			}
		}

		try
		{
			(double lat, double lon, string place) = ResolvePlace(s);
			List<(DateTime time, int condition, int temp)> rows = FetchForecast(lat, lon);
			if (rows.Count == 0)
			{
				throw new Exception("天气接口没有返回逐小时数据");
			}
			lock (Lock)
			{
				_cache = rows;
				_lastFetch = DateTime.Now;
				// 面板上要显示「现在」这一小时的天气，不是当天 0 点那条
				DateTime nowHour = DateTime.Now.Date.AddHours(DateTime.Now.Hour);
				var cur = rows.FirstOrDefault(r => r.time == nowHour);
				bool hitNow = cur.time == nowHour;
				_lastProbe = new Probe
				{
					Ok = true,
					Place = place,
					Condition = hitNow ? cur.condition : rows[0].condition,
					Temp = hitNow ? cur.temp : rows[0].temp,
					Source = string.Format(CultureInfo.InvariantCulture, "{0:F3},{1:F3}", lat, lon),
					At = DateTime.Now
				};
			}
			Log(string.Format("auto 天气：{0}（{1:F2},{2:F2}）当前 {3} / {4}°C",
				place, lat, lon, ConditionName(LastProbe().Condition), LastProbe().Temp));
			return rows;
		}
		catch (Exception ex)
		{
			lock (Lock)
			{
				_lastProbe = new Probe
				{
					Ok = false,
					Error = ex.Message,
					Condition = s.Condition,
					Temp = s.Temp,
					Source = "fallback",
					At = DateTime.Now
				};
			}
			Log("auto 天气取数失败，回退固定值：" + ex.Message);
			return null;
		}
	}

	private static (double lat, double lon, string place) ResolvePlace(Settings s)
	{
		if (s.AutoIp || string.IsNullOrWhiteSpace(s.Region))
		{
			return ResolveByIp();
		}
		try
		{
			return ResolveByRegion(s.Region);
		}
		catch (Exception ex)
		{
			// 地区名打错/查不到时不要整条链断掉，退回按 IP 定位
			Log("地区「" + s.Region + "」定位失败，改用本机 IP：" + ex.Message);
			return ResolveByIp();
		}
	}

	private static (double lat, double lon, string place) ResolveByIp()
	{
		string json = Http.GetStringAsync("http://ip-api.com/json/?lang=zh-CN&fields=status,message,city,regionName,country,lat,lon").GetAwaiter().GetResult();
		JsonNode node = JsonNode.Parse(json);
		string status = node?["status"]?.GetValue<string>() ?? "";
		if (status != "success")
		{
			throw new Exception("IP 定位失败：" + (node?["message"]?.GetValue<string>() ?? "unknown"));
		}
		double lat = node["lat"]!.GetValue<double>();
		double lon = node["lon"]!.GetValue<double>();
		string city = node["city"]?.GetValue<string>() ?? "";
		string region = node["regionName"]?.GetValue<string>() ?? "";
		string country = node["country"]?.GetValue<string>() ?? "";
		string place = string.IsNullOrEmpty(city) ? region : (string.IsNullOrEmpty(region) ? city : region + "·" + city);
		if (!string.IsNullOrEmpty(country))
		{
			place = country + " " + place;
		}
		return (lat, lon, place.Trim());
	}

	private static (double lat, double lon, string place) ResolveByRegion(string region)
	{
		string url = "https://geocoding-api.open-meteo.com/v1/search?count=1&language=zh&format=json&name="
			+ Uri.EscapeDataString(region);
		string gjson = Http.GetStringAsync(url).GetAwaiter().GetResult();
		JsonNode g = JsonNode.Parse(gjson);
		JsonArray results = g?["results"] as JsonArray;
		if (results == null || results.Count == 0)
		{
			throw new Exception("找不到地区「" + region + "」");
		}
		JsonNode first = results[0];
		double glat = first["latitude"]!.GetValue<double>();
		double glon = first["longitude"]!.GetValue<double>();
		string gname = first["name"]?.GetValue<string>() ?? region;
		string admin = first["admin1"]?.GetValue<string>() ?? "";
		string ctry = first["country"]?.GetValue<string>() ?? "";
		return (glat, glon, (ctry + " " + admin + "·" + gname).Trim());
	}

	private static List<(DateTime time, int condition, int temp)> FetchForecast(double lat, double lon)
	{
		string url = string.Format(CultureInfo.InvariantCulture,
			"https://api.open-meteo.com/v1/forecast?latitude={0:F4}&longitude={1:F4}"
			+ "&hourly=temperature_2m,weather_code&forecast_days=3&timezone=auto", lat, lon);
		string json = Http.GetStringAsync(url).GetAwaiter().GetResult();
		JsonNode node = JsonNode.Parse(json);
		JsonArray times = node?["hourly"]?["time"] as JsonArray;
		JsonArray codes = node?["hourly"]?["weather_code"] as JsonArray;
		JsonArray temps = node?["hourly"]?["temperature_2m"] as JsonArray;
		if (times == null || codes == null || temps == null)
		{
			throw new Exception("天气接口返回格式不对");
		}

		List<(DateTime, int, int)> rows = new List<(DateTime, int, int)>();
		for (int i = 0; i < times.Count; i++)
		{
			// time 形如 2026-10-31T13:00（timezone=auto，已是当地时间）
			if (!DateTime.TryParse(times[i]?.GetValue<string>(), CultureInfo.InvariantCulture,
				DateTimeStyles.None, out DateTime t))
			{
				continue;
			}
			int code = codes[i]?.GetValue<int>() ?? 0;
			double temp = temps[i]?.GetValue<double>() ?? 0;
			rows.Add((t, MapWmo(code), (int)Math.Round(temp)));
		}
		return rows;
	}

	/// <summary>WMO 天气码 -> 客户端 WeatherType。</summary>
	private static int MapWmo(int code)
	{
		switch (code)
		{
		case 0:
		case 1:
			return 1;   // Sunny
		case 2:
			return 2;   // Cloudy
		case 3:
			return 3;   // HeavyCloudy
		case 45:
		case 48:
			return 2;   // 雾 -> 多云
		case 51: case 53: case 55: case 56: case 57:
		case 61: case 63: case 65: case 66: case 67:
		case 80: case 81: case 82:
			return 4;   // Rainy
		case 71: case 73: case 75: case 77:
		case 85: case 86:
			return 5;   // Snowy
		case 95: case 96: case 99:
			return 7;   // Lightning
		default:
			return 2;
		}
	}

	public static string ConditionName(int c)
	{
		switch (c)
		{
		case 0: return "None";
		case 1: return "晴";
		case 2: return "多云";
		case 3: return "阴";
		case 4: return "雨";
		case 5: return "雪";
		case 6: return "风";
		case 7: return "雷";
		case 8: return "极端";
		default: return "?" + c;
		}
	}

	private static void Log(string message)
	{
		Common.Utils.Logger c = new Common.Utils.Logger("Weather", ConsoleColor.DarkCyan, traceOnError: false);
		c.Log(message);
	}
}
