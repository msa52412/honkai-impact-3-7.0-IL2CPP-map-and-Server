using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PemukulPaku.GameServer;
using PemukulPaku.GameServer.Commands;

namespace HttpServer.Controllers
{
	/// <summary>
	/// GM 控制台（浏览器版）。
	///
	/// 为什么不做成 WinForms/WPF 窗口：服务端已经是 ASP.NET Core（8081 / 443），
	/// 加一个路由 + 一个静态页就能用，客户端机器上不用装任何运行时，
	/// 手机、平板、另一台电脑的浏览器都能开 —— 兼容性最好的「窗口」就是浏览器。
	///
	/// 路由：
	///   GET  /gm                 页面（从 run\webui\gm.html 读，改完刷新即可，不用重新编译）
	///   GET  /gm/api/commands    命令清单（来自 CommandFactory，和命令行 help 同一份）
	///   GET  /gm/api/sessions    在线会话
	///   POST /gm/api/exec        { "session": "ip:port", "line": "give gold 123" }
	///
	/// 执行逻辑和 Console 的 ReadLine 完全一致：按空格切分、按名字找 Command、
	/// Console/All 类型直接跑，Player 类型必须先选中会话。
	/// </summary>
	public static class GmController
	{
		/// <summary>同一时刻只允许一个 GM 请求改 Console.Out，否则输出会串。</summary>
		private static readonly object ExecLock = new object();

		private sealed class ExecRequest
		{
			public string session { get; set; }
			public string line { get; set; }
		}

		private sealed class HookRequest
		{
			public string name { get; set; }
		}

		/// <summary>gm_hooks.json 的条目：一条「外部命令」（.bat / .exe / python 脚本）</summary>
		public sealed class Hook
		{
			[JsonProperty("name")]
			public string Name { get; set; }

			[JsonProperty("desc")]
			public string Desc { get; set; }

			/// <summary>整条命令行，例如 `python tools\mem_scan.py 0x1234`</summary>
			[JsonProperty("cmd")]
			public string Cmd { get; set; }

			/// <summary>工作目录，相对服务端目录（run），默认 "."</summary>
			[JsonProperty("cwd")]
			public string Cwd { get; set; }

			/// <summary>超时秒数，默认 120</summary>
			[JsonProperty("timeout")]
			public int Timeout { get; set; }
		}

		private sealed class HooksFile
		{
			[JsonProperty("allow_remote")]
			public bool AllowRemote { get; set; }

			[JsonProperty("hooks")]
			public List<Hook> Hooks { get; set; }
		}

		private static HooksFile _hooks = new HooksFile { Hooks = new List<Hook>() };
		private static DateTime _hooksStamp = DateTime.MinValue;

		private static HooksFile LoadHooks()
		{
			string path = Path.Combine(Directory.GetCurrentDirectory(), "gm_hooks.json");
			try
			{
				if (!File.Exists(path))
				{
					return _hooks;
				}
				DateTime stamp = File.GetLastWriteTimeUtc(path);
				if (stamp == _hooksStamp)
				{
					return _hooks;
				}
				HooksFile parsed = JsonConvert.DeserializeObject<HooksFile>(File.ReadAllText(path));
				if (parsed != null)
				{
					parsed.Hooks ??= new List<Hook>();
					_hooks = parsed;
					_hooksStamp = stamp;
				}
			}
			catch (Exception ex)
			{
				PacketLog("读取 gm_hooks.json 失败：" + ex.Message);
			}
			return _hooks;
		}

		private static void PacketLog(string message)
		{
			Common.Utils.Logger c = new Common.Utils.Logger("GM", ConsoleColor.Magenta, traceOnError: false);
			c.Log(message);
		}

		public static void AddHandlers(WebApplication app)
		{
			// 注意：只能注册 "/gm" 一个。同时注册 "/gm/" 会让 "/gm" 命中两条路由，
			// 抛 AmbiguousMatchException，Kestrel 直接回 500 且响应体为空。
			app.MapGet("/gm", ServePage);
			app.MapGet("/gm/api/commands", ListCommands);
			app.MapGet("/gm/api/sessions", ListSessions);
			app.MapPost("/gm/api/exec", Exec);
			app.MapGet("/gm/api/hooks", ListHooks);
			app.MapPost("/gm/api/hook", RunHook);
		}

