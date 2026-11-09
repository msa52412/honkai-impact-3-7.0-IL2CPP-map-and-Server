using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using PemukulPaku.GameServer.Game;

namespace HttpServer.Controllers
{
	/// <summary>
	/// GM 面板的「天气」控制 —— 主界面天气（不是爬塔那个天气）。
	///
	///   GET  /gm/api/weather           查当前模式 + 上次取数结果
	///   POST /gm/api/weather           {mode, condition, region, auto_ip, temp}
	///
	/// 两种模式（下拉切换）：
	///   锁定 = 固定一个 WeatherType，24 小时全一样
	///   自动 = 填地区（留空则按本机 IP），服务端请求真实天气接口后映射给客户端
	///
	/// 落盘是 run\weather.json，WeatherTuning 按 mtime 热加载，改完下一次客户端请求就生效。
	/// 客户端那边要重进主界面（或切一次场景）才会重新拉天气。
	/// </summary>
	public static class GmWeatherController
	{
		private sealed class SaveRequest
		{
			public string mode { get; set; }
			public int condition { get; set; }
			public string region { get; set; }
			public bool auto_ip { get; set; }
			public int temp { get; set; }
		}

		public static void AddHandlers(WebApplication app)
		{
			app.MapGet("/gm/api/weather", (HttpContext ctx) => Guard(ctx, () => GetState(ctx)));
			app.MapPost("/gm/api/weather", (HttpContext ctx) => Guard(ctx, () => Save(ctx)));
		}

		private static async Task Guard(HttpContext ctx, Func<Task> body)
		{
			try
			{
				await body();
			}
			catch (Exception ex)
			{
				Common.Utils.Logger c = new Common.Utils.Logger("GM", ConsoleColor.Magenta, traceOnError: false);
				c.Warn("天气接口异常：" + ex);
				if (!ctx.Response.HasStarted)
				{
					ctx.Response.StatusCode = 200;
					await ctx.Response.WriteAsJsonAsync(new
					{
						ok = false,
						output = "内部错误：" + ex.GetType().Name + " - " + ex.Message
					});
				}
			}
		}

		private static async Task GetState(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			WeatherTuning.Settings s = WeatherTuning.Current();
			WeatherTuning.Probe p = WeatherTuning.LastProbe();
			await ctx.Response.WriteAsJsonAsync(new
			{
				ok = true,
				mode = s.Mode,
				condition = s.Condition,
				conditionName = WeatherTuning.ConditionName(s.Condition),
				region = s.Region,
				autoIp = s.AutoIp,
				temp = s.Temp,
				cacheSeconds = s.CacheSeconds,
				last = new
				{
					ok = p.Ok,
					error = p.Error,
					place = p.Place,
					condition = p.Condition,
					conditionName = WeatherTuning.ConditionName(p.Condition),
					temp = p.Temp,
					source = p.Source,
					at = p.At.ToString("HH:mm:ss")
				}
			});
		}

