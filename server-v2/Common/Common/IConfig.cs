using Config.Net;

namespace Common;

public interface IConfig
{
	public interface IGameserver
	{
		[Option(DefaultValue = "127.0.0.1")]
		string Host { get; set; }

		[Option(DefaultValue = 16100u)]
		uint Port { get; set; }

		[Option(DefaultValue = "overseas01")]
		string RegionName { get; set; }
	}

	public interface IHttp
	{
		[Option(DefaultValue = 80u)]
		uint HttpPort { get; set; }

		[Option(DefaultValue = 443u)]
		uint HttpsPort { get; set; }

		/// <summary>
		/// 静态资源（nginx）监听的端口。
		/// 客户端的 ex_audio_and_video_url_list 条目会被强制前置 "https://"，
		/// 所以那两个目录索引请求会打到 HttpsPort（本进程），
		/// 由 StaticProxy 反向代理到这里的 StaticPort（nginx）。
		/// </summary>
		[Option(DefaultValue = 80u)]
		uint StaticPort { get; set; }
	}

	[Option(DefaultValue = VerboseLevel.Normal)]
	VerboseLevel VerboseLevel { get; set; }

	[Option(DefaultValue = false)]
	bool UseLocalCache { get; set; }

	[Option(DefaultValue = true)]
	bool CreateAccountOnLoginAttempt { get; set; }

	[Option(DefaultValue = "mongodb://127.0.0.1:27017/PemukulPaku")]
	string DatabaseUri { get; set; }

	/// <summary>
	/// v2 起用 SQLite。默认落在 run\data\pemukul.sqlite（相对工作目录）。
	/// DatabaseUri 保留只为兼容老 config.json，不再使用。
	/// </summary>
	[Option(DefaultValue = @"data\pemukul.sqlite")]
	string DatabaseFile { get; set; }

	[Option]
	IGameserver Gameserver { get; set; }

	[Option]
	IHttp Http { get; set; }
}