		/// <summary>
		/// GM 接口只允许本机访问。
		///
		/// Kestrel 监听的是 `http://*:8081`（全网卡），而 GM 能改玩家数据、还能拉起本机进程，
		/// 直接暴露在局域网上等于给同网段任何人一个后门。默认只放行回环地址，
		/// 确实需要远程访问时在 gm_hooks.json 里写 "allow_remote": true。
		/// </summary>
		internal static bool IsAllowed(HttpContext ctx)
		{
			if (LoadHooks().AllowRemote)
			{
				return true;
			}
			IPAddress ip = ctx.Connection.RemoteIpAddress;
			if (ip == null)
			{
				return true;
			}
			if (IPAddress.IsLoopback(ip))
			{
				return true;
			}
			return ip.IsIPv4MappedToIPv6 && IPAddress.IsLoopback(ip.MapToIPv4());
		}

		internal static async Task Deny(HttpContext ctx)
		{
			ctx.Response.StatusCode = 403;
			await ctx.Response.WriteAsJsonAsync(new
			{
				ok = false,
				output = "GM 控制台只允许本机访问（gm_hooks.json 里 allow_remote=true 可放开）"
			});
		}

		private static async Task ServePage(HttpContext ctx)
		{
			if (!IsAllowed(ctx)) { await Deny(ctx); return; }
			string path = Path.Combine(Directory.GetCurrentDirectory(), "webui", "gm.html");
			ctx.Response.ContentType = "text/html; charset=utf-8";
			try
			{
				if (!File.Exists(path))
				{
					await ctx.Response.WriteAsync(
						"<html><body style=\"background:#111;color:#eee;font:14px/1.6 monospace\">"
						+ "<h3>缺少页面文件</h3><p>把 gm.html 放到 <code>" + path + "</code></p></body></html>");
					return;
				}
				await ctx.Response.WriteAsync(await File.ReadAllTextAsync(path, Encoding.UTF8));
			}
			catch (Exception ex)
			{
				ctx.Response.ContentType = "text/plain; charset=utf-8";
				await ctx.Response.WriteAsync("读取页面失败：" + ex.Message);
			}
		}

		private static async Task ListCommands(HttpContext ctx)
		{
			if (!IsAllowed(ctx)) { await Deny(ctx); return; }
			var list = CommandFactory.Commands
				.OrderBy(c => c.CmdType)
				.ThenBy(c => c.Name)
				.Select(c => new
				{
					name = c.Name,
					desc = c.Description,
					examples = c.Examples ?? Array.Empty<string>(),
					type = c.CmdType.ToString()
				});

			await ctx.Response.WriteAsJsonAsync(new { ok = true, commands = list });
		}

		private static async Task ListSessions(HttpContext ctx)
		{
			if (!IsAllowed(ctx)) { await Deny(ctx); return; }
			List<object> items = new List<object>();
			foreach (KeyValuePair<string, Session> kv in PemukulPaku.GameServer.Server.GetInstance().Sessions)
			{
				string nick = string.Empty;
				uint uid = 0u;
				uint level = 0u;
				try
				{
					if (kv.Value.Player != null)
					{
						nick = kv.Value.Player.User.Nick ?? string.Empty;
						uid = kv.Value.Player.User.Uid;
						level = kv.Value.Player.GetDetailData().Level;
					}
				}
				catch
				{
					// 会话刚建立 / 刚断开时 Player 可能不完整，忽略即可
				}
				items.Add(new { id = kv.Key, sessionId = kv.Value.Id, uid, nick, level });
			}

			await ctx.Response.WriteAsJsonAsync(new { ok = true, sessions = items });
		}

		private static async Task Exec(HttpContext ctx)
		{
			if (!IsAllowed(ctx)) { await Deny(ctx); return; }
			ExecRequest req;
			try
			{
				req = await ctx.Request.ReadFromJsonAsync<ExecRequest>();
			}
			catch (Exception ex)
			{
				await ctx.Response.WriteAsJsonAsync(new { ok = false, output = "请求体不是合法 JSON：" + ex.Message });
				return;
			}

			string line = (req?.line ?? string.Empty).Trim();
			if (line.Length == 0)
			{
				await ctx.Response.WriteAsJsonAsync(new { ok = false, output = "命令为空" });
				return;
			}

			string output = Run(line, req.session);

			await ctx.Response.WriteAsJsonAsync(new { ok = true, output });
		}

		/// <summary>跑一条控制台命令，返回捕获到的输出（含异常信息）。</summary>
		private static string Run(string line, string sessionKey)
		{
			string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length == 0)
			{
				return string.Empty;
			}

			string name = parts[0].ToLowerInvariant();
			string[] args = parts.Skip(1).ToArray();

			Command cmd = CommandFactory.Commands.Find(c => c.Name.ToLowerInvariant() == name);
			if (cmd == null)
			{
				return "命令不存在：" + name + "（输入 help 看清单）";
			}

			Session session = null;
			if (!string.IsNullOrEmpty(sessionKey))
			{
				PemukulPaku.GameServer.Server.GetInstance().Sessions.TryGetValue(sessionKey, out session);
			}

