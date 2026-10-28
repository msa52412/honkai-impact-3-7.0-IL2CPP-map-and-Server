namespace Common.Database;

/// <summary>
/// 自增序列（UID / Weapon / Stigmata / GrandKey 的编号来源）。
///
/// 保持原 Mongo 版的语义：首次取号返回 starting + 1（不是 starting），之后每次 +1。
/// 整个「读-改-写」放在一个 SQLite 事务里，并发下不会重号 —— 这是 Mongo standalone 做不到的。
/// </summary>
public class AutoIncrement
{
	public class AI : IRow
	{
		public long Id { get; set; }

		public long CreatedAt { get; set; }

		public string Name { get; set; }

		public int Count { get; set; }
	}

	public static readonly SqlTable<AI> collection = Global.Table<AI>("AutoIncrements",
		("name", "TEXT", x => x.Name, true),
		("count", "INTEGER", x => x.Count, false));

	public static int GetNextNumber(string name, int starting = 100)
	{
		int result = 0;
		Global.Store.InTransaction(() =>
		{
			AI row = collection.FirstOrDefault(x => x.Name == name);
			if (row == null)
			{
				// 原实现：upsert 写入 starting，然后 Inc 1，返回 starting + 1
				result = starting + 1;
				collection.InsertOne(new AI { Name = name, Count = result });
			}
			else
			{
				row.Count++;
				collection.ReplaceOne(row);
				result = row.Count;
			}
		});
		return result;
	}
}
