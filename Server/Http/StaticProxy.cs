using System.Net;
using Common;
using Common.Utils;

namespace HttpServer;

/// <summary>
/// 把 443 端口上的 /com.miHoYo.bh3oversea 反向代理到本机 nginx(:80)。
///
/// 为什么需要它：
///   客户端拿到的 ex_audio_and_video_url_list 条目会被**无条件前置 "https://"**
///   （MoleMole.WwiseAudioManager.CheckPackageManifestCoroutine），
///   所以「音频包清单 / StreamingAsb 清单」这两个目录索引请求只会落在 TLS 端口，
///   也就是本进程的 443，而不是 nginx 的 80。
///
/// 为什么不在 C# 里自己列目录：
///   目录索引已经由 nginx 的 autoindex 生成好了（web\com.miHoYo.bh3oversea 下的两个
///   子目录是指向 BH3_Data\StreamingAssets 的目录联接，12G 音频 / 17G asb 零拷贝）。
///   客户端是靠**比对索引页里的文件名与本地文件**来判断要不要下载的，
///   一旦 C# 生成的 HTML 与 nginx 的格式有差异，就会被误判成「全部缺失」，
///   进而触发几十 G 的下载。所以这里只做一层透明转发，nginx 仍是唯一的静态资源来源。
///
/// 顺带的好处：assets 仍然只有一份（runtime\web），不会出现两套配置各自漂移。
/// </summary>
internal static class StaticProxy
{
	/// <summary>只有这个前缀的请求才转发，其余照常走 Kestrel 自己的路由。</summary>
	private const string Prefix = "/com.miHoYo.bh3oversea";

	private static readonly Logger c = new("HTTP", ConsoleColor.Green);

	/// <summary>
	/// 不走系统代理（系统代理此时指向 mitmdump:8891，走代理会绕回自己形成环路），
	/// 也不自动跟随 301 —— 跳转要原样交给客户端。
	/// </summary>
	private static readonly HttpClient Client = new(new HttpClientHandler
	{
		AllowAutoRedirect = false,
		UseProxy = false,
	})
	{
		// 本地盘读 12G 音频 / 17G asb，正常很快；给足时间防止大文件传输被超时打断。
		Timeout = TimeSpan.FromMinutes(30),
	};

	public static IApplicationBuilder UseStaticProxy(this IApplicationBuilder app)
	{
		return app.Use(async (context, next) =>
		{
			if (!context.Request.Path.StartsWithSegments(Prefix))
			{
				await next();
				return;
			}

			await ForwardAsync(context);
		});
	}

	private static async Task ForwardAsync(HttpContext context)
	{
		HttpRequest req = context.Request;
		string target = $"http://127.0.0.1:{Global.config.Http.StaticPort}{req.Path}{req.QueryString}";

		using HttpRequestMessage msg = new(new HttpMethod(req.Method), target);
		if (req.ContentLength > 0)
			msg.Content = new StreamContent(req.Body);

		HttpResponseMessage resp;
		try
		{
			resp = await Client.SendAsync(msg, HttpCompletionOption.ResponseHeadersRead,
				context.RequestAborted);
		}
		catch (Exception e)
		{
			// 最常见的原因：nginx 没起来。给出明确提示，别让客户端只看到 502。
			c.Log($"静态资源转发失败 {target} -> {e.GetType().Name}: {e.Message}（nginx 是否在跑？）");
			context.Response.StatusCode = 502;
			return;
		}

		using (resp)
		{
			context.Response.StatusCode = (int)resp.StatusCode;

			foreach (KeyValuePair<string, IEnumerable<string>> h in resp.Headers)
			{
				// ★ 千万不能透传 Transfer-Encoding。
				// HttpClient 已经把 chunked 解开了（body 是原始 HTML），
				// 但 .NET 仍然把 "Transfer-Encoding: chunked" 留在 resp.Headers 里；
				// 一旦把它写给 Kestrel，Kestrel 就认为「分帧由应用负责」，
				// 于是直接输出未分块的 HTML —— 头和体对不上，
				// 客户端 / 测试脚本按 chunked 解析就会拿到 "<html>" 当成块长度。
				// Connection 也一并丢掉：由 Kestrel 自己管理。
				if (h.Key.Equals("Transfer-Encoding", StringComparison.OrdinalIgnoreCase)
				    || h.Key.Equals("Connection", StringComparison.OrdinalIgnoreCase))
					continue;

				context.Response.Headers[h.Key] = h.Value.ToArray();
			}

			foreach (KeyValuePair<string, IEnumerable<string>> h in resp.Content.Headers)
			{
				if (h.Key.Equals("Transfer-Encoding", StringComparison.OrdinalIgnoreCase))
					continue;

				context.Response.Headers[h.Key] = h.Value.ToArray();
			}

			// nginx 对「目录少了结尾斜杠」会回 301，Location 里是 http://127.0.0.1/...
			// 客户端走的是 https，原样透传会把它降级到 80 端口。
			// 改成以 "/" 开头的相对跳转，让客户端按当前 scheme（https）重新拼。
			if ((resp.StatusCode == HttpStatusCode.MovedPermanently
			     || resp.StatusCode == HttpStatusCode.Found)
			    && context.Response.Headers.TryGetValue("Location", out var location))
			{
				string raw = location.ToString();
				int idx = raw.IndexOf(Prefix, StringComparison.Ordinal);
				if (idx > 0)
					context.Response.Headers["Location"] = raw.Substring(idx);
			}

			await resp.Content.CopyToAsync(context.Response.Body, context.RequestAborted);
		}
	}
}
