using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Database;

public class Login
{
	public static readonly SqlTable<LoginScheme> collection = Global.Table<LoginScheme>("Logins", ("owner_uid", "INTEGER", x => x.OwnerUid, false));

	public static bool UserLogin(uint Uid)
	{
		LoginScheme userLastLogin = GetUserLastLogin(Uid);
		bool newDay = userLastLogin != null && LocalDate(userLastLogin) < DateTime.Now.Date;
		collection.InsertOne(new LoginScheme
		{
			OwnerUid = Uid
		});
		return newDay;
	}

	/// <summary>行创建时间的本地日期。原来读的是 Mongo ObjectId 内嵌的时间戳。</summary>
	private static DateTime LocalDate(LoginScheme login)
	{
		return DateTimeOffset.FromUnixTimeSeconds(login.CreatedAt).LocalDateTime.Date;
	}

	public static List<LoginScheme> GetUserLogins(uint Uid)
	{
		return (from login in collection.All()
			where login.OwnerUid == Uid
			select login).ToList();
	}

	public static LoginScheme? GetUserLastLogin(uint Uid)
	{
		// 只要「有时间戳」的最后一条：v1 迁移过来的老记录 CreatedAt 是 0，
		// 拿它判断 newDay 会永远得到 1970-01-01 < 今天 = true。
		return GetUserLogins(Uid).Where(x => x.CreatedAt > 0).LastOrDefault();
	}

	/// <summary>
	/// 累计登录天数（按本地日期去重，同一天登录多次只算一天）。
	///
	/// 必须跳过 CreatedAt &lt;= 0 的行：v1 → v2 迁移进来的 160 多条老记录没有时间戳，
	/// 换算出来全是 1970-01-01，DistinctBy 之后会平白多算一天，
	/// 授勋界面就会显示「已登录 9 天」这种和实际对不上的数字。
	/// </summary>
	public static uint GetUserLoginDays(uint Uid)
	{
		return (uint)GetUserLogins(Uid)
			.Where(x => x.CreatedAt > 0)
			.Select(LocalDate)
			.Distinct()
			.Count();
	}
}
