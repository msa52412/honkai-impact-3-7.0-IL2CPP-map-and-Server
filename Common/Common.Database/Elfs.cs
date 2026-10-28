using System.Collections.Generic;
using System.Linq;

namespace Common.Database;

public class Elfs
{
	public static readonly SqlTable<ElfsScheme> collection = Global.Table<ElfsScheme>("Elfs", ("owner_uid", "INTEGER", x => x.OwnerUid, true));

	public static ElfsScheme FromUid(uint uid)
	{
		return (from collection in collection.All()
			where collection.OwnerUid == uid
			select collection).FirstOrDefault() ?? Create(uid);
	}

	public static ElfsScheme Create(uint uid)
	{
		ElfsScheme elfsScheme = (from collection in collection.All()
			where collection.OwnerUid == uid
			select collection).FirstOrDefault();
		if (elfsScheme != null)
		{
			return elfsScheme;
		}
		ElfsScheme elfsScheme2 = new ElfsScheme
		{
			OwnerUid = uid,
			Elves = new List<ElfsScheme.Elf>()
		};
		collection.InsertOne(elfsScheme2);
		return elfsScheme2;
	}
}
