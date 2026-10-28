using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace HttpServer.Controllers
{
    /// <summary>
    /// 7.0 的 HttpServer.dll 里有、但上游仓库没实现的路由。
    ///
    /// 清单来自对 7.0 那份 HttpServer.dll 的字符串提取（#US 堆）：
    ///   /combo/box/api/config/sw/precache
    ///   /bh3_os/combo/red_dot/list
    ///   /common/bh3_os/announcement/api/getAlertAnn
    ///   /common/bh3_os/announcement/api/getAlertPic
    ///   /common/h5log/log/batch
    ///   /status/server
    ///   /sw.html
    ///
    /// 这些全是「配置 / 公告 / 埋点」类接口，客户端拿到合法空结构就会继续，
    /// 所以统一返回 retcode=0 + 空 data。缺了它们客户端会在登录前后卡住。
    /// </summary>
    public static class ExtraEndpoints
    {
        public static void AddHandlers(WebApplication app)
        {
            // 客户端会拿这个判断服务端是否在线
            app.Map("/status/server", (HttpContext ctx) =>
                ctx.Response.WriteAsJsonAsync(new
                {
                    status = "OK",
                    retcode = 0,
                    message = "OK"
                }));

            // 公告：返回空列表，客户端不弹窗
            app.Map("/common/bh3_os/announcement/api/getAlertAnn", (HttpContext ctx) =>
                ctx.Response.WriteAsJsonAsync(new
                {
                    retcode = 0,
                    message = "OK",
                    data = new
                    {
                        alert = false,
                        alert_id = 0,
                        alert_type = 0,
                        alert_title = "",
                        alert_content = "",
                        alert_image = "",
                        alert_url = "",
                        alert_time = "",
                        alert_remark = ""
                    }
                }));

            app.Map("/common/bh3_os/announcement/api/getAlertPic", (HttpContext ctx) =>
                ctx.Response.WriteAsJsonAsync(new
                {
                    retcode = 0,
                    message = "OK",
                    data = new { alert_pic_list = Array.Empty<object>() }
                }));

            // 红点
            app.Map("/bh3_os/combo/red_dot/list", (HttpContext ctx) =>
                ctx.Response.WriteAsJsonAsync(new
                {
                    retcode = 0,
                    message = "OK",
                    data = new { list = Array.Empty<object>() }
                }));

            // 预缓存配置
            app.Map("/combo/box/api/config/sw/precache", (HttpContext ctx) =>
                ctx.Response.WriteAsJsonAsync(new
                {
                    retcode = 0,
                    message = "OK",
                    data = new
                    {
                        vals = new
                        {
                            enable = "false",
                            wifi_only = "true",
                            expt = "",
                            url = ""
                        }
                    }
                }));

            // 埋点上报：客户端不等结果，返回 200 空体即可
            app.Map("/common/h5log/log/batch", async (HttpContext ctx) =>
            {
                ctx.Response.StatusCode = 200;
                await ctx.Response.WriteAsync("{\"retcode\":0,\"message\":\"OK\"}");
            });

            // 旧的网页版入口，客户端偶尔探一下
            app.Map("/sw.html", async (HttpContext ctx) =>
            {
                ctx.Response.ContentType = "text/html; charset=utf-8";
                await ctx.Response.WriteAsync("<html><body>OK</body></html>");
            });

            // /account 与 /oaserver /common 在 query_gateway 里作为 base url 下发，
            // 客户端可能直接探根路径，给个空 JSON 免得 404。
            app.Map("/account", (HttpContext ctx) =>
                ctx.Response.WriteAsJsonAsync(new { retcode = 0, message = "OK" }));

            app.Map("/oaserver", (HttpContext ctx) =>
                ctx.Response.WriteAsJsonAsync(new { retcode = 0, message = "OK" }));

            app.Map("/common", (HttpContext ctx) =>
                ctx.Response.WriteAsJsonAsync(new { retcode = 0, message = "OK" }));
        }
    }
}
