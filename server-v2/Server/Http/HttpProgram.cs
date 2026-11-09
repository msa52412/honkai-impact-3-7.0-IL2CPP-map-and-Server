using Common;
using Common.Utils;
using HttpServer.Controllers;
using Microsoft.Extensions.FileProviders;

namespace HttpServer
{
    public class Program
    {
        private static readonly Logger c = new("HTTP", ConsoleColor.Green);

        public static void Main()
        {
            Thread.CurrentThread.IsBackground = true;
            var builder = WebApplication.CreateBuilder();

            var app = builder.Build();

            app.UsePathBase("/");
            app.Urls.Add($"http://*:{Global.config.Http.HttpPort}");
            app.Urls.Add($"https://*:{Global.config.Http.HttpsPort}");

            // 443 上兼任静态资源（音频包清单 / StreamingAsb 清单）的转发：
            // 客户端对 ex_audio_and_video_url_list 的条目会无条件前置 "https://"，
            // 所以那两个目录索引只能落在 TLS 端口。真正的目录索引仍由 nginx 生成。
            app.UseStaticProxy();

            DispatchController.AddHandlers(app);
            AccountController.AddHandlers(app);
            ConfigController.AddHandlers(app);
            ExtraEndpoints.AddHandlers(app);
            GmController.AddHandlers(app);
            GmEndlessController.AddHandlers(app);
            GmPatchController.AddHandlers(app);
            GmConfigController.AddHandlers(app);
            GmTowerController.AddHandlers(app);
            GmGachaController.AddHandlers(app);
            GmWeatherController.AddHandlers(app);
            GmWarshipController.AddHandlers(app);
            GmInventoryController.AddHandlers(app);
            LocalPages.AddHandlers(app);

            app.UseMiddleware<RequestLoggingMiddleware>();
            c.Log($"HTTP server started on port {Global.config.Http.HttpPort} & {Global.config.Http.HttpsPort}");
            app.Run();
        }

        private class RequestLoggingMiddleware
        {
            private readonly RequestDelegate _next;
            private static readonly string[] SurpressedRoutes = new string[] { "/report", "/sdk/dataUpload" };

            public RequestLoggingMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task Invoke(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                finally
                {
                    if ((int)Global.config.VerboseLevel > (int)VerboseLevel.Normal)
                    {
                        c.Log($"{context.Response.StatusCode} {context.Request.Method.ToUpper()} {context.Request.Path}");
                    }else if(((int)Global.config.VerboseLevel > (int)VerboseLevel.Silent) && !SurpressedRoutes.Contains(context.Request.Path.ToString()))
                    {
                        c.Log($"{context.Response.StatusCode} {context.Request.Method.ToUpper()} {context.Request.Path}");
                    }
                }
            }
        }
    }
}