			lock (ExecLock)
			{
				TextWriter old = Console.Out;
				StringWriter buffer = new StringWriter();
				Console.SetOut(buffer);
				try
				{
					Console.WriteLine("> " + line);
					if (cmd.CmdType == CommandType.All || cmd.CmdType == CommandType.Console)
					{
						cmd.Run(args);
					}
					else if (session != null)
					{
						cmd.Run(session, args);
					}
					else
					{
						Console.WriteLine("这个命令需要先选会话（target <id>，或在页面左侧点一个会话）");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("执行出错：" + ex.Message);
				}
				finally
				{
					Console.SetOut(old);
				}
				return buffer.ToString();
			}
		}

		/// <summary>
		/// 外部命令清单。来自 run\gm_hooks.json，改完刷新页面即可生效（按 mtime 热加载）。
		/// 那些「改内存的 python 脚本」就是挂在这里：服务端不碰 ctypes，
		/// 只负责按白名单把命令行拉起来、把 stdout/stderr 抓回来。
		/// </summary>
		private static async Task ListHooks(HttpContext ctx)
		{
			if (!IsAllowed(ctx)) { await Deny(ctx); return; }

			var list = LoadHooks().Hooks.Select(h => new
			{
				name = h.Name,
				desc = h.Desc,
				cmd = h.Cmd
			});

			await ctx.Response.WriteAsJsonAsync(new { ok = true, hooks = list });
		}

		/// <summary>跑一条白名单里的外部命令。</summary>
		private static async Task RunHook(HttpContext ctx)
		{
			if (!IsAllowed(ctx)) { await Deny(ctx); return; }

			HookRequest req;
			try
			{
				req = await ctx.Request.ReadFromJsonAsync<HookRequest>();
			}
			catch (Exception ex)
			{
				await ctx.Response.WriteAsJsonAsync(new { ok = false, output = "请求体不是合法 JSON：" + ex.Message });
				return;
			}

			string name = (req?.name ?? string.Empty).Trim();
			Hook hook = LoadHooks().Hooks.FirstOrDefault(h =>
				string.Equals(h.Name, name, StringComparison.OrdinalIgnoreCase));
			if (hook == null)
			{
				await ctx.Response.WriteAsJsonAsync(new { ok = false, output = "没有这个 hook：" + name });
				return;
			}
			if (string.IsNullOrWhiteSpace(hook.Cmd))
			{
				await ctx.Response.WriteAsJsonAsync(new { ok = false, output = "hook 没配 cmd：" + name });
				return;
			}

			string baseDir = Directory.GetCurrentDirectory();
			string workDir = string.IsNullOrWhiteSpace(hook.Cwd)
				? baseDir
				: Path.GetFullPath(Path.Combine(baseDir, hook.Cwd));

			int timeoutSec = hook.Timeout > 0 ? hook.Timeout : 120;

			StringBuilder sb = new StringBuilder();
			try
			{
				ProcessStartInfo psi = new ProcessStartInfo
				{
					FileName = "cmd.exe",
					// 先 chcp 65001：cmd 内置命令和多数控制台程序会按当前代码页输出，
					// 默认是 936(GBK)，用 UTF-8 解码就成了「锟斤拷」。
					Arguments = "/c chcp 65001 >nul & " + hook.Cmd,
					WorkingDirectory = workDir,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					CreateNoWindow = true,
					StandardOutputEncoding = Encoding.UTF8,
					StandardErrorEncoding = Encoding.UTF8
				};
				// python 自己的 stdout 编码跟控制台无关，单独钉成 utf-8
				psi.Environment["PYTHONIOENCODING"] = "utf-8";

				using Process p = Process.Start(psi);
				Task<string> outTask = p.StandardOutput.ReadToEndAsync();
				Task<string> errTask = p.StandardError.ReadToEndAsync();

				bool exited = p.WaitForExit(timeoutSec * 1000);
				if (!exited)
				{
					try { p.Kill(true); } catch { }
					sb.AppendLine("[超时 " + timeoutSec + "s，已强杀]");
				}

				sb.Append(await outTask);
				string err = await errTask;
				if (!string.IsNullOrWhiteSpace(err))
				{
					sb.AppendLine();
					sb.AppendLine("[stderr]");
					sb.Append(err);
				}
				sb.AppendLine();
				sb.AppendLine("[exit=" + (exited ? p.ExitCode.ToString() : "killed") + "]");
			}
			catch (Exception ex)
			{
				sb.AppendLine("启动失败：" + ex.Message);
			}

			await ctx.Response.WriteAsJsonAsync(new { ok = true, output = sb.ToString() });
		}
	}
}
