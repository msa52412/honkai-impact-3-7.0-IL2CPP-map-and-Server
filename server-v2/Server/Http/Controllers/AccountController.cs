using Newtonsoft.Json;
using HttpServer.Models;
using Common.Database;
using Newtonsoft.Json.Linq;

namespace HttpServer.Controllers
{
    public class AccountController
    {
        /// <summary>
        /// 免登录（宽容模式）的回落账号。
        ///
        /// 背景：SDK 启动时会拿本地缓存的 token 调 /mdk/shield/api/verify。
        /// 以前 token 在库里找不到就返回 Account=null，SDK 认为「没登录」，
        /// 于是弹出它自己的 HoYoverse 原生登录面板（不是网页，没法缓存）。
        ///
        /// 这里改成：认不出 token 就回落到 run\default_uid.txt 指定的本地账号
        /// （缺省 1002），并把 DB 里该账号的真实 token 一起发回去。
        /// SDK 拿到合法账号后直接进入游戏，全程不出现登录面板。
        ///
        /// 想关掉这个行为（恢复「必须登录」），把 run\default_uid.txt 内容改成 0。
        /// </summary>
        private static readonly uint FallbackUid = ReadFallbackUid();

        private const uint FallbackUidOff = 0u;

        private static uint ReadFallbackUid()
        {
            try
            {
                string path = Path.Combine(AppContext.BaseDirectory, "default_uid.txt");
                if (!File.Exists(path))
                    return 1002u;
                string text = File.ReadAllText(path).Trim();
                if (uint.TryParse(text, out uint uid))
                    return uid;
            }
            catch
            {
                // 读不到就用缺省值，绝不让登录链路因为配置问题挂掉
            }
            return 1002u;
        }

        private static UserScheme? ResolveFallback(string reason, string token)
        {
            if (FallbackUid == FallbackUidOff)
                return null;

            UserScheme? user = User.FromUid(FallbackUid);
            if (user == null)
            {
                Console.WriteLine($"[SDK-LOGIN] 回落失败：库里没有 uid={FallbackUid}（{reason} token={token}）");
                return null;
            }

            Console.WriteLine($"[SDK-LOGIN] 宽容登录：{reason} 未命中 token={token}，回落 uid={user.Uid} name={user.Name}");
            return user;
        }

