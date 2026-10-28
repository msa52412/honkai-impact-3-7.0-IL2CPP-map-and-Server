using HttpServer.Models;
using Newtonsoft.Json;
using Common;
using System.Text.RegularExpressions;

namespace HttpServer.Controllers
{
    public class DispatchController
    {
        /// <summary>
        /// 客户端拿到的所有「回连地址」都必须带 HTTP 端口。
        ///
        /// 这是 2026-10-21 实测出来的坑：原版 HttpServer.dll 返回的是
        /// `http://192.168.0.103:8081/query_gateway`（mitm 日志可见），
        /// 而上游开源版写的是 `http://{Host}/query_gateway`（默认 80）。
        /// 客户端拿到不带端口的地址就去连 80，那里是 nginx（只服务 asset_bundle），
        /// 于是 /query_gateway 404 → 游戏报「连接失败」。
        /// </summary>
        private static string HttpBase =>
            $"http://{Global.config.Gameserver.Host}:{Global.config.Http.HttpPort}";

        /// <summary>
        /// 静态资源（asset_bundle）走 nginx 的 80 端口，不带端口号 —— 和原版 DLL 一致。
        /// </summary>
        private static string WebBase =>
            $"http://{Global.config.Gameserver.Host}";

        public static void AddHandlers(WebApplication app)
        {
            app.Map("/query_dispatch", (ctx) =>
            {
                QueryDispatch rsp = new()
                {
                    Retcode = 0,
                    RegionList = new Region[] {
                        new Region() {
                            Retcode = 0,
                            DispatchUrl = $"{HttpBase}/query_gateway",
                            Name = Global.config.Gameserver.RegionName,
                            Title = "",
                            Ext = GetExt(ctx.Request.Query["version"].ToString())
                        }
                    }
                };
                ctx.Response.Headers.Add("Content-Type", "application/json");
                return ctx.Response.WriteAsync(JsonConvert.SerializeObject(rsp));
            });

            app.Map("/query_gateway", (ctx) =>
            {
                string Version = ctx.Request.Query["version"].ToString();
                Gameserver Gameserver = new()
                {
                    Ip = Global.config.Gameserver.Host,
                    Port = Global.config.Gameserver.Port
                };

                QueryGateway rsp = new()
                {
                    Retcode = 0,
                    Msg = "",
                    RegionName = Global.config.Gameserver.RegionName,
                    AccountUrl = $"{HttpBase}/account",
                    AccountUrlBackup = $"{HttpBase}/account",
                    AssetBundleUrlList = GetAssetBundleUrlList(Version),
                    ExAudioAndVideoUrlList = GetExAudioAndVideoUrlList(Version),
                    ExResourceUrlList = GetExResourceUrlList(Version),
                    Ext = GetExt(Version),
                    Gameserver = Gameserver,
                    Gateway = Gameserver,
                    IsDataReady = true,
                    OaserverUrl = $"{HttpBase}/oaserver",
                    ServerCurTime = Global.GetUnixInSeconds(),
                    ServerCurTimezone = 8,
                    ServerExt = new ServerExt()
                    {
                        CdkeyUrl = $"{HttpBase}/common",
                        MihoyoSdkEnv = "2"
                    }
                };
                ctx.Response.Headers.Add("Content-Type", "application/json");
                return ctx.Response.WriteAsync(JsonConvert.SerializeObject(rsp));
            });
        }

        public static Ext GetExt(string version)
        {
            return new Ext()
            {
                AiUseAssetBoundle = "1",
                ApmLogLevel = "2",
                ApmSwitch = "1",
                ApmSwitchCrash = "1",
                DataUseAssetBoundle = "1",
                EnableWatermark = "1",
                ExAudioAndVideoUrlList = GetExAudioAndVideoUrlList(version),
                ExResPrePublish = "0",
                ExResUseHttp = "0",
                ExResourceUrlList = GetExResourceUrlList(version),
                ForbidRecharge = "1",
                IsChecksumOff = "1",
                OfflineReportSwitch = "1",
                ResUseAssetBoundle = "1",
                ShowVersionText = "0",
                UpdateStreamingAsb = "1",
                UseMultyCdn = "1",
                ApmLogDest = "2",
                ApmSwitchGameLog = "1",
                BlockErrorDialog = "1",
                ElevatorModelPath = "GameEntry/EVA/StartLoading_Model",
                ExResBuffSize = "10485760",
                IsXxxx = "0",
                MtpSwitch = "0",
                NetworkFeedbackEnable = "0",
                ShowBulletinButton = "0",
                ShowBulletinEmptyDialogBg = "0"
            };
        }

