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
		return GetUserLogins(Uid).LastOrDefault();
	}

	public static uint GetUserLoginDays(uint Uid)
	{
		return (uint)GetUserLogins(Uid).DistinctBy(LocalDate).Count();
	}
}