		private static async Task Save(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			SaveRequest req;
			try
			{
				req = await ctx.Request.ReadFromJsonAsync<SaveRequest>();
			}
			catch (Exception ex)
			{
				await ctx.Response.WriteAsJsonAsync(new { ok = false, output = "请求体不是合法 JSON：" + ex.Message });
				return;
			}

			WeatherTuning.Settings old = WeatherTuning.Current();
			WeatherTuning.Settings s = new WeatherTuning.Settings
			{
				Mode = string.Equals(req?.mode, "auto", StringComparison.OrdinalIgnoreCase) ? "auto" : "lock",
				Condition = Math.Clamp(req?.condition ?? 1, 0, 8),
				Region = (req?.region ?? "").Trim(),
				AutoIp = req?.auto_ip ?? true,
				Temp = req?.temp ?? old.Temp,
				CacheSeconds = old.CacheSeconds
			};
			WeatherTuning.Save(s);

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			if (s.Mode == "lock")
			{
				sb.AppendLine("已保存：锁定模式，天气 = " + WeatherTuning.ConditionName(s.Condition)
					+ "，温度 " + s.Temp + "°C（24 小时固定）");
				// 客户端主界面的天气场景读 WarshipThemeData.WeatherIdx，
				// 不是读这个 HTTP 接口（实测它几乎不主动拉），所以这里直接改舰桥设置
				uint wid = Common.Database.WarshipSettings.WeatherIdOf(s.Condition);
				Common.Database.WarshipSettings.ApplyWeatherToAll(true, wid);
				sb.AppendLine("已同步到舰桥天气：WeatherIdx=" + wid + "（IsWeatherFixed=true）");
			}
			else
			{
				sb.AppendLine("已保存：自动模式，"
					+ (s.AutoIp || string.IsNullOrEmpty(s.Region) ? "按本机 IP 定位" : "地区 = " + s.Region));
				// 立刻查一次，把结果回给面板，省得再点一次
				WeatherTuning.Probe p = WeatherTuning.Refresh();
				if (p.Ok)
				{
					sb.AppendLine("取数成功：" + p.Place + " · " + WeatherTuning.ConditionName(p.Condition)
						+ " · " + p.Temp + "°C（坐标 " + p.Source + "）");
					uint wid = Common.Database.WarshipSettings.WeatherIdOf(p.Condition);
					Common.Database.WarshipSettings.ApplyWeatherToAll(true, wid);
					sb.AppendLine("已同步到舰桥天气：WeatherIdx=" + wid + "（IsWeatherFixed=true）");
				}
				else
				{
					sb.AppendLine("取数失败，已回退固定值：" + p.Error);
				}
			}
			sb.AppendLine("已写入 weather.json。");
			sb.AppendLine("客户端主界面天气读的是舰桥设置（WeatherIdx），改完要回标题重登一次才看得到。");

			// 登录那几秒还会闪一下旧天气：客户端在 PlayerPrefs 里缓存了
			// `USD_V2_<uid>_LastWeatherInfo_h2106018179`（Unity JsonUtility 序列化的
			// {"temperature":..,"weatherType":"Extreme",..}），启动时先用它渲染，
			// 等 GetWarshipDataRsp 到了才切。这里把这个缓存一并刷掉，避免闪雷。
			int probeCond = s.Mode == "lock" ? s.Condition : WeatherTuning.LastProbe().Condition;
			int probeTemp = s.Mode == "lock" ? s.Temp : WeatherTuning.LastProbe().Temp;
			string cacheNote = SyncClientCache(probeCond, probeTemp);
			if (cacheNote.Length > 0)
			{
				sb.AppendLine(cacheNote);
			}

			await ctx.Response.WriteAsJsonAsync(new { ok = true, output = sb.ToString() });
		}

		/// <summary>
		/// 刷客户端本地的天气缓存（PlayerPrefs，落在注册表
		/// `HKCU\Software\miHoYo\Honkai Impact 3` 下的 `USD_V2_&lt;uid&gt;_LastWeatherInfo_*`）。
		///
		/// 登录时客户端先用这份缓存渲染主界面，等 GetWarshipDataRsp 到了才切，
		/// 所以缓存不刷的话每次登录都会先闪几秒旧天气（之前一直是 Extreme = 雷）。
		///
		/// 只对跑服务端的这台机器有效 —— 私服场景够了。失败不影响主流程。
		/// </summary>
		private static string SyncClientCache(int weatherType, int temp)
		{
			try
			{
				using Microsoft.Win32.RegistryKey? root =
					Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\miHoYo\Honkai Impact 3", true);
				if (root == null)
				{
					return "";
				}
				string name = WeatherTypeName(weatherType);
				string json = "{\"temperature\":" + temp + ".0,\"weatherType\":\"" + name
					+ "\",\"extraInfo\":null,\"infoTime\":\""
					+ DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffffffzzz")
					+ "\"}";
				byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);

				int n = 0;
				foreach (string key in root.GetValueNames())
				{
					if (key.Contains("_LastWeatherInfo_"))
					{
						root.SetValue(key, bytes, Microsoft.Win32.RegistryValueKind.Binary);
						n++;
					}
				}
				return n > 0
					? "已刷新客户端本地天气缓存 " + n + " 处（weatherType=" + name + "），登录不再闪旧天气。"
					: "";
			}
			catch (Exception ex)
			{
				return "刷新客户端本地缓存失败（不影响使用）：" + ex.Message;
			}
		}

		/// <summary>condition(0~8) -> 客户端 WeatherType 的枚举名（Unity 序列化用的是名字）。</summary>
		private static string WeatherTypeName(int c)
		{
			switch (c)
			{
				case 0: return "None";
				case 1: return "Sunny";
				case 2: return "Cloudy";
				case 3: return "HeavyCloudy";
				case 4: return "Rainy";
				case 5: return "Snowy";
				case 6: return "Windy";
				case 7: return "Lightning";
				case 8: return "Extreme";
				default: return "Sunny";
			}
		}
	}
}
