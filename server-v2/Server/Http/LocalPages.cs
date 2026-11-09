using System.Text;

namespace HttpServer
{
    /// <summary>
    /// WebView 兜底页。
    ///
    /// 背景
    /// ----
    /// SDK 的 WebView 会去加载 https://account-pre.hoyoverse.com/login-platform/index.html
    /// （注册 / 找回密码 / 换号 / 人机验证都在这条链上）。
    /// 按「所有连接不出公网」的要求，这个域名已被 hosts 指到 127.0.0.1。
    /// 但本机如果什么都不回，WebView 就是一片白 —— 用户根本不知道发生了什么。
    ///
    /// 所以这里回一个说明页：
    ///   * 告诉用户现在是「本地模式」，登录/注册这些公网功能已被切断
    ///   * 告诉用户换号该改哪个文件（比原页面更快：改一行，重启即生效）
    ///
    /// 页面本身是纯静态 HTML，不引用任何外链资源（字体、图片、脚本全部内联），
    /// 保证它自己也不会产生第二条公网连接。
    /// </summary>
    public static class LocalPages
    {
        public static void AddHandlers(WebApplication app)
        {
            // WebView 实际会命中的路径（来自 resources.assets 里的 SDK 配置表）
            string[] paths =
            {
                "/login-platform",
                "/login-platform/{**rest}",
                "/sandbox",
                "/sandbox/{**rest}",
                "/index.html",
                "/ageLimit.html",
                "/content-page.html",
                "/geetestV2.html",
                "/others-verify-m.html",
                "/others-verify-pc.html",
                "/psnBinding.html"
            };

            foreach (string p in paths)
                app.Map(p, (HttpContext ctx) => Serve(ctx));
        }

        private static Task Serve(HttpContext ctx)
        {
            ctx.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
            ctx.Response.Headers["Cache-Control"] = "no-store";
            return ctx.Response.WriteAsync(Html);
        }

        private const string Html = """
<!DOCTYPE html>
<html lang="zh-CN">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>本地模式 · 无需登录</title>
<style>
  :root { color-scheme: dark; }
  html,body { height:100%; margin:0; }
  body {
    background:#14171c;
    color:#e6e9ef;
    font-family: "Microsoft YaHei UI","Microsoft YaHei",system-ui,-apple-system,"Segoe UI",sans-serif;
    display:flex; align-items:center; justify-content:center;
  }
  .card {
    width:min(560px,92vw);
    background:#1b1f26;
    border:1px solid #2b313b;
    border-radius:14px;
    padding:26px 28px 22px;
  }
  h1 { margin:0 0 6px; font-size:19px; font-weight:600; letter-spacing:.5px; }
  .sub { color:#8b93a1; font-size:12.5px; margin-bottom:18px; }
  .row { display:flex; gap:10px; padding:9px 0; border-top:1px solid #232932; font-size:13px; }
  .row .k { color:#8b93a1; width:86px; flex:none; }
  .row .v { color:#dfe4ec; }
  code {
    background:#11151a; border:1px solid #2b313b; border-radius:5px;
    padding:1px 6px; font-family:Consolas,"Cascadia Mono",monospace; font-size:12.5px; color:#9ad1ff;
  }
  .tip { margin-top:16px; padding:11px 13px; background:#171d14; border:1px solid #2c3a24;
         border-radius:9px; color:#a9c893; font-size:12.5px; line-height:1.7; }
</style>
</head>
<body>
  <div class="card">
    <h1>本地模式 · 登录环节已跳过</h1>
    <div class="sub">当前客户端不连接任何外部服务器，公网请求为 0。</div>

    <div class="row"><div class="k">登录</div><div class="v">不需要。启动时服务端直接回本地账号。</div></div>
    <div class="row"><div class="k">注册 / 找回</div><div class="v">已切断（这两个功能只能走公网）。</div></div>
    <div class="row"><div class="k">人机验证</div><div class="v">已切断（依赖第三方 geetest）。</div></div>

    <div class="tip">
      换号 / 切换账号：改 <code>server-v2\run\default_uid.txt</code>，
      内容填目标账号的 uid（当前 <b>1002</b>），保存后重启服务端即可。<br>
      填 <code>0</code> 表示关闭宽容登录，恢复「必须手动登录」。
    </div>
  </div>
</body>
</html>
""";
    }
}
