using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HttpServer.Controllers
{
	/// <summary>
	/// GM 面板的「查看全部配置」—— 把配置表渲染成网页。
	///
	///   GET /gm/config/dirac            迪拉克之海天气boss配置表.xlsx
	///   GET /gm/config/quantum          量子奇点_配置表_分区域.xlsx（按区域，BOSS 全部候选）
	///   GET /gm/config/ultra            超弦空间_天气boss配置表.xlsx
	///   GET /gm/config/dlc              后崩坏书1 爬塔（DLCTowerSchedule/Weather/Buff）
	///   GET /gm/config/scdlc            后崩坏书2 爬塔（ScDLCTowerSchedule/Weather/Buff）
	///   GET /gm/config/{key}/xlsx       下载原始 xlsx（只有深渊那三张有）
	///
	/// 渲染不在这里做：C# 没有读 xlsx 的库，也不该为了一张查表引入 EPPlus/ClosedXML。
	/// 交给 tools\abyss_config_html.py（**纯标准库**，zipfile + xml.etree + json），
	/// 用项目自带的内嵌 python 跑，目标机不用装任何东西。
	///
	/// 每次请求先比 mtime：任一源文件比 html 新（或 html 不存在）就同步重跑一次生成器，
	/// 所以改完表刷新页面就是新的。生成器失败时保留旧页面 + 顶部加一条警告，
	/// 不让「看不了表」变成「页面 500」。
	/// </summary>
	public static class GmConfigController
	{
		private sealed class Source
		{
			/// <summary>相对项目根的源文件（任一改动都触发重建）。</summary>
			public string[] Files;

			/// <summary>xlsx 文件名；null = 这个页面不提供「下载原表」。</summary>
			public string Download;
		}

		/// <summary>★ key 和 tools\abyss_config_html.py 的 SPECS 必须一致。</summary>
		private static readonly Dictionary<string, Source> Sources = new(StringComparer.OrdinalIgnoreCase)
		{
			["dirac"] = new Source
			{
				Files = new[] { @"docs\迪拉克之海天气boss配置表.xlsx" },
				Download = "迪拉克之海天气boss配置表.xlsx"
			},
			["quantum"] = new Source
			{
				Files = new[] { @"docs\量子奇点_配置表_分区域.xlsx" },
				Download = "量子奇点_配置表_分区域.xlsx"
			},
			["ultra"] = new Source
			{
				Files = new[] { @"docs\超弦空间_天气boss配置表.xlsx" },
				Download = "超弦空间_天气boss配置表.xlsx"
			},
			["dlc"] = new Source
			{
				Files = new[]
				{
					@"server-v2\run\Resources\ExcelOutputAsset\DLCTowerSchedule.json",
					@"server-v2\run\Resources\ExcelOutputAsset\DLCTowerWeather.json",
					@"server-v2\run\Resources\ExcelOutputAsset\DLCTowerBuff.json"
				},
				Download = null
			},
			["scdlc"] = new Source
			{
				Files = new[]
				{
					@"server-v2\run\Resources\ExcelOutputAsset\ScDLCTowerSchedule.json",
					@"server-v2\run\Resources\ExcelOutputAsset\ScDLCTowerWeather.json",
					@"server-v2\run\Resources\ExcelOutputAsset\ScDLCTowerBuff.json"
				},
				Download = null
			}
		};

		private static readonly object BuildLock = new object();

		public static void AddHandlers(WebApplication app)
		{
			app.MapGet("/gm/config/{key}", (HttpContext ctx, string key) => ServePage(ctx, key));
			app.MapGet("/gm/config/{key}/xlsx", (HttpContext ctx, string key) => ServeXlsx(ctx, key));
		}

		// ---------------------------------------------------------------- 路径

		/// <summary>run\ -> 项目根（run 的上一级是 server-v2，再上一级是项目根）。</summary>
		private static string ProjectRoot =>
			Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", ".."));

		private static string FullPathOf(string relative) =>
			Path.Combine(ProjectRoot, relative.Replace('\\', Path.DirectorySeparatorChar));

		/// <summary>
		/// 源文件不一定就在 ProjectRoot 下：docs 现在在项目根（ProjectRoot 的上一级），
		/// 而 server-v2\run\Resources 在 ProjectRoot 里。两处都找一遍，别再因为搬家整页打不开。
		/// </summary>
		private static string Resolve(string relative)
		{
			string[] roots =
			{
				ProjectRoot,
				Path.GetFullPath(Path.Combine(ProjectRoot, ".."))
			};
			foreach (string root in roots)
			{
				string path = Path.Combine(root, relative.Replace('\\', Path.DirectorySeparatorChar));
				if (File.Exists(path))
				{
					return path;
				}
			}
			return FullPathOf(relative);
		}

		private static string HtmlPath(string key) =>
			Path.Combine(Directory.GetCurrentDirectory(), "webui", "config", key + ".html");

		private static string GeneratorPath => Path.Combine(ProjectRoot, "tools", "abyss_config_html.py");

		private static string PythonPath => Path.Combine(ProjectRoot, "tools", "python", "python.exe");

		// ---------------------------------------------------------------- 页面

		private static async Task ServePage(HttpContext ctx, string key)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }
			if (!Sources.ContainsKey(key)) { await NotFound(ctx, key); return; }

			string note = null;
			if (NeedRebuild(key))
			{
				note = Rebuild(key);
			}

			string htmlPath = HtmlPath(key);
			if (!File.Exists(htmlPath))
			{
				await ctx.Response.WriteAsync(ErrorPage(key, note ?? "页面还没生成"));
				return;
			}

			string body;
			try
			{
				body = File.ReadAllText(htmlPath, Encoding.UTF8);
			}
			catch (Exception ex)
			{
				await ctx.Response.WriteAsync(ErrorPage(key, "读取页面失败：" + ex.Message));
				return;
			}

			// 生成失败但旧页面还在：顶上挂一条黄条，别让人以为看到的是新数据
			if (!string.IsNullOrEmpty(note))
			{
				body = body.Replace("<body>",
					"<body><div style=\"background:#2a2113;border-bottom:1px solid #4a3a1a;"
					+ "color:#ffb020;padding:8px 20px;font:12.5px 'Microsoft YaHei',sans-serif\">"
					+ "⚠ 配置表可能不是最新：" + Escape(note) + "（下面是上一次生成的结果）</div>", StringComparison.Ordinal);
			}

			ctx.Response.ContentType = "text/html; charset=utf-8";
			await ctx.Response.WriteAsync(body);
		}

		private static async Task NotFound(HttpContext ctx, string key)
		{
			ctx.Response.ContentType = "text/html; charset=utf-8";
			await ctx.Response.WriteAsync(ErrorPage(key,
				"未知的配置表：" + key + "（支持 " + string.Join(" / ", Sources.Keys) + "）"));
		}

		private static async Task ServeXlsx(HttpContext ctx, string key)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }
			if (!Sources.TryGetValue(key, out Source src) || src.Download == null)
			{
				ctx.Response.ContentType = "text/html; charset=utf-8";
				await ctx.Response.WriteAsync(ErrorPage(key, "这张表没有原始 xlsx（爬塔表来自游戏配置 json）"));
				return;
			}

			string path = Resolve(src.Files[0]);
			if (!File.Exists(path))
			{
				ctx.Response.ContentType = "text/html; charset=utf-8";
				await ctx.Response.WriteAsync(ErrorPage(key, "源文件不存在：" + path));
				return;
			}

			ctx.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			// 文件名是中文，必须用 RFC 5987 的 filename*；filename 给个 ASCII 兜底
			ctx.Response.Headers["Content-Disposition"] =
				"attachment; filename=\"" + key + ".xlsx\"; filename*=UTF-8''"
				+ Uri.EscapeDataString(src.Download);
			await ctx.Response.SendFileAsync(path);
		}

		// ---------------------------------------------------------------- 重建

		private static bool NeedRebuild(string key)
		{
			string html = HtmlPath(key);
			if (!File.Exists(html))
			{
				return true;
			}
			DateTime htmlTime = File.GetLastWriteTimeUtc(html);
			foreach (string rel in Sources[key].Files)
			{
				string path = Resolve(rel);
				if (File.Exists(path) && File.GetLastWriteTimeUtc(path) > htmlTime)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>返回 null 表示成功，否则是给用户看的失败原因。</summary>
		private static string Rebuild(string key)
		{
			lock (BuildLock)
			{
				// 并发进来的第二个请求，等锁的时候前一个可能已经建好了
				if (!NeedRebuild(key))
				{
					return null;
				}
				if (!File.Exists(GeneratorPath))
				{
					return "找不到生成器 " + GeneratorPath;
				}
				if (!File.Exists(PythonPath))
				{
					return "找不到内嵌 python " + PythonPath;
				}

				try
				{
					ProcessStartInfo psi = new ProcessStartInfo
					{
						FileName = PythonPath,
						WorkingDirectory = ProjectRoot,
						UseShellExecute = false,
						RedirectStandardOutput = true,
						RedirectStandardError = true,
						CreateNoWindow = true,
						StandardOutputEncoding = Encoding.UTF8,
						StandardErrorEncoding = Encoding.UTF8
					};
					psi.ArgumentList.Add(GeneratorPath);
					psi.ArgumentList.Add("--only");
					psi.ArgumentList.Add(key);
					psi.Environment["PYTHONIOENCODING"] = "utf-8";

					using Process p = Process.Start(psi);
					if (p == null)
					{
						return "生成器进程启动失败";
					}
					string stdout = p.StandardOutput.ReadToEnd();
					string stderr = p.StandardError.ReadToEnd();
					if (!p.WaitForExit(30000))
					{
						try { p.Kill(true); } catch { /* 忽略 */ }
						return "生成器 30 秒超时";
					}
					if (p.ExitCode != 0)
					{
						string detail = string.IsNullOrWhiteSpace(stderr) ? stdout : stderr;
						return "生成器退出码 " + p.ExitCode + "：" + Shorten(detail, 400);
					}

					Common.Utils.Logger c = new Common.Utils.Logger("GM", ConsoleColor.Magenta, traceOnError: false);
					c.Log("[配置表] 已重新生成 " + key + "：" + Shorten(stdout.Trim(), 200));
					return null;
				}
				catch (Exception ex)
				{
					return "调用生成器异常：" + ex.Message;
				}
			}
		}

		// ---------------------------------------------------------------- 工具

		private static string Shorten(string s, int max)
		{
			if (string.IsNullOrEmpty(s))
			{
				return "";
			}
			s = s.Replace("\r", " ").Replace("\n", " ").Trim();
			return s.Length <= max ? s : s.Substring(0, max) + "…";
		}

		private static string Escape(string s) =>
			s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");

		private static string ErrorPage(string key, string message)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<!DOCTYPE html><html lang=\"zh-CN\"><head><meta charset=\"utf-8\">");
			sb.Append("<title>配置表 ").Append(Escape(key)).Append("</title><style>");
			sb.Append("body{background:#12141a;color:#e6e9ef;font:14px/1.7 'Microsoft YaHei',sans-serif;");
			sb.Append("margin:0;padding:40px}h2{color:#ffb020;font-size:16px}");
			sb.Append("code{background:#20242e;padding:2px 6px;border-radius:4px;color:#ffd479}");
			sb.Append("a{color:#4c9aff}</style></head><body>");
			sb.Append("<h2>配置表打不开</h2><p>").Append(Escape(message)).Append("</p>");
			sb.Append("<p>排查：<br>1. 确认源文件存在：<code>");
			if (Sources.TryGetValue(key, out Source src))
			{
				sb.Append(Escape(string.Join(" / ", src.Files)));
			}
			sb.Append("</code><br>2. 手动跑一次 <code>tools\\python\\python.exe tools\\abyss_config_html.py --only ");
			sb.Append(Escape(key)).Append("</code> 看报什么错</p>");
			sb.Append("<p><a href=\"/gm\">返回控制台</a></p></body></html>");
			return sb.ToString();
		}
	}
}
