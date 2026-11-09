using System;
using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;

namespace Common.Database;

public class User
{
	public static readonly SqlTable<UserScheme> collection = Global.Table<UserScheme>("Users", ("uid", "INTEGER", x => x.Uid, true), ("name", "TEXT", x => x.Name, true), ("token", "TEXT", x => x.Token, true));

	public static UserScheme Create(string name)
	{
		string name2 = name;
		UserScheme userScheme = (from d in collection.All()
			where d.Name == name2
			select d).FirstOrDefault();
		if (userScheme != null)
		{
			return userScheme;
		}
		UserScheme userScheme2 = new UserScheme();
		userScheme2.Name = name2;
		userScheme2.Uid = (uint)AutoIncrement.GetNextNumber("UID", 1000);
		userScheme2.Nick = "";
		userScheme2.Exp = 0;
		userScheme2.Hcoin = 0;
		userScheme2.Stamina = 80;
		userScheme2.SelfDesc = "";
		userScheme2.IsFirstLogin = true;
		userScheme2.Token = Guid.NewGuid().ToString();
		userScheme2.WarshipId = 0;
		userScheme2.WarshipAvatar = new WarshipAvatarData
		{
			WarshipFirstAvatarId = 101u,
			WarshipSecondAvatarId = 0u
		};
		userScheme2.CustomHeadId = 161001;
		userScheme2.FrameId = 200001;
		userScheme2.AssistantAvatarId = 101;
		userScheme2.BirthDate = 0;
		userScheme2.AbyssDynamicHard = 100u;
		userScheme2.AbyssGroupLevel = 8u;
		userScheme2.AvatarTeamList = new List<AvatarTeam>
		{
			new AvatarTeam
			{
				AvatarIdLists = new uint[1] { 101u },
				StageType = 1u
			}
		};
		userScheme2.CustomAvatarTeamList = new List<CustomAvatarTeam>();
		UserScheme userScheme3 = userScheme2;
		collection.InsertOne(userScheme3);
		return userScheme3;
	}

	public static UserScheme FromName(string name)
	{
		string name2 = name;
		UserScheme userScheme = (from d in collection.All()
			where d.Name == name2
			select d).FirstOrDefault();
		return userScheme ?? Create(name2);
	}

	public static UserScheme? FromToken(string token)
	{
		string token2 = token;
		return (from d in collection.All()
			where d.Token == token2
			select d).FirstOrDefault();
	}

	public static UserScheme? FromUid(uint uid)
	{
		return (from d in collection.All()
			where d.Uid == uid
			select d).FirstOrDefault();
	}
}