        /// <summary>
        /// 资源包（含数值文件）地址：**恒走本地 nginx，不看 UseLocalCache**。
        ///
        /// 2026-10-21 实测：原版 HttpServer.dll 返回的就是
        /// http://192.168.0.103/asset_bundle/overseas01/1.1，
        /// 客户端直接打 nginx:80（access.log 里有
        /// /asset_bundle/overseas01/1.1/data/build.status 200）。
        ///
        /// 上游开源版在 UseLocalCache=false 时返回 mihoyo CDN，
        /// 而 proxy.py 只重写 hk-bigfile-os-mihayo（音视频），不重写 hk-bundle-os-mihayo，
        /// 于是 bundle 域名 DNS 解析失败 → 客户端报「下载数值文件失败 502」。
        /// 本地 nginx 上有完整 2616 个包，没有任何理由再走外网。
        /// </summary>
        public static string[] GetAssetBundleUrlList(string version)
        {
            string baseUrl = WebBase + "/asset_bundle";
            string local;
            Match matches = new Regex(@"^(.*?)_(os|gf|global)_(.*?)$").Match(version);
            if (matches.Success && matches.Groups[2].Value == "gf")
            {
                local = version.Contains("beta")
                    ? baseUrl + "/beta_release/1.0"
                    : baseUrl + "/android01/1.0";
            }
            else if (matches.Success && matches.Groups[2].Value == "global")
            {
                local = baseUrl + "/usa01/1.1";
            }
            else
            {
                local = baseUrl + "/overseas01/1.1";
            }
            return new[] { local, local };
        }
        
        /// <summary>
        /// 音视频/流式资源地址（音频包清单、StreamingAsb 清单）：**恒走本地 nginx**。
        ///
        /// 客户端启动时会做 CheckStreamingAssetBundle：
        ///   GET {base}/Audio/Windows/7_0/0/          <- 音频包清单
        ///   GET {base}/StreamingAsb/7_0_0/pc         <- 流式资源清单
        /// 它要的是目录索引（HTML 文件名列表），拿到 404 会重试 3 次然后弹
        /// 「下载活动资源失败」并退出。
        ///
        /// 2026-10-21 实测：v1 时代这两个 URL 由第三方镜像 3rd.ilulu.ink 提供
        /// （9.0k / 13.8k 的 autoindex 页），之后镜像上 Audio 目录被删，只剩 404。
        /// 现在改为本地 nginx 直接列目录，子目录是指向 BH3_Data\StreamingAssets 的目录联接，
        /// 列出来的文件名与客户端本地文件完全一致，所以不会触发任何下载。
        ///
        /// ★ 这里**只能返回 host+path，不能带 "http://"**。
        /// 客户端 WwiseAudioManager.CheckPackageManifestCoroutine 拿到条目后是
        /// 无条件 `"https://" + 条目` 拼接的（不判断有没有 scheme），
        /// 返回 "http://192.168.0.103/..." 会被拼成
        /// "https://http://192.168.0.103/..." → 主机名 "http" → 502。
        /// 原版 v1 返回的 "hk-bigfile-os-mihayo.akamaized.net/com.miHoYo.bh3oversea"
        /// 正是这种不带 scheme 的格式。
        /// 因为客户端强制 https，实际落到 443 端口 —— 由 HttpProgram 里的
        /// StaticProxy 反向代理到 nginx:80，目录索引仍由 nginx autoindex 生成。
        /// </summary>
        public static string[] GetExAudioAndVideoUrlList(string version)
        {
            string local = Global.config.Gameserver.Host + "/com.miHoYo.bh3oversea";
            return new[] { local, local };
        }
        
        /// <summary>
        /// 额外资源（视频 / 字幕 / 公告图等）：**恒走本地**，并且和 ex_audio 一样只给 host+path。
        ///
        /// 上游开源版在 UseLocalCache=false 时返回 mihoyo / akamaized 的 CDN 域名，
        /// 本机 DNS 解析不了（getaddrinfo failed）→ mitm 回 502 → 客户端报
        /// 「下载活动资源失败」。本地目录不存在时只会拿到 404，客户端会跳过，
        /// 不会卡住整条启动链。
        /// </summary>
        public static string[] GetExResourceUrlList(string version)
        {
            string host = Global.config.Gameserver.Host;
            Match matches = new Regex(@"^(.*?)_(os|gf|global)_(.*?)$").Match(version);

            string path;
            if (!matches.Success)
                path = "/com.miHoYo.bh3oversea";
            else if (matches.Groups[2].Value == "gf")
                path = version.Contains("beta") ? "/tmp/beta" : "/tmp/Original";
            else if (matches.Groups[2].Value == "global")
                path = "/tmp/com.miHoYo.bh3global";
            else
                path = "/com.miHoYo.bh3oversea";

            string local = host + path;
            return new[] { local, local };
        }
    }
}