        public static void AddHandlers(WebApplication app)
        {
            app.Map("/account/risky/api/check", (HttpContext ctx) =>
            {
                RiskyCheck rsp = new()
                {
                    Retcode = 0,
                    Message = "",
                    Data = new RiskyCheck.DataScheme()
                    {
                        Id = "",
                        Action = "ACTION_NONE",
                        Geetest = null
                    }
                };

                ctx.Response.Headers.Add("Content-Type", "application/json");

                return ctx.Response.WriteAsync(JsonConvert.SerializeObject(rsp));
            });

#pragma warning disable CS8600, CS8602 // Converting null literal or possible null value to non-nullable type.
            app.MapPost("/{game_biz}/combo/granter/login/v2/login", (ctx) =>
            {
                StreamReader Reader = new(ctx.Request.Body);
                GranterLoginBody Data = JsonConvert.DeserializeObject<GranterLoginBody>(Reader.ReadToEndAsync().Result);
                GranterLoginBody.GranterLoginBodyData GranterLoginData = JsonConvert.DeserializeObject<GranterLoginBody.GranterLoginBodyData>(Data.Data);
                

                return ctx.Response.WriteAsJsonAsync(new
                {
                    retcode = 0,
                    message = "OK",
                    data = new {
                        combo_id = "0",
                        open_id = GranterLoginData.Uid,
                        combo_token = GranterLoginData.Token,
                        data = JsonConvert.SerializeObject(new
                        {
                            guest = GranterLoginData.Guest
                        }),
                        heartbeat = false,
                        account_type = 1,
                    }
                });
            });
            
            app.MapPost("/{game_biz}/mdk/shield/api/verify", (ctx) =>
            {
                StreamReader Reader = new(ctx.Request.Body);
                ShieldVerifyBody Data = JsonConvert.DeserializeObject<ShieldVerifyBody>(Reader.ReadToEndAsync().Result);
                UserScheme? user = User.FromToken(Data.Token);

                // 宽容模式：token 认不出来也要给一个账号，否则 SDK 会弹登录面板
                if (user == null)
                    user = ResolveFallback("verify", Data.Token);

                ShieldLoginResponse rsp = new()
                {
                    Retcode = 0,
                    Message = "OK",
                    Data = new()
                    {
                        Account = null
                    }
                };

                if (user != null)
                {
                    rsp.Data = new()
                    {
                        Account = new()
                        {
                            Uid = user.Uid,
                            Name = user.Name,
                            Email = "",
                            Mobile = "",
                            IsEmailVerify = "0",
                            Realname = "",
                            IdentityCard = "",
                            Token = user.Token.ToString(),
                            SafeMobile = "",
                            FacebookName = "",
                            GoogleName = "",
                            TwitterName = "",
                            GameCenterName = "",
                            AppleName = "",
                            SonyName = "",
                            TapName = "",
                            Country = "SG",
                            ReactivateTicket = "",
                            AreaCode = "**",
                            DeviceGrantTicket = "",
                            SteamName = "",
                            UnmaskedEmail = "",
                            UnmaskedEmailType = 0
                        },
                        DeviceGrantRequired = false,
                        SafeMoblieRequired = false,
                        RealpersonRequired = false,
                        ReactivateRequired = false,
                        RealnameOperation = "None"
                    };
                }

                ctx.Response.Headers.Add("Content-Type", "application/json");

                return ctx.Response.WriteAsync(JsonConvert.SerializeObject(rsp));
            });

            app.MapPost("/{game_biz}/mdk/shield/api/login", (ctx) =>
            {
                StreamReader Reader = new(ctx.Request.Body);
                ShieldLoginBody Data = JsonConvert.DeserializeObject<ShieldLoginBody>(Reader.ReadToEndAsync().Result);

                // 面板里随便填一个用户名（哪怕不存在）都给它落到本地账号。
                // 注意这里**不能**用 User.FromName —— 它会为陌生名字现场建号，
                // 结果就是把主账号的数据"顶掉"成空白新号。只查不建。
                UserScheme? user = User.collection.All().FirstOrDefault(d => d.Name == Data.Account);
                if (user == null)
                    user = ResolveFallback("shield/login", Data.Account);
                if (user == null)
                {
                    return ctx.Response.WriteAsJsonAsync(new { retcode = -1, message = "no local account", data = (object?)null });
                }

                ShieldLoginResponse rsp = new()
                {
                    Retcode = 0,
                    Message = "OK",
                    Data = new()
                    {
                        Account = new()
                        {
                            Uid = user.Uid,
                            Name = user.Name,
                            Email = "",
                            Mobile = "",
                            IsEmailVerify = "0",
                            Realname = "",
                            IdentityCard = "",
                            Token = user.Token.ToString(),
                            SafeMobile = "",
                            FacebookName = "",
                            GoogleName = "",
                            TwitterName = "",
                            GameCenterName = "",
                            AppleName = "",
                            SonyName = "",
                            TapName = "",
                            Country = "**",
                            ReactivateTicket = "",
                            AreaCode = "**",
                            DeviceGrantTicket = "",
                            SteamName = "",
                            UnmaskedEmail = "",
                            UnmaskedEmailType = 0
                        },
                        DeviceGrantRequired = false,
                        SafeMoblieRequired = false,
                        RealpersonRequired = false,
                        ReactivateRequired = false,
                        RealnameOperation = "None"
                    }
                };

                ctx.Response.Headers.Add("Content-Type", "application/json");

                return ctx.Response.WriteAsync(JsonConvert.SerializeObject(rsp));
            });
#pragma warning restore CS8600, CS8602 // Converting null literal or possible null value to non-nullable type.
        }
    }
}
