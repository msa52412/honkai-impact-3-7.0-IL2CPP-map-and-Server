using System;
using Common.Database;
using Common.Utils;
using Config.Net;

namespace Common;

public static class Global
{
	public static readonly IConfig config = new ConfigurationBuilder<IConfig>().UseJsonFile("config.json").Build();

	public static readonly Logger c = new Logger("Global");

	/// <summary>
	/// SQLite 存储（v2 起替代 MongoDB）。
	/// 数据库文件默认 data\pemukul.sqlite，可在 config.json 的 DatabaseFile 里改。
	/// </summary>
	public static readonly SqliteStore Store = new SqliteStore(config.DatabaseFile);

	/// <summary>
	/// 取一张表。同名表只建一次，之后复用同一个 SqlTable 实例。
	/// cols 是「业务键列」：只用于唯一约束和人工排查，过滤仍然走内存 LINQ。
	/// </summary>
	public static SqlTable<T> Table<T>(string name,
		params (string column, string type, Func<T, object> get, bool unique)[] cols)
		where T : class, IRow, new()
	{
		object cached = Store.GetCached(name);
		if (cached is SqlTable<T> hit)
		{
			return hit;
		}
		SqlTable<T> table = new SqlTable<T>(Store, name, cols);
		Store.Cache(name, table);
		return table;
	}

	public static long GetUnixInSeconds()
	{
		return ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
	}

	public static uint GetRandomSeed()
	{
		return (uint)(GetUnixInSeconds() * new Random().Next(1, 10) / 10);
	}
}
