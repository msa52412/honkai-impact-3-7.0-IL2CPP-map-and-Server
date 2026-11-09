using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HttpServer.Controllers
{
	/// <summary>
	/// GM 面板的「量子深渊修复」开关 —— 按需拉起 / 停止注入守护 qt_watch.py。
	///
	/// 为什么改成按需，而不是开机常驻
	/// ------------------------------
	/// 守护的工作是往游戏进程里 `lua_dostring` 下发补丁。它必须每 0.2 秒看一次
	/// `&g_lua_state`，而客户端每换一次场景/切一次界面都会新建一个 lua_State。
	/// 在那个「VM 刚建好、脚本还在挂载」的窗口里写内存，ulua.dll 会直接访问违例：
	/// 2026-11-08 12:12:33 就是这么崩的（模块 ulua.dll +0x27c450，0xc0000005）。
	///
	/// 补丁本身只在「打量子深渊」时需要（第 6 场难度越界、第 20 场协程异常），
	/// 平时刷别的关卡完全用不上。所以改成玩家自己点「量子深渊修复」才启动，
	/// 打完点「停止」，把风险窗口压到最短。
	///
	///   GET  /gm/api/patch/status   运行状态 + 最近日志
	///   POST /gm/api/patch/start    拉起守护（已在跑则不重复拉起）
	///   POST /gm/api/patch/stop     结束守护
	///
	/// PID 由守护自己写进 _ext/tmp/qt_watch.pid，所以「手工在命令行启动的」
	/// 也能被这里认出来，不会出现两个守护同时写内存。
	/// </summary>
	internal static class GmPatchController
	{
		private static readonly object Gate = new();

		public static void AddHandlers(WebApplication app)
		{
			app.MapGet("/gm/api/patch/status", (HttpContext ctx) => Guard(ctx, () => Status(ctx)));
			app.MapPost("/gm/api/patch/start", (HttpContext ctx) => Guard(ctx, () => Start(ctx)));
			app.MapPost("/gm/api/patch/stop", (HttpContext ctx) => Guard(ctx, () => Stop(ctx)));
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
				c.Warn("修复开关异常：" + ex);
				if (!ctx.Response.HasStarted)
				{
					ctx.Response.StatusCode = 200;
					await ctx.Response.WriteAsJsonAsync(new { ok = false, output = "内部错误：" + ex.GetType().Name + " - " + ex.Message });
				}
			}
		}

		// ---------------------------------------------------------------- 路径

		/// <summary>从 run\ 往上找仓库根（含 _ext\tools\qt_watch.py 的那一层）。</summary>
		private static string FindRoot()
		{
			string dir = Directory.GetCurrentDirectory();
			for (int i = 0; i < 6 && !string.IsNullOrEmpty(dir); i++)
			{
				if (File.Exists(Path.Combine(dir, "_ext", "tools", "qt_watch.py")))
				{
					return dir;
				}
				dir = Path.GetDirectoryName(dir);
			}
			return null;
		}

		private static string ScriptPath(string root) => Path.Combine(root, "_ext", "tools", "qt_watch.py");

		private static string PidPath(string root) => Path.Combine(root, "_ext", "tmp", "qt_watch.pid");

		private static string LogPath(string root) => Path.Combine(root, "_ext", "tmp", "qt_watch.log");

		/// <summary>守护在「补丁已生效」时写的印记：内容 = 被打补丁的 BH3.exe pid + 时间戳。
		/// 补丁写进客户端内存后停止守护也撤不回来，只能重启客户端，
		/// GM 面板靠它决定要不要亮「玩其他模式先重启」的提示。</summary>
		private static string DirtyPath(string root) => Path.Combine(root, "_ext", "tmp", "qt_patch.dirty");

		private static string PythonPath(string root)
		{
			string bundled = Path.Combine(root, "app", "tools", "python", "python.exe");
			return File.Exists(bundled) ? bundled : "python";
		}

		// ---------------------------------------------------------------- 状态

		private static int ReadPid(string root)
		{
			try
			{
				string raw = File.ReadAllText(PidPath(root)).Trim();
				return int.TryParse(raw, out int pid) ? pid : 0;
			}
			catch
			{
				return 0;
			}
		}

		/// <summary>PID 是否还活着。只看「活着」，不看进程名——PID 复用概率极低，
		/// 真撞上了也就是面板显示错误，不会造成误杀（我们只在 stop 时按这个 PID 结束）。</summary>
		private static bool Alive(int pid)
		{
			if (pid <= 0)
			{
				return false;
			}
			try
			{
				using Process p = Process.GetProcessById(pid);
				return !p.HasExited;
			}
			catch
			{
				return false;
			}
		}

		private static string TailLog(string root, int lines)
		{
			string path = LogPath(root);
			try
			{
				if (!File.Exists(path))
				{
					return "(还没有日志，点「启动」后这里会出现补丁下发记录)";
				}
				string[] all = File.ReadAllLines(path, Encoding.UTF8);
				int from = Math.Max(0, all.Length - lines);
				StringBuilder sb = new StringBuilder();
				for (int i = from; i < all.Length; i++)
				{
					sb.AppendLine(all[i]);
				}
				return sb.ToString().TrimEnd();
			}
			catch (Exception ex)
			{
				return "读取日志失败：" + ex.Message;
			}
		}

		private static async Task Status(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			string root = FindRoot();
			if (root == null)
			{
				await ctx.Response.WriteAsJsonAsync(new
				{
					ok = true,
					running = false,
					pid = 0,
					game = false,
					gamePid = 0,
					dirty = false,
					log = "找不到 _ext\\tools\\qt_watch.py，确认服务端是从 app\\server-v2\\run 启动的",
				});
				return;
			}

			int pid = ReadPid(root);
			bool running = Alive(pid);
			int gamePid = GamePid();

			await ctx.Response.WriteAsJsonAsync(new
			{
				ok = true,
				running,
				pid = running ? pid : 0,
				game = gamePid > 0,
				gamePid,
				// 印记里的 pid 和当前客户端一致 → 补丁还在这份客户端内存里
				dirty = IsDirty(root, gamePid),
				log = TailLog(root, 18),
			});
		}

		/// <summary>当前 BH3.exe 的 pid；没开就是 0。</summary>
		private static int GamePid()
		{
			try
			{
				Process[] all = Process.GetProcessesByName("BH3");
				if (all.Length > 0)
				{
					return all[0].Id;
				}
			}
			catch { }
			return 0;
		}

		/// <summary>补丁是否还留在当前客户端内存里。
		/// 印记 pid == 当前 pid → 是（客户端没重启过）；否则说明重启了，顺手清印记。</summary>
		private static bool IsDirty(string root, int gamePid)
		{
			string path = DirtyPath(root);
			try
			{
				if (!File.Exists(path))
				{
					return false;
				}
				string raw = File.ReadAllText(path).Trim();
				int markPid = 0;
				int sp = raw.IndexOfAny(new[] { ' ', '\t' });
				int.TryParse(sp > 0 ? raw.Substring(0, sp) : raw, out markPid);
				if (gamePid > 0 && markPid == gamePid)
				{
					return true;
				}
				// 客户端 pid 变了（或游戏根本没开）→ 旧内存已随进程销毁，印记作废
				File.Delete(path);
				return false;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>游戏进程在不在 —— 守护会等游戏起来才干活，面板上要能看出这一点。</summary>
		private static bool GameRunning() => GamePid() > 0;

		// ---------------------------------------------------------------- 启动

		private static async Task Start(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			string root = FindRoot();
			if (root == null)
			{
				await Fail(ctx, "找不到 _ext\\tools\\qt_watch.py");
				return;
			}

			string script = ScriptPath(root);
			if (!File.Exists(script))
			{
				await Fail(ctx, "守护脚本不存在：" + script);
				return;
			}

			// lock 里只做同步的「检查 + 拉起」，响应写到锁外面 await，
			// 避免持锁期间跑异步操作（lock 里不能 await）。
			string output;
			lock (Gate)
			{
				output = StartCore(root, script);
			}
			await ctx.Response.WriteAsJsonAsync(new { ok = true, output });
		}

		private static string StartCore(string root, string script)
		{
			int old = ReadPid(root);
			if (Alive(old))
			{
				// 已经在跑就别再起一个：两个守护同时写游戏内存会直接把客户端写崩。
				return "守护已在运行（pid=" + old + "），没有重复启动。";
			}

			ProcessStartInfo psi = new ProcessStartInfo
			{
				FileName = PythonPath(root),
				Arguments = "\"" + script + "\"",
				WorkingDirectory = Path.GetDirectoryName(script),
				UseShellExecute = false,
				CreateNoWindow = true,
				// 必须重定向并消费：守护会一直 print，父进程不读的话
				// 管道缓冲区满了会把子进程卡死在 write 上。
				RedirectStandardOutput = true,
				RedirectStandardError = true,
			};
			psi.Environment["PYTHONIOENCODING"] = "utf-8";

			Process proc = new Process { StartInfo = psi, EnableRaisingEvents = true };
			proc.OutputDataReceived += (s, e) => { };
			proc.ErrorDataReceived += (s, e) => { };
			proc.Start();
			proc.BeginOutputReadLine();
			proc.BeginErrorReadLine();
			try { proc.StandardInput.Close(); } catch { }

			// 守护写完 pid 文件才算真的起来了，给它一点时间
			int waited = 0;
			while (waited < 3000 && !File.Exists(PidPath(root)))
			{
				System.Threading.Thread.Sleep(100);
				waited += 100;
			}
			System.Threading.Thread.Sleep(400);

			int pid = ReadPid(root);
			bool up = Alive(pid);

			StringBuilder sb = new StringBuilder();
			sb.AppendLine(up ? "量子深渊修复已启动（pid=" + pid + "）" : "已拉起进程（pid 文件还没写出来，稍等刷新）");
			sb.AppendLine(GameRunning()
				? "检测到 BH3.exe 在运行，守护会自动补齐补丁。"
				: "游戏还没开：守护会一直等 BH3.exe，开游戏后自动接管。");
			sb.AppendLine("【提醒】补丁是直接写进客户端内存的，打上之后撤不回来：");
			sb.AppendLine("  · 只打量子深渊没问题；");
			sb.AppendLine("  · 打完后要玩迪拉克/主线等其他模式，请先重启客户端（关掉 BH3.exe 再开）；");
			sb.AppendLine("  · 点「停止」只是停守护，已经写进去的补丁还在，同样要重启才干净。");
			return sb.ToString();
		}

		// ---------------------------------------------------------------- 停止

		private static async Task Stop(HttpContext ctx)
		{
			if (!GmController.IsAllowed(ctx)) { await GmController.Deny(ctx); return; }

			string root = FindRoot();
			if (root == null)
			{
				await Fail(ctx, "找不到仓库根目录");
				return;
			}

			string output;
			lock (Gate)
			{
				output = StopCore(root);
			}
			await ctx.Response.WriteAsJsonAsync(new { ok = true, output });
		}

		private static string StopCore(string root)
		{
			int pid = ReadPid(root);
			bool dirty = IsDirty(root, GamePid());
			if (!Alive(pid))
			{
				try { File.Delete(PidPath(root)); } catch { }
				return dirty
					? "守护本来就没在运行。\r\n【提醒】之前下发的补丁仍在这个客户端进程里，"
						+ "要玩其他模式请先重启客户端（关掉 BH3.exe 再开）。"
					: "守护本来就没在运行。";
			}

			try
			{
				using Process p = Process.GetProcessById(pid);
				p.Kill(true);
				p.WaitForExit(3000);
			}
			catch (Exception ex)
			{
				return "结束进程失败：" + ex.Message;
			}

			try { File.Delete(PidPath(root)); } catch { }

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("量子深渊修复已停止（pid=" + pid + "）。");
			if (IsDirty(root, GamePid()))
			{
				sb.AppendLine("【提醒】停止只停了守护，已经下发的补丁仍留在客户端内存里，撤回不了。");
				sb.AppendLine("  · 接着打量子深渊：没问题；");
				sb.AppendLine("  · 要玩迪拉克/主线等其他模式：请先重启客户端（关掉 BH3.exe 再开）再进。");
			}
			else
			{
				sb.AppendLine("当前客户端还没被打过补丁，直接玩其他模式没有影响。");
			}
			return sb.ToString();
		}

		// ---------------------------------------------------------------- 小工具

		private static Task Fail(HttpContext ctx, string msg)
		{
			return FailAsync(ctx, msg);
		}

		private static async Task FailAsync(HttpContext ctx, string msg)
		{
			await ctx.Response.WriteAsJsonAsync(new { ok = false, output = msg });
		}
	}
}
